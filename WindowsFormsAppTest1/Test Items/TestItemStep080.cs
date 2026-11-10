using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;

    class TestItemStep080_P1 : TestItemForm
    {
        public string step = "_080 P1";
        private DR2003 DR2003;      //main power
        private IT6512C IT6512C;    //Battery simulation
        private DAQadc taskADC;
        private double r1 = 6.8;
        private double r2 = 6.8;
        
        public TestItemStep080_P1()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //DAQ setting
            try
            {
                DR2003 = new DR2003(Power_DR2002MO.comPort, Power_DR2002MO.baudRate);
                //DR2003.Connect();
                //DR2003.SetVOLT(Convert.ToString(Power_DR2002MO.V195));    //19.5V
                //System.Threading.Thread.Sleep(30);
                //DR2003.SetCURR(Convert.ToString(Power_DR2002MO.CURR1));   //1A
                //System.Threading.Thread.Sleep(30);
                //DR2003.SetOCPOn(Convert.ToString(Power_DR2002MO.OCP));
                //System.Threading.Thread.Sleep(30);
                //DR2003.SetOVPOn(Convert.ToString(Power_DR2002MO.OVP));
                //System.Threading.Thread.Sleep(30);
                //DR2003.OutPutOn();
                //DR2003.Disconnect();

                IT6512C = new IT6512C(Power_IT6512C.usbId);
                IT6512C.Connect();
                IT6512C.SetVOLT(Convert.ToString(Power_IT6512C.V72));   //7.2V
                IT6512C.SetCURR(Convert.ToString(Power_IT6512C.CURR1)); //1A
                IT6512C.SetLoadInternalCURR1P5();
                IT6512C.SetLoadOn();
                IT6512C.OutPutOn();
                IT6512C.Disconnect();

                System.Threading.Thread.Sleep(500);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPDischarger, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPChargeFlashCap, 0);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_OFF, 1);
                StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Awake, 1);

                //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Bat_En_Charging, 0);
                //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Bat_En_Adapter, 1);
                //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Bat_En_Battery, 0);

                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP301_AI8);

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
                    double measured_v = average * (r1 + r2) / r2;
                    sMeasured = Convert.ToString(measured_v);
                    //Console.WriteLine("ai TP301_AI8 average:{0}, measured: {1}", average, measured_v);

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
            DR2003 = null;
            IT6512C = null;
        }
    }
    
    class TestItemStep080_P2 : TestItemForm
    {
        public string step = "_080 P2";

        private int iCount = 0;
        public TestItemStep080_P2()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //StaticValues.hwTestRPCDevice.ConfigurePin(StaticValues.uC_AdapterConnectedPin,
            //                    HardwareTestRPCSerialDevice.PinTypeValue.InputPin,
            //                    HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);
            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            ushort data = 0;
            sMeasured = "";
            if (StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_AdapterConnectedPin, ref data))
            {
                sMeasured = Convert.ToString(data);
            }

            if (!PassFailJudge())
            {
                if (iCount > 3)
                {
                    return WORK_STATUS.FAIL;
                }
                else
                {
                    iCount++;
                    return WORK_STATUS.WORKING;
                }
            }
            else
            {
                return WORK_STATUS.PASS;
            }
        }

        public override void release()
        {

        }
    }


    class TestItemStep080_P3 : TestItemForm
    {
        public string step = "_080 P3";
        private DR2003 DR2003;      //main power

        public TestItemStep080_P3()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            // set to battery mode,turn off 19.5V
            DR2003 = new DR2003(Power_DR2002MO.comPort, Power_DR2002MO.baudRate);
            DR2003.Connect();
            bool isOutputOn = true;
            do
            {
                DR2003.OutPutOff();
                System.Threading.Thread.Sleep(30);
                isOutputOn = DR2003.GetOutStatus();
                //Console.WriteLine("isOutputOn = " + isOutputOn);
            } while (isOutputOn);
            DR2003.Disconnect();
            //putting the device in battery mode
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Bat_En_Adapter, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Bat_En_Battery, 1);
            // Wait 1.0 sec
            System.Threading.Thread.Sleep(1000);
            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            StaticValues.hwTestRPCDevice.ConfigurePin(StaticValues.uC_SupplyVoltage14V,
                   HardwareTestRPCSerialDevice.PinTypeValue.AdcPin,
                   HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);
            if (StaticValues.hwTestRPCDevice.ConfigureADC(0, 0, 2, 2))
            {
                //Console.WriteLine("ConfigureADC uC_SupplyVoltage14V pass ");

            }
            sMeasured = "";
            ushort data = 0;
            if (StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_SupplyVoltage14V, ref data))
            {
                double ADCin = 1.0 * (double)data / 4095;
                double Resdiv = (100 + 140 + 10) / 10;
                double volt = ADCin * Resdiv;
                //Console.WriteLine("ADC =  " + data + " ; volt =  " + volt);
                StaticValues.step080_P3 = volt;
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
            DR2003 = null;
        }

    }


    class TestItemStep080_P4 : TestItemForm
    {
        public string step = "_080 P4";
        private DAQadc taskADC;
        private double r1 = 6.8;
        private double r2 = 6.8;
        public TestItemStep080_P4()
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
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP301_AI8);

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
                    double measured_v = average * (r1 + r2) / r2;
                    sMeasured = Convert.ToString(measured_v);
                    //Console.WriteLine("ai TP301_AI8 average:{0}, measured: {1}", average, measured_v);
                    StaticValues.step080_P4 = measured_v;

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


    class TestItemStep080_P5 : TestItemForm
    {
        public string step = "_080 P5";

        public TestItemStep080_P5()
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
            double Diff = StaticValues.step080_P3 - StaticValues.step080_P4;
            //Console.WriteLine("step 080 P5 diff = " + Diff);
            sMeasured = Convert.ToString(Diff);
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
    class TestItemStep080_P6 : TestItemForm
    {
        public string step = "_080 P6";

        private int iCount = 0;
        public TestItemStep080_P6()
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
            ushort data = 1;
            sMeasured = "";
            if (StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_AdapterConnectedPin, ref data))
            {
                sMeasured = Convert.ToString(data);
            }

            if (!PassFailJudge())
            {
                if (iCount > 3)
                {
                    return WORK_STATUS.FAIL;
                }
                else
                {
                    iCount++;
                    return WORK_STATUS.WORKING;
                }
            }
            else
            {
                return WORK_STATUS.PASS;
            }
        }

        public override void release()
        {

        }
    }
    class TestItemStep080_P7 : TestItemForm
    {
        public string step = "_080 P7";
        private DR2003 DR2003;      //main power
        private DAQadc taskADC;
        private double r1 = 6.8;
        private double r2 = 6.8;
        public TestItemStep080_P7()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //19.5V supply is turned ON
            DR2003 = new DR2003(Power_DR2002MO.comPort, Power_DR2002MO.baudRate);
            DR2003.Connect();
            //DR2003.SetVOLT(Convert.ToString(Power_DR2002MO.V195));    //19.5V
            //DR2003.SetCURR(Convert.ToString(Power_DR2002MO.CURR1));   //1A
            //DR2003.SetOCPOn(Convert.ToString(Power_DR2002MO.OCP));
            //DR2003.SetOVPOn(Convert.ToString(Power_DR2002MO.OVP));

            bool isOutputOn = false;
            do
            {
                DR2003.OutPutOn();
                System.Threading.Thread.Sleep(30);
                isOutputOn = DR2003.GetOutStatus();
                //Console.WriteLine("isOutputOn = " + isOutputOn);
            } while (!isOutputOn);
            DR2003.Disconnect();

            // set to adapter mode
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Bat_En_Adapter, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Bat_En_Battery, 0);
            // Wait 0.2 sec
            System.Threading.Thread.Sleep(200);
            //DAQ setting
            try
            {
                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP301_AI8);

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
                    double measured_v = average * (r1 + r2) / r2;
                    sMeasured = Convert.ToString(measured_v);
                    //Console.WriteLine("ai TP301_AI8 average:{0}, measured: {1}", average, measured_v);

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
            DR2003 = null;
        }
    }

    
    class TestItemStep080_P8 : TestItemForm
    {
        public string step = "_080 P8";

        private int iCount = 0;

        public TestItemStep080_P8()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            StaticValues.hwTestRPCDevice.ConfigurePin(StaticValues.uC_AdapterConnectedPin,
                                HardwareTestRPCSerialDevice.PinTypeValue.InputPin,
                                HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            ushort data = 0;
            sMeasured = "";
            if (StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_AdapterConnectedPin, ref data))
            {
                sMeasured = Convert.ToString(data);
            }

            if (!PassFailJudge())
            {
                if (iCount > 3)
                {
                    return WORK_STATUS.FAIL;
                }
                else
                {
                    iCount++;
                    return WORK_STATUS.WORKING;
                }
            }
            else
            {
                return WORK_STATUS.PASS;
            }

        }

        public override void release()
        {

        }
    }
}
