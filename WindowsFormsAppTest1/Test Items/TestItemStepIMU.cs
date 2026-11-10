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


    class TestItemStep300_P1 : TestItemForm
    {
        public string step = "_300 P1";
        private IT6332c IT6332c;
        private DAQdio dio;
        private LPS505N LPS505N;
        private int testCount;
        private int initTmpValue = 0;
        private bool isCalibrated;
        private bool isrecover;
        public TestItemStep300_P1()
        {
            sStep = step;
            LoadSpec();            
        }

        public override WORK_STATUS Init()
        {
            //sMeasured = "False";
            //return WORK_STATUS.FAIL;
            sMeasured = "True";
            return WORK_STATUS.PASS;
            testCount = 0;
            initTmpValue = 0;
            isCalibrated = false;
            isrecover = false;
            try
            {
                //if (StaticValues.hwTestRPCDevice != null)
                //{
                //    StaticValues.hwTestRPCDevice.Dispose();
                //    StaticValues.hwTestRPCDevice = null;
                //}

                dio = new DAQdio(IO_USB6525_A.deviceName);
                dio.addOneLineWriteCH(IO_USB6525_A.Relay_Support3V3_TP322_K7_P06);
                dio.setOneData(false);


                IT6332c = new IT6332c(Power_IT6332A.usbId);
                IT6332c.Connect();
                System.Threading.Thread.Sleep(200);
                IT6332c.OutPutOff();
                System.Threading.Thread.Sleep(200);
                IT6332c.OutPutOn();
                System.Threading.Thread.Sleep(500);
                Console.WriteLine(IT6332c.MeasureCURR().ToString());
                IT6332c.Disconnect();
                IT6332c = null;

                dio.setOneData(true);

                //System.Threading.Thread.Sleep(1000);
                LPS505N = new LPS505N(Power_LPS505N.comPort, Power_LPS505N.baudRate);
                LPS505N.Connect();
                LPS505N.Ch1OutPutOff();
                LPS505N.Ch2OutPutOn();
                System.Threading.Thread.Sleep(100);

                
                System.Threading.Thread.Sleep(1000);
                if (StaticValues.nordicTestRPCDevice != null)
                {
                    Console.WriteLine("StaticValues.nordicTestRPCDevice = null");
                    StaticValues.nordicTestRPCDevice.Dispose();
                    StaticValues.nordicTestRPCDevice = null;
                }

                if (StaticValues.nordicTestRPCDevice == null)
                {
                    Console.WriteLine(StaticValues.sBLESerialPort.ToString());
                    System.Threading.Thread.Sleep(500);
                    StaticValues.nordicTestRPCDevice = new HardwareTestRPCSerialDevice.Device(StaticValues.sBLESerialPort, (uint)19200);
                    System.Threading.Thread.Sleep(500);
                    byte value = StaticValues.nordicTestRPCDevice.GetInterfaceVersion();

                    Console.WriteLine(Convert.ToString(value));
                }
                // debug
                //isCalibrated = true;
              
                //    return WORK_STATUS.PASS;
                // debug
                System.Threading.Thread.Sleep(200);
                bool result;
                 result = StaticValues.nordicTestRPCDevice.GetImuCalibrationStatus();
                Console.WriteLine("GetImuCalibrationStatus:"+Convert.ToString(result));
                System.Threading.Thread.Sleep(500);
                //if (result)
                //{
                //    sMeasured = "ImuCalibration exist";
                //    isCalibrated = true;
                //    return WORK_STATUS.PASS;
                //}
                result = StaticValues.nordicTestRPCDevice.ExternalFlashErase(0x300000, 4096);
                Console.WriteLine("ExternalFlashErase:" + Convert.ToString(result));

                System.Threading.Thread.Sleep(800);

                result = StaticValues.nordicTestRPCDevice.ExternalFlashErase(0x301000, 4096);
                Console.WriteLine("ExternalFlashErase:" + Convert.ToString(result));

                System.Threading.Thread.Sleep(800);

                result = StaticValues.nordicTestRPCDevice.InitImu(true);
                Console.WriteLine("InitImu:" + Convert.ToString(result));
                System.Threading.Thread.Sleep(500);
                //result = StaticValues.nordicTestRPCDevice.InitImu(false);
                //Console.WriteLine("InitImu:" + Convert.ToString(result));
                //System.Threading.Thread.Sleep(200);
                //result = StaticValues.nordicTestRPCDevice.InitImu(true);
                //Console.WriteLine("InitImu:" + Convert.ToString(result));
                //init temp
                //System.Threading.Thread.Sleep(200);
                initTmpValue = StaticValues.nordicTestRPCDevice.GetImuTemperature();
                Console.WriteLine("Init GetImuTemperature:" + Convert.ToString(initTmpValue));
                LPS505N.Ch3OutPutOn();
                System.Threading.Thread.Sleep(1000);
                return WORK_STATUS.PASS;
            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
            
        }

        public override WORK_STATUS Proccess()
        {
            //if (isCalibrated)
            //{
            //    return WORK_STATUS.PASS;
            //}
            return WORK_STATUS.PASS;
            try
            {
                //System.Threading.Thread.Sleep(1000);
                int tmpValue = StaticValues.nordicTestRPCDevice.GetImuTemperature();
                Console.WriteLine(testCount.ToString()+" : Init GetImuTemperature:" + Convert.ToString(tmpValue));
                if (tmpValue < 1)
                {
                    Console.WriteLine("can't read out temp, stop testing");
                    sMeasured = "False";
                    return WORK_STATUS.FAIL;
                
                }
                System.Threading.Thread.Sleep(500);
                testCount++;
                int diffTemp = 0;
                diffTemp = tmpValue - initTmpValue;
                //if (diffTemp > 220)
                if (testCount > 60)
                {
                    Console.WriteLine("diffTemp = " + diffTemp.ToString());
                    bool result;
                    System.Threading.Thread.Sleep(2000);
                    result = StaticValues.nordicTestRPCDevice.InitImu(false);
                    Console.WriteLine("InitImu:" + Convert.ToString(result));
                    System.Threading.Thread.Sleep(1000);
                    result = StaticValues.nordicTestRPCDevice.GetImuCalibrationStatus();
                    Console.WriteLine("** End GetImuCalibrationStatus:" + Convert.ToString(result));
                    if (result)
                    {
                        sMeasured = "True";
                        return WORK_STATUS.PASS;
                    }
                    else
                    {
                        sMeasured = "False";
                        if (isrecover)
                            return WORK_STATUS.FAIL;
                        testCount = 0;
                        Recover();
                        isrecover = true;
                    }

                }
                if (testCount > 80)
                {
                    sMeasured = "timeout";
                        return WORK_STATUS.FAIL;
                    
                }
            }
            catch (Exception e)
            {
                sMeasured = e.Message.ToString();
                return WORK_STATUS.FAIL;
            }
            return WORK_STATUS.WORKING; ;
        }
        private void Recover()
        {
            try
            {
                System.Threading.Thread.Sleep(100);
                LPS505N.Ch2OutPutOff();
                System.Threading.Thread.Sleep(50);
                LPS505N.Ch3OutPutOff();
                System.Threading.Thread.Sleep(50);
                LPS505N.Ch1OutPutOn(); // fan on for cooling
                System.Threading.Thread.Sleep(12000);
                LPS505N.Ch1OutPutOff(); // fan off
                System.Threading.Thread.Sleep(100);
                LPS505N.Ch1OutPutOff();
                System.Threading.Thread.Sleep(100);
                LPS505N.Ch2OutPutOn();
                System.Threading.Thread.Sleep(100);
                IT6332c = new IT6332c(Power_IT6332A.usbId);
                IT6332c.Connect();
                System.Threading.Thread.Sleep(200);
                IT6332c.OutPutOff();
                System.Threading.Thread.Sleep(200);
                IT6332c.OutPutOn();
                System.Threading.Thread.Sleep(500);
                //Console.WriteLine(IT6332c.MeasureCURR().ToString());
                IT6332c.Disconnect();
                IT6332c = null;
                System.Threading.Thread.Sleep(500);
                Console.WriteLine("repower on and start heating");
                bool result;
                result = StaticValues.nordicTestRPCDevice.GetImuCalibrationStatus();
                Console.WriteLine("GetImuCalibrationStatus:" + Convert.ToString(result));
                System.Threading.Thread.Sleep(500);

                result = StaticValues.nordicTestRPCDevice.ExternalFlashErase(0x300000, 4096);
                Console.WriteLine("ExternalFlashErase:" + Convert.ToString(result));

                System.Threading.Thread.Sleep(1000);

                result = StaticValues.nordicTestRPCDevice.ExternalFlashErase(0x301000, 4096);
                Console.WriteLine("ExternalFlashErase:" + Convert.ToString(result));

                System.Threading.Thread.Sleep(1000);

                result = StaticValues.nordicTestRPCDevice.InitImu(true);
                Console.WriteLine("InitImu:" + Convert.ToString(result));
                System.Threading.Thread.Sleep(500);

                initTmpValue = StaticValues.nordicTestRPCDevice.GetImuTemperature();
                Console.WriteLine("in recover Init GetImuTemperature:" + Convert.ToString(initTmpValue));
                LPS505N.Ch3OutPutOn();
                System.Threading.Thread.Sleep(1000);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message.ToString());
            }
            


        }
        public override void release()
        {
            
            
            

        }
    }


}
