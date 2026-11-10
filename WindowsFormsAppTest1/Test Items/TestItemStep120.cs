using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;

    class TestItemStep120_P1 : TestItemForm
    {
        public string step = "_120 P1";
        private DAQadc taskADC;
        public TestItemStep120_P1()
        {
            sStep = step;
            LoadSpec();            
        }

        public override WORK_STATUS Init()
        {
            try
            {
            
                ushort DACval = 0;
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_STS_IR, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_STS_Red, 1);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_SCA_I_DAC, DACval);
                System.Threading.Thread.Sleep(200);
                taskADC = new DAQadc(
                        10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_5);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                    taskADC.addDChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP603_AI4);

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
                    double measured_v0 = taskADC.getAverage(0) * (133 + 133) / 133;

                    Console.WriteLine("TP603_AI4 measured: {0}", measured_v0.ToString());
                    sMeasured = measured_v0.ToString("F3");
                    if (PassFailJudge())
                    {
                        return WORK_STATUS.PASS;
                    }
                    else
                    {
                        return WORK_STATUS.FAIL;
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
            taskADC = null;
        }
    }
    class TestItemStep120_P2 : TestItemForm
    {
        public string step = "_120 P2";
        private DAQadc taskADC;
        private int testCount;
        public TestItemStep120_P2()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            try
            {

                ushort DACval = 511;
                testCount = 0;
                //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_STS_IR, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_STS_Red, 1);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_SCA_I_DAC, DACval);
                System.Threading.Thread.Sleep(200);
                taskADC = new DAQadc(
                        10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_5);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                taskADC.addDChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP603_AI4);

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
                    double measured_v0 = taskADC.getAverage(0) * (133 + 133) / 133;

                    Console.WriteLine("TP603_AI4 measured: {0}", measured_v0.ToString());
                    sMeasured = measured_v0.ToString("F3");


                    if (PassFailJudge())
                    {
                        return WORK_STATUS.PASS;
                    }
                    else
                    {
                        ushort DACval = 511;
                        
                        StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_STS_IR, 1);
                        StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_STS_Red, 0);
                        System.Threading.Thread.Sleep(200);
                        StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_STS_IR, 0);
                        StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_STS_Red, 1);
                        StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_SCA_I_DAC, DACval);
                        System.Threading.Thread.Sleep(300);
                        if (testCount > 10)
                        {
                            sMeasured = "False";
                            return WORK_STATUS.FAIL;

                        }
                        taskADC = null;
                        taskADC = new DAQadc(
                        10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_5);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                        taskADC.addDChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP603_AI4);
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
            taskADC = null;
        }
    }

    class TestItemStep120_P3 : TestItemForm
    {
        public string step = "_120 P3";
        private DAQadc taskADC;
        public TestItemStep120_P3()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            try
            {

                ushort DACval = 1023;
                //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_STS_IR, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_STS_Red, 1);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_SCA_I_DAC, DACval);
                System.Threading.Thread.Sleep(200);
                taskADC = new DAQadc(
                        10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_5);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                taskADC.addDChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP603_AI4);

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
                    double measured_v0 = taskADC.getAverage(0) * (133 + 133) / 133;

                    Console.WriteLine("TP603_AI4 measured: {0}", measured_v0.ToString());
                    sMeasured = measured_v0.ToString("F3");
                    if (PassFailJudge())
                    {
                        return WORK_STATUS.PASS;
                    }
                    else
                    {
                        return WORK_STATUS.FAIL;
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
            taskADC = null;
        }
    }

    class TestItemStep120_P4 : TestItemForm
    {
        public string step = "_120 P4";
        private DAQadc taskADC;
        public TestItemStep120_P4()
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
                taskADC.addDChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP604_AI5);

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
                    double measured_v0 = taskADC.getAverage(0) * (133 + 133) / 133;

                    Console.WriteLine("TP604_AI5 measured: {0}", measured_v0.ToString());
                    sMeasured = measured_v0.ToString("F3");

                    if (PassFailJudge())
                    {
                        return WORK_STATUS.PASS;
                    }
                    else
                    {
                        return WORK_STATUS.FAIL;
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
            taskADC = null;
        }
    }

}
