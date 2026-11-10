using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;

    class TestItemStep117_P1 : TestItemForm
    {
        public string step = "_117 P1";

        public TestItemStep117_P1()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            StaticValues.hwTestRPCDevice.ConfigurePin(StaticValues.uC_AttachmentConnected,
                    HardwareTestRPCSerialDevice.PinTypeValue.InputPin,
                    HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);

            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
           
            sMeasured = "";
            ushort data = 0;

            if (StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_AttachmentConnected, ref data))
            {
                sMeasured =data.ToString("F3");

                //check limitation
                if (PassFailJudge())
                {
                    return WORK_STATUS.PASS;
                }
            }

            return WORK_STATUS.FAIL;
        }

        public override void release()
        {

        }
    }


    class TestItemStep117_P2 : TestItemForm
    {
        public string step = "_117 P2";

        private DAQadc taskADC;

        public TestItemStep117_P2()
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
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP627_AI14);

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
                    double measured_v = average;//* (r1 + r2) / r2;
                    sMeasured = measured_v.ToString("F3");
                    Console.WriteLine("ai TP627_AI14, measured: {0}", measured_v);

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
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
    class TestItemStep117_P3 : TestItemForm
    {
        public string step = "_117 P3";

        private DAQadc taskADC;

        public TestItemStep117_P3()
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
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP626_AI27);

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
                    double measured_v = average;//* (r1 + r2) / r2;
                    sMeasured = measured_v.ToString("F3");
                    Console.WriteLine("ai TP626_AI27, measured: {0}", measured_v);

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
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
    class TestItemStep117_P4 : TestItemForm
    {
        public string step = "_117 P4";
        private DAQadc taskADC;
        private DAQdio dio;

        public TestItemStep117_P4()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            
            try
            {
                //DIO setting
                dio = new DAQdio(IO_USB6525_A.deviceName);
                dio.addOneLineWriteCH(IO_USB6525_A.Relay_Att_Conn1_TP512_K1_P00);
                dio.setOneData(true);//Switch on relay

               
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
            sMeasured = "";
            ushort data = 0;

            if (StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_AttachmentConnected, ref data))
            {
                sMeasured = sMeasured = Convert.ToString(data);

                //check limitation
                if (PassFailJudge())
                {
                    return WORK_STATUS.PASS;
                }
            }

            return WORK_STATUS.FAIL;
        }

        public override void release()
        {
            
            dio = null;
        }
    }

    class TestItemStep117_P5 : TestItemForm
    {
        public string step = "_117 P5";

        private DAQadc taskADC;

        public TestItemStep117_P5()
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
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP627_AI14);

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
                    double measured_v = average;//* (r1 + r2) / r2;
                    sMeasured = measured_v.ToString("F3");
                    Console.WriteLine("ai TP627_AI14, measured: {0}", measured_v);

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
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }

    class TestItemStep117_P6 : TestItemForm
    {
        public string step = "_117 P6";

        private DAQadc taskADC;
        private int testCount;
        public TestItemStep117_P6()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            testCount = 0;
            try
            {
                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_5);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP625_AI19);

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
                    double measured_v = average;//* (r1 + r2) / r2;
                    sMeasured = measured_v.ToString("F3");
                    Console.WriteLine("ai TP625_AI19, measured: {0}", measured_v);

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
                            //sMeasured = "False";
                            return WORK_STATUS.FAIL;

                        }
                        taskADC = null;
                        taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_5);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                        taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP625_AI19);
                        taskADC.start();
                        testCount++;
                        //re = WORK_STATUS.FAIL;
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
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }

    class TestItemStep117_P7 : TestItemForm
    {
        public string step = "_117 P7";
        private DAQadc taskADC;
        private DAQdio dio;

        public TestItemStep117_P7()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {

            try
            {
                //DIO setting
                dio = new DAQdio(IO_USB6525_A.deviceName);
                dio.addOneLineWriteCH(IO_USB6525_A.Relay_Att_Conn2_TP517_K2_P01);
                dio.setOneData(true);//Switch on relay


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
            sMeasured = "";
            ushort data = 0;

            if (StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_AttachmentConnected, ref data))
            {
                 sMeasured = Convert.ToString(data);

                //check limitation
                if (PassFailJudge())
                {
                    return WORK_STATUS.PASS;
                }
            }

            return WORK_STATUS.FAIL;
        }

        public override void release()
        {

            dio = null;
        }
    }

    class TestItemStep117_P8 : TestItemForm
    {
        public string step = "_117 P8";

        private DAQadc taskADC;

        public TestItemStep117_P8()
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
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP627_AI14);

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
                    double measured_v = average;//* (r1 + r2) / r2;
                    sMeasured = measured_v.ToString("F3");
                    Console.WriteLine("ai TP627_AI14, measured: {0}", measured_v);

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
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }

    class TestItemStep117_P9 : TestItemForm
    {
        public string step = "_117 P9";

        private DAQadc taskADC;

        public TestItemStep117_P9()
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
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP624_AI26);

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
                    double measured_v = average;//* (r1 + r2) / r2;
                    sMeasured = measured_v.ToString("F3");
                    Console.WriteLine("ai TP624_AI26, measured: {0}", measured_v);

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
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
    class TestItemStep117_P10 : TestItemForm
    {
        public string step = "_117 P10";
        private DAQadc taskADC;
        private DAQdio dio;

        public TestItemStep117_P10()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {

            try
            {
                //DIO setting
                dio = new DAQdio(IO_USB6525_A.deviceName);
                dio.addOneLineWriteCH(IO_USB6525_A.Relay_Att_Conn3_TP518_K3_P02);
                dio.setOneData(true);//Switch on relay


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
            sMeasured = "";
            ushort data = 0;

            if (StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_AttachmentConnected, ref data))
            {
                sMeasured =data.ToString("F3");

                //check limitation
                if (PassFailJudge())
                {
                    return WORK_STATUS.PASS;
                }
            }

            return WORK_STATUS.FAIL;
        }

        public override void release()
        {

            dio = null;
        }
    }

    class TestItemStep117_P11 : TestItemForm
    {
        public string step = "_117 P11";

        private DAQadc taskADC;

        public TestItemStep117_P11()
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
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP627_AI14);

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
                    double measured_v = average;//* (r1 + r2) / r2;
                    sMeasured = measured_v.ToString("F3");
                    Console.WriteLine("ai TP627_AI14, measured: {0}", measured_v);

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
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }






}
