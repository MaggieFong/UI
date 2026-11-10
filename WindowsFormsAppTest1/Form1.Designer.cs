namespace WindowsFormsAppTest1
{
    partial class PhilipsEmeraldTestTool
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.workerTestProccess = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tabControlPattern = new System.Windows.Forms.TabControl();
            this.tabPageAll = new System.Windows.Forms.TabPage();
            this.labelLastSn = new System.Windows.Forms.Label();
            this.labelPFGolden = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelGloden = new System.Windows.Forms.Label();
            this.labelSfisInfo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelSfisResult = new System.Windows.Forms.Label();
            this.richTextSfis = new System.Windows.Forms.RichTextBox();
            this.labelTestResult = new System.Windows.Forms.Label();
            this.labelPoccess = new System.Windows.Forms.Label();
            this.gridViewTest = new System.Windows.Forms.DataGridView();
            this.tabPageVISA = new System.Windows.Forms.TabPage();
            this.lab_DR2003_OnOff = new System.Windows.Forms.Label();
            this.btn_DR2003_OnOff = new System.Windows.Forms.Button();
            this.cboGPIB = new System.Windows.Forms.ComboBox();
            this.rbtGPIB = new System.Windows.Forms.RadioButton();
            this.cboBaudRate = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.groupBoxDisplay = new System.Windows.Forms.GroupBox();
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCommand = new System.Windows.Forms.Label();
            this.cboCommand = new System.Windows.Forms.ComboBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.lblOverTime = new System.Windows.Forms.Label();
            this.nudTimeout = new System.Windows.Forms.NumericUpDown();
            this.lblTimeout = new System.Windows.Forms.Label();
            this.lblStopBits = new System.Windows.Forms.Label();
            this.cboStopBits = new System.Windows.Forms.ComboBox();
            this.lblDataBits = new System.Windows.Forms.Label();
            this.cboDataBits = new System.Windows.Forms.ComboBox();
            this.lblFlowControl = new System.Windows.Forms.Label();
            this.cboFlowControl = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboParity = new System.Windows.Forms.ComboBox();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.rbtRS232 = new System.Windows.Forms.RadioButton();
            this.cboUSB = new System.Windows.Forms.ComboBox();
            this.cboRS232 = new System.Windows.Forms.ComboBox();
            this.rbtUSB = new System.Windows.Forms.RadioButton();
            this.tabPageGPIO = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox_readValue = new System.Windows.Forms.TextBox();
            this.textBox_enterInput = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.radio_Attachement_EN_H = new System.Windows.Forms.RadioButton();
            this.radio_Attachement_EN_L = new System.Windows.Forms.RadioButton();
            this.label21 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.radio_Cooling_H = new System.Windows.Forms.RadioButton();
            this.radio_Cooling_L = new System.Windows.Forms.RadioButton();
            this.label27 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.radio_SCAred_H = new System.Windows.Forms.RadioButton();
            this.radio_SCAred_L = new System.Windows.Forms.RadioButton();
            this.label24 = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.radio_SCAir_H = new System.Windows.Forms.RadioButton();
            this.radio_SCAir_L = new System.Windows.Forms.RadioButton();
            this.label28 = new System.Windows.Forms.Label();
            this.panel13 = new System.Windows.Forms.Panel();
            this.radio_Awake_H = new System.Windows.Forms.RadioButton();
            this.radio_Awake_L = new System.Windows.Forms.RadioButton();
            this.label26 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.radio_Bat_En_Battery_H = new System.Windows.Forms.RadioButton();
            this.radio_Bat_En_Battery_L = new System.Windows.Forms.RadioButton();
            this.label16 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.radio_CONV_OFF_H = new System.Windows.Forms.RadioButton();
            this.radio_CONV_OFF_L = new System.Windows.Forms.RadioButton();
            this.label20 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.radio_Bat_En_Adapter_H = new System.Windows.Forms.RadioButton();
            this.radio_Bat_En_Adapter_L = new System.Windows.Forms.RadioButton();
            this.label18 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.radio_Bat_En_Charging_H = new System.Windows.Forms.RadioButton();
            this.radio_Bat_En_Charging_L = new System.Windows.Forms.RadioButton();
            this.label25 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.radio_LED_driver_resetN_H = new System.Windows.Forms.RadioButton();
            this.radio_LED_driver_resetN_L = new System.Windows.Forms.RadioButton();
            this.label17 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radio_CPChargeFlashCap_H = new System.Windows.Forms.RadioButton();
            this.radio_CPChargeFlashCap_L = new System.Windows.Forms.RadioButton();
            this.label19 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.radio_LampPWM_H = new System.Windows.Forms.RadioButton();
            this.radio_LampPWM_L = new System.Windows.Forms.RadioButton();
            this.label23 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radio_CPDischarger_H = new System.Windows.Forms.RadioButton();
            this.radio_CPDischarger_L = new System.Windows.Forms.RadioButton();
            this.label15 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.btn_pullL = new System.Windows.Forms.Button();
            this.btn_pullH = new System.Windows.Forms.Button();
            this.textB_enterpinN = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPageLED = new System.Windows.Forms.TabPage();
            this.richText_FeasaA = new System.Windows.Forms.RichTextBox();
            this.button_FeasaA_Capture = new System.Windows.Forms.Button();
            this.button_Led_Config = new System.Windows.Forms.Button();
            this.panel21 = new System.Windows.Forms.Panel();
            this.radio_BOLed_Off = new System.Windows.Forms.RadioButton();
            this.radio_BOLed_On = new System.Windows.Forms.RadioButton();
            this.panel20 = new System.Windows.Forms.Panel();
            this.radio_BWLed_Off = new System.Windows.Forms.RadioButton();
            this.radio_BWLed_On = new System.Windows.Forms.RadioButton();
            this.panel19 = new System.Windows.Forms.Panel();
            this.radio_TOLed_Off = new System.Windows.Forms.RadioButton();
            this.radio_TOLed_On = new System.Windows.Forms.RadioButton();
            this.panel18 = new System.Windows.Forms.Panel();
            this.radio_TWLed_Off = new System.Windows.Forms.RadioButton();
            this.radio_TWLed_On = new System.Windows.Forms.RadioButton();
            this.panel16 = new System.Windows.Forms.Panel();
            this.radio_3v3_Off = new System.Windows.Forms.RadioButton();
            this.radio_3v3_On = new System.Windows.Forms.RadioButton();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.radio_19v5_Off = new System.Windows.Forms.RadioButton();
            this.radio_19v5_On = new System.Windows.Forms.RadioButton();
            this.label39 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPageSystem = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gridViewLib = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridViewFW = new System.Windows.Forms.DataGridView();
            this.ColumnCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSfisName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCalCRC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSfisCRC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnStart = new System.Windows.Forms.Button();
            this.labelPn = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textFixture = new System.Windows.Forms.TextBox();
            this.textEmp = new System.Windows.Forms.TextBox();
            this.textSn = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelCounter = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ColumnStep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMeasured = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOffset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnUL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnErrorCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControlPattern.SuspendLayout();
            this.tabPageAll.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTest)).BeginInit();
            this.tabPageVISA.SuspendLayout();
            this.groupBoxDisplay.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeout)).BeginInit();
            this.tabPageGPIO.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPageLED.SuspendLayout();
            this.panel21.SuspendLayout();
            this.panel20.SuspendLayout();
            this.panel19.SuspendLayout();
            this.panel18.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel15.SuspendLayout();
            this.tabPageSystem.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLib)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFW)).BeginInit();
            this.SuspendLayout();
            // 
            // workerTestProccess
            // 
            this.workerTestProccess.WorkerReportsProgress = true;
            this.workerTestProccess.WorkerSupportsCancellation = true;
            this.workerTestProccess.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerTestProccess_DoWork);
            this.workerTestProccess.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.workerTestProccess_ProgressChanged);
            this.workerTestProccess.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workerTestProccess_Completed);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(680, 10);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(259, 13);
            this.progressBar1.TabIndex = 3;
            // 
            // tabControlPattern
            // 
            this.tabControlPattern.Controls.Add(this.tabPageAll);
            this.tabControlPattern.Controls.Add(this.tabPageVISA);
            this.tabControlPattern.Controls.Add(this.tabPageGPIO);
            this.tabControlPattern.Controls.Add(this.tabPageLED);
            this.tabControlPattern.Controls.Add(this.tabPageSystem);
            this.tabControlPattern.Location = new System.Drawing.Point(16, 90);
            this.tabControlPattern.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControlPattern.Name = "tabControlPattern";
            this.tabControlPattern.SelectedIndex = 0;
            this.tabControlPattern.Size = new System.Drawing.Size(2851, 1443);
            this.tabControlPattern.TabIndex = 7;
            this.tabControlPattern.TabStop = false;
            this.tabControlPattern.SelectedIndexChanged += new System.EventHandler(this.selectedPage);
            // 
            // tabPageAll
            // 
            this.tabPageAll.Controls.Add(this.labelLastSn);
            this.tabPageAll.Controls.Add(this.labelPFGolden);
            this.tabPageAll.Controls.Add(this.labelVersion);
            this.tabPageAll.Controls.Add(this.labelGloden);
            this.tabPageAll.Controls.Add(this.labelSfisInfo);
            this.tabPageAll.Controls.Add(this.panel1);
            this.tabPageAll.Controls.Add(this.labelTestResult);
            this.tabPageAll.Controls.Add(this.labelPoccess);
            this.tabPageAll.Controls.Add(this.progressBar1);
            this.tabPageAll.Controls.Add(this.gridViewTest);
            this.tabPageAll.Location = new System.Drawing.Point(4, 32);
            this.tabPageAll.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageAll.Name = "tabPageAll";
            this.tabPageAll.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageAll.Size = new System.Drawing.Size(2843, 1407);
            this.tabPageAll.TabIndex = 2;
            this.tabPageAll.Text = "Test Items";
            this.tabPageAll.UseVisualStyleBackColor = true;
            // 
            // labelLastSn
            // 
            this.labelLastSn.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLastSn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.labelLastSn.Location = new System.Drawing.Point(1033, 109);
            this.labelLastSn.Name = "labelLastSn";
            this.labelLastSn.Size = new System.Drawing.Size(111, 19);
            this.labelLastSn.TabIndex = 19;
            this.labelLastSn.Text = "MA1234564";
            this.labelLastSn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelLastSn.Visible = false;
            // 
            // labelPFGolden
            // 
            this.labelPFGolden.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelPFGolden.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPFGolden.ForeColor = System.Drawing.Color.DeepPink;
            this.labelPFGolden.Location = new System.Drawing.Point(1035, 238);
            this.labelPFGolden.Name = "labelPFGolden";
            this.labelPFGolden.Size = new System.Drawing.Size(107, 76);
            this.labelPFGolden.TabIndex = 18;
            this.labelPFGolden.Text = "Pass Golden";
            this.labelPFGolden.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelPFGolden.Visible = false;
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelVersion.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.ForeColor = System.Drawing.Color.Blue;
            this.labelVersion.Location = new System.Drawing.Point(1074, 498);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(83, 43);
            this.labelVersion.TabIndex = 17;
            this.labelVersion.Text = "V1.1";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelVersion.Click += new System.EventHandler(this.labelVersion_Click);
            // 
            // labelGloden
            // 
            this.labelGloden.BackColor = System.Drawing.Color.Gold;
            this.labelGloden.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelGloden.Enabled = false;
            this.labelGloden.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGloden.Location = new System.Drawing.Point(1035, 144);
            this.labelGloden.Name = "labelGloden";
            this.labelGloden.Size = new System.Drawing.Size(107, 76);
            this.labelGloden.TabIndex = 16;
            this.labelGloden.Text = "Golden";
            this.labelGloden.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelGloden.Visible = false;
            // 
            // labelSfisInfo
            // 
            this.labelSfisInfo.AutoSize = true;
            this.labelSfisInfo.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSfisInfo.Location = new System.Drawing.Point(31, 464);
            this.labelSfisInfo.Name = "labelSfisInfo";
            this.labelSfisInfo.Size = new System.Drawing.Size(95, 24);
            this.labelSfisInfo.TabIndex = 0;
            this.labelSfisInfo.Text = " SFIS Info. ";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelSfisResult);
            this.panel1.Controls.Add(this.richTextSfis);
            this.panel1.Location = new System.Drawing.Point(15, 475);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1013, 70);
            this.panel1.TabIndex = 15;
            // 
            // labelSfisResult
            // 
            this.labelSfisResult.BackColor = System.Drawing.Color.LimeGreen;
            this.labelSfisResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelSfisResult.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSfisResult.Location = new System.Drawing.Point(878, 13);
            this.labelSfisResult.Name = "labelSfisResult";
            this.labelSfisResult.Size = new System.Drawing.Size(118, 44);
            this.labelSfisResult.TabIndex = 15;
            this.labelSfisResult.Text = "N/A";
            this.labelSfisResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelSfisResult.Visible = false;
            this.labelSfisResult.TextChanged += new System.EventHandler(this.labelResult_TextChanged);
            // 
            // richTextSfis
            // 
            this.richTextSfis.Location = new System.Drawing.Point(19, 13);
            this.richTextSfis.Name = "richTextSfis";
            this.richTextSfis.ReadOnly = true;
            this.richTextSfis.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextSfis.Size = new System.Drawing.Size(844, 44);
            this.richTextSfis.TabIndex = 0;
            this.richTextSfis.Text = "<<     | SSS,MMM,FFF,1,,OK\n    >> | OK";
            // 
            // labelTestResult
            // 
            this.labelTestResult.BackColor = System.Drawing.Color.LimeGreen;
            this.labelTestResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelTestResult.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTestResult.Location = new System.Drawing.Point(1035, 33);
            this.labelTestResult.Name = "labelTestResult";
            this.labelTestResult.Size = new System.Drawing.Size(107, 76);
            this.labelTestResult.TabIndex = 14;
            this.labelTestResult.Text = "N/A";
            this.labelTestResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelTestResult.Visible = false;
            this.labelTestResult.TextChanged += new System.EventHandler(this.labelTestResult_TextChanged);
            // 
            // labelPoccess
            // 
            this.labelPoccess.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPoccess.Location = new System.Drawing.Point(946, 7);
            this.labelPoccess.Name = "labelPoccess";
            this.labelPoccess.Size = new System.Drawing.Size(82, 19);
            this.labelPoccess.TabIndex = 4;
            this.labelPoccess.Text = "147/147";
            this.labelPoccess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gridViewTest
            // 
            this.gridViewTest.AllowUserToAddRows = false;
            this.gridViewTest.AllowUserToDeleteRows = false;
            this.gridViewTest.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridViewTest.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gridViewTest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewTest.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnStep,
            this.ColumnTest,
            this.ColumnMeasured,
            this.ColumnOffset,
            this.ColumnLL,
            this.ColumnUL,
            this.ColumnErrorCode,
            this.ColumnStatus});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridViewTest.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridViewTest.Location = new System.Drawing.Point(15, 33);
            this.gridViewTest.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridViewTest.Name = "gridViewTest";
            this.gridViewTest.ReadOnly = true;
            this.gridViewTest.RowHeadersWidth = 24;
            this.gridViewTest.RowTemplate.Height = 24;
            this.gridViewTest.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridViewTest.Size = new System.Drawing.Size(1013, 426);
            this.gridViewTest.TabIndex = 0;
            this.gridViewTest.TabStop = false;
            this.gridViewTest.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gridViewTest_CellPainting);
            // 
            // tabPageVISA
            // 
            this.tabPageVISA.Controls.Add(this.lab_DR2003_OnOff);
            this.tabPageVISA.Controls.Add(this.btn_DR2003_OnOff);
            this.tabPageVISA.Controls.Add(this.cboGPIB);
            this.tabPageVISA.Controls.Add(this.rbtGPIB);
            this.tabPageVISA.Controls.Add(this.cboBaudRate);
            this.tabPageVISA.Controls.Add(this.btnRefresh);
            this.tabPageVISA.Controls.Add(this.groupBoxDisplay);
            this.tabPageVISA.Controls.Add(this.flowLayoutPanel3);
            this.tabPageVISA.Controls.Add(this.lblStopBits);
            this.tabPageVISA.Controls.Add(this.cboStopBits);
            this.tabPageVISA.Controls.Add(this.lblDataBits);
            this.tabPageVISA.Controls.Add(this.cboDataBits);
            this.tabPageVISA.Controls.Add(this.lblFlowControl);
            this.tabPageVISA.Controls.Add(this.cboFlowControl);
            this.tabPageVISA.Controls.Add(this.label1);
            this.tabPageVISA.Controls.Add(this.cboParity);
            this.tabPageVISA.Controls.Add(this.lblBaudRate);
            this.tabPageVISA.Controls.Add(this.btnOpen);
            this.tabPageVISA.Controls.Add(this.rbtRS232);
            this.tabPageVISA.Controls.Add(this.cboUSB);
            this.tabPageVISA.Controls.Add(this.cboRS232);
            this.tabPageVISA.Controls.Add(this.rbtUSB);
            this.tabPageVISA.Location = new System.Drawing.Point(4, 32);
            this.tabPageVISA.Name = "tabPageVISA";
            this.tabPageVISA.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageVISA.Size = new System.Drawing.Size(2843, 1407);
            this.tabPageVISA.TabIndex = 3;
            this.tabPageVISA.Text = "VISA Instrument";
            this.tabPageVISA.UseVisualStyleBackColor = true;
            // 
            // lab_DR2003_OnOff
            // 
            this.lab_DR2003_OnOff.AutoSize = true;
            this.lab_DR2003_OnOff.Location = new System.Drawing.Point(957, 127);
            this.lab_DR2003_OnOff.Name = "lab_DR2003_OnOff";
            this.lab_DR2003_OnOff.Size = new System.Drawing.Size(153, 23);
            this.lab_DR2003_OnOff.TabIndex = 50;
            this.lab_DR2003_OnOff.Text = " Power On Off Test";
            // 
            // btn_DR2003_OnOff
            // 
            this.btn_DR2003_OnOff.Location = new System.Drawing.Point(960, 148);
            this.btn_DR2003_OnOff.Name = "btn_DR2003_OnOff";
            this.btn_DR2003_OnOff.Size = new System.Drawing.Size(99, 25);
            this.btn_DR2003_OnOff.TabIndex = 49;
            this.btn_DR2003_OnOff.Text = "On";
            this.btn_DR2003_OnOff.UseVisualStyleBackColor = true;
            this.btn_DR2003_OnOff.Click += new System.EventHandler(this.btn_DR2003_OnOff_Click);
            // 
            // cboGPIB
            // 
            this.cboGPIB.FormattingEnabled = true;
            this.cboGPIB.Location = new System.Drawing.Point(86, 82);
            this.cboGPIB.Name = "cboGPIB";
            this.cboGPIB.Size = new System.Drawing.Size(426, 31);
            this.cboGPIB.TabIndex = 48;
            // 
            // rbtGPIB
            // 
            this.rbtGPIB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbtGPIB.AutoSize = true;
            this.rbtGPIB.Location = new System.Drawing.Point(25, 83);
            this.rbtGPIB.Name = "rbtGPIB";
            this.rbtGPIB.Size = new System.Drawing.Size(73, 27);
            this.rbtGPIB.TabIndex = 47;
            this.rbtGPIB.Text = "GPIB ";
            this.rbtGPIB.UseVisualStyleBackColor = true;
            // 
            // cboBaudRate
            // 
            this.cboBaudRate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBaudRate.FormattingEnabled = true;
            this.cboBaudRate.Location = new System.Drawing.Point(305, 4);
            this.cboBaudRate.Margin = new System.Windows.Forms.Padding(3, 3, 17, 3);
            this.cboBaudRate.Name = "cboBaudRate";
            this.cboBaudRate.Size = new System.Drawing.Size(73, 31);
            this.cboBaudRate.TabIndex = 46;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(26, 117);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(70, 23);
            this.btnRefresh.TabIndex = 45;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // groupBoxDisplay
            // 
            this.groupBoxDisplay.Controls.Add(this.txtDisplay);
            this.groupBoxDisplay.Location = new System.Drawing.Point(11, 186);
            this.groupBoxDisplay.Name = "groupBoxDisplay";
            this.groupBoxDisplay.Size = new System.Drawing.Size(833, 334);
            this.groupBoxDisplay.TabIndex = 44;
            this.groupBoxDisplay.TabStop = false;
            this.groupBoxDisplay.Text = "顯示";
            // 
            // txtDisplay
            // 
            this.txtDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDisplay.Location = new System.Drawing.Point(3, 26);
            this.txtDisplay.Multiline = true;
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.ReadOnly = true;
            this.txtDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDisplay.Size = new System.Drawing.Size(827, 305);
            this.txtDisplay.TabIndex = 1;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.lblCommand);
            this.flowLayoutPanel3.Controls.Add(this.cboCommand);
            this.flowLayoutPanel3.Controls.Add(this.btnWrite);
            this.flowLayoutPanel3.Controls.Add(this.btnRead);
            this.flowLayoutPanel3.Controls.Add(this.btnQuery);
            this.flowLayoutPanel3.Controls.Add(this.lblOverTime);
            this.flowLayoutPanel3.Controls.Add(this.nudTimeout);
            this.flowLayoutPanel3.Controls.Add(this.lblTimeout);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(9, 148);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(742, 37);
            this.flowLayoutPanel3.TabIndex = 43;
            // 
            // lblCommand
            // 
            this.lblCommand.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCommand.AutoSize = true;
            this.lblCommand.Location = new System.Drawing.Point(15, 6);
            this.lblCommand.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.lblCommand.Name = "lblCommand";
            this.lblCommand.Size = new System.Drawing.Size(89, 23);
            this.lblCommand.TabIndex = 0;
            this.lblCommand.Text = "Command";
            // 
            // cboCommand
            // 
            this.cboCommand.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboCommand.FormattingEnabled = true;
            this.cboCommand.Location = new System.Drawing.Point(110, 6);
            this.cboCommand.Name = "cboCommand";
            this.cboCommand.Size = new System.Drawing.Size(277, 31);
            this.cboCommand.TabIndex = 1;
            // 
            // btnWrite
            // 
            this.btnWrite.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnWrite.Location = new System.Drawing.Point(393, 3);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(53, 30);
            this.btnWrite.TabIndex = 2;
            this.btnWrite.Text = "Write";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnRead
            // 
            this.btnRead.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnRead.Location = new System.Drawing.Point(452, 3);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(53, 30);
            this.btnRead.TabIndex = 3;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnQuery.Location = new System.Drawing.Point(511, 4);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(59, 28);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "Query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // lblOverTime
            // 
            this.lblOverTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblOverTime.AutoSize = true;
            this.lblOverTime.Location = new System.Drawing.Point(603, 6);
            this.lblOverTime.Margin = new System.Windows.Forms.Padding(30, 0, 3, 0);
            this.lblOverTime.Name = "lblOverTime";
            this.lblOverTime.Size = new System.Drawing.Size(73, 23);
            this.lblOverTime.TabIndex = 5;
            this.lblOverTime.Text = "Timeout";
            // 
            // nudTimeout
            // 
            this.nudTimeout.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudTimeout.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudTimeout.Location = new System.Drawing.Point(3, 39);
            this.nudTimeout.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudTimeout.Name = "nudTimeout";
            this.nudTimeout.Size = new System.Drawing.Size(94, 30);
            this.nudTimeout.TabIndex = 6;
            this.nudTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudTimeout.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // lblTimeout
            // 
            this.lblTimeout.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTimeout.AutoSize = true;
            this.lblTimeout.Location = new System.Drawing.Point(103, 42);
            this.lblTimeout.Name = "lblTimeout";
            this.lblTimeout.Size = new System.Drawing.Size(33, 23);
            this.lblTimeout.TabIndex = 7;
            this.lblTimeout.Text = "ms";
            // 
            // lblStopBits
            // 
            this.lblStopBits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStopBits.AutoSize = true;
            this.lblStopBits.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStopBits.Location = new System.Drawing.Point(572, 8);
            this.lblStopBits.Name = "lblStopBits";
            this.lblStopBits.Size = new System.Drawing.Size(71, 19);
            this.lblStopBits.TabIndex = 41;
            this.lblStopBits.Text = "停止位:";
            // 
            // cboStopBits
            // 
            this.cboStopBits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStopBits.FormattingEnabled = true;
            this.cboStopBits.Location = new System.Drawing.Point(634, 3);
            this.cboStopBits.Margin = new System.Windows.Forms.Padding(3, 3, 17, 3);
            this.cboStopBits.Name = "cboStopBits";
            this.cboStopBits.Size = new System.Drawing.Size(77, 31);
            this.cboStopBits.TabIndex = 42;
            // 
            // lblDataBits
            // 
            this.lblDataBits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDataBits.AutoSize = true;
            this.lblDataBits.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataBits.Location = new System.Drawing.Point(731, 5);
            this.lblDataBits.Name = "lblDataBits";
            this.lblDataBits.Size = new System.Drawing.Size(71, 19);
            this.lblDataBits.TabIndex = 37;
            this.lblDataBits.Text = "數據位:";
            // 
            // cboDataBits
            // 
            this.cboDataBits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDataBits.FormattingEnabled = true;
            this.cboDataBits.Location = new System.Drawing.Point(793, 3);
            this.cboDataBits.Margin = new System.Windows.Forms.Padding(3, 3, 17, 3);
            this.cboDataBits.Name = "cboDataBits";
            this.cboDataBits.Size = new System.Drawing.Size(73, 31);
            this.cboDataBits.TabIndex = 39;
            // 
            // lblFlowControl
            // 
            this.lblFlowControl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFlowControl.AutoSize = true;
            this.lblFlowControl.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFlowControl.Location = new System.Drawing.Point(886, 5);
            this.lblFlowControl.Name = "lblFlowControl";
            this.lblFlowControl.Size = new System.Drawing.Size(71, 19);
            this.lblFlowControl.TabIndex = 38;
            this.lblFlowControl.Text = "控制流:";
            // 
            // cboFlowControl
            // 
            this.cboFlowControl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboFlowControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFlowControl.FormattingEnabled = true;
            this.cboFlowControl.Location = new System.Drawing.Point(948, 1);
            this.cboFlowControl.Name = "cboFlowControl";
            this.cboFlowControl.Size = new System.Drawing.Size(73, 31);
            this.cboFlowControl.TabIndex = 40;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(387, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 19);
            this.label1.TabIndex = 36;
            this.label1.Text = "奇偶校驗位:";
            // 
            // cboParity
            // 
            this.cboParity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboParity.FormattingEnabled = true;
            this.cboParity.Location = new System.Drawing.Point(479, 3);
            this.cboParity.Margin = new System.Windows.Forms.Padding(3, 3, 17, 3);
            this.cboParity.Name = "cboParity";
            this.cboParity.Size = new System.Drawing.Size(73, 31);
            this.cboParity.TabIndex = 35;
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Location = new System.Drawing.Point(230, 7);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(92, 23);
            this.lblBaudRate.TabIndex = 34;
            this.lblBaudRate.Text = "Baud Rate:";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(125, 115);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(99, 25);
            this.btnOpen.TabIndex = 33;
            this.btnOpen.Text = "Connect";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // rbtRS232
            // 
            this.rbtRS232.AutoSize = true;
            this.rbtRS232.Checked = true;
            this.rbtRS232.Location = new System.Drawing.Point(25, 5);
            this.rbtRS232.Name = "rbtRS232";
            this.rbtRS232.Size = new System.Drawing.Size(80, 27);
            this.rbtRS232.TabIndex = 29;
            this.rbtRS232.TabStop = true;
            this.rbtRS232.Text = "RS232";
            this.rbtRS232.UseVisualStyleBackColor = true;
            // 
            // cboUSB
            // 
            this.cboUSB.FormattingEnabled = true;
            this.cboUSB.Location = new System.Drawing.Point(86, 41);
            this.cboUSB.Name = "cboUSB";
            this.cboUSB.Size = new System.Drawing.Size(426, 31);
            this.cboUSB.TabIndex = 32;
            // 
            // cboRS232
            // 
            this.cboRS232.FormattingEnabled = true;
            this.cboRS232.Location = new System.Drawing.Point(86, 0);
            this.cboRS232.Name = "cboRS232";
            this.cboRS232.Size = new System.Drawing.Size(121, 31);
            this.cboRS232.TabIndex = 31;
            // 
            // rbtUSB
            // 
            this.rbtUSB.AutoSize = true;
            this.rbtUSB.Location = new System.Drawing.Point(25, 45);
            this.rbtUSB.Name = "rbtUSB";
            this.rbtUSB.Size = new System.Drawing.Size(62, 27);
            this.rbtUSB.TabIndex = 30;
            this.rbtUSB.Text = "USB";
            this.rbtUSB.UseVisualStyleBackColor = true;
            // 
            // tabPageGPIO
            // 
            this.tabPageGPIO.Controls.Add(this.button2);
            this.tabPageGPIO.Controls.Add(this.textBox_readValue);
            this.tabPageGPIO.Controls.Add(this.textBox_enterInput);
            this.tabPageGPIO.Controls.Add(this.label42);
            this.tabPageGPIO.Controls.Add(this.groupBox1);
            this.tabPageGPIO.Controls.Add(this.btn_pullL);
            this.tabPageGPIO.Controls.Add(this.btn_pullH);
            this.tabPageGPIO.Controls.Add(this.textB_enterpinN);
            this.tabPageGPIO.Controls.Add(this.label12);
            this.tabPageGPIO.Controls.Add(this.button1);
            this.tabPageGPIO.Location = new System.Drawing.Point(4, 32);
            this.tabPageGPIO.Name = "tabPageGPIO";
            this.tabPageGPIO.Size = new System.Drawing.Size(2843, 1407);
            this.tabPageGPIO.TabIndex = 4;
            this.tabPageGPIO.Text = "DUT GPIO";
            this.tabPageGPIO.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(400, 169);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 33);
            this.button2.TabIndex = 66;
            this.button2.Text = "READ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.readbutton_Click);
            // 
            // textBox_readValue
            // 
            this.textBox_readValue.Location = new System.Drawing.Point(550, 173);
            this.textBox_readValue.Name = "textBox_readValue";
            this.textBox_readValue.Size = new System.Drawing.Size(244, 30);
            this.textBox_readValue.TabIndex = 65;
            // 
            // textBox_enterInput
            // 
            this.textBox_enterInput.Location = new System.Drawing.Point(550, 129);
            this.textBox_enterInput.Name = "textBox_enterInput";
            this.textBox_enterInput.Size = new System.Drawing.Size(244, 30);
            this.textBox_enterInput.TabIndex = 64;
            this.textBox_enterInput.Text = "16";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(397, 132);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(167, 23);
            this.label42.TabIndex = 63;
            this.label42.Text = "Read Input Pin value";
            this.label42.Click += new System.EventHandler(this.label42_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.panel14);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.panel10);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.panel11);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.panel12);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.panel13);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.panel6);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.panel7);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.panel8);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.panel9);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.panel5);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Location = new System.Drawing.Point(43, 115);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 417);
            this.groupBox1.TabIndex = 62;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "uC Output";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(30, 62);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(142, 23);
            this.label14.TabIndex = 5;
            this.label14.Text = "uC_CPDischarger";
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.radio_Attachement_EN_H);
            this.panel14.Controls.Add(this.radio_Attachement_EN_L);
            this.panel14.Location = new System.Drawing.Point(230, 350);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(67, 18);
            this.panel14.TabIndex = 61;
            this.panel14.Tag = "34";
            // 
            // radio_Attachement_EN_H
            // 
            this.radio_Attachement_EN_H.AutoSize = true;
            this.radio_Attachement_EN_H.Location = new System.Drawing.Point(3, 2);
            this.radio_Attachement_EN_H.Name = "radio_Attachement_EN_H";
            this.radio_Attachement_EN_H.Size = new System.Drawing.Size(17, 16);
            this.radio_Attachement_EN_H.TabIndex = 48;
            this.radio_Attachement_EN_H.TabStop = true;
            this.radio_Attachement_EN_H.Tag = "1";
            this.radio_Attachement_EN_H.UseVisualStyleBackColor = true;
            this.radio_Attachement_EN_H.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // radio_Attachement_EN_L
            // 
            this.radio_Attachement_EN_L.AutoSize = true;
            this.radio_Attachement_EN_L.Location = new System.Drawing.Point(44, 2);
            this.radio_Attachement_EN_L.Name = "radio_Attachement_EN_L";
            this.radio_Attachement_EN_L.Size = new System.Drawing.Size(17, 16);
            this.radio_Attachement_EN_L.TabIndex = 49;
            this.radio_Attachement_EN_L.TabStop = true;
            this.radio_Attachement_EN_L.Tag = "0";
            this.radio_Attachement_EN_L.UseVisualStyleBackColor = true;
            this.radio_Attachement_EN_L.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(30, 158);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(169, 23);
            this.label21.TabIndex = 9;
            this.label21.Text = "uC_Bat_En_Charging";
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.radio_Cooling_H);
            this.panel10.Controls.Add(this.radio_Cooling_L);
            this.panel10.Location = new System.Drawing.Point(230, 326);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(67, 18);
            this.panel10.TabIndex = 60;
            this.panel10.Tag = "41";
            // 
            // radio_Cooling_H
            // 
            this.radio_Cooling_H.AutoSize = true;
            this.radio_Cooling_H.Location = new System.Drawing.Point(3, 2);
            this.radio_Cooling_H.Name = "radio_Cooling_H";
            this.radio_Cooling_H.Size = new System.Drawing.Size(17, 16);
            this.radio_Cooling_H.TabIndex = 48;
            this.radio_Cooling_H.TabStop = true;
            this.radio_Cooling_H.Tag = "1";
            this.radio_Cooling_H.UseVisualStyleBackColor = true;
            this.radio_Cooling_H.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // radio_Cooling_L
            // 
            this.radio_Cooling_L.AutoSize = true;
            this.radio_Cooling_L.Location = new System.Drawing.Point(44, 2);
            this.radio_Cooling_L.Name = "radio_Cooling_L";
            this.radio_Cooling_L.Size = new System.Drawing.Size(17, 16);
            this.radio_Cooling_L.TabIndex = 49;
            this.radio_Cooling_L.TabStop = true;
            this.radio_Cooling_L.Tag = "0";
            this.radio_Cooling_L.UseVisualStyleBackColor = true;
            this.radio_Cooling_L.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(30, 254);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(90, 23);
            this.label27.TabIndex = 13;
            this.label27.Text = "uC_Awake";
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.radio_SCAred_H);
            this.panel11.Controls.Add(this.radio_SCAred_L);
            this.panel11.Location = new System.Drawing.Point(230, 278);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(67, 18);
            this.panel11.TabIndex = 58;
            this.panel11.Tag = "25";
            // 
            // radio_SCAred_H
            // 
            this.radio_SCAred_H.AutoSize = true;
            this.radio_SCAred_H.Location = new System.Drawing.Point(3, 2);
            this.radio_SCAred_H.Name = "radio_SCAred_H";
            this.radio_SCAred_H.Size = new System.Drawing.Size(17, 16);
            this.radio_SCAred_H.TabIndex = 48;
            this.radio_SCAred_H.TabStop = true;
            this.radio_SCAred_H.Tag = "1";
            this.radio_SCAred_H.UseVisualStyleBackColor = true;
            this.radio_SCAred_H.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // radio_SCAred_L
            // 
            this.radio_SCAred_L.AutoSize = true;
            this.radio_SCAred_L.Location = new System.Drawing.Point(44, 2);
            this.radio_SCAred_L.Name = "radio_SCAred_L";
            this.radio_SCAred_L.Size = new System.Drawing.Size(17, 16);
            this.radio_SCAred_L.TabIndex = 49;
            this.radio_SCAred_L.TabStop = true;
            this.radio_SCAred_L.Tag = "0";
            this.radio_SCAred_L.UseVisualStyleBackColor = true;
            this.radio_SCAred_L.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(231, 41);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(26, 23);
            this.label24.TabIndex = 21;
            this.label24.Text = "Hi";
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.radio_SCAir_H);
            this.panel12.Controls.Add(this.radio_SCAir_L);
            this.panel12.Location = new System.Drawing.Point(230, 302);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(67, 18);
            this.panel12.TabIndex = 59;
            this.panel12.Tag = "23";
            // 
            // radio_SCAir_H
            // 
            this.radio_SCAir_H.AutoSize = true;
            this.radio_SCAir_H.Location = new System.Drawing.Point(3, 2);
            this.radio_SCAir_H.Name = "radio_SCAir_H";
            this.radio_SCAir_H.Size = new System.Drawing.Size(17, 16);
            this.radio_SCAir_H.TabIndex = 48;
            this.radio_SCAir_H.TabStop = true;
            this.radio_SCAir_H.Tag = "1";
            this.radio_SCAir_H.UseVisualStyleBackColor = true;
            this.radio_SCAir_H.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // radio_SCAir_L
            // 
            this.radio_SCAir_L.AutoSize = true;
            this.radio_SCAir_L.Location = new System.Drawing.Point(44, 2);
            this.radio_SCAir_L.Name = "radio_SCAir_L";
            this.radio_SCAir_L.Size = new System.Drawing.Size(17, 16);
            this.radio_SCAir_L.TabIndex = 49;
            this.radio_SCAir_L.TabStop = true;
            this.radio_SCAir_L.Tag = "0";
            this.radio_SCAir_L.UseVisualStyleBackColor = true;
            this.radio_SCAir_L.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(272, 41);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(28, 23);
            this.label28.TabIndex = 22;
            this.label28.Text = "Lo";
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.radio_Awake_H);
            this.panel13.Controls.Add(this.radio_Awake_L);
            this.panel13.Location = new System.Drawing.Point(230, 254);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(67, 18);
            this.panel13.TabIndex = 57;
            this.panel13.Tag = "15";
            // 
            // radio_Awake_H
            // 
            this.radio_Awake_H.AutoSize = true;
            this.radio_Awake_H.Location = new System.Drawing.Point(3, 2);
            this.radio_Awake_H.Name = "radio_Awake_H";
            this.radio_Awake_H.Size = new System.Drawing.Size(17, 16);
            this.radio_Awake_H.TabIndex = 48;
            this.radio_Awake_H.TabStop = true;
            this.radio_Awake_H.Tag = "1";
            this.radio_Awake_H.UseVisualStyleBackColor = true;
            this.radio_Awake_H.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // radio_Awake_L
            // 
            this.radio_Awake_L.AutoSize = true;
            this.radio_Awake_L.Location = new System.Drawing.Point(44, 2);
            this.radio_Awake_L.Name = "radio_Awake_L";
            this.radio_Awake_L.Size = new System.Drawing.Size(17, 16);
            this.radio_Awake_L.TabIndex = 49;
            this.radio_Awake_L.TabStop = true;
            this.radio_Awake_L.Tag = "0";
            this.radio_Awake_L.UseVisualStyleBackColor = true;
            this.radio_Awake_L.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(30, 278);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(95, 23);
            this.label26.TabIndex = 14;
            this.label26.Text = "uC_SCAred";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.radio_Bat_En_Battery_H);
            this.panel6.Controls.Add(this.radio_Bat_En_Battery_L);
            this.panel6.Location = new System.Drawing.Point(230, 230);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(67, 18);
            this.panel6.TabIndex = 56;
            this.panel6.Tag = "46";
            // 
            // radio_Bat_En_Battery_H
            // 
            this.radio_Bat_En_Battery_H.AutoSize = true;
            this.radio_Bat_En_Battery_H.Location = new System.Drawing.Point(3, 2);
            this.radio_Bat_En_Battery_H.Name = "radio_Bat_En_Battery_H";
            this.radio_Bat_En_Battery_H.Size = new System.Drawing.Size(17, 16);
            this.radio_Bat_En_Battery_H.TabIndex = 48;
            this.radio_Bat_En_Battery_H.TabStop = true;
            this.radio_Bat_En_Battery_H.Tag = "1";
            this.radio_Bat_En_Battery_H.UseVisualStyleBackColor = true;
            this.radio_Bat_En_Battery_H.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // radio_Bat_En_Battery_L
            // 
            this.radio_Bat_En_Battery_L.AutoSize = true;
            this.radio_Bat_En_Battery_L.Location = new System.Drawing.Point(44, 2);
            this.radio_Bat_En_Battery_L.Name = "radio_Bat_En_Battery_L";
            this.radio_Bat_En_Battery_L.Size = new System.Drawing.Size(17, 16);
            this.radio_Bat_En_Battery_L.TabIndex = 49;
            this.radio_Bat_En_Battery_L.TabStop = true;
            this.radio_Bat_En_Battery_L.Tag = "0";
            this.radio_Bat_En_Battery_L.UseVisualStyleBackColor = true;
            this.radio_Bat_En_Battery_L.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(30, 134);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(183, 23);
            this.label16.TabIndex = 8;
            this.label16.Text = "uC_LED_driver_resetN";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.radio_CONV_OFF_H);
            this.panel7.Controls.Add(this.radio_CONV_OFF_L);
            this.panel7.Location = new System.Drawing.Point(230, 182);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(67, 18);
            this.panel7.TabIndex = 54;
            this.panel7.Tag = "18";
            // 
            // radio_CONV_OFF_H
            // 
            this.radio_CONV_OFF_H.AutoSize = true;
            this.radio_CONV_OFF_H.Location = new System.Drawing.Point(3, 2);
            this.radio_CONV_OFF_H.Name = "radio_CONV_OFF_H";
            this.radio_CONV_OFF_H.Size = new System.Drawing.Size(17, 16);
            this.radio_CONV_OFF_H.TabIndex = 48;
            this.radio_CONV_OFF_H.TabStop = true;
            this.radio_CONV_OFF_H.Tag = "1";
            this.radio_CONV_OFF_H.UseVisualStyleBackColor = true;
            this.radio_CONV_OFF_H.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // radio_CONV_OFF_L
            // 
            this.radio_CONV_OFF_L.AutoSize = true;
            this.radio_CONV_OFF_L.Location = new System.Drawing.Point(44, 2);
            this.radio_CONV_OFF_L.Name = "radio_CONV_OFF_L";
            this.radio_CONV_OFF_L.Size = new System.Drawing.Size(17, 16);
            this.radio_CONV_OFF_L.TabIndex = 49;
            this.radio_CONV_OFF_L.TabStop = true;
            this.radio_CONV_OFF_L.Tag = "0";
            this.radio_CONV_OFF_L.UseVisualStyleBackColor = true;
            this.radio_CONV_OFF_L.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(30, 182);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(119, 23);
            this.label20.TabIndex = 10;
            this.label20.Text = "uC_CONV_ON";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.radio_Bat_En_Adapter_H);
            this.panel8.Controls.Add(this.radio_Bat_En_Adapter_L);
            this.panel8.Location = new System.Drawing.Point(230, 206);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(67, 18);
            this.panel8.TabIndex = 55;
            this.panel8.Tag = "43";
            // 
            // radio_Bat_En_Adapter_H
            // 
            this.radio_Bat_En_Adapter_H.AutoSize = true;
            this.radio_Bat_En_Adapter_H.Location = new System.Drawing.Point(3, 2);
            this.radio_Bat_En_Adapter_H.Name = "radio_Bat_En_Adapter_H";
            this.radio_Bat_En_Adapter_H.Size = new System.Drawing.Size(17, 16);
            this.radio_Bat_En_Adapter_H.TabIndex = 48;
            this.radio_Bat_En_Adapter_H.TabStop = true;
            this.radio_Bat_En_Adapter_H.Tag = "1";
            this.radio_Bat_En_Adapter_H.UseVisualStyleBackColor = true;
            this.radio_Bat_En_Adapter_H.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // radio_Bat_En_Adapter_L
            // 
            this.radio_Bat_En_Adapter_L.AutoSize = true;
            this.radio_Bat_En_Adapter_L.Location = new System.Drawing.Point(44, 2);
            this.radio_Bat_En_Adapter_L.Name = "radio_Bat_En_Adapter_L";
            this.radio_Bat_En_Adapter_L.Size = new System.Drawing.Size(17, 16);
            this.radio_Bat_En_Adapter_L.TabIndex = 49;
            this.radio_Bat_En_Adapter_L.TabStop = true;
            this.radio_Bat_En_Adapter_L.Tag = "0";
            this.radio_Bat_En_Adapter_L.UseVisualStyleBackColor = true;
            this.radio_Bat_En_Adapter_L.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(30, 230);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(157, 23);
            this.label18.TabIndex = 12;
            this.label18.Text = "uC_Bat_En_Battery";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.radio_Bat_En_Charging_H);
            this.panel9.Controls.Add(this.radio_Bat_En_Charging_L);
            this.panel9.Location = new System.Drawing.Point(230, 158);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(67, 18);
            this.panel9.TabIndex = 53;
            this.panel9.Tag = "55";
            // 
            // radio_Bat_En_Charging_H
            // 
            this.radio_Bat_En_Charging_H.AutoSize = true;
            this.radio_Bat_En_Charging_H.Location = new System.Drawing.Point(3, 2);
            this.radio_Bat_En_Charging_H.Name = "radio_Bat_En_Charging_H";
            this.radio_Bat_En_Charging_H.Size = new System.Drawing.Size(17, 16);
            this.radio_Bat_En_Charging_H.TabIndex = 48;
            this.radio_Bat_En_Charging_H.TabStop = true;
            this.radio_Bat_En_Charging_H.Tag = "1";
            this.radio_Bat_En_Charging_H.UseVisualStyleBackColor = true;
            this.radio_Bat_En_Charging_H.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // radio_Bat_En_Charging_L
            // 
            this.radio_Bat_En_Charging_L.AutoSize = true;
            this.radio_Bat_En_Charging_L.Location = new System.Drawing.Point(44, 2);
            this.radio_Bat_En_Charging_L.Name = "radio_Bat_En_Charging_L";
            this.radio_Bat_En_Charging_L.Size = new System.Drawing.Size(17, 16);
            this.radio_Bat_En_Charging_L.TabIndex = 49;
            this.radio_Bat_En_Charging_L.TabStop = true;
            this.radio_Bat_En_Charging_L.Tag = "0";
            this.radio_Bat_En_Charging_L.UseVisualStyleBackColor = true;
            this.radio_Bat_En_Charging_L.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(30, 302);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(80, 23);
            this.label25.TabIndex = 15;
            this.label25.Text = "uC_SCAir";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.radio_LED_driver_resetN_H);
            this.panel4.Controls.Add(this.radio_LED_driver_resetN_L);
            this.panel4.Location = new System.Drawing.Point(230, 134);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(67, 18);
            this.panel4.TabIndex = 52;
            this.panel4.Tag = "14";
            // 
            // radio_LED_driver_resetN_H
            // 
            this.radio_LED_driver_resetN_H.AutoSize = true;
            this.radio_LED_driver_resetN_H.Location = new System.Drawing.Point(3, 2);
            this.radio_LED_driver_resetN_H.Name = "radio_LED_driver_resetN_H";
            this.radio_LED_driver_resetN_H.Size = new System.Drawing.Size(17, 16);
            this.radio_LED_driver_resetN_H.TabIndex = 48;
            this.radio_LED_driver_resetN_H.TabStop = true;
            this.radio_LED_driver_resetN_H.Tag = "1";
            this.radio_LED_driver_resetN_H.UseVisualStyleBackColor = true;
            this.radio_LED_driver_resetN_H.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // radio_LED_driver_resetN_L
            // 
            this.radio_LED_driver_resetN_L.AutoSize = true;
            this.radio_LED_driver_resetN_L.Location = new System.Drawing.Point(44, 2);
            this.radio_LED_driver_resetN_L.Name = "radio_LED_driver_resetN_L";
            this.radio_LED_driver_resetN_L.Size = new System.Drawing.Size(17, 16);
            this.radio_LED_driver_resetN_L.TabIndex = 49;
            this.radio_LED_driver_resetN_L.TabStop = true;
            this.radio_LED_driver_resetN_L.Tag = "0";
            this.radio_LED_driver_resetN_L.UseVisualStyleBackColor = true;
            this.radio_LED_driver_resetN_L.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(30, 110);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(124, 23);
            this.label17.TabIndex = 7;
            this.label17.Text = "uC_LampPWM";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.radio_CPChargeFlashCap_H);
            this.panel3.Controls.Add(this.radio_CPChargeFlashCap_L);
            this.panel3.Location = new System.Drawing.Point(230, 86);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(67, 18);
            this.panel3.TabIndex = 50;
            this.panel3.Tag = "28";
            // 
            // radio_CPChargeFlashCap_H
            // 
            this.radio_CPChargeFlashCap_H.AutoSize = true;
            this.radio_CPChargeFlashCap_H.Location = new System.Drawing.Point(3, 2);
            this.radio_CPChargeFlashCap_H.Name = "radio_CPChargeFlashCap_H";
            this.radio_CPChargeFlashCap_H.Size = new System.Drawing.Size(17, 16);
            this.radio_CPChargeFlashCap_H.TabIndex = 48;
            this.radio_CPChargeFlashCap_H.TabStop = true;
            this.radio_CPChargeFlashCap_H.Tag = "1";
            this.radio_CPChargeFlashCap_H.UseVisualStyleBackColor = true;
            this.radio_CPChargeFlashCap_H.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // radio_CPChargeFlashCap_L
            // 
            this.radio_CPChargeFlashCap_L.AutoSize = true;
            this.radio_CPChargeFlashCap_L.Location = new System.Drawing.Point(44, 2);
            this.radio_CPChargeFlashCap_L.Name = "radio_CPChargeFlashCap_L";
            this.radio_CPChargeFlashCap_L.Size = new System.Drawing.Size(17, 16);
            this.radio_CPChargeFlashCap_L.TabIndex = 49;
            this.radio_CPChargeFlashCap_L.TabStop = true;
            this.radio_CPChargeFlashCap_L.Tag = "0";
            this.radio_CPChargeFlashCap_L.UseVisualStyleBackColor = true;
            this.radio_CPChargeFlashCap_L.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(30, 206);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(163, 23);
            this.label19.TabIndex = 11;
            this.label19.Text = "uC_Bat_En_Adapter";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.radio_LampPWM_H);
            this.panel5.Controls.Add(this.radio_LampPWM_L);
            this.panel5.Location = new System.Drawing.Point(230, 110);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(67, 18);
            this.panel5.TabIndex = 51;
            this.panel5.Tag = "24";
            // 
            // radio_LampPWM_H
            // 
            this.radio_LampPWM_H.AutoSize = true;
            this.radio_LampPWM_H.Location = new System.Drawing.Point(3, 2);
            this.radio_LampPWM_H.Name = "radio_LampPWM_H";
            this.radio_LampPWM_H.Size = new System.Drawing.Size(17, 16);
            this.radio_LampPWM_H.TabIndex = 48;
            this.radio_LampPWM_H.TabStop = true;
            this.radio_LampPWM_H.Tag = "1";
            this.radio_LampPWM_H.UseVisualStyleBackColor = true;
            this.radio_LampPWM_H.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // radio_LampPWM_L
            // 
            this.radio_LampPWM_L.AutoSize = true;
            this.radio_LampPWM_L.Location = new System.Drawing.Point(44, 2);
            this.radio_LampPWM_L.Name = "radio_LampPWM_L";
            this.radio_LampPWM_L.Size = new System.Drawing.Size(17, 16);
            this.radio_LampPWM_L.TabIndex = 49;
            this.radio_LampPWM_L.TabStop = true;
            this.radio_LampPWM_L.Tag = "0";
            this.radio_LampPWM_L.UseVisualStyleBackColor = true;
            this.radio_LampPWM_L.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(30, 326);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(96, 23);
            this.label23.TabIndex = 17;
            this.label23.Text = "uC_Cooling";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radio_CPDischarger_H);
            this.panel2.Controls.Add(this.radio_CPDischarger_L);
            this.panel2.Location = new System.Drawing.Point(230, 62);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(67, 18);
            this.panel2.TabIndex = 47;
            this.panel2.Tag = "36";
            // 
            // radio_CPDischarger_H
            // 
            this.radio_CPDischarger_H.AutoSize = true;
            this.radio_CPDischarger_H.Location = new System.Drawing.Point(3, 2);
            this.radio_CPDischarger_H.Name = "radio_CPDischarger_H";
            this.radio_CPDischarger_H.Size = new System.Drawing.Size(17, 16);
            this.radio_CPDischarger_H.TabIndex = 48;
            this.radio_CPDischarger_H.TabStop = true;
            this.radio_CPDischarger_H.Tag = "1";
            this.radio_CPDischarger_H.UseVisualStyleBackColor = true;
            this.radio_CPDischarger_H.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // radio_CPDischarger_L
            // 
            this.radio_CPDischarger_L.AutoSize = true;
            this.radio_CPDischarger_L.Location = new System.Drawing.Point(44, 2);
            this.radio_CPDischarger_L.Name = "radio_CPDischarger_L";
            this.radio_CPDischarger_L.Size = new System.Drawing.Size(17, 16);
            this.radio_CPDischarger_L.TabIndex = 49;
            this.radio_CPDischarger_L.TabStop = true;
            this.radio_CPDischarger_L.Tag = "0";
            this.radio_CPDischarger_L.UseVisualStyleBackColor = true;
            this.radio_CPDischarger_L.Click += new System.EventHandler(this.uC_Output_StateChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(30, 86);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(182, 23);
            this.label15.TabIndex = 6;
            this.label15.Text = "uC_CPChargeFlashCap";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(30, 350);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(168, 23);
            this.label22.TabIndex = 18;
            this.label22.Text = "uC_Attachement_EN";
            // 
            // btn_pullL
            // 
            this.btn_pullL.Location = new System.Drawing.Point(522, 61);
            this.btn_pullL.Name = "btn_pullL";
            this.btn_pullL.Size = new System.Drawing.Size(151, 33);
            this.btn_pullL.TabIndex = 4;
            this.btn_pullL.Text = "PULL LOW";
            this.btn_pullL.UseVisualStyleBackColor = true;
            this.btn_pullL.Click += new System.EventHandler(this.btn_pullL_click);
            // 
            // btn_pullH
            // 
            this.btn_pullH.Location = new System.Drawing.Point(351, 61);
            this.btn_pullH.Name = "btn_pullH";
            this.btn_pullH.Size = new System.Drawing.Size(151, 33);
            this.btn_pullH.TabIndex = 3;
            this.btn_pullH.Text = "PULL HIGH";
            this.btn_pullH.UseVisualStyleBackColor = true;
            this.btn_pullH.Click += new System.EventHandler(this.btn_pullH_click);
            // 
            // textB_enterpinN
            // 
            this.textB_enterpinN.Location = new System.Drawing.Point(234, 65);
            this.textB_enterpinN.Name = "textB_enterpinN";
            this.textB_enterpinN.Size = new System.Drawing.Size(244, 30);
            this.textB_enterpinN.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(40, 73);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(205, 23);
            this.label12.TabIndex = 1;
            this.label12.Text = "Enter Output Pin Number";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(43, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "Init GPIO";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btn_initgpio_click);
            // 
            // tabPageLED
            // 
            this.tabPageLED.Controls.Add(this.richText_FeasaA);
            this.tabPageLED.Controls.Add(this.button_FeasaA_Capture);
            this.tabPageLED.Controls.Add(this.button_Led_Config);
            this.tabPageLED.Controls.Add(this.panel21);
            this.tabPageLED.Controls.Add(this.panel20);
            this.tabPageLED.Controls.Add(this.panel19);
            this.tabPageLED.Controls.Add(this.panel18);
            this.tabPageLED.Controls.Add(this.panel16);
            this.tabPageLED.Controls.Add(this.label41);
            this.tabPageLED.Controls.Add(this.label40);
            this.tabPageLED.Controls.Add(this.panel15);
            this.tabPageLED.Controls.Add(this.label39);
            this.tabPageLED.Controls.Add(this.label38);
            this.tabPageLED.Controls.Add(this.label37);
            this.tabPageLED.Controls.Add(this.label36);
            this.tabPageLED.Controls.Add(this.label11);
            this.tabPageLED.Controls.Add(this.label7);
            this.tabPageLED.Controls.Add(this.label6);
            this.tabPageLED.Location = new System.Drawing.Point(4, 32);
            this.tabPageLED.Name = "tabPageLED";
            this.tabPageLED.Size = new System.Drawing.Size(2843, 1407);
            this.tabPageLED.TabIndex = 5;
            this.tabPageLED.Text = "LED";
            this.tabPageLED.UseVisualStyleBackColor = true;
            // 
            // richText_FeasaA
            // 
            this.richText_FeasaA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richText_FeasaA.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richText_FeasaA.Location = new System.Drawing.Point(367, 72);
            this.richText_FeasaA.Name = "richText_FeasaA";
            this.richText_FeasaA.Size = new System.Drawing.Size(297, 257);
            this.richText_FeasaA.TabIndex = 61;
            this.richText_FeasaA.Text = "";
            // 
            // button_FeasaA_Capture
            // 
            this.button_FeasaA_Capture.Location = new System.Drawing.Point(367, 31);
            this.button_FeasaA_Capture.Name = "button_FeasaA_Capture";
            this.button_FeasaA_Capture.Size = new System.Drawing.Size(138, 28);
            this.button_FeasaA_Capture.TabIndex = 59;
            this.button_FeasaA_Capture.Text = "Feasa A Capture";
            this.button_FeasaA_Capture.UseVisualStyleBackColor = true;
            this.button_FeasaA_Capture.Click += new System.EventHandler(this.button_FeasaA_Capture_Click);
            // 
            // button_Led_Config
            // 
            this.button_Led_Config.Location = new System.Drawing.Point(202, 101);
            this.button_Led_Config.Name = "button_Led_Config";
            this.button_Led_Config.Size = new System.Drawing.Size(81, 28);
            this.button_Led_Config.TabIndex = 58;
            this.button_Led_Config.Text = "Run";
            this.button_Led_Config.UseVisualStyleBackColor = true;
            this.button_Led_Config.Click += new System.EventHandler(this.button_Led_Config_Click);
            // 
            // panel21
            // 
            this.panel21.Controls.Add(this.radio_BOLed_Off);
            this.panel21.Controls.Add(this.radio_BOLed_On);
            this.panel21.Location = new System.Drawing.Point(203, 214);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(79, 18);
            this.panel21.TabIndex = 50;
            this.panel21.Tag = "36";
            // 
            // radio_BOLed_Off
            // 
            this.radio_BOLed_Off.AutoSize = true;
            this.radio_BOLed_Off.Checked = true;
            this.radio_BOLed_Off.Location = new System.Drawing.Point(11, 2);
            this.radio_BOLed_Off.Name = "radio_BOLed_Off";
            this.radio_BOLed_Off.Size = new System.Drawing.Size(17, 16);
            this.radio_BOLed_Off.TabIndex = 48;
            this.radio_BOLed_Off.TabStop = true;
            this.radio_BOLed_Off.Tag = "0";
            this.radio_BOLed_Off.UseVisualStyleBackColor = true;
            this.radio_BOLed_Off.Click += new System.EventHandler(this.radio_BOLed_StateChanged);
            // 
            // radio_BOLed_On
            // 
            this.radio_BOLed_On.AutoSize = true;
            this.radio_BOLed_On.Location = new System.Drawing.Point(56, 2);
            this.radio_BOLed_On.Name = "radio_BOLed_On";
            this.radio_BOLed_On.Size = new System.Drawing.Size(17, 16);
            this.radio_BOLed_On.TabIndex = 49;
            this.radio_BOLed_On.Tag = "1";
            this.radio_BOLed_On.UseVisualStyleBackColor = true;
            this.radio_BOLed_On.Click += new System.EventHandler(this.radio_BOLed_StateChanged);
            // 
            // panel20
            // 
            this.panel20.Controls.Add(this.radio_BWLed_Off);
            this.panel20.Controls.Add(this.radio_BWLed_On);
            this.panel20.Location = new System.Drawing.Point(203, 188);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(79, 18);
            this.panel20.TabIndex = 50;
            this.panel20.Tag = "36";
            // 
            // radio_BWLed_Off
            // 
            this.radio_BWLed_Off.AutoSize = true;
            this.radio_BWLed_Off.Checked = true;
            this.radio_BWLed_Off.Location = new System.Drawing.Point(11, 2);
            this.radio_BWLed_Off.Name = "radio_BWLed_Off";
            this.radio_BWLed_Off.Size = new System.Drawing.Size(17, 16);
            this.radio_BWLed_Off.TabIndex = 48;
            this.radio_BWLed_Off.TabStop = true;
            this.radio_BWLed_Off.Tag = "0";
            this.radio_BWLed_Off.UseVisualStyleBackColor = true;
            this.radio_BWLed_Off.Click += new System.EventHandler(this.radio_BWLed_StateChanged);
            // 
            // radio_BWLed_On
            // 
            this.radio_BWLed_On.AutoSize = true;
            this.radio_BWLed_On.Location = new System.Drawing.Point(56, 2);
            this.radio_BWLed_On.Name = "radio_BWLed_On";
            this.radio_BWLed_On.Size = new System.Drawing.Size(17, 16);
            this.radio_BWLed_On.TabIndex = 49;
            this.radio_BWLed_On.Tag = "1";
            this.radio_BWLed_On.UseVisualStyleBackColor = true;
            this.radio_BWLed_On.Click += new System.EventHandler(this.radio_BWLed_StateChanged);
            // 
            // panel19
            // 
            this.panel19.Controls.Add(this.radio_TOLed_Off);
            this.panel19.Controls.Add(this.radio_TOLed_On);
            this.panel19.Location = new System.Drawing.Point(203, 162);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(79, 18);
            this.panel19.TabIndex = 50;
            this.panel19.Tag = "36";
            // 
            // radio_TOLed_Off
            // 
            this.radio_TOLed_Off.AutoSize = true;
            this.radio_TOLed_Off.Checked = true;
            this.radio_TOLed_Off.Location = new System.Drawing.Point(11, 2);
            this.radio_TOLed_Off.Name = "radio_TOLed_Off";
            this.radio_TOLed_Off.Size = new System.Drawing.Size(17, 16);
            this.radio_TOLed_Off.TabIndex = 48;
            this.radio_TOLed_Off.TabStop = true;
            this.radio_TOLed_Off.Tag = "0";
            this.radio_TOLed_Off.UseVisualStyleBackColor = true;
            this.radio_TOLed_Off.Click += new System.EventHandler(this.radio_TOLed_StateChanged);
            // 
            // radio_TOLed_On
            // 
            this.radio_TOLed_On.AutoSize = true;
            this.radio_TOLed_On.Location = new System.Drawing.Point(56, 2);
            this.radio_TOLed_On.Name = "radio_TOLed_On";
            this.radio_TOLed_On.Size = new System.Drawing.Size(17, 16);
            this.radio_TOLed_On.TabIndex = 49;
            this.radio_TOLed_On.Tag = "1";
            this.radio_TOLed_On.UseVisualStyleBackColor = true;
            this.radio_TOLed_On.Click += new System.EventHandler(this.radio_TOLed_StateChanged);
            // 
            // panel18
            // 
            this.panel18.Controls.Add(this.radio_TWLed_Off);
            this.panel18.Controls.Add(this.radio_TWLed_On);
            this.panel18.Location = new System.Drawing.Point(203, 136);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(79, 18);
            this.panel18.TabIndex = 50;
            this.panel18.Tag = "36";
            // 
            // radio_TWLed_Off
            // 
            this.radio_TWLed_Off.AutoSize = true;
            this.radio_TWLed_Off.Checked = true;
            this.radio_TWLed_Off.Location = new System.Drawing.Point(11, 2);
            this.radio_TWLed_Off.Name = "radio_TWLed_Off";
            this.radio_TWLed_Off.Size = new System.Drawing.Size(17, 16);
            this.radio_TWLed_Off.TabIndex = 48;
            this.radio_TWLed_Off.TabStop = true;
            this.radio_TWLed_Off.Tag = "0";
            this.radio_TWLed_Off.UseVisualStyleBackColor = true;
            this.radio_TWLed_Off.Click += new System.EventHandler(this.radio_TWLed_StateChanged);
            // 
            // radio_TWLed_On
            // 
            this.radio_TWLed_On.AutoSize = true;
            this.radio_TWLed_On.Location = new System.Drawing.Point(56, 2);
            this.radio_TWLed_On.Name = "radio_TWLed_On";
            this.radio_TWLed_On.Size = new System.Drawing.Size(17, 16);
            this.radio_TWLed_On.TabIndex = 49;
            this.radio_TWLed_On.Tag = "1";
            this.radio_TWLed_On.UseVisualStyleBackColor = true;
            this.radio_TWLed_On.Click += new System.EventHandler(this.radio_TWLed_StateChanged);
            // 
            // panel16
            // 
            this.panel16.Controls.Add(this.radio_3v3_Off);
            this.panel16.Controls.Add(this.radio_3v3_On);
            this.panel16.Location = new System.Drawing.Point(203, 76);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(79, 18);
            this.panel16.TabIndex = 57;
            this.panel16.Tag = "36";
            // 
            // radio_3v3_Off
            // 
            this.radio_3v3_Off.AutoSize = true;
            this.radio_3v3_Off.Checked = true;
            this.radio_3v3_Off.Location = new System.Drawing.Point(11, 2);
            this.radio_3v3_Off.Name = "radio_3v3_Off";
            this.radio_3v3_Off.Size = new System.Drawing.Size(17, 16);
            this.radio_3v3_Off.TabIndex = 48;
            this.radio_3v3_Off.TabStop = true;
            this.radio_3v3_Off.Tag = "0";
            this.radio_3v3_Off.UseVisualStyleBackColor = true;
            this.radio_3v3_Off.Click += new System.EventHandler(this.radio_3v3_StateChanged);
            // 
            // radio_3v3_On
            // 
            this.radio_3v3_On.AutoSize = true;
            this.radio_3v3_On.Location = new System.Drawing.Point(56, 2);
            this.radio_3v3_On.Name = "radio_3v3_On";
            this.radio_3v3_On.Size = new System.Drawing.Size(17, 16);
            this.radio_3v3_On.TabIndex = 49;
            this.radio_3v3_On.Tag = "1";
            this.radio_3v3_On.UseVisualStyleBackColor = true;
            this.radio_3v3_On.Click += new System.EventHandler(this.radio_3v3_StateChanged);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(251, 31);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(35, 23);
            this.label41.TabIndex = 56;
            this.label41.Text = "ON";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(204, 31);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(41, 23);
            this.label40.TabIndex = 55;
            this.label40.Text = "OFF";
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.radio_19v5_Off);
            this.panel15.Controls.Add(this.radio_19v5_On);
            this.panel15.Location = new System.Drawing.Point(203, 50);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(79, 18);
            this.panel15.TabIndex = 48;
            this.panel15.Tag = "36";
            // 
            // radio_19v5_Off
            // 
            this.radio_19v5_Off.AutoSize = true;
            this.radio_19v5_Off.Checked = true;
            this.radio_19v5_Off.Location = new System.Drawing.Point(11, 2);
            this.radio_19v5_Off.Name = "radio_19v5_Off";
            this.radio_19v5_Off.Size = new System.Drawing.Size(17, 16);
            this.radio_19v5_Off.TabIndex = 48;
            this.radio_19v5_Off.TabStop = true;
            this.radio_19v5_Off.Tag = "0";
            this.radio_19v5_Off.UseVisualStyleBackColor = true;
            this.radio_19v5_Off.Click += new System.EventHandler(this.radio_19v5_StateChanged);
            // 
            // radio_19v5_On
            // 
            this.radio_19v5_On.AutoSize = true;
            this.radio_19v5_On.Location = new System.Drawing.Point(56, 2);
            this.radio_19v5_On.Name = "radio_19v5_On";
            this.radio_19v5_On.Size = new System.Drawing.Size(17, 16);
            this.radio_19v5_On.TabIndex = 49;
            this.radio_19v5_On.Tag = "1";
            this.radio_19v5_On.UseVisualStyleBackColor = true;
            this.radio_19v5_On.Click += new System.EventHandler(this.radio_19v5_StateChanged);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(56, 214);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(161, 23);
            this.label39.TabIndex = 6;
            this.label39.Text = "Bottom Orange LED";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(56, 188);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(150, 23);
            this.label38.TabIndex = 5;
            this.label38.Text = "Bottom White LED";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(56, 162);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(131, 23);
            this.label37.TabIndex = 4;
            this.label37.Text = "Top Orange LED";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(56, 136);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(120, 23);
            this.label36.TabIndex = 3;
            this.label36.Text = "Top White LED";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(56, 106);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(119, 23);
            this.label11.TabIndex = 2;
            this.label11.Text = "DUT IO Config";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(56, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 23);
            this.label7.TabIndex = 1;
            this.label7.Text = "Board 3.3V";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 23);
            this.label6.TabIndex = 0;
            this.label6.Text = "19.5V";
            // 
            // tabPageSystem
            // 
            this.tabPageSystem.Controls.Add(this.groupBox3);
            this.tabPageSystem.Controls.Add(this.groupBox2);
            this.tabPageSystem.Location = new System.Drawing.Point(4, 32);
            this.tabPageSystem.Name = "tabPageSystem";
            this.tabPageSystem.Size = new System.Drawing.Size(2843, 1407);
            this.tabPageSystem.TabIndex = 6;
            this.tabPageSystem.Text = "System";
            this.tabPageSystem.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gridViewLib);
            this.groupBox3.Location = new System.Drawing.Point(19, 289);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(576, 256);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Libraries";
            // 
            // gridViewLib
            // 
            this.gridViewLib.AllowUserToAddRows = false;
            this.gridViewLib.AllowUserToDeleteRows = false;
            this.gridViewLib.AllowUserToResizeColumns = false;
            this.gridViewLib.AllowUserToResizeRows = false;
            this.gridViewLib.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewLib.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.gridViewLib.Location = new System.Drawing.Point(16, 25);
            this.gridViewLib.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridViewLib.Name = "gridViewLib";
            this.gridViewLib.ReadOnly = true;
            this.gridViewLib.RowHeadersVisible = false;
            this.gridViewLib.RowHeadersWidth = 51;
            this.gridViewLib.RowTemplate.Height = 24;
            this.gridViewLib.Size = new System.Drawing.Size(639, 181);
            this.gridViewLib.TabIndex = 0;
            this.gridViewLib.TabStop = false;
            this.gridViewLib.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gridViewLib_CellPainting);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Category";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 125;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "File Name";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 240;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Cal. CRC";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 125;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Exp. CRC";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 125;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridViewFW);
            this.groupBox2.Location = new System.Drawing.Point(19, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(877, 144);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FW / SW";
            // 
            // gridViewFW
            // 
            this.gridViewFW.AllowUserToAddRows = false;
            this.gridViewFW.AllowUserToDeleteRows = false;
            this.gridViewFW.AllowUserToResizeColumns = false;
            this.gridViewFW.AllowUserToResizeRows = false;
            this.gridViewFW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewFW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCategory,
            this.ColumnFile,
            this.ColumnSfisName,
            this.ColumnCalCRC,
            this.ColumnSfisCRC});
            this.gridViewFW.Location = new System.Drawing.Point(16, 25);
            this.gridViewFW.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridViewFW.Name = "gridViewFW";
            this.gridViewFW.ReadOnly = true;
            this.gridViewFW.RowHeadersVisible = false;
            this.gridViewFW.RowHeadersWidth = 200;
            this.gridViewFW.RowTemplate.Height = 24;
            this.gridViewFW.Size = new System.Drawing.Size(835, 100);
            this.gridViewFW.TabIndex = 0;
            this.gridViewFW.TabStop = false;
            this.gridViewFW.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gridViewFW_CellPainting);
            // 
            // ColumnCategory
            // 
            this.ColumnCategory.HeaderText = "Category";
            this.ColumnCategory.MinimumWidth = 6;
            this.ColumnCategory.Name = "ColumnCategory";
            this.ColumnCategory.ReadOnly = true;
            this.ColumnCategory.Width = 125;
            // 
            // ColumnFile
            // 
            this.ColumnFile.HeaderText = "File Name";
            this.ColumnFile.MinimumWidth = 6;
            this.ColumnFile.Name = "ColumnFile";
            this.ColumnFile.ReadOnly = true;
            this.ColumnFile.Width = 240;
            // 
            // ColumnSfisName
            // 
            this.ColumnSfisName.HeaderText = "SFIS Name";
            this.ColumnSfisName.MinimumWidth = 6;
            this.ColumnSfisName.Name = "ColumnSfisName";
            this.ColumnSfisName.ReadOnly = true;
            this.ColumnSfisName.Width = 200;
            // 
            // ColumnCalCRC
            // 
            this.ColumnCalCRC.HeaderText = "Cal. CRC";
            this.ColumnCalCRC.MinimumWidth = 6;
            this.ColumnCalCRC.Name = "ColumnCalCRC";
            this.ColumnCalCRC.ReadOnly = true;
            this.ColumnCalCRC.Width = 125;
            // 
            // ColumnSfisCRC
            // 
            this.ColumnSfisCRC.HeaderText = "SFIS CRC";
            this.ColumnSfisCRC.MinimumWidth = 6;
            this.ColumnSfisCRC.Name = "ColumnSfisCRC";
            this.ColumnSfisCRC.ReadOnly = true;
            this.ColumnSfisCRC.Width = 125;
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(1045, 14);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(139, 91);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // labelPn
            // 
            this.labelPn.Font = new System.Drawing.Font("Calibri", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPn.Location = new System.Drawing.Point(739, 14);
            this.labelPn.Name = "labelPn";
            this.labelPn.Size = new System.Drawing.Size(293, 39);
            this.labelPn.TabIndex = 8;
            this.labelPn.Text = "XXXX-XXXXXX-XX";
            this.labelPn.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 29);
            this.label2.TabIndex = 9;
            this.label2.Text = "Fixture ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(362, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 29);
            this.label3.TabIndex = 10;
            this.label3.Text = "EMP ID";
            // 
            // textFixture
            // 
            this.textFixture.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textFixture.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFixture.Location = new System.Drawing.Point(106, 14);
            this.textFixture.Name = "textFixture";
            this.textFixture.Size = new System.Drawing.Size(218, 36);
            this.textFixture.TabIndex = 0;
            this.textFixture.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textFixture_KeyPress);
            // 
            // textEmp
            // 
            this.textEmp.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textEmp.Enabled = false;
            this.textEmp.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEmp.Location = new System.Drawing.Point(435, 14);
            this.textEmp.Name = "textEmp";
            this.textEmp.Size = new System.Drawing.Size(159, 36);
            this.textEmp.TabIndex = 1;
            this.textEmp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textEmp_KeyPress);
            // 
            // textSn
            // 
            this.textSn.BackColor = System.Drawing.SystemColors.Window;
            this.textSn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textSn.Enabled = false;
            this.textSn.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textSn.Location = new System.Drawing.Point(106, 51);
            this.textSn.Name = "textSn";
            this.textSn.Size = new System.Drawing.Size(488, 36);
            this.textSn.TabIndex = 3;
            this.textSn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textSn_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 29);
            this.label4.TabIndex = 9;
            this.label4.Text = "Serial No.";
            // 
            // labelCounter
            // 
            this.labelCounter.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCounter.ForeColor = System.Drawing.Color.Blue;
            this.labelCounter.Location = new System.Drawing.Point(813, 78);
            this.labelCounter.Name = "labelCounter";
            this.labelCounter.Size = new System.Drawing.Size(217, 23);
            this.labelCounter.TabIndex = 11;
            this.labelCounter.Text = "0 / 10000";
            this.labelCounter.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.labelCounter.TextChanged += new System.EventHandler(this.labelCounter_TextChanged);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Pink;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(600, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 76);
            this.label5.TabIndex = 20;
            this.label5.Text = "Debug";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.Visible = false;
            // 
            // ColumnStep
            // 
            this.ColumnStep.HeaderText = "Step";
            this.ColumnStep.MinimumWidth = 6;
            this.ColumnStep.Name = "ColumnStep";
            this.ColumnStep.ReadOnly = true;
            this.ColumnStep.Width = 80;
            // 
            // ColumnTest
            // 
            this.ColumnTest.HeaderText = "Test";
            this.ColumnTest.MinimumWidth = 6;
            this.ColumnTest.Name = "ColumnTest";
            this.ColumnTest.ReadOnly = true;
            this.ColumnTest.Width = 320;
            // 
            // ColumnMeasured
            // 
            this.ColumnMeasured.HeaderText = "Measured";
            this.ColumnMeasured.MinimumWidth = 6;
            this.ColumnMeasured.Name = "ColumnMeasured";
            this.ColumnMeasured.ReadOnly = true;
            this.ColumnMeasured.Width = 230;
            // 
            // ColumnOffset
            // 
            this.ColumnOffset.HeaderText = "Offset";
            this.ColumnOffset.MinimumWidth = 6;
            this.ColumnOffset.Name = "ColumnOffset";
            this.ColumnOffset.ReadOnly = true;
            this.ColumnOffset.Width = 10;
            // 
            // ColumnLL
            // 
            this.ColumnLL.HeaderText = "LL";
            this.ColumnLL.MinimumWidth = 6;
            this.ColumnLL.Name = "ColumnLL";
            this.ColumnLL.ReadOnly = true;
            this.ColumnLL.Width = 80;
            // 
            // ColumnUL
            // 
            this.ColumnUL.HeaderText = "UL";
            this.ColumnUL.MinimumWidth = 6;
            this.ColumnUL.Name = "ColumnUL";
            this.ColumnUL.ReadOnly = true;
            this.ColumnUL.Width = 80;
            // 
            // ColumnErrorCode
            // 
            this.ColumnErrorCode.HeaderText = "Error Code";
            this.ColumnErrorCode.MinimumWidth = 6;
            this.ColumnErrorCode.Name = "ColumnErrorCode";
            this.ColumnErrorCode.ReadOnly = true;
            this.ColumnErrorCode.Width = 125;
            // 
            // ColumnStatus
            // 
            this.ColumnStatus.HeaderText = "Status";
            this.ColumnStatus.MinimumWidth = 6;
            this.ColumnStatus.Name = "ColumnStatus";
            this.ColumnStatus.ReadOnly = true;
            this.ColumnStatus.Width = 70;
            // 
            // PhilipsEmeraldTestTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 674);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelCounter);
            this.Controls.Add(this.textEmp);
            this.Controls.Add(this.textSn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textFixture);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.labelPn);
            this.Controls.Add(this.tabControlPattern);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PhilipsEmeraldTestTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Philips Emerald Main Tester";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.PhilipsEmeraldTestTool_Load);
            this.tabControlPattern.ResumeLayout(false);
            this.tabPageAll.ResumeLayout(false);
            this.tabPageAll.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTest)).EndInit();
            this.tabPageVISA.ResumeLayout(false);
            this.tabPageVISA.PerformLayout();
            this.groupBoxDisplay.ResumeLayout(false);
            this.groupBoxDisplay.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeout)).EndInit();
            this.tabPageGPIO.ResumeLayout(false);
            this.tabPageGPIO.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPageLED.ResumeLayout(false);
            this.tabPageLED.PerformLayout();
            this.panel21.ResumeLayout(false);
            this.panel21.PerformLayout();
            this.panel20.ResumeLayout(false);
            this.panel20.PerformLayout();
            this.panel19.ResumeLayout(false);
            this.panel19.PerformLayout();
            this.panel18.ResumeLayout(false);
            this.panel18.PerformLayout();
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.tabPageSystem.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLib)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFW)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker workerTestProccess;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TabControl tabControlPattern;
        private System.Windows.Forms.TabPage tabPageAll;
        private System.Windows.Forms.DataGridView gridViewTest;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label labelPn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textEmp;
        private System.Windows.Forms.TextBox textFixture;
        private System.Windows.Forms.TextBox textSn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelPoccess;
        private System.Windows.Forms.Label labelSfisInfo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelSfisResult;
        private System.Windows.Forms.RichTextBox richTextSfis;
        private System.Windows.Forms.Label labelTestResult;
        private System.Windows.Forms.TabPage tabPageVISA;
        private System.Windows.Forms.ComboBox cboGPIB;
        private System.Windows.Forms.RadioButton rbtGPIB;
        private System.Windows.Forms.ComboBox cboBaudRate;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.GroupBox groupBoxDisplay;
        private System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label lblCommand;
        private System.Windows.Forms.ComboBox cboCommand;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label lblOverTime;
        private System.Windows.Forms.NumericUpDown nudTimeout;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.Label lblStopBits;
        private System.Windows.Forms.ComboBox cboStopBits;
        private System.Windows.Forms.Label lblDataBits;
        private System.Windows.Forms.ComboBox cboDataBits;
        private System.Windows.Forms.Label lblFlowControl;
        private System.Windows.Forms.ComboBox cboFlowControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboParity;
        private System.Windows.Forms.Label lblBaudRate;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.RadioButton rbtRS232;
        private System.Windows.Forms.ComboBox cboUSB;
        private System.Windows.Forms.ComboBox cboRS232;
        private System.Windows.Forms.RadioButton rbtUSB;
        private System.Windows.Forms.Label labelCounter;
        private System.Windows.Forms.Button btn_DR2003_OnOff;
        private System.Windows.Forms.Label lab_DR2003_OnOff;
        private System.Windows.Forms.TabPage tabPageGPIO;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_pullL;
        private System.Windows.Forms.Button btn_pullH;
        private System.Windows.Forms.TextBox textB_enterpinN;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.RadioButton radio_Attachement_EN_H;
        private System.Windows.Forms.RadioButton radio_Attachement_EN_L;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.RadioButton radio_Cooling_H;
        private System.Windows.Forms.RadioButton radio_Cooling_L;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.RadioButton radio_SCAred_H;
        private System.Windows.Forms.RadioButton radio_SCAred_L;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.RadioButton radio_SCAir_H;
        private System.Windows.Forms.RadioButton radio_SCAir_L;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.RadioButton radio_Awake_H;
        private System.Windows.Forms.RadioButton radio_Awake_L;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.RadioButton radio_Bat_En_Battery_H;
        private System.Windows.Forms.RadioButton radio_Bat_En_Battery_L;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.RadioButton radio_CONV_OFF_H;
        private System.Windows.Forms.RadioButton radio_CONV_OFF_L;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.RadioButton radio_Bat_En_Adapter_H;
        private System.Windows.Forms.RadioButton radio_Bat_En_Adapter_L;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.RadioButton radio_Bat_En_Charging_H;
        private System.Windows.Forms.RadioButton radio_Bat_En_Charging_L;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton radio_LED_driver_resetN_H;
        private System.Windows.Forms.RadioButton radio_LED_driver_resetN_L;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton radio_CPChargeFlashCap_H;
        private System.Windows.Forms.RadioButton radio_CPChargeFlashCap_L;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton radio_LampPWM_H;
        private System.Windows.Forms.RadioButton radio_LampPWM_L;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radio_CPDischarger_H;
        private System.Windows.Forms.RadioButton radio_CPDischarger_L;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelGloden;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelPFGolden;
        private System.Windows.Forms.Label labelLastSn;
        private System.Windows.Forms.TabPage tabPageLED;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.RadioButton radio_19v5_Off;
        private System.Windows.Forms.RadioButton radio_19v5_On;
        private System.Windows.Forms.RichTextBox richText_FeasaA;
        private System.Windows.Forms.Button button_FeasaA_Capture;
        private System.Windows.Forms.Button button_Led_Config;
        private System.Windows.Forms.Panel panel21;
        private System.Windows.Forms.RadioButton radio_BOLed_Off;
        private System.Windows.Forms.RadioButton radio_BOLed_On;
        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.RadioButton radio_BWLed_Off;
        private System.Windows.Forms.RadioButton radio_BWLed_On;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.RadioButton radio_TOLed_Off;
        private System.Windows.Forms.RadioButton radio_TOLed_On;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.RadioButton radio_TWLed_Off;
        private System.Windows.Forms.RadioButton radio_TWLed_On;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.RadioButton radio_3v3_Off;
        private System.Windows.Forms.RadioButton radio_3v3_On;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.TabPage tabPageSystem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView gridViewLib;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.TextBox textBox_readValue;
        private System.Windows.Forms.TextBox textBox_enterInput;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView gridViewFW;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSfisName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCalCRC;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSfisCRC;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStep;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTest;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMeasured;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOffset;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnErrorCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStatus;
    }
}

