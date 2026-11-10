using System;
using Ivi.Visa;
using NationalInstruments.Visa;
using WindowsFormsAppTest1.Port;

namespace WindowsFormsAppTest1
{
    class IT6332c
    {
        private string devaddr;
        public PortOperatorBase portOperatorBase;
        private bool connect_state;

        public IT6332c(string addr)
        {
            devaddr = addr;
        }
        ~IT6332c()
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
        public string IdentifyDevice()
        {
            string result;
            string content = $"*IDN?";
            portOperatorBase.WriteLine(content);    //送出command
           
            result = portOperatorBase.ReadLine();
            Console.WriteLine("ID:" + result);
            return result;
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

        public void SetVOLT(string vol)
        {
            string content;
            content = $"VOLT {vol}";               
            portOperatorBase.WriteLine(content);    //送出command
        }
        public string MeasureVOLT()
        {
            string content;
            string result;
            content = $"MEAS:VOLT?";
            portOperatorBase.WriteLine(content);    //送出command
            result = portOperatorBase.ReadLine();
            return result;
        }
        public void SetCURR(string vol)
        {
            string content;
            content = $"INST CH1";
            portOperatorBase.WriteLine(content);
            content = $"CURR {vol}";              
            portOperatorBase.WriteLine(content);    //送出command
        }
        public string MeasureCURR()
        {
            string content;
            string result;
            content = $"MEAS:CURR?";
            portOperatorBase.WriteLine(content);    //送出command
            result = portOperatorBase.ReadLine();
            return result;
        }
        public void OutPutOn()
        {
            string content;
            content = $"OUTP 1";
            portOperatorBase.WriteLine(content);    //送出command
        }

        public void OutPutOff()
        {
            string content;
            content = $"OUTP 0";
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void SetOCPOn(string val)
        {
            string content;
            content = $"OISET {val}";               
            portOperatorBase.WriteLine(content);    //送出command
            content = $"OCP 1";                
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void SetOCPOff()
        {
            string content;
            content = $"OCP 0";              
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void SetOVPOn(string val)
        {
            string content;
            content = $"OVSET {val}";          
            portOperatorBase.WriteLine(content);    //送出command
            content = $"OVP 1";           
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void SetOVPOff()
        {
            string content;
            content = $"OVP 0";  
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void SetLoadOff()
        {
            string content;
            content = $"Load 0";
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void SetLoadOn()
        {
            string content;
            content = $"Load 1";
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void SetLoadInternalCURR1P5()
        {
            string content;
            content = $"LOAD:SELection INT";
            portOperatorBase.WriteLine(content);    //送出command
            content = $"LOAD:CURR 1.5";
            portOperatorBase.WriteLine(content);    //送出command
        }
    }
}