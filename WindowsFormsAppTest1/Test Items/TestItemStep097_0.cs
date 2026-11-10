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

        //discharge flashcap; set initial condition
        public TestItemStep097()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
     
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPChargeFlashCap, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPDischarger, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_ON, 0);
            StaticValues.hwTestRPCDevice.ConfigurePin(StaticValues.uC_FlashCapVoltage,
                              HardwareTestRPCSerialDevice.PinTypeValue.AdcPin,
                              HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);
            if (StaticValues.hwTestRPCDevice.ConfigureADC(15, 2, 2, 2))
            {
                //Console.WriteLine("ConfigureADC AdcPin pass ");

            }
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            sMeasured = "";

            ushort data = 0;
            StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_FlashCapVoltage, ref data);
            double volt = 1.0 * (double)data / 4095;
            Console.WriteLine("ADC =  " + data + " ; volt =  " + volt);
            if (volt > 50)
            {
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPDischarger, 0);
                //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPChargeFlashCap, 1);
                return WORK_STATUS.PASS;
            }
            //testcount++;
            //if (testcount > 20)// 2 seconds time out
            //{
            //    return WORK_STATUS.FAIL;
            //}



            System.Threading.Thread.Sleep(100);
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPDischarger, 0);

            sMeasured = "True";
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_ON, 1);
        }
    }
}
