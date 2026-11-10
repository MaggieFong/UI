using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;

    // 008

    class TestItemStep008 : TestItemForm
    {
        public string step = "_008";

        public TestItemStep008()
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
            return WORK_STATUS.PASS;
        }

        public override void release()
        {

        }
    }
    class TestItemStep008_P1 : TestItemForm
    {
        public string step = "_008 P1";

        public TestItemStep008_P1()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            Console.WriteLine(sTest + " is initial");
            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            if (Dut.comPort != null)
            {
                try
                {
                    if (StaticValues.hwTestRPCDevice == null)
                    {
                        System.Threading.Thread.Sleep(100);
                        StaticValues.hwTestRPCDevice = new HardwareTestRPCSerialDevice.Device(StaticValues.sSerialPort,115200);
                        //string readStatus_msg = Console.ReadLine();
                        if (StaticValues.hwTestRPCDevice == null)
                            return WORK_STATUS.FAIL;
                    }

                    // Get uC interface version
                    //System.Threading.Thread.Sleep(100);
                    byte value = StaticValues.hwTestRPCDevice.GetInterfaceVersion();

                    sMeasured = Convert.ToString(value);

                    //check limitation
                    if (PassFailJudge())
                    {
                        return WORK_STATUS.PASS;
                    }
                    else
                    {
                        return WORK_STATUS.FAIL;
                    }
                }
                catch (System.Exception e)
                {
                    sMeasured = e.Message;
                    StaticValues.hwTestRPCDevice = null;
                    Console.WriteLine(sTest + " Exception");

                    //return WORK_STATUS.FAIL;
                }
            }
            else
            {
                sMeasured = "Serial port error";
                return WORK_STATUS.FAIL;
            }
            return WORK_STATUS.WORKING;
        }

        public override void release()
        {

        }
    }
        //class TestItemStep008_P1 : TestItemForm
        //{
        //    public string step = "_008 P1";
        //    private DR2003 DR2003;
        //    public TestItemStep008_P1()
        //    {
        //        sStep = step;
        //        LoadSpec();
        //    }

        //    public override WORK_STATUS Init()
        //    {
        //        //Console.WriteLine(sTest + " is initial");
        //        DR2003 = new DR2003(Power_DR2002MO.comPort, Power_DR2002MO.baudRate);
        //        DR2003.Connect();
        //        DR2003.OutPutOff();
        //        System.Threading.Thread.Sleep(20);
        //        DR2003.OutPutOn();
        //        return WORK_STATUS.PASS;
        //    }

        //    public override WORK_STATUS Proccess()
        //    {
        //        //Console.WriteLine(sTest + " is proccessing");
        //        //StaticValues.sSerialPort = "COM" + Dut.comPort;

        //        if (Dut.comPort != null)
        //        {
        //            try
        //            {
        //                if (StaticValues.hwTestRPCDevice == null)
        //                {
        //                    DR2003.OutPutOn();
        //                    System.Threading.Thread.Sleep(100);
        //                    StaticValues.hwTestRPCDevice = new HardwareTestRPCSerialDevice.Device(StaticValues.sSerialPort,115200);
        //                    if (StaticValues.hwTestRPCDevice == null)
        //                        return WORK_STATUS.FAIL;
        //                }

        //                // Get uC interface version
        //                //System.Threading.Thread.Sleep(100);
        //                byte value = StaticValues.hwTestRPCDevice.GetInterfaceVersion();

        //                sMeasured = Convert.ToString(value);

        //                //check limitation
        //                if (PassFailJudge())
        //                {
        //                    return WORK_STATUS.PASS;
        //                }
        //                else
        //                {
        //                    return WORK_STATUS.FAIL;
        //                }
        //            }
        //            catch (System.Exception e)
        //            {
        //                sMeasured = e.Message;

        //                StaticValues.hwTestRPCDevice = null;
        //                DR2003.OutPutOff();
        //                Console.WriteLine(sTest + " Exception");
        //                //return WORK_STATUS.FAIL;
        //            }
        //        }
        //        else
        //        {
        //            sMeasured = "Serial port error";
        //            return WORK_STATUS.FAIL;
        //        }
        //        return WORK_STATUS.WORKING;
        //    }

        //    public override void release()
        //    {
        //        DR2003.OutPutOn();
        //        DR2003.Disconnect();
        //        DR2003 = null;
        //    }
        //}

        class TestItemStep008_P2 : TestItemForm
    {
        public string step = "_008 P2";
        private DAQadc taskADC;
        public TestItemStep008_P2()
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
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP328_AI6);

                if (taskADC.start())
                {
                    sMeasured = "init pass";
                    return WORK_STATUS.PASS;
                }
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
                    sMeasured = Convert.ToString(measured_v);
                    //Console.WriteLine("ai TP320_AI6  measured: {0}",  measured_v);

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
            //return WORK_STATUS.PASS;
        }

        public override void release()
        {

            taskADC = null;        //free memory
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
