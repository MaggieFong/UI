using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;

    class TestItemStep097 : TestItemForm
    {
        public string step = "_097";
        private int testcount;
        private DAQdio dioA;
        private DAQdio dioB;
        DR2003 DR2003;
        //discharge flashcap; set initial condition
        public TestItemStep097()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            try
            {
                DR2003 = new DR2003(Power_DR2002MO.comPort, Power_DR2002MO.baudRate);
                DR2003.Connect();
                DR2003.SetCURR(Convert.ToString(Power_DR2002MO.FLASHCAP_CURR5));   //6A
                DR2003.Disconnect();
                //dioA = new DAQdio(IO_USB6525_A.deviceName);
                //dioA.addOneLineWriteCH(IO_USB6525_A.Relay_Discharge_Res_K12_K13_P04);

                //dioB = new DAQdio(IO_USB6525_B.deviceName);
                //dioB.addOneLineWriteCH(IO_USB6525_B.CTL_HV_RELAY_P05);


            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }

            //dioA.setOneData(true);
            //dioB.setOneData(true);
            //System.Threading.Thread.Sleep(200);
            //dioA.setOneData(false);
            //dioB.setOneData(false);
            testcount = 0;
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_FC_Charge, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPDischargerCurrent, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_ON, 1);//## // start charge
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_FC_Charge, 1);
            StaticValues.hwTestRPCDevice.ConfigurePin(StaticValues.uC_FlashCapVoltage,
                              HardwareTestRPCSerialDevice.PinTypeValue.AdcPin,
                              HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);
            if (StaticValues.hwTestRPCDevice.ConfigureADC(0, 0, 2, 2))
            {
                //Console.WriteLine("ConfigureADC AdcPin pass ");

            }
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            sMeasured = "";

            ushort data = 0;
            ushort data1 = 0;
            StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_FlashCapVoltage, ref data);
            StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_LampVoltage, ref data1);
            double ADCFlashCap = 1.0 * (double)data / 4095;
            double FlashCapVoltage = ADCFlashCap * ((3000 + 6.8) / 6.8);
            if (data1 == 0)
            {
                StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_LampVoltage, ref data1);
            }
            Console.WriteLine("ADC =  " + ADCFlashCap + " ; volt =  " + FlashCapVoltage);
            double ADCLamp = 1.0 * (double)data1 / 4095;
            double LampVoltage = ADCLamp * ((600 + 1) / 1);

            Console.WriteLine("ADC =  " + ADCLamp + " ; LampVoltage =  " + LampVoltage);
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_ON, 1);//## // start charge
            if (FlashCapVoltage > 400)
            {
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_FC_Charge, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_ON, 0); //##
                StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_FlashCapVoltage, ref data);
                double ADC = 1.0 * (double)data / 4095;
                double FlashV = ADC * ((3000 + 6.8) / 6.8);

                Console.WriteLine("end ADC =  " + ADC + " ; volt =  " + FlashV);
                //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPChargeFlashCap, 1);
                return WORK_STATUS.PASS;
            }
            testcount++;
            if (testcount > 40)// 4 seconds time out
            {
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Enable_FC_Charge, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_ON, 0);
                return WORK_STATUS.PASS;
            }



            //System.Threading.Thread.Sleep(100);
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPDischarger, 0);

            sMeasured = "True";
            return WORK_STATUS.WORKING;
        }

        public override void release()
        {
            DR2003 = null;
            dioA = null;
            dioB = null;

        }
    }
}
