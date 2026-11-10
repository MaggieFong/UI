using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RPCSerialDevice;
namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;
    using NationalInstruments.DAQmx;
    using System.Diagnostics;

    //class TestItemStep22_18_1 : TestItemForm
    //{
    //    public string step = "_22.18.1";

    //    public TestItemStep22_18_1()
    //    {
    //        sStep = step;
    //        LoadSpec();
    //    }

    //    public override WORK_STATUS Init()
    //    {
    //        if (!File.Exists(ProdConfig.fileProgrammer))
    //        {
    //            sMeasured = "atprogram.exe not found";
    //            return WORK_STATUS.FAIL;
    //        }
    //        else if (!File.Exists(ProdConfig.fileProductCode))
    //        {
    //            sMeasured = "ProductCode not found";
    //            Console.WriteLine("ProdConfig.fileProductCode " + ProdConfig.fileProductCode);
    //            return WORK_STATUS.FAIL;
    //        }
    //        else
    //            return WORK_STATUS.PASS;
    //        //try

    //    }
    //    public override WORK_STATUS Proccess()
    //    {
    //        WORK_STATUS re = WORK_STATUS.FAIL;
    //        sMeasured = "Program fail";

    //        // Use ProcessStartInfo class
    //        ProcessStartInfo startInfo = new ProcessStartInfo();
    //        startInfo.CreateNoWindow = false;
    //        startInfo.UseShellExecute = false;
    //        startInfo.FileName = ProdConfig.fileProgrammer;
    //        //startInNI USB-6218 "6218A"fo.WindowStyle = ProcessWindowStyle.Hidden;
    //        //startInfo.Arguments = "-t atmelice -i SWD -d ATSAMD20J18 -cl 10mhz chiperase program -f " + ProdConfig.fileTestCode;
    //        startInfo.Arguments = "-t samice -i SWD -d ATSAMD20J18 -cl 10mhz chiperase program -f " + ProdConfig.fileTestCode;

    //        //Console.WriteLine("startInfo.Arguments :" + startInfo.Arguments);

    //        try
    //        {
    //            // Start the process with the info we specified.
    //            // Call WaitForExit and then the using statement will close.
    //            using (Process exeProcess = Process.Start(startInfo))
    //            {
    //                bool result = exeProcess.WaitForExit(0xffff);

    //                //Console.WriteLine("program result: " + exeProcess.ExitCode);
    //                //exeProcess.WaitForExit();

    //                if (exeProcess.ExitCode == 0)
    //                {
    //                    //sMeasured = expected;
    //                    sMeasured = "Error = 0";
    //                    System.Threading.Thread.Sleep(100);
    //                    re = WORK_STATUS.PASS;
    //                }
    //                else
    //                {
    //                    //sMeasured = "Error code: " + exeProcess.ExitCode;
    //                    sMeasured = "Error = " + exeProcess.ExitCode;
    //                }
    //            }
    //        }
    //        catch
    //        {
    //            // Log error.
    //        }

    //        return re;
    //    }

    //    public override void release()
    //    {
    //        //System.Threading.Thread.Sleep(3000);
    //    }
    //}
    class TestItemStep22_18_1 : TestItemForm
    {
        public string step = "_22.18.1";
        private int retryCount = 2;
        //string batFile = null;
        string ExePath = Path.GetDirectoryName(ProdConfig.fileProgrammer);
        string codeFile = Path.GetFileName(ProdConfig.fileProductCode);

        public TestItemStep22_18_1()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            if (!File.Exists(ProdConfig.fileProgrammer))
            {
                sMeasured = "atprogram.exe not found";
                return WORK_STATUS.FAIL;
            }
            else if (!File.Exists(ProdConfig.fileScript))
            {
                sMeasured = "Script file not found";
                //Console.WriteLine("ProdConfig.fileScript " + ProdConfig.fileScript);
                return WORK_STATUS.FAIL;
            }
            else if (!File.Exists(ProdConfig.fileProductCode))
            {
                sMeasured = "Product firmware not found";
                //Console.WriteLine("ProdConfig.fileProductCode " + ProdConfig.fileProductCode);
                return WORK_STATUS.FAIL;
            }

            File.Copy(ProdConfig.fileProductCode, ExePath + @"\" + codeFile, true);

            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.FAIL;

            string ExePath = Path.GetDirectoryName(ProdConfig.fileProgrammer);

            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.WorkingDirectory = ExePath;
            startInfo.FileName = ProdConfig.fileProgrammer;
            startInfo.Arguments = " program Sunstone.elf --setfuse --secure atmelice";
            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    bool result = exeProcess.WaitForExit(0xffff);

                    //Console.WriteLine("program result: " + exeProcess.ExitCode);
                    //exeProcess.WaitForExit();

                    if (exeProcess.ExitCode == 0)
                    {
                        //sMeasured = expected;
                        sMeasured = "Error = 0";
                        re = WORK_STATUS.PASS;
                    }
                    else
                    {
                        //sMeasured = "Error code: " + exeProcess.ExitCode;
                        sMeasured = "Error = " + exeProcess.ExitCode;
                    }
                }
            }
            catch
            {
                // Log error.
            }

            return re;
        }
        //public override WORK_STATUS Proccess()
        //{
        //    WORK_STATUS re = WORK_STATUS.FAIL;
        //    sMeasured = "Program fail";

        //    // Use ProcessStartInfo class
        //    ProcessStartInfo startInfo = new ProcessStartInfo();
        //    startInfo.CreateNoWindow = false;
        //    startInfo.UseShellExecute = false;
        //    startInfo.FileName = ProdConfig.fileProgrammer;
        //    //startInNI USB-6218 "6218A"fo.WindowStyle = ProcessWindowStyle.Hidden;
        //    //startInfo.Arguments = "-t atmelice -i SWD -d ATSAMD20J18 -cl 10mhz chiperase program -f " + ProdConfig.fileTestCode;
        //    startInfo.Arguments = "-t samice -i SWD -d ATSAMD20J18 -cl 10mhz chiperase program -f " + ProdConfig.fileProductCode;

        //    //Console.WriteLine("startInfo.Arguments :" + startInfo.Arguments);

        //    try
        //    {
        //        // Start the process with the info we specified.
        //        // Call WaitForExit and then the using statement will close.
        //        using (Process exeProcess = Process.Start(startInfo))
        //        {
        //            bool result = exeProcess.WaitForExit(0xffff);

        //            //Console.WriteLine("program result: " + exeProcess.ExitCode);
        //            //exeProcess.WaitForExit();
        //            sMeasured = exeProcess.ExitCode.ToString().Trim();
        //            if (sMeasured == "0")
        //            {
        //                //sMeasured = expected;
        //                sMeasured = "Error = 0";
        //                //System.Threading.Thread.Sleep(200);
        //                re = WORK_STATUS.PASS;
        //            }
        //            else
        //            {
        //                retryCount--;
        //                if (retryCount == 0)
        //                {
        //                    return WORK_STATUS.FAIL;
        //                }
        //                else
        //                {
        //                    return WORK_STATUS.WORKING;
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        // Log error.
        //    }

        //    return re;
        //}
        public override void release()
        {
            File.Delete(ExePath + @"\" + codeFile);
        }
    }

    class TestItemStep22_18_2 : TestItemForm
    {
        public string step = "_22.18.2";

        public TestItemStep22_18_2()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            StaticValues.setS22TestTimer();
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            sMeasured = "True";
            return WORK_STATUS.PASS;
        }

        public override void release()
        {

        }
    }


    class TestItemStep22_18_3 : TestItemForm
    {
        public string step = "_22.18.3";
        //private DR2003 DR2003;
        private DAQadc taskADC;
        private double r1 = 2000;
        private double r2 = 1000;
        //private DAQdio dio;
        public TestItemStep22_18_3()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            try
            {

                taskADC = new DAQadc(
                    10000, 50, DAQadc.VOLTAGE_RANGE.RANGE_0_10);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP111_AI23);
                //System.Threading.Thread.Sleep(400);
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
                    double average = taskADC.getAverage(0) * (r1 + r2) / r2;
                    double measured_v = average;
                    sMeasured = Convert.ToString(measured_v);
                    Console.WriteLine("ai TP111_AI23 measured: {0}", measured_v);

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
            taskADC = null;
        }
    }


    class TestItemStep22_18_4 : TestItemForm
    {
        public string step = "_22.18.4";
        private DR2003 DR2003;
        //private int count;
        public TestItemStep22_18_4()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //DAQ setting
            System.Threading.Thread.Sleep(50);
            //count = 0;
            try
            {
                DR2003 = new DR2003(Power_DR2002MO.comPort, Power_DR2002MO.baudRate);
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

            sMeasured = "";
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
            else
            {
                System.Threading.Thread.Sleep(50);
                DR2003.Connect();
                sMeasured = DR2003.MeasureCURR();
                DR2003.Disconnect();
                Console.WriteLine("DR2003.MeasureCURR");
                //count++;
                //if (count > 3)
                //{
                //    re = WORK_STATUS.FAIL;
                //}
            }
            return re;
        }

        public override void release()
        {
            DR2003 = null;        //free memory
        }
    }


    class TestItemStep22_18_5 : TestItemForm
    {
        public string step = "_22.18.5";

        public TestItemStep22_18_5()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            System.Threading.Thread.Sleep(800);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            sMeasured = "True";
            return WORK_STATUS.PASS;

        }

        public override void release()
        {

        }
    }

    //class TestItemStep22_18_6 : TestItemForm
    //{
    //    public string step = "_22.18.6";

    //    private Feasa_LED myFS_LED1;
    //    private RGB_Data RGB_ch0;
    //    private XY_Data XY_ch0;

    //    public TestItemStep22_18_6()
    //    {
    //        sStep = step;
    //        LoadSpec();
    //    }

    //    public override WORK_STATUS Init()
    //    {
    //        try
    //        {

    //            long delta = DateTime.Now.Ticks - StaticValues.S22_StartTime;
    //            float tmpT = (float)(delta) / 10000; // ms
    //            int testTime = Convert.ToInt32(tmpT % 675 + 100);
    //            Console.WriteLine("Set Checking LED Setting 1 time: " + testTime);
    //            StaticValues.s22_samplerate = testTime;
    //        }
    //        catch (Exception e)
    //        {
    //            StaticValues.s22_samplerate = 675;
    //        }

    //        try
    //        {
    //            RGB_ch0 = new RGB_Data();
    //            XY_ch0 = new XY_Data();
    //            myFS_LED1 = new Feasa_LED(Feasa_10F_A.comPort);
    //        }
    //        catch (Exception e)
    //        {
    //            sMeasured = e.Message;
    //            return WORK_STATUS.FAIL;
    //        }
    //        return WORK_STATUS.PASS;
    //    }

    //    public override WORK_STATUS Proccess()
    //    {
    //        sMeasured = "";

    //        System.Threading.Thread.Sleep(StaticValues.s22_samplerate);
    //        myFS_LED1.doSequenceCapture(20, 20, 30);
    //        System.Threading.Thread.Sleep(200);

    //        myFS_LED1.ReadXYWithSeqMax(1, XY_ch0);
    //        if (!checkwhileLED(XY_ch0))
    //        {
    //            return WORK_STATUS.FAIL;
    //        }
    //        return WORK_STATUS.PASS;
    //    }

    //    public override void release()
    //    {
    //        myFS_LED1.Close();
    //        myFS_LED1 = null;
    //        RGB_ch0 = null;
    //        XY_ch0 = null;
    //    }

    //    private bool checkwhileLED(XY_Data data)
    //    {
    //        //Console.WriteLine("X : " + data.val_X);
    //        //Console.WriteLine("Y : " + data.val_Y);
    //        //Console.WriteLine("I White: " + data.val_I_XY);
    //        if (data.val_X < Feasa_10F_A.WLED_X_LLimit || data.val_X > Feasa_10F_A.WLED_X_ULimit ||
    //            data.val_Y < Feasa_10F_A.WLED_Y_LLimit || data.val_Y > Feasa_10F_A.WLED_Y_ULimit)
    //        {
    //            sMeasured = data.val_I_XY + " " + data.val_X + " " + data.val_Y;
    //            return false;
    //        }
    //        sMeasured = data.val_I_XY.ToString();
    //        if (!PassFailJudge())
    //        {
    //            sMeasured = data.val_I_XY + " " + data.val_X + " " + data.val_Y;
    //            return false;
    //        }
    //        sMeasured = data.val_I_XY + " " + data.val_X + " " + data.val_Y;
    //        return true;
    //    }
    //}


    //class TestItemStep22_18_6 : TestItemForm
    //{
    //    public string step = "_22.18.6";

    //    //private Feasa_LED myFS_LED1;
    //    //private RGB_Data RGB_ch0;
    //    //private XY_Data XY_ch0;
    //    private DAQadc taskADC;
    //    private int testCount;
    //    public TestItemStep22_18_6()
    //    {
    //        sStep = step;
    //        LoadSpec();
    //    }

    //    public override WORK_STATUS Init()
    //    {
    //        testCount = 0;
    //        //try
    //        //{

    //        //    long delta = DateTime.Now.Ticks - StaticValues.S22_StartTime;
    //        //    float tmpT = (float)(delta) / 10000; // ms
    //        //    int testTime = Convert.ToInt32(tmpT % 675 + 100);
    //        //    Console.WriteLine("Set Checking LED Setting 1 time: " + testTime);
    //        //    StaticValues.s22_samplerate = testTime;
    //        //}
    //        //catch (Exception e)
    //        //{
    //        //    StaticValues.s22_samplerate = 675;
    //        //}
    //        try
    //        {
    //            taskADC = new DAQadc(
    //                    1000, 30, DAQadc.VOLTAGE_RANGE.RANGE_N5_5);
    //            taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP102_AI15);//
    //            if (taskADC.start())
    //            {
    //                sMeasured = "init pass";
    //                return WORK_STATUS.PASS;
    //            }
    //            else
    //            {
    //                sMeasured = taskADC.getErrorMsg();
    //                return WORK_STATUS.FAIL;
    //            }


    //        }
    //        catch (Exception e)
    //        {
    //            sMeasured = e.Message;
    //            return WORK_STATUS.FAIL;
    //        }
    //        return WORK_STATUS.PASS;
    //    }

    //    public override WORK_STATUS Proccess()
    //    {
    //        WORK_STATUS re = WORK_STATUS.WORKING;

    //        if (taskADC.isEnable() == false)
    //        {
    //            if (taskADC.getErrorMsg() == null)      //must check if there were any error when DAQadc.callbackADC()
    //            {
    //                //StaticValues.s22_LED_NIdata = taskADC.getRawData(0);
    //                double average = taskADC.getAverage(0);
    //                sMeasured = Convert.ToString(average);
    //                Console.WriteLine("ai TP111_AI23 measured: {0}", sMeasured);
    //                //check limitation
    //                if (average < 3.5 && average > 2.5)
    //                {
    //                    sMeasured = "True";
    //                    return WORK_STATUS.PASS;

    //                }
    //                else
    //                {
    //                    if (testCount > 20)
    //                    {
    //                        sMeasured = "False";
    //                        return WORK_STATUS.FAIL;

    //                    }
    //                    taskADC = null;
    //                    taskADC = new DAQadc(
    //                    1000, 30, DAQadc.VOLTAGE_RANGE.RANGE_N5_5);
    //                    taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP102_AI15);//
    //                    taskADC.start();
    //                    testCount++;

    //                }

    //            }
    //            else
    //            {
    //                sMeasured = taskADC.getErrorMsg();
    //                re = WORK_STATUS.FAIL;
    //            }
    //        }

    //        return re;
    //        //return WORK_STATUS.PASS;
    //    }

    //    public override void release()
    //    {
    //        taskADC = null;        //free memory
    //        testCount = 0;
    //    }
    //    private bool checkLedVolt()
    //    {
    //        int num = 0;
    //        int passcount = 0;

    //        double current_value;
    //        while (num < StaticValues.s22_LED_NIdata.Length)
    //        {
    //            current_value = Convert.ToDouble(StaticValues.s22_LED_NIdata.GetValue(num));
    //            //Console.WriteLine("S22 {0}", current_value);
    //            if (current_value < 3.5 && current_value > 2.5)
    //            {
    //                passcount++;
    //            }

    //            num++;
    //        }
    //        Console.WriteLine("s22 checkLedVolt count = {0}", passcount);
    //        if (passcount > 2)
    //        {
    //            return true;
    //        }
    //        return false;
    //    }
    //    private bool checkwhileLED(XY_Data data)
    //    {
    //        //Console.WriteLine("X : " + data.val_X);
    //        //Console.WriteLine("Y : " + data.val_Y);
    //        //Console.WriteLine("I White: " + data.val_I_XY);
    //        if (data.val_X < Feasa_10F_A.WLED_X_LLimit || data.val_X > Feasa_10F_A.WLED_X_ULimit ||
    //            data.val_Y < Feasa_10F_A.WLED_Y_LLimit || data.val_Y > Feasa_10F_A.WLED_Y_ULimit)
    //        {
    //            sMeasured = data.val_I_XY + " " + data.val_X + " " + data.val_Y;
    //            return false;
    //        }
    //        sMeasured = data.val_I_XY.ToString();
    //        if (!PassFailJudge())
    //        {
    //            sMeasured = data.val_I_XY + " " + data.val_X + " " + data.val_Y;
    //            return false;
    //        }
    //        sMeasured = data.val_I_XY + " " + data.val_X + " " + data.val_Y;
    //        return true;
    //    }
    //}
    class TestItemStep22_18_6 : TestItemForm
    {
        public string step = "_22.18.6";


    
        public TestItemStep22_18_6()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            
            try
            {
                if (StaticValues.hwTestRPCDevice != null)
                {
                    StaticValues.hwTestRPCDevice.Dispose();
                    StaticValues.hwTestRPCDevice = null;
                }
                if (StaticValues.rpcSerialDevice == null)
                {
                    StaticValues.rpcSerialDevice = new RPCSerialDevice.Device("COM56");
                }
              
                byte value1 = StaticValues.rpcSerialDevice.GetDeviceInterfaceVersion();

                Console.WriteLine("rpc version = {0}", Convert.ToString(value1));
                Console.WriteLine("rpc FW version = {0}", StaticValues.rpcSerialDevice.GetVersionNumber());
                return WORK_STATUS.PASS;
            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
            return WORK_STATUS.WORKING;
        }

        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            if (StaticValues.rpcSerialDevice != null)
            {
                StaticValues.rpcSerialDevice.Dispose();
                StaticValues.rpcSerialDevice = null;
            }
        }
        
    }


    class TestItemStep22_18_7 : TestItemForm
    {
        public string step = "_22.18.7";

        private DAQdio dio;
        private DAQdio dioB;
        private DAQadc taskADC;

        private int failcnt = 0;
        public TestItemStep22_18_7()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {//DIO setting
            try
            {
                //System.Threading.Thread.Sleep(200);
                failcnt = 0;
                dio = new DAQdio(IO_USB6525_A.deviceName);
                dio.addOneLineWriteCH(IO_USB6525_A.Relay_Discharge_Res_K12_K13_P04);
                dio.setOneData(true);

                dioB = new DAQdio(IO_USB6525_B.deviceName);
                dioB.addOneLineWriteCH(IO_USB6525_B.CTL_HV_RELAY_P05);
                dioB.setOneData(true);

                taskADC = new DAQadc(
                10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_N10_10);
                taskADC.addNChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP104_AI0);
                taskADC.addNChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP101_AI1);
                taskADC.start();
                System.Threading.Thread.Sleep(200);
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
            double FCV;
            double LMV;
            double FlashCapVoltage = 0;
            double LampVoltage = 0;
            
            if (taskADC.isEnable() == false)
            {
                if (taskADC.getErrorMsg() == null)
                {
                    FCV = taskADC.getAverage(0);
                    LMV = taskADC.getAverage(1);
                    FlashCapVoltage = FCV * (3000 + 47) / 47;
                    LampVoltage = LMV * (3000 + 47) / 47;
                    Console.WriteLine("FlashCapVoltage = "+ FlashCapVoltage);
                    Console.WriteLine("LampVoltage = " + LampVoltage);
                    if (FlashCapVoltage < 24 && LampVoltage < 24)
                    {
                        sMeasured = "True";
                        return WORK_STATUS.PASS;
                    }
                    else
                    {
                        taskADC = null;
                        Console.WriteLine("Voltage > 24V");
                        if (failcnt != 0)
                        {
                            sMeasured = "False:Volt = "+Convert.ToString(FlashCapVoltage);
                            return WORK_STATUS.FAIL;
                        }
                        System.Threading.Thread.Sleep(1000);
                        failcnt++;

                        try
                        {
                            taskADC = new DAQadc(
                            10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_N10_10);
                            taskADC.addNChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP104_AI0);
                            taskADC.addNChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP101_AI1);
                            taskADC.start();
                            
                        }
                        catch (Exception e)
                        {
                            sMeasured = e.Message;
                            return WORK_STATUS.FAIL;
                        }

                    }
                } 
            }
            
            return WORK_STATUS.WORKING;

        }

        public override void release()
        {
            dio = null;
            dioB = null;
            failcnt = 0;
            taskADC = null;
        }
    }


    class TestItemStep22_18_8 : TestItemForm
    {
        public string step = "_22.18.8";

        public TestItemStep22_18_8()
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
            sMeasured = "True";
            return WORK_STATUS.PASS;
        }

        public override void release()
        {

        }
    }


    class TestItemStep22_18_9 : TestItemForm
    {
        public string step = "_22.18.9";

        public TestItemStep22_18_9()
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
            sMeasured = "True";
            return WORK_STATUS.PASS;
        }

        public override void release()
        {

        }
    }
}
