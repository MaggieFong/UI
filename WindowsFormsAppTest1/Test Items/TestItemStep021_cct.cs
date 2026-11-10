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
        private void Init_Feasa()
        {
            Feasa_10F_A.ch1_D500w =0;
            Feasa_10F_A.ch2_D510w = 0;
            Feasa_10F_A.ch3_D501w = 0;
            Feasa_10F_A.ch4_D511w = 0;
            Feasa_10F_A.ch5_D502w = 0;
            Feasa_10F_A.ch6_D631o = 0;


            Feasa_10F_B.ch1_D507w = 0;
            Feasa_10F_B.ch2_D516w = 0;
            Feasa_10F_B.ch3_D525w = 0;
            Feasa_10F_B.ch4_D521w = 0;
            Feasa_10F_B.ch5_D520w = 0;
            Feasa_10F_B.ch6_D522w = 0;
            Feasa_10F_B.ch7_D526w = 0;
            Feasa_10F_B.ch8_D517w = 0;
            Feasa_10F_B.ch9_D508w = 0;

            Feasa_10F_B.ch1_D505o = 0;
            Feasa_10F_B.ch2_D514o = 0;
            Feasa_10F_B.ch3_D523o = 0;
            Feasa_10F_B.ch4_D527o = 0;
            Feasa_10F_B.ch6_D528o = 0;
            Feasa_10F_B.ch7_D524o = 0;
            Feasa_10F_B.ch8_D515o = 0;
            Feasa_10F_B.ch9_D506o = 0;

           
        }
        public override WORK_STATUS Init()
        {
            Init_Feasa();
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UIstandby, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet1, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet2, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet3, 1);// on
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet4, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet5, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedError, 0);//organge
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedRTF, 1); //white
            try
            {
                myFS_LED1 = new Feasa_LED(Feasa_10F_A.comPort);
                myFS_LED2 = new Feasa_LED(Feasa_10F_B.comPort);
                myFS_LED1.doC3Capture();
                myFS_LED2.doC3Capture();
                CCT_Data cctdataA = new CCT_Data();
                CCT_Data cctdataB = new CCT_Data();
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
                myFS_LED1.ReadCCT(1, cctdataA);
                myFS_LED2.ReadCCT(1, cctdataB);
                Feasa_10F_A.ch1_D500w = cctdataA.val_CCT1;
                Feasa_10F_A.ch2_D510w = cctdataA.val_CCT2;
                Feasa_10F_A.ch3_D501w = cctdataA.val_CCT3;
                Feasa_10F_A.ch4_D511w = cctdataA.val_CCT4;
                Feasa_10F_A.ch5_D502w = cctdataA.val_CCT5;
                Feasa_10F_A.ch6_D631o = cctdataA.val_CCT6;

                Feasa_10F_B.ch1_D507w = cctdataB.val_CCT1;
                Feasa_10F_B.ch2_D516w = cctdataB.val_CCT2;
                Feasa_10F_B.ch3_D525w = cctdataB.val_CCT3;
                Feasa_10F_B.ch4_D521w = cctdataB.val_CCT4;
                Feasa_10F_B.ch5_D520w = cctdataB.val_CCT5;
                Feasa_10F_B.ch6_D522w = cctdataB.val_CCT6;
                Feasa_10F_B.ch7_D526w = cctdataB.val_CCT7;
                Feasa_10F_B.ch8_D517w = cctdataB.val_CCT8;
                Feasa_10F_B.ch9_D508w = cctdataB.val_CCT9;
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
            try
            {
                CCT_Data cctdataB = new CCT_Data();
                myFS_LED2.doC3Capture();
                //myFS_LED1.ReadCCT(1, cctdataA);
                myFS_LED2.ReadCCT(1, cctdataB);
                Feasa_10F_B.ch1_D505o = cctdataB.val_CCT1;
                Feasa_10F_B.ch2_D514o = cctdataB.val_CCT2;
                Feasa_10F_B.ch3_D523o = cctdataB.val_CCT3;
                Feasa_10F_B.ch4_D527o = cctdataB.val_CCT4;
                
                Feasa_10F_B.ch6_D528o = cctdataB.val_CCT6;
                Feasa_10F_B.ch7_D524o = cctdataB.val_CCT7;
                Feasa_10F_B.ch8_D515o = cctdataB.val_CCT8;
                Feasa_10F_B.ch9_D506o = cctdataB.val_CCT9;
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_A.ch1_D500w;
            sMeasured = Convert.ToString(xy_Data.val_I_XY);
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

    class TestItemStep021_P3 : TestItemForm
    {
        public string step = "_021 P3";
        private XY_Data xy_Data;

        public TestItemStep021_P3()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_A.ch1_D500w;
            sMeasured = Convert.ToString(xy_Data.val_X);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_A.ch1_D500w;
            sMeasured = Convert.ToString(xy_Data.val_Y);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_A.ch2_D510w;
            sMeasured = Convert.ToString(xy_Data.val_I_XY);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_A.ch2_D510w;
            sMeasured = Convert.ToString(xy_Data.val_X);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_A.ch2_D510w;
            sMeasured = Convert.ToString(xy_Data.val_Y);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_A.ch3_D501w;
            sMeasured = Convert.ToString(xy_Data.val_I_XY);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_A.ch3_D501w;
            sMeasured = Convert.ToString(xy_Data.val_X);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_A.ch3_D501w;
            sMeasured = Convert.ToString(xy_Data.val_Y);
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
        xy_Data = new XY_Data();
        xy_Data = Feasa_10F_A.ch4_D511w;
        sMeasured = Convert.ToString(xy_Data.val_I_XY);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_A.ch4_D511w;
            sMeasured = Convert.ToString(xy_Data.val_X);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_A.ch4_D511w;
            sMeasured = Convert.ToString(xy_Data.val_Y);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_A.ch5_D502w;
            sMeasured = Convert.ToString(xy_Data.val_I_XY);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_A.ch5_D502w;
            sMeasured = Convert.ToString(xy_Data.val_X);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_A.ch5_D502w;
            sMeasured = Convert.ToString(xy_Data.val_Y);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch1_D505o;
            sMeasured = Convert.ToString(xy_Data.val_I_XY);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch1_D505o;
            sMeasured = Convert.ToString(xy_Data.val_X);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch1_D505o;
            sMeasured = Convert.ToString(xy_Data.val_Y);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch2_D514o;
            sMeasured = Convert.ToString(xy_Data.val_I_XY);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch2_D514o;
            sMeasured = Convert.ToString(xy_Data.val_X);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch2_D514o;
            sMeasured = Convert.ToString(xy_Data.val_Y);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch3_D523o;
            sMeasured = Convert.ToString(xy_Data.val_I_XY);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch3_D523o;
            sMeasured = Convert.ToString(xy_Data.val_X);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch3_D523o;
            sMeasured = Convert.ToString(xy_Data.val_Y);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch4_D527o;
            sMeasured = Convert.ToString(xy_Data.val_I_XY);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch4_D527o;
            sMeasured = Convert.ToString(xy_Data.val_X);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch4_D527o;
            sMeasured = Convert.ToString(xy_Data.val_Y);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch6_D528o;
            sMeasured = Convert.ToString(xy_Data.val_I_XY);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch6_D528o;
            sMeasured = Convert.ToString(xy_Data.val_X);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch6_D528o;
            sMeasured = Convert.ToString(xy_Data.val_Y);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch7_D524o;
            sMeasured = Convert.ToString(xy_Data.val_I_XY);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch7_D524o;
            sMeasured = Convert.ToString(xy_Data.val_X);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch7_D524o;
            sMeasured = Convert.ToString(xy_Data.val_Y);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch8_D515o;
            sMeasured = Convert.ToString(xy_Data.val_I_XY);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch8_D515o;
            sMeasured = Convert.ToString(xy_Data.val_X);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch8_D515o;
            sMeasured = Convert.ToString(xy_Data.val_Y);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch9_D506o;
            sMeasured = Convert.ToString(xy_Data.val_I_XY);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch9_D506o;
            sMeasured = Convert.ToString(xy_Data.val_X);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch9_D506o;
            sMeasured = Convert.ToString(xy_Data.val_Y);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch1_D507w;
            sMeasured = Convert.ToString(xy_Data.val_I_XY);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch1_D507w;
            sMeasured = Convert.ToString(xy_Data.val_X);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch1_D507w;
            sMeasured = Convert.ToString(xy_Data.val_Y);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch2_D516w;
            sMeasured = Convert.ToString(xy_Data.val_I_XY);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch2_D516w;
            sMeasured = Convert.ToString(xy_Data.val_X);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch2_D516w;
            sMeasured = Convert.ToString(xy_Data.val_Y);
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
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch3_D525w;
            sMeasured = Convert.ToString(xy_Data.val_I_XY);
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
        private XY_Data xy_Data;

        public TestItemStep021_P48()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch3_D525w;
            sMeasured = Convert.ToString(xy_Data.val_X);
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

    class TestItemStep021_P49 : TestItemForm
    {
        public string step = "_021 P49";
        private XY_Data xy_Data;

        public TestItemStep021_P49()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch3_D525w;
            sMeasured = Convert.ToString(xy_Data.val_Y);
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

    class TestItemStep021_P50 : TestItemForm
    {
        public string step = "_021 P50";
        private XY_Data xy_Data;

        public TestItemStep021_P50()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch4_D521w;
            sMeasured = Convert.ToString(xy_Data.val_I_XY);
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

    class TestItemStep021_P51 : TestItemForm
    {
        public string step = "_021 P51";
        private XY_Data xy_Data;

        public TestItemStep021_P51()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch4_D521w;
            sMeasured = Convert.ToString(xy_Data.val_X);
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

    class TestItemStep021_P52 : TestItemForm
    {
        public string step = "_021 P52";
        private XY_Data xy_Data;

        public TestItemStep021_P52()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch4_D521w;
            sMeasured = Convert.ToString(xy_Data.val_Y);
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

    class TestItemStep021_P53 : TestItemForm
    {
        public string step = "_021 P53";
        private XY_Data xy_Data;

        public TestItemStep021_P53()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch5_D520w;
            sMeasured = Convert.ToString(xy_Data.val_I_XY);
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

    class TestItemStep021_P54 : TestItemForm
    {
        public string step = "_021 P54";
        private XY_Data xy_Data;

        public TestItemStep021_P54()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch5_D520w;
            sMeasured = Convert.ToString(xy_Data.val_X);
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

    class TestItemStep021_P55 : TestItemForm
    {
        public string step = "_021 P55";
        private XY_Data xy_Data;

        public TestItemStep021_P55()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch5_D520w;
            sMeasured = Convert.ToString(xy_Data.val_Y);
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

    class TestItemStep021_P56 : TestItemForm
    {
        public string step = "_021 P56";
        private XY_Data xy_Data;

        public TestItemStep021_P56()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch6_D522w;
            sMeasured = Convert.ToString(xy_Data.val_I_XY);
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

    class TestItemStep021_P57 : TestItemForm
    {
        public string step = "_021 P57";
        private XY_Data xy_Data;

        public TestItemStep021_P57()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch6_D522w;
            sMeasured = Convert.ToString(xy_Data.val_X);
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

    class TestItemStep021_P58 : TestItemForm
    {
        public string step = "_021 P58";
        private XY_Data xy_Data;

        public TestItemStep021_P58()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch6_D522w;
            sMeasured = Convert.ToString(xy_Data.val_Y);
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

    class TestItemStep021_P59 : TestItemForm
    {
        public string step = "_021 P59";
        private XY_Data xy_Data;

        public TestItemStep021_P59()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch7_D526w;
            sMeasured = Convert.ToString(xy_Data.val_I_XY);
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

    class TestItemStep021_P60 : TestItemForm
    {
        public string step = "_021 P60";
        private XY_Data xy_Data;

        public TestItemStep021_P60()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch7_D526w;
            sMeasured = Convert.ToString(xy_Data.val_X);
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

    class TestItemStep021_P61 : TestItemForm
    {
        public string step = "_021 P61";
        private XY_Data xy_Data;

        public TestItemStep021_P61()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch7_D526w;
            sMeasured = Convert.ToString(xy_Data.val_Y);
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

    class TestItemStep021_P62 : TestItemForm
    {
        public string step = "_021 P62";
        private XY_Data xy_Data;

        public TestItemStep021_P62()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch8_D517w;
            sMeasured = Convert.ToString(xy_Data.val_I_XY);
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

    class TestItemStep021_P63 : TestItemForm
    {
        public string step = "_021 P63";
        private XY_Data xy_Data;

        public TestItemStep021_P63()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch8_D517w;
            sMeasured = Convert.ToString(xy_Data.val_X);
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

    class TestItemStep021_P64 : TestItemForm
    {
        public string step = "_021 P64";
        private XY_Data xy_Data;

        public TestItemStep021_P64()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch8_D517w;
            sMeasured = Convert.ToString(xy_Data.val_Y);
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

    class TestItemStep021_P65 : TestItemForm
    {
        public string step = "_021 P65";
        private XY_Data xy_Data;

        public TestItemStep021_P65()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch9_D508w;
            sMeasured = Convert.ToString(xy_Data.val_I_XY);
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

    class TestItemStep021_P66 : TestItemForm
    {
        public string step = "_021 P66";
        private XY_Data xy_Data;

        public TestItemStep021_P66()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch9_D508w;
            sMeasured = Convert.ToString(xy_Data.val_X);
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

    class TestItemStep021_P67 : TestItemForm
    {
        public string step = "_021 P67";
        private XY_Data xy_Data;

        public TestItemStep021_P67()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_B.ch9_D508w;
            sMeasured = Convert.ToString(xy_Data.val_Y);
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

    class TestItemStep021_P68 : TestItemForm
    {
        public string step = "_021 P68";
        private XY_Data xy_Data;

        public TestItemStep021_P68()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = Feasa_10F_A.ch6_D631o;
            sMeasured = Convert.ToString(xy_Data.val_I_XY);
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

    class TestItemStep021_P69 : TestItemForm
    {
        public string step = "_021 P69";
        private XY_Data xy_Data;

        public TestItemStep021_P69()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = xy_Data = Feasa_10F_A.ch6_D631o;
            sMeasured = Convert.ToString(xy_Data.val_X);
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

    class TestItemStep021_P70 : TestItemForm
    {
        public string step = "_021 P70";
        private XY_Data xy_Data;

        public TestItemStep021_P70()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            xy_Data = new XY_Data();
            xy_Data = xy_Data = Feasa_10F_A.ch6_D631o;
            sMeasured = Convert.ToString(xy_Data.val_Y);
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

    class TestItemStep021_P71 : TestItemForm
    {
        public string step = "_021 P71";

        public TestItemStep021_P71()
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
