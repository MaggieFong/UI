using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Reflection;
using WindowsFormsAppTest1.Utility;
using WindowsFormsAppTest1.Port;
using WindowsFormsAppTest1.Extension;
using WindowsFormsAppTest1.Properties;
using Ivi.Visa;
using NationalInstruments.Visa;

using IniFileAccess;
using ShopFlowInformationSystem;
using myCRC32;


namespace WindowsFormsAppTest1
{
    using TestItemForm;
    
    public partial class PhilipsEmeraldTestTool : Form
    {
        //type define
        private enum TEST_PHASE
        {
            INIT,
            UPDATE
        };

        private class ReportProccessBarInfo
        {
            public int index;
            public TEST_PHASE phase;
            public bool result;
            public String message;

            public ReportProccessBarInfo(int i, TEST_PHASE p, bool r, String m)
            {
                index = i;
                phase = p;
                result = r;
                message = m;
            }
        };

        private const string Version = "V1.1_IMU_20260513";

        // Constant definition
        private const int VK_RETURN = 13;
        private const int COL_NUM_MEASURED = 2;
        private const int COL_NUM_STATUS = 7;

        private const int COL_CAL_CRC = 3;
        private const int COL_SFIS_CRC = 4;

        private const int COL_LIB_CAL_CRC = 2;
        private const int COL_LIB_EXP_CRC = 3;

        private const string fileConfig = @".\config.ini";

        //member var.
        private TestItems testItems;// = new TestItems();
        private int dTotalTestSteps;
        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        public bool CancelDisplayForm { private set; get; }
        private bool jigNotClosed = false;
        private bool goldenTest = false;
        public string TxtLogPath = "";
        public StreamWriter TxtLogSW;
        public void InitTxtLog()
        {//save console write message as txt file
            DateTime now = DateTime.Now;
            string TimeToday = now.ToString("yyyyMMdd");

            if (!Directory.Exists(Config.pathLog + "\\" + TimeToday))
            {
                Directory.CreateDirectory(Config.pathLog + "\\" + TimeToday);
            }
            TxtLogPath = Config.pathLog + "\\" + TimeToday;

            FileStream filestream = new FileStream(TxtLogPath + "\\out.txt", FileMode.Create);
            TxtLogSW = new StreamWriter(filestream);
            TxtLogSW.AutoFlush = true;
            Console.SetOut(TxtLogSW);
            Console.SetError(TxtLogSW);
        }
        public PhilipsEmeraldTestTool()
        {
            InitializeComponent();

            initConfig();
            InitTxtLog();
            //---------------------------------------
            if (!dllCrcList())
            {
                CancelDisplayForm = true;
            }

            if (!codeCrcList())
            {
                MessageBox.Show("Show FW list fail");
                CancelDisplayForm = true;
            }            

            gridViewTest.DoubleBuffered(true);
            gridViewFW.DoubleBuffered(true);

           

            
            // test sop initial
            testItems = new TestItems();
            dTotalTestSteps = testItems.getTestItemsCount();
            Console.WriteLine("Total Test items: " + dTotalTestSteps);
            DataGridViewRowCollection rows = gridViewTest.Rows;
            for (uint i = 0; i < dTotalTestSteps; i++)
            {
                //rows.Add(new Object[] { testItems.getTestString(i),
                //    testItems.getMeasuredString(i),
                //    testItems.getExpectedString(i), "" });

                rows.Add(new Object[] { testItems.getStepString(i),
                                    testItems.getTestString(i),
                                    "",
                                    testItems.getOffsetString(i),
                                    testItems.getLowerLimitString(i),
                                    testItems.getUpperLimitString(i),
                                    testItems.getErrorCodeString(i),
                                    "" });
            }
            labelPoccess.Text = "0 / " + Convert.ToString(dTotalTestSteps);

           

        }

        int[] baudRate = { 256000, 128000, 115200, 57600, 56000, 43000, 38400, 28800, 19200, 9600, 4800, 2400, 1200, 600, 300, 110 };
        int[] dataBits = { 8, 7, 6 };
        string[] asciiCommands = { "*IDN?", "*TST?", "*RST", "*CLS", "*ESE", "*ESE?", "*ESR?", "*OPC", "*OPC?", "*PSC", "*PSC?", "*SRE", "*SRE?", "*STB?", "*SAV", "*RCL", "*TRG" };
        string[] hexCommands;
        bool isAsciiCommand = true;
        CancellationTokenSource cts;
   
        //private DR2003 DR2003;

