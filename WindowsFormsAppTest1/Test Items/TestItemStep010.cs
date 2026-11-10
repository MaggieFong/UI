using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;

    // 010 P1
    class TestItemStep010_P1 : TestItemForm
    {
        public string step = "_010 P1";
        
        public TestItemStep010_P1()
        {
            sStep = step;
            LoadSpec();            
        }

        public override WORK_STATUS Init()
        {
            bool[] Btn_default = { false, false, false, false };
            Array.Copy(Btn_default, Step20_OutPut.Btn_On, 4);
            Array.Copy(Btn_default, Step20_OutPut.Btn_Flash, 4);
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


    // 010 P2
    class TestItemStep010_P2 : TestItemForm
    {
        public string step = "_010 P2";

        public TestItemStep010_P2()
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
            
            if (configurePin())
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


        private void setInputPullNone(byte io_idx)
        {
            StaticValues.hwTestRPCDevice.ConfigurePin(io_idx,
                    HardwareTestRPCSerialDevice.PinTypeValue.InputPin,
                    HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);
        }

        private void setOutputPullNone(byte io_idx)
        {
            StaticValues.hwTestRPCDevice.ConfigurePin(io_idx,
                    HardwareTestRPCSerialDevice.PinTypeValue.OutputPin,
                    HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);
        }

        private void setADCPullNone(byte io_idx)
        {
            StaticValues.hwTestRPCDevice.ConfigurePin(io_idx,
                    HardwareTestRPCSerialDevice.PinTypeValue.AdcPin,
                    HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);
        }

        private void setDACPullNone(byte io_idx)
        {
            StaticValues.hwTestRPCDevice.ConfigurePin(io_idx,
                    HardwareTestRPCSerialDevice.PinTypeValue.DacPin,
                    HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);
        }
        private void setI2c_sercom3(byte sda, byte scl)
        {
            bool I2Cstatus = StaticValues.hwTestRPCDevice.ConfigureMasterI2C(sda,
                scl,
                HardwareTestRPCSerialDevice.SercomNumValue.Sercom3);
            Console.WriteLine("setI2c_sercom3 = "+ I2Cstatus);
        }
        private void setI2c_sercom4(byte sda, byte scl)
        {
            StaticValues.hwTestRPCDevice.ConfigureMasterI2C(sda,
                scl,
                HardwareTestRPCSerialDevice.SercomNumValue.Sercom4);
        }

        private void setI2c_sercom5(byte sda, byte scl)
        {
            StaticValues.hwTestRPCDevice.ConfigureMasterI2C(sda,
                scl,
                HardwareTestRPCSerialDevice.SercomNumValue.Sercom5);
        }

        private bool configurePin()
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



                //i2c
                 setI2c_sercom5(StaticValues.uC_I2C_sda_Attach, StaticValues.uC_I2C_scl_Attach);
                //setI2c_sercom4(StaticValues.uC_I2C_sda_LED, StaticValues.uC_I2C_scl_LED);

                //ADC
                setADCPullNone(StaticValues.uC_CPDischargerCurrent);//
                setADCPullNone(StaticValues.uC_FlashCapVoltage);//
                setADCPullNone(StaticValues.uC_LampVoltage);//
                setADCPullNone(StaticValues.uC_STS_Sensor_Filtered);//
                setADCPullNone(StaticValues.uC_SCATemp);//
                setADCPullNone(StaticValues.uC_SupplyVoltage14V);//
                setADCPullNone(StaticValues.uC_AirOutTemp);//
                setADCPullNone(StaticValues.uC_Lampframe_TC);//
                //setADCPullNone(StaticValues.uC_Touch1);
                //setADCPullNone(StaticValues.uC_Touch2);

                //DAC
                setDACPullNone(StaticValues.uC_SCA_I_DAC);
            }
            catch
            {
                return false;
            }

            return true;
        }

    }


    // 010 P3
    class TestItemStep010_P3 : TestItemForm
    {
        public string step = "_010 P3";
        private DAQdio taskdio;
        private DAQdio taskdio1;
        private DAQadc taskADC;
        public TestItemStep010_P3()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            try
            {
                initialOutputs();
               
                sMeasured = "True";
              

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

        }

        public override void release()
        {
            
        }

        private void initialOutputs()
        {
          

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
            
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_ON, 0);//## //D241652 requirement mentioned
            System.Threading.Thread.Sleep(1);

            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Awake, 1);// keep board awake
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Attachment_EN, 0); //11-12 uC_Attachment_EN :0 enable 3V3 switched
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_12V_Ena, 0); //11-12 uC_Attachment_EN :0 enable 3V3 switched

            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Cooling, 0);//fan off 


            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedError, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedRTF, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UIstandby, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet1, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet2, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet3, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet4, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet5, 1);

            ushort data = 1;
            bool btnconfigure = StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonON, ref data);
            Console.WriteLine("{0} On button = {1}", btnconfigure.ToString(), data.ToString());
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
        private void Btn_Init()
        {
            ushort data = 1;
            bool btnconfigure = StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonON, ref data);
            Console.WriteLine("On button = ", btnconfigure);
            Console.WriteLine("uC_UIButtonON =" + data.ToString());
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
            Console.WriteLine("uC_UIButtonON ="+data.ToString());
            if (data == 1)
            {
                Step20_OutPut.Btn_On[1] = true;
            }

            data = 0;
            StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_UIButtonFlash, ref data);
            Console.WriteLine("uC_UIButtonFlash =" + data.ToString());
            if (data == 1)
            {
                Step20_OutPut.Btn_Flash[1] = true;
            }

        }
    }
}
