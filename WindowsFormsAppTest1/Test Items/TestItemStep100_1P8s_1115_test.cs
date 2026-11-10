using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;

    class TestItemStep100_P1 : TestItemForm
    {
        public string step = "_100 P1";
        private DR2003 DR2003;      //main power
        private DAQdio dio;
        private DAQdio dioB;
        private DAQadc taskADC;
        private double r1 = 3000.0;
        private double r2 = 47.0;
        private bool NIADC_start = false;
        private int BeginstopchargingIDX = 0;
        private int EndstopchargingIDX = 0;
        private bool Beginstopcharging = false;
        private bool Endstopcharging = false;
        //private double P400_RANGE_H = 7.0;
        //private double P400_RANGE_L = -0.5;
        //private double N400_RANGE_H = 0.5;
        //private double N400_RANGE_L = -7.0;
        private int testcount =0;
        private int samplerate = 10000;//0.1ms
        //private int samplerate = 2000;//0.1ms
        public TestItemStep100_P1()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            Step100_OutPut.Savelog_Enable = false;
            sMeasured = "";
            if (preCharging()) 
            {
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPChargeFlashCap, 1);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPDischarger, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_ON, 1); //##
                //System.Threading.Thread.Sleep(30);
                return WORK_STATUS.PASS;
            }
            testcount++;
            if (testcount > 40)// 4 seconds time out
            {
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPChargeFlashCap, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_ON, 0);
                return WORK_STATUS.FAIL;
            }

            return WORK_STATUS.WORKING;
        }

        public override WORK_STATUS Proccess()
        {
            
            if (!NIADC_start)
            {
                try
                {
                    //taskADCb.start();
                    taskADC.start();
                    NIADC_start = true;
                    if (!ReadADC())
                    {
                        return WORK_STATUS.FAIL;
                    }
                }
                catch (Exception e)
                {
                    sMeasured = e.Message;
                    return WORK_STATUS.FAIL;
                }
            }

            if (taskADC.isEnable() == false && NIADC_start )
            {
                if (taskADC.getErrorMsg() == null)
                {

                    StaticValues.s100_NI_VoltFlashCap = taskADC.getRawData(3);
                    StaticValues.s100_NI_N_VoltLamp = taskADC.getRawData(0);
                    StaticValues.s100_NI_P_VoltLamp = taskADC.getRawData(2);
                    //Console.WriteLine("3V3 =" + taskADC.getRawData(1));
                    //Console.WriteLine("AI29 =" + taskADC.getRawData(2));
                    Check_VFC(StaticValues.s100_VoltFlashCap);
                    Check_VLamp(StaticValues.s100_VoltLamp);
                    Check_NIdata(StaticValues.s100_NI_VoltFlashCap, StaticValues.s100_NI_P_VoltLamp, StaticValues.s100_NI_N_VoltLamp);
                    //Check_NIdata(StaticValues.s100_NI_N_VoltLamp, StaticValues.s100_NI_N_VoltLamp, StaticValues.s100_NI_N_VoltLamp);

                    sMeasured = Step100_OutPut.s100_P1_re;
                    
                    //check limitation
                    if (!PassFailJudge())
                    {
                        //Step100_OutPut.Savelog_Enable = true;
                        return WORK_STATUS.FAIL;
                    }

                    if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                    {
                        if (!offsetCalculate())
                            return WORK_STATUS.FAIL;
                    }

                    return WORK_STATUS.PASS;
                }
            }

            return WORK_STATUS.WORKING;
        }

        public override void release()
        {
            testcount = 0;
            dio.setOneData(false);
            dioB.setOneData(false);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_LampPWM, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPChargeFlashCap, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_ON, 0); //##

            dio = null;
            dioB = null;
            taskADC = null;
            //taskADCb = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            DR2003 = null;
            NIADC_start = false;
            Beginstopcharging = false;
            Endstopcharging = false;


        }
        private bool preCharging()
        {
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPChargeFlashCap, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPDischarger, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_ON, 1);//## // start charge
            StaticValues.hwTestRPCDevice.ConfigurePin(StaticValues.uC_FlashCapVoltage,
                              HardwareTestRPCSerialDevice.PinTypeValue.AdcPin,
                              HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);
            //if (StaticValues.hwTestRPCDevice.ConfigureADC(0, 0, 2, 2))
            //{
            //    //Console.WriteLine("ConfigureADC AdcPin pass ");

            //}
            ushort data = 0;
            ushort data1 = 0;
            StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_FlashCapVoltage, ref data);
            StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_LampVoltage, ref data1);
            double ADCFlashCap = 1.0 * (double)data / 4095;
            double FlashCapVoltage = ADCFlashCap * ((3000 + 6.8) / 6.8);
            Console.WriteLine("ADC =  " + ADCFlashCap + " ; volt =  " + FlashCapVoltage);
            double ADCLamp = 1.0 * (double)data1 / 4095;
            double LampVoltage = ADCLamp * ((600 + 1) / 1);
            Console.WriteLine("ADC =  " + ADCLamp + " ; LampVoltage =  " + LampVoltage);
            if (FlashCapVoltage > 50)
            {
                //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPChargeFlashCap, 0);
                System.Threading.Thread.Sleep(30);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_ON, 0); //##
                Init_Eq();
                StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_FlashCapVoltage, ref data);
                StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_LampVoltage, ref data1);
                ADCFlashCap = 1.0 * (double)data / 4095;
                FlashCapVoltage = ADCFlashCap * ((3000 + 6.8) / 6.8);
                Console.WriteLine("ADC =  " + ADCFlashCap + " ; volt =  " + FlashCapVoltage);
                ADCLamp = 1.0 * (double)data1 / 4095;
                LampVoltage = ADCLamp * ((600 + 1) / 1);
                Console.WriteLine("ADC =  " + ADCLamp + " ; LampVoltage =  " + LampVoltage);
                //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPChargeFlashCap, 1);
                return true;
            }
            
            return false;
        }
        private void Init_Eq()
        {

            try
            {
                dio = new DAQdio(IO_USB6525_A.deviceName);
                dio.addOneLineWriteCH(IO_USB6525_A.Relay_FlashButton_TP515_K6_P05); // flash button
                dioB = new DAQdio(IO_USB6525_B.deviceName);
                dioB.addOneLineWriteCH(IO_USB6525_B.AISENSE_K11_P02);
                dioB.setOneData(true);
                //Discharge is disabled, Switch Q600 is closed, and 19.5V supply is ON.
                DR2003 = new DR2003(Power_DR2002MO.comPort, Power_DR2002MO.baudRate);
                DR2003.Connect();
                DR2003.SetCURR(Convert.ToString(Power_DR2002MO.FLASHCAP_CURR5));   //6A
                DR2003.Disconnect();

                taskADC = new DAQadc(
                samplerate, 1800, DAQadc.VOLTAGE_RANGE.RANGE_N10_10);
                taskADC.addNChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP103_AI2);
                taskADC.addNChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP105_AI7);
                taskADC.addNChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP101_AI1);
                taskADC.addNChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP104_AI0);

            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                Console.WriteLine("Init_Eq Exception");
            }

        }
        private bool ReadADC()
        {
            bool get_Charge60V_time = false;
            bool get_Charge380V_time = false;
            bool IGBT_done = false;
            double Charge60V_time = 0;
            double Charge380V_time = 0;
            double startT = 0.2;
            double stopT = startT + 0.5;
            double convOffstartT = 0.4;
            double convOffstopT =   0.65;
            var FlashCapVList = new List<double>();
            var LampVaList = new List<double>();
            //bool pinstate = false;
            long dStartTime;
            long delta;
            int cnt = 0;
            BeginstopchargingIDX = 0;
            EndstopchargingIDX = 0;
            //ushort _data = 0;
            //StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_FlashCapVoltage, ref _data);
            //double _ADCFlashCap = 1.0 * (double)_data / 4095;
            //double _FlashCapVoltage = _ADCFlashCap * ((3000 + 6.8) / 6.8);
            //Console.WriteLine("ReadADC flashcap volt = " + _FlashCapVoltage);
            
            for (double x = 0.0; x < 2.15; x += 0.05)// 50ms
                //for (double x = 0.0; x < 4; x += 0.05)// 50ms
            {
                cnt++;
                ////Console.WriteLine("cnt = " + cnt);
                dStartTime = DateTime.Now.Ticks;
                ushort data = 0;
                ushort data1 = 0;
                StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_FlashCapVoltage, ref data);
                StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_LampVoltage, ref data1);
                if (data == 0)
                {
                    StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_FlashCapVoltage, ref data);
                }
                if (data1 == 0)
                {
                    StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_LampVoltage, ref data1);
                }
                double ADCFlashCap = 1.0 * (double)data / 4095;
                double FlashCapVoltage = ADCFlashCap * ((3000 + 6.8) / 6.8);
                Console.WriteLine("x = " + x);
                Console.WriteLine("ADC =  " + ADCFlashCap + " ; FlashCapVoltage =  " + FlashCapVoltage);
                FlashCapVList.Add(FlashCapVoltage);

                double ADCLamp = 1.0 * (double)data1 / 4095;
                double LampVoltage = ADCLamp * ((600 + 1) / 1);
                LampVaList.Add(LampVoltage);
                Console.WriteLine("ADC =  " + ADCLamp + " ; LampVoltage =  " + LampVoltage);

                //The charge time:t@60V and t@380V minus 0.5 sec
                if (FlashCapVoltage > 60 && !get_Charge60V_time)
                {
                    get_Charge60V_time = true;
                    Charge60V_time = x;
                    Console.WriteLine("60V time  {0}", x);
                }
                if (FlashCapVoltage > 380 && !get_Charge380V_time)
                {
                    get_Charge380V_time = true;
                    Charge380V_time = x;
                    Console.WriteLine("380V time  {0}", x);
                }

                //for satety
                if (!Check_Safety(LampVoltage)) //// FlashCapVoltage --> LampVoltage
                {
                    return false;
                }
                
                if (x < startT || x > stopT)
                {
                    ////Console.WriteLine("Charging");
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPChargeFlashCap, 1);
                    
                    if (Beginstopcharging && !Endstopcharging)
                    {
                        Endstopcharging = true;
                        EndstopchargingIDX = cnt;
                        //Console.WriteLine(" Charging idx = " + EndstopchargingIDX);
                    }

                }
                else
                {
                    /*test neglamp and IGBT*/
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPChargeFlashCap, 0);
                    if (x > convOffstartT && x < convOffstopT) // 0.4~0.6s
                    {
                        StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_ON, 0);
                        Console.WriteLine("uC_CONV_ON off");
                    }
                    else
                    {
                        StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_ON, 1); //## //start charge
                        Console.WriteLine("uC_CONV_ON on");
                    }
                    if (!Beginstopcharging)
                    {
                        Beginstopcharging = true;
                        BeginstopchargingIDX = cnt;
                        //Console.WriteLine("Not  Charging idx = " + BeginstopchargingIDX);
                    }
                }
                
                if (x > 1.5 && !IGBT_done)
                {//test neg lamp and IGBT
                    ////Console.WriteLine("x > 3");
                    IGBT_done = true;

                    StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_LampVoltage, ref data1);

                    // ADCLamp = 1.0 * (double)data1 / 4095;
                    // LampVoltage = ADCLamp * ((600 + 1) / 1);

                    //Console.WriteLine("ADC =  " + ADCLamp + " ; LampVoltage =  " + LampVoltage);

                    dio.setOneData(true);  //// Swap ????
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_LampPWM, 1);
                }
                delta = DateTime.Now.Ticks - dStartTime;
                float testTime = (float)(delta / 10000);
                Console.WriteLine("testTime = " + testTime);
                if (testTime < 50)
                {
                    int delayTime = Convert.ToInt32(50.0 - testTime);
                    Console.WriteLine("delay T = " + delayTime.ToString());
                    System.Threading.Thread.Sleep(delayTime);
                }
            }

            Check_P1_chargerTime(Charge60V_time, Charge380V_time);
            StaticValues.s100_VoltFlashCap = FlashCapVList.ToArray();
            StaticValues.s100_VoltLamp = LampVaList.ToArray();

            return true;

        }

        private void Check_P1_chargerTime(double startT, double endT)
        {
            double chargT = endT - startT - 0.5;
            Step100_OutPut.s100_P1_re = Convert.ToString(chargT);
        }

        private void Check_VFC(double[] VFCdata)
        {
            //1.8 sec 36sample=>VFCdata [20] , t=1.0s
            //2.1 sec 42sample=>VFCdata [20] , t=1.0s
            //2.4 sec 48 sample
            double P2_Vflash = (VFCdata[32] + VFCdata[33] + VFCdata[34]) / 3;//t=1.6s,The max value (mean over 3 samples) of the Flashcap
                                                                             //            double P2_Vflash = (VFCdata[36] + VFCdata[37] + VFCdata[38]) / 3;//t=1.8s,The max value (mean over 3 samples) of the Flashcap
            //double P2_Vflash = (VFCdata[42] + VFCdata[43] + VFCdata[44]) / 3;//t=2.1s,The max value (mean over 3 samples) of the Flashcap

            Step100_OutPut.s100_P2_re = Convert.ToString(P2_Vflash);
            double P3_maxval = (VFCdata[26] + VFCdata[27] + VFCdata[28]) / 3;//t=1.3s,The max value (mean over 3 samples) of the Flashcap 
            //double P3_maxval = (VFCdata[32] + VFCdata[33] + VFCdata[34]) / 3;
            //double P3_maxval = (VFCdata[38] + VFCdata[39] + VFCdata[40]) / 3; //t=1.9

            Step100_OutPut.s100_P3_re = Convert.ToString(P3_maxval);// new 
            ////P6
            double P6_T05_val = VFCdata[BeginstopchargingIDX]; //t=0.5s,disabling CPChargeFlashcap
            Console.WriteLine("T=0.5s : P6 = " + VFCdata[BeginstopchargingIDX]);
            Step100_OutPut.s100_P6_re = Convert.ToString(P6_T05_val);
            ////P7
            
            double P7_T10_val = VFCdata[EndstopchargingIDX - 1] - VFCdata[BeginstopchargingIDX];  //// The flashcap voltage difference to the value at 0.5 sec is measured
            Step100_OutPut.s100_P7_re = Convert.ToString(P7_T10_val);
        }

        private void Check_VLamp(double[] VLampdata)
        {
            //1.8 sec 36sample=>VFCdata [20] , t=1.0s
            //2.4 sec 48 sample
            //P5
            double P5_T15_val = (VLampdata[26] + VLampdata[27] + VLampdata[28]) / 3;
            //double P5_T15_val = (VLampdata[38]+ VLampdata[39] + VLampdata[40]) /3;// t=1.9s ,The max value (mean over 3 samples)
            Step100_OutPut.s100_P5_re = Convert.ToString(P5_T15_val);
            ////P8
            double P8_T05_val = VLampdata[BeginstopchargingIDX]; //t=0.2s,disabling CPChargeFlashcap
            //Console.WriteLine("T=0.5s : P8 = " + VLampdata[BeginstopchargingIDX]);
            Step100_OutPut.s100_P8_re = Convert.ToString(P8_T05_val);
            ////P9
            double P9_T10_val = VLampdata[EndstopchargingIDX - 1]; //t=0.7s,restart charging
            Step100_OutPut.s100_P9_re = Convert.ToString(P9_T10_val);
            //P12
            double P12_VLamp = (VLampdata[31] + VLampdata[32] + VLampdata[33]) / 3;//t=1.55s
            //double P12_VLamp = (VLampdata[43] + VLampdata[44] + VLampdata[45]) / 3;//t=2.15s,The max value (mean over 3 samples) of the Vlamp
            Step100_OutPut.s100_P12_re = Convert.ToString(P12_VLamp);
        }

        private void Check_NIdata(Array VFC, Array Vlamp_P, Array Vlamp_N)
        {

            //P4
            int idx = Convert.ToInt32(samplerate * 1.55);
            int arrLen = Convert.ToInt32(samplerate * 0.15);
            double[] VFCarr = new double[arrLen];
            Console.WriteLine("NI data lenth = {0} , t=1.55s idx = {1} ,data = {2}", VFC.Length
                , idx, VFCarr[0]);
            System.Array.Copy(VFC, idx, VFCarr, 0, arrLen);
            double VFCAVG = VFCarr.Average();
            double Resdiv = (r1 + r2) / r2;
            Console.WriteLine("P4 VFCAVG = {0}  ; {1}", VFCAVG, VFCAVG * Resdiv);

            Step100_OutPut.s100_P4_re = Convert.ToString(VFCAVG * Resdiv);
            //////P10
            double[] Vlamp_P_arr = new double[arrLen];
            System.Array.Copy(Vlamp_P, idx, Vlamp_P_arr, 0, arrLen);
            double VlampP_AVG = Vlamp_P_arr.Average();
            Step100_OutPut.s100_P10_re = Convert.ToString(VlampP_AVG * Resdiv);
            //P13
            int idx1 = Convert.ToInt32(samplerate * 0.4);
            int arrLen1 = Convert.ToInt32(samplerate * 0.3);
            double[] Vlamp_P_arr1 = new double[arrLen1];
            System.Array.Copy(Vlamp_P, idx1, Vlamp_P_arr1, 0, arrLen1);
            int num1 = 0;
            double vlampMin_value = 500;
            double current_value1;
  
            //Console.WriteLine("aryLength = " + aryLength);
            while (num1 < arrLen1)
            {
                current_value1 = Convert.ToDouble(Vlamp_P_arr1.GetValue(num1));
                if (current_value1 < vlampMin_value)
                    vlampMin_value = current_value1;
                //DisplayToTextBoxBuffer($"{value[num]}");

                num1++;
            }
            Console.WriteLine("The Min Value : {0}", vlampMin_value);
            Step100_OutPut.s100_P13_re = Convert.ToString(vlampMin_value * Resdiv);
            //P11
            int num = 0;
            double Min_value = 0;
            double current_value;
            double aryLength = Vlamp_N.Length;
            double Neg_Resdiv = (399 + 6.8) / 6.8;
            //Console.WriteLine("aryLength = " + aryLength);
            while (num < aryLength)
            {
                current_value = Convert.ToDouble(Vlamp_N.GetValue(num));
                if (current_value < Min_value)
                    Min_value = current_value;
                //DisplayToTextBoxBuffer($"{value[num]}");

                num++;
            }
            Console.WriteLine("The Min Value : {0}", Min_value);

            Step100_OutPut.s100_P11_re = Convert.ToString(Min_value * Neg_Resdiv);
        }

        private bool Check_Safety(double VLamp)
        {
            if (VLamp > 450)
            {
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_ON, 0); //##
                DR2003.Connect();
                DR2003.OutPutOff();
                DR2003.Disconnect();
                return false;
            }
            return true;
        }
    }

    class TestItemStep100_P2 : TestItemForm
    {
        public string step = "_100 P2";

        public TestItemStep100_P2()
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
            WORK_STATUS re = WORK_STATUS.WORKING;
            sMeasured = Step100_OutPut.s100_P2_re;
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
                Step100_OutPut.Savelog_Enable = true;
                re = WORK_STATUS.FAIL;
            }
            return re;
        }

        public override void release()
        {

        }
    }

    class TestItemStep100_P3 : TestItemForm
    {
        public string step = "_100 P3";

        public TestItemStep100_P3()
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
            WORK_STATUS re = WORK_STATUS.WORKING;
            sMeasured = Step100_OutPut.s100_P3_re;
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
                Step100_OutPut.Savelog_Enable = true;
                re = WORK_STATUS.FAIL;
            }
            return re;
        }

        public override void release()
        {

        }
    }

    class TestItemStep100_P4 : TestItemForm
    {
        public string step = "_100 P4";

        public TestItemStep100_P4()
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
            WORK_STATUS re = WORK_STATUS.WORKING;
            sMeasured = Step100_OutPut.s100_P4_re;
            //check limitation
            if (PassFailJudge())
            {
                // Check offset value in the range
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                re = WORK_STATUS.PASS;
            }
            else
            {
                Step100_OutPut.Savelog_Enable = true;
                re = WORK_STATUS.FAIL;
            }
            return re;
        }

        public override void release()
        {

        }
    }


    class TestItemStep100_P5 : TestItemForm
    {
        public string step = "_100 P5";

        public TestItemStep100_P5()
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
            WORK_STATUS re = WORK_STATUS.WORKING;
            sMeasured = Step100_OutPut.s100_P5_re;
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
                Step100_OutPut.Savelog_Enable = true;
                re = WORK_STATUS.FAIL;
            }
            return re;
        }

        public override void release()
        {

        }
    }

    class TestItemStep100_P6 : TestItemForm
    {
        public string step = "_100 P6";

        public TestItemStep100_P6()
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
            WORK_STATUS re = WORK_STATUS.WORKING;
            sMeasured = Step100_OutPut.s100_P6_re;
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
                Step100_OutPut.Savelog_Enable = true;
                re = WORK_STATUS.FAIL;
            }
            return re;
        }

        public override void release()
        {

        }
    }


    class TestItemStep100_P7 : TestItemForm
    {
        public string step = "_100 P7";

        public TestItemStep100_P7()
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
            WORK_STATUS re = WORK_STATUS.WORKING;
            sMeasured = Step100_OutPut.s100_P7_re;
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
                Step100_OutPut.Savelog_Enable = true;
                re = WORK_STATUS.FAIL;
            }
            return re;
        }

        public override void release()
        {

        }
    }


    class TestItemStep100_P8 : TestItemForm
    {
        public string step = "_100 P8";

        public TestItemStep100_P8()
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
            WORK_STATUS re = WORK_STATUS.WORKING;
            sMeasured = Step100_OutPut.s100_P8_re;
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
                Step100_OutPut.Savelog_Enable = true;
                re = WORK_STATUS.FAIL;
            }
            return re;
        }

        public override void release()
        {

        }
    }


    class TestItemStep100_P9 : TestItemForm
    {
        public string step = "_100 P9";

        public TestItemStep100_P9()
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
            WORK_STATUS re = WORK_STATUS.WORKING;
            sMeasured = Step100_OutPut.s100_P9_re;
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
                Step100_OutPut.Savelog_Enable = true;
                re = WORK_STATUS.FAIL;
            }
            return re;
        }

        public override void release()
        {

        }
    }


    class TestItemStep100_P10 : TestItemForm
    {
        public string step = "_100 P10";

        public TestItemStep100_P10()
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
            WORK_STATUS re = WORK_STATUS.WORKING;
            sMeasured = Step100_OutPut.s100_P10_re;
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
                Step100_OutPut.Savelog_Enable = true;
                re = WORK_STATUS.FAIL;
            }
            return re;
        }

        public override void release()
        {

        }
    }


    class TestItemStep100_P11 : TestItemForm
    {
        public string step = "_100 P11";

        public TestItemStep100_P11()
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
            WORK_STATUS re = WORK_STATUS.WORKING;
            sMeasured = Step100_OutPut.s100_P11_re;
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
                Step100_OutPut.Savelog_Enable = true;
                re = WORK_STATUS.FAIL;
            }
            return re;
        }

        public override void release()
        {

        }
    }


    class TestItemStep100_P12 : TestItemForm
    {
        public string step = "_100 P12";

        public TestItemStep100_P12()
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
            WORK_STATUS re = WORK_STATUS.WORKING;
            sMeasured = Step100_OutPut.s100_P12_re;
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
                Step100_OutPut.Savelog_Enable = true;
                re = WORK_STATUS.FAIL;
            }
            return re;
        }

        public override void release()
        {

        }
    }

    class TestItemStep100_P13 : TestItemForm
    {
        public string step = "_100 P13";

        public TestItemStep100_P13()
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
            WORK_STATUS re = WORK_STATUS.WORKING;
            sMeasured = Step100_OutPut.s100_P13_re;
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
                Step100_OutPut.Savelog_Enable = true;
                re = WORK_STATUS.FAIL;
            }
            return re;
        }

        public override void release()
        {

        }
    }
}
