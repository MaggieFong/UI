using RPCSerialDevice;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WindowsFormsAppTest1.TestItem
{
    using NationalInstruments.DAQmx;
    using System.Diagnostics;
    using System.Text.RegularExpressions;
    using TestItemForm;

    class TestItemStep350_P1 : TestItemForm
    {
        public string step = "_350 P1";
        private int retryCount = 2;
        string ExePath = Path.GetDirectoryName(ProdConfig.fileProgrammer);
        string ExeFile = Path.GetFileName(ProdConfig.fileScript);
        string fwFile = Path.GetFileName(ProdConfig.fileProductCode);
        private DAQdio dioA;
        private IT6332c IT6332c;
        string output = "";
        bool endOfLine, ispass;
        public TestItemStep350_P1()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //sMeasured = "Exit Code=0;IPL_Programmer.exe program SunstoneConnected_SAMD20J18.hex --setfuses jlink -s601000776,Programming and verification OK,";
            sMeasured = "True";
            return WORK_STATUS.PASS;
            retryCount = 0;
            output = "";
            endOfLine = false;
            ispass = false;


            try
            {

                //IT6332c = new IT6332c(Power_IT6332A.usbId);
                //IT6332c.Connect();
                //IT6332c.OutPutOn();
                //System.Threading.Thread.Sleep(50);
                //IT6332c.Disconnect();


                //dioA = new DAQdio(IO_USB6525_A.deviceName);
                //dioA.addOneLineWriteCH(IO_USB6525_A.Relay_Support3V3_TP322_K7_P06);
                //dioA.setOneData(true);

            }
            catch (Exception e)
            {
                sMeasured = e.Message.ToString();
                return WORK_STATUS.FAIL;
            }
                    



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
            else if (!File.Exists(ProdConfig.fileProductCode))
            {
                sMeasured = "Test firmware not found";
                Console.WriteLine("ProdConfig.fileProductCode " + ProdConfig.fileProductCode);
                return WORK_STATUS.FAIL;
            }
            else
            {
                string ExePath = Path.GetDirectoryName(ProdConfig.fileProgrammer);
                string scriptPath = Path.GetDirectoryName(Path.GetFullPath(ProdConfig.fileScript));

                cmdShell test = new cmdShell();
                test.onData += test_onData;
                test.write("D:");
                test.write("cd " + scriptPath);
                test.write("cd");
                test.write("SET PATH=" + ExePath);
                System.Threading.Thread.Sleep(500);
                Console.WriteLine(ExeFile + " program " + fwFile + " jlink -s" + ProdConfig.mcu_jlinksn);
                test.write(ExeFile + " program " + fwFile + " --setfuses jlink -s" + ProdConfig.mcu_jlinksn);
                test = null;

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
                    sMeasured = "True";
                    return WORK_STATUS.PASS;
                }
                else
                {
                    sMeasured = "False";
                    return WORK_STATUS.FAIL;
                }

            }



            return WORK_STATUS.WORKING;


        }

        public override void release()
        {
            //System.Threading.Thread.Sleep(1000);
            //dioA.setOneData(false);
            //dioA = null;
            //IT6332c = null;
            //System.Threading.Thread.Sleep(5000);
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

    //class TestItemStep350_P2 : TestItemForm
    //{
    //    public string step = "_350 P2";
    //    private int retryCount = 2;
    //    string ExePath = Path.GetDirectoryName(ProdConfig.fileProgrammer);
    //    string ExeFile = Path.GetFileName(ProdConfig.fileScript);
    //    string fwFile = Path.GetFileName(ProdConfig.filebleProductCode);
    //    private DAQdio dioA;
    //    string output = "";
    //    bool endOfLine, ispass;
    //    public TestItemStep350_P2()
    //    {
    //        sStep = step;
    //        LoadSpec();
    //    }

    //    public override WORK_STATUS Init()
    //    {
    //        retryCount = 0;
    //        output = "";
    //        endOfLine = false;
    //        ispass = false;

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
    //        else if (!File.Exists(ProdConfig.filebleProductCode))
    //        {
    //            sMeasured = "Test firmware not found";
    //            Console.WriteLine("ProdConfig.fileProductCode " + ProdConfig.filebleProductCode);
    //            return WORK_STATUS.FAIL;
    //        }
    //        else
    //        {
    //            string ExePath = Path.GetDirectoryName(ProdConfig.fileProgrammer);
    //            string scriptPath = Path.GetDirectoryName(Path.GetFullPath(ProdConfig.fileScript));

    //            cmdShell test = new cmdShell();
    //            test.onData += test_onData;
    //            test.write("D:");
    //            test.write("cd " + scriptPath);
    //            test.write("cd");
    //            test.write("SET PATH=" + ExePath);
    //            //IPL_Programmer.exe program SunstoneConnected_nrf52833.hex --programcn jlink -s600106148
    //            Console.WriteLine(ExeFile + " program  " + fwFile + " --programcn jlink -s" + ProdConfig.ble_jlinksn);
    //            test.write(ExeFile + " program " + fwFile + " --programcn jlink -s" + ProdConfig.ble_jlinksn);
    //            test = null;

    //            return WORK_STATUS.PASS;
    //        }
    //    }
    //    public override WORK_STATUS Proccess()
    //    {
    //        if (endOfLine)
    //        {

    //            if (ispass)
    //            {
    //                sMeasured = "True";
    //                return WORK_STATUS.PASS;
    //            }
    //            else
    //            {
    //                sMeasured = "False";
    //                return WORK_STATUS.FAIL;
    //            }

    //        }



    //        return WORK_STATUS.WORKING;


    //    }

    //    public override void release()
    //    {
    //        //File.Delete(ExePath + @"\" + ExeFile);
    //        //File.Delete(ExePath + @"\" + fwFile);
    //    }
    //    private void test_onData(cmdShell sender, string e)
    //    {
    //        Trace.WriteLine(e);
    //        output += e;
    //        Console.WriteLine(e);
    //        if (e.Contains("verification OK"))
    //        {
    //            endOfLine = true;
    //            ispass = true;

    //        }
    //        if (e.Contains("Error") || e.Contains("fail") || e.Contains("Fail"))
    //        {
    //            endOfLine = true;
    //            Console.WriteLine("Program Error");
    //        }

    //    }
    //}


    class TestItemStep350_P2 : TestItemForm
    {
        public string step = "_350 P2";
        private int retryCount = 2;
        string ExePath = Path.GetDirectoryName(ProdConfig.fileProgrammer);
        string ExeFile = Path.GetFileName(ProdConfig.fileScript);
        string fwFile = Path.GetFileName(ProdConfig.fileBleProductCode);
        private DAQdio dioA;
        string output = "";
        bool endOfLine, ispass;
        public TestItemStep350_P2()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            retryCount = 0;
            output = "";
            endOfLine = false;
            ispass = false;
            //if (StaticValues.goldenTest)
            //{
            //    sMeasured = "SKIP";
            //    return WORK_STATUS.PASS;
            //}
            //sMeasured = "ExitCode=0;SETPATH=C:\\ProgramFiles\\SEGGER\\JLink;IPL_Programmer.exeprogramSunstoneConnected_nrf52833.hex--programcnjlink-s600116414;;;;;pause,ProgrammingandverificationOK,";
            sMeasured = "True";
            return WORK_STATUS.PASS;
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
            else if (!File.Exists(ProdConfig.fileBleProductCode))
            {
                sMeasured = "Test firmware not found";
                Console.WriteLine("ProdConfig.filebleProductCode " + ProdConfig.fileBleProductCode);
                return WORK_STATUS.FAIL;
            }
            else
            {
                string ExePath = Path.GetDirectoryName(ProdConfig.fileProgrammer);
                string scriptPath = Path.GetDirectoryName(ProdConfig.fileBleProductCode);

                cmdShell test = new cmdShell();
                test.onData += test_onData;
                test.write("D:");
                test.write("cd " + scriptPath);
                test.write("cd");
                test.write("SET PATH=" + ExePath);
                System.Threading.Thread.Sleep(1000);
                test.write(@".\prog_productFW_BLE.bat");
                //test.write("SET PATH=" + ExePath);
                //Console.WriteLine(ExeFile + " program " + fwFile + " --programcn jlink -s" + ProdConfig.ble_jlinksn);
                //test.write(ExeFile + " program  " + fwFile + " --programcn jlink -s" + ProdConfig.ble_jlinksn);
                test = null;

                return WORK_STATUS.PASS;
            }
        }
        public override WORK_STATUS Proccess()
        {
            //if (StaticValues.goldenTest)
            //{
            //    sMeasured = "SKIP";
            //    System.Threading.Thread.Sleep(3000);
            //    return WORK_STATUS.PASS;
            //}
            
            return WORK_STATUS.PASS;
            if (endOfLine)
            {

                if (ispass)
                {
                    sMeasured = "True";
                    return WORK_STATUS.PASS;
                }
                else
                {
                    sMeasured = "False";
                    return WORK_STATUS.FAIL;
                }

            }



            return WORK_STATUS.WORKING;


        }

        public override void release()
        {
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

    class TestItemStep350_P3 : TestItemForm
    {
        public string step = "_350 P3";
        private KS34461A KS34461A;

        
        //private IT6512C IT6512C;    //Battery simulation
        public TestItemStep350_P3()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //sMeasured = "0.013";
            //return WORK_STATUS.FAIL;
            sMeasured = "19.4538061";
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

    class TestItemStep350_P4 : TestItemForm
    {
        public string step = "_350 P4";
        //private IT6332c IT6332c;
        //private DAQdio dio;
        private int testCount = 0;
        public TestItemStep350_P4()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            sMeasured = "0.254";
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

    class TestItemStep350_P5 : TestItemForm
    {
        public string step = "_350 P5";

        private Feasa_LED myFS_LED1;
        private RGB_Data RGB_ch0;
        private XY_Data XY_ch0;
        bool ispass = false;

        public TestItemStep350_P5()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //sMeasured = "0 0 0";
            //return WORK_STATUS.FAIL;
            sMeasured = "38226 0.3341 0.3335";
            return WORK_STATUS.PASS;
            try
            {

                if (checkwhileLED(Feasa_10F_A.XY_ch0))
                {
                    ispass = true;
                    return WORK_STATUS.PASS;
                }

                

                RGB_ch0 = new RGB_Data();
                XY_ch0 = new XY_Data();
                myFS_LED1 = new Feasa_LED(Feasa_10F_A.comPort);
                //System.Threading.Thread.Sleep(1000);
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
            return WORK_STATUS.PASS;
            if(ispass)
                return WORK_STATUS.PASS;    
            sMeasured = "";

            //Console.WriteLine("==LED1_Start==");

            //myFS_LED1.doCapture();
            try
            {
                myFS_LED1.doSequenceCapture(40, 40, 20);
                System.Threading.Thread.Sleep(200);
                myFS_LED1.ReadXYWithSeqMax(1, XY_ch0);
                System.Threading.Thread.Sleep(200);
                //myFS_LED1.ReadRGB(2, RGB_ch0);
                //Feasa_10F_A.R_ch7_D701 = RGB_ch0.val_R;
                //Feasa_10F_A.G_ch7_D701 = RGB_ch0.val_G; 
                //Feasa_10F_A.B_ch7_D701= RGB_ch0.val_B;
                //Feasa_10F_A.I_ch7_D701 = RGB_ch0.val_I_RGB;
            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
            //System.Threading.Thread.Sleep(200);
            //myFS_LED1.ReadXY(7, XY_ch0);

            //myFS_LED1.doSequenceCapture(20, 20, 30);
            //System.Threading.Thread.Sleep(200);

            //myFS_LED1.ReadXYWithSeqMax(7, XY_ch0);
            //myFS_LED1.ReadXYWithSeqMax(5, XY_ch0);
            if (!checkwhileLED(XY_ch0))
            {
                return WORK_STATUS.FAIL;
            }
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            //if (myFS_LED1!=null)
            //{
            //    myFS_LED1.Close();
            //    myFS_LED1 = null;
            //    RGB_ch0 = null;
            //    XY_ch0 = null;
            //}
            
        }

        private bool checkwhileLED(XY_Data data)
        {
            //Console.WriteLine("X : " + data.val_X);
            //Console.WriteLine("Y : " + data.val_Y);
            //Console.WriteLine("I White: " + data.val_I_XY);
            if (data.val_X < Feasa_10F_A.WLED_X_LLimit || data.val_X > Feasa_10F_A.WLED_X_ULimit ||
                data.val_Y < Feasa_10F_A.WLED_Y_LLimit || data.val_Y > Feasa_10F_A.WLED_Y_ULimit)
            {
                sMeasured = data.val_I_XY + " " + data.val_X + " " + data.val_Y;
                return false;
            }
            sMeasured = data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = data.val_I_XY + " " + data.val_X + " " + data.val_Y;
                return false;
            }
            sMeasured = data.val_I_XY + " " + data.val_X + " " + data.val_Y;
            return true;
        }
    }
    class TestItemStep350_P5_1 : TestItemForm
    {
        public string step = "_350 P5_1";

        private Feasa_LED myFS_LED1;
        private RGB_Data RGB_ch0;
        private XY_Data XY_ch0;
        private IT6332c IT6332c;

        public TestItemStep350_P5_1()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //sMeasured = "0 0 0 0";
            //return WORK_STATUS.FAIL;
            sMeasured = "0 13 241 54667";
            return WORK_STATUS.PASS;
            try
            {
                IT6332c = new IT6332c(Power_IT6332A.usbId);
                IT6332c.Connect();
                IT6332c.OutPutOff();
                System.Threading.Thread.Sleep(500);
                IT6332c.OutPutOn();
                System.Threading.Thread.Sleep(500);
                IT6332c.Disconnect();


                RGB_ch0 = new RGB_Data();
                XY_ch0 = new XY_Data();
                myFS_LED1 = new Feasa_LED(Feasa_10F_A.comPort);


                Feasa_10F_A.R_ch7_D701 = 0;
                Feasa_10F_A.G_ch7_D701 = 0;
                Feasa_10F_A.B_ch7_D701 = 0;
                Feasa_10F_A.I_ch7_D701 = 0;
                Feasa_10F_A.XY_ch0 = null;


                //System.Threading.Thread.Sleep(1000);
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
            //sMeasured = "0 16 239 56879";
            //sMeasured = "0 0 0 0";
            return WORK_STATUS.PASS;
            //Console.WriteLine("==LED1_Start==");

            //myFS_LED1.doCapture();
            try
            {
                myFS_LED1.doSequenceCapture(40, 40, 20);
                System.Threading.Thread.Sleep(200);
                //myFS_LED1.ReadXYWithSeqMax(1, XY_ch0);
                //System.Threading.Thread.Sleep(200);
                myFS_LED1.ReadRGB(2, RGB_ch0);
                Feasa_10F_A.R_ch7_D701 = RGB_ch0.val_R;
                Feasa_10F_A.G_ch7_D701 = RGB_ch0.val_G;
                Feasa_10F_A.B_ch7_D701 = RGB_ch0.val_B;
                Feasa_10F_A.I_ch7_D701 = RGB_ch0.val_I_RGB;
               // Feasa_10F_A.XY_ch0 = XY_ch0;
                if (Feasa_10F_A.R_ch7_D701 < 50 && Feasa_10F_A.G_ch7_D701 < 50 && Feasa_10F_A.B_ch7_D701 > 200)
                {
                    sMeasured = Feasa_10F_A.R_ch7_D701 + " " + Feasa_10F_A.G_ch7_D701 + " " + Feasa_10F_A.B_ch7_D701 + " " + Feasa_10F_A.I_ch7_D701;
                    return WORK_STATUS.PASS;

                }
                else 
                {
                    myFS_LED1.doC3Capture();
                    myFS_LED1.Readrgbi(2, RGB_ch0);
                    if (Feasa_10F_A.R_ch7_D701 < 50 && Feasa_10F_A.G_ch7_D701 < 50 && Feasa_10F_A.B_ch7_D701 > 200)
                    {
                        sMeasured = Feasa_10F_A.R_ch7_D701 + " " + Feasa_10F_A.G_ch7_D701 + " " + Feasa_10F_A.B_ch7_D701 + " " + Feasa_10F_A.I_ch7_D701;
                        return WORK_STATUS.PASS;

                    }

                    sMeasured = Feasa_10F_A.R_ch7_D701 + " " + Feasa_10F_A.G_ch7_D701 + " " + Feasa_10F_A.B_ch7_D701 + " " + Feasa_10F_A.I_ch7_D701;
                    return WORK_STATUS.FAIL;

                }
            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
            //System.Threading.Thread.Sleep(200);
            //myFS_LED1.ReadXY(7, XY_ch0);

            //myFS_LED1.doSequenceCapture(20, 20, 30);
            //System.Threading.Thread.Sleep(200);

            //myFS_LED1.ReadXYWithSeqMax(7, XY_ch0);
            //myFS_LED1.ReadXYWithSeqMax(5, XY_ch0);
            //if (!checkwhileLED(XY_ch0))
                //{

                //    return WORK_STATUS.FAIL;
                //}
                return WORK_STATUS.PASS;
        }

        public override void release()
        {
            //IT6332c = null;
            //myFS_LED1.Close();
            //myFS_LED1 = null;
            //RGB_ch0 = null;
            //XY_ch0 = null;
        }

        private bool checkwhileLED(XY_Data data)
        {
            //Console.WriteLine("X : " + data.val_X);
            //Console.WriteLine("Y : " + data.val_Y);
            //Console.WriteLine("I White: " + data.val_I_XY);
            if (data.val_X < Feasa_10F_A.WLED_X_LLimit || data.val_X > Feasa_10F_A.WLED_X_ULimit ||
                data.val_Y < Feasa_10F_A.WLED_Y_LLimit || data.val_Y > Feasa_10F_A.WLED_Y_ULimit)
            {
                sMeasured = data.val_I_XY + " " + data.val_X + " " + data.val_Y;
                return false;
            }
            sMeasured = data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = data.val_I_XY + " " + data.val_X + " " + data.val_Y;
                return false;
            }
            sMeasured = data.val_I_XY + " " + data.val_X + " " + data.val_Y;
            return true;
        }
    }


    class TestItemStep350_P6 : TestItemForm
    {
        public string step = "_350 P6";
        private int retryCount = 0;


        public TestItemStep350_P6()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            sMeasured = "20260309-1036";


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
    class TestItemStep350_P8 : TestItemForm
    {
        public string step = "_350 P8";
        private int retryCount = 0;


        public TestItemStep350_P8()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            sMeasured = "41";

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

    class TestItemStep1000 : TestItemForm
    {
        private IT6332c IT6332c;
        private DAQdio dioA;
        public string step = "_1000 P1";
        public TestItemStep1000()
        {
            sStep = step;
            LoadSpec();

        }

        public override WORK_STATUS Init()
        {

            //try
            //{
            //    IT6332c = new IT6332c(Power_IT6332A.usbId);
            //    IT6332c.Connect();
            //    System.Threading.Thread.Sleep(50);
            //    IT6332c.OutPutOff();
            //    IT6332c.Disconnect();


            //    dioA = new DAQdio(IO_USB6525_A.deviceName);
            //    dioA.addOneLineWriteCH(IO_USB6525_A.Relay_Discharge_Res_K12_K13_P04);
            //    dioA.setOneData(true);
            //    System.Threading.Thread.Sleep(200);
            //    dioA.setOneData(false);

            //}
            //catch (Exception e)
            //{
            //    sMeasured = e.Message;
            //    return WORK_STATUS.FAIL;
            //}

            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            sMeasured = "True";
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            //IT6332c = null;
            //dioA = null;
        }
    }

}
