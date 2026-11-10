using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;

    class TestItemStep015_P1 : TestItemForm
    {
        public string step = "_015 P1";
        private DAQadc taskADC;
        private DAQdio taskdio;
        private DAQdio taskdio1;
        //private DR2003 DR2003;      //main power
        //private IT6512C IT6512C;    //Battery simulation
        public TestItemStep015_P1()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
           
            
            try
            {
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_12V_Ena, 1);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Attachment_EN, 1);
                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP317_AI7);
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
                    double average3 = taskADC.getAverage(0) * (6.8 + 6.8+6.8) / 6.8; ;
                    sMeasured = average3.ToString("F3");
               
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
            taskADC = null;        //free memory;
          
            
            GC.Collect();
            GC.WaitForPendingFinalizers();

        }
    }
    
    class TestItemStep015_P2 : TestItemForm
    {
        public string step = "_015 P2";
        private DAQadc taskADC;
        private double r1 = 6.8;
        private double r2 = 6.8;
        private int testCount;
        public TestItemStep015_P2()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            
            try
            {
                testCount = 0;
                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP313_AI24);


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
                    double average3 = taskADC.getAverage(0) * (6.8 + 6.8) / 6.8; ;
                    sMeasured = average3.ToString("F3");

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
                        if (testCount > 10)
                        {
                            sMeasured = "False";
                            return WORK_STATUS.FAIL;

                        }
                        StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_12V_Ena, 0);
                        System.Threading.Thread.Sleep(200);
                        StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_12V_Ena, 1);
                        System.Threading.Thread.Sleep(200);
                        taskADC = null;
                        taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                        taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP313_AI24);
                        taskADC.start();
                        testCount++;
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
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
        }
    }
    class TestItemStep015_P3 : TestItemForm
    {
        public string step = "_015 P3";
        private DAQadc taskADC;

        private DAQdio taskdio1;
        public TestItemStep015_P3()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {

           
            try
            {
           
                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_5);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP328_AI6);
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
                    double average3 = taskADC.getAverage(0) ; 
                    sMeasured = average3.ToString("F3");

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

    class TestItemStep015_P4 : TestItemForm
    {
        public string step = "_015 P4";
        private DAQadc taskADC;

        private DAQdio dio;
        private int testCount;
        public TestItemStep015_P4()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {

            testCount = 0;
            try
            {
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Attachment_EN, 0);
                System.Threading.Thread.Sleep(200);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Attachment_EN, 1);
                dio = new DAQdio(IO_USB6525_A.deviceName);
                dio.addOneLineWriteCH(IO_USB6525_A.Relay_I2C_K4_K5_P03);
                //System.Threading.Thread.Sleep(200);
                //dio.setOneData(true); //enable I2C
                //System.Threading.Thread.Sleep(200);

                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_5);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP615_AI30);
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
                    double average3 = taskADC.getAverage(0);
                    sMeasured = average3.ToString("F3");

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
                        if (testCount > 20)
                        {
                            sMeasured = "False";
                            return WORK_STATUS.FAIL;

                        }
                        StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Attachment_EN, 0);
                        System.Threading.Thread.Sleep(200);
                        StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Attachment_EN, 1);
                        System.Threading.Thread.Sleep(200);
                        taskADC = null;
                        taskADC = new DAQadc(
                     10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_5);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                        taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP615_AI30);
                        taskADC.start();
                        testCount++;

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
            dio = null;
        }
    }

    class TestItemStep015_P5 : TestItemForm
    {
        public string step = "_015 P5";
        private DAQadc taskADC;
        public TestItemStep015_P5()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {

            StaticValues.hwTestRPCDevice.ConfigurePin(StaticValues.uC_SupplyVoltage14V,
                   HardwareTestRPCSerialDevice.PinTypeValue.AdcPin,
                   HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);
            if (StaticValues.hwTestRPCDevice.ConfigureADC(0, 0, 2, 2))
            {
                Console.WriteLine("ConfigureADC uC_SupplyVoltage14V pass ");

            }

            return WORK_STATUS.PASS;

           
        }
        public override WORK_STATUS Proccess()
        {
            sMeasured = "";
            ushort data = 0;
            StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_SupplyVoltage14V, ref data);
            //Console.WriteLine(" uC_SupplyVoltage14V ADC = "  +data);
            if (data > 0)
            {
                double volt = 1.0 * (double)data / 4095;
                //Console.WriteLine("ADC =  " + data + " ; volt =  " + volt);
                double aa = volt * 25.0;
                sMeasured = aa.ToString("F3");
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

    class TestItemStep015_P6 : TestItemForm
    {
        public string step = "_015 P6";
        private DR2003 DR2003;
        private int testCount = 0;
        public TestItemStep015_P6()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            DR2003 = new DR2003(Power_DR2002MO.comPort, Power_DR2002MO.baudRate);

            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            DR2003.Connect();
            sMeasured = DR2003.MeasureCURR();
            DR2003.Disconnect();
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

            testCount++;
            if (testCount > 10)
                re = WORK_STATUS.FAIL;

            return re;
        }

        public override void release()
        {
            DR2003 = null;
            testCount = 0;
        }
    }

}
