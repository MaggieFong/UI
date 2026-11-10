using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RPCSerialDevice;
using System.ComponentModel;
//using ObjectDumper;
namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;
    using NationalInstruments.DAQmx;
    using System.Diagnostics;

    class TestItemStep500 : TestItemForm
    {
        public string step = "_500";

        //private DAQdio dioA;
        private DAQdio dioB, dioB1;

        public TestItemStep500()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            try
            {
                if (StaticValues.hwTestRPCDevice != null)
                {
                    StaticValues.hwTestRPCDevice.Dispose();
                    StaticValues.hwTestRPCDevice = null;
                }
                StaticValues.rpcSerialDevice = new RPCSerialDevice.Device(StaticValues.sSerialPort);

                dioB = new DAQdio(IO_USB6525_B.deviceName);
                dioB.addOneLineWriteCH(IO_USB6525_B.CTL_BUTTON_P04);
                
                dioB.setOneData(true);
                System.Threading.Thread.Sleep(200);
                dioB.setOneData(false);
                dioB = null;
                dioB1 = new DAQdio(IO_USB6525_B.deviceName);
                dioB1.addOneLineWriteCH(IO_USB6525_B.CTL_UnlockT_P03);
                //dioB1.setOneData(true);
                
                //dioB = null;
                StsCalibrationData data = StaticValues.rpcSerialDevice.GetStsCalibrationData();
                Console.Write(ObjectDumper.Dump(data));
                SkinAnalysisMeasurements datas = StaticValues.rpcSerialDevice.SkinGetLatestMeasurement();
                Console.Write(ObjectDumper.Dump(datas));
                //SystemTemperatureValues_ datatemp = StaticValues.rpcSerialDevice.GetTemperatures();
                System.Threading.Thread.Sleep(5000);
                dioB1.setOneData(false);
                StsCalibrationData data1 = StaticValues.rpcSerialDevice.GetStsCalibrationData();
                Console.Write(ObjectDumper.Dump(data1));
                SkinAnalysisMeasurements datas1 = StaticValues.rpcSerialDevice.SkinGetLatestMeasurement();
                Console.Write(ObjectDumper.Dump(datas1));

                System.Threading.Thread.Sleep(5000);
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
            System.Threading.Thread.Sleep(200);
            //dioB.setAllLineDatas(IO_USB6525_B.OutDefault);
            dioB1 = null;
            if (StaticValues.rpcSerialDevice != null)
            {
                StaticValues.rpcSerialDevice.Dispose();
                StaticValues.rpcSerialDevice = null;
            }
        }
    }

    class TestItemStep600 : TestItemForm
    {
        public string step = "_600";

        private DAQdio dioA;
        private DAQdio dioB, dioB1;
        private Feasa_LED myFS_LED1;
        private void Init_Feasa()
        {
            Feasa_10F_A.ch1_D630o = new XY_Data();
            Feasa_10F_A.ch2_D631o = new XY_Data();
            Feasa_10F_A.ch3_D500w = new XY_Data();
            Feasa_10F_A.ch4_D510w = new XY_Data();
            Feasa_10F_A.ch5_D501w = new XY_Data();
            Feasa_10F_A.ch6_D612o = new XY_Data();
            Feasa_10F_A.ch7_D502w = new XY_Data();
            Feasa_10F_A.ch8_D511w = new XY_Data();

           
        }
        public TestItemStep600()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            Init_Feasa();
            try
            {
                dioA = new DAQdio(IO_USB6525_A.deviceName);
                dioA.addOneLineWriteCH(IO_USB6525_A.Relay_FlashButton_TP515_K6_P05);


                dioB = new DAQdio(IO_USB6525_B.deviceName);
                dioB.addOneLineWriteCH(IO_USB6525_B.CTL_BUTTON_P04);

                dioB.setOneData(true);
                System.Threading.Thread.Sleep(200);
                dioB.setOneData(false);
             
                System.Threading.Thread.Sleep(200);
                dioB.setOneData(true);
                System.Threading.Thread.Sleep(200);
                dioB.setOneData(false);
                System.Threading.Thread.Sleep(200);
                dioB.setOneData(true);
                System.Threading.Thread.Sleep(200);
                dioB.setOneData(false);
                System.Threading.Thread.Sleep(200);
                dioB.setOneData(true);
                System.Threading.Thread.Sleep(200);
                dioB.setOneData(false);
                System.Threading.Thread.Sleep(200);
                //dioB.setOneData(true);
                //System.Threading.Thread.Sleep(200);
                //dioB.setOneData(false);
                System.Threading.Thread.Sleep(200);
                dioB = null;
                dioB1 = new DAQdio(IO_USB6525_B.deviceName);
                dioB1.addOneLineWriteCH(IO_USB6525_B.CTL_UnlockT_P03);
                dioB1.setOneData(true);

                myFS_LED1 = new Feasa_LED(Feasa_10F_A.comPort);
                myFS_LED1.doC3Capture();
                

                //myFS_LED1.ReadXY(1, Feasa_10F_A.ch1_D630o);
                //myFS_LED1.ReadXY(2, Feasa_10F_A.ch2_D631o);
                myFS_LED1.ReadXY(3, Feasa_10F_A.ch3_D500w);
                myFS_LED1.ReadXY(4, Feasa_10F_A.ch4_D510w);
                myFS_LED1.ReadXY(5, Feasa_10F_A.ch5_D501w);
                //myFS_LED1.ReadXY(6, Feasa_10F_A.ch6_D612o);
                myFS_LED1.ReadXY(7, Feasa_10F_A.ch7_D502w);
                myFS_LED1.ReadXY(8, Feasa_10F_A.ch8_D511w);

                
                System.Threading.Thread.Sleep(5000);
                dioA.setOneData(true);
                System.Threading.Thread.Sleep(200);
                dioA.setOneData(false);

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
            System.Threading.Thread.Sleep(200);
            //dioB.setAllLineDatas(IO_USB6525_B.OutDefault);
            dioB1 = null;
            dioA = null;
        }
    }



}
