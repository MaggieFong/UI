using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;

    class TestItemStep135_P1 : TestItemForm
    {
        public string step = "_135 P1";
        private DAQdio dioB;
        public TestItemStep135_P1()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            try
            {
                dioB = new DAQdio(IO_USB6525_B.deviceName);
                dioB.addOneLineWriteCH(IO_USB6525_B.Relay_SCA_K12_K13_P03);
                dioB.setOneData(true);

            }
            catch (Exception e)
            {

                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
            if (StaticValues.hwTestRPCDevice.ConfigureADC(15, 2, 2, 2))
            {
                //Console.WriteLine("ConfigureADC uC_SCATemp pass ");
            }
            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            sMeasured = "";
            ushort data = 0;
            
            if (StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_STS_Sensor_Filtered, ref data))
            {
                double volt1 = 3.3 * (double)data / 4095;
                //Console.WriteLine("ADC =  " + data + " ; volt =  " + volt);
                sMeasured = volt1.ToString("F3");
                StaticValues.step135_P1 = volt1;
            }
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
            dioB = null;

        }
    }


    class TestItemStep135_P2 : TestItemForm
    {
        public string step = "_135 P2";
        private DAQadc taskADC;

        public TestItemStep135_P2()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {

            //DAQ setting
            try
            {
                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_5);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP611_AI21);


                if (!taskADC.start())
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

            return WORK_STATUS.PASS;
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

                    sMeasured = measured_v.ToString("F3");
                    Console.WriteLine("ai TP611_AI21, measured: {0}", measured_v);
                    StaticValues.step135_P2 = measured_v;
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
            //System.Threading.Thread.Sleep(100);
            //dioC.setOneData(false);
            //dioC = null;
        }
    }


    class TestItemStep135_P3 : TestItemForm
    {
        public string step = "_135 P3";
        

        public TestItemStep135_P3()
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
            double diff = StaticValues.step135_P2 - StaticValues.step135_P1;
            sMeasured = diff.ToString("F3");

           
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
           
        }
    }

    class TestItemStep136_P1 : TestItemForm
    {
        public string step = "_136 P1";
        private DAQdio dioB;
        public TestItemStep136_P1()
        {
            sStep = step;
            LoadSpec();            
        }

        public override WORK_STATUS Init()
        {
            try
            {
                dioB = new DAQdio(IO_USB6525_B.deviceName);
                dioB.addOneLineWriteCH(IO_USB6525_B.Relay_SCA_K12_K13_P03);
                //dioB.setOneData(true);

            }
            catch (Exception e)
            {

                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
            if (StaticValues.hwTestRPCDevice.ConfigureADC(15, 2, 2, 2))
            {
                //Console.WriteLine("ConfigureADC uC_SCATemp pass ");
            }
            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            sMeasured = "";
            ushort data = 0;
            if (StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_SCATemp, ref data))
            {
                double volt = 3.3 * (double)data / 4095;
                Console.WriteLine("ADC =  " + data+" ; volt =  " + volt);

                sMeasured = volt.ToString("F3");
                StaticValues.step135_P4 = volt;
            }
            data = 0;
            if (StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_STS_Sensor_Filtered, ref data))
            {
                double volt1 = 3.3 * (double)data / 4095;
                //Console.WriteLine("ADC =  " + data + " ; volt =  " + volt);
                //StaticValues.step135_P4 = volt1;
                
            }
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
            dioB = null;

        }
    }


    class TestItemStep136_P2 : TestItemForm
    {
        public string step = "_136 P2";
        private DAQadc taskADC;
        
        public TestItemStep136_P2()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            
            //DAQ setting
            try
            {
                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_5);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP623_AI20);
                

                if (!taskADC.start())
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
            
            return WORK_STATUS.PASS;
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
                   
                    sMeasured = measured_v.ToString("F3");
                    //Console.WriteLine("ai TP502_AI20, measured: {0}", measured_v);
                    StaticValues.step135_P5 = measured_v;
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
            //System.Threading.Thread.Sleep(100);
            //dioC.setOneData(false);
            //dioC = null;
        }
    }
    class TestItemStep136_P3 : TestItemForm
    {
        public string step = "_136 P3";


        public TestItemStep136_P3()
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
            double diff = StaticValues.step135_P4 - StaticValues.step135_P5;
            sMeasured = diff.ToString("F3");


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

        }
    }
}
