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
        private DAQadc taskADC, taskADCb;
        private double r1 = 3000.0;
        private double r2 = 47.0;
        private bool NIADC_start = false;
        private int BeginstopchargingIDX = 0;
        private int EndstopchargingIDX = 0;
        private bool Beginstopcharging = false;
        private bool Endstopcharging = false;
        private double P400_RANGE_H = 7.0;
        private double P400_RANGE_L = -0.5;
        private double N400_RANGE_H = 0.5;
        private double N400_RANGE_L = -7.0;

        private int samplerate = 2000;//0.1ms
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
            try
            {
                dio = new DAQdio(IO_USB6525_A.deviceName);
                dio.addOneLineWriteCH(IO_USB6525_A.Relay_FlashButton_TP515_K6_P05); // flash button
                dioB = new DAQdio(IO_USB6525_B.deviceName);
                dioB.addOneLineWriteCH(IO_USB6525_B.AISENSE_K11_P02);
                //dioB.setOneData(true);
                //StaticValues.hwTestRPCDevice.ConfigurePin(StaticValues.uC_UIButtonSetDown,
                //                    HardwareTestRPCSerialDevice.PinTypeValue.OutputPin,
                //                    HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);
                //Discharge is disabled, Switch Q600 is closed, and 19.5V supply is ON.
                DR2003 = new DR2003(Power_DR2002MO.comPort, Power_DR2002MO.baudRate);
                DR2003.Connect();
                //DR2003.SetVOLT(Convert.ToString(Power_DR2002MO.V195));    //19.5V
                //System.Threading.Thread.Sleep(30);
                DR2003.SetCURR(Convert.ToString(Power_DR2002MO.FLASHCAP_CURR5));   //6A
                //System.Threading.Thread.Sleep(30);
                //DR2003.SetOCPOn(Convert.ToString(Power_DR2002MO.FLASHCAP_OCP)); //6A
                //System.Threading.Thread.Sleep(30);
                //DR2003.SetOVPOn(Convert.ToString(Power_DR2002MO.OVP)); //22V
                //System.Threading.Thread.Sleep(30);
                //DR2003.OutPutOn();
                DR2003.Disconnect();

                taskADCb = new DAQadc(
                samplerate, 4000, DAQadc.VOLTAGE_RANGE.RANGE_N10_10);
                //samplerate, 4000);
                taskADCb.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.AI24);
                taskADC = new DAQadc(
                samplerate, 4000, DAQadc.VOLTAGE_RANGE.RANGE_N10_10);
                //samplerate, 4000);
                

                //taskADC.addNChannelWithRange(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.AI31,5.0,-7.0);
                //taskADC.addNChannelWithRange(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP101_AI1, 7.0, 0.0);
                //taskADC.addNChannelWithRange(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP104_AI0, 7.0, 0.0);
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP101_AI1);
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP104_AI0);
                //taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP104_AI0);
                //taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP101_AI1);
                //taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP103_AI2);
                //taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP104_AI0);
                //taskADC = new DAQadc(samplerate, 4000);
                //taskADC.addNChannelWithRange(DAQ_USB6218.deviceName, DAQ_USB6218.TP104_AI0, N400_RANGE_H, N400_RANGE_L);
                //taskADC.addNChannelWithRange(DAQ_USB6218.deviceName, DAQ_USB6218.TP101_AI1, P400_RANGE_H, P400_RANGE_L);
                //taskADC.addNChannelWithRange(DAQ_USB6218.deviceName, DAQ_USB6218.TP103_AI2, P400_RANGE_H, P400_RANGE_L);
            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }

            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Bat_En_Adapter, 1);
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Bat_En_Battery, 0);
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_OFF, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPDischarger, 0);
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPChargeFlashCap, 1);

            StaticValues.hwTestRPCDevice.ConfigureADC(0, 0, 2, 2);

            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_OFF, 0);
            if (!NIADC_start)
            {
                try
                {
                    
                    taskADC.start();
                    taskADCb.start();
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
                    System.Threading.Thread.Sleep(10);
                    Console.WriteLine("taskADC done");
                    if (taskADCb.isEnable() == false)
                    {
                        Console.WriteLine("taskADCb.isEnable() done");
                        if (taskADCb.getErrorMsg() == null)
                        {
                            Console.WriteLine("taskADCb  done ###");
                            //StaticValues.s100_NI_N_VoltLamp = taskADC.getRawData(0);
                            //StaticValues.s100_NI_VoltFlashCap = taskADC.getRawData(1);
                            //StaticValues.s100_NI_P_VoltLamp = taskADC.getRawData(2);
                            StaticValues.s100_NI_VoltFlashCap = taskADC.getRawData(1);
                           
                            StaticValues.s100_NI_P_VoltLamp = taskADC.getRawData(0);
                            StaticValues.s100_NI_N_VoltLamp = taskADCb.getRawData(0);
                            ////Console.WriteLine(StaticValues.s100_NI_VoltFlashCap.Length);
                            Check_VFC(StaticValues.s100_VoltFlashCap);
                            Check_VLamp(StaticValues.s100_VoltLamp);
                            Check_NIdata(StaticValues.s100_NI_VoltFlashCap, StaticValues.s100_NI_P_VoltLamp, StaticValues.s100_NI_N_VoltLamp);
                            
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
                        else
                        {
                            sMeasured = taskADCb.getErrorMsg();
                            return WORK_STATUS.FAIL;
                        }
                        
                    }

                    //return WORK_STATUS.PASS;
                }
                else
                {
                    sMeasured = taskADCb.getErrorMsg();
                    return WORK_STATUS.FAIL;
                }
            }

            return WORK_STATUS.WORKING;
        }

        public override void release()
        {
            dio.setOneData(false);
            //dioB.setOneData(false);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_LampPWM, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPChargeFlashCap, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_OFF, 1);
            Console.WriteLine("release");
            dio = null;
            dioB = null;
            taskADC = null;
            taskADCb = null;
            DR2003 = null;
            NIADC_start = false;
            Beginstopcharging = false;
            Endstopcharging = false;


        }
        private bool ReadADC()
        {
            bool get_Charge60V_time = false;
            bool get_Charge380V_time = false;
            bool IGBT_done = false;
            double Charge60V_time = 0;
            double Charge380V_time = 0;
            double startT = 0.5;
            double stopT = startT + 0.5;
            var FlashCapVList = new List<double>();
            var LampVaList = new List<double>();
            //bool pinstate = false;
            long dStartTime;
            long delta;
            int cnt = 0;
            BeginstopchargingIDX = 0;
            EndstopchargingIDX = 0;
            for (double x = 0.0; x < 4.0; x += 0.05)// 50ms
            {
                cnt++;
                ////Console.WriteLine("cnt = " + cnt);
                dStartTime = DateTime.Now.Ticks;
                ushort data = 0;
                ushort data1 = 0;
                // get flashcap ADC
                //if (pinstate)
                //{
                //    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UIButtonSetDown, 1);
                //    pinstate = false;
                //}
                //else {
                //    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UIButtonSetDown, 0);
                //    pinstate = true;
                //}
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

                    if (!Beginstopcharging)
                    {
                        Beginstopcharging = true;
                        BeginstopchargingIDX = cnt;
                        //Console.WriteLine("Not  Charging idx = " + BeginstopchargingIDX);
                    }
                }

                if (x > 3 && !IGBT_done)
                {//test neg lamp and IGBT
                    ////Console.WriteLine("x > 3");
                    IGBT_done = true;
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
            double[] result = new double[20];
            Array.Copy(VFCdata, 60, result, 0, 20);
            double reAVG = result.Average();
            Step100_OutPut.s100_P2_re = Convert.ToString(reAVG);
            double P3_maxval = (VFCdata[32]+ VFCdata[33])/2;//t=1.6s,The max value (mean over 2 samples) of the Flashcap 
            Step100_OutPut.s100_P3_re = Convert.ToString(P3_maxval);
            ////P6
            double P6_T05_val = VFCdata[BeginstopchargingIDX]; //t=0.5s,disabling CPChargeFlashcap
            //Console.WriteLine("T=0.5s : P6 = " + VFCdata[BeginstopchargingIDX]);
            Step100_OutPut.s100_P6_re = Convert.ToString(P6_T05_val);
            ////P7
            //double P7_T10_val = VFCdata[20]; //t=1.0s,restart charging ????
            double P7_T10_val = VFCdata[EndstopchargingIDX - 1] - VFCdata[BeginstopchargingIDX];  //// The flashcap voltage difference to the value at 0.5 sec is measured
            Step100_OutPut.s100_P7_re = Convert.ToString(P7_T10_val);
        }

        private void Check_VLamp(double[] VLampdata)
        {
            //P5
            double P5_T15_val = (VLampdata[32]+ VLampdata[33])/2;// t=1.6s The max value
            Step100_OutPut.s100_P5_re = Convert.ToString(P5_T15_val);
            ////P8
            double P8_T05_val = VLampdata[BeginstopchargingIDX]; //t=0.5s,disabling CPChargeFlashcap
            //Console.WriteLine("T=0.5s : P8 = " + VLampdata[BeginstopchargingIDX]);
            Step100_OutPut.s100_P8_re = Convert.ToString(P8_T05_val);
            ////P9
            double P9_T10_val = VLampdata[EndstopchargingIDX - 1]; //t=1.0s,restart charging
            Step100_OutPut.s100_P9_re = Convert.ToString(P9_T10_val);
            //P12
            double[] result = new double[19];
            Array.Copy(VLampdata, 62, result, 0, 19);//t=3.1~4.0 sec
            ////Console.WriteLine("print");
            ////Console.WriteLine(string.Join(",", result));
            double reAVG = result.Average();
            Step100_OutPut.s100_P12_re = Convert.ToString(reAVG);
        }

        private void Check_NIdata(Array VFC, Array Vlamp_P, Array Vlamp_N)
        {
            // t=3.0~4.0 sec
            //P4
            double[] VFCarr = new double[samplerate];
            int idx = samplerate * 3;
            System.Array.Copy(VFC, idx, VFCarr, 0, samplerate);
            double VFCAVG = VFCarr.Average();
            double Resdiv = (r1 + r2) / r2;
            //Console.WriteLine("P4 VFCAVG = {0}  ; {1}", VFCAVG, VFCAVG * Resdiv);

            Step100_OutPut.s100_P4_re = Convert.ToString(VFCAVG * Resdiv);
            //////P10
            double[] Vlamp_P_arr = new double[samplerate];
            System.Array.Copy(Vlamp_P, idx, Vlamp_P_arr, 0, samplerate);
            double VlampP_AVG = Vlamp_P_arr.Average();
            Step100_OutPut.s100_P10_re = Convert.ToString(VlampP_AVG * Resdiv);

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
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_OFF, 1);
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
}
