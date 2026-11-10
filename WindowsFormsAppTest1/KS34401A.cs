using System;
using Ivi.Visa;
using NationalInstruments.Visa;
using WindowsFormsAppTest1.Port;
using System.Threading;

namespace WindowsFormsAppTest1
{
    class KS34461A
    {
        private string devaddr;
        public PortOperatorBase portOperatorBase;
        private bool connect_state;
        public KS34461A(string addr)
        {
            devaddr = addr;
        }
    
        ~KS34461A()
        {
             devaddr = null;
			
        }

        public bool Connect()
        {
            connect_state = false;

            portOperatorBase = new USBPortOperator(devaddr);
            try
            {
                portOperatorBase.Open();
                //Console.WriteLine("Connect to IT6512C.\r\n");
                connect_state = true;
            }
            catch
            {
                //Console.WriteLine("Connect to IT6512C FAIL!!!\r\n");
                connect_state = false;
            }
            return connect_state;
        }

        public void Disconnect()
        {
            try
            {
                portOperatorBase.Close();
                //Console.WriteLine("Disconnect to IT6512C.\r\n");
            }
            catch
            {
                //Console.WriteLine("Disconnect to IT6512C FAIL!!!\r\n");
            }
        }

        public void Write(string content)
        {
                portOperatorBase.WriteLine(content);    //送出command
         }

        public string Read(string content)
        {
            string result;
            portOperatorBase.WriteLine(content);    //送出command
            result = portOperatorBase.ReadLine();
            return result;
        }

        public string IdentifyDevice()
        {
            string result;
            string content = $"*IDN?";
            portOperatorBase.WriteLine(content);    //送出command
            result = portOperatorBase.ReadLine();
            return result;
        }

        public void MeasurePreConfig()
        {
            string content;

            content = $"SYST:REM";
            portOperatorBase.WriteLine(content);
            Thread.Sleep(30);
            content = $"*CLS";
            portOperatorBase.WriteLine(content);
            Thread.Sleep(30);
            content = $"TRIG:SOUR IMM";
            portOperatorBase.WriteLine(content);
        }

        public string MeasureVOLT()
        {
            string content;
            string result;
            float value;

            content = $"MEAS:VOLT?";
            portOperatorBase.WriteLine(content);    //送出command
            Thread.Sleep(100);
            result = portOperatorBase.ReadLine();
            Console.WriteLine("MeasureVOLT:"+ result);
            value = float.Parse(result);
            //保留三位小数
            value = (float)Math.Round(value, 3);

            return result.Trim();
        }

        public string MeasureCURR()
        {
            string content;
            string result;
            float value;

            content = $"MEAS:CURR?";
            Thread.Sleep(100);
            result = portOperatorBase.ReadLine();

            value = float.Parse(result);
            //保留三位小数
            value = (float)Math.Round(value, 3);

            return result.Trim();
        }
    }
}