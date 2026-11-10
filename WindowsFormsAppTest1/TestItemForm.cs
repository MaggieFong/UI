namespace TestItemForm
{
    using System;
    using WindowsFormsAppTest1.TestItem;
    using IniFileAccess;
    using System.IO;
    using WindowsFormsAppTest1;

    public abstract class TestItemForm
    {
        public enum WORK_STATUS
        {
            WORKING,
            FAIL,
            PASS
        };

        public enum PASSFAIL_JUDGE_TYPE
        {
            JUST_HAVE_VALUE,
            HIGH_LOW_LIMIT,
            HI_LO_STATE,
            PROCESS_ONLY
        };

        public enum CALIBRATED_OP_CODE
        {
            ADD,
            SUBTRACT,
            MULTIPLY,
            DIVID
        };

        public string sStep;
        public string sTest;
        public string sOffset;
        public string sMeasured;
        private string sCalibratedValue;
        public string sExpected;
        public string sType;
        public string sLowerLimit;
        public string sUpperLimit;
        public string sErrorCode;
        public string sOffsetLimit;
        public string sGoldenValue;
        public bool bResult;
        public bool bNeedOffset;
        private PASSFAIL_JUDGE_TYPE eType;
        private CALIBRATED_OP_CODE eOp_code;

        public abstract WORK_STATUS Init();
        public abstract WORK_STATUS Proccess();
        public abstract void release();

        public bool PassFailJudge()
        {
            if (eType == PASSFAIL_JUDGE_TYPE.JUST_HAVE_VALUE) // 有值就pass
            {
                if (sMeasured == null)
                    return false;
            }
            else if (eType == PASSFAIL_JUDGE_TYPE.HIGH_LOW_LIMIT || eType == PASSFAIL_JUDGE_TYPE.HI_LO_STATE)
            { //type1 --數值上下限; type2 --High/Low; 
                try
                {
                    double HLimit = Convert.ToDouble(sUpperLimit);
                    double LLimit = Convert.ToDouble(sLowerLimit);
                    double measureVal = Convert.ToDouble(sMeasured);

                    //Console.WriteLine("Measured raw value = {0}", sMeasured);

                    if (eType == PASSFAIL_JUDGE_TYPE.HIGH_LOW_LIMIT)
                    {
                        // Tester calibration
                        if (sCalibratedValue != "0" || sCalibratedValue != null)
                        {
                            //Console.WriteLine("Calibrated value = {0}", sCalibratedValue);
                            double caliVal = Convert.ToDouble(sCalibratedValue);
                            
                            switch (eOp_code)
                            {
                                case CALIBRATED_OP_CODE.ADD:
                                    measureVal += caliVal;
                                    break;

                                case CALIBRATED_OP_CODE.SUBTRACT:
                                    measureVal -= caliVal;
                                    break;

                                case CALIBRATED_OP_CODE.MULTIPLY:
                                    measureVal *= caliVal;
                                    break;

                                case CALIBRATED_OP_CODE.DIVID:
                                    measureVal /= caliVal;
                                    break;
                            }

                            sMeasured = Convert.ToString(measureVal);
                        }

                        // Measurement offset
                        if (Golden.verified == true && bNeedOffset == true)
                        {
                            Console.WriteLine("Offset value = {0}", sOffset);
                            double offsetVal = Convert.ToDouble(sOffset);
                            measureVal += offsetVal;
                            sMeasured = Convert.ToString(measureVal);
                        }
                    }

                    Console.WriteLine("PassFailJudge:  {0} <=  val {1} <=  {2}", LLimit, measureVal, HLimit);
                    if (measureVal < LLimit || measureVal > HLimit)
                    {
                        return false;
                    }
                }
                catch (FormatException)
                {
                    //Console.WriteLine("Unable to convert ");
                    return false;
                }
                catch (OverflowException)
                {
                    //Console.WriteLine("is outside the range of a Double.");
                    return false;
                }
            }

            return true;
        }

        public bool offsetCalculate()
        {
            bool ret;

            if (Golden.empertyData != true)
            {
                if (sOffsetLimit != "N/A")
                {
                    double offsetValue = Convert.ToDouble(sGoldenValue) - Convert.ToDouble(sMeasured);
                    sOffset = offsetValue.ToString("0.0000");
                }
                else
                {
                    sOffset = "0";
                }
            }
            else
            {
                sOffset = "0";
            }

            ret = OffsetJudge();
            return ret;
        }

        private bool OffsetJudge()
        {
            if (eType == PASSFAIL_JUDGE_TYPE.HIGH_LOW_LIMIT && Golden.empertyData == false && sOffsetLimit != "N/A")
            {
                try
                {
                    if (Math.Abs(Convert.ToDouble(sOffset)) > Convert.ToDouble(sOffsetLimit))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch
                {
                    Console.WriteLine("sOffset = {0}, sOffsetLimit = {1}", sOffset, sOffsetLimit);
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public void LoadSpec()
        {
            TIniFile iniSpec = new TIniFile(Config.fileSpec);
            string sectionName = "TestStep/" + sStep;
            sTest = iniSpec.iniReadValue(sectionName, "Test", null);
            sType = iniSpec.iniReadValue(sectionName, "Type", null);
            sLowerLimit = iniSpec.iniReadValue(sectionName, "Lower Limit", null);
            sUpperLimit = iniSpec.iniReadValue(sectionName, "Upper Limit", null);
            sOffsetLimit = iniSpec.iniReadValue(sectionName, "Offset Limit", "N/A");
            sErrorCode = iniSpec.iniReadValue(sectionName, "Error Code", "N/A");
            sOffset = "";
            sGoldenValue = "N/A";
            bResult = false;
            bNeedOffset = (sOffsetLimit == "N/A")? false : true;

            switch (sType)
            {
                case "0":
                    eType = PASSFAIL_JUDGE_TYPE.JUST_HAVE_VALUE;
                    break;

                case "1":
                    eType = PASSFAIL_JUDGE_TYPE.HIGH_LOW_LIMIT;
                    break;

                case "2":
                    eType = PASSFAIL_JUDGE_TYPE.HI_LO_STATE;
                    break;

                case "3":
                    eType = PASSFAIL_JUDGE_TYPE.PROCESS_ONLY;
                    break;
            }
        }

        public void LoadCalibratedValue(string fixture)
        {
            TIniFile iniCalibration = new TIniFile(Config.fileCalibration);
            string sectionName = fixture;
            string Operator;
            string calData = "+,0";
            calData = iniCalibration.iniReadValue(sectionName, sStep, "+,0");
            string[] calDataSplit = calData.Split(',');
            Operator = calDataSplit[0];
            sCalibratedValue = calDataSplit[1];

            switch (Operator)
            {
                case "+":
                    eOp_code = CALIBRATED_OP_CODE.ADD;
                    break;

                case "-":
                    eOp_code = CALIBRATED_OP_CODE.SUBTRACT;
                    break;

                case "*":
                    eOp_code = CALIBRATED_OP_CODE.MULTIPLY;
                    break;

                case "/":
                    eOp_code = CALIBRATED_OP_CODE.DIVID;
                    break;
            }
        }
    }
    //public abstract class TestItemForm1
    //{
    //}
}

