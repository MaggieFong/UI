using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;

    class TestItemStep137_P1 : TestItemForm
    {
        public string step = "_137 P1";
        private DAQdio dio;
        HardwareTestRPCSerialDevice.I2CMessageDataMiSo i2cMoSo;
        byte[] dataClear = { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                             0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                             0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                             0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };

        public TestItemStep137_P1()
        {
            sStep = step;
            LoadSpec();            
        }
        
        public override WORK_STATUS Init()
        {
            //DIO setting
            try
            {
                dio = new DAQdio(IO_USB6525_A.deviceName);
                dio.addOneLineWriteCH(IO_USB6525_A.Relay_I2C_K4_K5_P03);
                //System.Threading.Thread.Sleep(200);
                dio.setOneData(true); //enable I2C
            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }

            //StaticValues.hwTestRPCDevice.ConfigurePin(StaticValues.uC_Attachement_EN,
            //       HardwareTestRPCSerialDevice.PinTypeValue.OutputPin,
            //       HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Attachment_EN, 0);

            //StaticValues.hwTestRPCDevice.ConfigureMasterI2C(StaticValues.uC_I2C_sda_Attach,
            //    StaticValues.uC_I2C_scl_Attach,
            //    HardwareTestRPCSerialDevice.SercomNumValue.Sercom5);

            i2cMoSo = new HardwareTestRPCSerialDevice.I2CMessageDataMiSo();
            i2cMoSo.Data = new byte[1];
            i2cMoSo.Length =1;
            
            
          
            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
           
            sMeasured = "";

            byte[] addr = { 0, 32, 64, 96, 128, 160, 192, 224 };
            string outputdata = "";
            bool string_eq = true;
            bool eepromEqual = true;
            for (int i = 0; i < 8; i++)
            {
                Array.Copy(dataClear, i2cMoSo.Data, i2cMoSo.Length);
                i2cMoSo.Data[0] = addr[i];

                StaticValues.hwTestRPCDevice.ReadMasterI2C(StaticValues.uC_I2CEepromAddress, HardwareTestRPCSerialDevice.SercomNumValue.Sercom5,
                ref i2cMoSo);

                outputdata = BitConverter.ToString(i2cMoSo.Data);
                Console.WriteLine(BitConverter.ToString(i2cMoSo.Data));
                //string_eq = String.Equals(outputdata, StaticValues.EEprom_File[i]);

                //if (!string_eq)
                //{
                //    eepromEqual = false;
                //    Console.WriteLine("eeprom " + i + " Not Equals  ");
                //}
                if (i == 0 && outputdata != "01")
                {
                   eepromEqual = false;
                }
                if (i == 1 && outputdata != "00")
                {
                    eepromEqual = false;
                }
                if (i == 2 && outputdata != "0F")
                {
                    eepromEqual = false;
                }


            }

            if (!eepromEqual)
            {
                eepromEqual = true;
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Attachment_EN, 1);
                System.Threading.Thread.Sleep(1000);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Attachment_EN, 0);
                System.Threading.Thread.Sleep(200);

                for (int i = 0; i < 8; i++)
                {
                    Array.Copy(dataClear, i2cMoSo.Data, i2cMoSo.Length);
                    i2cMoSo.Data[0] = addr[i];

                    StaticValues.hwTestRPCDevice.ReadMasterI2C(StaticValues.uC_I2CEepromAddress, HardwareTestRPCSerialDevice.SercomNumValue.Sercom5,
                    ref i2cMoSo);

                    outputdata = BitConverter.ToString(i2cMoSo.Data);
                    Console.WriteLine(BitConverter.ToString(i2cMoSo.Data));
                    
                    if (i == 0 && outputdata != "01")
                    {
                        eepromEqual = false;
                    }
                    if (i == 1 && outputdata != "00")
                    {
                        eepromEqual = false;
                    }
                    if (i == 2 && outputdata != "0F")
                    {
                        eepromEqual = false;
                    }
                }
                if (eepromEqual)
                { 
                    sMeasured = "True";
                    return WORK_STATUS.PASS;
                }

                sMeasured = "False";
                return WORK_STATUS.FAIL;
            }
            else
            {
                sMeasured = "True";
                return WORK_STATUS.PASS;
            }
        }

        public override void release()
        {
            //System.Threading.Thread.Sleep(200);
            //dio.setOneData(false);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Attachment_EN, 1);
            System.Threading.Thread.Sleep(200);
            dio = null;
            i2cMoSo = null;
        }
    }

    class TestItemStep137_P2 : TestItemForm
    {
        public string step = "_137 P2";
        //private DAQdio dio;
        HardwareTestRPCSerialDevice.I2CMessageDataMiSo i2cMoSo;
        byte[] dataClear = { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                             0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                             0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                             0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };

        public TestItemStep137_P2()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            return WORK_STATUS.PASS;
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Attachment_EN, 1);
            //System.Threading.Thread.Sleep(100);
            i2cMoSo = new HardwareTestRPCSerialDevice.I2CMessageDataMiSo();
            i2cMoSo.Data = new byte[1];
            i2cMoSo.Length = 1;
            StaticValues.hwTestRPCDevice.ReadMasterI2C(StaticValues.uC_I2CEepromAddress, HardwareTestRPCSerialDevice.SercomNumValue.Sercom5,
            ref i2cMoSo);
            string outputdata = BitConverter.ToString(i2cMoSo.Data);
            if (outputdata == "00")
            {
                sMeasured = "True";
                return WORK_STATUS.PASS;
            }
            else
            {
                sMeasured = outputdata;
                return WORK_STATUS.FAIL;
            }
            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            sMeasured = NI_OutPut.s137_P2.ToString("0.0");
            if (PassFailJudge())
            {
                
               return WORK_STATUS.PASS;
            }
            else
            {
                return WORK_STATUS.FAIL;
            }
            //if (ADC_OutPut.s137_P2 == 1)
            //{
            //    sMeasured = "PASS";
            //    return WORK_STATUS.PASS;
            //}
            sMeasured = "FAIL";
            return WORK_STATUS.FAIL;
            
            sMeasured = "";

            byte[] addr = { 0, 32, 64, 96, 128, 160, 192, 224 };
            string outputdata = "";
            bool string_eq = true;
            bool eepromEqual = true;
            //for (int i = 0; i < 8; i++)
            //{
            //    Array.Copy(dataClear, i2cMoSo.Data, i2cMoSo.Length);
                i2cMoSo.Data[0] = addr[0];

                StaticValues.hwTestRPCDevice.ReadMasterI2C(StaticValues.uC_I2CEepromAddress, HardwareTestRPCSerialDevice.SercomNumValue.Sercom5,
                ref i2cMoSo);

                outputdata = BitConverter.ToString(i2cMoSo.Data);
                Console.WriteLine(BitConverter.ToString(i2cMoSo.Data));
            if(outputdata != "00") 
            {
                sMeasured = "True";
                return WORK_STATUS.PASS;
            }
            else
            {
                sMeasured = "False";
                return WORK_STATUS.FAIL;
            }
            //string_eq = String.Equals(outputdata, StaticValues.P2_EEprom_File[i]);
            //string_eq = String.Equals(outputdata, "FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF");
            //if (!string_eq)
            //    {
            //        eepromEqual = false;
            //        Console.WriteLine("eeprom " + i + " Not Equals  ");
            //    }

            //}

            if (!eepromEqual)
            {
                sMeasured = "False";
                return WORK_STATUS.FAIL;
            }
            else
            {
                sMeasured = "True";
                return WORK_STATUS.PASS;
            }
        }

        public override void release()
        {
            //System.Threading.Thread.Sleep(200);
            //dio.setOneData(false);
            //dio = null;
            i2cMoSo = null;
        }
    }
}
