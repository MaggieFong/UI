using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;

/*
    class TestItemStep085 : TestItemForm
    {
        public string step = "_085";

        public TestItemStep085()
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
            

            sMeasured = "";
            return WORK_STATUS.PASS;
        }

        public override void release()
        {

        }
    }
*/

    class TestItemStep085_P1 : TestItemForm
    {
        public string step = "_085 P1";
        public TestItemStep085_P1()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            return WORK_STATUS.PASS;
            StaticValues.hwTestRPCDevice.ConfigurePin(StaticValues.uC_AirOutTemp,
                   HardwareTestRPCSerialDevice.PinTypeValue.AdcPin,
                   HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);
            if (StaticValues.hwTestRPCDevice.ConfigureADC(15, 2, 2, 2))
            {
                //Console.WriteLine("ConfigureADC uC_AirOutTemp pass ");

            }
            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            
            //sMeasured = Convert.ToString(StaticValues.step085_P1);
            //if (PassFailJudge())
            //{
            //    if (!Golden.verified && !Golden.empertyData && bNeedOffset)
            //    {
            //        if (!offsetCalculate())
            //            return WORK_STATUS.FAIL;
            //    }

            //    return WORK_STATUS.PASS;
            //}
            //else
            //{
            //    return WORK_STATUS.FAIL;
            //}
            sMeasured = "";
            ushort data = 0;
            StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_AirOutTemp, ref data);
            //Console.WriteLine(" uC_AirOutTemp ADC = "  +data);
            if (data > 0)
            {
                double volt = 3.3 * (double)data / 4095;
                //Console.WriteLine("ADC =  " + data + " ; volt =  " + volt);
                StaticValues.step085_P1 = volt;
                sMeasured = volt.ToString("F3");
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
            else
            {
                //Console.WriteLine(" reget uC_AirOutTemp ADC ");
            }
            System.Threading.Thread.Sleep(50);

            return WORK_STATUS.WORKING;
        }

        public override void release()
        {

        }
    }


    class TestItemStep085_P2 : TestItemForm
    {
        public string step = "_085 P2";
        private DAQadc taskADC;
        
        public TestItemStep085_P2()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //return WORK_STATUS.PASS;
            //DAQ setting
            try
            {


               
                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_5);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP215_AI18);

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
            //sMeasured = Convert.ToString(StaticValues.step085_P2);
            //if (PassFailJudge())
            //{
            //    if (!Golden.verified && !Golden.empertyData && bNeedOffset)
            //    {
            //        if (!offsetCalculate())
            //            return WORK_STATUS.FAIL;
            //    }

            //    return WORK_STATUS.PASS;
            //}
            //else
            //{
            //    return WORK_STATUS.FAIL;
            //}
            WORK_STATUS re = WORK_STATUS.WORKING;

            if (taskADC.isEnable() == false)
            {
                if (taskADC.getErrorMsg() == null)      //must check if there were any error when DAQadc.callbackADC()
                {
                    double average = taskADC.getAverage(0);
                    double measured_v = average;
                    sMeasured = measured_v.ToString("F3") ;
                    //Console.WriteLine("ai TP511_AI18  measured: {0}", measured_v);
                    StaticValues.step085_P2 = measured_v;
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
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Cooling, 0);
        }
    }


    class TestItemStep085_P3 : TestItemForm
    {
        public string step = "_085 P3";

        public TestItemStep085_P3()
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
            sMeasured = "";
            double Diff = StaticValues.step085_P1 - StaticValues.step085_P2;
            //Console.WriteLine("step 085 P3 diff = " + Diff);
            sMeasured = Diff.ToString("F3");
            if (!PassFailJudge())
            {
                return WORK_STATUS.FAIL;
            }

            if (!Golden.verified && !Golden.empertyData && bNeedOffset)
            {
                if (!offsetCalculate())
                    return WORK_STATUS.FAIL;
            }

            return WORK_STATUS.PASS;
        }

        public override void release()
        {

        }
    }

    class TestItemStep085_P4 : TestItemForm
    {
        public string step = "_085 P4";
        public TestItemStep085_P4()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_ON, 0);
            //StaticValues.hwTestRPCDevice.ConfigurePin(StaticValues.uC_Lampframe_TC,
            //       HardwareTestRPCSerialDevice.PinTypeValue.AdcPin,
            //       HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);
            //if (StaticValues.hwTestRPCDevice.ConfigureADC(15, 2, 2, 2))
            //{
            //    //Console.WriteLine("ConfigureADC uC_Lampframe_TC pass ");

            //}
            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            sMeasured = "";
            ushort data = 0;
            StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_Lampframe_TC, ref data);
            Console.WriteLine(" uC_Lampframe_TC ADC = " + data);
            ushort data1 = 0;

            if (StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_AttachmentConnected, ref data1))
            {
                ADC_OutPut.s117_P1 = data1;
            }
                if (data > 0)
            {
                double volt = 3.3 * (double)data / 4095;
                Console.WriteLine("ADC =  " + data + " ; volt =  " + volt);
                //StaticValues.step085_P1 = volt;
                sMeasured = volt.ToString("F3");
                
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
            else
            {
                //Console.WriteLine(" reget uC_AirOutTemp ADC ");
            }
            System.Threading.Thread.Sleep(50);

            return WORK_STATUS.WORKING;
        }

        public override void release()
        {

        }
    }

    class TestItemStep085_P5 : TestItemForm
    {
        public string step = "_085 P5";
        private DAQadc taskADC;
        private DAQdio dio;

        public TestItemStep085_P5()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //DAQ setting
            try
            {
                //DIO setting
                //dio = new DAQdio(IO_USB6525_A.deviceName);
                //dio.addOneLineWriteCH(IO_USB6525_A.Relay_Att_Conn1_TP512_K1_P00);
                

                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_5);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP425_AI9);
              
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
                    double measured_v = average;
                   
                    sMeasured = measured_v.ToString("F3") ;
                 
                    
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
           
        }
    }
    class TestItemStep085_P6 : TestItemForm
    {
        public string step = "_085 P6";
        private DAQdio dioA;
        private DAQdio dioB;

        public TestItemStep085_P6()
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
            sMeasured = "";
            double Diff = StaticValues.step085_P4 - StaticValues.step085_P5;
            //Console.WriteLine("step 085 P3 diff = " + Diff);
            sMeasured = Convert.ToString(Diff);
            if (!PassFailJudge())
            {
                return WORK_STATUS.FAIL;
            }

            if (!Golden.verified && !Golden.empertyData && bNeedOffset)
            {
                if (!offsetCalculate())
                    return WORK_STATUS.FAIL;
            }

            return WORK_STATUS.PASS;
        }

        public override void release()
        {
          
        }
    }

   

   
}
