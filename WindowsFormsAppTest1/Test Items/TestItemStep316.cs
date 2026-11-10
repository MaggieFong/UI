using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;
    using NationalInstruments.DAQmx;
    using System.Diagnostics;


    class TestItemStep316_P1 : TestItemForm
    {
        public string step = "_316 P1";
        private DAQadc taskADC;
        private DR2003 DR2003;      //main power
        private BK8601 BK8601;      //E-loader
        private IT6512C IT6512C;    //Battery simulation

        public TestItemStep316_P1()
        {
            sStep = step;
            LoadSpec();            
        }

        public override WORK_STATUS Init()
        {
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Bat_En_Adapter, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Bat_En_Battery, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPChargeFlashCap, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPDischarger, 0);

            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Bat_En_Charging, 1);

            try
            {
                DR2003 = new DR2003(Power_DR2002MO.comPort, Power_DR2002MO.baudRate);

				IT6512C = new IT6512C(Power_IT6512C.usbId);
				IT6512C.Connect();
				IT6512C.SetVOLT(Convert.ToString(Power_IT6512C.V72));   //7.2V
				IT6512C.SetCURR(Convert.ToString(Power_IT6512C.CURR1)); //1A
				IT6512C.SetLoadInternalCURR1P5();
				IT6512C.SetLoadOn();
				IT6512C.OutPutOn();
				IT6512C.Disconnect();
				
				BK8601 = new BK8601(DCLoad_8601.usbId);
				BK8601.Connect();
				BK8601.SetCCMode();
				BK8601.SetCURR("16");
				BK8601.TraceClear();
				BK8601.TracePoints("400");
				BK8601.TraceFeed("CURR");
				BK8601.TraceDelay("MIN");
				BK8601.TriggerSource("BUS");
				BK8601.TraceTim("0.001");
				BK8601.SourceRemoteSense("0");
				BK8601.TraceFeedControl("NEXT");
				BK8601.Disconnect();   
				
                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_N10_10);  //sample rate: 10000hz, sample time: 100ms, voltage range: -10~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP100_AI16);//BAT_VOLT
				taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP102_AI15);//BAT_GND
				taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP319_AI29);//BAT_1/2
        
				System.Threading.Thread.Sleep(200);

		
                if (taskADC.start())
                    return WORK_STATUS.PASS;
                else
                {
                    sMeasured = taskADC.getErrorMsg();
                    return WORK_STATUS.FAIL;
                }
            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
        }
        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            if (taskADC.isEnable() == false)
            {
                if (taskADC.getErrorMsg() == null)      //must check if there were any error when DAQadc.callbackADC()
                {
                    double average = taskADC.getAverage(0);
					double average1 = taskADC.getAverage(1);
					double average2 = taskADC.getAverage(2);
                    sMeasured = Convert.ToString(average);
					Step316_OutPut.s316_P2_re = Convert.ToString(average1);
					Step316_OutPut.s316_P3_re = Convert.ToString(average2);
					Console.WriteLine("ai16 average:{0}", average);

                    //check limitation
                    if (PassFailJudge())
                    {
                        if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                        {
                            if (!offsetCalculate())
                                return WORK_STATUS.FAIL;
                        }

                        re = WORK_STATUS.PASS;
                    }
                    else {
                        re = WORK_STATUS.FAIL;
                    }
                }
                else
                {
                    sMeasured = taskADC.getErrorMsg();
                    re = WORK_STATUS.FAIL;
                }
            }
            return re;
        }

        public override void release()
        {
            taskADC = null;        //free memory
            DR2003 = null;
            IT6512C = null;
			BK8601 = null;
        }
    }
    class TestItemStep316_P2 : TestItemForm
    {
        public string step = "_316 P2";

        public TestItemStep316_P2()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {

            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {

            sMeasured = Step316_OutPut.s316_P2_re;
        
            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                return WORK_STATUS.PASS;
            }
            else
            {
                Step316_OutPut.Savelog_Enable = true;
                return WORK_STATUS.FAIL;
            }
           
        }

        public override void release()
        {

        }
    }
    class TestItemStep316_P3 : TestItemForm
    {
        public string step = "_316 P3";

        public TestItemStep316_P3()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {

            sMeasured = Step316_OutPut.s316_P3_re;

            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                return WORK_STATUS.PASS;
            }
            else
            {
                Step316_OutPut.Savelog_Enable = true;
                return WORK_STATUS.FAIL;
            }

        }

        public override void release()
        {

        }
    }
    /// <summary>
    /// /////////////////////////////////////////////
    /// </summary>
    class TestItemStep316_P4 : TestItemForm
    {
        public string step = "_316 P4";
        private DAQadc taskADC;
        private IT6512C IT6512C;
        //private DAQdio dio;

        public TestItemStep316_P4()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            IT6512C = new IT6512C(Power_IT6512C.usbId);
            IT6512C.Connect();
			IT6512C.SetVOLT(Convert.ToString(Power_IT6512C.V45));
			IT6512C.OutPutOn();
			IT6512C.Disconnect();
            System.Threading.Thread.Sleep(300);

            try
            {
                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_N10_10);  
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP102_AI15); //BAT_GND
				taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP100_AI16);//BAT_VOLT
                if (taskADC.start())
                    return WORK_STATUS.PASS;
                else
                {
                    sMeasured = taskADC.getErrorMsg();
                    return WORK_STATUS.FAIL;
                }
            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
            
        }
        public override WORK_STATUS Proccess()
        {
            
            WORK_STATUS re = WORK_STATUS.WORKING;
            if (taskADC.isEnable() == false)
            {
                if (taskADC.getErrorMsg() == null)      //must check if there were any error when DAQadc.callbackADC()
                {
                    double average = taskADC.getAverage(0);
                    sMeasured = Convert.ToString(average);
					double average1 = taskADC.getAverage(1);
                    Step316_OutPut.s316_P5_re = Convert.ToString(average1);
                    Console.WriteLine("ai15 average:{0}", average);

                    //check limitation
                    if (PassFailJudge())
                    {
                        if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                        {
                            if (!offsetCalculate())
                                return WORK_STATUS.FAIL;
                        }

                        re = WORK_STATUS.PASS;
                    }
                    else
                    {
                        re = WORK_STATUS.FAIL;
                    }
                }
                else
                {
                    sMeasured = taskADC.getErrorMsg();
                    re = WORK_STATUS.FAIL;
                }
            }
            return re;
        }

        public override void release()
        {
            taskADC = null;        //free memory
            IT6512C = null;


        }
    }
    class TestItemStep316_P5 : TestItemForm
    {
        public string step = "_316 P5";

        public TestItemStep316_P5()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {

            sMeasured = Step316_OutPut.s316_P5_re;

            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                return WORK_STATUS.PASS;
            }
            else
            {
                Step316_OutPut.Savelog_Enable = true;
                return WORK_STATUS.FAIL;
            }

        }

        public override void release()
        {

        }
    }
    class TestItemStep316_P6 : TestItemForm
    {
        public string step = "_316 P6";
        private DAQadc taskADC;
        private DAQdio dio;
        private IT6512C IT6512C;
        public TestItemStep316_P6()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {


            //DIO setting
            try
            {
				IT6512C = new IT6512C(Power_IT6512C.usbId);
				IT6512C.Connect();
				IT6512C.SetVOLT(Convert.ToString(Power_IT6512C.V65));
				IT6512C.OutPutOn();
				IT6512C.Disconnect();
				
                dio = new DAQdio(IO_USB6525_B.deviceName);
                dio.addOneLineWriteCH(IO_USB6525_B.Relay_VBAT_GND_TP109_TP102_K10_P01);
                //dio.setOneData(false);
            }
            catch (Exception e)
            {

                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
            try
            {
                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_N10_10);  //sample rate: 10000hz, sample time: 100ms, voltage range: -10~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP102_AI15);

                if (taskADC.start())
                    return WORK_STATUS.PASS;
                else
                {
                    sMeasured = taskADC.getErrorMsg();
                    return WORK_STATUS.FAIL;
                }
            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
        }
        public override WORK_STATUS Proccess()
        {
            
              WORK_STATUS re = WORK_STATUS.WORKING;
            if (taskADC.isEnable() == false)
            {
                if (taskADC.getErrorMsg() == null)      //must check if there were any error when DAQadc.callbackADC()
                {
                    double average = taskADC.getAverage(0);
                    sMeasured = Convert.ToString(average);
                    Console.WriteLine("ai15 average:{0}", average);

                    //check limitation
                    if (PassFailJudge())
                    {
                        if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                        {
                            if (!offsetCalculate())
                                return WORK_STATUS.FAIL;
                        }

                        re = WORK_STATUS.PASS;
                    }
                    else
                    {
                        re = WORK_STATUS.FAIL;
                    }
                }
                else
                {
                    sMeasured = taskADC.getErrorMsg();
                    re = WORK_STATUS.FAIL;
                }
            }
            return re;
        }

        public override void release()
        {
            taskADC = null;        //free memory
			dio.setOneData(true);
            dio = null;
            IT6512C = null;
        }
    }
    class TestItemStep316_P7 : TestItemForm
    {
        public string step = "_316 P7";
        private DAQadc taskADC;
        private DAQdio dio;
 
        public TestItemStep316_P7()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            
            //DIO setting
			
            try
            {
                dio = new DAQdio(IO_USB6525_B.deviceName);
                dio.addOneLineWriteCH(IO_USB6525_B.Relay_VBAT_GND_TP109_TP102_K10_P01);
                
            }
            catch (Exception e)
            {

                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
            try
            {
                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_N10_10);  //sample rate: 10000hz, sample time: 100ms, voltage range: -10~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP102_AI15);

                if (taskADC.start())
                    return WORK_STATUS.PASS;
                else
                {
                    sMeasured = taskADC.getErrorMsg();
                    return WORK_STATUS.FAIL;
                }
            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
        }
        public override WORK_STATUS Proccess()
        {
            //System.Threading.Thread.Sleep(5000);

            WORK_STATUS re = WORK_STATUS.WORKING;
            if (taskADC.isEnable() == false)
            {
                if (taskADC.getErrorMsg() == null)      //must check if there were any error when DAQadc.callbackADC()
                {
                    double average = taskADC.getAverage(0);
                    sMeasured = Convert.ToString(average);
                    Console.WriteLine("ai15 average:{0}", average);

                    //check limitation
                    if (PassFailJudge())
                    {
                        if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                        {
                            if (!offsetCalculate())
                                return WORK_STATUS.FAIL;
                        }

                        re = WORK_STATUS.PASS;
                    }
                    else
                    {
                        re = WORK_STATUS.FAIL;
                    }
                }
                else
                {
                    sMeasured = taskADC.getErrorMsg();
                    re = WORK_STATUS.FAIL;
                }
            }
            return re;
        }

        public override void release()
        {
            System.Threading.Thread.Sleep(100);
            dio.setOneData(false);
            taskADC = null;        //free memory
            dio = null;
            
        }
    }
    class TestItemStep316_P8 : TestItemForm
    {
        public string step = "_316 P8";
        private DAQadc taskADC;
        private IT6512C IT6512C;

        public TestItemStep316_P8()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
      
            try
            {
				IT6512C = new IT6512C(Power_IT6512C.usbId);
				IT6512C.Connect();
				IT6512C.SetVOLT(Convert.ToString(Power_IT6512C.V72));   //v7.2
				IT6512C.OutPutOn();
				IT6512C.Disconnect();
				System.Threading.Thread.Sleep(200);
                
				taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_N10_10);  //sample rate: 10000hz, sample time: 100ms, voltage range: 10~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP102_AI15);

                if (taskADC.start())
                    return WORK_STATUS.PASS;
                else
                {
                    sMeasured = taskADC.getErrorMsg();
                    return WORK_STATUS.FAIL;
                }
            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
        }
        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            if (taskADC.isEnable() == false)
            {
                if (taskADC.getErrorMsg() == null)      //must check if there were any error when DAQadc.callbackADC()
                {
                    double average = taskADC.getAverage(0);
                    sMeasured = Convert.ToString(average);
                    Console.WriteLine("ai15 average:{0}", average);

                    //check limitation
                    if (PassFailJudge())
                    {
                        if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                        {
                            if (!offsetCalculate())
                                return WORK_STATUS.FAIL;
                        }

                        re = WORK_STATUS.PASS;
                    }
                    else
                    {
                        re = WORK_STATUS.FAIL;
                    }
                }
                else
                {
                    sMeasured = taskADC.getErrorMsg();
                    re = WORK_STATUS.FAIL;
                }
            }
            return re;
        }

        public override void release()
        {
            taskADC = null;        //free memory
            
            IT6512C = null;
        }
    }
    class TestItemStep316_P9 : TestItemForm
    {
        public string step = "_316 P9";
        private BK8601 BK8601;
        private IT6512C IT6512C;

        public TestItemStep316_P9()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            IT6512C = new IT6512C(Power_IT6512C.usbId);
            IT6512C.Connect();
			IT6512C.SetLoadOff();
			IT6512C.OutPutOn();
			IT6512C.Disconnect();

            BK8601 = new BK8601(DCLoad_8601.usbId);
            BK8601.Connect();
            BK8601.TriggerStart();
			System.Threading.Thread.Sleep(100);
			BK8601.InPutOn();			
			System.Threading.Thread.Sleep(400);
			//BK8601.InPutOn();	
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            string result;

            result = BK8601.ReadBuffer(400 * 8);
            BK8601.Disconnect();
            string[] value = result.Split(',');
            int aryLength = value.Length;
            int num = 0;
            double Max_value = 0;
            double current_value;
            while (num < aryLength)
            {
                current_value = Convert.ToDouble(value[num]);
                if (current_value > Max_value)
                    Max_value = current_value;
                //DisplayToTextBoxBuffer($"{value[num]}");
                Console.WriteLine(value[num]);
                num++;
            }
            Console.WriteLine("The Max Value : {0}", Max_value);
            sMeasured = Convert.ToString(Max_value);
            //check limitation
            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }
        }

        public override void release()
        {
            IT6512C = null;
            BK8601 = null;        //free memory
        }
    }
    class TestItemStep316_P10 : TestItemForm
    {
        public string step = "_316 P10";
        private BK8601 BK8601;

        public TestItemStep316_P10()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            BK8601 = new BK8601(DCLoad_8601.usbId);

            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;

            
            BK8601.Connect();
            sMeasured = BK8601.MeasureCURR();
            BK8601.Disconnect();
             
            //check limitation
            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                re = WORK_STATUS.PASS;
            }
            else
            {
                re = WORK_STATUS.FAIL;
            }
            return re;
        }

        public override void release()
        {
            BK8601 = null;        //free memory
        }
    }
    class TestItemStep316_P11 : TestItemForm
    {
        public string step = "_316 P11";
        private DAQadc taskADC;
        private BK8601 BK8601;
        private IT6512C IT6512C;

        public TestItemStep316_P11()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            IT6512C = new IT6512C(Power_IT6512C.usbId);
            BK8601 = new BK8601(DCLoad_8601.usbId);

            BK8601.Connect();
            BK8601.InPutOff();
            BK8601.Disconnect();

            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Bat_En_Charging, 0);
            System.Threading.Thread.Sleep(10);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Bat_En_Charging, 1);

            try
            {
                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_N10_10);  //sample rate: 10000hz, sample time: 100ms, voltage range: -10~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP102_AI15);

                if (taskADC.start())
                    return WORK_STATUS.PASS;
                else
                {
                    sMeasured = taskADC.getErrorMsg();
                    return WORK_STATUS.FAIL;
                }
            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
        }
        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            if (taskADC.isEnable() == false)
            {
                if (taskADC.getErrorMsg() == null)      //must check if there were any error when DAQadc.callbackADC()
                {
                    double average = taskADC.getAverage(0);
                    sMeasured = Convert.ToString(average);
                    Console.WriteLine("ai15 average:{0}", average);

                    //check limitation
                    if (PassFailJudge())
                    {
                        if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                        {
                            if (!offsetCalculate())
                                return WORK_STATUS.FAIL;
                        }

                        re = WORK_STATUS.PASS;
                    }
                    else
                    {
                        re = WORK_STATUS.FAIL;
                    }
                }
                else
                {
                    sMeasured = taskADC.getErrorMsg();
                    re = WORK_STATUS.FAIL;
                }
            }
            return re;
        }

        public override void release()
        {
            IT6512C.Connect();
            IT6512C.OutPutOff();
            IT6512C.Disconnect();

            IT6512C = null;
            BK8601 = null;
            taskADC = null;        //free memory
        }
    }

}
