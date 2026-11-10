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


    class TestItemStep006_P1 : TestItemForm
    {
        public string step = "_006 P1";
        private DR2003 DR2003;
        private DAQdio dio;
        private DAQadc taskADC;
        private double r1 = 2000;
        private double r2 = 1000;
        private int testCount;
        public TestItemStep006_P1()
        {
            sStep = step;
            LoadSpec();            
        }

        public override WORK_STATUS Init()
        {
            testCount = 0;
            try
            {
                dio = new DAQdio(IO_USB6525_A.deviceName);
                dio.addOneLineWriteCH(IO_USB6525_A.Relay_Support3V3_TP322_K7_P06);
                dio.setOneData(false);

                DR2003 = new DR2003(Power_DR2002MO.comPort, Power_DR2002MO.baudRate);
                DR2003.Connect();
                //DR2003.SetVOLT(Convert.ToString(Power_DR2002MO.V195));    //19.5V
                //System.Threading.Thread.Sleep(30);
                //DR2003.SetCURR(Convert.ToString(Power_DR2002MO.CURR1));   //1A
                //System.Threading.Thread.Sleep(30);
                //DR2003.SetOCPOn(Convert.ToString(Power_DR2002MO.OCP));
                //System.Threading.Thread.Sleep(30);
                //DR2003.SetOVPOn(Convert.ToString(Power_DR2002MO.OVP));
                System.Threading.Thread.Sleep(100);
                DR2003.OutPutOn();
                System.Threading.Thread.Sleep(100);
                if (!DR2003.GetOutStatus())
                {
                    System.Threading.Thread.Sleep(100);
                    Console.WriteLine("re turn on DR2002 -1 ");
                    DR2003.OutPutOn();
                    System.Threading.Thread.Sleep(100);
                    if (!DR2003.GetOutStatus())
                    {
                        System.Threading.Thread.Sleep(100);
                        Console.WriteLine("re turn on DR2002 -2 ");
                        DR2003.OutPutOn();
                        System.Threading.Thread.Sleep(100);
                    }
                }

                DR2003.Disconnect();
                System.Threading.Thread.Sleep(500);
                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP300_AI23);
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
                    double average = taskADC.getAverage(0) * (r1 + r2) / r2;
                    double measured_v = average;
                    sMeasured = measured_v.ToString("F3");
                    Console.WriteLine("ai TP111_AI23 measured: {0}", measured_v);

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
                        if (testCount > 3) {
                            return WORK_STATUS.FAIL;

                        }
                        taskADC = null;
                        taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                        taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP300_AI23);
                        taskADC.start();
                        testCount++;
                        //re = WORK_STATUS.FAIL;
                    }

                }
                else
                {
                    sMeasured = taskADC.getErrorMsg();
                    if (testCount > 3)
                    {
                        return WORK_STATUS.FAIL;

                    }
                    taskADC = null;
                    taskADC = new DAQadc(
                10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                    taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP300_AI23);
                    taskADC.start();
                    testCount++;
                    //re = WORK_STATUS.FAIL;
                }
            }

            return re;
        }

        public override void release()
        {

            taskADC = null;        //free memory
            DR2003 = null;
            dio = null;
            testCount= 0;
        }
    }


    class TestItemStep006_P2 : TestItemForm
    {
        public string step = "_006 P2";
        private DR2003 DR2003;
        private int testCount = 0;
        public TestItemStep006_P2()
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
            WORK_STATUS re = WORK_STATUS.WORKING;
            DR2003.Connect();
            sMeasured = DR2003.MeasureCURR();
            DR2003.Disconnect();
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

            testCount++;
            if (testCount > 10)
                re = WORK_STATUS.FAIL;

            return re;
        }

        public override void release()
        {
            DR2003 = null;
            testCount = 0;
        }
    }

    class TestItemStep006_P3 : TestItemForm
    {
        public string step = "_006 P3";
        private DAQadc taskADC;
        private DAQdio dio;
        public TestItemStep006_P3()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //DAQ setting
            try
            {
                dio = new DAQdio(IO_USB6525_A.deviceName);
                dio.addOneLineWriteCH(IO_USB6525_A.Relay_Support3V3_TP322_K7_P06);
                dio.setOneData(false);
                System.Threading.Thread.Sleep(200);
                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_5);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP328_AI6);

                if (taskADC.start())
                {
                    sMeasured = "init pass";
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

                    double measured_v = average;
                    sMeasured = measured_v.ToString("F3");
                    //Console.WriteLine("ai TP320_AI6  measured: {0}",  measured_v);

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
            //return WORK_STATUS.PASS;
        }

        public override void release()
        {
            //dio.setOneData(true);
            dio = null;
            taskADC = null;        //free memory
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }

    class TestItemStep006_P4 : TestItemForm
    {
        public string step = "_006 P4";
        private DR2003 DR2003;
        private int testCount = 0;
        public TestItemStep006_P4()
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
            WORK_STATUS re = WORK_STATUS.WORKING;
            DR2003.Connect();
            sMeasured = DR2003.MeasureCURR();
            DR2003.Disconnect();
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

            testCount++;
            if (testCount > 10)
                re = WORK_STATUS.FAIL;

            return re;
        }

        public override void release()
        {
            DR2003 = null;
            testCount = 0;
        }
    }

    class TestItemStep006_P5 : TestItemForm
    {
        public string step = "_006 P5";
        private DAQadc taskADC;
        private DAQdio dio;
        private int testCount;
        public TestItemStep006_P5()
        {
            sStep = step;
            LoadSpec();            
        }

        public override WORK_STATUS Init()
        {
            testCount = 0;
            //DAQ setting
            try
            {
                dio = new DAQdio(IO_USB6525_A.deviceName);
                dio.addOneLineWriteCH(IO_USB6525_A.Relay_Support3V3_TP322_K7_P06);
                dio.setOneData(true);

                taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP328_AI6);

                if (taskADC.start())
                {
                    sMeasured = "init pass";
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
             
                    double measured_v = average;
                    sMeasured = measured_v.ToString("F3");
                    Console.WriteLine("ai TP320_AI6  measured: {0}", measured_v);

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



                        if (testCount > 10)
                        {
                            sMeasured = "False";
                            return WORK_STATUS.FAIL;

                        }


                        dio.setOneData(false);
                        System.Threading.Thread.Sleep(300);
                        dio.setOneData(true);
                        System.Threading.Thread.Sleep(300);
                        taskADC = null;
                        taskADC = new DAQadc(
                   10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);  //sample rate: 10000hz, sample time: 300ms, voltage range: 0~10v
                        taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP328_AI6);
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
            //return WORK_STATUS.PASS;
        }

        public override void release()
        {
            dio = null;
            taskADC = null;        //free memory
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
