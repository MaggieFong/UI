using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;

    class TestItemStep110_P1 : TestItemForm
    {
        public string step = "_110 P1";
        private DR2003 DR2003;      //main power
        private DAQdio dioA;
        private DAQdio dioB;
        
        bool get_Charge150V_time = false;
        bool get_Charge380V_time = false;
        double Charge150V_time = 2.5;
        double Charge380V_time = 0.0;

       
        public TestItemStep110_P1()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            Step110_OutPut.Savelog_Enable = false;
            sMeasured = "";
            try
            {
                dioA = new DAQdio(IO_USB6525_A.deviceName);
                dioA.addOneLineWriteCH(IO_USB6525_A.Relay_Discharge_Res_K12_K13_P04);

                dioB = new DAQdio(IO_USB6525_B.deviceName);
                dioB.addOneLineWriteCH(IO_USB6525_B.CTL_HV_RELAY_P05);

                //DR2003 = new DR2003(Power_DR2002MO.comPort, Power_DR2002MO.baudRate);
                //DR2003.Connect();
                //DR2003.SetVOLT(Convert.ToString(Power_DR2002MO.V195));    //19.5V
                //System.Threading.Thread.Sleep(30);
                //DR2003.SetCURR(Convert.ToString(Power_DR2002MO.FLASHCAP_CURR5));   //6A
                //System.Threading.Thread.Sleep(30);
                //DR2003.SetOCPOn(Convert.ToString(Power_DR2002MO.FLASHCAP_OCP)); //6A
                //System.Threading.Thread.Sleep(30);
                //DR2003.SetOVPOn(Convert.ToString(Power_DR2002MO.OVP)); //22V
                //System.Threading.Thread.Sleep(30);
                //DR2003.OutPutOn();
                //DR2003.Disconnect();
                

            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
  
            StaticValues.hwTestRPCDevice.ConfigureADC(0, 0, 2, 2);
    
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            ReadADC();
            //Console.WriteLine("P1 = " + Step110_OutPut.s110_P1_re);
            //Console.WriteLine("P2 = " + Step110_OutPut.s110_P2_re);
            //Console.WriteLine("P4 = " + Step110_OutPut.s110_P4_re);
            double P5diff = Convert.ToDouble(Step110_OutPut.s110_P5_re) - Convert.ToDouble(Step110_OutPut.s110_P4_re);
            Step110_OutPut.s110_P5_re = Convert.ToString(P5diff);
            //Console.WriteLine("P5 = " + Step110_OutPut.s110_P5_re);
            //Console.WriteLine("P6 = " + Step110_OutPut.s110_P6_re);
            //Console.WriteLine("P8 = " + Step110_OutPut.s110_P8_re);

            sMeasured = Step110_OutPut.s110_P1_re;
            //check limitation
            if (PassFailJudge())
            {
                //Step110_OutPut.Savelog_Enable = true;
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
            
            DR2003 = null;
            dioA.setOneData(true);
            dioB.setOneData(true);
            System.Threading.Thread.Sleep(500);
            dioA.setOneData(false);
            dioB.setOneData(false);
            dioA = null;
            dioB = null;
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPDischarger, 0);
           
        }
        private void ReadADC()
        {
            var FlashCapVList = new List<double>();
            var CPDischaTempList = new List<double>();

            double startT = 2;
            double stopT = startT + 0.5;
            float var = 0.00f;
            ushort data = 0;
            ushort data1 = 0;

            long dStartTime;
            long delta;
            int cnt = 0;
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPDischarger, 1);
            for (double x = 0; x < 3.5; x += 0.05)
            {
                dStartTime = DateTime.Now.Ticks;

                var = (float)x;
                Console.WriteLine("x  = {0}  ", x);

                StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_FlashCapVoltage, ref data);
                if (data == 0)
                {
                    StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_FlashCapVoltage, ref data);
                    //Console.WriteLine("retry get  FlashCapVoltage ADC " + data);
                }
                
                //StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_CPDischargerTemp, ref data1);

                double ADCFlashCap = 1.0 * (double)data / 4095;
                double FlashCapVoltage = ADCFlashCap * 442.18;

                Console.WriteLine("ADC =  " + ADCFlashCap + " ; FlashCapVoltage =  " + FlashCapVoltage);
                FlashCapVList.Add(FlashCapVoltage);

                //double ADCCPDischargerT = 1.0 * (double)data1 / 4095;
                //double CPDischargerTemp = 1000 * ADCCPDischargerT / 15;

                ////Console.WriteLine("ADC =  " + ADCCPDischargerT + " ; CPDischargerTemp =  " + CPDischargerTemp);
                //CPDischaTempList.Add(CPDischargerTemp);
                
                //The discharge time:t@380V and t@150V minus 0.5 sec
                if (FlashCapVoltage < 380 && !get_Charge380V_time)
                {
                    get_Charge380V_time = true;
                    Charge380V_time = x;
                    //Console.WriteLine("380V time  {0}", x);
                }

                if (FlashCapVoltage < 150 && !get_Charge150V_time)
                {
                    get_Charge150V_time = true;
                    Charge150V_time = x;
                    //Console.WriteLine("150V time  {0}", x);
                }

                if (var < startT || var > stopT)
                {
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPDischarger, 1);
                }
                else
                {
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPDischarger, 0);

                }
                if (var == 3.50F)//P2
                {
                    Step110_OutPut.s110_P2_re = Convert.ToString(FlashCapVoltage);
                    //Console.WriteLine("s110_P2_re = " + Step110_OutPut.s110_P2_re);

                }
                if (var == 2.05F)//P4
                {
                    Step110_OutPut.s110_P4_re = Convert.ToString(FlashCapVoltage);
                    //Console.WriteLine("s110_P4_re = " + Step110_OutPut.s110_P4_re);
                }
                if (var == 2.50F)//P5
                {
                    Step110_OutPut.s110_P5_re = Convert.ToString(FlashCapVoltage);
                    //Console.WriteLine("s110_P5_re = " + Step110_OutPut.s110_P5_re);
                }
                if (var == 0.05F)
                {
                    data1 = 0;
                    StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_CPDischargerCurrent, ref data1);
                    double ADCCPDischargerT = 1.0 * (double)data1 / 4095;
                    double CPDischargerTemp = 1000 * ADCCPDischargerT / 15;
                    Console.WriteLine("ADC =  " + ADCCPDischargerT + " ; CPDischargerTemp =  " + CPDischargerTemp);
                    Step110_OutPut.s110_P6_re = Convert.ToString(CPDischargerTemp);
                    //Console.WriteLine("s110_P6_re = " + Step110_OutPut.s110_P6_re);
                }
                if (var == 2.10F)
                {
                    data1 = 0;
                    StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_CPDischargerCurrent, ref data1);
                    double ADCCPDischargerT = 1.0 * (double)data1 / 4095;
                    double CPDischargerTemp = 1000 * ADCCPDischargerT / 15;
                    //Console.WriteLine("ADC =  " + ADCCPDischargerT + " ; CPDischargerTemp =  " + CPDischargerTemp);
                    Step110_OutPut.s110_P8_re = Convert.ToString(CPDischargerTemp);
                    //Console.WriteLine("s110_P8_re = " + Step110_OutPut.s110_P8_re);
                }
                //if (var == 0.05F)//P6
                //{
                //    Step110_OutPut.s110_P6_re = Convert.ToString(CPDischargerTemp);
                //    //Console.WriteLine("s110_P6_re = " + Step110_OutPut.s110_P6_re);
                //}
                //if (var == 2.10F)//P8
                //{
                //    Step110_OutPut.s110_P8_re = Convert.ToString(CPDischargerTemp);
                //    //Console.WriteLine("s110_P8_re = " + Step110_OutPut.s110_P8_re);
                //}
                delta = DateTime.Now.Ticks - dStartTime;
                float testTime = (float)(delta / 10000);
                //Console.WriteLine("testTime = " + testTime);
                if (testTime < 50)
                {
                    int delayTime = Convert.ToInt32(50.0 - testTime);
                    ////Console.WriteLine("delay T = " + delayTime.ToString());
                    System.Threading.Thread.Sleep(delayTime);
                }
                else {
                    cnt++;
                    //Console.WriteLine("********** WARMING **********");
                }
            }
            //Console.WriteLine("********** WARMING ********** : "+cnt);
            StaticValues.s110_VoltFlashCap = FlashCapVList.ToArray();
            StaticValues.s110_CPDischargCURR = CPDischaTempList.ToArray();
            Check_P1_dischargerTime(Charge380V_time, Charge150V_time);
        }
        private void Check_P1_dischargerTime(double startT, double endT)
        {
            double chargT = endT - startT - 0.5;
            Step110_OutPut.s110_P1_re = Convert.ToString(chargT);

        }
        
    }
    class TestItemStep110_P2 : TestItemForm
    {
        public string step = "_110 P2";

        public TestItemStep110_P2()
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
            sMeasured = Step110_OutPut.s110_P2_re;
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
                Step110_OutPut.Savelog_Enable = true;
                re = WORK_STATUS.FAIL;
            }
            return re;
        }

        public override void release()
        {

        }
    }

    class TestItemStep110_P4 : TestItemForm
    {
        public string step = "_110 P4";

        public TestItemStep110_P4()
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
            sMeasured = Step110_OutPut.s110_P4_re;
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
                Step110_OutPut.Savelog_Enable = true;
                re = WORK_STATUS.FAIL;
            }
            return re;
        }

        public override void release()
        {

        }
    }

    class TestItemStep110_P5 : TestItemForm
    {
        public string step = "_110 P5";

        public TestItemStep110_P5()
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
            sMeasured = Step110_OutPut.s110_P5_re;
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
                Step110_OutPut.Savelog_Enable = true;
                re = WORK_STATUS.FAIL;
            }
            return re;
        }

        public override void release()
        {

        }
    }

    class TestItemStep110_P6 : TestItemForm
    {
        public string step = "_110 P6";

        public TestItemStep110_P6()
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
            sMeasured = Step110_OutPut.s110_P6_re;
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
                Step110_OutPut.Savelog_Enable = true;
                re = WORK_STATUS.FAIL;
            }
            return re;
        }

        public override void release()
        {

        }
    }

    class TestItemStep110_P8 : TestItemForm
    {
        public string step = "_110 P8";

        public TestItemStep110_P8()
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
            sMeasured = Step110_OutPut.s110_P8_re;
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
                Step110_OutPut.Savelog_Enable = true;
                re = WORK_STATUS.FAIL;
            }
            return re;
        }

        public override void release()
        {

        }
    }
}