        private void PhilipsEmeraldTestTool_Load(object sender, EventArgs e)
        {
            
            if (StaticValues.MODE == "debug")
            {
                this.Text = "Smart Tester__" + Version;
                label5.Visible = false;
                Config.sfisEnable = false; //Disable SFIS
            }
            else if (StaticValues.MODE == "production")
            {
                this.Text = "Smart Tester_" + Version;
                label5.Visible = false;
                Config.sfisEnable = true; //Enable SFIS
            }

            //try
            //{
            //    StaticValues.sSerialPort = "COM" + Dut.comPort;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);

            //}


            try
            {
                //maggie 11-27 Debug (2)
                if (!testerInit())
                    CancelDisplayForm = true;




                //maggie 11-27
                cts = new CancellationTokenSource();
                rbtUSB.Checked = true;
                btnRefresh.PerformClick();
                btnOpen.Text = Resources.OpenString;
                cboBaudRate.DataSource = baudRate;
                cboBaudRate.SelectedIndex = 3;
                cboParity.DataSource = Enum.GetValues(typeof(SerialParity));
                cboStopBits.DataSource = Enum.GetValues(typeof(SerialStopBitsMode));
                cboStopBits.SelectedIndex = 0;
                cboDataBits.DataSource = dataBits;
                cboFlowControl.DataSource = Enum.GetValues(typeof(SerialFlowControlModes));
                //Ascii
                asciiCommands = asciiCommands.OrderBy(n => n).ToArray();
                cboCommand.DataSource = asciiCommands;
                //Hex
                hexCommands = asciiCommands.ToHexString();
                cboCommand.SelectedIndex = 4;
                //EnableControl(true);

                // Remove the remark before release
                if (CancelDisplayForm)
                    Close();

                if (!Config.debugEnable)
                {
                    ;
                    tabPageVISA.Parent = null;
                    tabPageGPIO.Parent = null;
                    tabPageLED.Parent = null;
                }

                //Disable gridView sort function
                for (int i = 0; i < gridViewTest.Columns.Count; i++)
                    gridViewTest.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                for (int i = 0; i < gridViewFW.Columns.Count; i++)
                    gridViewFW.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;


                labelPn.Text = ProdConfig.PartNumber;
                if (Config.sfisEnable != true)
                {
                    richTextSfis.BackColor = SystemColors.AppWorkspace;
                    richTextSfis.Text = "[SFIS] Disabled";
                    labelSfisInfo.BackColor = Color.Yellow;
                }
                else
                {
                    richTextSfis.BackColor = SystemColors.Window;
                    richTextSfis.Clear();
                    labelSfisInfo.BackColor = Color.Transparent;
                }

                if (Golden.verified == false)
                {
                    //labelGloden.Visible = true;
                }

                labelCounter.Text = "0 / " + Counter.maxNumber.ToString();

                textSn.MaxLength = ProdConfig.snLength;
                textFixture.Focus();
                try
                {
                    StaticValues.sSerialPort = "COM" + Dut.comPort;
                    StaticValues.sBLESerialPort = "COM" + bleDut.comPort;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textFixture_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (textFixture.Text.Length > 0 && e.KeyChar == VK_RETURN)
            {// maggie 11-26 Debug(3)
                TIniFile iniProdConfig = new TIniFile(Config.fileProdConfig);
                bool bIsValidatedFixture = Convert.ToBoolean(iniProdConfig.iniReadValue("Validated_Tester", textFixture.Text, "false"));
                if (!bIsValidatedFixture)
                {
                    MessageBox.Show(null, "\"" + textFixture.Text + "\" is NOT validated for " + ProdConfig.PartNumber, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textFixture.Clear();
                    return;
                }

                TIniFile iniCounter = new TIniFile(Config.fileCounter);
                Counter.currentCount = Convert.ToInt32(iniCounter.iniReadValue("Fixture_Count", textFixture.Text, "0"));
                labelCounter.Text = Counter.currentCount.ToString() + " / " + Counter.maxNumber.ToString();
                StaticValues.sFixtureId = textFixture.Text;

                if (Config.testerCalibration == true)
                {
                    for (uint i = 0; i < dTotalTestSteps; i++)
                    {
                        testItems.setCalibratedString(i, StaticValues.sFixtureId);
                    }
                }

                if (Config.measuredOffset != true)
                {
                    for (uint i = 0; i < dTotalTestSteps; i++)
                    {
                        testItems.setNoNeedCalibration(i);
                    }
                }   //Debug(3)
                // maggie 11-26
                textFixture.Enabled = false;
                textEmp.Enabled = true;
                textEmp.Focus();
                
            }
        }

        private void textEmp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textEmp.Text.Length > 0 && e.KeyChar == VK_RETURN)
            {
                textEmp.Enabled = false;
                textSn.Enabled = true;
                textSn.BackColor = System.Drawing.Color.Gold;
                textSn.Focus();
            }
        }

        private void textSn_KeyPress(object sender, KeyPressEventArgs e)
        {
           

            if (e.KeyChar != VK_RETURN || textSn.Text.Length == 0)
                return;

            if (Config.sfisEnable == true)
                richTextSfis.Clear();

            labelSfisResult.Text = "N/A";
            labelTestResult.Text = "N/A";
            progressBar1.Value = 0;
            goldenTest = false;
            labelPFGolden.Visible = false;
            labelLastSn.Text = textSn.Text;
            TestItemInit();
           

            // check SN length
            if (textSn.Text.Length != ProdConfig.snLength)
            {
                MessageBox.Show(null, "SN length error.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textSn.Text = "";
                textSn.Focus();
                return;
            }
            
            // check SN prefix
            if (textSn.Text.Substring(0, (textSn.Text.Length > ProdConfig.snPrefix.Length) ? ProdConfig.snPrefix.Length : textSn.Text.Length) != ProdConfig.snPrefix)
            {
                MessageBox.Show(null, "SN not valid.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textSn.Text = "";
                textSn.Focus();
                return;
            }

            // check probe count
            if (Counter.currentCount >= Counter.maxNumber)
            {
                PasswordForm passwordQueryForm = new PasswordForm();
                passwordQueryForm.ShowDialog();

                if (Counter.passwordMatch == true)
                {
                    TIniFile iniCounter = new TIniFile(Config.fileCounter);

                    Counter.currentCount = 0;
                    labelCounter.Text = "0";
                    labelCounter.Text = Counter.currentCount.ToString() + " / " + Counter.maxNumber.ToString();
                    iniCounter.iniWriteValue("Fixture_Count", textFixture.Text, Counter.currentCount.ToString());
                }
                else
                {
                    MessageBox.Show(null, "Probe counter reaches the max number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textSn.Text = "";
                    textSn.Focus();
                    return;
                }
            }
            // maggie 11-27

            //// check golden sample SN
            if (Golden.verified == false)
            {
                List<string> listFiles = new List<string>();
                listFiles = FindFilesInFolder(Path.GetDirectoryName(Config.fileProdConfig), textSn.Text + "*.GRU");
                if (listFiles.Count == 0)
                {
                    MessageBox.Show(textSn.Text + " is not a golden sample");
                    textSn.Text = "";
                    textSn.Focus();
                    return;
                }
                else // gloden name file existing
                {
                    string crcString;
                    CRC32 goldenCRC = new CRC32();
                    Config.fileGolden = null;

                    foreach (string goldenFile in listFiles)
                    {
                        FileStream fs = new FileStream(goldenFile, FileMode.Open, FileAccess.Read);
                        if (fs.Length != 0)
                        {
                            byte[] fileContent = new byte[fs.Length];
                            fs.Read(fileContent, 0, (int)fs.Length);
                            crcString = goldenCRC.GetCRC32(fileContent);
                            Console.WriteLine("gold 111111111");
                            if (Path.GetFileNameWithoutExtension(goldenFile).ToUpper() == (textSn.Text + "_" + crcString.ToUpper()))
                            {
                                Config.fileGolden = goldenFile;
                                Console.WriteLine("gold 22222");
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("gold 33333");
                            if (Path.GetFileNameWithoutExtension(goldenFile) == (textSn.Text + "_00000000"))
                            {
                                Console.WriteLine("gold 44444");
                                Golden.empertyData = true;
                                Config.fileGolden = goldenFile;
                                break;
                            }
                        }

                        fs.Close();
                        Console.WriteLine("gold 5555");
                        //fs.Dispose();
                    }

                    if (Config.fileGolden == null || Config.fileGolden == "")
                    {
                        MessageBox.Show("Golden sample data not valid");
                        textSn.Text = "";
                        textSn.Focus();
                        return;
                    }
                    else
                    {
                        labelPFGolden.Text = "Pass\nGolden";
                        labelPFGolden.ForeColor = System.Drawing.Color.Teal;
                        goldenTest = true;
                        StaticValues.goldenTest = true;
                        //labelPFGolden.Visible = true;
                        Console.WriteLine("gold 77777");
                    }
                }
            }
            else // check if golden sample test after verified
            {
                Console.WriteLine("gold 888888");
                List<string> listFiles = new List<string>();
                listFiles = FindFilesInFolder(Path.GetDirectoryName(Config.fileProdConfig), textSn.Text + "*.GRU");
                if (listFiles.Count == 0) // not a golden sample
                {
                    goldenTest = false;
                    Console.WriteLine("gold 9999");
                }
                else // a golden sample
                {
                    goldenTest = true;
                    Console.WriteLine("gold 1010101010");
                    if (Path.GetFileNameWithoutExtension(listFiles[0]).ToUpper().Contains("FAIL"))
                    {
                        labelPFGolden.Text = "Fail\nGolden";
                        labelPFGolden.ForeColor = System.Drawing.Color.DeepPink;
                        labelPFGolden.Visible = true;
                        Console.WriteLine("gold 212121", listFiles[0]);
                    }
                    else
                    {
                        labelPFGolden.Text = "Pass\nGolden";
                        labelPFGolden.ForeColor = System.Drawing.Color.Teal;
                        labelPFGolden.Visible = true;
                    }
                }
            }

            // Load golden value
            if (Golden.verified == false)
            {
                Console.WriteLine("gold 313131");
                List<string> goldenData = new List<string>();
                goldenData = ReadTextFileToList(Config.fileGolden);

                for (uint i = 0; i < dTotalTestSteps; i++)
                {
                    for (int j = 0; j < goldenData.Count; j++)
                    {
                        string[] goldenItems = goldenData[j].Split(',');
                        if (testItems.getStepString(i) == goldenItems[0])
                        {
                            testItems.getTestItem((int)i).sGoldenValue = goldenItems[2];
                            //Console.WriteLine("{0} = {1}", testItems.getStepString(i), testItems.getTestItem((int)i).sGoldenValue);
                        }
                    }
                }
            }
            // maggie 11-27
            // SFIS
            //if (Config.sfisEnable == true)
            //{
            //    string sfisResult;
            //    string DutSn = textSn.Text;

            //    if (Config.sfisSendStep1 == true && !goldenTest)
            //    {
            //        // check DUT, EMP, Fixture status
            //        sfisResult = SendSfis(DutSn, 1, null, null);
            //        if (!sfisResult.ToUpper().Contains("OK"))
            //        {
            //            MessageBox.Show(null, "[SFIS] " + sfisResult, "SFIS Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            textSn.Text = "";
            //            textSn.Focus();
            //            return;
            //        }
            //    }

            //    // check the code file match with SFIS
            //    sfisResult = SendSfis(DutSn, 7, "KP_NO=???", null);
            //    if (sfisResult.ToUpper().Contains("ERROR"))
            //    {
            //        MessageBox.Show(null, "[SFIS] " + sfisResult, "SFIS Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        textSn.Text = "";
            //        textSn.Focus();
            //        return;
            //    }
            //    else
            //    {
            //        if (!codeCompareWithSfis(sfisResult))
            //        {
            //            textSn.Text = "";
            //            textSn.Focus();
            //            return;
            //        }
            //        Console.WriteLine("codeCompareWithSfis == true");
            //    }
               
            //    // pass all check process
            //    btnStart.Enabled = true;
            //    btnStart.Focus();
            //    Console.WriteLine("btnStart.Focus()");
            //    if (ProdConfig.autoRun == true)
            //        btnStart.PerformClick();
            //}
            //else //if (Config.sfisEnable == true)
            //{
                btnStart.Enabled = true;
                btnStart.Focus();
                Console.WriteLine("run 111");
                if (ProdConfig.autoRun == true)
                {
                    btnStart.PerformClick();
                    Console.WriteLine("run 222");
                }
            //}
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Console.WriteLine("run 333");
            TIniFile iniCounter = new TIniFile(Config.fileCounter);
            Counter.currentCount += 1;
            labelCounter.Text = Counter.currentCount.ToString() + " / " + Counter.maxNumber.ToString();
            iniCounter.iniWriteValue("Fixture_Count", textFixture.Text, Counter.currentCount.ToString());

            StaticValues.sBarCode = "@"+textSn.Text;

            if (workerTestProccess.IsBusy == false)     //stop -> start
            {
                button.Text = "Stop";
                textFixture.Enabled = false;
                textEmp.Enabled = false;
                textSn.Enabled = false;
                btnStart.Enabled = false;
                workerTestProccess.RunWorkerAsync();
                Console.WriteLine("run 444");
            }
            else        //start -> stop
            {
                button.Text = "Start";
                workerTestProccess.CancelAsync();
                Console.WriteLine("run 555");
            }
        }


        private void workerTestProccess_DoWork(object sender, DoWorkEventArgs e)
        {
            int testIndex = 0;
            int testStatus = 0;
            Console.WriteLine("workerTestProccess_DoWork");
            StaticValues.setTestTimer();       //start to measure testing time
            workerTestProccess.ReportProgress(0, new ReportProccessBarInfo(0, TEST_PHASE.INIT, true, ""));
            //Console.WriteLine("dTotalTestSteps=", dTotalTestSteps);
            while (dTotalTestSteps != testIndex)
            {
                //Console.WriteLine("dTotalTestSteps=", dTotalTestSteps.ToString());
                //Console.WriteLine("testIndex=", testIndex.ToString());
                System.Threading.Thread.Sleep(1);      //

                if (testIndex > 14 && testIndex != dTotalTestSteps - 1)
                    ScrollFirstRow(testIndex - 14);

                if (workerTestProccess.CancellationPending)
                {
                    Console.WriteLine("CancellationPending");
                    e.Cancel = true;
                    return;
                }
                else
                {
                    switch (testStatus)
                    {
                        case 0:     //init
                            Console.WriteLine("{{ Step {0}: {1} }}", testItems.getTestItem(testIndex).sStep, testItems.getTestItem(testIndex).sTest);
                            //Console.WriteLine("testIndex=", testIndex.ToString());
                            StaticValues.dStepStartTime = DateTime.Now.Ticks;
                            switch (testItems.getTestItem(testIndex).Init())
                            {
                                case TestItemForm.WORK_STATUS.WORKING:
                                    break;

                                case TestItemForm.WORK_STATUS.FAIL:
                                    workerTestProccess.ReportProgress(testIndex,
                                        new ReportProccessBarInfo(testIndex, TEST_PHASE.UPDATE, true, "Fail"));
                                    testItems.getTestItem(testIndex).release();     //release memory

                                    testStatus = 0;
                                    if (ProdConfig.failContinueTest && StaticValues.MODE != "debug")
                                        testIndex += 1;
                                    else
                                        testIndex = dTotalTestSteps - 1;

                                    break;
                                    //return;     //break out test proccess

                                case TestItemForm.WORK_STATUS.PASS:
                                    testStatus = 1;
                                    workerTestProccess.ReportProgress(testIndex,
                                        new ReportProccessBarInfo(testIndex, TEST_PHASE.UPDATE, true, "Process..."));
                                    break;
                            }
                            break;

                        case 1:     //proccess
                            switch (testItems.getTestItem(testIndex).Proccess())
                            {
                                case TestItemForm.WORK_STATUS.WORKING:
                                    break;

                                case TestItemForm.WORK_STATUS.FAIL:
                                    workerTestProccess.ReportProgress(testIndex,
                                        new ReportProccessBarInfo(testIndex, TEST_PHASE.UPDATE, true, "Fail"));
                                    testItems.getTestItem(testIndex).release();     //release memory

                                    testStatus = 0;
                                    //Console.WriteLine("TestItemForm.WORK_STATUS.FAIL:" + testIndex.ToString());
                                    if ((dTotalTestSteps-1) == testIndex)
                                    {
                                        testIndex += 1;
                                        break;
                                    }
                                    if (ProdConfig.failContinueTest )
                                        testIndex += 1;
                                    else
                                        testIndex = dTotalTestSteps - 1;

                                    break;
                                    //return; 

                                case TestItemForm.WORK_STATUS.PASS:
                                    workerTestProccess.ReportProgress(testIndex,
                                        new ReportProccessBarInfo(testIndex, TEST_PHASE.UPDATE, true, "Pass"));

                                    testItems.getTestItem(testIndex).bResult = true;
                                    testItems.getTestItem(testIndex).release();     //release memory
                                    testStatus = 0;
                                    testIndex += 1;
                                    break;
                            }
                            long dEndTime = DateTime.Now.Ticks;
                            float testTime = (float)(dEndTime - StaticValues.dStepStartTime) / 10000000;
                            Console.WriteLine("Total testing time: " + testTime);
                            break;
                    }
                }
                // maggie debug
                uint jigState = 0x3f;
                //uint jigState = jigCloseCheck();
                if ((!ProdConfig.failContinueTest && jigState != 0x3f) ||
                    (ProdConfig.failContinueTest && (jigState & 0x24) == 0))
                {
                    Console.WriteLine("Jig not closed! Stop test.");
                    jigNotClosed = true;
                    workerTestProccess.CancelAsync();
                }

            }
        }

        private void workerTestProccess_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ReportProccessBarInfo info = (ReportProccessBarInfo)e.UserState;
            int index = e.ProgressPercentage;
            switch (info.phase)
            {
                case TEST_PHASE.INIT:
                    break;
                case TEST_PHASE.UPDATE:
                    progressBar1.Value = (index + 1) * 100 / dTotalTestSteps;
                    labelPoccess.Text = Convert.ToString(index + 1) + " / " + Convert.ToString(dTotalTestSteps);

                    DataGridViewRowCollection rows = gridViewTest.Rows;
                    rows[index].SetValues(new Object[] {
                            testItems.getStepString((uint)index),
                            testItems.getTestString((uint)index),
                            testItems.getMeasuredString((uint)index),
                            testItems.getOffsetString((uint)index),
                            testItems.getLowerLimitString((uint)index),
                            testItems.getUpperLimitString((uint)index),
                            testItems.getErrorCodeString((uint)index),
                            info.message});

                    break;
            }

        }

        private void workerTestProccess_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {


            }
            else
            {

                //progressBar1.Value = 100;
            }

            if (StaticValues.hwTestRPCDevice != null)
            {
                StaticValues.hwTestRPCDevice.Dispose();
                StaticValues.hwTestRPCDevice = null;
            }
            StaticValues.goldenTest = false;
            btnStart.Text = "Start";
            btnStart.Enabled = false;
            bool allResult = true;
            string errorCode = null;
            for (int i = 0; i < dTotalTestSteps; i++)
            {
                allResult &= testItems.getTestItem(i).bResult;
                if (allResult != true)
                {
                    errorCode = testItems.getTestItem(i).sErrorCode;
                    break;
                }
            }

            if (allResult == true)
            {
                labelTestResult.Text = "PASS";

                if (Golden.verified == false && Golden.empertyData == true)
                {
                    //SaveLog();
                    MessageBox.Show("Plase restart the program with proper Golden Data.");
                    Close();
                }
                else if (Golden.verified == false && Golden.empertyData == false)
                {
                    Golden.verified = true;
                    labelGloden.Visible = false;
                    textSn.BackColor = SystemColors.Window;
                }
            }
            else
            {
                labelTestResult.Text = "FAIL";
            }
            // maggie debug
            testerInit();

            // maggie debug
            SaveLog();

            if (jigNotClosed)
                MessageBox.Show("Jig not closed properly, test stop!");

            if (Config.sfisEnable == true && !jigNotClosed && !goldenTest)
            {
                if (allResult == true || (allResult == false && Config.sfisSendFail == true && errorCode != null))
                {
                    if (errorCode != "N/A")
                    {
                        string sfisResult = SendSfis(textSn.Text, 2, null, errorCode);

                        if (sfisResult == "OK")
                        {
                            if (errorCode == null)
                                textSn.Clear();

                            labelSfisResult.Text = "PASS";
                        }
                        else
                            labelSfisResult.Text = "FAIL";
                    }
                }
            }

            StaticValues.s100_VoltFlashCap = null;
            StaticValues.s100_VoltLamp = null;
            StaticValues.s100_NI_VoltFlashCap = null;
            StaticValues.s100_NI_P_VoltLamp = null;
            StaticValues.s100_NI_N_VoltLamp = null;
            StaticValues.s110_VoltFlashCap = null;
            StaticValues.s110_CPDischargCURR = null;
            StaticValues.s316_NI_Bat_VOLT = null;
            StaticValues.s316_NI_Bat_GND = null;
            StaticValues.s316_Loader_data = null;
            StaticValues.s130_NIdata = null;
            StaticValues.s21_NIdata = null;
            StaticValues.s22_LED_NIdata = null;
            jigNotClosed = false;
            textSn.Enabled = true;
            textSn.Clear();
            textSn.Focus();

        
        }

        private void DoSomethingForRadioButton(params Action[] actionOfRbt)
        {
            if (actionOfRbt.Length != 3) throw new ArgumentException();
            if (rbtRS232.Checked) actionOfRbt[0]();
            if (rbtUSB.Checked) actionOfRbt[1]();
            if (rbtGPIB.Checked) actionOfRbt[2]();
        }

        private void selectedPage(object sender, EventArgs e)
        {
                        
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CancelDisplayForm)
                testerInit();

            if (t != null && !t.IsCompleted)
            {
                MessageBox.Show(Resources.LoadingInstrumentResource);
                e.Cancel = true;
                return;
            }
            //cts.Cancel();
            try
            {
                cts.Cancel();
				portOperatorBase?.Close();
            }
            catch { }
        }

        private void combo_serial_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAtmelPath_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Test for DAQ DIO
        /// </summary>
        private DAQdio dio;
   
        /// <summary>
        /// Test for DAQ DIO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        

        Task t = null;
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string title = Text;
            t = Task.Factory.StartNew(() =>
            {
                InvokeToForm(() => { btnRefresh.Enabled = false; btnOpen.Enabled = false; Text = title + Resources.LoadingRS232; });
                string[] content1 = PortUltility.FindAddresses(PortType.RS232);
                string[] content2 = PortUltility.FindRS232Type(content1);
                List<string> list1 = new List<string>();
                List<string> list2 = new List<string>();
                for (int i = 0; i < content2.Length; i++)
                {
                    if (content2[i].Contains("LPT")) continue;
                    list1.Add(content1[i]);
                    list2.Add(content2[i]);
                }
                content1 = list1.ToArray();
                content2 = list2.ToArray();
                InvokeToForm(() => cboRS232.ShowAndDisplay(content1, content2));
                InvokeToForm(() => { Text = title + Resources.LoadingUSB; });
                content1 = PortUltility.FindAddresses(PortType.USB);
                InvokeToForm(() => cboUSB.ShowAndDisplay(content1));
                InvokeToForm(() => { Text = title + Resources.LoadingGPIB; });
                content1 = PortUltility.FindAddresses(PortType.GPIB);
                InvokeToForm(() => cboGPIB.ShowAndDisplay(content1));
                //InvokeToForm(() => { Text = title + Resources.LoadingLAN; });
                //content1 = PortUltility.FindAddresses(PortType.LAN);
                //InvokeToForm(() => cboLAN.ShowAndDisplay(content1));
                InvokeToForm(() => { btnRefresh.Enabled = true; btnOpen.Enabled = true; Text = title; });
            }).ContinueWith(x =>
            {
                if (x.IsFaulted)
                {
                    CancelDisplayForm = true;
                    InvokeToForm(() => { this.Text = Resources.RuntimeError; });
                }
            });
        }

        PortOperatorBase portOperatorBase;
        bool isWritingError = false;
        private void btnWrite_Click(object sender, EventArgs e)
        {
            isWritingError = false;
            if (string.IsNullOrEmpty(cboCommand.Text))
            {
                MessageBox.Show(Resources.CommandNotEmpty);
                return;
            }
            string content;
            if (isAsciiCommand)
            {
                content = cboCommand.Text;
            }
            else
            {
                try
                {
                    content = cboCommand.Text.ToAsciiString();
                }
                catch (Exception ex)
                {
                    isWritingError = true;
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                portOperatorBase.WriteLine(content);    //送出command
                cboCommand.AddItem(cboCommand.Text);    //將送出的command增加到List清單
            }
            catch
            {
                content = $"寫入命令\"{cboCommand.Text}\"失敗！";
            }
            DisplayToTextBox($"[Time:{stopwatch.ElapsedMilliseconds}ms] Write: {cboCommand.Text}");
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            ClearIfTextBoxOverFlow();
            string result;
            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                if (isAsciiCommand)
                {
                    result = portOperatorBase.ReadLine();
                }
                else
                {
                    result = portOperatorBase.ReadLine().ToHexString();
                }
            }
            catch (IOTimeoutException)
            {
                result = Resources.ReadTimeout;
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            DisplayToTextBox($"[Time:{stopwatch.ElapsedMilliseconds}ms] Read:  {result}");
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            btnWrite.PerformClick();
            if (!isWritingError) btnRead.PerformClick();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (btnOpen.Text == Resources.OpenString)   //Connect
            {
                if (NewPortInstance())
                {
                    try
                    {
                        portOperatorBase.Open();
                        btnOpen.Text = Resources.CloseString;
                        EnableControl(false);
                    }
                    catch { }
                }
            }
            else
            {
                try
                {
                    portOperatorBase.Close();
                }
                catch { }
                btnOpen.Text = Resources.OpenString;
                EnableControl(true);
            }
        }
         
       
        
        private bool NewPortInstance()
        {
            bool hasAddress = false;
            bool hasException = false;
            DoSomethingForRadioButton(
                () =>
                {
                    if (cboRS232.SelectedIndex == -1) return;
                    try
                    {
                        portOperatorBase = new RS232PortOperator(((Pair<string, string>)cboRS232.SelectedItem).Value.ToString(),
                                               (int)cboBaudRate.SelectedItem, (SerialParity)cboParity.SelectedItem,
                                               (SerialStopBitsMode)cboStopBits.SelectedItem, (int)cboDataBits.SelectedItem);
                        hasAddress = true;
                    }
                    catch
                    {
                        hasException = true;
                    }
                },
                () =>
                {
                    if (cboUSB.SelectedIndex == -1) return;
                    try
                    {
                        portOperatorBase = new USBPortOperator(cboUSB.SelectedItem.ToString());
                        hasAddress = true;
                    }
                    catch
                    {
                        hasException = true;
                    }
                },
                () =>
                {
                    if (cboGPIB.SelectedIndex == -1) return;
                    try
                    {
                        portOperatorBase = new GPIBPortOperator(cboGPIB.SelectedItem.ToString());
                        hasAddress = true;
                    }
                    catch
                    {
                        hasException = true;
                    }
                });
            if (!hasException) portOperatorBase.Timeout = (int)nudTimeout.Value;
            return hasAddress;
        }

        private void DisplayToTextBox(string content)
        {
            txtDisplay.Text += $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} {content}\r\n";
            txtDisplay.SelectionStart = txtDisplay.Text.Length - 1;
            txtDisplay.ScrollToCaret();
        }

        private void ClearIfTextBoxOverFlow()
        {
            if (txtDisplay.Text.Length > 20480) txtDisplay.Clear();
        }
                
        private void InvokeToForm(Action action)
        {
            try
            {
                this.Invoke(action);
            }
            catch { }
        }
           
        private void EnableControl(bool enable)
        {
            btnRefresh.Enabled = enable;
            lblCommand.Enabled = !enable;
            cboCommand.Enabled = !enable;
            btnWrite.Enabled = !enable;
            btnRead.Enabled = !enable;
            btnQuery.Enabled = !enable;
            lblOverTime.Enabled = enable;
            lblTimeout.Enabled = enable;
            nudTimeout.Enabled = enable;
        }

        private void gridViewTest_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            DataGridView dataGridView = (DataGridView)sender;
            DataGridViewRow row = dataGridView.Rows[e.RowIndex];
            if (e.ColumnIndex == COL_NUM_STATUS)
            {
                switch (row.Cells[COL_NUM_STATUS].Value.ToString())
                {
                    case "":
                        e.CellStyle.BackColor = SystemColors.Window;
                        break;

                    case "Pass":
                        e.CellStyle.BackColor = System.Drawing.Color.LimeGreen;
                        break;

                    case "Fail":
                        e.CellStyle.BackColor = System.Drawing.Color.Red;
                        break;

                    case "Process...":
                        e.CellStyle.BackColor = System.Drawing.Color.Yellow;
                        break;

                    default:
                        break;
                }
            }
        }

