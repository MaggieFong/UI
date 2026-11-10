using System;
using Ivi.Visa;
using NationalInstruments.Visa;
using WindowsFormsAppTest1.Port;

namespace WindowsFormsAppTest1
{
    class DR2003
    {
        private string Com_num; 
		private int Baud_Rate;
        public PortOperatorBase portOperatorBase;
        private bool connect_state;

        public DR2003(string COM, int BR)
        {
            Com_num = COM;
			Baud_Rate = BR;
        }
        ~DR2003()
        {
            Com_num = null;
			
        }

        public bool Connect()
        {
            connect_state = false;
         
            portOperatorBase = new RS232PortOperator(Com_num , Baud_Rate, (SerialParity)0 , (SerialStopBitsMode)0 , (int)8 );
            try
            {
                portOperatorBase.Open();
                //Console.WriteLine("Connect to DR2003.\r\n");
                connect_state = true;
            }
            catch
            {
                //Console.WriteLine("Connect to DR2003 FAIL!!!\r\n");
                connect_state = false;
            }
            return connect_state;
        }

        public void Disconnect()
        {
            try
            {
                portOperatorBase.Close();
                //Console.WriteLine("Disconnect to DR2003.\r\n");
            }
            catch
            {
                //Console.WriteLine("Disconnect to DR2003 FAIL!!!\r\n");
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
        public void Reset()
        {
            
            string content = $"*RST";
            portOperatorBase.WriteLine(content);    //送出command
            
            
        }

        public bool GetOutStatus()
        {
            string result;
            string content;
            content = $"OUT?";
            portOperatorBase.WriteLine(content);    //送出command
            result = portOperatorBase.ReadLine();
            //Console.WriteLine("result = " + result);
            if(result.Contains("ON"))
            {
                return true;
            }
            return false;
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
            return result.Trim();
        }
        public void SetCURR(string vol)
        {
            string content;
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
            return result.Trim();
        }
        public void OutPutOn()
        {
            string content;
            content = $"OUT 1";
            portOperatorBase.WriteLine(content);    //送出command
        }

        public void OutPutOff()
        {
            string content;
            content = $"OUT 0";
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
    }
}