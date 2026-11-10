using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;
 
    class TestItemStep021_P1 : TestItemForm
    {
        //[DllImport("feasacom64.dll")]
        //private static extern int FeasaCom_Close(int SerialNumber);
        //private Feasa_LED myFS_LED1;
        //private Feasa_LED myFS_LED2;
        private DAQadc taskADC;
        private DAQadc taskADC1;
        private DAQadc taskADC2;
        private DAQadc taskADC3;
        private DAQadc taskADC4;
        private DAQadc[] taskList;
        //private List<DAQadc> taskList = new List<DAQadc>();
        private double[] NIavg;
        private int taskind;
        public string step = "_021 P1";

        public TestItemStep021_P1()
        {
            sStep = step;
            LoadSpec();
        }
        private void LED_ON()
        {
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UIstandby, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet1, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet2, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet3, 1);// on
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet4, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet5, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedError, 1);//organge
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedRTF, 1); //white

        }
        private void Init_Feasa()
        {
            Feasa_10F_A.ch1_D630o = new XY_Data();
            Feasa_10F_A.ch2_D631o = new XY_Data();
            Feasa_10F_A.ch3_D500w = new XY_Data();
            Feasa_10F_A.ch4_D510w = new XY_Data();
            Feasa_10F_A.ch5_D501w = new XY_Data();
            Feasa_10F_A.ch6_D612o = new XY_Data();
            Feasa_10F_A.ch7_D502w = new XY_Data();
            Feasa_10F_A.ch8_D511w = new XY_Data();

            Feasa_10F_B.ch1_D625w = new XY_Data();
            Feasa_10F_B.ch2_D624w = new XY_Data();
            Feasa_10F_B.ch3_D621w = new XY_Data();
            Feasa_10F_B.ch4_D622w = new XY_Data();
            Feasa_10F_B.ch5_D623w = new XY_Data();
            Feasa_10F_B.ch6_D626w = new XY_Data();
            Feasa_10F_B.ch7_D627w = new XY_Data();
            Feasa_10F_B.ch8_D628w = new XY_Data();
            Feasa_10F_B.ch9_D629w = new XY_Data();
        }
        private void Init_taskADC()
        {
            taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);
            taskADC.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDSet1_I2_AI_1);//
            taskADC.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDSet2_I3_AI_2);//
            taskADC.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDSet3_I4_AI_3);//
            taskADC.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDSet4_I5_AI_4);
            taskADC.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDSet5_I6_AI_5);//
        }
        private void Init_taskADC1()
        {
            taskADC1 = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);
            taskADC1.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDSTD_STR1_I7_AI_6);//
            taskADC1.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDSTD_STR2_I8_AI_7);//
        }
        private void Init_taskADC2()
        {
            taskADC2 = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);
            taskADC2.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDRTF_STR1_I9_AI_8);//
            taskADC2.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDRTF_STR2_I10_AI_9);//
            taskADC2.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDRTF_STR3_I11_AI_10);//
            //taskADC2.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDSTB_I12_AI_11);//
        }
        private void Init_taskADC3()
        {
            taskADC3 = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);
            taskADC3.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDSTB_I12_AI_11);//
            taskADC3.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_UI_Standby_I13_AI_12);//
            
        }
        private void Init_taskADC4()
        {
            taskADC4 = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);
            //taskADC3.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDSTB_I12_AI_11);//
            //taskADC4.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_UI_Standby_I13_AI_12);//
            taskADC4.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_UISet1_I14_AI_13);//
            taskADC4.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_UISet2_I15_AI_14);//
            taskADC4.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_UISet3_I16_AI_15);//
            taskADC4.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_UISet4_I17_AI_16);//
            taskADC4.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_UISet5_I18_AI_17);//
            taskADC4.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_UILedError_I19_AI_18);//
            taskADC4.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_UILedRTF_I20_AI_19);//
        }
        public override WORK_STATUS Init()
        {
            LED_ON();
            //Init_Feasa();
            Init_taskADC();
            Init_taskADC1();
            Init_taskADC2();
            Init_taskADC3();
            Init_taskADC4();
            taskList = new DAQadc[5];
            
            taskList[0] = taskADC;
            taskList[1] = taskADC1;
            taskList[2] = taskADC2;
            taskList[3] = taskADC3;
            taskList[4] = taskADC4;
            
            try
            {
                //myFS_LED1 = new Feasa_LED(Feasa_10F_A.comPort);
                //myFS_LED2 = new Feasa_LED(Feasa_10F_B.comPort);

                NIavg = new double[19];

                //myFS_LED1.doC3Capture();
                //myFS_LED2.doC3Capture();

                //myFS_LED1.ReadXY(1, Feasa_10F_A.ch1_D630o);
                //myFS_LED1.ReadXY(2, Feasa_10F_A.ch2_D631o);
                //myFS_LED1.ReadXY(3, Feasa_10F_A.ch3_D500w);
                //myFS_LED1.ReadXY(4, Feasa_10F_A.ch4_D510w);
                //myFS_LED1.ReadXY(5, Feasa_10F_A.ch5_D501w);
                //myFS_LED1.ReadXY(6, Feasa_10F_A.ch6_D612o);
                //myFS_LED1.ReadXY(7, Feasa_10F_A.ch7_D502w);
                //myFS_LED1.ReadXY(8, Feasa_10F_A.ch8_D511w);

                //Console.WriteLine("ch1_D630o = " + Feasa_10F_A.ch1_D630o.val_I_XY + " " 
                //    + Feasa_10F_A.ch1_D630o.val_X + " " + Feasa_10F_A.ch1_D630o.val_Y);
                //Console.WriteLine("ch2_D631o = " + Feasa_10F_A.ch2_D631o.val_I_XY + " " 
                //    + Feasa_10F_A.ch2_D631o.val_X + " " + Feasa_10F_A.ch2_D631o.val_Y);
                //Console.WriteLine("ch3_D500w = " + Feasa_10F_A.ch3_D500w.val_I_XY + " " 
                //    + Feasa_10F_A.ch3_D500w.val_X + " " + Feasa_10F_A.ch3_D500w.val_Y);
                //Console.WriteLine("ch4_D510w = " + Feasa_10F_A.ch4_D510w.val_I_XY + " " 
                //    + Feasa_10F_A.ch4_D510w.val_X + " " + Feasa_10F_A.ch4_D510w.val_Y);
                //Console.WriteLine("ch5_D501w = " + Feasa_10F_A.ch5_D501w.val_I_XY + " " 
                //    + Feasa_10F_A.ch5_D501w.val_X + " " + Feasa_10F_A.ch5_D501w.val_Y);
                //Console.WriteLine("ch6_D612o = " + Feasa_10F_A.ch6_D612o.val_I_XY + " " 
                //    + Feasa_10F_A.ch6_D612o.val_X + " " + Feasa_10F_A.ch6_D612o.val_Y);
                //Console.WriteLine("ch7_D502w = " + Feasa_10F_A.ch7_D502w.val_I_XY + " " 
                //    + Feasa_10F_A.ch7_D502w.val_X + " " + Feasa_10F_A.ch7_D502w.val_Y);
                //Console.WriteLine("ch8_D511w = " + Feasa_10F_A.ch8_D511w.val_I_XY + " " 
                //    + Feasa_10F_A.ch8_D511w.val_X + " " + Feasa_10F_A.ch8_D511w.val_Y);


                //myFS_LED2.ReadXY(1, Feasa_10F_B.ch1_D625w);
                //myFS_LED2.ReadXY(2, Feasa_10F_B.ch2_D624w);
                //myFS_LED2.ReadXY(3, Feasa_10F_B.ch3_D621w);
                //myFS_LED2.ReadXY(4, Feasa_10F_B.ch4_D622w);
                //myFS_LED2.ReadXY(5, Feasa_10F_B.ch5_D623w);
                //myFS_LED2.ReadXY(6, Feasa_10F_B.ch6_D626w);
                //myFS_LED2.ReadXY(7, Feasa_10F_B.ch7_D627w);
                //myFS_LED2.ReadXY(8, Feasa_10F_B.ch8_D628w);
                //myFS_LED2.ReadXY(9, Feasa_10F_B.ch9_D629w);

                //Console.WriteLine(" B ch1_D625w = " + Feasa_10F_B.ch1_D625w.val_I_XY + " "
                //    + Feasa_10F_B.ch1_D625w.val_X + " " + Feasa_10F_B.ch1_D625w.val_Y);
                //Console.WriteLine(" B ch2_D624w = " + Feasa_10F_B.ch2_D624w.val_I_XY + " "
                //    + Feasa_10F_B.ch2_D624w.val_X + " " + Feasa_10F_B.ch2_D624w.val_Y);
                //Console.WriteLine(" B ch3_D621w = " + Feasa_10F_B.ch3_D621w.val_I_XY + " "
                //    + Feasa_10F_B.ch3_D621w.val_X + " " + Feasa_10F_B.ch3_D621w.val_Y);
                //Console.WriteLine(" B ch4_D622w = " + Feasa_10F_B.ch4_D622w.val_I_XY + " "
                //    + Feasa_10F_B.ch4_D622w.val_X + " " + Feasa_10F_B.ch4_D622w.val_Y);
                //Console.WriteLine(" B ch5_D623w = " + Feasa_10F_B.ch5_D623w.val_I_XY + " "
                //    + Feasa_10F_B.ch5_D623w.val_X + " " + Feasa_10F_B.ch5_D623w.val_Y);
                //Console.WriteLine(" B ch6_D626w = " + Feasa_10F_B.ch6_D626w.val_I_XY + " "
                //    + Feasa_10F_B.ch6_D626w.val_X + " " + Feasa_10F_B.ch6_D626w.val_Y);
                //Console.WriteLine(" B ch7_D627w = " + Feasa_10F_B.ch7_D627w.val_I_XY + " "
                //    + Feasa_10F_B.ch7_D627w.val_X + " " + Feasa_10F_B.ch7_D627w.val_Y);
                //Console.WriteLine(" B ch8_D628w = " + Feasa_10F_B.ch8_D628w.val_I_XY + " "
                //    + Feasa_10F_B.ch8_D628w.val_X + " " + Feasa_10F_B.ch8_D628w.val_Y);
                //Console.WriteLine(" B ch9_D629w = " + Feasa_10F_B.ch9_D629w.val_I_XY + " "
                //    + Feasa_10F_B.ch9_D629w.val_X + " " + Feasa_10F_B.ch9_D629w.val_Y);

            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
            //myFS_LED1.Close();
            //myFS_LED1 = null;
            //myFS_LED2.Close();
            //myFS_LED2 = null;
            //System.Threading.Thread.Sleep(50);
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedError, 1);//organge
            taskind = 0;
            if (taskADC.start())
                return WORK_STATUS.PASS;
            else
            {
                return WORK_STATUS.FAIL;
            }
            taskADC = null;
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            //return WORK_STATUS.PASS;
            
            int[] chstart = { 0, 5, 7,10, 12 };
            int[] chend = { 4, 6, 9, 11,18 };
            WORK_STATUS re = WORK_STATUS.WORKING;
            //if (taskList[taskind] != null)
            {
                if (taskList[taskind].isEnable() == false)
                {
                    if (taskList[taskind].getErrorMsg() == null)      
                    {
                        int cnt = 0;
                        for (int i = chstart[taskind]; i < chend[taskind]+1; i++)
                        {
                            NIavg[i] = taskList[taskind].getAverage(cnt);
                            
                            cnt++;
                            Console.WriteLine("i= {0} AI {1} = {2}", Convert.ToString(i),
                                Convert.ToString(cnt), NIavg[i].ToString());
                        }
                        taskList[taskind] = null;
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        taskind++;
                        if (taskind == taskList.Length)
                        {
                            StaticValues.s21_NIdata = NIavg;
                            re = WORK_STATUS.PASS;
                        }
                        else
                        {
                            if (!taskList[taskind].start()){ re = WORK_STATUS.FAIL; }
                            
                        }

                    }
                    else
                    {
                        sMeasured = taskList[taskind].getErrorMsg();
                        re = WORK_STATUS.FAIL;
                    }
                }
            }

            return re;
        }

        public override void release()
        {
            //myFS_LED1.Close();
            //myFS_LED1 = null;
            //myFS_LED2.Close();
            //myFS_LED2 = null;
            NIavg = null;
            taskList = null;
            //taskADC = null;
        }
    }

    class TestItemStep021_P2 : TestItemForm
    {
        //[DllImport("feasacom64.dll")]
        //private static extern int FeasaCom_Close(int SerialNumber);

        public string step = "_021 P2";
        private Feasa_LED myFS_LED1;
        private Feasa_LED myFS_LED2;
        //private DAQadc taskADC;
        //private double r1 = 1100;
        //private double r2 = 1000;
        //private double[] NIavg;
        public TestItemStep021_P2()
        {
            sStep = step;
            LoadSpec();
        }
        private void Init_Feasa()
        {
            Feasa_10F_A.ch1_D630o = new XY_Data();
            Feasa_10F_A.ch2_D631o = new XY_Data();
            Feasa_10F_A.ch3_D500w = new XY_Data();
            Feasa_10F_A.ch4_D510w = new XY_Data();
            Feasa_10F_A.ch5_D501w = new XY_Data();
            Feasa_10F_A.ch6_D612o = new XY_Data();
            Feasa_10F_A.ch7_D502w = new XY_Data();
            Feasa_10F_A.ch8_D511w = new XY_Data();

            Feasa_10F_B.ch1_D625w = new XY_Data();
            Feasa_10F_B.ch2_D624w = new XY_Data();
            Feasa_10F_B.ch3_D621w = new XY_Data();
            Feasa_10F_B.ch4_D622w = new XY_Data();
            Feasa_10F_B.ch5_D623w = new XY_Data();
            Feasa_10F_B.ch6_D626w = new XY_Data();
            Feasa_10F_B.ch7_D627w = new XY_Data();
            Feasa_10F_B.ch8_D628w = new XY_Data();
            Feasa_10F_B.ch9_D629w = new XY_Data();

            Feasa_10F_B.ch2_D616o = new XY_Data();
            Feasa_10F_B.ch3_D615o = new XY_Data();
            Feasa_10F_B.ch4_D614o = new XY_Data();
            Feasa_10F_B.ch5_D613o = new XY_Data();
            Feasa_10F_B.ch6_D620o = new XY_Data();
            Feasa_10F_B.ch7_D619o = new XY_Data();
            Feasa_10F_B.ch8_D618o = new XY_Data();
            Feasa_10F_B.ch9_D617o = new XY_Data();
        }
        public override WORK_STATUS Init()
        {
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedRTF, 0); //white
            Init_Feasa();
            try
            {
                myFS_LED1 = new Feasa_LED(Feasa_10F_A.comPort);
                myFS_LED2 = new Feasa_LED(Feasa_10F_B.comPort);
                myFS_LED1.doC3Capture();
                myFS_LED2.doC3Capture();

                myFS_LED1.ReadXY(1, Feasa_10F_A.ch1_D630o);
                myFS_LED1.ReadXY(2, Feasa_10F_A.ch2_D631o);
                myFS_LED1.ReadXY(3, Feasa_10F_A.ch3_D500w);
                myFS_LED1.ReadXY(4, Feasa_10F_A.ch4_D510w);
                myFS_LED1.ReadXY(5, Feasa_10F_A.ch5_D501w);
                myFS_LED1.ReadXY(6, Feasa_10F_A.ch6_D612o);
                myFS_LED1.ReadXY(7, Feasa_10F_A.ch7_D502w);
                myFS_LED1.ReadXY(8, Feasa_10F_A.ch8_D511w);

                myFS_LED2.ReadXY(2, Feasa_10F_B.ch2_D616o);
                myFS_LED2.ReadXY(3, Feasa_10F_B.ch3_D615o);
                myFS_LED2.ReadXY(4, Feasa_10F_B.ch4_D614o);
                myFS_LED2.ReadXY(5, Feasa_10F_B.ch5_D613o);
                myFS_LED2.ReadXY(6, Feasa_10F_B.ch6_D620o);
                myFS_LED2.ReadXY(7, Feasa_10F_B.ch7_D619o);
                myFS_LED2.ReadXY(8, Feasa_10F_B.ch8_D618o);
                myFS_LED2.ReadXY(9, Feasa_10F_B.ch9_D617o);

                Console.WriteLine("ch1_D630o = " + Feasa_10F_A.ch1_D630o.val_I_XY + " "
                    + Feasa_10F_A.ch1_D630o.val_X + " " + Feasa_10F_A.ch1_D630o.val_Y);
                Console.WriteLine("ch2_D631o = " + Feasa_10F_A.ch2_D631o.val_I_XY + " "
                    + Feasa_10F_A.ch2_D631o.val_X + " " + Feasa_10F_A.ch2_D631o.val_Y);
                Console.WriteLine("ch3_D500w = " + Feasa_10F_A.ch3_D500w.val_I_XY + " "
                    + Feasa_10F_A.ch3_D500w.val_X + " " + Feasa_10F_A.ch3_D500w.val_Y);
                Console.WriteLine("ch4_D510w = " + Feasa_10F_A.ch4_D510w.val_I_XY + " "
                    + Feasa_10F_A.ch4_D510w.val_X + " " + Feasa_10F_A.ch4_D510w.val_Y);
                Console.WriteLine("ch5_D501w = " + Feasa_10F_A.ch5_D501w.val_I_XY + " "
                    + Feasa_10F_A.ch5_D501w.val_X + " " + Feasa_10F_A.ch5_D501w.val_Y);
                Console.WriteLine("ch6_D612o = " + Feasa_10F_A.ch6_D612o.val_I_XY + " "
                    + Feasa_10F_A.ch6_D612o.val_X + " " + Feasa_10F_A.ch6_D612o.val_Y);
                Console.WriteLine("ch7_D502w = " + Feasa_10F_A.ch7_D502w.val_I_XY + " "
                    + Feasa_10F_A.ch7_D502w.val_X + " " + Feasa_10F_A.ch7_D502w.val_Y);
                Console.WriteLine("ch8_D511w = " + Feasa_10F_A.ch8_D511w.val_I_XY + " "
                    + Feasa_10F_A.ch8_D511w.val_X + " " + Feasa_10F_A.ch8_D511w.val_Y);

                Console.WriteLine(" B ch2_D616o = " + Feasa_10F_B.ch2_D616o.val_I_XY + " "
                    + Feasa_10F_B.ch2_D616o.val_X + " " + Feasa_10F_B.ch2_D616o.val_Y);
                Console.WriteLine(" B ch3_D615o = " + Feasa_10F_B.ch3_D615o.val_I_XY + " "
                    + Feasa_10F_B.ch3_D615o.val_X + " " + Feasa_10F_B.ch3_D615o.val_Y);
                Console.WriteLine(" B ch4_D614o = " + Feasa_10F_B.ch4_D614o.val_I_XY + " "
                    + Feasa_10F_B.ch4_D614o.val_X + " " + Feasa_10F_B.ch4_D614o.val_Y);
                Console.WriteLine(" B ch5_D613o = " + Feasa_10F_B.ch5_D613o.val_I_XY + " "
                    + Feasa_10F_B.ch5_D613o.val_X + " " + Feasa_10F_B.ch5_D613o.val_Y);
                Console.WriteLine(" B ch6_D620o = " + Feasa_10F_B.ch6_D620o.val_I_XY + " "
                    + Feasa_10F_B.ch6_D620o.val_X + " " + Feasa_10F_B.ch6_D620o.val_Y);
                Console.WriteLine(" B ch7_D619o = " + Feasa_10F_B.ch7_D619o.val_I_XY + " "
                    + Feasa_10F_B.ch7_D619o.val_X + " " + Feasa_10F_B.ch7_D619o.val_Y);
                Console.WriteLine(" B ch8_D618o = " + Feasa_10F_B.ch8_D618o.val_I_XY + " "
                    + Feasa_10F_B.ch8_D618o.val_X + " " + Feasa_10F_B.ch8_D618o.val_Y);
                Console.WriteLine(" B ch9_D617o = " + Feasa_10F_B.ch9_D617o.val_I_XY + " "
                    + Feasa_10F_B.ch9_D617o.val_X + " " + Feasa_10F_B.ch9_D617o.val_Y);
                return WORK_STATUS.PASS;
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
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedRTF, 1); //white
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedError, 0);//organge
            try
            {
                myFS_LED2.doC3Capture();
                myFS_LED2.ReadXY(1, Feasa_10F_B.ch1_D625w);
                myFS_LED2.ReadXY(2, Feasa_10F_B.ch2_D624w);
                myFS_LED2.ReadXY(3, Feasa_10F_B.ch3_D621w);
                myFS_LED2.ReadXY(4, Feasa_10F_B.ch4_D622w);
                myFS_LED2.ReadXY(5, Feasa_10F_B.ch5_D623w);
                myFS_LED2.ReadXY(6, Feasa_10F_B.ch6_D626w);
                myFS_LED2.ReadXY(7, Feasa_10F_B.ch7_D627w);
                myFS_LED2.ReadXY(8, Feasa_10F_B.ch8_D628w);
                myFS_LED2.ReadXY(9, Feasa_10F_B.ch9_D629w);

                Console.WriteLine(" B ch1_D625w = " + Feasa_10F_B.ch1_D625w.val_I_XY + " "
                    + Feasa_10F_B.ch1_D625w.val_X + " " + Feasa_10F_B.ch1_D625w.val_Y);
                Console.WriteLine(" B ch2_D624w = " + Feasa_10F_B.ch2_D624w.val_I_XY + " "
                    + Feasa_10F_B.ch2_D624w.val_X + " " + Feasa_10F_B.ch2_D624w.val_Y);
                Console.WriteLine(" B ch3_D621w = " + Feasa_10F_B.ch3_D621w.val_I_XY + " "
                    + Feasa_10F_B.ch3_D621w.val_X + " " + Feasa_10F_B.ch3_D621w.val_Y);
                Console.WriteLine(" B ch4_D622w = " + Feasa_10F_B.ch4_D622w.val_I_XY + " "
                    + Feasa_10F_B.ch4_D622w.val_X + " " + Feasa_10F_B.ch4_D622w.val_Y);
                Console.WriteLine(" B ch5_D623w = " + Feasa_10F_B.ch5_D623w.val_I_XY + " "
                    + Feasa_10F_B.ch5_D623w.val_X + " " + Feasa_10F_B.ch5_D623w.val_Y);
                Console.WriteLine(" B ch6_D626w = " + Feasa_10F_B.ch6_D626w.val_I_XY + " "
                    + Feasa_10F_B.ch6_D626w.val_X + " " + Feasa_10F_B.ch6_D626w.val_Y);
                Console.WriteLine(" B ch7_D627w = " + Feasa_10F_B.ch7_D627w.val_I_XY + " "
                    + Feasa_10F_B.ch7_D627w.val_X + " " + Feasa_10F_B.ch7_D627w.val_Y);
                Console.WriteLine(" B ch8_D628w = " + Feasa_10F_B.ch8_D628w.val_I_XY + " "
                    + Feasa_10F_B.ch8_D628w.val_X + " " + Feasa_10F_B.ch8_D628w.val_Y);
                Console.WriteLine(" B ch9_D629w = " + Feasa_10F_B.ch9_D629w.val_I_XY + " "
                    + Feasa_10F_B.ch9_D629w.val_X + " " + Feasa_10F_B.ch9_D629w.val_Y);

            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
            return WORK_STATUS.PASS;
            

        }

        public override void release()
        {
            myFS_LED1.Close();
            myFS_LED1 = null;
            myFS_LED2.Close();
            myFS_LED2 = null;
        }
    }

    class TestItemStep021_P2_1 : TestItemForm
    {
        public string step = "_021 P2.1";
        public TestItemStep021_P2_1()
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
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            sMeasured = StaticValues.s21_NIdata[12].ToString();
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
            return re;
        }
        public override void release()
        {
           
        }
    }
    class TestItemStep021_P2_1a : TestItemForm
    {
        public string step = "_021 P2.1a";
        public TestItemStep021_P2_1a()
        {
            sStep = step;
            LoadSpec();
        }
        public override WORK_STATUS Init()
        {return WORK_STATUS.PASS;}

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
             // R1 =1100k , R2 = 1000k
            double val = StaticValues.s21_NIdata[0] * (2100 / 1000);
            sMeasured = val.ToString();
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
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P2_1o : TestItemForm
    {
        public string step = "_021 P2.D500";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_1o()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_A.ch3_D500w;

            ledXY = new LED_Check(Feasa_10F_A.WLED_X_LLimit,
                                  Feasa_10F_A.WLED_X_ULimit,
                                  Feasa_10F_A.WLED_Y_LLimit,
                                  Feasa_10F_A.WLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_A.ch3_D500w = null;
        }
    }

    class TestItemStep021_P2_2 : TestItemForm
    {
        public string step = "_021 P2.2";
        public TestItemStep021_P2_2()
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
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            sMeasured = StaticValues.s21_NIdata[13].ToString();
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
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P2_2a : TestItemForm
    {
        public string step = "_021 P2.2a";
        public TestItemStep021_P2_2a()
        {
            sStep = step;
            LoadSpec();
        }
        public override WORK_STATUS Init()
        { return WORK_STATUS.PASS; }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            // R1 =1100k , R2 = 1000k
            double val = StaticValues.s21_NIdata[1] * (2100 / 1000);
            sMeasured = val.ToString();
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
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P2_2o : TestItemForm
    {
        public string step = "_021 P2.D510";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_2o()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_A.ch4_D510w;

            ledXY = new LED_Check(Feasa_10F_A.WLED_X_LLimit,
                                  Feasa_10F_A.WLED_X_ULimit,
                                  Feasa_10F_A.WLED_Y_LLimit,
                                  Feasa_10F_A.WLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_A.ch4_D510w = null;
        }
    }

    class TestItemStep021_P2_3 : TestItemForm
    {
        public string step = "_021 P2.3";
        public TestItemStep021_P2_3()
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
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            sMeasured = StaticValues.s21_NIdata[14].ToString();
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
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P2_3a : TestItemForm
    {
        public string step = "_021 P2.3a";
        public TestItemStep021_P2_3a()
        {
            sStep = step;
            LoadSpec();
        }
        public override WORK_STATUS Init()
        { return WORK_STATUS.PASS; }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            // R1 =1100k , R2 = 1000k
            double val = StaticValues.s21_NIdata[2] * (2100 / 1000);
            sMeasured = val.ToString();
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
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P2_3o : TestItemForm
    {
        public string step = "_021 P2.D501";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_3o()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_A.ch5_D501w;

            ledXY = new LED_Check(Feasa_10F_A.WLED_X_LLimit,
                                  Feasa_10F_A.WLED_X_ULimit,
                                  Feasa_10F_A.WLED_Y_LLimit,
                                  Feasa_10F_A.WLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_A.ch5_D501w = null;
        }
    }

    class TestItemStep021_P2_4 : TestItemForm
    {
        public string step = "_021 P2.4";
        public TestItemStep021_P2_4()
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
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            sMeasured = StaticValues.s21_NIdata[15].ToString();
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
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P2_4a : TestItemForm
    {
        public string step = "_021 P2.4a";
        public TestItemStep021_P2_4a()
        {
            sStep = step;
            LoadSpec();
        }
        public override WORK_STATUS Init()
        { return WORK_STATUS.PASS; }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            // R1 =1100k , R2 = 1000k
            double val = StaticValues.s21_NIdata[3] * (2100 / 1000);
            sMeasured = val.ToString();
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
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P2_4o : TestItemForm
    {
        public string step = "_021 P2.D511";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_4o()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_A.ch8_D511w;

            ledXY = new LED_Check(Feasa_10F_A.WLED_X_LLimit,
                                  Feasa_10F_A.WLED_X_ULimit,
                                  Feasa_10F_A.WLED_Y_LLimit,
                                  Feasa_10F_A.WLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_A.ch8_D511w = null;
        }
    }

    class TestItemStep021_P2_5 : TestItemForm
    {
        public string step = "_021 P2.5";
        public TestItemStep021_P2_5()
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
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            sMeasured = StaticValues.s21_NIdata[16].ToString();
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
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P2_5a : TestItemForm
    {
        public string step = "_021 P2.5a";
        public TestItemStep021_P2_5a()
        {
            sStep = step;
            LoadSpec();
        }
        public override WORK_STATUS Init()
        { return WORK_STATUS.PASS; }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            // R1 =1100k , R2 = 1000k
            double val = StaticValues.s21_NIdata[4] * (2100 / 1000);
            sMeasured = val.ToString();
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
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P2_5o : TestItemForm
    {
        public string step = "_021 P2.D502";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_5o()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_A.ch7_D502w;

            ledXY = new LED_Check(Feasa_10F_A.WLED_X_LLimit,
                                  Feasa_10F_A.WLED_X_ULimit,
                                  Feasa_10F_A.WLED_Y_LLimit,
                                  Feasa_10F_A.WLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_A.ch7_D502w = null;
        }
    }

    class TestItemStep021_P2_6 : TestItemForm
    {
        public string step = "_021 P2.6";
        public TestItemStep021_P2_6()
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
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            sMeasured = StaticValues.s21_NIdata[17].ToString();
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
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P2_6a : TestItemForm
    {
        public string step = "_021 P2.6a";
        public TestItemStep021_P2_6a()
        {
            sStep = step;
            LoadSpec();
        }
        public override WORK_STATUS Init()
        { return WORK_STATUS.PASS; }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            // R1 =1100k , R2 = 1000k
            double val = StaticValues.s21_NIdata[5] * (2100 / 1000);
            sMeasured = val.ToString();
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
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P2_6b : TestItemForm
    {
        public string step = "_021 P2.6b";
        public TestItemStep021_P2_6b()
        {
            sStep = step;
            LoadSpec();
        }
        public override WORK_STATUS Init()
        { return WORK_STATUS.PASS; }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            // R1 =1100k , R2 = 1000k
            double val = StaticValues.s21_NIdata[6] * (2100 / 1000);
            sMeasured = val.ToString();
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
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P2_6o1 : TestItemForm
    {
        public string step = "_021 P2.D613";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_6o1()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch5_D613o;

            ledXY = new LED_Check(Feasa_10F_B.OLED_X_LLimit,
                      Feasa_10F_B.OLED_X_ULimit,
                      Feasa_10F_B.OLED_Y_LLimit,
                      Feasa_10F_B.OLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_B.ch5_D613o = null;
        }
    }
    class TestItemStep021_P2_6o2 : TestItemForm
    {
        public string step = "_021 P2.D614";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_6o2()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch4_D614o;

            ledXY = new LED_Check(Feasa_10F_B.OLED_X_LLimit,
                      Feasa_10F_B.OLED_X_ULimit,
                      Feasa_10F_B.OLED_Y_LLimit,
                      Feasa_10F_B.OLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_B.ch4_D614o = null;
        }
    }
    class TestItemStep021_P2_6o3 : TestItemForm
    {
        public string step = "_021 P2.D615";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_6o3()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch3_D615o;

            ledXY = new LED_Check(Feasa_10F_B.OLED_X_LLimit,
                                  Feasa_10F_B.OLED_X_ULimit,
                                  Feasa_10F_B.OLED_Y_LLimit,
                                  Feasa_10F_B.OLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_B.ch3_D615o = null;
        }
    }
    class TestItemStep021_P2_6o4 : TestItemForm
    {
        public string step = "_021 P2.D616";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_6o4()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch2_D616o;

            ledXY = new LED_Check(Feasa_10F_B.OLED_X_LLimit,
                                  Feasa_10F_B.OLED_X_ULimit,
                                  Feasa_10F_B.OLED_Y_LLimit,
                                  Feasa_10F_B.OLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_B.ch2_D616o = null;
        }
    }
    class TestItemStep021_P2_6o5 : TestItemForm
    {
        public string step = "_021 P2.D617";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_6o5()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch9_D617o;

            ledXY = new LED_Check(Feasa_10F_B.OLED_X_LLimit,
                                  Feasa_10F_B.OLED_X_ULimit,
                                  Feasa_10F_B.OLED_Y_LLimit,
                                  Feasa_10F_B.OLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_B.ch9_D617o = null;
        }
    }
    class TestItemStep021_P2_6o6 : TestItemForm
    {
        public string step = "_021 P2.D618";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_6o6()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch8_D618o;

            ledXY = new LED_Check(Feasa_10F_B.OLED_X_LLimit,
                                  Feasa_10F_B.OLED_X_ULimit,
                                  Feasa_10F_B.OLED_Y_LLimit,
                                  Feasa_10F_B.OLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_B.ch8_D618o = null;
        }
    }
    class TestItemStep021_P2_6o7 : TestItemForm
    {
        public string step = "_021 P2.D619";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_6o7()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch7_D619o;

            ledXY = new LED_Check(Feasa_10F_B.OLED_X_LLimit,
                                  Feasa_10F_B.OLED_X_ULimit,
                                  Feasa_10F_B.OLED_Y_LLimit,
                                  Feasa_10F_B.OLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_B.ch7_D619o = null;
        }
    }
    class TestItemStep021_P2_6o8 : TestItemForm
    {
        public string step = "_021 P2.D620";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_6o8()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch6_D620o;

            ledXY = new LED_Check(Feasa_10F_B.OLED_X_LLimit,
                                  Feasa_10F_B.OLED_X_ULimit,
                                  Feasa_10F_B.OLED_Y_LLimit,
                                  Feasa_10F_B.OLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_B.ch6_D620o = null;
        }
    }

    class TestItemStep021_P2_7 : TestItemForm
    {
        public string step = "_021 P2.7";
        public TestItemStep021_P2_7()
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
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            sMeasured = StaticValues.s21_NIdata[18].ToString();
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
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P2_7a : TestItemForm
    {
        public string step = "_021 P2.7a";
        public TestItemStep021_P2_7a()
        {
            sStep = step;
            LoadSpec();
        }
        public override WORK_STATUS Init()
        { return WORK_STATUS.PASS; }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            // R1 =1100k , R2 = 1000k
            double val = StaticValues.s21_NIdata[7] * (2100 / 1000);
            sMeasured = val.ToString();
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
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P2_7b : TestItemForm
    {
        public string step = "_021 P2.7b";
        public TestItemStep021_P2_7b()
        {
            sStep = step;
            LoadSpec();
        }
        public override WORK_STATUS Init()
        { return WORK_STATUS.PASS; }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            // R1 =1100k , R2 = 1000k
            double val = StaticValues.s21_NIdata[8] * (2100 / 1000);
            sMeasured = val.ToString();
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
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P2_7c : TestItemForm
    {
        public string step = "_021 P2.7c";
        public TestItemStep021_P2_7c()
        {
            sStep = step;
            LoadSpec();
        }
        public override WORK_STATUS Init()
        { return WORK_STATUS.PASS; }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            // R1 =1100k , R2 = 1000k
            double val = StaticValues.s21_NIdata[9] * (2100 / 1000);
            sMeasured = val.ToString();
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
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P2_7o1 : TestItemForm
    {
        public string step = "_021 P2.D621";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_7o1()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch3_D621w;

            ledXY = new LED_Check(Feasa_10F_B.WLED_X_LLimit,
                      Feasa_10F_B.WLED_X_ULimit,
                      Feasa_10F_B.WLED_Y_LLimit,
                      Feasa_10F_B.WLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_B.ch3_D621w = null;
        }
    }
    class TestItemStep021_P2_7o2 : TestItemForm
    {
        public string step = "_021 P2.D622";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_7o2()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch4_D622w;

            ledXY = new LED_Check(Feasa_10F_B.WLED_X_LLimit,
                      Feasa_10F_B.WLED_X_ULimit,
                      Feasa_10F_B.WLED_Y_LLimit,
                      Feasa_10F_B.WLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_B.ch4_D622w = null;
        }
    }
    class TestItemStep021_P2_7o3 : TestItemForm
    {
        public string step = "_021 P2.D623";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_7o3()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch5_D623w;

            ledXY = new LED_Check(Feasa_10F_B.WLED_X_LLimit,
                      Feasa_10F_B.WLED_X_ULimit,
                      Feasa_10F_B.WLED_Y_LLimit,
                      Feasa_10F_B.WLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_B.ch5_D623w = null;
        }
    }
    class TestItemStep021_P2_7o4 : TestItemForm
    {
        public string step = "_021 P2.D624";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_7o4()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch2_D624w;

            ledXY = new LED_Check(Feasa_10F_B.WLED_X_LLimit,
                      Feasa_10F_B.WLED_X_ULimit,
                      Feasa_10F_B.WLED_Y_LLimit,
                      Feasa_10F_B.WLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_B.ch2_D624w = null;
        }
    }
    class TestItemStep021_P2_7o5 : TestItemForm
    {
        public string step = "_021 P2.D625";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_7o5()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch1_D625w;

            ledXY = new LED_Check(Feasa_10F_B.WLED_X_LLimit,
                      Feasa_10F_B.WLED_X_ULimit,
                      Feasa_10F_B.WLED_Y_LLimit,
                      Feasa_10F_B.WLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_B.ch1_D625w = null;
        }
    }
    class TestItemStep021_P2_7o6 : TestItemForm
    {
        public string step = "_021 P2.D626";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_7o6()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch6_D626w;

            ledXY = new LED_Check(Feasa_10F_B.WLED_X_LLimit,
                      Feasa_10F_B.WLED_X_ULimit,
                      Feasa_10F_B.WLED_Y_LLimit,
                      Feasa_10F_B.WLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_B.ch6_D626w = null;
        }
    }
    class TestItemStep021_P2_7o7 : TestItemForm
    {
        public string step = "_021 P2.D627";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_7o7()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch7_D627w;

            ledXY = new LED_Check(Feasa_10F_B.WLED_X_LLimit,
                      Feasa_10F_B.WLED_X_ULimit,
                      Feasa_10F_B.WLED_Y_LLimit,
                      Feasa_10F_B.WLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_B.ch7_D627w = null;
        }
    }
    class TestItemStep021_P2_7o8 : TestItemForm
    {
        public string step = "_021 P2.D628";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_7o8()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch8_D628w;

            ledXY = new LED_Check(Feasa_10F_B.WLED_X_LLimit,
                                  Feasa_10F_B.WLED_X_ULimit,
                                  Feasa_10F_B.WLED_Y_LLimit,
                                  Feasa_10F_B.WLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_B.ch8_D628w = null;
        }
    }
    class TestItemStep021_P2_7o9 : TestItemForm
    {
        public string step = "_021 P2.D629";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_7o9()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch9_D629w;

            ledXY = new LED_Check(Feasa_10F_B.WLED_X_LLimit,
                                  Feasa_10F_B.WLED_X_ULimit,
                                  Feasa_10F_B.WLED_Y_LLimit,
                                  Feasa_10F_B.WLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_B.ch9_D629w = null;
        }
    }
    class TestItemStep021_P2_8 : TestItemForm
    {
        public string step = "_021 P2.8";
        public TestItemStep021_P2_8()
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
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            sMeasured = StaticValues.s21_NIdata[11].ToString();
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
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P2_8a : TestItemForm
    {
        public string step = "_021 P2.8a";
        public TestItemStep021_P2_8a()
        {
            sStep = step;
            LoadSpec();
        }
        public override WORK_STATUS Init()
        { return WORK_STATUS.PASS; }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            // R1 =1100k , R2 = 1000k
            double val = StaticValues.s21_NIdata[10] * (2000 / 1000);
            sMeasured = val.ToString();
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
            return re;
        }
        public override void release()
        {
            StaticValues.s21_NIdata = null;
        }
    }
    class TestItemStep021_P2_8o1 : TestItemForm
    {
        //public string step = "_021 P2.D612";
        public string step = "_021 P2.D631";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_8o1()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_A.ch6_D612o;

            ledXY = new LED_Check(Feasa_10F_A.OLED_X_LLimit,
                                  Feasa_10F_A.OLED_X_ULimit,
                                  Feasa_10F_A.OLED_Y_LLimit,
                                  Feasa_10F_A.OLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_A.ch6_D612o = null;
        }
    }
    class TestItemStep021_P2_8o2 : TestItemForm
    {
        public string step = "_021 P2.D630";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_8o2()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_A.ch1_D630o;

            ledXY = new LED_Check(Feasa_10F_A.OLED_X_LLimit,
                                  Feasa_10F_A.OLED_X_ULimit,
                                  Feasa_10F_A.OLED_Y_LLimit,
                                  Feasa_10F_A.OLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_A.ch1_D630o = null;
        }
    }
    class TestItemStep021_P2_8o3 : TestItemForm
    {
        public string step = "_021 P2.D631";
        private LED_Check ledXY;
        private XY_Data xy_Data;

        public TestItemStep021_P2_8o3()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_A.ch2_D631o;

            ledXY = new LED_Check(Feasa_10F_A.OLED_X_LLimit,
                                  Feasa_10F_A.OLED_X_ULimit,
                                  Feasa_10F_A.OLED_Y_LLimit,
                                  Feasa_10F_A.OLED_Y_ULimit);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            Console.WriteLine("XYi = " + xy_Data.val_X + ", " + xy_Data.val_Y + ", " + xy_Data.val_I_XY);
            if (!ledXY.checkLedXY(xy_Data))
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }

            sMeasured = xy_Data.val_I_XY.ToString();
            if (!PassFailJudge())
            {
                sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
                return WORK_STATUS.FAIL;
            }
            sMeasured = xy_Data.val_I_XY + " " + xy_Data.val_X + " " + xy_Data.val_Y;
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            Feasa_10F_A.ch2_D631o = null;
        }
    }
    class LED_Check
    {
        private float x_LL;
        private float x_UL;
        private float y_LL;
        private float y_UL;

        public LED_Check(float xLL, float xUL, float yLL, float yUL)
        {
            x_LL = xLL;
            x_UL = xUL;
            y_LL = yLL;
            y_UL = yUL;
        }

        public bool checkLedXY(XY_Data data)
        {
            if (data.val_X < x_LL || data.val_X > x_UL ||
                data.val_Y < y_LL || data.val_Y > y_UL)
            {
                return false;
            }

            return true;
        }
    }

    class LED_Check_WL
    {
        private int wl_LL;
        private int wl_UL;

        public LED_Check_WL(int wlLL, int wlUL)
        {
            wl_LL = wlLL;
            wl_UL = wlUL;
        }

        public bool checkLedWL(WL_Data data)
        {
            if (data.val_WL < wl_LL || data.val_WL > wl_UL)
            {
                return false;
            }

            return true;
        }
    }
}
