using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;
    using NationalInstruments.DAQmx;
    using System.Diagnostics;


/*
    class TestItemStep082 : TestItemForm // not check 
    {
        public string step = "_082";

        
        public TestItemStep082()
        {
            sStep = step;
            LoadSpec();            
        }

        public override WORK_STATUS Init()
        {
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPDischarger, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPChargeFlashCap, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_ON, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Awake, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Bat_En_Adapter, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Bat_En_Battery, 0);
            Fan_ON(true);

            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            sMeasured = "";
            return WORK_STATUS.PASS;
        }

        public override void release()
        {

        }

        private void Fan_ON(bool on)
        {
            ushort val = 0;
            if (on)
            {
                val = 1;
            }
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Cooling, val);
        }
    }
*/

    class TestItemStep082_P1 : TestItemForm
    {
        public string step = "_082 P1";      //AI25
        private DAQadc taskADC;
        private double r1 = 10;     //10k
        private double r2 = 10;     //10k

        public TestItemStep082_P1()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            StaticValues.step82_P2 = 999;
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPDischarger, 0);
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CPChargeFlashCap, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_ON, 1); //##
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Attachment_EN , 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Awake, 1);
            // Fan on
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Cooling, 1);
            System.Threading.Thread.Sleep(500);
            //DAQ setting
            try
            {
                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP314_AI25);
                //taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP327_AI17);
                //taskADC.addChannel(DAQ_USB6218.deviceName, DAQ_USB6218.TP511_AI18);

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
                    sMeasured = measured_v.ToString("F3");
                    //double average1 = taskADC.getAverage(1);
                    //double measured_v1 = average1 * (r1 + r2) / r2;
                    //sMeasured = Convert.ToString(measured_v);
                    //StaticValues.step82_P2 = measured_v1;
                    Console.WriteLine("ai TP314_AI25 average:{0}, measured: {1}", average, measured_v);
                    //Console.WriteLine("ai TP327_AI17 average:{0}, measured: {1}", average1, measured_v1);
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
                        // Fan off
                        StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Cooling, 0);
                        System.Threading.Thread.Sleep(200);
                        // Fan on
                        StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Cooling, 1);
                        System.Threading.Thread.Sleep(200);
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
            taskADC = null;
            // Fan Off
           // StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Cooling, 0);
        }
    }


    class TestItemStep082_P2 : TestItemForm
    {
        public string step = "_082 P2";  //AI17
        private DAQadc taskADC;
        private double r1 = 10;     //10k
        private double r2 = 10;     //10k
        private int testCount;
        public TestItemStep082_P2()
        {
            sStep = step;
            LoadSpec();            
        }

        public override WORK_STATUS Init()
        {

            //DAQ setting
            try
            {
                testCount = 0;
                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_N5_5);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                //taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP342_AI25);
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP315_AI17);
                //taskADC.addChannel(DAQ_USB6218.deviceName, DAQ_USB6218.TP511_AI18);

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

                    sMeasured = measured_v.ToString("F3");//Convert.ToString(measured_v);

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
                        if (testCount > 20)
                        {
                            //sMeasured = "False";
                            return WORK_STATUS.FAIL;

                        }
                        taskADC = null;
                        taskADC = new DAQadc(
                   10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_N5_5);  
                        taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP315_AI17);
                        taskADC.start();
                        testCount++;

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
            // Fan Off
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Cooling, 0);
            taskADC = null;        //free memory
        }
    }
    class TestItemStep082_P3 : TestItemForm
    {
        public string step = "_082 P3";  //AI17
        private DAQadc taskADC;
        private double r1 = 10;     //10k
        private double r2 = 10;     //10k
        public TestItemStep082_P3()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //DAQ setting
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Cooling, 0);
            System.Threading.Thread.Sleep(100);
            //ushort DACval = 511;
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_SCAir, 0);
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_SCAred, 1);
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_SCA_I_DAC, DACval);
            try
            {
                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP315_AI17);
                //taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP511_AI18);
                //taskADC.addDChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP501_AI4);
                if (taskADC.start())
                {
                    StaticValues.hwTestRPCDevice.ConfigurePin(StaticValues.uC_AirOutTemp,
                   HardwareTestRPCSerialDevice.PinTypeValue.AdcPin,
                   HardwareTestRPCSerialDevice.PullResistorConfiguration.PullNone);
                    if (StaticValues.hwTestRPCDevice.ConfigureADC(15, 2, 2, 2))
                    {
                        //Console.WriteLine("ConfigureADC uC_AirOutTemp pass ");

                    }
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
                    //sMeasured = Convert.ToString(measured_v);
                    sMeasured = measured_v.ToString("F3");
                    ushort data = 0;
                    StaticValues.hwTestRPCDevice.GetValue(StaticValues.uC_AirOutTemp, ref data);
                    //Console.WriteLine(" uC_AirOutTemp ADC = "  +data);
                    if (data > 0)
                    {
                        double volt = 3.3 * (double)data / 4095;
                        //Console.WriteLine("ADC =  " + data + " ; volt =  " + volt);
                        StaticValues.step085_P1 = volt;
                        
                    }


                    Console.WriteLine("ai TP315_AI17 average:{0}, measured: {1}", average, measured_v);

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
            // Fan Off
            //StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_Cooling, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_CONV_ON, 0); //##
            taskADC = null;        //free memory
        }
    }
}
