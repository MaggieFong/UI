using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;

    //class TestItemStep020_P1 : TestItemForm
    //{
    //    public string step = "_020 P1";
    //    private DAQdio taskdio;
    //    private DAQdio dioB;
    //    private DAQdio taskdio1;

    //    public TestItemStep020_P1()
    //    {
    //        sStep = step;
    //        LoadSpec();
    //    }

    //    public override WORK_STATUS Init()
    //    {
           
    //        StaticValues.hwTestRPCDevice.ConfigurePin(StaticValues.uC_UIButtonFlash,
    //               HardwareTestRPCSerialDevice.PinTypeValue.InputPin,
    //               HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);

    //        StaticValues.hwTestRPCDevice.ConfigurePin(StaticValues.uC_UIButtonON,
    //               HardwareTestRPCSerialDevice.PinTypeValue.InputPin,
    //               HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);




    //        Btn_status_init();
    //        //DIO setting
    //        try
    //        {
    //            dioB = new DAQdio(IO_USB6525_B.deviceName);
    //            dioB.addOneLineWriteCH(IO_USB6525_B.CTL_BUTTON_P04);
               
    //            taskdio1 = new DAQdio(IO_USB6525_A.deviceName);
    //            taskdio1.addOneLineWriteCH(IO_USB6525_A.Relay_FlashButton_TP515_K6_P05); //flash button


    //            //Byte pinN = Convert.ToByte(textBox_enterInput.Text);
    //            //ushort data = 1;
    //            //StaticValues.hwTestRPCDevice.GetValue(pinN, ref data);
    //            //textBox_readValue.Text = Convert.ToString(data);


    //            ushort data = 1;
    //            bool isgetval = StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonON, ref data);
    //            Console.WriteLine("({0}) release uC_UIButtonON = {1}" , isgetval.ToString(), Convert.ToString(data)); 
    //            //Btn_Init();
    //            Console.WriteLine("Init PUsh button");
    //            dioB.setOneData(true);
    //            System.Threading.Thread.Sleep(5000);
    //            Console.WriteLine("delay 5000");
    //            ushort data1 = 0;
    //            isgetval =  StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonON, ref data1);
    //            //Console.WriteLine("push uC_UIButtonON = " + data1.ToString()); ;
    //            Console.WriteLine("({0}) push uC_UIButtonON = {1}", (isgetval == true) ? "True" : "False", data1.ToString()); ;
    //            if (data1 == 1)
    //            {
    //                Step20_OutPut.Btn_On[1] = true;
    //            }

    //            data1 = 0;

    //            taskdio1.setOneData(true);
    //            System.Threading.Thread.Sleep(500);
    //            data = 0;
    //            isgetval = StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonFlash, ref data);
    //            if (data == 1)
    //            {
    //                Step20_OutPut.Btn_Flash[1] = true;
    //            }
    //            Console.WriteLine("({0}) push uC_UIButtonFlash = {1}", isgetval.ToString(), data.ToString());
    //            return WORK_STATUS.PASS;
    //        }
    //        catch (Exception e)
    //        {

    //            sMeasured = e.Message;
    //            return WORK_STATUS.FAIL;
    //        }

    //        // check button init status
           
    //    }

    //    public override WORK_STATUS Proccess()
    //    {
    //        if (Step20_OutPut.Btn_On[3])
    //        {
    //            sMeasured = "True";
    //            return WORK_STATUS.PASS;
    //        }
    //        System.Threading.Thread.Sleep(300);
    //        Btn_Press();
    //        dioB.setOneData(false);
    //        taskdio1.setOneData(false);
    //        System.Threading.Thread.Sleep(100);
    //        Btn_Released();
    //        //System.Threading.Thread.Sleep(30);
    //        Btn_Result();
    //        //Console.WriteLine(" Btn_On init:{0} press:{1} release:{2}  ",
    //        //Convert.ToString(Step20_OutPut.Btn_On[0]),
    //        //Convert.ToString(Step20_OutPut.Btn_On[1]),
    //        //Convert.ToString(Step20_OutPut.Btn_On[2]));

    //        if (Step20_OutPut.Btn_On[3])
    //        {
    //            sMeasured = "True";
    //            return WORK_STATUS.PASS;
    //        }
    //        else
    //        {
    //            //sMeasured = "False";
    //            sMeasured = Convert.ToString(Step20_OutPut.Btn_On[0]) + "," +
    //                Convert.ToString(Step20_OutPut.Btn_On[1]) + "," +
    //                Convert.ToString(Step20_OutPut.Btn_On[2]);
    //            return WORK_STATUS.FAIL;
    //        }
    //    }

    //    public override void release()
    //    {
    //        dioB = null;        //free memory
    //        taskdio1 = null;        //free memory
    //    }
    //    private void Btn_Init()
    //    {
    //        ushort data = 1;
    //        bool btnconfigure = StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonON, ref data);
    //        Console.WriteLine("Btn_Init On button = " + data.ToString());
    //        if (data == 0)
    //        {
    //            Step20_OutPut.Btn_On[0] = true;
    //        }

    //        data = 1;
    //        StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonFlash, ref data);
    //        if (data == 0)
    //        {
    //            Step20_OutPut.Btn_Flash[0] = true;
    //        }

    //    }

    //    private void Btn_Press()
    //    {
    //        ushort data = 0;
    //        StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonON, ref data);
    //        if (data == 1)
    //        {
    //            Step20_OutPut.Btn_On[1] = true;
    //        }

    //        data = 0;
    //        StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonFlash, ref data);
    //        if (data == 1)
    //        {
    //            Step20_OutPut.Btn_Flash[1] = true;
    //        }

    //    }

    //    private void Btn_Released()
    //    {
    //        ushort data = 1;
    //        StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonON, ref data);
    //        if (data == 0)
    //        {
    //            Step20_OutPut.Btn_On[2] = true;
    //        }

    //        data = 1;
    //        StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonFlash, ref data);
    //        if (data == 0)
    //        {
    //            Step20_OutPut.Btn_Flash[2] = true;
    //        }


    //    }

    //    private void Btn_Result()
    //    {
    //        int CalculateValue0 = Step20_OutPut.Btn_On.Count(c => c == true);
    //        Step20_OutPut.Btn_On[3] = (CalculateValue0 == 3) ? true : false;
    //        int CalculateValue1 = Step20_OutPut.Btn_Flash.Count(c => c == true);
    //        Step20_OutPut.Btn_Flash[3] = (CalculateValue1 == 3) ? true : false;

    //    }
    //    private void Btn_status_init()
    //    {
    //        bool[] Btn_default = { false, false, false, false };
    //        Array.Copy(Btn_default, Step20_OutPut.Btn_On, 4);
    //        Array.Copy(Btn_default, Step20_OutPut.Btn_Flash, 4);

    //    }
    //}

    //class TestItemStep020_P2 : TestItemForm
    //{
    //    public string step = "_020 P2";

    //    public TestItemStep020_P2()
    //    {
    //        sStep = step;
    //        LoadSpec();
    //    }

    //    public override WORK_STATUS Init()
    //    {
    //        Console.WriteLine(" Btn_Flash init:{0} press:{1} release:{2}  ",
    //        Convert.ToString(Step20_OutPut.Btn_Flash[0]),
    //        Convert.ToString(Step20_OutPut.Btn_Flash[1]),
    //        Convert.ToString(Step20_OutPut.Btn_Flash[2]));
    //        return WORK_STATUS.PASS;
    //    }
    //    public override WORK_STATUS Proccess()
    //    {
    //        if (Step20_OutPut.Btn_Flash[3])
    //        {
    //            sMeasured = "True";
    //            return WORK_STATUS.PASS;
    //        }
    //        else
    //        {
    //            sMeasured = "False";
    //            return WORK_STATUS.FAIL;
    //        }

    //    }

    //    public override void release()
    //    {

    //    }
    //}
    class TestItemStep020_P0 : TestItemForm
    {
        public string step = "_020 P0";
        private DAQdio taskdio;
        private DAQdio taskdio1;
        private DAQadc taskADC;
        public TestItemStep020_P0()
        {
            //sStep = step;
            //LoadSpec();
            sStep = "_020 P0";
            sTest = "Button Test";
        }

        public override WORK_STATUS Init()
        {
            Btn_status_init();
            //DIO setting
            try
            {
                taskdio1 = new DAQdio(IO_USB6525_A.deviceName);
                taskdio1.addOneLineWriteCH(IO_USB6525_A.Relay_Support3V3_TP322_K7_P06);
                taskdio1.setOneData(false);
                taskdio1 = null;
                taskdio = new DAQdio(IO_USB6525_B.deviceName);
                taskdio.addOneLineWriteCH(IO_USB6525_B.CTL_BUTTON_P04);
                taskdio1 = new DAQdio(IO_USB6525_A.deviceName);
                taskdio1.addOneLineWriteCH(IO_USB6525_A.Relay_FlashButton_TP515_K6_P05); //flash button

                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP736_AI29);
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP739_AI22);

                // check button init status
                //Btn_Init();
                taskdio.setOneData(true);
                //System.Threading.Thread.Sleep(500);
                taskdio1.setOneData(true);
                Console.WriteLine("Button PUsh!");
               
                System.Threading.Thread.Sleep(1000);
                Btn_Press();
                if (taskADC.start())
                {
                    
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
            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;

            if (taskADC.isEnable() == false)
            {
                if (taskADC.getErrorMsg() == null)      //must check if there were any error when DAQadc.callbackADC()
                {
                    double average = taskADC.getAverage(0);
                    double average1 = taskADC.getAverage(1);
                    Step20_OutPut.UI_button_on_V = average.ToString("F3");
                    Step20_OutPut.Flash_button_on_V = average1.ToString("F3");
                    taskdio.setOneData(false);
                    taskdio1.setOneData(false);
                    System.Threading.Thread.Sleep(300);
                    //Btn_Released();
                    //Btn_Result();
                    sMeasured = "True";
                    re = WORK_STATUS.PASS;
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
            //taskdio.setOneData(false);
            //taskdio1.setOneData(false);
            taskdio = null;        //free memory
            taskdio1 = null;        //free memory
        }
        private void Btn_Init()
        {
            ushort data = 1;
            StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonON, ref data);
            Console.WriteLine("Btn_Init uC_UIButtonON =" + data.ToString());
            if (data == 0)
            {
                Step20_OutPut.Btn_On[0] = true;
            }

            data = 1;
            StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonFlash, ref data);
            Console.WriteLine("Btn_Init uC_UIButtonON =" + data.ToString());
            if (data == 0)
            {
                Step20_OutPut.Btn_Flash[0] = true;
            }

            
        }

        private void Btn_Press()
        {
            ushort data = 0;
            StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonON, ref data);
            Console.WriteLine("uC_UIButtonON =" + data.ToString());
            if (data != 0)
            {
                Step20_OutPut.Btn_On[1] = true;
            }
            else
            {
                System.Threading.Thread.Sleep(200);
                StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonON, ref data);
                Console.WriteLine("uC_UIButtonON =" + data.ToString());
                if (data != 0)
                {
                    Step20_OutPut.Btn_On[1] = true;
                }
            }
            Console.WriteLine("Step20_OutPut.Btn_On[1] =" + Step20_OutPut.Btn_On[1].ToString());
            data = 0;
            StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonFlash, ref data);
            Console.WriteLine("uC_UIButtonFlash =" + data.ToString());
            if (data != 0)
            {
                Step20_OutPut.Btn_Flash[1] = true;
            }

            
        }

        private void Btn_Released()
        {
            ushort data = 1;
            StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonON, ref data);

             Console.WriteLine("Btn_Released uC_UIButtonON =" + data.ToString());
            if (data == 0)
            {
                Step20_OutPut.Btn_On[2] = true;
            }

            data = 1;
            StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonFlash, ref data);
            if (data == 0)
            {
                Step20_OutPut.Btn_Flash[2] = true;
            }
            Console.WriteLine("Btn_Released uC_UIButtonFlash =" + data.ToString());

        }

        private void Btn_Result()
        {
            int CalculateValue0 = Step20_OutPut.Btn_On.Count(c => c == true);
            Step20_OutPut.Btn_On[3] = (CalculateValue0 == 3) ? true : false;
            int CalculateValue1 = Step20_OutPut.Btn_Flash.Count(c => c == true);
            Step20_OutPut.Btn_Flash[3] = (CalculateValue1 == 3) ? true : false;
           

        }
        private void Btn_status_init()
        {
            bool[] Btn_default = { false, false, false, false };
            Array.Copy(Btn_default, Step20_OutPut.Btn_On, 4);
           
        }
    }
    class TestItemStep020_P1 : TestItemForm
    {
        public string step = "_020 P1";
        private DAQdio taskdio;
        private DAQdio taskdio1;
        private DAQadc taskADC;
        public TestItemStep020_P1()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
           
            //DIO setting
            try
            {


                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_N5_5);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP736_AI29);
                //taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP739_AI22);



                if (taskADC.start())
                {

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
            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;

            if (taskADC.isEnable() == false)
            {
                if (taskADC.getErrorMsg() == null)      //must check if there were any error when DAQadc.callbackADC()
                {
                    double average = taskADC.getAverage(0);
                    //double average1 = taskADC.getAverage(1);
                    Step20_OutPut.UI_button_off_V = average.ToString("F3");
                    //Step20_OutPut.Flash_button_off_V = average1.ToString("F3");
                    Console.WriteLine(" Btn_On init:{0} press:{1} release:{2}  ",
            Convert.ToString(Step20_OutPut.Btn_On[0]),
            Convert.ToString(Step20_OutPut.Btn_On[1]),
            Convert.ToString(Step20_OutPut.Btn_On[2]));
                    if (Step20_OutPut.Btn_On[1])
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
        private void Btn_Init()
        {
            ushort data = 1;
            StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonON, ref data);
            if (data == 0)
            {
                Step20_OutPut.Btn_On[0] = true;
            }

            data = 1;
            StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonFlash, ref data);
            if (data == 0)
            {
                Step20_OutPut.Btn_Flash[0] = true;
            }


        }

        private void Btn_Press()
        {
            ushort data = 0;
            StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonON, ref data);
            if (data == 1)
            {
                Step20_OutPut.Btn_On[1] = true;
            }

            data = 0;
            StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonFlash, ref data);
            if (data == 1)
            {
                Step20_OutPut.Btn_Flash[1] = true;
            }


        }

        private void Btn_Released()
        {
            ushort data = 1;
            StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonON, ref data);
            if (data == 0)
            {
                Step20_OutPut.Btn_On[2] = true;
            }

            data = 1;
            StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonFlash, ref data);
            if (data == 0)
            {
                Step20_OutPut.Btn_Flash[2] = true;
            }


        }

        private void Btn_Result()
        {
            int CalculateValue0 = Step20_OutPut.Btn_On.Count(c => c == true);
            Step20_OutPut.Btn_On[3] = (CalculateValue0 == 3) ? true : false;
            int CalculateValue1 = Step20_OutPut.Btn_Flash.Count(c => c == true);
            Step20_OutPut.Btn_Flash[3] = (CalculateValue1 == 3) ? true : false;


        }
        private void Btn_status_init()
        {
            bool[] Btn_default = { false, false, false, false };
            Array.Copy(Btn_default, Step20_OutPut.Btn_On, 4);

        }
    }
    class TestItemStep020_P2 : TestItemForm
    {
        public string step = "_020 P2";

        public TestItemStep020_P2()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            Console.WriteLine(" Btn_Flash init:{0} press:{1} release:{2}  ",
            Convert.ToString(Step20_OutPut.Btn_Flash[0]),
            Convert.ToString(Step20_OutPut.Btn_Flash[1]),
            Convert.ToString(Step20_OutPut.Btn_Flash[2]));
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            if (Step20_OutPut.Btn_Flash[1])
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

        public override void release()
        {

        }
    }
    class TestItemStep020_P3 : TestItemForm
        {
            public string step = "_020 P3";

            public TestItemStep020_P3()
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
                sMeasured = Step20_OutPut.UI_button_on_V;
                if (PassFailJudge())
                {
                    if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                    {
                        if (!offsetCalculate())
                            return WORK_STATUS.FAIL;
                    }

                    return WORK_STATUS.PASS;
                }
                else
                {
                    return WORK_STATUS.FAIL;
                }

        }

            public override void release()
            {

            }
        }
    class TestItemStep020_P4 : TestItemForm
    {
        public string step = "_020 P4";
        private DAQdio taskdio1;
        private DAQadc taskADC;
        bool ispass = false;

        public TestItemStep020_P4()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            try
            {
                ispass = false;
                taskdio1 = new DAQdio(IO_USB6525_A.deviceName);
                taskdio1.addOneLineWriteCH(IO_USB6525_A.Relay_FlashButton_TP515_K6_P05); //flash button
                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v

                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP736_AI29);
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP739_AI22);
                sMeasured = Step20_OutPut.Flash_button_on_V;
                if (PassFailJudge())
                {
                    if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                    {
                        if (!offsetCalculate())
                            return WORK_STATUS.FAIL;
                    }
                    ispass = true;
                    return WORK_STATUS.PASS;
                }
                else
                {
                    taskdio1.setOneData(true);
                    System.Threading.Thread.Sleep(500);
                    if (taskADC.start())
                    {

                        return WORK_STATUS.PASS;
                    }
                    else
                    {
                        sMeasured = taskADC.getErrorMsg();
                        return WORK_STATUS.FAIL;
                    }
                    //return WORK_STATUS.FAIL;
                }
            }
            catch (Exception e)
            {

                sMeasured = e.Message.ToString();
            }


           
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            if (ispass)
                return WORK_STATUS.PASS;

            WORK_STATUS re = WORK_STATUS.WORKING;

            if (taskADC.isEnable() == false)
            {
                if (taskADC.getErrorMsg() == null)      //must check if there were any error when DAQadc.callbackADC()
                {
                    double average = taskADC.getAverage(1);
                    sMeasured = average.ToString("F3");
                    taskdio1.setOneData(false);
                    System.Threading.Thread.Sleep(300);
                    if (PassFailJudge())
                    {
                        if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                        {
                            if (!offsetCalculate())
                                return WORK_STATUS.FAIL;
                        }

                        return WORK_STATUS.PASS;
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
           
            taskdio1 = null;
            taskADC = null;
        }
    }

    class TestItemStep020_P5 : TestItemForm
    {
        public string step = "_020 P5";

        public TestItemStep020_P5()
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
            sMeasured = Step20_OutPut.UI_button_off_V;
            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }

        }

        public override void release()
        {

        }
    }
    class TestItemStep020_P6 : TestItemForm
    {
        public string step = "_020 P6";
        
        private DAQadc taskADC;
        public TestItemStep020_P6()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
           
            //DIO setting
            try
            {


                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_N5_5);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                //taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP736_AI29);
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP739_AI22);



                if (taskADC.start())
                {

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
            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;

            if (taskADC.isEnable() == false)
            {
                if (taskADC.getErrorMsg() == null)      //must check if there were any error when DAQadc.callbackADC()
                {
                    double average = taskADC.getAverage(0);


                    Step20_OutPut.Flash_button_off_V = average.ToString("F3");
                    sMeasured = Step20_OutPut.Flash_button_off_V;
                    if (PassFailJudge())
                    {
                        if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                        {
                            if (!offsetCalculate())
                                return WORK_STATUS.FAIL;
                        }

                        return WORK_STATUS.PASS;
                    }
                    else
                    {
                        return WORK_STATUS.FAIL;
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
}
