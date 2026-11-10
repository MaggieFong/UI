using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;
    using System.Diagnostics;
    class TestItemStep007_ : TestItemForm
    {
        public string step = "_007 ";
        private int retryCount = 2;
        //private DAQdio dio;
        public TestItemStep007_()
        {
            sStep = step;
            LoadSpec();            
        }

        public override WORK_STATUS Init()
        {
            //DIO setting
            //try
            //{
            //    dio = new DAQdio(IO_USB6525_A.deviceName);
            //    dio.addOneLineWriteCH(IO_USB6525_A.TP322_K7_P06);
            //    dio.setOneData(true);
            //}
            //catch (Exception e)
            //{

            //    sMeasured = e.Message;
            //    return WORK_STATUS.FAIL;
            //}
            //if (!File.Exists(ProdConfig.fileProgrammer))
            //{
            //    sMeasured = "atprogram.exe not found";
            //    return WORK_STATUS.FAIL;
            //}
            //else if (!File.Exists(ProdConfig.fileTestCode))
            //{
            //    sMeasured = "Test firmware not found";
            //    Console.WriteLine("ProdConfig.fileTestCode " + ProdConfig.fileTestCode);
            //    return WORK_STATUS.FAIL;
            //}
            //else
                return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            
            sMeasured = "True";
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            //dio = null;
        }
    }
    class TestItemStep007_P1 : TestItemForm
    {
        public string step = "_007 P1";
        private int retryCount = 2;
        //string batFile = null;
        string ExePath = Path.GetDirectoryName(ProdConfig.fileProgrammer);
        string fileTestCode = Path.GetFileName(ProdConfig.fileTestCode);
        

        public TestItemStep007_P1()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            Console.WriteLine("Init");
            

            if (!File.Exists(ProdConfig.fileProgrammer))
            {
                sMeasured = "IPL_Programmer.exe not found";
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


            File.Copy(ProdConfig.fileProductCode, ExePath + @"\" + fileTestCode, true);

            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
               
            WORK_STATUS re = WORK_STATUS.FAIL;
            sMeasured = "Program fail";

            // Use ProcessStartInfo class
            string ExePath = Path.GetDirectoryName(ProdConfig.fileProgrammer);
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.WorkingDirectory = ExePath;
            startInfo.FileName = ProdConfig.fileProgrammer;
            
            startInfo.Arguments = " program SAMD20J18.elf atmelice";
            //startInfo.Arguments = "-t samice -i SWD -d ATSAMD20J18 -cl 10mhz chiperase program -f " + ProdConfig.fileTestCode;

            Console.WriteLine("startInfo.Arguments :" + startInfo.Arguments);

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                   
                    bool result = exeProcess.WaitForExit(0xffff);

                    Console.WriteLine("program result: " + exeProcess.ExitCode);
                    //exeProcess.WaitForExit();

                    if (exeProcess.ExitCode == 0)
                    {
                        //sMeasured = expected;
                        sMeasured = "Error = 0";
                        System.Threading.Thread.Sleep(200);
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
       
        public override void release()
        {
            File.Delete(ExePath + @"\" + fileTestCode);
        }
    }
    //class TestItemStep007_P1 : TestItemForm
    //   {
    //       public string step = "_007 P1";
    //       private int retryCount = 2;
    //       public TestItemStep007_P1()
    //       {
    //           sStep = step;
    //           LoadSpec();            
    //       }

    //       public override WORK_STATUS Init()
    //       {
    //           retryCount = 2;
    //           if (!File.Exists(ProdConfig.fileProgrammer))
    //           {
    //               sMeasured = "atprogram.exe not found";
    //               return WORK_STATUS.FAIL;
    //           }
    //           else if (!File.Exists(ProdConfig.fileTestCode))
    //           {
    //               sMeasured = "Test firmware not found";
    //               Console.WriteLine("ProdConfig.fileTestCode " + ProdConfig.fileTestCode);
    //               return WORK_STATUS.FAIL;
    //           }
    //           else
    //               return WORK_STATUS.PASS;
    //           //try
    //           //{
    //           //    Console.WriteLine("12345677");
    //           //    if (!File.Exists(ProdConfig.fileProductCode))
    //           //    {
    //           //        Console.WriteLine ( "Product firmware not found");
    //           //        Console.WriteLine("ProdConfig.fileProductCode " + ProdConfig.fileProductCode);

    //           //    }
    //           //    string ExePath = Path.GetDirectoryName(ProdConfig.fileProgrammer);
    //           //    string codeFile = Path.GetFileName(ProdConfig.fileProductCode);
    //           //    //Console.WriteLine("codefile =" + codeFile);


    //           //    //File.Copy(ProdConfig.fileProductCode, ExePath + @"\" + codeFile, true);
    //           //    File.Copy(ProdConfig.fileProductCode, Path.Combine(ExePath, codeFile), true);
    //           //    Console.WriteLine("copy done");
    //           //}
    //           //catch(IOException copyerror) {
    //           //    Console.WriteLine("ERROR "+copyerror.Message);
    //           //    sMeasured = copyerror.Message;
    //           //}
    //           //return WORK_STATUS.PASS;
    //       }
    //       public override WORK_STATUS Proccess()
    //       {
    //           WORK_STATUS re = WORK_STATUS.FAIL;
    //           sMeasured = "Program fail";

    //           // Use ProcessStartInfo class
    //           ProcessStartInfo startInfo = new ProcessStartInfo();
    //           startInfo.CreateNoWindow = false;
    //           startInfo.UseShellExecute = false;
    //           startInfo.FileName = ProdConfig.fileProgrammer;
    //           //startInNI USB-6218 "6218A"fo.WindowStyle = ProcessWindowStyle.Hidden;
    //           startInfo.Arguments = "-t atmelice -i SWD -d ATSAMD20J18 -cl 10mhz chiperase program -f " + ProdConfig.fileTestCode;
    //           //startInfo.Arguments = "-t samice -i SWD -d ATSAMD20J18 -cl 10mhz chiperase program -f " + ProdConfig.fileTestCode;

    //           //Console.WriteLine("startInfo.Arguments :" + startInfo.Arguments);

    //           try
    //           {
    //               // Start the process with the info we specified.
    //               // Call WaitForExit and then the using statement will close.
    //               using (Process exeProcess = Process.Start(startInfo))
    //               {
    //                   bool result = exeProcess.WaitForExit(0xffff);

    //                   //Console.WriteLine("program result: " + exeProcess.ExitCode);
    //                   //exeProcess.WaitForExit();

    //                   if (exeProcess.ExitCode == 0)
    //                   {
    //                       //sMeasured = expected;
    //                       sMeasured = "Error = 0";
    //                       System.Threading.Thread.Sleep(200);
    //                       re = WORK_STATUS.PASS;
    //                   }
    //                   else
    //                   {
    //                       //sMeasured = "Error code: " + exeProcess.ExitCode;
    //                       sMeasured = "Error = " + exeProcess.ExitCode;
    //                   }
    //               }
    //           }
    //           catch
    //           {
    //               // Log error.
    //           }

    //           return re;
    //       }
    //   //    public override WORK_STATUS Proccess()
    //   //    {
    //   //        WORK_STATUS re = WORK_STATUS.FAIL;
    //   //        sMeasured = "Program fail";

    //   //        Use ProcessStartInfo class
    //   //       ProcessStartInfo startInfo = new ProcessStartInfo();
    //   //    startInfo.CreateNoWindow = false;
    //   //        startInfo.UseShellExecute = false;
    //   //        startInfo.FileName = ProdConfig.fileProgrammer;
    //   //        startInNI USB-6218 "6218A"fo.WindowStyle = ProcessWindowStyle.Hidden;
    //   //        startInfo.Arguments = "-t atmelice -i SWD -d ATSAMD20J18 -cl 10mhz chiperase program -f " + ProdConfig.fileTestCode;
    //   //        startInfo.Arguments = "-t samice -i SWD -d ATSAMD20J18 -cl 10mhz chiperase program -f " + ProdConfig.fileTestCode;

    //   //        Console.WriteLine("startInfo.Arguments :" + startInfo.Arguments);

    //   //        try
    //   //        {
    //   //             Start the process with the info we specified.
    //   //             Call WaitForExit and then the using statement will close.
    //   //            using (Process exeProcess = Process.Start(startInfo))
    //   //            {
    //   //                bool result = exeProcess.WaitForExit(0xffff);

    //   //Console.WriteLine("program result: " + exeProcess.ExitCode);
    //   //                exeProcess.WaitForExit();
    //   //                sMeasured = exeProcess.ExitCode.ToString().Trim();
    //   //                if (sMeasured == "0")
    //   //                {
    //   //                    sMeasured = expected;
    //   //                    sMeasured = "Error = 0";
    //   //                    System.Threading.Thread.Sleep(200);
    //   //                    re = WORK_STATUS.PASS;
    //   //                }
    //   //                else
    //   //                {
    //   //                    retryCount--;
    //   //                    if (retryCount == 0)
    //   //                    {
    //   //                        return WORK_STATUS.FAIL;
    //   //                    }
    //   //                    else
    //   //                    {
    //   //                        return WORK_STATUS.WORKING;
    //   //                    }
    //   //                }
    //   //            }
    //   //        }
    //   //        catch
    //   //        {
    //   //             Log error.
    //   //        }

    //   //        return re;
    //   //    }
    //       public override void release()
    //       {
    //           //System.Threading.Thread.Sleep(300);

    //       }
    //   }

    class TestItemStep007_P2 : TestItemForm
    {
        public string step = "_007 P2";
        private DAQadc taskADC;
        public TestItemStep007_P2()
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
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP320_AI6);

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
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
        }
    }
}
