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
        public string step = "_021 P1";
        private Feasa_LED myFS_LED1;
        private Feasa_LED myFS_LED2;

        public TestItemStep021_P1()
        {
            sStep = step;
            LoadSpec();
        }
        private void Load_FeasaData_W(CCT_Data cctA, CCT_Data cctB, WL_allData wlA, WL_allData wlB)
        {
            Feasa_10F_A.CCT_ch5_D502w = cctA.val_CCT2;
            Feasa_10F_A.CCT_ch3_D501w = cctA.val_CCT4;
            Feasa_10F_A.CCT_ch2_D510w = cctA.val_CCT5;
            Feasa_10F_A.CCT_ch1_D500w = cctA.val_CCT6;
            Feasa_10F_A.CCT_ch4_D511w = cctA.val_CCT3;
            Feasa_10F_A.I_ch5_D502w = wlA.val_I_WL2;
            Feasa_10F_A.I_ch3_D501w = wlA.val_I_WL4;
            Feasa_10F_A.I_ch2_D510w = wlA.val_I_WL5;
            Feasa_10F_A.I_ch1_D500w = wlA.val_I_WL6;
            Feasa_10F_A.I_ch4_D511w = wlA.val_I_WL4;
            Feasa_10F_A.I_ch6_D631o = wlA.val_I_WL1;
            Feasa_10F_A.W_ch6_D631o = wlA.val_WL1;
            Feasa_10F_A.I_ch7_D701 = wlA.val_I_WL7;
            Feasa_10F_A.W_ch7_D701 = wlA.val_WL7;

            Feasa_10F_B.CCT_ch1_D507w = cctB.val_CCT9;
            Feasa_10F_B.CCT_ch2_D516w = cctB.val_CCT8;
            Feasa_10F_B.CCT_ch3_D525w = cctB.val_CCT7;
            Feasa_10F_B.CCT_ch4_D521w = cctB.val_CCT6;
            Feasa_10F_B.CCT_ch5_D520w = cctB.val_CCT5;
            Feasa_10F_B.CCT_ch6_D522w = cctB.val_CCT4;
            Feasa_10F_B.CCT_ch7_D526w = cctB.val_CCT3;
            Feasa_10F_B.CCT_ch8_D517w = cctB.val_CCT2;
            Feasa_10F_B.CCT_ch9_D508w = cctB.val_CCT1;
            Feasa_10F_B.I_ch1_D507w = wlB.val_I_WL9;
            Feasa_10F_B.I_ch2_D516w = wlB.val_I_WL8;
            Feasa_10F_B.I_ch3_D525w = wlB.val_I_WL7;
            Feasa_10F_B.I_ch4_D521w = wlB.val_I_WL6;
            Feasa_10F_B.I_ch5_D520w = wlB.val_I_WL5;
            Feasa_10F_B.I_ch6_D522w = wlB.val_I_WL4;
            Feasa_10F_B.I_ch7_D526w = wlB.val_I_WL3;
            Feasa_10F_B.I_ch8_D517w = wlB.val_I_WL2;
            Feasa_10F_B.I_ch9_D508w = wlB.val_I_WL1;

            
        }
        private void Load_FeasaData_O(WL_allData wlB)
        {
            
            //Feasa_10F_A.I_ch6_D631o = wlA.val_I_WL6;
            //Feasa_10F_A.W_ch6_D631o = wlA.val_WL6;

            Feasa_10F_B.I_ch1_D505o = wlB.val_I_WL9;
            Feasa_10F_B.I_ch2_D514o = wlB.val_I_WL8 ;
            Feasa_10F_B.I_ch3_D523o = wlB.val_I_WL7 ;
            Feasa_10F_B.I_ch4_D527o = wlB.val_I_WL6;
            Feasa_10F_B.W_ch1_D505o = wlB.val_WL9;
            Feasa_10F_B.W_ch2_D514o = wlB.val_WL8;
            Feasa_10F_B.W_ch3_D523o = wlB.val_WL7;
            Feasa_10F_B.W_ch4_D527o = wlB.val_WL6;

            Feasa_10F_B.W_ch6_D528o = wlB.val_WL4;
            Feasa_10F_B.W_ch7_D524o = wlB.val_WL3;
            Feasa_10F_B.W_ch8_D515o = wlB.val_WL2;
            Feasa_10F_B.W_ch9_D506o = wlB.val_WL1;
            Feasa_10F_B.I_ch6_D528o = wlB.val_I_WL4;
            Feasa_10F_B.I_ch7_D524o = wlB.val_I_WL3 ;
            Feasa_10F_B.I_ch8_D515o = wlB.val_I_WL2 ;
            Feasa_10F_B.I_ch9_D506o = wlB.val_I_WL1 ;

        }
        private void Init_Feasa()
        {
            Feasa_10F_A.CCT_ch5_D502w = 0;
            Feasa_10F_A.CCT_ch3_D501w = 0;
            Feasa_10F_A.CCT_ch2_D510w =0 ;
            Feasa_10F_A.CCT_ch1_D500w = 0;
            Feasa_10F_A.CCT_ch4_D511w =0 ;
            Feasa_10F_A.I_ch5_D502w =0 ;
            Feasa_10F_A.I_ch3_D501w = 0;
            Feasa_10F_A.I_ch2_D510w = 0;
            Feasa_10F_A.I_ch1_D500w = 0;
            Feasa_10F_A.I_ch4_D511w = 0;
            Feasa_10F_A.I_ch6_D631o = 0;
            Feasa_10F_A.W_ch6_D631o = 0;
            Feasa_10F_A.W_ch7_D701 = 0;
            Feasa_10F_A.I_ch7_D701 = 0;

            Feasa_10F_B.CCT_ch1_D507w = 0;
            Feasa_10F_B.CCT_ch2_D516w = 0;
            Feasa_10F_B.CCT_ch3_D525w = 0;
            Feasa_10F_B.CCT_ch4_D521w = 0;
            Feasa_10F_B.CCT_ch5_D520w = 0;
            Feasa_10F_B.CCT_ch6_D522w = 0;
            Feasa_10F_B.CCT_ch7_D526w = 0;
            Feasa_10F_B.CCT_ch8_D517w = 0;
            Feasa_10F_B.CCT_ch9_D508w = 0;
            Feasa_10F_B.I_ch1_D507w = 0;
            Feasa_10F_B.I_ch2_D516w = 0;
            Feasa_10F_B.I_ch3_D525w = 0;
            Feasa_10F_B.I_ch4_D521w = 0;
            Feasa_10F_B.I_ch5_D520w = 0;
            Feasa_10F_B.I_ch6_D522w = 0;
            Feasa_10F_B.I_ch7_D526w = 0;
            Feasa_10F_B.I_ch8_D517w = 0;
            Feasa_10F_B.I_ch9_D508w = 0;

            Feasa_10F_B.I_ch1_D505o = 0;
            Feasa_10F_B.I_ch2_D514o = 0;
            Feasa_10F_B.I_ch3_D523o = 0;
            Feasa_10F_B.I_ch4_D527o = 0;
            Feasa_10F_B.W_ch1_D505o = 0;
            Feasa_10F_B.W_ch2_D514o = 0;
            Feasa_10F_B.W_ch3_D523o = 0;
            Feasa_10F_B.W_ch4_D527o = 0;

            Feasa_10F_B.W_ch6_D528o = 0;
            Feasa_10F_B.W_ch7_D524o = 0;
            Feasa_10F_B.W_ch8_D515o = 0;
            Feasa_10F_B.W_ch9_D506o = 0;
            Feasa_10F_B.I_ch6_D528o = 0;
            Feasa_10F_B.I_ch7_D524o = 0;
            Feasa_10F_B.I_ch8_D515o = 0;
            Feasa_10F_B.I_ch9_D506o = 0;
        }
        public override WORK_STATUS Init()
        {
            Init_Feasa();

            sMeasured = "True";

            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UIstandby, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet1, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet2, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet3, 1);// on
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet4, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet5, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedError, 0);//organge
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedRTF, 1); //white
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UI_LED_BLE_R, 1);//37
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UI_LED_BLE_G, 0); //42
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UI_LED_BLE_B, 0);//18
            System.Threading.Thread.Sleep(200);

            try
            {
                myFS_LED1 = new Feasa_LED(Feasa_10F_A.comPort);
                myFS_LED2 = new Feasa_LED(Feasa_10F_B.comPort);
                CCT_Data cctDataA = new CCT_Data();
                CCT_Data cctDataB = new CCT_Data();
                WL_allData I_DataA = new WL_allData();
                WL_allData I_DataB = new WL_allData();
                myFS_LED1.doC4Capture();
                //myFS_LED1.doCapture();
                myFS_LED2.doC4Capture();
                //System.Threading.Thread.Sleep(1000);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedRTF, 0); //white
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedError, 1);//organge
                myFS_LED1.ReadCCT(1, cctDataA);
                myFS_LED2.ReadCCT(1, cctDataB);
                myFS_LED1.ReadWavelenall(I_DataA);
                myFS_LED2.ReadWavelenall(I_DataB);
                Load_FeasaData_W(cctDataA, cctDataB, I_DataA, I_DataB);
                //myFS_LED1.ReadXY(1, Feasa_10F_A.ch1_D500w);
                //myFS_LED1.ReadXY(2, Feasa_10F_A.ch2_D510w);
                //myFS_LED1.ReadXY(3, Feasa_10F_A.ch3_D501w);
                //myFS_LED1.ReadXY(4, Feasa_10F_A.ch4_D511w);
                //myFS_LED1.ReadXY(5, Feasa_10F_A.ch5_D502w);
                //myFS_LED1.ReadXY(6, Feasa_10F_A.ch6_D631o);


                //myFS_LED2.ReadXY(1, Feasa_10F_B.ch1_D507w);
                //myFS_LED2.ReadXY(2, Feasa_10F_B.ch2_D516w);
                //myFS_LED2.ReadXY(3, Feasa_10F_B.ch3_D525w);
                //myFS_LED2.ReadXY(4, Feasa_10F_B.ch4_D521w);
                //myFS_LED2.ReadXY(4, Feasa_10F_B.ch5_D520w);
                //myFS_LED2.ReadXY(6, Feasa_10F_B.ch6_D522w);
                //myFS_LED2.ReadXY(7, Feasa_10F_B.ch7_D526w);
                //myFS_LED2.ReadXY(8, Feasa_10F_B.ch8_D517w);
                //myFS_LED2.ReadXY(9, Feasa_10F_B.ch9_D508w);

                //Console.WriteLine("ch1_D500w = " + Feasa_10F_A.ch1_D500w.val_I_XY + " "
                //    + Feasa_10F_A.ch1_D500w.val_X + " " + Feasa_10F_A.ch1_D500w.val_Y);
                //Console.WriteLine("ch2_D510w = " + Feasa_10F_A.ch2_D510w.val_I_XY + " "
                //    + Feasa_10F_A.ch2_D510w.val_X + " " + Feasa_10F_A.ch2_D510w.val_Y);
                //Console.WriteLine("ch3_D501w = " + Feasa_10F_A.ch3_D501w.val_I_XY + " "
                //    + Feasa_10F_A.ch3_D501w.val_X + " " + Feasa_10F_A.ch3_D501w.val_Y);
                //Console.WriteLine("ch4_D511w = " + Feasa_10F_A.ch4_D511w.val_I_XY + " "
                //    + Feasa_10F_A.ch4_D511w.val_X + " " + Feasa_10F_A.ch4_D511w.val_Y);
                //Console.WriteLine("ch5_D502w = " + Feasa_10F_A.ch5_D502w.val_I_XY + " "
                //    + Feasa_10F_A.ch6_D631o.val_X + " " + Feasa_10F_A.ch5_D502w.val_Y);
                //Console.WriteLine("ch6_D612o = " + Feasa_10F_A.ch6_D631o.val_I_XY + " "
                //    + Feasa_10F_A.ch6_D631o.val_X + " " + Feasa_10F_A.ch6_D631o.val_Y);

                //Console.WriteLine(" B ch1_D507w = " + Feasa_10F_B.ch1_D507w.val_I_XY + " "
                //    + Feasa_10F_B.ch1_D507w.val_X + " " + Feasa_10F_B.ch1_D507w.val_Y);
                //Console.WriteLine(" B ch2_D516w = " + Feasa_10F_B.ch2_D516w.val_I_XY + " "
                //    + Feasa_10F_B.ch2_D516w.val_X + " " + Feasa_10F_B.ch2_D516w.val_Y);
                //Console.WriteLine(" B ch3_D525w = " + Feasa_10F_B.ch3_D525w.val_I_XY + " "
                //    + Feasa_10F_B.ch3_D525w.val_X + " " + Feasa_10F_B.ch3_D525w.val_Y);
                //Console.WriteLine(" B ch4_D521w = " + Feasa_10F_B.ch4_D521w.val_I_XY + " "
                //    + Feasa_10F_B.ch4_D521w.val_X + " " + Feasa_10F_B.ch4_D521w.val_Y);
                //Console.WriteLine(" B ch5_D520w = " + Feasa_10F_B.ch5_D520w.val_I_XY + " "
                //    + Feasa_10F_B.ch5_D520w.val_X + " " + Feasa_10F_B.ch5_D520w.val_Y);
                //Console.WriteLine(" B ch6_D522w = " + Feasa_10F_B.ch6_D522w.val_I_XY + " "
                //    + Feasa_10F_B.ch6_D522w.val_X + " " + Feasa_10F_B.ch6_D522w.val_Y);
                //Console.WriteLine(" B ch7_D526w = " + Feasa_10F_B.ch7_D526w.val_I_XY + " "
                //    + Feasa_10F_B.ch7_D526w.val_X + " " + Feasa_10F_B.ch7_D526w.val_Y);
                //Console.WriteLine(" B ch8_D517w = " + Feasa_10F_B.ch8_D517w.val_I_XY + " "
                //    + Feasa_10F_B.ch8_D517w.val_X + " " + Feasa_10F_B.ch8_D517w.val_Y);
                //Console.WriteLine(" B ch9_D508w = " + Feasa_10F_B.ch9_D508w.val_I_XY + " "
                //    + Feasa_10F_B.ch9_D508w.val_X + " " + Feasa_10F_B.ch9_D508w.val_Y);
                return WORK_STATUS.PASS;
            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
            //return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedRTF, 0); //white
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedError, 1);//organge
            //System.Threading.Thread.Sleep(500);
            try
            {
                WL_allData I_DataB = new WL_allData();
                myFS_LED2.doC4Capture();
                //System.Threading.Thread.Sleep(1000);

                myFS_LED2.ReadWavelenall(I_DataB);
                Load_FeasaData_O(I_DataB);
                //myFS_LED2.ReadXY(1, Feasa_10F_B.ch1_D505o);
                //myFS_LED2.ReadXY(2, Feasa_10F_B.ch2_D514o);
                //myFS_LED2.ReadXY(3, Feasa_10F_B.ch3_D523o);
                //myFS_LED2.ReadXY(4, Feasa_10F_B.ch4_D527o);
                //myFS_LED2.ReadXY(6, Feasa_10F_B.ch6_D528o);
                //myFS_LED2.ReadXY(7, Feasa_10F_B.ch7_D524o);
                //myFS_LED2.ReadXY(8, Feasa_10F_B.ch8_D515o);
                //myFS_LED2.ReadXY(9, Feasa_10F_B.ch9_D506o);

                //Console.WriteLine(" B ch1_D505o = " + Feasa_10F_B.ch1_D505o.val_I_XY + " "
                //    + Feasa_10F_B.ch1_D505o.val_X + " " + Feasa_10F_B.ch1_D505o.val_Y);
                //Console.WriteLine(" B ch2_D514o = " + Feasa_10F_B.ch2_D514o.val_I_XY + " "
                //    + Feasa_10F_B.ch2_D514o.val_X + " " + Feasa_10F_B.ch2_D514o.val_Y);
                //Console.WriteLine(" B ch3_D523o = " + Feasa_10F_B.ch3_D523o.val_I_XY + " "
                //    + Feasa_10F_B.ch3_D523o.val_X + " " + Feasa_10F_B.ch3_D523o.val_Y);
                //Console.WriteLine(" B ch4_D527o = " + Feasa_10F_B.ch4_D527o.val_I_XY + " "
                //    + Feasa_10F_B.ch4_D527o.val_X + " " + Feasa_10F_B.ch4_D527o.val_Y);

                //Console.WriteLine(" B ch6_D528o = " + Feasa_10F_B.ch6_D528o.val_I_XY + " "
                //    + Feasa_10F_B.ch6_D528o.val_X + " " + Feasa_10F_B.ch6_D528o.val_Y);
                //Console.WriteLine(" B ch7_D524o = " + Feasa_10F_B.ch7_D524o.val_I_XY + " "
                //    + Feasa_10F_B.ch7_D524o.val_X + " " + Feasa_10F_B.ch7_D524o.val_Y);
                //Console.WriteLine(" B ch8_D515o = " + Feasa_10F_B.ch8_D515o.val_I_XY + " "
                //    + Feasa_10F_B.ch8_D515o.val_X + " " + Feasa_10F_B.ch8_D515o.val_Y);
                //Console.WriteLine(" B ch9_D506o = " + Feasa_10F_B.ch9_D506o.val_I_XY + " "
                //    + Feasa_10F_B.ch9_D506o.val_X + " " + Feasa_10F_B.ch9_D506o.val_Y);

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
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UIstandby, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet1, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet2, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet3, 0);// on
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet4, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet5, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedError, 0);//organge
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedRTF, 0); //white
        }
    }


    class TestItemStep021_P3 : TestItemForm
    {
        public string step = "_021 P3";
        public TestItemStep021_P3()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            
            sMeasured = Convert.ToString(Feasa_10F_A.CCT_ch1_D500w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }
            
        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            //xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P2 : TestItemForm
    {
        public string step = "_021 P2";
        private XY_Data xy_Data;

        public TestItemStep021_P2()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            
            sMeasured = Convert.ToString(Feasa_10F_A.I_ch1_D500w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P4 : TestItemForm
    {
        public string step = "_021 P4";
        private XY_Data xy_Data;

        public TestItemStep021_P4()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            
            sMeasured = Convert.ToString(Feasa_10F_A.I_ch2_D510w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P5 : TestItemForm
    {
        public string step = "_021 P5";
        private XY_Data xy_Data;

        public TestItemStep021_P5()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            
            sMeasured = Convert.ToString(Feasa_10F_A.CCT_ch2_D510w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P6 : TestItemForm
    {
        public string step = "_021 P6";
        private XY_Data xy_Data;

        public TestItemStep021_P6()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_A.ch2_D510w;
            sMeasured = Convert.ToString(Feasa_10F_A.I_ch3_D501w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P7 : TestItemForm
    {
        public string step = "_021 P7";
        private XY_Data xy_Data;

        public TestItemStep021_P7()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_A.ch2_D510w;
            sMeasured = Convert.ToString(Feasa_10F_A.CCT_ch3_D501w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P8 : TestItemForm
    {
        public string step = "_021 P8";
        private XY_Data xy_Data;

        public TestItemStep021_P8()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_A.ch3_D501w;
            sMeasured = Convert.ToString(Feasa_10F_A.I_ch4_D511w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P9 : TestItemForm
    {
        public string step = "_021 P9";
        private XY_Data xy_Data;

        public TestItemStep021_P9()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_A.ch3_D501w;
            sMeasured = Convert.ToString(Feasa_10F_A.CCT_ch3_D501w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P10 : TestItemForm
    {
        public string step = "_021 P10";
        private XY_Data xy_Data;

        public TestItemStep021_P10()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_A.ch3_D501w;
            sMeasured = Convert.ToString(Feasa_10F_A.I_ch5_D502w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P11 : TestItemForm
    {
        public string step = "_021 P11";
    private XY_Data xy_Data;

    public TestItemStep021_P11()
    {
        sStep = step;
        LoadSpec();
    }

    public override WORK_STATUS Init()
    {
        //xy_Data = new XY_Data();
        //xy_Data = Feasa_10F_A.ch4_D511w;
        sMeasured = Convert.ToString(Feasa_10F_A.CCT_ch5_D502w);
        if (PassFailJudge())
        {
            return WORK_STATUS.PASS;
        }
        else
        {
            return WORK_STATUS.FAIL;
        }

    }
    public override WORK_STATUS Proccess()
    {
        return WORK_STATUS.PASS;
    }

    public override void release()
    {
        xy_Data = null;
        //Feasa_10F_A.ch1_D500w = null;
    }
}

    class TestItemStep021_P12 : TestItemForm
{
        public string step = "_021 P12";
        private XY_Data xy_Data;

        public TestItemStep021_P12()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_A.ch4_D511w;
            sMeasured = Convert.ToString(Feasa_10F_B.I_ch1_D505o);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

         }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P13  : TestItemForm
    {
        public string step = "_021 P13";
        private XY_Data xy_Data;

        public TestItemStep021_P13()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_A.ch4_D511w;
            sMeasured = Convert.ToString(Feasa_10F_B.W_ch1_D505o);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P14 : TestItemForm
    {
        public string step = "_021 P14";
        private XY_Data xy_Data;

        public TestItemStep021_P14()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_A.ch5_D502w;
            sMeasured = Convert.ToString(Feasa_10F_B.I_ch2_D514o);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P15 : TestItemForm
    {
        public string step = "_021 P15";
        private XY_Data xy_Data;

        public TestItemStep021_P15()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_A.ch5_D502w;
            sMeasured = Convert.ToString(Feasa_10F_B.W_ch2_D514o);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P16 : TestItemForm
    {
        public string step = "_021 P16";
        private XY_Data xy_Data;

        public TestItemStep021_P16()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_A.ch5_D502w;
            sMeasured = Convert.ToString(Feasa_10F_B.I_ch3_D523o);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P17 : TestItemForm
    {
        public string step = "_021 P17";
        private XY_Data xy_Data;

        public TestItemStep021_P17()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch1_D505o;
            sMeasured = Convert.ToString(Feasa_10F_B.W_ch3_D523o);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P18 : TestItemForm
    {
        public string step = "_021 P18";
        private XY_Data xy_Data;

        public TestItemStep021_P18()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch1_D505o;
            sMeasured = Convert.ToString(Feasa_10F_B.I_ch4_D527o);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P19 : TestItemForm
    {
        public string step = "_021 P19";
        private XY_Data xy_Data;

        public TestItemStep021_P19()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch1_D505o;
            sMeasured = Convert.ToString(Feasa_10F_B.W_ch4_D527o);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P20 : TestItemForm
    {
        public string step = "_021 P20";
        private XY_Data xy_Data;

        public TestItemStep021_P20()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch2_D514o;
            sMeasured = Convert.ToString(Feasa_10F_B.I_ch6_D528o);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P21 : TestItemForm
    {
        public string step = "_021 P21";
        private XY_Data xy_Data;

        public TestItemStep021_P21()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch2_D514o;
            sMeasured = Convert.ToString(Feasa_10F_B.W_ch6_D528o);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P22 : TestItemForm
    {
        public string step = "_021 P22";
        private XY_Data xy_Data;

        public TestItemStep021_P22()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch2_D514o;
            sMeasured = Convert.ToString(Feasa_10F_B.I_ch7_D524o);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }


    class TestItemStep021_P23 : TestItemForm
    {
        public string step = "_021 P23";
        private XY_Data xy_Data;

        public TestItemStep021_P23()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch3_D523o;
            sMeasured = Convert.ToString(Feasa_10F_B.W_ch7_D524o);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P24 : TestItemForm
    {
        public string step = "_021 P24";
        private XY_Data xy_Data;

        public TestItemStep021_P24()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch3_D523o;
            sMeasured = Convert.ToString(Feasa_10F_B.I_ch8_D515o);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P25 : TestItemForm
    {
        public string step = "_021 P25";
        private XY_Data xy_Data;

        public TestItemStep021_P25()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch3_D523o;
            sMeasured = Convert.ToString(Feasa_10F_B.W_ch8_D515o);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P26 : TestItemForm
    {
        public string step = "_021 P26";
        private XY_Data xy_Data;

        public TestItemStep021_P26()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch4_D527o;
            sMeasured = Convert.ToString(Feasa_10F_B.I_ch9_D506o);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P27 : TestItemForm
    {
        public string step = "_021 P27";
        private XY_Data xy_Data;

        public TestItemStep021_P27()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch4_D527o;
            sMeasured = Convert.ToString(Feasa_10F_B.W_ch9_D506o);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P28 : TestItemForm
    {
        public string step = "_021 P28";
        private XY_Data xy_Data;

        public TestItemStep021_P28()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch4_D527o;
            sMeasured = Convert.ToString(Feasa_10F_B.I_ch1_D507w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P29 : TestItemForm
    {
        public string step = "_021 P29";
        private XY_Data xy_Data;

        public TestItemStep021_P29()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch6_D528o;
            sMeasured = Convert.ToString(Feasa_10F_B.CCT_ch1_D507w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P30 : TestItemForm
    {
        public string step = "_021 P30";
        private XY_Data xy_Data;

        public TestItemStep021_P30()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch6_D528o;
            sMeasured = Convert.ToString(Feasa_10F_B.I_ch2_D516w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P31 : TestItemForm
    {
        public string step = "_021 P31";
        private XY_Data xy_Data;

        public TestItemStep021_P31()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch6_D528o;
            sMeasured = Convert.ToString(Feasa_10F_B.CCT_ch2_D516w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P32 : TestItemForm
    {
        public string step = "_021 P32";
        private XY_Data xy_Data;

        public TestItemStep021_P32()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch7_D524o;
            sMeasured = Convert.ToString(Feasa_10F_B.I_ch3_D525w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P33 : TestItemForm
    {
        public string step = "_021 P33";
        private XY_Data xy_Data;

        public TestItemStep021_P33()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch7_D524o;
            sMeasured = Convert.ToString(Feasa_10F_B.CCT_ch3_D525w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P34 : TestItemForm
    {
        public string step = "_021 P34";
        private XY_Data xy_Data;

        public TestItemStep021_P34()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch7_D524o;
            sMeasured = Convert.ToString(Feasa_10F_B.I_ch4_D521w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P35 : TestItemForm
    {
        public string step = "_021 P35";
        private XY_Data xy_Data;

        public TestItemStep021_P35()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch8_D515o;
            sMeasured = Convert.ToString(Feasa_10F_B.CCT_ch4_D521w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P36 : TestItemForm
    {
        public string step = "_021 P36";
        private XY_Data xy_Data;

        public TestItemStep021_P36()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch8_D515o;
            sMeasured = Convert.ToString(Feasa_10F_B.I_ch5_D520w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P37 : TestItemForm
    {
        public string step = "_021 P37";
        private XY_Data xy_Data;

        public TestItemStep021_P37()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch8_D515o;
            sMeasured = Convert.ToString(Feasa_10F_B.CCT_ch5_D520w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P38 : TestItemForm
    {
        public string step = "_021 P38";
        private XY_Data xy_Data;

        public TestItemStep021_P38()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch9_D506o;
            sMeasured = Convert.ToString(Feasa_10F_B.I_ch6_D522w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P39 : TestItemForm
    {
        public string step = "_021 P39";
        private XY_Data xy_Data;

        public TestItemStep021_P39()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch9_D506o;
            sMeasured = Convert.ToString(Feasa_10F_B.CCT_ch6_D522w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P40 : TestItemForm
    {
        public string step = "_021 P40";
        private XY_Data xy_Data;

        public TestItemStep021_P40()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch9_D506o;
            sMeasured = Convert.ToString(Feasa_10F_B.I_ch7_D526w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P41 : TestItemForm
    {
        public string step = "_021 P41";
        private XY_Data xy_Data;

        public TestItemStep021_P41()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch1_D507w;
            sMeasured = Convert.ToString(Feasa_10F_B.CCT_ch7_D526w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P42 : TestItemForm
    {
        public string step = "_021 P42";
        private XY_Data xy_Data;

        public TestItemStep021_P42()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch1_D507w;
            sMeasured = Convert.ToString(Feasa_10F_B.I_ch8_D517w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P43 : TestItemForm
    {
        public string step = "_021 P43";
        private XY_Data xy_Data;

        public TestItemStep021_P43()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch1_D507w;
            sMeasured = Convert.ToString(Feasa_10F_B.CCT_ch8_D517w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P44 : TestItemForm
    {
        public string step = "_021 P44";
        private XY_Data xy_Data;

        public TestItemStep021_P44()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch2_D516w;
            sMeasured = Convert.ToString(Feasa_10F_B.I_ch9_D508w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P45 : TestItemForm
    {
        public string step = "_021 P45";
        private XY_Data xy_Data;

        public TestItemStep021_P45()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch2_D516w;
            sMeasured = Convert.ToString(Feasa_10F_B.CCT_ch9_D508w);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P46 : TestItemForm
    {
        public string step = "_021 P46";
        private XY_Data xy_Data;

        public TestItemStep021_P46()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch2_D516w;
            sMeasured = Convert.ToString(Feasa_10F_A.I_ch6_D631o);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P47 : TestItemForm
    {
        public string step = "_021 P47";
        private XY_Data xy_Data;

        public TestItemStep021_P47()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //xy_Data = new XY_Data();
            //xy_Data = Feasa_10F_B.ch3_D525w;
            sMeasured = Convert.ToString(Feasa_10F_A.W_ch6_D631o);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }
        public override WORK_STATUS Proccess()
        {
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            xy_Data = null;
            //Feasa_10F_A.ch1_D500w = null;
        }
    }

    class TestItemStep021_P48 : TestItemForm
    {
        public string step = "_021 P48";

        public TestItemStep021_P48()
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
            sMeasured = Convert.ToString(Feasa_10F_A.I_ch7_D701);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }
            return WORK_STATUS.PASS;
        }

        public override void release()
        {

        }
    }
    class TestItemStep021_P49 : TestItemForm
    {
        public string step = "_021 P49";

        public TestItemStep021_P49()
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
            sMeasured = Convert.ToString(Feasa_10F_A.W_ch7_D701);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }
            return WORK_STATUS.PASS;
        }

        public override void release()
        {

        }
    }

    class TestItemStep021_P50 : TestItemForm
    {
        public string step = "_021 P50"; // D701 G
        private Feasa_LED myFS_LED1;
        private Feasa_LED myFS_LED2;

        public TestItemStep021_P50()
        {
            sStep = step;
            LoadSpec();
        }
        private void Load_FeasaData_W(CCT_Data cctA, CCT_Data cctB, WL_allData wlA, WL_allData wlB)
        {
            Feasa_10F_A.CCT_ch5_D502w = cctA.val_CCT2;
            Feasa_10F_A.CCT_ch3_D501w = cctA.val_CCT4;
            Feasa_10F_A.CCT_ch2_D510w = cctA.val_CCT5;
            Feasa_10F_A.CCT_ch1_D500w = cctA.val_CCT6;
            Feasa_10F_A.CCT_ch4_D511w = cctA.val_CCT3;
            Feasa_10F_A.I_ch5_D502w = wlA.val_I_WL2;
            Feasa_10F_A.I_ch3_D501w = wlA.val_I_WL4;
            Feasa_10F_A.I_ch2_D510w = wlA.val_I_WL5;
            Feasa_10F_A.I_ch1_D500w = wlA.val_I_WL6;
            Feasa_10F_A.I_ch4_D511w = wlA.val_I_WL4;
            Feasa_10F_A.I_ch6_D631o = wlA.val_I_WL1;
            Feasa_10F_A.W_ch6_D631o = wlA.val_WL1;
            Feasa_10F_A.I_ch7_D701 = wlA.val_I_WL7;
            Feasa_10F_A.W_ch7_D701 = wlA.val_WL7;
            Feasa_10F_B.CCT_ch1_D507w = cctB.val_CCT9;
            Feasa_10F_B.CCT_ch2_D516w = cctB.val_CCT8;
            Feasa_10F_B.CCT_ch3_D525w = cctB.val_CCT7;
            Feasa_10F_B.CCT_ch4_D521w = cctB.val_CCT6;
            Feasa_10F_B.CCT_ch5_D520w = cctB.val_CCT5;
            Feasa_10F_B.CCT_ch6_D522w = cctB.val_CCT4;
            Feasa_10F_B.CCT_ch7_D526w = cctB.val_CCT3;
            Feasa_10F_B.CCT_ch8_D517w = cctB.val_CCT2;
            Feasa_10F_B.CCT_ch9_D508w = cctB.val_CCT1;
            Feasa_10F_B.I_ch1_D507w = wlB.val_I_WL9;
            Feasa_10F_B.I_ch2_D516w = wlB.val_I_WL8;
            Feasa_10F_B.I_ch3_D525w = wlB.val_I_WL7;
            Feasa_10F_B.I_ch4_D521w = wlB.val_I_WL6;
            Feasa_10F_B.I_ch5_D520w = wlB.val_I_WL5;
            Feasa_10F_B.I_ch6_D522w = wlB.val_I_WL4;
            Feasa_10F_B.I_ch7_D526w = wlB.val_I_WL3;
            Feasa_10F_B.I_ch8_D517w = wlB.val_I_WL2;
            Feasa_10F_B.I_ch9_D508w = wlB.val_I_WL1;


        }
        private void Load_FeasaData_O(WL_allData wlB)
        {

            //Feasa_10F_A.I_ch6_D631o = wlA.val_I_WL6;
            //Feasa_10F_A.W_ch6_D631o = wlA.val_WL6;

            Feasa_10F_B.I_ch1_D505o = wlB.val_I_WL9;
            Feasa_10F_B.I_ch2_D514o = wlB.val_I_WL8;
            Feasa_10F_B.I_ch3_D523o = wlB.val_I_WL7;
            Feasa_10F_B.I_ch4_D527o = wlB.val_I_WL6;
            Feasa_10F_B.W_ch1_D505o = wlB.val_WL9;
            Feasa_10F_B.W_ch2_D514o = wlB.val_WL8;
            Feasa_10F_B.W_ch3_D523o = wlB.val_WL7;
            Feasa_10F_B.W_ch4_D527o = wlB.val_WL6;

            Feasa_10F_B.W_ch6_D528o = wlB.val_WL4;
            Feasa_10F_B.W_ch7_D524o = wlB.val_WL3;
            Feasa_10F_B.W_ch8_D515o = wlB.val_WL2;
            Feasa_10F_B.W_ch9_D506o = wlB.val_WL1;
            Feasa_10F_B.I_ch6_D528o = wlB.val_I_WL4;
            Feasa_10F_B.I_ch7_D524o = wlB.val_I_WL3;
            Feasa_10F_B.I_ch8_D515o = wlB.val_I_WL2;
            Feasa_10F_B.I_ch9_D506o = wlB.val_I_WL1;

        }
        private void Init_Feasa()
        {
            Feasa_10F_A.CCT_ch5_D502w = 0;
            Feasa_10F_A.CCT_ch3_D501w = 0;
            Feasa_10F_A.CCT_ch2_D510w = 0;
            Feasa_10F_A.CCT_ch1_D500w = 0;
            Feasa_10F_A.CCT_ch4_D511w = 0;
            Feasa_10F_A.I_ch5_D502w = 0;
            Feasa_10F_A.I_ch3_D501w = 0;
            Feasa_10F_A.I_ch2_D510w = 0;
            Feasa_10F_A.I_ch1_D500w = 0;
            Feasa_10F_A.I_ch4_D511w = 0;
            Feasa_10F_A.I_ch6_D631o = 0;
            Feasa_10F_A.W_ch6_D631o = 0;
            Feasa_10F_A.W_ch7_D701 = 0;
            Feasa_10F_A.I_ch7_D701 = 0;

            Feasa_10F_B.CCT_ch1_D507w = 0;
            Feasa_10F_B.CCT_ch2_D516w = 0;
            Feasa_10F_B.CCT_ch3_D525w = 0;
            Feasa_10F_B.CCT_ch4_D521w = 0;
            Feasa_10F_B.CCT_ch5_D520w = 0;
            Feasa_10F_B.CCT_ch6_D522w = 0;
            Feasa_10F_B.CCT_ch7_D526w = 0;
            Feasa_10F_B.CCT_ch8_D517w = 0;
            Feasa_10F_B.CCT_ch9_D508w = 0;
            Feasa_10F_B.I_ch1_D507w = 0;
            Feasa_10F_B.I_ch2_D516w = 0;
            Feasa_10F_B.I_ch3_D525w = 0;
            Feasa_10F_B.I_ch4_D521w = 0;
            Feasa_10F_B.I_ch5_D520w = 0;
            Feasa_10F_B.I_ch6_D522w = 0;
            Feasa_10F_B.I_ch7_D526w = 0;
            Feasa_10F_B.I_ch8_D517w = 0;
            Feasa_10F_B.I_ch9_D508w = 0;

            Feasa_10F_B.I_ch1_D505o = 0;
            Feasa_10F_B.I_ch2_D514o = 0;
            Feasa_10F_B.I_ch3_D523o = 0;
            Feasa_10F_B.I_ch4_D527o = 0;
            Feasa_10F_B.W_ch1_D505o = 0;
            Feasa_10F_B.W_ch2_D514o = 0;
            Feasa_10F_B.W_ch3_D523o = 0;
            Feasa_10F_B.W_ch4_D527o = 0;

            Feasa_10F_B.W_ch6_D528o = 0;
            Feasa_10F_B.W_ch7_D524o = 0;
            Feasa_10F_B.W_ch8_D515o = 0;
            Feasa_10F_B.W_ch9_D506o = 0;
            Feasa_10F_B.I_ch6_D528o = 0;
            Feasa_10F_B.I_ch7_D524o = 0;
            Feasa_10F_B.I_ch8_D515o = 0;
            Feasa_10F_B.I_ch9_D506o = 0;
        }
        public override WORK_STATUS Init()
        {
            Init_Feasa();

            sMeasured = "True";

            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UIstandby, 0);
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet1, 1);
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet2, 1);
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet3, 1);// on
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet4, 1);
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet5, 1);
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedError, 0);//organge
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedRTF, 1); //white
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UI_LED_BLE_R, 0);//37
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UI_LED_BLE_G, 1); //42
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UI_LED_BLE_B, 0);//18
            System.Threading.Thread.Sleep(200);

            try
            {
                myFS_LED1 = new Feasa_LED(Feasa_10F_A.comPort);
                myFS_LED2 = new Feasa_LED(Feasa_10F_B.comPort);
                CCT_Data cctDataA = new CCT_Data();
                CCT_Data cctDataB = new CCT_Data();
                WL_allData I_DataA = new WL_allData();
                WL_allData I_DataB = new WL_allData();

                myFS_LED1.doC4Capture();
                //myFS_LED1.doCapture();
                myFS_LED2.doC4Capture();

                myFS_LED1.ReadCCT(1, cctDataA);
                myFS_LED2.ReadCCT(1, cctDataB);
                myFS_LED1.ReadWavelenall(I_DataA);
                myFS_LED2.ReadWavelenall(I_DataB);
                
                Load_FeasaData_W(cctDataA, cctDataB, I_DataA, I_DataB);


                sMeasured = Convert.ToString(Feasa_10F_A.I_ch7_D701);
                if (PassFailJudge())
                {
                    return WORK_STATUS.PASS;
                }
                else
                {
                    return WORK_STATUS.FAIL;
                }


                return WORK_STATUS.PASS;
            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
            //return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
    
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

    class TestItemStep021_P51 : TestItemForm
    {
        public string step = "_021 P51";

        public TestItemStep021_P51()
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
            sMeasured = Convert.ToString(Feasa_10F_A.W_ch7_D701);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }
            return WORK_STATUS.PASS;
        }

        public override void release()
        {

        }
    }

    class TestItemStep021_P52 : TestItemForm
    {
        public string step = "_021 P52";
        private Feasa_LED myFS_LED1;
        private Feasa_LED myFS_LED2;

        public TestItemStep021_P52()
        {
            sStep = step;
            LoadSpec();
        }
        private void Load_FeasaData_W(CCT_Data cctA, CCT_Data cctB, WL_allData wlA, WL_allData wlB)
        {
            Feasa_10F_A.CCT_ch5_D502w = cctA.val_CCT2;
            Feasa_10F_A.CCT_ch3_D501w = cctA.val_CCT4;
            Feasa_10F_A.CCT_ch2_D510w = cctA.val_CCT5;
            Feasa_10F_A.CCT_ch1_D500w = cctA.val_CCT6;
            Feasa_10F_A.CCT_ch4_D511w = cctA.val_CCT3;
            Feasa_10F_A.I_ch5_D502w = wlA.val_I_WL2;
            Feasa_10F_A.I_ch3_D501w = wlA.val_I_WL4;
            Feasa_10F_A.I_ch2_D510w = wlA.val_I_WL5;
            Feasa_10F_A.I_ch1_D500w = wlA.val_I_WL6;
            Feasa_10F_A.I_ch4_D511w = wlA.val_I_WL4;
            Feasa_10F_A.I_ch6_D631o = wlA.val_I_WL1;
            Feasa_10F_A.W_ch6_D631o = wlA.val_WL1;
            Feasa_10F_A.I_ch7_D701 = wlA.val_I_WL7;
            Feasa_10F_A.W_ch7_D701 = wlA.val_WL7;
            Feasa_10F_B.CCT_ch1_D507w = cctB.val_CCT9;
            Feasa_10F_B.CCT_ch2_D516w = cctB.val_CCT8;
            Feasa_10F_B.CCT_ch3_D525w = cctB.val_CCT7;
            Feasa_10F_B.CCT_ch4_D521w = cctB.val_CCT6;
            Feasa_10F_B.CCT_ch5_D520w = cctB.val_CCT5;
            Feasa_10F_B.CCT_ch6_D522w = cctB.val_CCT4;
            Feasa_10F_B.CCT_ch7_D526w = cctB.val_CCT3;
            Feasa_10F_B.CCT_ch8_D517w = cctB.val_CCT2;
            Feasa_10F_B.CCT_ch9_D508w = cctB.val_CCT1;
            Feasa_10F_B.I_ch1_D507w = wlB.val_I_WL9;
            Feasa_10F_B.I_ch2_D516w = wlB.val_I_WL8;
            Feasa_10F_B.I_ch3_D525w = wlB.val_I_WL7;
            Feasa_10F_B.I_ch4_D521w = wlB.val_I_WL6;
            Feasa_10F_B.I_ch5_D520w = wlB.val_I_WL5;
            Feasa_10F_B.I_ch6_D522w = wlB.val_I_WL4;
            Feasa_10F_B.I_ch7_D526w = wlB.val_I_WL3;
            Feasa_10F_B.I_ch8_D517w = wlB.val_I_WL2;
            Feasa_10F_B.I_ch9_D508w = wlB.val_I_WL1;


        }
        private void Load_FeasaData_O(WL_allData wlB)
        {

            //Feasa_10F_A.I_ch6_D631o = wlA.val_I_WL6;
            //Feasa_10F_A.W_ch6_D631o = wlA.val_WL6;

            Feasa_10F_B.I_ch1_D505o = wlB.val_I_WL9;
            Feasa_10F_B.I_ch2_D514o = wlB.val_I_WL8;
            Feasa_10F_B.I_ch3_D523o = wlB.val_I_WL7;
            Feasa_10F_B.I_ch4_D527o = wlB.val_I_WL6;
            Feasa_10F_B.W_ch1_D505o = wlB.val_WL9;
            Feasa_10F_B.W_ch2_D514o = wlB.val_WL8;
            Feasa_10F_B.W_ch3_D523o = wlB.val_WL7;
            Feasa_10F_B.W_ch4_D527o = wlB.val_WL6;

            Feasa_10F_B.W_ch6_D528o = wlB.val_WL4;
            Feasa_10F_B.W_ch7_D524o = wlB.val_WL3;
            Feasa_10F_B.W_ch8_D515o = wlB.val_WL2;
            Feasa_10F_B.W_ch9_D506o = wlB.val_WL1;
            Feasa_10F_B.I_ch6_D528o = wlB.val_I_WL4;
            Feasa_10F_B.I_ch7_D524o = wlB.val_I_WL3;
            Feasa_10F_B.I_ch8_D515o = wlB.val_I_WL2;
            Feasa_10F_B.I_ch9_D506o = wlB.val_I_WL1;

        }
        private void Init_Feasa()
        {
            Feasa_10F_A.CCT_ch5_D502w = 0;
            Feasa_10F_A.CCT_ch3_D501w = 0;
            Feasa_10F_A.CCT_ch2_D510w = 0;
            Feasa_10F_A.CCT_ch1_D500w = 0;
            Feasa_10F_A.CCT_ch4_D511w = 0;
            Feasa_10F_A.I_ch5_D502w = 0;
            Feasa_10F_A.I_ch3_D501w = 0;
            Feasa_10F_A.I_ch2_D510w = 0;
            Feasa_10F_A.I_ch1_D500w = 0;
            Feasa_10F_A.I_ch4_D511w = 0;
            Feasa_10F_A.I_ch6_D631o = 0;
            Feasa_10F_A.W_ch6_D631o = 0;
            Feasa_10F_A.W_ch7_D701 = 0;
            Feasa_10F_A.I_ch7_D701 = 0;

            Feasa_10F_B.CCT_ch1_D507w = 0;
            Feasa_10F_B.CCT_ch2_D516w = 0;
            Feasa_10F_B.CCT_ch3_D525w = 0;
            Feasa_10F_B.CCT_ch4_D521w = 0;
            Feasa_10F_B.CCT_ch5_D520w = 0;
            Feasa_10F_B.CCT_ch6_D522w = 0;
            Feasa_10F_B.CCT_ch7_D526w = 0;
            Feasa_10F_B.CCT_ch8_D517w = 0;
            Feasa_10F_B.CCT_ch9_D508w = 0;
            Feasa_10F_B.I_ch1_D507w = 0;
            Feasa_10F_B.I_ch2_D516w = 0;
            Feasa_10F_B.I_ch3_D525w = 0;
            Feasa_10F_B.I_ch4_D521w = 0;
            Feasa_10F_B.I_ch5_D520w = 0;
            Feasa_10F_B.I_ch6_D522w = 0;
            Feasa_10F_B.I_ch7_D526w = 0;
            Feasa_10F_B.I_ch8_D517w = 0;
            Feasa_10F_B.I_ch9_D508w = 0;

            Feasa_10F_B.I_ch1_D505o = 0;
            Feasa_10F_B.I_ch2_D514o = 0;
            Feasa_10F_B.I_ch3_D523o = 0;
            Feasa_10F_B.I_ch4_D527o = 0;
            Feasa_10F_B.W_ch1_D505o = 0;
            Feasa_10F_B.W_ch2_D514o = 0;
            Feasa_10F_B.W_ch3_D523o = 0;
            Feasa_10F_B.W_ch4_D527o = 0;

            Feasa_10F_B.W_ch6_D528o = 0;
            Feasa_10F_B.W_ch7_D524o = 0;
            Feasa_10F_B.W_ch8_D515o = 0;
            Feasa_10F_B.W_ch9_D506o = 0;
            Feasa_10F_B.I_ch6_D528o = 0;
            Feasa_10F_B.I_ch7_D524o = 0;
            Feasa_10F_B.I_ch8_D515o = 0;
            Feasa_10F_B.I_ch9_D506o = 0;
        }
        public override WORK_STATUS Init()
        {
            Init_Feasa();

            sMeasured = "True";

            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UIstandby, 0);
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet1, 1);
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet2, 1);
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet3, 1);// on
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet4, 1);
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet5, 1);
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedError, 0);//organge
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedRTF, 1); //white
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UI_LED_BLE_R, 0);//37
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UI_LED_BLE_G, 0); //42
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UI_LED_BLE_B, 1);//18
            System.Threading.Thread.Sleep(200);

            try
            {
                myFS_LED1 = new Feasa_LED(Feasa_10F_A.comPort);
                myFS_LED2 = new Feasa_LED(Feasa_10F_B.comPort);
                CCT_Data cctDataA = new CCT_Data();
                CCT_Data cctDataB = new CCT_Data();
                WL_allData I_DataA = new WL_allData();
                WL_allData I_DataB = new WL_allData();

                myFS_LED1.doC4Capture();
                //myFS_LED1.doCapture();
                myFS_LED2.doC4Capture();
                myFS_LED1.ReadCCT(1, cctDataA);
                myFS_LED2.ReadCCT(1, cctDataB);
                myFS_LED1.ReadWavelenall(I_DataA);
                myFS_LED2.ReadWavelenall(I_DataB);

                Load_FeasaData_W(cctDataA, cctDataB, I_DataA, I_DataB);


                sMeasured = Convert.ToString(Feasa_10F_A.I_ch7_D701);
                if (PassFailJudge())
                {
                    return WORK_STATUS.PASS;
                }
                else
                {
                    return WORK_STATUS.FAIL;
                }


                return WORK_STATUS.PASS;
            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
            //return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {

            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            myFS_LED1.Close();
            myFS_LED1 = null;
            myFS_LED2.Close();
            myFS_LED2 = null;
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UIstandby, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet1, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet2, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet3, 0);// on
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet4, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet5, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedError, 0);//organge
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedRTF, 0); //white
        }
    }

    class TestItemStep021_P53 : TestItemForm
    {
        public string step = "_021 P53";

        public TestItemStep021_P53()
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
            sMeasured = Convert.ToString(Feasa_10F_A.W_ch7_D701);
            if (PassFailJudge())
            {
                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }
            return WORK_STATUS.PASS;
        }

        public override void release()
        {

        }
    }
}