        private void gridViewFW_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridView dataGridView = (DataGridView)sender;
            DataGridViewRow row = dataGridView.Rows[e.RowIndex];
            if (e.ColumnIndex == COL_CAL_CRC)
            {
                if (row.Cells[COL_SFIS_CRC].Value.ToString() == "")
                {
                    e.CellStyle.BackColor = SystemColors.Window;
                }
                else
                {
                    if (row.Cells[COL_SFIS_CRC].Value.ToString() == row.Cells[COL_CAL_CRC].Value.ToString())
                        e.CellStyle.BackColor = System.Drawing.Color.LimeGreen;
                    else
                        e.CellStyle.BackColor = System.Drawing.Color.Red;
                }
            }
        }

        private void initConfig()
        {

            //Read ini file
            try
            {
                TIniFile iniConfig = new TIniFile(fileConfig);
                Config.fileProdConfig = iniConfig.iniReadValue("Path", "Product Setting", null) + "ProdConfig.ini";
                Config.fileSpec = iniConfig.iniReadValue("Path", "Product Setting", null) + "Spec.ini";
                Config.pathLog = iniConfig.iniReadValue("Path", "Log", null);
                Config.fileCounter = iniConfig.iniReadValue("Path", "Counter", null) + "Counter.ini";
                Config.fileCalibration = iniConfig.iniReadValue("Path", "Product Setting", null) + "Calibration.ini";

                Config.sfisIp = iniConfig.iniReadValue("SFIS", "Ip", "");
                Config.sfisPort = Convert.ToInt32(iniConfig.iniReadValue("SFIS", "Port", "1024"));
                Config.sfisLine = iniConfig.iniReadValue("SFIS", "Line", null);
                Config.sfisEnable = Convert.ToBoolean(iniConfig.iniReadValue("SFIS", "Enable", "true"));
                Config.sfisSendFail = Convert.ToBoolean(iniConfig.iniReadValue("SFIS", "SendFail", "true"));
                Config.sfisSendStep1 = Convert.ToBoolean(iniConfig.iniReadValue("SFIS", "SendStep1", "true"));

                Config.debugEnable = Convert.ToBoolean(iniConfig.iniReadValue("Debug Tool", "Enable", "false"));

                Config.testerCalibration = Convert.ToBoolean(iniConfig.iniReadValue("Tester Deviation", "Calibration", "true"));
                Config.measuredOffset = Convert.ToBoolean(iniConfig.iniReadValue("Tester Deviation", "Offset", "false"));

                // Equipmeent Interface
                Power_IT6332A.usbId = iniConfig.iniReadValue("IT6332A/ITECH", "USB_ID", null);
                DMM_IT6332A.usbId = iniConfig.iniReadValue("34461A/DMM", "USB_ID", null);
                DCLoad_8601.usbId = iniConfig.iniReadValue("8601/B+K", "USB_ID", null);
                Power_DR2002MO.comPort = iniConfig.iniReadValue("DR2002-MO/B+K", "COM_Name", null);
                Power_DR2002MO.baudRate = Convert.ToInt32(iniConfig.iniReadValue("DR2002-MO/B+K", "Baudrate", null));
                Power_IT6512C.usbId = iniConfig.iniReadValue("IT6512C/ITECH", "USB_ID", null);
                Power_LPS505N.comPort = iniConfig.iniReadValue("LPS505N/MOTECH", "COM_Name", null);
                Power_LPS505N.baudRate = Convert.ToInt32(iniConfig.iniReadValue("LPS505N/MOTECH", "Baudrate", null));


                DAQ_USB6218_A.deviceName = iniConfig.iniReadValue("USB-6218_(A)/NI", "Device_Name", null);
                //DAQ_USB6218_B.deviceName = iniConfig.iniReadValue("USB-6218_(B)/NI", "Device_Name", null);
                IO_USB6525_A.deviceName = iniConfig.iniReadValue("USB-6525_(A)/NI", "Device_Name", null);
                IO_USB6525_B.deviceName = iniConfig.iniReadValue("USB-6525_(B)/NI", "Device_Name", null);
                //IO_USB6525_C.deviceName = iniConfig.iniReadValue("USB-6525_(C)/NI", "Device_Name", null);

                Feasa_10F_A.comPort = Convert.ToInt32(iniConfig.iniReadValue("10-F_(A)/Feasa", "COM_Number", null));
                Feasa_10F_A.baudRate = Convert.ToInt32(iniConfig.iniReadValue("10-F_(A)/Feasa", "Baudrate", null));
                Feasa_10F_B.comPort = Convert.ToInt32(iniConfig.iniReadValue("10-F_(B)/Feasa", "COM_Number", null));
                Feasa_10F_B.baudRate = Convert.ToInt32(iniConfig.iniReadValue("10-F_(B)/Feasa", "Baudrate", null));
                Dut.comPort = iniConfig.iniReadValue("DUT/USI", "COM_Number", null);
                Dut.baudRate = Convert.ToInt32(iniConfig.iniReadValue("DUT/USI", "Baudrate", null));
                bleDut.comPort = iniConfig.iniReadValue("BLE/USI", "COM_Number", null);
                bleDut.baudRate = Convert.ToInt32(iniConfig.iniReadValue("BLE/USI", "Baudrate", null));

                //Console.WriteLine("DCLoad_8601: {0}\nPower_DR2002MO: {1}, {2}\nPower_IT6512C: {3}", DCLoad_8601.usbId, Power_DR2002MO.comPort, Power_DR2002MO.baudRate, Power_IT6512C.usbId);
                //Console.WriteLine("DAQ_USB6218_A: {0}\nIO_USB6525_A: {1}\nIO_USB6525_B: {2}", DAQ_USB6218_A.deviceName, IO_USB6525_A.deviceName, IO_USB6525_B.deviceName);
                //Console.WriteLine("Feasa_10F_A: {0}, {1}\nFeasa_10F_B: {2}, {3}", Feasa_10F_A.comPort, Feasa_10F_A.baudRate, Feasa_10F_B.comPort, Feasa_10F_B.baudRate);
                //Console.WriteLine("DUT: {0}, {1}", Dut.comPort, Dut.baudRate);

                string codePath = iniConfig.iniReadValue("Path", "Product Setting", null) + @"Code\";
                string dllsPath = iniConfig.iniReadValue("Path", "Product Setting", null) + @"DLLs\";

                TIniFile iniProdConfig = new TIniFile(Config.fileProdConfig);
                ProdConfig.PartNumber = iniProdConfig.iniReadValue("Part_Number", "Value", "");
                ProdConfig.snPrefix = iniProdConfig.iniReadValue("SN", "Prefix", null);
                ProdConfig.snLength = Convert.ToInt32(iniProdConfig.iniReadValue("SN", "Length", null));
                ProdConfig.fileProgrammer = iniProdConfig.iniReadValue("Atmel_ICE", "Programmer", null);
                ProdConfig.fileScript = codePath + iniProdConfig.iniReadValue("Atmel_ICE", "ScriptFile", null);
                ProdConfig.fileTestCode = codePath + iniProdConfig.iniReadValue("Code_File", "Test_Code", null);
                ProdConfig.fileProductCode = codePath + iniProdConfig.iniReadValue("Code_File", "Product_Code", null);
                ProdConfig.fileBleTestCode = codePath + iniProdConfig.iniReadValue("Code_File", "BLE_Test_Code", null);
                ProdConfig.fileBleProductCode = codePath + iniProdConfig.iniReadValue("Code_File", "BLE_Product_Code", null);
                ProdConfig.fileSpiCode = codePath + iniProdConfig.iniReadValue("Code_File", "Flash_Code", null);
                ProdConfig.sfisnameScript = iniProdConfig.iniReadValue("Atmel_ICE", "ScriptFile", 1, null);
                ProdConfig.sfisnameTestCode = iniProdConfig.iniReadValue("Code_File", "Test_Code", 1, null);
                ProdConfig.sfisnameProductCode = iniProdConfig.iniReadValue("Code_File", "Product_Code", 1, null);
                ProdConfig.sfisnameBleTestCode = iniProdConfig.iniReadValue("Code_File", "BLE_Test_Code", 1, null);
                ProdConfig.sfisnameBleProductCode = iniProdConfig.iniReadValue("Code_File", "BLE_Product_Code", 1, null);
                ProdConfig.sfisnameSpiCode = iniProdConfig.iniReadValue("Code_File", "Flash_Code", 1, null);

                ProdConfig.mcu_jlinksn = iniProdConfig.iniReadValue("SEGGER", "MCU_JLink_SN", "600106169");
                ProdConfig.ble_jlinksn = iniProdConfig.iniReadValue("SEGGER", "BLE_JLink_SN", "600106169");
                ProdConfig.flash_jlinksn = iniProdConfig.iniReadValue("SEGGER", "FLASH_JLink_SN", "600106169");
                // maggie test IPL_programmer.exe
                //string fileProgrammer = Path.GetFileName(ProdConfig.fileProgrammer);
                //string ExePath = Path.GetDirectoryName(ProdConfig.fileProgrammer);
                //File.Copy(ProdConfig.fileScript, ExePath + @"\" + fileProgrammer, true);
                ProdConfig.autoRun = Convert.ToBoolean(iniProdConfig.iniReadValue("Auto_Run", "Enable", "true"));
                ProdConfig.savePassRawData = Convert.ToBoolean(iniProdConfig.iniReadValue("Save_Raw_Data", "Pass", "false"));
                ProdConfig.saveFailRawData = Convert.ToBoolean(iniProdConfig.iniReadValue("Save_Raw_Data", "Fail", "false"));
                ProdConfig.failContinueTest = Convert.ToBoolean(iniProdConfig.iniReadValue("Fail_Continue_Test", "Enable", "false"));

                ControlledDLLs.DLL1 = new Code();
                ControlledDLLs.DLL1.name = dllsPath + iniProdConfig.iniReadValue("Controlled_DLLs", "DLL1_Name", null);
                ControlledDLLs.DLL1.crc32 = iniProdConfig.iniReadValue("Controlled_DLLs", "DLL1_CRC", null);
                ControlledDLLs.DLL2 = new Code();
                ControlledDLLs.DLL2.name = dllsPath + iniProdConfig.iniReadValue("Controlled_DLLs", "DLL2_Name", null);
                ControlledDLLs.DLL2.crc32 = iniProdConfig.iniReadValue("Controlled_DLLs", "DLL2_CRC", null);
                ControlledDLLs.DLL3 = new Code();
                ControlledDLLs.DLL3.name = dllsPath + iniProdConfig.iniReadValue("Controlled_DLLs", "DLL3_Name", null);
                ControlledDLLs.DLL3.crc32 = iniProdConfig.iniReadValue("Controlled_DLLs", "DLL3_CRC", null);
                ControlledDLLs.DLL4 = new Code();
                ControlledDLLs.DLL4.name = dllsPath + iniProdConfig.iniReadValue("Controlled_DLLs", "DLL4_Name", null);
                ControlledDLLs.DLL4.crc32 = iniProdConfig.iniReadValue("Controlled_DLLs", "DLL4_CRC", null);
                ControlledDLLs.DLL5 = new Code();
                ControlledDLLs.DLL5.name = dllsPath + iniProdConfig.iniReadValue("Controlled_DLLs", "DLL5_Name", null);
                ControlledDLLs.DLL5.crc32 = iniProdConfig.iniReadValue("Controlled_DLLs", "DLL5_CRC", null);
                ControlledDLLs.DLL6 = new Code();
                ControlledDLLs.DLL6.name = dllsPath + iniProdConfig.iniReadValue("Controlled_DLLs", "DLL6_Name", null);
                ControlledDLLs.DLL6.crc32 = iniProdConfig.iniReadValue("Controlled_DLLs", "DLL6_CRC", null);

                // Countere
                TIniFile iniCounter = new TIniFile(Config.fileCounter);
                Counter.maxNumber = Convert.ToInt32(iniCounter.iniReadValue("Counter", "Max_Number", "5000"));
                Counter.resetPassword = iniCounter.iniReadValue("Counter", "Reset_Password", "usikste");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //Console.WriteLine("{0}", Config.fileSpec);
        }

        private string SendSfis(string sn, int step, string queryString, string errorCode)
        {
            string msgFromSfis = "";
            string msgToSfis = "";
            string szSfisStep = step.ToString();
            TSfis sfis = new TSfis();

            switch (step)
            {
                case 1:
                    msgToSfis = textFixture.Text + "," + sn + "," + szSfisStep + "," + textEmp.Text + "," + Config.sfisLine + ",,OK";
                    msgFromSfis = sfis.Send(Config.sfisIp, Config.sfisPort, msgToSfis);
                    break;

                case 2:
                    if (errorCode == null)
                    {
                        msgToSfis = textFixture.Text + "," + sn + "," + szSfisStep + "," + textEmp.Text + "," + Config.sfisLine + ",,OK";
                    }
                    else
                    {
                        msgToSfis = textFixture.Text + "," + sn + "," + szSfisStep + "," + textEmp.Text + "," + Config.sfisLine + ",,FAIL,," + errorCode;
                    }
                    msgFromSfis = sfis.Send(Config.sfisIp, Config.sfisPort, msgToSfis);
                    break;

                case 7:
                    msgToSfis = textFixture.Text + "," + sn + "," + szSfisStep + "," + textEmp.Text + "," + Config.sfisLine + ",,OK," + queryString;
                    msgFromSfis = sfis.Send(Config.sfisIp, Config.sfisPort, msgToSfis);
                    break;

                default:
                    break;
            }

            //MessageBox.Show(msgToSfis + "\n" + msgFromSfis);
            richTextSfis.AppendText("<<=     | " + msgToSfis + "\n");
            richTextSfis.AppendText("    =>> | " + msgFromSfis + "\n");

            string retString = "";
            if (step == 1 || step == 2)
            {
                if (msgFromSfis.ToUpper().Contains("OK"))
                {
                    retString = "OK";
                }
                else
                {
                    retString = msgFromSfis;
                }
            }
            else if (step == 7)
            {
                if (msgFromSfis.ToUpper().Contains("OK"))
                {
                    string[] msgSplit = msgFromSfis.Split('=');
                    retString = msgSplit[msgSplit.Length - 1];
                }
                else
                {
                    retString = "ERROR: " + msgFromSfis;
                }
            }
            return retString;
        }

        private void labelTestResult_TextChanged(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (label.Text == "PASS")
            {
                label.BackColor = Color.LimeGreen;
                label.Visible = true;
                labelLastSn.Visible = true;
            }
            else if (label.Text == "FAIL")
            {
                label.BackColor = Color.Red;
                label.Visible = true;
                labelLastSn.Visible = true;
            }
            else
            {
                label.Visible = false;
                labelLastSn.Visible = false;
            }

            label.Refresh();
        }

        private void labelResult_TextChanged(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (label.Text == "PASS")
            {
                label.BackColor = Color.LimeGreen;
                label.Visible = true;
            }
            else if (label.Text == "FAIL")
            {
                label.BackColor = Color.Red;
                label.Visible = true;
            }
            else
            {
                label.Visible = false;
            }

            label.Refresh();
        }

        private void labelCounter_TextChanged(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (Counter.currentCount >= Counter.maxNumber)
                label.ForeColor = Color.Red;
            else
                label.ForeColor = Color.Blue;

            label.Refresh();
        }

        private void btn_DR2003_OnOff_Click(object sender, EventArgs e)
        {
            IT6332c IT6332c;
            IT6332c = new IT6332c(Power_IT6332A.usbId);
            IT6332c.Connect();
            System.Threading.Thread.Sleep(50);


            if (btn_DR2003_OnOff.Text == Resources.On2003String)   //Connect
            {
                btn_DR2003_OnOff.Text = Resources.Off2003String;
                //content = $"INP 1";
                IT6332c.OutPutOn();
                //portOperatorBase.WriteLine(content);    //送出command
            }
            else
            {
                btn_DR2003_OnOff.Text = Resources.On2003String;
                //content = $"INP 0";
                IT6332c.OutPutOff();
                //portOperatorBase.WriteLine(content);    //送出command
            }
            //BK8601.Write(content);
            IT6332c.Disconnect();

        }

        private void TestItemInit()
        {
            for (int i = 0; i < dTotalTestSteps; i++)
            {
                gridViewTest.Rows[i].Cells[COL_NUM_MEASURED].Value = "";
                gridViewTest.Rows[i].Cells[COL_NUM_STATUS].Value = "";

                testItems.getTestItem(i).sMeasured = null;
                testItems.getTestItem(i).bResult = false;

                if (Golden.verified == false)
                    testItems.getTestItem(i).sGoldenValue = "N/A";
            }

            ScrollFirstRow(0);

            for (int i = 0; i < 3; i++)
            {
                gridViewFW.Rows[i].Cells[COL_SFIS_CRC].Value = "";
            }
            gridViewFW.Refresh();
        }

        //private DAQdio dio;
        
       
       
        

        private void btn_initgpio_click(object sender, EventArgs e)
        {
            try
            {
                if (StaticValues.hwTestRPCDevice == null)
                {
                    StaticValues.hwTestRPCDevice = new HardwareTestRPCSerialDevice.Device(StaticValues.sSerialPort,115200);
                }

                configurePin();
                //set outputs
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_FC_Discharge, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UI_LED_BLE_R, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_SpareP14, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UI_LED_BLE_G, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_MCU_Reset_BLE, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_12V_Ena, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_SpareP38, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_STS_IR, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Trigger_Lamp, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_STS_Red, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_NC_27, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_FC_Charge, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_BLE_Awake, 0);
                //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_ON, 1);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_ON, 0);//## //D241652 requirement mentioned
                System.Threading.Thread.Sleep(1);

                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Awake, 1);// keep board awake
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Attachment_EN, 1); //11-12 uC_Attachment_EN :0 enable 3V3 switched

                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Cooling, 0);//fan off 


                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedError, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedRTF, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UIstandby, 1);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet1, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet2, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet3, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet4, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet5, 0);


                if (StaticValues.hwTestRPCDevice != null)
                {
                    StaticValues.hwTestRPCDevice.Dispose();
                    StaticValues.hwTestRPCDevice = null;
                }

                radio_CPDischarger_L.Checked = true;
                radio_CPChargeFlashCap_L.Checked = true;
                radio_LampPWM_L.Checked = true;
                radio_CONV_OFF_H.Checked = true;
                radio_Bat_En_Battery_L.Checked = true;
                radio_Bat_En_Adapter_H.Checked = true;
                radio_Bat_En_Charging_L.Checked = true;
                radio_Awake_H.Checked = true;
                radio_Attachement_EN_L.Checked = true;
                radio_Cooling_L.Checked = true;
                radio_SCAred_L.Checked = true;
                radio_SCAir_L.Checked = true;
                radio_LED_driver_resetN_H.Checked = true;

                //Console.WriteLine("Init DUT output pin Done");
                MessageBox.Show("Init DUT output pin Done");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Init DUT GPIO failed: " + ex.Message);
            }
        }

        private void setOutputPullNone(byte io_idx)
        {
            StaticValues.hwTestRPCDevice.ConfigurePin(io_idx,
                    HardwareTestRPCSerialDevice.PinTypeValue.OutputPin,
                    HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);
        }

        private void setI2c_sercom4(byte sda, byte scl)
        {
            StaticValues.hwTestRPCDevice.ConfigureMasterI2C(sda,
                scl,
                HardwareTestRPCSerialDevice.SercomNumValue.Sercom4);
        }

        private void btn_pullL_click(object sender, EventArgs e)
        {
            try
            {
                if (StaticValues.hwTestRPCDevice == null)
                {
                    StaticValues.hwTestRPCDevice = new HardwareTestRPCSerialDevice.Device(StaticValues.sSerialPort,115200);
                }
            
                Byte pinN = Convert.ToByte(textB_enterpinN.Text);
                StaticValues.hwTestRPCDevice.SetPin(pinN, 0);
                Console.WriteLine("PULL LOW pin " + pinN);


                if (StaticValues.hwTestRPCDevice != null)
                {
                    StaticValues.hwTestRPCDevice.Dispose();
                    StaticValues.hwTestRPCDevice = null;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("set pin failed: " + ex.Message);
                MessageBox.Show("Set DUT pin failed: " + ex.Message);
            }
        }

        private void btn_pullH_click(object sender, EventArgs e)
        {
            try
            { 
                if (StaticValues.hwTestRPCDevice == null)
                {
                    StaticValues.hwTestRPCDevice = new HardwareTestRPCSerialDevice.Device(StaticValues.sSerialPort,115200);
                }

                Byte pinN = Convert.ToByte(textB_enterpinN.Text);
                StaticValues.hwTestRPCDevice.SetPin(pinN, 1);
                Console.WriteLine("PULL HIIGH pin " + pinN);

                if (StaticValues.hwTestRPCDevice != null)
                {
                    StaticValues.hwTestRPCDevice.Dispose();
                    StaticValues.hwTestRPCDevice = null;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("set pin failed: " + ex.Message);
                MessageBox.Show("Set DUT pin failed: " + ex.Message);
            }
        }

        private bool codeCompareWithSfis(string sfisMessage)
        {
            bool ret = true;
            string[] codeList = sfisMessage.Trim().ToUpper().Split(';');
            Code sfisTestFW = new Code();
            Code sfisProdFW = new Code();
            Code sfisScript = new Code();
            Code sfisBleTestFW = new Code();
            Code sfisBleProdFW = new Code();
            Code sfisSpiCode = new Code();
            string[] NameCrc = null;

            NameCrc = codeList[0].Trim().Split(':');
            sfisSpiCode.name = NameCrc[0].Trim();
            sfisSpiCode.crc32 = NameCrc[1].Trim();
            Console.WriteLine("codeCompareWithSfis 1");
            NameCrc = null;
            NameCrc = codeList[1].Trim().Split(':');
            sfisBleTestFW.name = NameCrc[0].Trim();
            sfisBleTestFW.crc32 = NameCrc[1].Trim();
            Console.WriteLine("codeCompareWithSfis 2");
            NameCrc = null;
            NameCrc = codeList[2].Trim().Split(':');
            sfisBleProdFW.name = NameCrc[0].Trim();
            sfisBleProdFW.crc32 = NameCrc[1].Trim();
            Console.WriteLine("codeCompareWithSfis 3");
            NameCrc = null;
            NameCrc = codeList[3].Trim().Split(':');
            sfisTestFW.name = NameCrc[0].Trim();
            sfisTestFW.crc32 = NameCrc[1].Trim();

            NameCrc = null;
            NameCrc = codeList[4].Trim().Split(':');
            sfisProdFW.name = NameCrc[0].Trim();
            sfisProdFW.crc32 = NameCrc[1].Trim();
            Console.WriteLine("codeCompareWithSfis 4");
            NameCrc = null;
            NameCrc = codeList[5].Trim().Split(':');
            sfisScript.name = NameCrc[0].Trim();
            sfisScript.crc32 = NameCrc[1].Trim();
            Console.WriteLine("codeCompareWithSfis 4-1");
            Console.WriteLine("sfis"+sfisSpiCode.name.ToString());
            Console.WriteLine("33"+Programming.externalFlash.name.ToUpper().ToString());

            // Check External Flash code name and crc with SFIS response
            if (sfisSpiCode.name != Programming.externalFlash.name.ToUpper())
            {
                MessageBox.Show(null, "External Flash code name not correct.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ret = false;
            }
            else
            {
                gridViewFW.Rows[0].Cells[4].Value =  sfisSpiCode.crc32;

                if (sfisSpiCode.crc32 != Programming.externalFlash.crc32.ToUpper())
                {
                    MessageBox.Show(null, "External Flash code CRC not correct.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ret = false;
                }
            }
            Console.WriteLine("codeCompareWithSfis 5");
            // Check BLE Test FW name and crc with SFIS response
            if (sfisBleTestFW.name != Programming.bleDtmFW.name.ToUpper())
            {
                MessageBox.Show(null, "BLE Test FW name not correct.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ret = false;
            }
            else
            {
                gridViewFW.Rows[1].Cells[4].Value = sfisBleTestFW.crc32;

                if (sfisBleTestFW.crc32 != Programming.bleDtmFW.crc32.ToUpper())
                {
                    MessageBox.Show(null, "BLE Test FW CRC not correct.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ret = false;
                }
            }
            Console.WriteLine("codeCompareWithSfis 6");
            // Check BLE Product FW name and crc with SFIS response
            if (sfisBleProdFW.name != Programming.bleProductFW.name.ToUpper())
            {
                MessageBox.Show(null, "BLE Product FW name not correct.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ret = false;
            }
            else
            {
                gridViewFW.Rows[2].Cells[4].Value = sfisBleProdFW.crc32;

                if (sfisBleProdFW.crc32 != Programming.bleProductFW.crc32.ToUpper())
                {
                    MessageBox.Show(null, "BLE Product FW CRC not correct.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ret = false;
                }
            }
            Console.WriteLine("codeCompareWithSfis 7");
            // Check HW test FW name and crc with SFIS response
            if (sfisTestFW.name != Programming.testFW.name.ToUpper())
            {
                MessageBox.Show(null, "HW test FW name not correct.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ret = false;
            }
            else
            {
                gridViewFW.Rows[3].Cells[4].Value = sfisTestFW.crc32;

                if (sfisTestFW.crc32 != Programming.testFW.crc32.ToUpper())
                {
                    MessageBox.Show(null, "HW test FW CRC not correct.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ret = false;
                }
            }
            Console.WriteLine("codeCompareWithSfis 8");
            // Check product FW name and crc with SFIS response
            if (sfisProdFW.name != Programming.productFW.name.ToUpper())
            {
                MessageBox.Show(null, "Product code name not correct.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ret = false;
            }
            else
            {
                gridViewFW.Rows[4].Cells[4].Value = sfisProdFW.crc32;

                if (sfisProdFW.crc32 != Programming.productFW.crc32.ToUpper())
                {
                    MessageBox.Show(null, "Product code CRC not correct.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ret = false;
                }
            }
            Console.WriteLine("codeCompareWithSfis 9");
            // Check script file name and crc with SFIS response
            if (sfisScript.name != Programming.Script.name.ToUpper())
            {
                MessageBox.Show(null, "Script file name not correct.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ret = false;
            }
            else
            {
                gridViewFW.Rows[5].Cells[4].Value = sfisScript.crc32;

                if (sfisScript.crc32 != Programming.Script.crc32.ToUpper())
                {
                    MessageBox.Show(null, "Script file CRC not correct.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ret = false;
                }
            }
            Console.WriteLine("codeCompareWithSfis 10");
            gridViewFW.Refresh();

            return ret;
        }

        private void SaveLog()
        {
           
            string savepath, logFileAll, logFileSn;
            string szLogData = null, szLogTitle = null;
            string timeYM, timeYMD, timeD, timeHMS, timeHMS_file;
            
            timeYMD = DateTime.Now.ToString("MM/dd/yyyy");
            timeYM = DateTime.Now.ToString("yyyyMM");
            timeD = DateTime.Now.ToString("dd");
            timeHMS = DateTime.Now.ToString("HH:mm:ss");
            timeHMS_file = DateTime.Now.ToString("HHmmss");

            StreamWriter writer;
            savepath = Config.pathLog + "\\" + labelPn.Text + "\\" + timeYM + "\\" + timeD;
            logFileSn = savepath + "\\" + textSn.Text + "_" + timeHMS_file + ".csv";
            logFileAll = Config.pathLog + "\\" + labelPn.Text + "\\" + timeYM + "\\" + labelPn.Text + "_" + timeYM + ".csv";

            Directory.CreateDirectory(savepath);

            try
            {
                // Save overall log
                if (File.Exists(logFileAll))
                {
                    writer = File.AppendText(logFileAll);
                }
                else
                {
                    writer = File.AppendText(logFileAll);

                    szLogTitle = "USI_SN,Date_Time,Result,Emp_ID,Fixture_ID,Test_Program,";
                    for (int i = 0; i < dTotalTestSteps; i++)
                        szLogTitle += "Step" + testItems.getTestItem(i).sStep + ",";

                    writer.WriteLine(szLogTitle);
                    szLogTitle = null;
                }

                // Modify for Emerald
                szLogData += textSn.Text + ",";
                szLogData += timeYMD + " " + timeHMS + ",";
                szLogData += labelTestResult.Text + ",";
                szLogData += textEmp.Text + ",";
                szLogData += textFixture.Text + ",";
                szLogData += this.Text + ",";
                for (int i = 0; i < dTotalTestSteps; i++)
                    szLogData += testItems.getTestItem(i).sMeasured + ",";

                writer.WriteLine(szLogData);
                writer.Close();
                writer = null;
                szLogData = null;

                // Save individual log
                writer = File.AppendText(logFileSn);
                szLogData += "Test Program: " + this.Text + "\r\n";
                szLogData += "Fixure ID: " + textFixture.Text + "\r\n";
                szLogData += "EMP ID: " + textEmp.Text + "\r\n";
                szLogData += "Calibration: " + Config.testerCalibration.ToString() + "\r\n";
                szLogData += "Offset: " + Config.measuredOffset.ToString() + "\r\n";
                szLogData += "Part Number: " + ProdConfig.PartNumber + "\r\n";
                szLogData += "Serial Number: " + textSn.Text + "\r\n";
                szLogData += "Test Time: " + timeYMD + " " + timeHMS + "\r\n";
                szLogData += "===============================================================\r\n";
                writer.WriteLine(szLogData);
                szLogData = null;

                foreach (DataGridViewColumn column in gridViewTest.Columns)
                {
                    szLogTitle += column.HeaderText + ",";
                }
                writer.WriteLine(szLogTitle);

                for (int i = 0; i < gridViewTest.Rows.Count; i++)
                {
                    DataGridViewRow row = gridViewTest.Rows[i];
                    for (int j = 0; j < row.Cells.Count; j++)
                    {
                        szLogData += row.Cells[j].Value + ",";
                    }

                    writer.WriteLine(szLogData);
                    szLogData = null;
                }


                // save NI data 
                

                //TxtLogSW.Close();
                //DateTime now = DateTime.Now;
                //string TimeToday = now.ToString("yyyyMMdd");
                //string TimeNow = now.ToString("HHmmss");
                //File.Move(TxtLogPath + "\\out.txt", TxtLogPath + "\\[" + labelTestResult.Text + "]" + textSn.Text + "_" + TimeToday + "-" + TimeNow + ".txt");
                ////File.WriteAllText(TxtLogPath + "\\out.txt", string.Empty);
                //InitTxtLog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void uC_Output_StateChanged(object sender, EventArgs e)
        {
            byte uCPin;
            ushort value;

            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                Panel panel = (Panel)radioButton.Parent;

                uCPin = Convert.ToByte(panel.Tag);
                if (radioButton.Tag.ToString() == "1")
                    value = 1;
                else
                    value = 0;

                try
                {
                    if (StaticValues.hwTestRPCDevice == null)
                    {
                        StaticValues.hwTestRPCDevice = new HardwareTestRPCSerialDevice.Device(StaticValues.sSerialPort,115200);
                    }

                    setOutputPullNone(uCPin);
                    StaticValues.hwTestRPCDevice.SetPin(uCPin, value);

                    MessageBox.Show("Set uCPin " + uCPin.ToString() + " : " + value.ToString());
                    //Console.WriteLine("Set uCPin " + uCPin.ToString() + " : " + value.ToString());

                    if (StaticValues.hwTestRPCDevice != null)
                    {
                        StaticValues.hwTestRPCDevice.Dispose();
                        StaticValues.hwTestRPCDevice = null;
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Set uCPin " + uCPin.ToString() + " failed: " + ex.Message);
                    MessageBox.Show("Set uCPin " + uCPin.ToString() + " failed: " + ex.Message);
                }
            }
        }

        private uint jigCloseCheck()
        {
            DAQdio dioB;

            try
            {
                dioB = new DAQdio(IO_USB6525_B.deviceName);
                dioB.addAllLineReadCH(null);

                uint jigClosesState = dioB.getAllLineDatas();
                //Console.WriteLine(jigClosesState.ToString());
                dioB = null;

                return jigClosesState;
            }
            catch
            {
                dioB = null;
                MessageBox.Show("NI USB-6525(B) init failed.");
                return 0;
            }
        }

        private bool testerInit()
        {
            IT6332c IT6332c;
            KS34461A KS34461A;
            DAQdio dioA;
            LPS505N LPS505N;
            return true;
            //try
            //{
            //    string devInfo = null;
            //    LPS505N = new LPS505N(Power_LPS505N.comPort, Power_LPS505N.baudRate);
            //    LPS505N.Connect();
            //    devInfo = LPS505N.IdentifyDevice();
            //    //Console.WriteLine("DR2003 devInfo: " + devInfo);
            //    if (devInfo == null)
            //    {
            //        LPS505N.Disconnect();
            //        LPS505N = null;
            //        MessageBox.Show("LPS505N init failed.");
            //        return false;
            //    }
            //    LPS505N.SetCh1VOLT(Convert.ToString(Power_LPS505N.V24));
            //    LPS505N.SetCh2VOLT(Convert.ToString(Power_LPS505N.V24));
            //    LPS505N.SetCh3VOLT(Convert.ToString(Power_LPS505N.V15));


            //    LPS505N.SetCh1CURR(Convert.ToString(Power_LPS505N.CURR1));    //1A
            //    LPS505N.SetCh2CURR(Convert.ToString(Power_LPS505N.CURR1));    //1A
            //    LPS505N.SetCh3CURR(Convert.ToString(Power_LPS505N.CURR1));    //1A

            //    LPS505N.Ch1OutPutOff();
            //    System.Threading.Thread.Sleep(30);
            //    LPS505N.Ch2OutPutOff();
            //    System.Threading.Thread.Sleep(30);
            //    LPS505N.Ch3OutPutOff();
            //    System.Threading.Thread.Sleep(30);
            //    LPS505N.Disconnect();
            //    LPS505N = null;
            //}
            //catch
            //{
            //    LPS505N = null;
            //    MessageBox.Show("LPS505N init failed.");
            //    return false;
            //}
            try
            {
                string devInfo = null;
                Console.WriteLine("Power_IT6332A.usbId" + Power_IT6332A.usbId);
                IT6332c = new IT6332c(Power_IT6332A.usbId);
                IT6332c.Connect();
                devInfo = IT6332c.IdentifyDevice();
                Console.WriteLine("IT6332c devInfo: " + devInfo);
                if (devInfo == null)
                {
                    IT6332c.Disconnect();
                    IT6332c = null;
                    MessageBox.Show("IT6332c init failed (1).");
                    return false;
                }
                IT6332c.SetVOLT(Convert.ToString(Power_IT6332A.V195));    //19.5V
                System.Threading.Thread.Sleep(30);
                IT6332c.SetCURR(Convert.ToString(Power_IT6332A.CURR1));   //1A
                System.Threading.Thread.Sleep(30);
                IT6332c.OutPutOff();
                System.Threading.Thread.Sleep(30);
                IT6332c.Disconnect();
            }
            catch
            {
                IT6332c = null;
                MessageBox.Show("IT6332c init failed.(2)");

                return false;
            }

            try
            {
                string devInfo = null;
                KS34461A = new KS34461A(DMM_IT6332A.usbId);
                KS34461A.Connect();
                devInfo = KS34461A.IdentifyDevice();
                Console.WriteLine("IT6332c devInfo: " + devInfo);
                if (devInfo == null)
                {
                    KS34461A.Disconnect();
                    KS34461A = null;
                    MessageBox.Show("KS34461A init failed (1).");
                    return false;
                }
                KS34461A.MeasurePreConfig();
                System.Threading.Thread.Sleep(30);
                KS34461A.Disconnect();
            }
            catch
            {
                KS34461A = null;
                MessageBox.Show("KS34461A init failed.(2)");

                return false;
            }



            try
            {
                dioA = new DAQdio(IO_USB6525_A.deviceName);
                //Console.WriteLine("dioA deviceName =  {0}", IO_USB6525_A.deviceName);
                dioA.addAllLineWriteCH();
                dioA.setAllLineDatas(IO_USB6525_A.OutDefault);
            }
            catch
            {
                dioA = null;
                MessageBox.Show("NI USB-6525(A) init failed.");
                Console.WriteLine("testerInit NI USB-6525(A) init failed ");

                return false;
            }


            IT6332c = null;
            KS34461A = null;
            dioA = null;

            return true;
        }

        private delegate void GridViewScrollFirstRow(int rowIndex);
        private void ScrollFirstRow(int rowIndex)
        {
            if (this.InvokeRequired)
            {
                GridViewScrollFirstRow myScrollRow = new GridViewScrollFirstRow(ScrollFirstRow);
                this.Invoke(myScrollRow, rowIndex);
            }
            else
            {
                gridViewTest.FirstDisplayedScrollingRowIndex = rowIndex;
            }
        }

        private List<string> FindFilesInFolder(string FolderPath, string filter)//要查找的文件夹和文件类型
        {
            List<String> list = new List<string>();
            DirectoryInfo folder = new DirectoryInfo(FolderPath);
            foreach (DirectoryInfo nextfolder in folder.GetDirectories())
            {
                FindFilesInFolder(nextfolder.FullName, filter);
            }
            foreach (FileInfo nextfile in folder.GetFiles(filter))
            {
                list.Add(nextfile.FullName);
            }

            return list;
        }

        public List<string> ReadTextFileToList(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            List<string> list = new List<string>();
            StreamReader sr = new StreamReader(fs);
            //使用StreamReader类来读取文件 
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            // 从数据流中读取每一行，直到文件的最后一行
            string tmp = sr.ReadLine();
            while (tmp != null)
            {
                list.Add(tmp);
                tmp = sr.ReadLine();
            }
            //关闭此StreamReader对象 
            sr.Close();
            fs.Close();
            return list;
        }

        private bool dllCrcList()
        {
            bool bRet = true;
            string crcString = null;
            string filePath = null;

            DataGridViewRowCollection rows = gridViewLib.Rows;

            filePath = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + Path.GetFileName(ControlledDLLs.DLL1.name);
            if (File.Exists(ControlledDLLs.DLL1.name))
            {
                File.Copy(ControlledDLLs.DLL1.name, filePath, true);
            }
            if (File.Exists(filePath))
            {
                crcString = getFileCrc(filePath);
                rows.Add(new Object[] { "Lib", Path.GetFileName(ControlledDLLs.DLL1.name), crcString, ControlledDLLs.DLL1.crc32.Trim().ToUpper() });
                if (crcString != ControlledDLLs.DLL1.crc32.Trim().ToUpper())
                {
                    MessageBox.Show(null, Path.GetFileName(ControlledDLLs.DLL1.name) + " crc failed, must be " + ControlledDLLs.DLL1.crc32.Trim().ToUpper(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    bRet = false;
                }
                crcString = null;
            }
            else
            {
                MessageBox.Show(null, Path.GetFileName(ControlledDLLs.DLL1.name) + " not existing at local", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bRet = false;
            }
            filePath = null;
            return bRet;
            filePath = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + Path.GetFileName(ControlledDLLs.DLL2.name);
            if (File.Exists(ControlledDLLs.DLL2.name))
            {
                File.Copy(ControlledDLLs.DLL2.name, filePath, true);
            }
            if (File.Exists(filePath))
            {
                crcString = getFileCrc(filePath);
                rows.Add(new Object[] { "Lib", Path.GetFileName(ControlledDLLs.DLL2.name), crcString, ControlledDLLs.DLL2.crc32.Trim().ToUpper() });
                if (crcString != ControlledDLLs.DLL2.crc32.Trim().ToUpper())
                {
                    MessageBox.Show(null, Path.GetFileName(ControlledDLLs.DLL2.name) + " crc failed, must be " + ControlledDLLs.DLL2.crc32.Trim().ToUpper(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    bRet = false;
                }
                crcString = null;
            }
            else
            {
                MessageBox.Show(null, Path.GetFileName(ControlledDLLs.DLL2.name) + " not existing at local", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bRet = false;
            }
            filePath = null;

            filePath = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + Path.GetFileName(ControlledDLLs.DLL3.name);
            if (File.Exists(ControlledDLLs.DLL3.name))
            {
                File.Copy(ControlledDLLs.DLL3.name, filePath, true);
            }
            if (File.Exists(filePath))
            {
                crcString = getFileCrc(filePath);
                rows.Add(new Object[] { "Lib", Path.GetFileName(ControlledDLLs.DLL3.name), crcString, ControlledDLLs.DLL3.crc32.Trim().ToUpper() });
                if (crcString != ControlledDLLs.DLL3.crc32.Trim().ToUpper())
                {
                    MessageBox.Show(null, Path.GetFileName(ControlledDLLs.DLL3.name) + " crc failed, must be " + ControlledDLLs.DLL3.crc32.Trim().ToUpper(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    bRet = false;
                }
                crcString = null;
            }
            else
            {
                MessageBox.Show(null, Path.GetFileName(ControlledDLLs.DLL3.name) + " not existing at local", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bRet = false;
            }
            filePath = null;

            filePath = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + Path.GetFileName(ControlledDLLs.DLL4.name);
            if (File.Exists(ControlledDLLs.DLL4.name))
            {
                File.Copy(ControlledDLLs.DLL4.name, filePath, true);
            }
            if (File.Exists(filePath))
            {
                crcString = getFileCrc(filePath);
                rows.Add(new Object[] { "Lib", Path.GetFileName(ControlledDLLs.DLL4.name), crcString, ControlledDLLs.DLL4.crc32.Trim().ToUpper() });
                if (crcString != ControlledDLLs.DLL4.crc32.Trim().ToUpper())
                {
                    MessageBox.Show(null, Path.GetFileName(ControlledDLLs.DLL4.name) + " crc failed, must be " + ControlledDLLs.DLL4.crc32.Trim().ToUpper(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    bRet = false;
                }
                crcString = null;
            }
            else
            {
                MessageBox.Show(null, Path.GetFileName(ControlledDLLs.DLL4.name) + " not existing at local", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bRet = false;
            }
            filePath = null;

            filePath = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + Path.GetFileName(ControlledDLLs.DLL5.name);
            if (File.Exists(ControlledDLLs.DLL5.name))
            {
                File.Copy(ControlledDLLs.DLL5.name, filePath, true);
            }
            if (File.Exists(filePath))
            {
                crcString = getFileCrc(filePath);
                rows.Add(new Object[] { "Lib", Path.GetFileName(ControlledDLLs.DLL5.name), crcString, ControlledDLLs.DLL5.crc32.Trim().ToUpper() });
                if (crcString != ControlledDLLs.DLL5.crc32.Trim().ToUpper())
                {
                    MessageBox.Show(null, Path.GetFileName(ControlledDLLs.DLL5.name) + " crc failed, must be " + ControlledDLLs.DLL5.crc32.Trim().ToUpper(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    bRet = false;
                }
                crcString = null;
            }
            else
            {
                MessageBox.Show(null, Path.GetFileName(ControlledDLLs.DLL5.name) + " not existing at local", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bRet = false;
            }
            filePath = null;

            filePath = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + Path.GetFileName(ControlledDLLs.DLL6.name);
            if (File.Exists(ControlledDLLs.DLL6.name))
            {
                File.Copy(ControlledDLLs.DLL6.name, filePath, true);
            }
            if (File.Exists(filePath))
            {
                crcString = getFileCrc(filePath);
                rows.Add(new Object[] { "Lib", Path.GetFileName(ControlledDLLs.DLL6.name), crcString, ControlledDLLs.DLL6.crc32.Trim().ToUpper() });
                if (crcString != ControlledDLLs.DLL6.crc32.Trim().ToUpper())
                {
                    MessageBox.Show(null, Path.GetFileName(ControlledDLLs.DLL6.name) + " crc failed, must be " + ControlledDLLs.DLL6.crc32.Trim().ToUpper(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    bRet = false;
                }
                crcString = null;
            }
            else
            {
                MessageBox.Show(null, Path.GetFileName(ControlledDLLs.DLL6.name) + " not existing at local", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bRet = false;
            }
            filePath = null;

            return bRet;
        }

        private bool codeCrcList()
        {
            Programming.testFW = new Code();
            Programming.productFW = new Code();
            Programming.Script = new Code();
            Programming.bleDtmFW = new Code();
            Programming.bleProductFW = new Code();
            Programming.externalFlash = new Code();


            if (!File.Exists(ProdConfig.fileTestCode) ||
                !File.Exists(ProdConfig.fileProductCode) ||
                !File.Exists(ProdConfig.fileScript) ||
                !File.Exists(ProdConfig.fileBleTestCode) ||
                !File.Exists(ProdConfig.fileBleProductCode) ||
                !File.Exists(ProdConfig.fileSpiCode))
            {
                return false;
            }

            string crcString = null;
            DataGridViewRowCollection rows = gridViewFW.Rows;

            crcString = getFileCrc(ProdConfig.fileSpiCode);
            rows.Add(new Object[] { "SPI Flash Code", Path.GetFileName(ProdConfig.fileSpiCode), ProdConfig.sfisnameSpiCode, crcString, "" });
            //Programming.Script.name = Path.GetFileNameWithoutExtension(ProdConfig.fileSpiCode);
            Programming.externalFlash.name = ProdConfig.sfisnameSpiCode;
            Programming.externalFlash.crc32 = crcString;
            crcString = null;

            crcString = getFileCrc(ProdConfig.fileBleTestCode);
            rows.Add(new Object[] { "BLE DTM FW", Path.GetFileName(ProdConfig.fileBleTestCode), ProdConfig.sfisnameBleTestCode, crcString, "" });
            //Programming.Script.name = Path.GetFileNameWithoutExtension(ProdConfig.fileBleTestCode);
            Programming.bleDtmFW.name = ProdConfig.sfisnameBleTestCode;
            Programming.bleDtmFW.crc32 = crcString;
            crcString = null;

            crcString = getFileCrc(ProdConfig.fileBleProductCode);
            rows.Add(new Object[] { "BLE Product FW", Path.GetFileName(ProdConfig.fileBleProductCode), ProdConfig.sfisnameBleProductCode, crcString, "" });
            //Programming.Script.name = Path.GetFileNameWithoutExtension(ProdConfig.fileBleProductCode);
            Programming.bleProductFW.name = ProdConfig.sfisnameBleProductCode;
            Programming.bleProductFW.crc32 = crcString;
            crcString = null;

            crcString = getFileCrc(ProdConfig.fileTestCode);
            rows.Add(new Object[] { "Test FW", Path.GetFileName(ProdConfig.fileTestCode), ProdConfig.sfisnameTestCode, crcString, "" });
            //Programming.testFW.name = Path.GetFileNameWithoutExtension(ProdConfig.fileTestCode);
            Programming.testFW.name = ProdConfig.sfisnameTestCode;
            Programming.testFW.crc32 = crcString;
            crcString = null;

            crcString = getFileCrc(ProdConfig.fileProductCode);
            rows.Add(new Object[] { "Product FW", Path.GetFileName(ProdConfig.fileProductCode), ProdConfig.sfisnameProductCode, crcString, "" });
            //Programming.productFW.name = Path.GetFileNameWithoutExtension(ProdConfig.fileProductCode);
            Programming.productFW.name = ProdConfig.sfisnameProductCode;
            Programming.productFW.crc32 = crcString;
            crcString = null;

            crcString = getFileCrc(ProdConfig.fileScript);
            rows.Add(new Object[] { "Script", Path.GetFileName(ProdConfig.fileScript), ProdConfig.sfisnameScript, crcString, "" });
            //Programming.Script.name = Path.GetFileNameWithoutExtension(ProdConfig.fileScript);
            Programming.Script.name = ProdConfig.sfisnameScript;
            Programming.Script.crc32 = crcString;
            crcString = null;

            return true;
        }

        private string getFileCrc(string file)
        {
            string crcString = null;
            CRC32 fileCRC = new CRC32();

            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
            if (fs.Length != 0)
            {
                byte[] fileContent = new byte[fs.Length];
                fs.Read(fileContent, 0, (int)fs.Length);
                crcString = fileCRC.GetCRC32(fileContent);
            }
            fs.Close();
            //Console.WriteLine("getFileCrc ={0}", crcString);
            if (crcString.Length == 7)
            {
                Console.WriteLine("getFileCrc append ");
                crcString = "0" + crcString;
            }
            return crcString;
        }

        private void radio_19v5_StateChanged(object sender, EventArgs e)
        {
            Console.WriteLine("radio_19v5_StateChanged");
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                IT6332c IT6332c;
                try
                {

                    IT6332c = new IT6332c(Power_IT6332A.usbId);
                    IT6332c.Connect();
                    IT6332c.OutPutOn();
                    System.Threading.Thread.Sleep(50);
                    IT6332c.Disconnect();

                    if (radioButton.Tag.ToString() == "1")
                    {
                        IT6332c.SetVOLT(Convert.ToString(Power_DR2002MO.V195));    //19.5V
                        System.Threading.Thread.Sleep(50);
                        IT6332c.SetCURR(Convert.ToString(Power_DR2002MO.CURR1));   //1A
                        System.Threading.Thread.Sleep(50);
                        IT6332c.OutPutOn();
                        System.Threading.Thread.Sleep(50);
                    }
                    else
                    {
                        IT6332c.OutPutOff();
                        
                    }

                    IT6332c.Disconnect();
                }
                catch
                {
                    IT6332c = null;
                    MessageBox.Show("IT6332c init failed.(3)");
                }
                IT6332c = null;
            }
        }

        private void radio_3v3_StateChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                DAQdio dioA;
                try
                {
                    dioA = new DAQdio(IO_USB6525_A.deviceName);
                    dioA.addOneLineWriteCH(IO_USB6525_A.Relay_Support3V3_TP322_K7_P06);
                    if (radioButton.Tag.ToString() == "1")
                        dioA.setOneData(true);
                    else
                        dioA.setOneData(false);
                }
                catch
                {
                    dioA = null;
                    MessageBox.Show("NI USB-6525(A) init failed.");
                }

                dioA = null;
            }
        }

        private void radio_TWLed_StateChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            Console.WriteLine("radio_TWLed_StateChanged");
            try
            {
                if (StaticValues.hwTestRPCDevice == null)
                {
                    StaticValues.hwTestRPCDevice = new HardwareTestRPCSerialDevice.Device(StaticValues.sSerialPort,115200);
                }

                if (StaticValues.hwTestRPCDevice == null)
                {
                    MessageBox.Show("Open DUT COM port fail.1");
                    return;
                }
            }
            catch
            {
                if (StaticValues.hwTestRPCDevice != null)
                {
                    StaticValues.hwTestRPCDevice.Dispose();
                    StaticValues.hwTestRPCDevice = null;
                }
                MessageBox.Show("Open DUT COM port fail.2");
                return;
            }
            Console.WriteLine("radio_TWLed_StateChanged Open DUT COM port");
            if (radioButton.Checked)
            {
                
                if (radioButton.Tag.ToString() == "1")
                {
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UIstandby, 1); // organge off
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet1, 1);
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet2, 1);
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet3, 1);// on
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet4, 1);
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet5, 1);
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedError, 0);//organge
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedRTF, 0); //white
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UI_LED_BLE_R, 1);
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UI_LED_BLE_G, 1);
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UI_LED_BLE_B, 1);
                    Console.WriteLine("TWLed ON");
                }
                else
                {
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UIstandby, 1);  // organge off
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet1, 0);
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet2, 0);
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet3, 0);
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet4, 0);
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet5, 0);
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UI_LED_BLE_R, 0);
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UI_LED_BLE_G, 0);
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UI_LED_BLE_B, 0);
                    Console.WriteLine("TWLed OFF");
                }
            }
            if (StaticValues.hwTestRPCDevice != null)
            {
                StaticValues.hwTestRPCDevice.Dispose();
                StaticValues.hwTestRPCDevice = null;
            }


        }

        private void radio_TOLed_StateChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            try
            {
                if (StaticValues.hwTestRPCDevice == null)
                {
                    StaticValues.hwTestRPCDevice = new HardwareTestRPCSerialDevice.Device(StaticValues.sSerialPort,115200);
                }

                if (StaticValues.hwTestRPCDevice == null)
                {
                    MessageBox.Show("Open DUT COM port fail.1");
                    return;
                }                   

                
            }
            catch
            {
                if (StaticValues.hwTestRPCDevice != null)
                {
                    StaticValues.hwTestRPCDevice.Dispose();
                    StaticValues.hwTestRPCDevice = null;
                }
                MessageBox.Show("Open DUT COM port fail.2");
                return;
            }
            if (radioButton.Checked)
            {
                if (radioButton.Tag.ToString() == "1")
                {

                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UIstandby, 0);
                }
                else
                {
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UIstandby, 1);
                }
            }
            if (StaticValues.hwTestRPCDevice != null)
            {
                StaticValues.hwTestRPCDevice.Dispose();
                StaticValues.hwTestRPCDevice = null;
            }

        }

        private void radio_BWLed_StateChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

                try
                {
                    if (StaticValues.hwTestRPCDevice == null)
                    {
                        StaticValues.hwTestRPCDevice = new HardwareTestRPCSerialDevice.Device(StaticValues.sSerialPort,115200);
                    }

                    if (StaticValues.hwTestRPCDevice == null)
                    {
                        MessageBox.Show("Open DUT COM port fail.1");
                        return;
                    }

                   
                }
                catch
                {
                    if (StaticValues.hwTestRPCDevice != null)
                    {
                        StaticValues.hwTestRPCDevice.Dispose();
                        StaticValues.hwTestRPCDevice = null;
                    }
                    MessageBox.Show("Open DUT COM port fail.2");
                return;
            }
            if (radioButton.Checked)
            {
                if (radioButton.Tag.ToString() == "1")
                {
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedRTF, 1);//white
                }
                else
                {
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedRTF, 0);//white
                }
            }
            if (StaticValues.hwTestRPCDevice != null)
            {
                StaticValues.hwTestRPCDevice.Dispose();
                StaticValues.hwTestRPCDevice = null;
            }



        }

        private void radio_BOLed_StateChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            
            try
            {
                if (StaticValues.hwTestRPCDevice == null)
                {
                    StaticValues.hwTestRPCDevice = new HardwareTestRPCSerialDevice.Device(StaticValues.sSerialPort,115200);
                }

                if (StaticValues.hwTestRPCDevice == null)
                {
                    MessageBox.Show("Open DUT COM port fail.1");
                    return;
                }
                
            }
            catch
            {
                if (StaticValues.hwTestRPCDevice != null)
                {
                    StaticValues.hwTestRPCDevice.Dispose();
                    StaticValues.hwTestRPCDevice = null;
                }
                MessageBox.Show("Open DUT COM port fail.2");
                return;
            }
            if (radioButton.Checked)
            {
                if (radioButton.Tag.ToString() == "1")
                {
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedError, 1);//organge
                }
                else
                {
                    StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedError, 0);//organge
                }
            }
            if (StaticValues.hwTestRPCDevice != null)
            {
                StaticValues.hwTestRPCDevice.Dispose();
                StaticValues.hwTestRPCDevice = null;
            }
        }
        private void setInputPullNone(byte io_idx)
        {
            StaticValues.hwTestRPCDevice.ConfigurePin(io_idx,
                    HardwareTestRPCSerialDevice.PinTypeValue.InputPin,
                    HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);
        }
        private void configurePin()
        {
            //Console.WriteLine("configurePin start");

            try
            {
                //input

                setInputPullNone(StaticValues.uC_BLE_Force_MCU_BL);
                setInputPullNone(StaticValues.uC_BLE_Awake);
                setInputPullNone(StaticValues.uC_UIButtonFlash);
                setInputPullNone(StaticValues.uC_UIButtonON);
                setOutputPullNone(StaticValues.uC_UI_LED_BLE_B);
                setInputPullNone(StaticValues.uC_AttachmentConnected);

                //output

                setOutputPullNone(StaticValues.uC_Enable_FC_Discharge);
                setOutputPullNone(StaticValues.uC_UI_LED_BLE_R);
                setOutputPullNone(StaticValues.uC_CONV_ON);
                setOutputPullNone(StaticValues.uC_SpareP14);
                setOutputPullNone(StaticValues.uC_UI_LED_BLE_G);
                setOutputPullNone(StaticValues.uC_MCU_Reset_BLE);
                setOutputPullNone(StaticValues.uC_12V_Ena);
                setOutputPullNone(StaticValues.uC_UISet1);
                setOutputPullNone(StaticValues.uC_UISet2);
                setOutputPullNone(StaticValues.uC_UISet3);
                setOutputPullNone(StaticValues.uC_Awake);
                setOutputPullNone(StaticValues.uC_SpareP38);
                setOutputPullNone(StaticValues.uC_Attachment_EN);
                setOutputPullNone(StaticValues.uC_Enable_STS_IR);
                setOutputPullNone(StaticValues.uC_UILedError);
                setOutputPullNone(StaticValues.uC_UILedRTF);
                setOutputPullNone(StaticValues.uC_UIstandby);
                setOutputPullNone(StaticValues.uC_Trigger_Lamp);
                setOutputPullNone(StaticValues.uC_Enable_STS_Red);
                setOutputPullNone(StaticValues.uC_UISet4);
                setOutputPullNone(StaticValues.uC_UISet5);
                setOutputPullNone(StaticValues.uC_NC_27);
                setOutputPullNone(StaticValues.uC_Cooling);
                setOutputPullNone(StaticValues.uC_Enable_FC_Charge);



                
            }
            catch
            {
                ;
            }

            
        }
        private void button_Led_Config_Click(object sender, EventArgs e)
        {
            try
            {
                if (StaticValues.hwTestRPCDevice == null)
                {
                    StaticValues.hwTestRPCDevice = new HardwareTestRPCSerialDevice.Device(StaticValues.sSerialPort,115200);
                }

                if (StaticValues.hwTestRPCDevice == null)
                {
                    MessageBox.Show("Open DUT COM port fail.1");
                    return;
                }

                byte value = StaticValues.hwTestRPCDevice.GetInterfaceVersion();
                Console.WriteLine(Convert.ToString(value));
                configurePin();
                //set outputs
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_FC_Discharge, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UI_LED_BLE_R, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_SpareP14, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UI_LED_BLE_G, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_MCU_Reset_BLE, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_12V_Ena, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_SpareP38, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_STS_IR, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Trigger_Lamp, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_STS_Red, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_NC_27, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_FC_Charge, 0);
                
                //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_ON, 1);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_ON, 0);//## //D241652 requirement mentioned
                System.Threading.Thread.Sleep(1);

                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Awake, 1);// keep board awake
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Attachment_EN, 1); //11-12 uC_Attachment_EN :0 enable 3V3 switched

                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Cooling, 0);//fan off 


                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedError, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedRTF, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UIstandby, 1);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet1, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet2, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet3, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet4, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet5, 0);


                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_12V_Ena, 1);

                byte value5 = StaticValues.hwTestRPCDevice.GetInterfaceVersion();
                Console.WriteLine(Convert.ToString(value5));

                //i2cMoSi.Data[0] = 0x00;
                //i2cMoSi.Data[1] = 0x01;
                //i2cMoSi.Length = 2;
                //StaticValues.hwTestRPCDevice.SendMasterI2C(
                //    StaticValues.uC_I2CLEDdriverAddress,
                //    HardwareTestRPCSerialDevice.SercomNumValue.Sercom4,
                //    i2cMoSi);

                //byte value7 = StaticValues.hwTestRPCDevice.GetInterfaceVersion();
                //Console.WriteLine(Convert.ToString(value7));

                //i2cMoSi.Data[0] = 0x14;
                //i2cMoSi.Data[1] = 0xaa;
                //i2cMoSi.Length = 2;
                //StaticValues.hwTestRPCDevice.SendMasterI2C(
                //    StaticValues.uC_I2CLEDdriverAddress,
                //    HardwareTestRPCSerialDevice.SercomNumValue.Sercom4,
                //    i2cMoSi);

                //byte value8 = StaticValues.hwTestRPCDevice.GetInterfaceVersion();
                //Console.WriteLine(Convert.ToString(value8));

                //i2cMoSi.Data[0] = 0x15;
                //i2cMoSi.Data[1] = 0xaa;
                //i2cMoSi.Length = 2;
                //StaticValues.hwTestRPCDevice.SendMasterI2C(
                //    StaticValues.uC_I2CLEDdriverAddress,
                //    HardwareTestRPCSerialDevice.SercomNumValue.Sercom4,
                //    i2cMoSi);

                //i2cMoSi.Data[0] = 0x16;
                //i2cMoSi.Data[1] = 0xaa;
                //i2cMoSi.Length = 2;
                //StaticValues.hwTestRPCDevice.SendMasterI2C(
                //    StaticValues.uC_I2CLEDdriverAddress,
                //    HardwareTestRPCSerialDevice.SercomNumValue.Sercom4,
                //    i2cMoSi);

                //i2cMoSi.Data[0] = 0x17;
                //i2cMoSi.Data[1] = 0xaa;
                //i2cMoSi.Length = 2;
                //StaticValues.hwTestRPCDevice.SendMasterI2C(
                //    StaticValues.uC_I2CLEDdriverAddress,
                //    HardwareTestRPCSerialDevice.SercomNumValue.Sercom4,
                //    i2cMoSi);

                //byte value6 = StaticValues.hwTestRPCDevice.GetInterfaceVersion();
                //Console.WriteLine(Convert.ToString(value6));

                if (StaticValues.hwTestRPCDevice != null)
                {
                    StaticValues.hwTestRPCDevice.Dispose();
                    StaticValues.hwTestRPCDevice = null;
                }

                MessageBox.Show("DUT I/O config done.");
            }
            catch (Exception ex)
            {
                if (StaticValues.hwTestRPCDevice != null)
                {
                    StaticValues.hwTestRPCDevice.Dispose();
                    StaticValues.hwTestRPCDevice = null;
                }

                MessageBox.Show("Init DUT GPIO failed: " + ex.Message);
            }
        }

        private void button_FeasaA_Capture_Click(object sender, EventArgs e)
        {
            richText_FeasaA.Clear();

            Feasa_LED myFS_LED_A = new Feasa_LED(Feasa_10F_A.comPort);

            myFS_LED_A.doC3Capture();
            string captureData = null;
            XY_Data xydata = new XY_Data();
            WL_Data wldata = new WL_Data();
            WL_allData I_DataA = new WL_allData();

            for (int i = 1; i <= 10; i++)
            {
                myFS_LED_A.ReadXY(i, xydata);
                
                myFS_LED_A.ReadWaveLength(i, wldata);

                captureData += "[" + i.ToString() + "]  ";
                captureData += xydata.val_X.ToString();
                captureData += " ";
                captureData += xydata.val_Y.ToString();
                captureData += " | ";
                captureData += xydata.val_I_XY.ToString();
                captureData += " || ";
                captureData += wldata.val_WL.ToString();
                captureData += " | ";
                captureData += wldata.val_I_WL.ToString();
                captureData += "\n";

                richText_FeasaA.AppendText(captureData);
                captureData = null;
                System.Threading.Thread.Sleep(5);
            }
            myFS_LED_A.ReadWavelenall(I_DataA);
            captureData += I_DataA.val_I_WL1.ToString();
            captureData += " | ";
            captureData += I_DataA.val_WL1.ToString();
            captureData += "\n";
            captureData += I_DataA.val_I_WL2.ToString();
            captureData += " | ";
            captureData += I_DataA.val_WL2.ToString();
            captureData += "\n";
            richText_FeasaA.AppendText(captureData);
            captureData = null;

            myFS_LED_A.Close();
            myFS_LED_A = null;
        }

        
        private void gridViewLib_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridView dataGridView = (DataGridView)sender;
            DataGridViewRow row = dataGridView.Rows[e.RowIndex];
            if (e.ColumnIndex == COL_LIB_CAL_CRC)
            {
                if (row.Cells[COL_LIB_EXP_CRC].Value.ToString() == "")
                {
                    e.CellStyle.BackColor = SystemColors.Window;
                }
                else
                {
                    if (row.Cells[COL_LIB_EXP_CRC].Value.ToString() == row.Cells[COL_LIB_CAL_CRC].Value.ToString())
                        e.CellStyle.BackColor = System.Drawing.Color.LimeGreen;
                    else
                        e.CellStyle.BackColor = System.Drawing.Color.Red;
                }
            }
        }

        private void labelVersion_Click(object sender, EventArgs e)
        {

        }

        private void label42_Click(object sender, EventArgs e)
        {

        }

        private void readbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (StaticValues.hwTestRPCDevice == null)
                {
                    StaticValues.hwTestRPCDevice = new HardwareTestRPCSerialDevice.Device(StaticValues.sSerialPort,115200);
                }
                StaticValues.hwTestRPCDevice.ConfigurePin(StaticValues.uC_UIButtonFlash,
                   HardwareTestRPCSerialDevice.PinTypeValue.InputPin,
                   HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);
                Byte pinN = Convert.ToByte(textBox_enterInput.Text);
                ushort data = 1;
                StaticValues.hwTestRPCDevice.GetValue(pinN, ref data);
                textBox_readValue.Text = Convert.ToString(data);
                Console.WriteLine(" READ pin = " + data);
                MessageBox.Show(" READ pin "+pinN.ToString()+" = "+data.ToString());
                //Console.WriteLine("READ pin " + pinN +" = " + Convert.ToString(data));


                if (StaticValues.hwTestRPCDevice != null)
                {
                    StaticValues.hwTestRPCDevice.Dispose();
                    StaticValues.hwTestRPCDevice = null;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("set pin failed: " + ex.Message);
                MessageBox.Show("Set DUT pin failed: " + ex.Message);
            }
            
        }
    }

    public static class ExtensionMethods
    {
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }
}
