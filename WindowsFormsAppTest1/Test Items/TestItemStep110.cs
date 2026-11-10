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
        //private DR2003 DR2003;      //main power
        private DAQdio dioA;
        private DAQdio dioB;
       

       
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

            //DR2003 = null;
            dioA.setOneData(true);
            dioB.setOneData(true);
            System.Threading.Thread.Sleep(500);
            //dioA.setOneData(false);
            //dioB.setOneData(false);
            dioA = null;
            dioB = null;
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPDischarger, 0);
           
        }
        private void ReadADC()
        {
            var FlashCapVList = new List<double>();
            var CPDischaTempList = new List<double>();

            double startT = 0.1;
            double stopT = startT + 0.1;
            float var = 0.00f;
            ushort data = 0;
            ushort data1 = 0;

            long dStartTime;
            long delta;
            int cnt = 0;
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_FC_Discharge, 1);
            for (double x = 0; x < 0.31; x += 0.05)
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

                data1 = 0;
                StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_CPDischargerCurrent, ref data1);
                double ADCCPDischargerT = 1.0 * (double)data1 / 4095;
                double CPDischargerTemp = 1000 * ADCCPDischargerT / 15;

                Console.WriteLine("ADC =  " + ADCCPDischargerT + " ; CPDischargerTemp =  " + CPDischargerTemp);
                CPDischaTempList.Add(CPDischargerTemp);

                double CollectorCurrent = CPDischargerTemp - (56.8 - 53.1);
                double ratio = 1000.0 * FlashCapVoltage / CollectorCurrent;
                Console.WriteLine("DisChargeResistance =  " + ratio);
                if (var < startT || var > stopT)
                {
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_FC_Discharge, 1);
                }
                else
                {
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_FC_Discharge, 0);

                }
                if (var == 0.10F)//P1
                {
                    //double CollectorCurrent = CPDischargerTemp - (56.8 - 53.1);
                    //double ratio = 1000.0 * FlashCapVoltage / CollectorCurrent;
                    Step110_OutPut.s110_P1_re = ratio.ToString("F3");
                    Console.WriteLine("s110_P1_re = " + Step110_OutPut.s110_P1_re);

                }
                if (var == 0.20F)//P2
                {
                    Step110_OutPut.s110_P2_re = Convert.ToString(CPDischargerTemp);
                    Console.WriteLine("s110_P2_re = " + Step110_OutPut.s110_P2_re);

                }
                if (var == 0.30F)//P3
                {
                    
                    Step110_OutPut.s110_P3_re = ratio.ToString("F3");
                    Console.WriteLine("s110_P3_re = " + Step110_OutPut.s110_P3_re);

                }
                delta = DateTime.Now.Ticks - dStartTime;
                float testTime = (float)(delta / 10000);
                Console.WriteLine("testTime = " + testTime);
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
           
            StaticValues.s110_VoltFlashCap = FlashCapVList.ToArray();
            StaticValues.s110_CPDischargCURR = CPDischaTempList.ToArray();
            
        }
        //private void Check_P1_dischargerTime(double startT, double endT)
        //{
        //    double chargT = endT - startT - 0.5;
        //    Step110_OutPut.s110_P1_re = Convert.ToString(chargT);

        //}
        
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
                //Step110_OutPut.Savelog_Enable = true;
                re = WORK_STATUS.FAIL;
            }
            return re;
        }

        public override void release()
        {

        }
    }
    class TestItemStep110_P3 : TestItemForm
    {
        public string step = "_110 P3";

        public TestItemStep110_P3()
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
            sMeasured = Step110_OutPut.s110_P3_re;
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
                //Step110_OutPut.Savelog_Enable = true;
                re = WORK_STATUS.FAIL;
            }
            return re;
        }

        public override void release()
        {

        }
    }

}
