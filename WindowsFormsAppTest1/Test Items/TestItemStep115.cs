using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;
    class TestItemStep115_P1 : TestItemForm
    {
        public string step = "_115 P1";
        private DR2003 DR2003;      //main power
        private IT6512C IT6512C;    //Battery simulation
        public TestItemStep115_P1()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //ushort X = 0;
            sMeasured = "";
            //if (StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_BatFull, ref X))
            //{
            //    //Console.WriteLine("start uC_BatFull = " + X);
            //}
            DR2003 = new DR2003(Power_DR2002MO.comPort, Power_DR2002MO.baudRate);
            DR2003.Connect();
            //DR2003.SetVOLT(Convert.ToString(Power_DR2002MO.V195));    //19.5V
            //System.Threading.Thread.Sleep(30);
            DR2003.SetCURR(Convert.ToString(Power_DR2002MO.CURR1));   //1A
            //System.Threading.Thread.Sleep(30);
            //DR2003.SetOCPOn(Convert.ToString(Power_DR2002MO.OCP));
            //System.Threading.Thread.Sleep(30);
            //DR2003.SetOVPOn(Convert.ToString(Power_DR2002MO.OVP));
            //System.Threading.Thread.Sleep(30);
            //DR2003.OutPutOn();
            DR2003.Disconnect();

            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Awake, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Bat_En_Adapter, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Bat_En_Battery, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Bat_En_Charging, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPDischarger, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPChargeFlashCap, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_OFF, 1);

            IT6512C = new IT6512C(Power_IT6512C.usbId);
            IT6512C.Connect();
            IT6512C.SetVOLT(Convert.ToString(Power_IT6512C.V72));   //7.2V
            IT6512C.SetCURR(Convert.ToString(Power_IT6512C.CURR1)); //1A
            IT6512C.SetLoadInternalCURR1P5();
            IT6512C.SetLoadOn();
            IT6512C.OutPutOn();
            IT6512C.Disconnect();
            System.Threading.Thread.Sleep(200);
            //StaticValues.hwTestRPCDevice.ConfigurePin(StaticValues.uC_BatVoltage,
            //       HardwareTestRPCSerialDevice.PinTypeValue.AdcPin,
            //       HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);

            if (StaticValues.hwTestRPCDevice.ConfigureADC(0, 0, 2, 2))
            {
                //Console.WriteLine("ConfigureADC uC_BatVoltage pass ");
            }
            //ushort data = 0;
            //sMeasured = "";
            //if (StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_BatFull, ref data)) {
            //    //Console.WriteLine("uC_BatFull = "+data);
            //}

            System.Threading.Thread.Sleep(200);
            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            //ushort data1 = 0;
            //if (StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_BatFull, ref data1))
            //{
            //    //Console.WriteLine("uC_BatFull = " + data1);
            //}
            //return WORK_STATUS.PASS;

            ushort data = 0;
            for (int i = 0; i < 20; i++)
            {
                ushort data0 = 0;
                StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_BatVoltage, ref data0);
                data += data0;
            }

            //if (StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_BatVoltage, ref data))
            //{
                double ADCin = 1.0 * (double)data / 4095 / 20;
                double Resdiv = 9.30; // (274.0 + 33.0) / 33.0;
                double volt = ADCin * Resdiv;
                Console.WriteLine("data =  " + data + " ; ADCin = " + ADCin + " ; Resdiv = " + Resdiv + " ; volt = " + volt );


                StaticValues.step115_P1 = volt;
                sMeasured = Convert.ToString(volt);
            //}

            if (!PassFailJudge())
            {
                return WORK_STATUS.FAIL;
            }

            if (!Golden.verified && !Golden.empertyData && bNeedOffset)
            {
                if (!offsetCalculate())
                    return WORK_STATUS.FAIL;
            }

            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            DR2003 = null;
            IT6512C = null;
        }
    }


    class TestItemStep115_P2 : TestItemForm
    {
        public string step = "_115 P2";
        private DAQadc taskADC;

        public TestItemStep115_P2()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //DAQ setting
            try
            {
                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP100_AI16);

                if (taskADC.start())
                    return WORK_STATUS.PASS;
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
        }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;

            if (taskADC.isEnable() == false)
            {
                if (taskADC.getErrorMsg() == null)      //must check if there were any error when DAQadc.callbackADC()
                {
                    double average = taskADC.getAverage(0);
                    double measured_v = average;// * (r1 + r2) / r2;
                    sMeasured = Convert.ToString(measured_v);
                    //Console.WriteLine("ai TP100_AI16 measured: {0}", measured_v);
                    StaticValues.step115_P2 = measured_v;

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
            taskADC = null;        //free memory
        }
    }


    class TestItemStep115_P3 : TestItemForm
    {
        public string step = "_115 P3";

        public TestItemStep115_P3()
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
            sMeasured = "";
            double Diff = StaticValues.step115_P1 - StaticValues.step115_P2;
            //Console.WriteLine("step 115 P3 diff = " + Diff);
            sMeasured = Convert.ToString(Diff);
            if (!PassFailJudge())
            {
                return WORK_STATUS.FAIL;
            }

            if (!Golden.verified && !Golden.empertyData && bNeedOffset)
            {
                if (!offsetCalculate())
                    return WORK_STATUS.FAIL;
            }

            return WORK_STATUS.PASS;
        }

        public override void release()
        {

        }
    }


    class TestItemStep115_P4 : TestItemForm
    {
        public string step = "_115 P4";
        private DAQadc taskADC;

        public TestItemStep115_P4()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //DAQ setting
            try
            {

                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_5);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP108_AI28);

                if (taskADC.start())
                    return WORK_STATUS.PASS;
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
        }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;

            if (taskADC.isEnable() == false)
            {
                if (taskADC.getErrorMsg() == null)      //must check if there were any error when DAQadc.callbackADC()
                {
                    double average = taskADC.getAverage(0);
                    double measured_v = average;// * (r1 + r2) / r2;
                    sMeasured = Convert.ToString(measured_v);
                    //Console.WriteLine("ai TP108_AI28 measured: {0}", measured_v);
                    StaticValues.step115_P4 = measured_v;

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
            taskADC = null;        //free memory
        }
    }


    class TestItemStep115_P5 : TestItemForm
    {
        public string step = "_115 P5";
       
        public TestItemStep115_P5()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //StaticValues.hwTestRPCDevice.ConfigurePin(StaticValues.uC_BatTemp,
            //       HardwareTestRPCSerialDevice.PinTypeValue.AdcPin,
            //       HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);

            if (StaticValues.hwTestRPCDevice.ConfigureADC(15, 2, 2, 2))
            {
                //Console.WriteLine("ConfigureADC uC_BatTemp pass ");
            }

            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            sMeasured = "";
            ushort data = 0;
            if (StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_BatTemp, ref data))
            {
                double ADCin = 3.3 * (double)data / 4095;

                double volt = ADCin;
                //Console.WriteLine("ADC =  " + data + " ; volt =  " + volt);
                float x = 0.00f;
                x = (float)volt;

                StaticValues.step115_P5 = volt;

                sMeasured = Convert.ToString(volt);
            }

            if (!PassFailJudge())
            {
                return WORK_STATUS.FAIL;
            }

            if (!Golden.verified && !Golden.empertyData && bNeedOffset)
            {
                if (!offsetCalculate())
                    return WORK_STATUS.FAIL;
            }

            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            
        }

    }


    class TestItemStep115_P6 : TestItemForm
    {
        public string step = "_115 P6";
       
        private DR2003 DR2003;    //Battery simulation
        public TestItemStep115_P6()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            DR2003 = new DR2003(Power_DR2002MO.comPort, Power_DR2002MO.baudRate);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            DR2003.Connect();
            sMeasured = DR2003.MeasureCURR();
            DR2003.Disconnect();
            Console.WriteLine(sMeasured);
            if (!PassFailJudge())
            {
                return WORK_STATUS.FAIL;
            }

            if (!Golden.verified && !Golden.empertyData && bNeedOffset)
            {
                if (!offsetCalculate())
                    return WORK_STATUS.FAIL;
            }

            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            DR2003 = null;
        }
    }


    class TestItemStep115_P7 : TestItemForm
    {
        public string step = "_115 P7";
        private int timeout;
        public TestItemStep115_P7()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            timeout = 0;
            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            ushort data = 1;
            sMeasured = "";
            if (StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_BatFull, ref data))
            {
                //Console.WriteLine("uC_BatFull = " + data);
                sMeasured = Convert.ToString(data);
                if (PassFailJudge())
                {
                    return WORK_STATUS.PASS;
                }
                else {
                    return WORK_STATUS.FAIL;
                }
            }
            timeout++;
            if (timeout > 4)
            {
                return WORK_STATUS.FAIL;
            }


            return WORK_STATUS.WORKING;
        }

        public override void release()
        {

        }
    }


    class TestItemStep115_P8 : TestItemForm
    {
        public string step = "_115 P8";

        private IT6512C IT6512C;    //Battery simulation
        public TestItemStep115_P8()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            IT6512C = new IT6512C(Power_IT6512C.usbId);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            sMeasured = "";
            double curr = 0;
            IT6512C.Connect();
            curr = -1.0 * Convert.ToDouble(IT6512C.MeasureCURR());
            sMeasured = curr.ToString();
            IT6512C.Disconnect();

            if (!PassFailJudge())
            {
                return WORK_STATUS.FAIL;
            }

            if (!Golden.verified && !Golden.empertyData && bNeedOffset)
            {
                if (!offsetCalculate())
                    return WORK_STATUS.FAIL;
            }

            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            IT6512C = null;
        }
    }


    class TestItemStep115_P9 : TestItemForm
    {
        public string step = "_115 P9";
        private DAQdio dio;

        public TestItemStep115_P9()
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
                dio.addOneLineWriteCH(IO_USB6525_A.Relay_BAT_Temp_TP108_K8_P07);
                dio.setOneData(true);

                //StaticValues.hwTestRPCDevice.ConfigurePin(StaticValues.uC_BatTemp,
                //       HardwareTestRPCSerialDevice.PinTypeValue.AdcPin,
                //       HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);
                if (StaticValues.hwTestRPCDevice.ConfigureADC(15, 2, 2, 2))
                {
                    //Console.WriteLine("ConfigureADC uC_BatTemp pass ");
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
            sMeasured = "";
            ushort data = 0;
            if (StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_BatTemp, ref data))
            {
                double ADCin = 3.3 * (double)data / 4095;

                double volt = ADCin;
                //Console.WriteLine("ADC =  " + data + " ; volt =  " + volt);

                sMeasured = Convert.ToString(volt);
            }
            
            if (!PassFailJudge())
            {
                return WORK_STATUS.FAIL;
            }

            if (!Golden.verified && !Golden.empertyData && bNeedOffset)
            {
                if (!offsetCalculate())
                    return WORK_STATUS.FAIL;
            }

            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            dio = null;
        }

    }


    class TestItemStep115_P10 : TestItemForm
    {
        public string step = "_115 P10";

        private IT6512C IT6512C;    //Battery simulation

        public TestItemStep115_P10()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            IT6512C = new IT6512C(Power_IT6512C.usbId);

            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            sMeasured = "";
            double Diff = StaticValues.step115_P4 - StaticValues.step115_P5;
            //Console.WriteLine("step 115 P10 diff = " + Diff);
            sMeasured = Convert.ToString(Diff);
            if (!PassFailJudge())
            {
                return WORK_STATUS.FAIL;
            }

            if (!Golden.verified && !Golden.empertyData && bNeedOffset)
            {
                if (!offsetCalculate())
                    return WORK_STATUS.FAIL;
            }

            return WORK_STATUS.PASS;
        }

        public override void release()
        {
            IT6512C.Connect();
            IT6512C.OutPutOff();
            IT6512C.Disconnect();

            IT6512C = null;
        }
    }
}
