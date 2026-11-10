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
    using System.Diagnostics;
    using System.Windows.Forms;

    class DAQadc
    {
        public enum VOLTAGE_RANGE{
            RANGE_0_10,
            RANGE_0_5,
            RANGE_N5_10,
            RANGE_N5_5,
            RANGE_N5_0,
            RANGE_N10_10,
            RANGE_N10_5,
            RANGE_N10_0
        };

        private NationalInstruments.DAQmx.Task task;
        private AIChannel aIChannel;
        private AnalogMultiChannelReader reader;
        private AsyncCallback callback;
        private bool isStart;
        private double sampleRate, totalSampleTimes;
        private AnalogWaveform<double>[] data;
        private string errorMsg;
        private Array[] rawADC;
        private VOLTAGE_RANGE voltageRange;

        //public Array[] ADCdata;
        public DAQadc(int rate, int totalTime)
        {
            isStart = false;
            task = new Task();

            sampleRate = rate;
            totalSampleTimes = rate * totalTime / 1000;
        }

        public DAQadc(int rate, int totalTime, VOLTAGE_RANGE range)
        {
            isStart = false;
            task = new Task();

            sampleRate = rate;
            totalSampleTimes = rate * totalTime / 1000;
            voltageRange = range;
        }

        ~DAQadc(){
            errorMsg = null;
            data = null;
            aIChannel=null;
            if (rawADC != null)
            {
                for (int i = 0; i < rawADC.Length; i++)
                    rawADC[i] = null;
                rawADC = null;
            }
            task = null;

            Console.WriteLine("DAQ ADC release!!!!!!");
        }

        private void getVoltageRange(ref double min, ref double max)
        {
            switch (voltageRange)
            {
                case VOLTAGE_RANGE.RANGE_0_10:
                default:
                    max = 10;
                    min = 0;
                    break;
                case VOLTAGE_RANGE.RANGE_0_5:
                    max = 5;
                    min = 0;
                    break;
                case VOLTAGE_RANGE.RANGE_N5_10:
                    max = 10;
                    min = -5;
                    break;
                case VOLTAGE_RANGE.RANGE_N5_5:
                    max = 5;
                    min = -5;
                    break;
                case VOLTAGE_RANGE.RANGE_N5_0:
                    max = 0;
                    min = -5;
                    break;
                case VOLTAGE_RANGE.RANGE_N10_10:
                    max = 10;
                    min = -10;
                    break;
                case VOLTAGE_RANGE.RANGE_N10_5:
                    max = 5;
                    min = -10;
                    break;
                case VOLTAGE_RANGE.RANGE_N10_0:
                    max = 0;
                    min = -10;
                    break;
            }
        }

        public void addChannel(string dev,int num_ch)
        {
            string strPhyChName = dev+"/ai" + num_ch;
            string strAssignChName = "ADC" + num_ch;

            double maxV = 0, minV = 0;
            getVoltageRange(ref minV, ref maxV);

            //Console.WriteLine("maxV: {0}, minV: {1}", maxV, minV);
            try
            {
                 task.AIChannels.CreateVoltageChannel(
                    strPhyChName,
                    strAssignChName,
                    AITerminalConfiguration.Rse,
                    minV,
                    maxV,
                    AIVoltageUnits.Volts);
                //aIChannel.LowpassEnable = true;
                //aIChannel.LowpassCutoffFrequency = Convert.ToDouble(new System.Decimal(new int[] {
                //                                                                        1,
                //                                                                        0,
                //                                                                        0,
                //                                                                        0}));


            }
            catch (DaqException exception)
            {
                // Display Errors
                //MessageBox.Show(exception.Message);
                errorMsg = exception.Message;
                task.Dispose();
                isStart = false;

                throw new Exception(errorMsg);
            }
            catch(System.AccessViolationException e)
            {
                errorMsg = e.Message;
                task.Dispose();
                isStart = false;

                throw new Exception(errorMsg);
            }
        }
        public void addDChannel(string dev, int num_ch)
        {
            string strPhyChName = dev + "/ai" + num_ch;
            string strAssignChName = "ADC" + num_ch;

            double maxV = 0, minV = 0;
            getVoltageRange(ref minV, ref maxV);

            //Console.WriteLine("maxV: {0}, minV: {1}", maxV, minV);
            try
            {
                task.AIChannels.CreateVoltageChannel(
                   strPhyChName,
                   strAssignChName,
                   AITerminalConfiguration.Differential,
                   minV,
                   maxV,
                   AIVoltageUnits.Volts);
                //aIChannel.LowpassEnable = true;
                //aIChannel.LowpassCutoffFrequency = Convert.ToDouble(new System.Decimal(new int[] {
                //                                                                        1,
                //                                                                        0,
                //                                                                        0,
                //                                                                        0}));


            }
            catch (DaqException exception)
            {
                // Display Errors
                //MessageBox.Show(exception.Message);
                errorMsg = exception.Message;
                task.Dispose();
                isStart = false;

                throw new Exception(errorMsg);
            }
            catch (System.AccessViolationException e)
            {
                errorMsg = e.Message;
                task.Dispose();
                isStart = false;

                throw new Exception(errorMsg);
            }
        }
        public void addNChannel(string dev, int num_ch)
        {
            string strPhyChName = dev + "/ai" + num_ch;
            string strAssignChName = "ADC" + num_ch;

            double maxV = 0, minV = 0;
            getVoltageRange(ref minV, ref maxV);

            //Console.WriteLine("maxV: {0}, minV: {1}", maxV, minV);
            try
            {
                task.AIChannels.CreateVoltageChannel(
                    strPhyChName,
                    strAssignChName,
                    AITerminalConfiguration.Nrse,
                    minV,
                    maxV,
                    AIVoltageUnits.Volts);
            }
            catch (DaqException exception)
            {
                // Display Errors
                //MessageBox.Show(exception.Message);
                errorMsg = exception.Message;
                task.Dispose();
                isStart = false;

                throw new Exception(errorMsg);
            }
            catch (System.AccessViolationException e)
            {
                errorMsg = e.Message;
                task.Dispose();
                isStart = false;

                throw new Exception(errorMsg);
            }
        }

        public void addNChannelWithRange(string dev, int num_ch, double max, double min)
        {
            string strPhyChName = dev + "/ai" + num_ch;
            string strAssignChName = "ADC" + num_ch;

            double maxV = 0, minV = 0;
            //getVoltageRange(ref minV, ref maxV);
            maxV = max;
            minV = min;

            //Console.WriteLine("maxV: {0}, minV: {1}", maxV, minV);
            try
            {
                task.AIChannels.CreateVoltageChannel(
                    strPhyChName,
                    strAssignChName,
                    AITerminalConfiguration.Nrse,
                    minV,
                    maxV,
                    AIVoltageUnits.Volts);
            }
            catch (DaqException exception)
            {
                // Display Errors
                //MessageBox.Show(exception.Message);
                errorMsg = exception.Message;
                task.Dispose();
                isStart = false;

                throw new Exception(errorMsg);
            }
            catch (System.AccessViolationException e)
            {
                errorMsg = e.Message;
                task.Dispose();
                isStart = false;

                throw new Exception(errorMsg);
            }
        }
        
        public Array getRawData(int idx)
        {
            if (idx >= rawADC.Length)
                return null;
            else
            {
                //foreach (double s in rawADC[idx])
                //    Console.WriteLine(s);
                return rawADC[idx];
            }
        }

        public double getAverage(int idx)
        {
            if (idx >= rawADC.Length)
                return -1;
            else
            {
                int count = 0;
                double average = 0;
                

                foreach (double v in rawADC[idx])
                {
                    if (average == 0)
                        average = v;
                    else
                        average = (average + v) / 2;
                    count += 1;
                }
                return average;
            }
        }

        public string getErrorMsg()
        {
            if (errorMsg != null)
            {
                Console.WriteLine("getErrorMsg:{0}", errorMsg);
            }
            return errorMsg;
        }

        public bool isEnable()
        {
            return isStart;
        }

        public bool start()
        {
            if (isStart == false)
            {
                try
                {
                    isStart = true;
                    Console.Write("NI API start {0}   samplerate {1}", task.AIChannels.Count.ToString(), totalSampleTimes,ToString());
                    rawADC = new Array[task.AIChannels.Count];      //prepare raw data buffer and store data later
                    for (int i = 0; i < task.AIChannels.Count; i++)
                    {
                        rawADC[i] = new double[(int)totalSampleTimes];
                        
                    }

                    task.Timing.ConfigureSampleClock("", sampleRate, SampleClockActiveEdge.Rising,
                        SampleQuantityMode.ContinuousSamples);//, (int)totalSampleTimes);      //internal clock trig.
                    
                    //aIChannel.LowpassEnable = true;
                    //aIChannel.LowpassCutoffFrequency = Convert.ToDouble(new System.Decimal(new int[] {
                    //                                                                    1,
                    //                                                                    0,
                    //                                                                    0,
                    //                                                                    0}));
                    task.Control(TaskAction.Verify);
                    


                      reader = new AnalogMultiChannelReader(task.Stream);
                    callback = new AsyncCallback(callbackADC);
                    reader.SynchronizeCallbacks = true;

                    reader.BeginReadWaveform((int)totalSampleTimes, callback, task);
                    Console.WriteLine("ADC start[{0}]: sample rate = {1}, total simple times: {2}",
                    DateTime.Now.Ticks / 10000, sampleRate, (int)totalSampleTimes);
                }
                catch (DaqException exception)
                {
                    // Display Errors
                    //MessageBox.Show(exception.Message);
                    errorMsg = exception.Message;
                    task.Dispose();
                    isStart = false;

                    throw new Exception(exception.Message);
                }
            }

            return isStart;
        }

        private void callbackADC(IAsyncResult ar)
        {
            Console.WriteLine("ADC end[{0}]", DateTime.Now.Ticks / 10000);
            try
            {
                if (task == ar.AsyncState)
                {
                    // Read the available data from the channels
                    data = reader.EndReadWaveform(ar);

                    // Iterate over channels
                    int idxCount;
                    int currentLineIndex = 0;
                    foreach (AnalogWaveform<double> waveform in data)
                    {
                        idxCount = 0;
                        rawADC[currentLineIndex].Initialize();
                        for (int sample = 0; sample < waveform.Samples.Count; ++sample)
                        {
                            rawADC[currentLineIndex].SetValue(waveform.Samples[sample].Value, sample);
                            idxCount += 1;
                            //dataTable.Rows[sample][currentLineIndex] = waveform.Samples[sample].Value;
                        }
                        currentLineIndex++;

                        //Console.WriteLine("" + idxCount);
                    }

                    task.Dispose();
                    reader = null;
                    isStart = false;
                    //reader.BeginMemoryOptimizedReadWaveform((int)totalSampleTimes, callback, task, data);
                }
            }
            catch (DaqException exception)
            {
                // Display Errors
                //MessageBox.Show(exception.Message);
                errorMsg = exception.Message;
                isStart = false;
                task.Dispose();
            }
        }
    }
}
