using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1
{
    using NationalInstruments;
    using NationalInstruments.DAQmx;
    using System.Collections;

    class DAQdio
    {
        public enum D_OUT_TYPE
        {
            VALUE_LOW,
            VALUE_HIGH
        };

        private NationalInstruments.DAQmx.Task taskWriter;
        private NationalInstruments.DAQmx.Task taskReader;
        private string errorMsg;
        private ArrayList writeValue;
        private string devname;
        public DAQdio(string dev)
        {
            taskWriter = new Task();
            taskReader = new Task();
            writeValue = new ArrayList();
            devname = dev;
        }

        ~DAQdio()
        {
            errorMsg = null;
            writeValue.Clear();
            writeValue = null;
            taskReader = null;
            taskWriter = null;

            //Console.WriteLine("DAQ DIO release!!!!!!");
        }

        public string getErrorMsg()
        {
            return errorMsg;
        }
// get all connected NI devices
		public string[] getGetPhysicaldev()
        {
            
            return DaqSystem.Local.Devices;
        }

        public void addWriteChannel(string dev,int port, int line, bool default_value)
        {
            string strPhyChName = devname + "/port" + port + "/line" + line;      //ex: "Dev1/port0/line5"
            try
            {
                taskWriter.DOChannels.CreateChannel(strPhyChName, "", ChannelLineGrouping.OneChannelForEachLine);
                writeValue.Add(default_value);
            }
            catch (DaqException exception)
            {
                // Display Errors
                //MessageBox.Show(exception.Message);
                errorMsg = exception.Message;
                taskWriter.Dispose();

                throw new Exception(exception.Message);
            }
            catch (System.AccessViolationException e)
            {
                errorMsg = e.Message;
                taskWriter.Dispose();

                throw new Exception(e.Message);
            }
        }

        

        public void addReadChannel(string dev,int port, int line)
        {
            string strPhyChName = devname + "/port" + port + "/line" + line;      //ex: "Dev1/port0/line1"
            try
            {
                taskReader.DIChannels.CreateChannel(strPhyChName, "", ChannelLineGrouping.OneChannelForEachLine);
            }
            catch (DaqException exception)
            {
                // Display Errors
                //MessageBox.Show(exception.Message);
                errorMsg = exception.Message;
                taskReader.Dispose();

                throw new Exception(exception.Message);
            }
            catch (System.AccessViolationException e)
            {
                errorMsg = e.Message;
                taskReader.Dispose();

                throw new Exception(e.Message);
            }
        }

        public bool setData(int idx, bool value)
        {
            if (idx >= writeValue.Count)
            {
                errorMsg = "Write DAQ: Out of bounary";
                throw new Exception(errorMsg);
            }
            else
                writeValue[idx] = value;
            bool[] v = (bool[])writeValue.ToArray(typeof(bool));

            try
            {
                new DigitalSingleChannelWriter(taskWriter.Stream).WriteSingleSampleMultiLine(true, v);
            }
            catch (DaqException e)
            {
                errorMsg = e.Message;
                throw new Exception(e.Message);
            }
            return true;
        }

        public bool setDatas(bool[] value)
        {
            try
            {
                new DigitalSingleChannelWriter(taskWriter.Stream).WriteSingleSampleMultiLine(true, value);
            }
            catch(DaqException e)
            {
                errorMsg = e.Message;
                throw new Exception(e.Message);
            }

            return true;
        }

        public bool getData(int idx, ref bool value)
        {
            try
            {
                bool[] v = new DigitalSingleChannelReader(taskReader.Stream).ReadSingleSampleMultiLine();
                if (idx >= v.Length)
                {
                    errorMsg = "Read DAQ: Out of bounary";
                    throw new Exception(errorMsg);
                }
                else
                    value = v[idx];
            }
            catch (DaqException e)
            {
                errorMsg = e.Message;
                throw new Exception(e.Message);
            }
            return true;
        }
		public bool getDatas(ref bool[] value)
        {
            try { 
                value = new DigitalSingleChannelReader(taskReader.Stream).ReadSingleSampleMultiLine();
            }
            catch (DaqException e)
            {
                errorMsg = e.Message;
                throw new Exception(e.Message);
            }
            return true;

        }
/* NI IO API --- NI USB-6525
   Digital Output:Port0 (Line0~7) 
   Digital Inputput:Port1 (Line0~7) 
*/
		public void addOneLineWriteCH(int line)
        {
            try
            {
                taskWriter.DOChannels.CreateChannel(devname + "/port0/line"+line, "", ChannelLineGrouping.OneChannelForEachLine);
            }
            catch (DaqException exception)
            {
                // Display Errors
                //MessageBox.Show(exception.Message);
                errorMsg = exception.Message;
                taskWriter.Dispose();

                throw new Exception(exception.Message);
            }
            catch (System.AccessViolationException e)
            {
                errorMsg = e.Message;
                taskWriter.Dispose();

                throw new Exception(e.Message);
            }



        }
        public void addAllLineWriteCH()
        {
            //Console.WriteLine("addAllLineWriteCH");
            string strPhyChName = devname + "/port0";
            //Console.WriteLine("strPhyChName = {0}", strPhyChName);
            try
            {
                taskWriter.DOChannels.CreateChannel(strPhyChName, "port0", ChannelLineGrouping.OneChannelForAllLines);
                //taskWriter.DOChannels.CreateChannel(strPhyChName, "", ChannelLineGrouping.OneChannelForAllLines);
                //writeValue.Add(default_value);
            }
            catch (DaqException exception)
            {
                // Display Errors
                //MessageBox.Show(exception.Message);
                errorMsg = exception.Message;
                taskWriter.Dispose();

                throw new Exception(exception.Message);
            }
            catch (System.AccessViolationException e)
            {
                errorMsg = e.Message;
                taskWriter.Dispose();

                throw new Exception(e.Message);
            }
        }
		public void addAllLineReadCH(string dev)
        {
            //string strPhyChName = dev+"/port1";      //ex: "Dev1/port0/line1"
            try
            {
                taskReader.DIChannels.CreateChannel(devname + "/port1", "", ChannelLineGrouping.OneChannelForAllLines);
            }
            catch (DaqException exception)
            {
                // Display Errors
                //MessageBox.Show(exception.Message);
                errorMsg = exception.Message;
                taskReader.Dispose();

                throw new Exception(exception.Message);
            }
            catch (System.AccessViolationException e)
            {
                errorMsg = e.Message;
                taskReader.Dispose();

                throw new Exception(e.Message);
            }
        }

        public void addOneLineReadCH( int line)
        {
            string strPhyChName = devname + "/port1/line"+line;      //ex: "Dev1/port1/line1"
            try
            {
                taskReader.DIChannels.CreateChannel(strPhyChName, "", ChannelLineGrouping.OneChannelForEachLine);
            }
            catch (DaqException exception)
            {
                // Display Errors
                //MessageBox.Show(exception.Message);
                errorMsg = exception.Message;
                taskReader.Dispose();

                throw new Exception(exception.Message);
            }
            catch (System.AccessViolationException e)
            {
                errorMsg = e.Message;
                taskReader.Dispose();

                throw new Exception(e.Message);
            }
        }
		
		public bool setAllLineDatas(bool[] dataArray)
        {
            //Console.WriteLine("setAllLineDatas");
            try
            {
                new DigitalSingleChannelWriter(taskWriter.Stream).WriteSingleSampleMultiLine(true, dataArray);
                //Console.WriteLine("value = "+ dataArray);
            }
            catch(DaqException e)
            {
                errorMsg = e.Message;
                throw new Exception(e.Message);
            }

            return true;
        }
        public bool setOneData(bool val)
        {
            try
            {
                new DigitalSingleChannelWriter(taskWriter.Stream).WriteSingleSampleSingleLine(true,val);

            }
            catch (DaqException e)
            {
                errorMsg = e.Message;
                throw new Exception(e.Message);
            }

            return true;
        }
		public UInt32 getAllLineDatas()
        {
            UInt32 data = 0xff;
            try
            {
                //bool[] v = new DigitalSingleChannelReader(taskReader.Stream).ReadSingleSampleMultiLine();
                DigitalSingleChannelReader reader = new DigitalSingleChannelReader(taskReader.Stream);
                data = reader.ReadSingleSamplePortUInt32();
                

            }
            catch (DaqException e)
            {
                errorMsg = e.Message;
                throw new Exception(e.Message);
            }
            return data;
        }
		public bool getOneDatas()
        {
            bool data = false;
            try
            {
                DigitalSingleChannelReader reader = new DigitalSingleChannelReader(taskReader.Stream);
                data = reader.ReadSingleSampleSingleLine();
                //Console.WriteLine("read val1:" + val1);
               
            }
            catch (DaqException e)
            {
                errorMsg = e.Message;
                throw new Exception(e.Message);
            }
            return data;
        }
        
    }
}
