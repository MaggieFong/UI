using System;
using Ivi.Visa;
using NationalInstruments.Visa;
using WindowsFormsAppTest1.Port;

namespace WindowsFormsAppTest1
{
    class LPS505N
    {
        private string Com_num; 
		private int Baud_Rate;
        public PortOperatorBase portOperatorBase;
        private bool connect_state;

        public LPS505N(string COM, int BR)
        {
            Com_num = COM;
			Baud_Rate = BR;
        }
        ~LPS505N()
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
                Console.WriteLine("Connect to LPS505N.\r\n");
                connect_state = true;
            }
            catch
            {
                Console.WriteLine("Connect to LPS505N FAIL!!!\r\n");
                connect_state = false;
            }
            return connect_state;
        }

        public void Disconnect()
        {
            try
            {
                portOperatorBase.Close();
                Console.WriteLine("Disconnect to LPS505N.\r\n");
            }
            catch
            {
                Console.WriteLine("Disconnect to LPS505N FAIL!!!\r\n");
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
        public void SetCh1VOLT(string vol)
        {
            string content;
            content = $"VOLT1 {vol}";              
            portOperatorBase.WriteLine(content);    //送出command
        }
		public void SetCh2VOLT(string vol)
        {
            string content;
            content = $"VOLT2 {vol}";              
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void SetCh3VOLT(string vol)
        {
            string content;
            content = $"VOLT3 {vol}";              
            portOperatorBase.WriteLine(content);    //送出command
        }		
		
        public void SetCh1CURR(string vol)
        {
            string content;
            content = $"CURR1 {vol}";              
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void SetCh2CURR(string vol)
        {
            string content;
            content = $"CURR2 {vol}";              
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void SetCh3CURR(string vol)
        {
            string content;
            content = $"CURR3 {vol}";              
            portOperatorBase.WriteLine(content);    //送出command
        }		
		

        public void Ch1OutPutOn()
        {
            string content;
            content = $"OUT1 1";
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void Ch2OutPutOn()
        {
            string content;
            content = $"OUT2 1";
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void Ch3OutPutOn()
        {
            string content;
            content = $"OUT3 1";
            portOperatorBase.WriteLine(content);    //送出command
        }		
        public void Ch1OutPutOff()
        {
            string content;
            content = $"OUT1 0";
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void Ch2OutPutOff()
        {
            string content;
            content = $"OUT2 0";
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void Ch3OutPutOff()
        {
            string content;
            content = $"OUT3 0";
            portOperatorBase.WriteLine(content);    //送出command
        }
        public string MeasureCh1CURR()
        {
            string content;
            string result;
            content = $"IOUT1?";
            portOperatorBase.WriteLine(content);    //送出command
            result = portOperatorBase.ReadLine();
            return result.Trim();
        }
        public string MeasureCh2CURR()
        {
            string content;
            string result;
            content = $"IOUT2?";
            portOperatorBase.WriteLine(content);    //送出command
            result = portOperatorBase.ReadLine();
            return result.Trim();
        }
        public string MeasureCh3CURR()
        {
            string content;
            string result;
            content = $"IOUT3?";
            portOperatorBase.WriteLine(content);    //送出command
            result = portOperatorBase.ReadLine();
            return result.Trim();
        }
    }
}