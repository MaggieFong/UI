using System;
using Ivi.Visa;
using NationalInstruments.Visa;
using WindowsFormsAppTest1.Port;

namespace WindowsFormsAppTest1
{
    class BK8601
    {
        private string devaddr;
        public PortOperatorBase portOperatorBase;
        private bool connect_state;

        public BK8601(string addr)
        {
            devaddr = addr;
        }
        ~BK8601()
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
                //Console.WriteLine("Connect to BK8601.\r\n");
                connect_state = true;
            }
            catch
            {
                //Console.WriteLine("Connect to BK8601 FAIL!!!\r\n");
                connect_state = false;
            }
            return connect_state;
        }

        public void Disconnect()
        {
            try
            {
                portOperatorBase.Close();
                //Console.WriteLine("Disconnect to BK8601.\r\n");
            }
            catch
            {
                //Console.WriteLine("Disconnect to BK8601 FAIL!!!\r\n");
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

        public string ReadBuffer( int count)
        {
            string result;
            string content;
            content = $"TRAC:DATA?";
            portOperatorBase.WriteLine(content);    //送出command
            result = portOperatorBase.ReadLineBuffer(count);
            return result;
        }

        public void SetCCMode()
        {
            string content;
            content = $"FUNC CURR";             
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void SetCVMode()
        {
            string content;
            content = $"FUNC VOLT";             
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void SetCWMode()
        {
            string content;
            content = $"FUNC POW";   
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void SetCRMode()
        {
            string content;
            content = $"FUNC RES";       
            portOperatorBase.WriteLine(content);    //送出command
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
        public void SetPOW(string vol)
        {
            string content;
            content = $"POW {vol}";           
            portOperatorBase.WriteLine(content);    //送出command
        }
        public string MeasurePOW()
        {
            string content;
            string result;
            content = $"POW?";
            portOperatorBase.WriteLine(content);    //送出command
            result = portOperatorBase.ReadLine();
            return result;
        }
        public void SetRES(string vol)
        {
            string content;
            content = $"RES {vol}";         
            portOperatorBase.WriteLine(content);    //送出command
        }
        public string MeasureRES()
        {
            string content;
            string result;
            content = $"RES?";
            portOperatorBase.WriteLine(content);    //送出command
            result = portOperatorBase.ReadLine();
            return result;
        }
        public void InPutOn()
        {
            string content;
            content = $"INP 1";
            portOperatorBase.WriteLine(content);    //送出command
        }

        public void InPutOff()
        {
            string content;
            content = $"INP 0";
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void TraceClear()
        {
            string content;
            content = $"TRAC:CLE";
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void TracePoints(string val)
        {
            string content;
            content = $"TRAC:POIN {val}";
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void TraceFeed(string val)
        {
            string content;
            content = $"TRAC:FEED {val}";
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void TraceDelay(string val)
        {
            string content;
            content = $"TRAC:DEL {val}";
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void TriggerSource(string val)
        {
            string content;
            content = $"TRIG:SOUR {val}";   //BUS , EXT , HOLD , MANU , TIM
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void TraceTim(string val)
        {
            string content;
            content = $"TRAC:TIM {val}";   
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void SourceRemoteSense(string val)
        {
            string content;
            content = $"SOUR:REM:SENS {val}";   
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void TraceFeedControl(string val)
        {
            string content;
            content = $"TRAC:FEED:CONT {val}";   //NEV , NEXT
            portOperatorBase.WriteLine(content);    //送出command
        }
        public void TriggerStart()
        {
            string content;
            content = $"*TRG";
            portOperatorBase.WriteLine(content);    //送出command
        }
    }
}