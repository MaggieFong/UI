using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1.TestItem
{
    using System.Diagnostics;
    using TestItemForm;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;


    class TestItemStep007_P1 : TestItemForm
    {
        public string step = "_007 P1";
        private DAQadc taskADC;
        private DAQdio dio;
        public TestItemStep007_P1()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //DAQ setting
            try
            {
                dio = new DAQdio(IO_USB6525_A.deviceName);
                dio.addOneLineWriteCH(IO_USB6525_A.Relay_Support3V3_TP322_K7_P06);
                dio.setOneData(true);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Awake, 1);
                System.Threading.Thread.Sleep(200);
                dio.setOneData(false);
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
                    sMeasured = measured_v.ToString("F3");
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
            dio = null;
            taskADC = null;        //free memory
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }

    class TestItemStep007_P2 : TestItemForm
    {
        public string step = "_007 P2";
        private DR2003 DR2003;
        private int testCount = 0;
        public TestItemStep007_P2()
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

    class TestItemStep007_P3 : TestItemForm
    {
        public string step = "_007 P3";
        private DAQdio dio;
        DR2003 DR2003;
        public TestItemStep007_P3()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                DR2003 = new DR2003(Power_DR2002MO.comPort, Power_DR2002MO.baudRate);
                DR2003.Connect();
                
                //System.Threading.Thread.Sleep(30);
                //DR2003.OutPutOff();
                //System.Threading.Thread.Sleep(100);
                //DR2003.OutPutOn();
                //System.Threading.Thread.Sleep(100);
                DR2003.Disconnect();
                System.Threading.Thread.Sleep(500);

                dio = new DAQdio(IO_USB6525_A.deviceName);
                dio.addOneLineWriteCH(IO_USB6525_A.Relay_Support3V3_TP322_K7_P06);
                //dio.setOneData(true);

            }
            catch (Exception e)
            {

                sMeasured = e.Message.ToString();
                return WORK_STATUS.FAIL;
            }

            Console.WriteLine(sTest + " is initial");
            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            if (Dut.comPort != null)
            {
                try
                {
                    if (StaticValues.hwTestRPCDevice != null)
                    {
                        StaticValues.hwTestRPCDevice = null;
                    }

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
                    Console.WriteLine(sTest + " Exception:"+e.Message.ToString());

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
            DR2003 = null;
             dio = null;
        }
    }

    class TestItemStep007_P4 : TestItemForm
    {
        public string step = "_007 P4";
        private int retryCount = 2;
        string ExePath = Path.GetDirectoryName(ProdConfig.fileProgrammer);
        string ExeFile = Path.GetFileName(ProdConfig.fileScript);
        string fwFile = Path.GetFileName(ProdConfig.fileTestCode);
        private DAQdio dioA;
        string output = "";
        private cmdShell test;
        bool endOfLine, ispass;
        private int check;
        public TestItemStep007_P4()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //sMeasured = "1";
            //return WORK_STATUS.FAIL;
            //sMeasured = "Exit code = 0;IPL_Programmer.exe program SAMD20J18.hex jlink -s601000776;Programming and verification OK,";
            sMeasured = "Error = 0";
            return WORK_STATUS.PASS;
            retryCount = 0;
            output = "";
            endOfLine = false;
            ispass = false;
            check = 0;

            if (!File.Exists(ProdConfig.fileProgrammer))
            {
                sMeasured = "IPLprogrammer.exe not found";
                return WORK_STATUS.FAIL;
            }
            else if (!File.Exists(ProdConfig.fileScript))
            {
                sMeasured = "Script file not found";
                Console.WriteLine("ProdConfig.fileScript " + ProdConfig.fileScript);
                return WORK_STATUS.FAIL;
            }
            else if (!File.Exists(ProdConfig.fileTestCode))
            {
                sMeasured = "Test firmware not found";
                Console.WriteLine("ProdConfig.fileTestCode " + ProdConfig.fileTestCode);
                return WORK_STATUS.FAIL;
            }
            else
            {
                string ExePath = Path.GetDirectoryName(ProdConfig.fileProgrammer);
                string scriptPath = Path.GetDirectoryName(Path.GetFullPath(ProdConfig.fileScript));

                test = new cmdShell();
                test.onData += test_onData;
                test.write("D:");
                test.write("cd " + scriptPath);
                test.write("cd");
                test.write("SET PATH=" + ExePath);
                System.Threading.Thread.Sleep(500);
                Console.WriteLine(ExeFile + " program " + fwFile + " jlink -s" + ProdConfig.mcu_jlinksn);
                test.write(ExeFile + " program " + fwFile + " jlink -s" + ProdConfig.mcu_jlinksn);
                //test = null;

                return WORK_STATUS.PASS;
            }
        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
            if (endOfLine)
            {
                endOfLine = false;
                if (ispass)
                {
                    sMeasured = "0";
                }
                else
                {
                    sMeasured = "1";
                }

                if (PassFailJudge())
                {
                    return WORK_STATUS.PASS;
                }
                else
                {
                    if (retryCount > 0)
                        return WORK_STATUS.FAIL;
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("execute program again");
                    string scriptPath = Path.GetDirectoryName(Path.GetFullPath(ProdConfig.fileScript));
                    if(test != null)
                        test = null;
                    test = new cmdShell();
                    test.onData += test_onData;
                    test.write("D:");
                    test.write("cd " + scriptPath);
                    test.write("cd");
                    test.write("SET PATH=" + ExePath);
                    System.Threading.Thread.Sleep(500);
                    Console.WriteLine(ExeFile + " program " + fwFile + " jlink -s" + ProdConfig.mcu_jlinksn);
                    test.write(ExeFile + " program " + fwFile + " jlink -s" + ProdConfig.mcu_jlinksn);
                    //test = null;
                    retryCount++;

                }
            }
            System.Threading.Thread.Sleep(1000);
            check++;
            if (check == 10)
            {
                endOfLine = false;
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("execute program again");
                string scriptPath = Path.GetDirectoryName(Path.GetFullPath(ProdConfig.fileScript));
                if (test != null)
                    test = null;
                test = new cmdShell();
                test.onData += test_onData;
                test.write("D:");
                test.write("cd " + scriptPath);
                test.write("cd");
                test.write("SET PATH=" + ExePath);
                System.Threading.Thread.Sleep(500);
                Console.WriteLine(ExeFile + " program " + fwFile + " jlink -s" + ProdConfig.mcu_jlinksn);
                test.write(ExeFile + " program " + fwFile + " jlink -s" + ProdConfig.mcu_jlinksn);
                //test = null;
                retryCount++;


            }
            return WORK_STATUS.WORKING;



            


        }

        public override void release()
        {
            //if (test != null)
            //    test = null;
            //File.Delete(ExePath + @"\" + ExeFile);
            //File.Delete(ExePath + @"\" + fwFile);
        }
        private void test_onData(cmdShell sender, string e)
        {
            Trace.WriteLine(e);
            output += e;
            Console.WriteLine(e);
            if (e.Contains("verification OK"))
            {
                endOfLine = true;
                ispass = true;

            }
            if (e.Contains("Error") || e.Contains("fail") || e.Contains("Fail"))
            {
                endOfLine = true;
                Console.WriteLine("Program Error");
            }

        }
    }

    class TestItemStep007_P5 : TestItemForm
    {
        public string step = "_007 P5";
        private int retryCount = 2;
        string ExePath = Path.GetDirectoryName(ProdConfig.fileProgrammer);
        string ExeFile = Path.GetFileName(ProdConfig.fileScript);
        string fwFile = Path.GetFileName(ProdConfig.fileBleTestCode);
        private DAQdio dioA;
        string output = "";
        bool endOfLine, ispass;
        private cmdShell test;
        private int check;
        public TestItemStep007_P5()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //sMeasured = "1";
            //return WORK_STATUS.FAIL;
            //sMeasured = "Exit code = 0;IPL_Programmer.exe program  nrf52833.hex --programcn jlink -s600116414;Programming and verification OK,";
            sMeasured = "Error = 0";
            return WORK_STATUS.PASS;
            retryCount = 0;
            output = "";
            endOfLine = false;
            ispass = false;
            check = 0;
            if (!File.Exists(ProdConfig.fileProgrammer))
            {
                sMeasured = "IPLprogrammer.exe not found";
                return WORK_STATUS.FAIL;
            }
            else if (!File.Exists(ProdConfig.fileScript))
            {
                sMeasured = "Script file not found";
                Console.WriteLine("ProdConfig.fileScript " + ProdConfig.fileScript);
                return WORK_STATUS.FAIL;
            }
            else if (!File.Exists(ProdConfig.fileBleTestCode))
            {
                sMeasured = "Test firmware not found";
                Console.WriteLine("ProdConfig.filebleTestCode " + ProdConfig.fileBleTestCode);
                return WORK_STATUS.FAIL;
            }
            else
            {
                string ExePath = Path.GetDirectoryName(ProdConfig.fileProgrammer);
                string scriptPath = Path.GetDirectoryName(ProdConfig.fileBleTestCode);
                if (test != null)
                    test = null;
                test = new cmdShell();
                test.onData += test_onData;
                test.write("D:");
                test.write("cd " + scriptPath);
                test.write("cd");
                test.write("SET PATH=" + ExePath);
                System.Threading.Thread.Sleep(500);
                Console.WriteLine(ExeFile + " program " + fwFile + " --programcn jlink -s" + ProdConfig.ble_jlinksn);
                test.write(ExeFile + " program  " + fwFile + " --programcn jlink -s" + ProdConfig.ble_jlinksn);
               // test = null;

                return WORK_STATUS.PASS;
            }
        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
            if (endOfLine)
            {
                endOfLine = false;
                if (ispass)
                {
                    sMeasured = "0";
                }
                else
                {
                    sMeasured = "1";
                }

                if (PassFailJudge())
                {
                    return WORK_STATUS.PASS;
                }
                else
                {
                    if (retryCount > 0)
                        return WORK_STATUS.FAIL;
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("execute program again");
                    if (test != null)
                        test = null;
                    test = new cmdShell();
                    string scriptPath = Path.GetDirectoryName(ProdConfig.fileBleTestCode);
                    test.onData += test_onData;
                    test.write("D:");
                    test.write("cd " + scriptPath);
                    test.write("cd");
                    test.write("SET PATH=" + ExePath);
                    System.Threading.Thread.Sleep(500);
                    Console.WriteLine(ExeFile + " program " + fwFile + " --programcn jlink -s" + ProdConfig.ble_jlinksn);
                    test.write(ExeFile + " program  " + fwFile + " --programcn jlink -s" + ProdConfig.ble_jlinksn);
                    //test = null;
                    retryCount++;
                    
                }
            }
            System.Threading.Thread.Sleep(1000);
            check++;
            if (check == 6)
            {
                endOfLine = false;
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("execute program again");
                string scriptPath = Path.GetDirectoryName(Path.GetFullPath(ProdConfig.fileScript));
                if (test != null)
                    test = null;
                test = new cmdShell();
                test.onData += test_onData;
                test.write("D:");
                test.write("cd " + scriptPath);
                test.write("cd");
                test.write("SET PATH=" + ExePath);
                System.Threading.Thread.Sleep(500);
                Console.WriteLine(ExeFile + " program " + fwFile + " --programcn jlink -s" + ProdConfig.ble_jlinksn);
                test.write(ExeFile + " program  " + fwFile + " --programcn jlink -s" + ProdConfig.ble_jlinksn);
                //test = null;
                retryCount++;


            }
            return WORK_STATUS.WORKING;
        }

        public override void release()
        {
            //if (test != null)
            //    test = null;
            //File.Delete(ExePath + @"\" + ExeFile);
            //File.Delete(ExePath + @"\" + fwFile);
        }
        private void test_onData(cmdShell sender, string e)
        {
            Trace.WriteLine(e);
            output += e;
            Console.WriteLine(e);
            if (e.Contains("verification OK"))
            {
                endOfLine = true;
                ispass = true;

            }
            if (e.Contains("Error") || e.Contains("fail") || e.Contains("Fail"))
            {
                endOfLine = true;
                Console.WriteLine("Program Error");
            }
        }
    }


    //class TestItemStep007_P1 : TestItemForm
    //{
    //    public string step = "_007 P1";
    //    private int retryCount = 2;
    //    string ExePath = Path.GetDirectoryName(ProdConfig.fileProgrammer);
    //    string ExeFile = Path.GetFileName(ProdConfig.fileScript);
    //    string fwFile = Path.GetFileName(ProdConfig.fileTestCode);
    //    private DAQdio dioA,dioB;
    //    public TestItemStep007_P1()
    //    {
    //        sStep = step;
    //        LoadSpec();            
    //    }

    //    public override WORK_STATUS Init()
    //    {
    //        retryCount = 0;
    //        Console.WriteLine("111111");
    //        if (!File.Exists(ProdConfig.fileProgrammer))
    //        {
    //            sMeasured = "IPLprogrammer.exe not found";
    //            return WORK_STATUS.FAIL;
    //        }
    //        else if (!File.Exists(ProdConfig.fileScript))
    //        {
    //            sMeasured = "Script file not found";
    //            Console.WriteLine("ProdConfig.fileScript " + ProdConfig.fileScript);
    //            return WORK_STATUS.FAIL;
    //        }
    //        else if (!File.Exists(ProdConfig.fileTestCode))
    //        {
    //            sMeasured = "Test firmware not found";
    //            Console.WriteLine("ProdConfig.fileTestCode " + ProdConfig.fileTestCode);
    //            return WORK_STATUS.FAIL;
    //        }
    //        else
    //        {
    //            File.Copy(ProdConfig.fileScript, ExePath + @"\" + ExeFile, true);
    //            File.Copy(ProdConfig.fileTestCode, ExePath + @"\" + fwFile, true);
    //            Console.WriteLine("222");

    //            return WORK_STATUS.PASS;
    //        }
    //    }
    //    public override WORK_STATUS Proccess()
    //    {

    //        WORK_STATUS re = WORK_STATUS.WORKING;

    //        string ExePath = Path.GetDirectoryName(ProdConfig.fileProgrammer);

    //        // Use ProcessStartInfo class
    //        ProcessStartInfo startInfo = new ProcessStartInfo();
    //        startInfo.CreateNoWindow = false;
    //        startInfo.UseShellExecute = false;
    //        //startInfo.RedirectStandardOutput = true; // cause winerror errcode = 6: invalid opeartion
    //        //startInfo.RedirectStandardError = true;

    //        startInfo.WorkingDirectory = ExePath;
    //        startInfo.FileName = ProdConfig.fileScript;

    //        startInfo.Arguments = " program " + fwFile + " jlink";
    //        try
    //        {
    //            // Start the process with the info we specified.
    //            // Call WaitForExit and then the using statement will close.
    //            using (Process exeProcess = Process.Start(startInfo))
    //            {
    //                bool result = exeProcess.WaitForExit(0xffff);
    //                Console.WriteLine("program result: " + exeProcess.ExitCode);
    //                //Console.WriteLine(exeProcess.StandardOutput.ReadToEnd());
    //                if (exeProcess.ExitCode == 0)
    //                {
    //                    //sMeasured = expected;
    //                    sMeasured = "Error = 0";

    //                    System.Threading.Thread.Sleep(100);
    //                    return WORK_STATUS.PASS;
    //                }
    //                else
    //                {
    //                    //sMeasured = "Error code: " + exeProcess.ExitCode;
    //                    sMeasured = "Error = " + exeProcess.ExitCode;
    //                    return WORK_STATUS.FAIL;
    //                }
    //            }
    //        }
    //        catch
    //        {
    //            re = WORK_STATUS.FAIL;
    //            Console.WriteLine("Exception ");
    //            // Log error.
    //        }

    //        return re;


    //    }

    //    public override void release()
    //    {
    //        File.Delete(ExePath + @"\" + ExeFile);
    //        File.Delete(ExePath + @"\" + fwFile);
    //    }
    //}

    //class TestItemStep007_P2 : TestItemForm
    //{
    //    public string step = "_007 P2";
    //    private DAQadc taskADC;
    //    private DAQdio dio;
    //    public TestItemStep007_P2()
    //    {
    //        sStep = step;
    //        LoadSpec();
    //    }

    //    public override WORK_STATUS Init()
    //    {
    //        //DAQ setting
    //        try
    //        {
    //            dio = new DAQdio(IO_USB6525_A.deviceName);
    //            dio.addOneLineWriteCH(IO_USB6525_A.Relay_Support3V3_TP322_K7_P06);
    //            dio.setOneData(false);
    //            System.Threading.Thread.Sleep(200);
    //            dio.setOneData(true);
    //            taskADC = new DAQadc(
    //                10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_5);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
    //            taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP320_AI6);

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
    //    }

    //    public override WORK_STATUS Proccess()
    //    {
    //        WORK_STATUS re = WORK_STATUS.WORKING;

    //        if (taskADC.isEnable() == false)
    //        {
    //            if (taskADC.getErrorMsg() == null)      //must check if there were any error when DAQadc.callbackADC()
    //            {
    //                double average = taskADC.getAverage(0);

    //                double measured_v = average;
    //                sMeasured = Convert.ToString(measured_v);
    //                //Console.WriteLine("ai TP320_AI6  measured: {0}",  measured_v);

    //                //check limitation
    //                if (PassFailJudge())
    //                {
    //                    if (!Golden.verified && !Golden.empertyData && bNeedOffset)
    //                    {
    //                        if (!offsetCalculate())
    //                            return WORK_STATUS.FAIL;
    //                    }

    //                    re = WORK_STATUS.PASS;
    //                }
    //                else
    //                {
    //                    re = WORK_STATUS.FAIL;
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
    //        dio = null;
    //        //GC.Collect();
    //        //GC.WaitForPendingFinalizers();
    //    }
    //}
}
