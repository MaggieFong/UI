using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;

    // 001
    class TestItemStep001 : TestItemForm
    {
        public string step = "_001";
        private IT6332c IT6332c;
        private DAQdio dioA;
        public TestItemStep001()
        {
            //sStep = step;
            //LoadSpec();
            sStep = "005 P1";
            sTest = "ID code";
        }

        public override WORK_STATUS Init()
        {
           
            //try
            //{

            //    IT6332c = new IT6332c(Power_IT6332A.usbId);
            //    IT6332c.Connect();
            //    IT6332c.OutPutOn();
            //    System.Threading.Thread.Sleep(50);
            //    IT6332c.Disconnect();

            //    dioA = new DAQdio(IO_USB6525_A.deviceName);
            //    dioA.addOneLineWriteCH(IO_USB6525_A.Relay_Support3V3_TP322_K7_P06);
            //    dioA.setOneData(true);
            //}
            //catch (Exception e)
            //{
            //    sMeasured = e.Message.ToString();
            //    return WORK_STATUS.FAIL;
            //}
            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            if (StaticValues.sBarCode == null || StaticValues.sBarCode == "")
            {
                sMeasured = "No serial number";
                return WORK_STATUS.FAIL;
            }
            else
            {
                sMeasured = StaticValues.sBarCode;
                return WORK_STATUS.PASS;
            }
        }

        public override void release()
        {
            //IT6332c = null;
            //dioA = null;
        }
    }

    class TestItemStep001_0 : TestItemForm
    {
        public string step = "_001";
        private DAQadc taskADC, taskADCb;
        private int samplerate = 10000;//0.1ms
        private bool NIADC_start = false;
        public TestItemStep001_0()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            try
            {
                taskADCb = new DAQadc(
                    samplerate, 4000, DAQadc.VOLTAGE_RANGE.RANGE_N10_10);
                //samplerate, 4000);
                taskADCb.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.AI24);
                //taskADCb.addNChannelWithRange(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.AI24, 5.0, -7.0);
                taskADC = new DAQadc(
                samplerate, 4000, DAQadc.VOLTAGE_RANGE.RANGE_N10_10);
                //samplerate, 4000);
                taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP101_AI1);
                //taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP104_AI0);
                //taskADC.addNChannelWithRange(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP101_AI1, 7.0, 0.0);
                //taskADC.addNChannelWithRange(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP104_AI0, 7.0, 0.0);
            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }

            //Console.WriteLine(" Init 111:");
            //taskADC1 = new DAQadc(
            //1000, 1000, DAQadc.VOLTAGE_RANGE.RANGE_N10_10);
            //taskADC1.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_A.AI31);
            //Console.WriteLine(" Init 222:");
            //taskADC = new DAQadc(
            //    1000, 1000, DAQadc.VOLTAGE_RANGE.RANGE_N10_10);


            ////taskADC.addNChannelWithRange(DAQ_USB6218_B.deviceName, DAQ_USB6218_A.AI31, 5.0, -7.0);
            //taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP316_AI3);
            //taskADC.addChannel(DAQ_USB6218_A.deviceName, DAQ_USB6218_A.TP104_AI0);
            //Console.WriteLine(" Init 333:");
            //if (taskADC.start() && taskADC1.start())
            //{
            //    Console.WriteLine(" Init 333:");
            //    return WORK_STATUS.PASS;
            //}
            //else
            //{
            //    Console.WriteLine(" Init 444:");
            //    sMeasured = taskADC.getErrorMsg();
            //    return WORK_STATUS.FAIL;
            //}
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {

            if (!NIADC_start)
            {
                try
                {
                    //Console.WriteLine("taskADC start 1 ");
                    taskADC.start();
                    taskADCb.start();
                    NIADC_start = true;
                    //Console.WriteLine("taskADC start 2");
                    if (!ReadADC())
                    {
                        return WORK_STATUS.FAIL;
                    }
                    Console.WriteLine("ReadADC done");
                }
                catch (Exception e)
                {
                    sMeasured = e.Message;
                    return WORK_STATUS.FAIL;
                }
            }
            if (taskADCb != null)
            {
                if (taskADCb.isEnable() == false && NIADC_start)
                {
                    if (taskADCb.getErrorMsg() == null)
                    {
                        taskADCb = null;
                        Console.WriteLine("taskADCb done");
                        //return WORK_STATUS.PASS;
                    }
                    else
                    {
                        sMeasured = taskADCb.getErrorMsg();
                        return WORK_STATUS.FAIL;
                    }
                    //Console.WriteLine("taskADCb done value = {0}", taskADCb.getAverage(0));
                    //taskADCb = null;
                }
            }
            if (taskADCb ==null && taskADC.isEnable() == false)
            {
                Console.WriteLine("taskADC.isEnable() done  value = {0} ,{1}",
                    taskADC.getAverage(0), taskADC.getAverage(1));
                return WORK_STATUS.PASS;
                //taskADC = null;
                //if (taskADC.getErrorMsg() == null)
                //{
                //    Console.WriteLine("taskADC  done ###");
                //    return WORK_STATUS.PASS;
                //}
                //else
                //{
                //    sMeasured = taskADC.getErrorMsg();
                //    return WORK_STATUS.FAIL;
                //}

            }
            return WORK_STATUS.WORKING;

        }
        private bool ReadADC()
        {
            
            double startT = 0.5;
            double stopT = startT + 0.5;
            
            long dStartTime;
            long delta;
            int cnt = 0;
            
            for (double x = 0.0; x < 4.1; x += 0.05)// 50ms
            {
                cnt++;
                Console.WriteLine("cnt = " + cnt);
                dStartTime = DateTime.Now.Ticks;
          
                delta = DateTime.Now.Ticks - dStartTime;
                float testTime = (float)(delta / 10000);
                //Console.WriteLine("testTime = " + testTime);
                if (testTime < 50)
                {
                    int delayTime = Convert.ToInt32(50.0 - testTime);
                    //Console.WriteLine("delay T = " + delayTime.ToString());
                    System.Threading.Thread.Sleep(delayTime);
                }
            }

        
            return true;

        }
        /*public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;

            if (taskADC.isEnable() == false )
            {
                Console.WriteLine("taskADC 111:");
                if (taskADC.getErrorMsg() == null )      //must check if there were any error when DAQadc.callbackADC()
                {
                    if (taskADC1.isEnable() == false)
                    {
                        Console.WriteLine("taskADC1  6666:");
                        if (taskADC1.getErrorMsg() == null)      //must check if there were any error when DAQadc.callbackADC()
                        {
                            double average = taskADC1.getAverage(0);
                            double measured_v = average;
                            sMeasured = Convert.ToString(measured_v);
                            Console.WriteLine(" measured: {0},{1},{2}", measured_v, taskADC.getAverage(0),
                                taskADC1.getAverage(0));
                            re = WORK_STATUS.PASS;
                        }
                        else
                        {
                            sMeasured = taskADC.getErrorMsg();
                            re = WORK_STATUS.FAIL;
                        }
                    }
                    

                }
                else
                {
                    sMeasured = taskADC.getErrorMsg();
                    re = WORK_STATUS.FAIL;
                }
            }
            Console.WriteLine(" polling:");
            return re;
        }
        */
        public override void release()
        {
            taskADC = null;
            taskADCb = null;
            
            NIADC_start = false;
        }
    }
}
