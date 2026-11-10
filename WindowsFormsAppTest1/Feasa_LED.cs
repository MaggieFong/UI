using System;
using System.Text;
using System.Runtime.InteropServices;

namespace WindowsFormsAppTest1
{
    using System.Collections;

    public class RGB_Data
    {
        public int val_R;
        public int val_G;
        public int val_B;
        public int val_I_RGB;
    }
    public class XY_allData
    {
        public float val_X1;
        public float val_Y1;
        public int val_I_XY1;
        public float val_X2;
        public float val_Y2;
        public int val_I_XY2;
        public float val_X3;
        public float val_Y3;
        public int val_I_XY3;
        public float val_X4;
        public float val_Y4;
        public int val_I_XY4;
        public float val_X5;
        public float val_Y5;
        public int val_I_XY5;
        public float val_X6;
        public float val_Y6;
        public int val_I_XY6;
        public float val_X7;
        public float val_Y7;
        public int val_I_XY7;
        public float val_X8;
        public float val_Y8;
        public int val_I_XY8;
        public float val_X9;
        public float val_Y9;
        public int val_I_XY9;
        public float val_X10;
        public float val_Y10;
        public int val_I_XY10;

    }
    public class XY_Data
    {
        public float val_X;
        public float val_Y;
        public int val_I_XY;
    }
    public class CCT_Data
    {
        public int val_CCT1;
        public int val_CCT2;
        public int val_CCT3;
        public int val_CCT4;
        public int val_CCT5;
        public int val_CCT6;
        public int val_CCT7;
        public int val_CCT8;
        public int val_CCT9;
        public int val_CCT10;

    }
    public class WL_Data
    {
        public int val_WL;
        public int val_I_WL;
    }
    public class WL_allData
    {
        public int val_WL1;
        public int val_I_WL1;
        public int val_WL2;
        public int val_I_WL2;
        public int val_WL3;
        public int val_I_WL3;
        public int val_WL4;
        public int val_I_WL4;
        public int val_WL5;
        public int val_I_WL5;
        public int val_WL6;
        public int val_I_WL6;
        public int val_WL7;
        public int val_I_WL7;
        public int val_WL8;
        public int val_I_WL8;
        public int val_WL9;
        public int val_I_WL9;
        public int val_WL10;
        public int val_I_WL10;
    }
    class Feasa_LED
    {
        [DllImport("feasacom64.dll")]
        private static extern int FeasaCom_Open(int SerialNumber, string Baudrate);

        [DllImport("feasacom64.dll")]
        private static extern int FeasaCom_Close(int SerialNumber);

        [DllImport("feasacom64.dll")]
        private static extern int FeasaCom_Send(int SerialNumber, string Command, StringBuilder ResponseText);

        [DllImport("feasacom64.dll")]
        private static extern int FeasaCom_IsPortAvailable(int CommPort);

        private int dFeasa_port;
        private string errorMsg;
        private ArrayList readValue;
        int ret;
        StringBuilder sb_seqBuffer = new StringBuilder(10000);

        /* Feasa_LED constructor
         *      [Purpose]
         *      Open COM port
         *      
         *      [Argument]
         *      dFeasa_port : [INT32] COM port number
         *      
         */
        public Feasa_LED(int Feasa_port_num)
        {
            string baudrate = "57600";
            dFeasa_port = Feasa_port_num;

            ret = FeasaCom_Open(dFeasa_port, baudrate);
            //Console.WriteLine("Open COM port [" + dFeasa_port + "] " + ret);

            readValue = new ArrayList();
        }

        ~Feasa_LED()
        {
            //FeasaCom_Close(dFeasa_port);

            errorMsg = null;           
            readValue.Clear();
            readValue = null;

            //Console.WriteLine("Feasa_LED COM" + dFeasa_port + " release!!!!!!");
        }

        public string getErrorMsg()
        {
            return errorMsg;
        }

        /* ReadRGB
         *      [Purpose] 
         *      Read the RGB value of last capture
         *      
         *      [Argument]
         *      channel : requested channel number
         *      RGB_val : struct of RGB value
         *                  R,G,B : 0 - 255
         *                  Intensity : 0 - 99999
         */
        public void ReadRGB(int channel, RGB_Data RGB_val)
        {
            string strGetRGB = "getrgbi";
            string strChName = channel.ToString("00");            //Convert "1" to "01"
            StringBuilder sb_Ret_RGB = new StringBuilder(10000);
            string[] RGB_raw = new string[4];                     //R,G,B,Intensity
            int maxIntensity = -1;


            strGetRGB = strGetRGB + strChName;
            Console.WriteLine("ReadRGB 1 :"+ strGetRGB);
            try
            {
                ret = FeasaCom_Send(dFeasa_port, strGetRGB, sb_seqBuffer);
                Console.WriteLine(strGetRGB + " [" + sb_seqBuffer.ToString() + "] ret = " + ret);
                string[] strRawList = sb_seqBuffer.ToString().Split('\n');
                Console.WriteLine("strRawList len:" + strRawList.Length);
                RGB_val.val_R = -1;
                RGB_val.val_G = -1;
                RGB_val.val_B = -1;
                RGB_val.val_I_RGB = -1;
                for (int i = 0; i < strRawList.Length; i++)
                {
                    RGB_raw = strRawList[i].Split(' ');
                    Console.WriteLine("[{0}]  {1}  {2}   {3} ", i.ToString(), RGB_raw[0], ToString(), RGB_raw[1], ToString(), RGB_raw[2], ToString(), RGB_raw[3], ToString());
                    if (Int32.Parse(RGB_raw[3]) > maxIntensity)
                    {
                        Console.WriteLine("[{0}]", i.ToString());

                        maxIntensity = Int32.Parse(RGB_raw[3]);
                        RGB_val.val_R = Int32.Parse(RGB_raw[0]);
                        RGB_val.val_G = Int32.Parse(RGB_raw[1]);
                        RGB_val.val_B = Int32.Parse(RGB_raw[2]);
                        RGB_val.val_I_RGB = maxIntensity;
                    }
                }

                sb_seqBuffer.Clear();
            }
            catch (Exception e)
            {
                errorMsg = e.Message;
                Console.WriteLine(errorMsg);
                //throw new Exception(e.Message);
            }
            ///////

            ////


        }

        /* ReadWaveLength
         *      [Purpose] 
         *      Read the wave length value of last capture
         *      
         *      [Argument]
         *      channel : requested channel number
         *      WL_val : struct of wave length value
         *                  WL : 0 - 999
         *                  Intensity : 0 - 99999
         */
        public void ReadWaveLength(int channel, WL_Data WL_val)
        {
            string strGetWL = "getwi";
            string strChName = channel.ToString("00");            //Convert "1" to "01"
            StringBuilder sb_Ret_WL = new StringBuilder(25);
            string[] WL_raw = new string[2];                     //WaveLength,Intensity

            strGetWL = strGetWL + strChName;

            try
            {
                ret = FeasaCom_Send(dFeasa_port, strGetWL, sb_Ret_WL);
                //Console.WriteLine(strGetWL + " [" + sb_Ret_WL.ToString() + "] ret = " + ret);
                WL_raw = sb_Ret_WL.ToString().Split(' ');
                WL_val.val_WL = Int32.Parse(WL_raw[0]);
                WL_val.val_I_WL = Int32.Parse(WL_raw[1]);
            }
            catch (System.AccessViolationException e)
            {
                errorMsg = e.Message;

                throw new Exception(e.Message);
            }
        }

            public void Readrgbi(int channel, RGB_Data RGB_val)
        {
            string strGetWL = "getrgbi";
            string strChName = channel.ToString("00");            //Convert "1" to "01"
            StringBuilder sb_Ret_WL = new StringBuilder(10000);
            string[] RGB_raw = new string[4];                     //WaveLength,Intensity

            strGetWL = strGetWL + strChName;

            try
            {
                ret = FeasaCom_Send(dFeasa_port, strGetWL, sb_Ret_WL);
                //Console.WriteLine(strGetWL + " [" + sb_Ret_WL.ToString() + "] ret = " + ret);
                RGB_raw = sb_Ret_WL.ToString().Split(' ');
                RGB_val.val_R = Int32.Parse(RGB_raw[0]);
                RGB_val.val_G = Int32.Parse(RGB_raw[1]);
                RGB_val.val_B = Int32.Parse(RGB_raw[2]);
                RGB_val.val_I_RGB = Int32.Parse(RGB_raw[3]);
            }
            catch (System.AccessViolationException e)
            {
                errorMsg = e.Message;
                Console.WriteLine(errorMsg);
                //throw new Exception(e.Message);
            }
        }


        /* ReadXY
         *      [Purpose] 
         *      Read the chromaticity value of last capture
         *      
         *      [Argument]
         *      channel : requested channel number
         *      XY_val : struct of Chromaticity value
         *                  X,Y : 0 - 0.9999
         *                  Intensity : 0 - 99999
         *                  
         *      Notice: (X, Y) = (0.3333, 0.3333) => WHITE
         */
        public void ReadXYall(XY_allData XY_val)
        {
            string strGetXY = "getwiall";
            //string strChName = channel.ToString("00");              //Convert "1" to "01"
            StringBuilder sb_Ret_XY = new StringBuilder(250);
            string[] data_raw = new string[50];                        //X,Y,Intensity


            try
            {
                ret = FeasaCom_Send(dFeasa_port, strGetXY, sb_Ret_XY);
                Console.WriteLine(strGetXY + " [" + sb_Ret_XY.ToString() + "] ret = " + ret);
                char[] separator = { ' ', '\r', '\n' };
                data_raw = sb_Ret_XY.ToString().Split(separator);
                XY_val.val_X1 = float.Parse(data_raw[1]);
                XY_val.val_Y1 = float.Parse(data_raw[2]);
                XY_val.val_I_XY1 = Int32.Parse(data_raw[3]);
                //
                XY_val.val_X2 = float.Parse(data_raw[6]);
                XY_val.val_Y2 = float.Parse(data_raw[7]);
                XY_val.val_I_XY2 = Int32.Parse(data_raw[8]);
                //
                XY_val.val_X3 = float.Parse(data_raw[11]);
                XY_val.val_Y3 = float.Parse(data_raw[12]);
                XY_val.val_I_XY3 = Int32.Parse(data_raw[13]);
                //
                XY_val.val_X4 = float.Parse(data_raw[16]);
                XY_val.val_Y4 = float.Parse(data_raw[17]);
                XY_val.val_I_XY4 = Int32.Parse(data_raw[18]);
                //
                XY_val.val_X5 = float.Parse(data_raw[21]);
                XY_val.val_Y5 = float.Parse(data_raw[22]);
                XY_val.val_I_XY5 = Int32.Parse(data_raw[23]);
                //
                XY_val.val_X6 = float.Parse(data_raw[26]);
                XY_val.val_Y6 = float.Parse(data_raw[27]);
                XY_val.val_I_XY6 = Int32.Parse(data_raw[28]);
                //
                XY_val.val_X7 = float.Parse(data_raw[31]);
                XY_val.val_Y7 = float.Parse(data_raw[32]);
                XY_val.val_I_XY7 = Int32.Parse(data_raw[33]);
                //
                XY_val.val_X8 = float.Parse(data_raw[36]);
                XY_val.val_Y8 = float.Parse(data_raw[37]);
                XY_val.val_I_XY8 = Int32.Parse(data_raw[38]);
                //
                XY_val.val_X9 = float.Parse(data_raw[41]);
                XY_val.val_Y9 = float.Parse(data_raw[42]);
                XY_val.val_I_XY9 = Int32.Parse(data_raw[43]);
                //
                XY_val.val_X10 = float.Parse(data_raw[46]);
                XY_val.val_Y10 = float.Parse(data_raw[47]);
                XY_val.val_I_XY10 = Int32.Parse(data_raw[48]);
            }
            catch (System.AccessViolationException e)
            {
                errorMsg = e.Message;

                throw new Exception(e.Message);
            }
        }
        public void ReadWavelenall(WL_allData WL_val)
        {
            string strGetXY = "getwiall";
            //string strChName = channel.ToString("00");              //Convert "1" to "01"
            StringBuilder sb_Ret_XY = new StringBuilder(250);
            string[] data_raw = new string[50];                        //X,Y,Intensity


            try
            {
                ret = FeasaCom_Send(dFeasa_port, strGetXY, sb_Ret_XY);
                Console.WriteLine(strGetXY + " [" + sb_Ret_XY.ToString() + "] ret = " + ret);
                char[] separator = { ' ', '\r', '\n' };
                data_raw = sb_Ret_XY.ToString().Split(separator);

                for (int i = 0; i < data_raw.Length; i++)
                {
                    Console.WriteLine("data_raw[{0}]:{1}", i.ToString(), data_raw[i].ToString());
                }


                WL_val.val_WL1 = Int32.Parse(data_raw[1]);
                WL_val.val_I_WL1 = Int32.Parse(data_raw[2]);
                //
                WL_val.val_WL2 = Int32.Parse(data_raw[5]);
                WL_val.val_I_WL2 = Int32.Parse(data_raw[6]);
                //
                WL_val.val_WL3 = Int32.Parse(data_raw[9]);
                WL_val.val_I_WL3 = Int32.Parse(data_raw[10]);

                //
                WL_val.val_WL4 = Int32.Parse(data_raw[13]);
                WL_val.val_I_WL4 = Int32.Parse(data_raw[14]);

                //

                WL_val.val_WL5 = Int32.Parse(data_raw[17]);
                WL_val.val_I_WL5 = Int32.Parse(data_raw[18]);
                //

                WL_val.val_WL6 = Int32.Parse(data_raw[21]);
                WL_val.val_I_WL6 = Int32.Parse(data_raw[22]);
                //
                WL_val.val_WL7 = Int32.Parse(data_raw[25]);
                WL_val.val_I_WL7 = Int32.Parse(data_raw[26]);
                //
                WL_val.val_WL8 = Int32.Parse(data_raw[29]);
                WL_val.val_I_WL8 = Int32.Parse(data_raw[30]);
                //
                WL_val.val_WL9 = Int32.Parse(data_raw[33]);
                WL_val.val_I_WL9 = Int32.Parse(data_raw[34]);
                //
                WL_val.val_WL10 = Int32.Parse(data_raw[37]);
                WL_val.val_I_WL10 = Int32.Parse(data_raw[38]);
            }
            catch (System.AccessViolationException e)
            {
                errorMsg = e.Message;

                throw new Exception(e.Message);
            }
        }
        public void ReadXY(int channel, XY_Data XY_val)
        {
            string strGetXY = "getXYI";
            string strChName = channel.ToString("00");              //Convert "1" to "01"
            StringBuilder sb_Ret_XY = new StringBuilder(25);
            string[] XY_raw = new string[3];                        //X,Y,Intensity

            strGetXY = strGetXY + strChName;

            try
            {
                ret = FeasaCom_Send(dFeasa_port, strGetXY, sb_Ret_XY);
                Console.WriteLine(strGetXY + " [" + sb_Ret_XY.ToString() + "] ret = " + ret);
                XY_raw = sb_Ret_XY.ToString().Split(' ');
                XY_val.val_X = float.Parse(XY_raw[0]);
                XY_val.val_Y = float.Parse(XY_raw[1]);
                XY_val.val_I_XY = Int32.Parse(XY_raw[2]);
            }
            catch (System.AccessViolationException e)
            {
                errorMsg = e.Message;

                throw new Exception(e.Message);
            }
        }
        public void ReadCCT(int channel, CCT_Data cct_val)
        {
            string strGetXY = "getcctall";
            string strChName = channel.ToString("00");              //Convert "1" to "01"
            StringBuilder sb_Ret_XY = new StringBuilder(10000);
            string[] cct_raw = new string[20];                        //X,Y,Intensity

            //strGetXY = strGetXY + strChName;

            try
            {
                //ret = FeasaCom_Send(dFeasa_port, strGetXY, sb_Ret_XY);
                ret = FeasaCom_Send(dFeasa_port, strGetXY, sb_Ret_XY);
                Console.WriteLine(strGetXY + " [" + sb_Ret_XY.ToString() + "] ret = " + ret);
                cct_raw = sb_Ret_XY.ToString().Split(' ');
                for (int i = 0; i < cct_raw.Length; i++)
                {
                    Console.WriteLine("cct_raw[{0}]:{1}", i.ToString(), cct_raw[i].ToString());
                }
                Console.WriteLine("00000");
                cct_val.val_CCT1 = Int32.Parse(cct_raw[1]);
                Console.WriteLine("0001");
                cct_val.val_CCT2 = Int32.Parse(cct_raw[3]);
                Console.WriteLine("0003");
                cct_val.val_CCT3 = Int32.Parse(cct_raw[5]);
                Console.WriteLine("0005");
                cct_val.val_CCT4 = Int32.Parse(cct_raw[7]);
                Console.WriteLine("0007");
                cct_val.val_CCT5 = Int32.Parse(cct_raw[9]);
                cct_val.val_CCT6 = Int32.Parse(cct_raw[11]);
                cct_val.val_CCT7 = Int32.Parse(cct_raw[13]);
                cct_val.val_CCT8 = Int32.Parse(cct_raw[15]);
                cct_val.val_CCT9 = Int32.Parse(cct_raw[17]);
                cct_val.val_CCT10 = Int32.Parse(cct_raw[19]);
                Console.WriteLine("0000");
                //XY_val.val_I_XY = Int32.Parse(cct_raw[0]);
            }
            catch (System.AccessViolationException e)
            {
                errorMsg = e.Message;

                throw new Exception(e.Message);
            }
        }
        /* doCapture
         *      [Purpose]
         *      Capture LED color simultaneously
         *      
         *      [Argument]
         *      N/A
         *      
         *      [Notice]
         *      1. Do capture first, then ReadRGB or ReadXY
         *      2. It is ok if 
         *          (1) doCapture
         *          (2) ReadRGB
         *          (3) ReadXY
         *          
         *          The results of (2) and (3) are both from (1) accordingly
         */
        public bool doCapture()
        {
            string strPhyChName = "CAPTURE";
            StringBuilder sb_Ret_text = new StringBuilder(20);
            try
            {
                ret = FeasaCom_Send(dFeasa_port, strPhyChName, sb_Ret_text);
                Console.WriteLine(strPhyChName + " [" + sb_Ret_text.ToString() + "] ret = " + ret);
            }
            catch (System.AccessViolationException e)
            {
                errorMsg = e.Message;

                throw new Exception(e.Message);
            }

            return true;
        }

        public bool doC3Capture()
        {
            string strPhyChName = "C3";
            StringBuilder sb_Ret_text = new StringBuilder(20);
            try
            {
                ret = FeasaCom_Send(dFeasa_port, strPhyChName, sb_Ret_text);
                //Console.WriteLine(strPhyChName + " [" + sb_Ret_text.ToString() + "] ret = " + ret);
            }
            catch (System.AccessViolationException e)
            {
                errorMsg = e.Message;
                throw new Exception(e.Message);
            }

            return true;
        }

        public bool doC4Capture()
        {
            string strPhyChName = "C4";
            StringBuilder sb_Ret_text = new StringBuilder(20);
            try
            {
                ret = FeasaCom_Send(dFeasa_port, strPhyChName, sb_Ret_text);
                //Console.WriteLine(strPhyChName + " [" + sb_Ret_text.ToString() + "] ret = " + ret);
            }
            catch (System.AccessViolationException e)
            {
                errorMsg = e.Message;
                throw new Exception(e.Message);
            }

            return true;
        }

        public bool doSequenceCapture(int number, int captureTime, int waitTime)
        {
            string strSeqNumber = "putsequencenumber";
            string strSeqCapture = "putsequencecapture";
            string strSeqWait = "putsequencewait";

            string seqCount;
            if (number < 1000)
                seqCount = number.ToString("000");
            else
                seqCount = "999";

            strSeqNumber = strSeqNumber + seqCount;
            strSeqCapture = strSeqCapture + captureTime.ToString("00");
            strSeqWait = strSeqWait + waitTime.ToString("00");

            try
            {
                ret = FeasaCom_Send(dFeasa_port, "ENABLEEOT", sb_seqBuffer);
                Console.WriteLine("ENABLEEOT" + " [" + sb_seqBuffer.ToString() + "] ret = " + ret);
                sb_seqBuffer.Clear();

                ret = FeasaCom_Send(dFeasa_port, "putsequencemode0", sb_seqBuffer);
                sb_seqBuffer.Clear();

                ret = FeasaCom_Send(dFeasa_port, strSeqCapture, sb_seqBuffer);
                Console.WriteLine(strSeqCapture + " [" + sb_seqBuffer.ToString() + "] ret = " + ret);
                sb_seqBuffer.Clear();

                ret = FeasaCom_Send(dFeasa_port, strSeqWait, sb_seqBuffer);
                Console.WriteLine(strSeqWait + " [" + sb_seqBuffer.ToString() + "] ret = " + ret);
                sb_seqBuffer.Clear();

                ret = FeasaCom_Send(dFeasa_port, strSeqNumber, sb_seqBuffer);
                Console.WriteLine(strSeqNumber + " [" + sb_seqBuffer.ToString() + "] ret = " + ret);
                sb_seqBuffer.Clear();

                ret = FeasaCom_Send(dFeasa_port, "CS", sb_seqBuffer);
                Console.WriteLine("CS" + " [" + sb_seqBuffer.ToString() + "] ret = " + ret);
                sb_seqBuffer.Clear();
            }
            catch (System.AccessViolationException e)
            {
                errorMsg = e.Message;
                throw new Exception(e.Message);
            }

            return true;
        }

        public void ReadXYWithSeqMax(int channel, XY_Data XY_val)
        {
            string strGetXY = "getxyi";
            string strChName = channel.ToString("00");              //Convert "1" to "01"
            string[] XY_raw = new string[3];                        //X,Y,Intensity
            int maxIntensity = -1;

            strGetXY = strGetXY + strChName;

            try
            {
                ret = FeasaCom_Send(dFeasa_port, strGetXY, sb_seqBuffer);
                Console.WriteLine(strGetXY + " [" + sb_seqBuffer.ToString() + "] ret = " + ret);
                string[] strRawList = sb_seqBuffer.ToString().Split('\n');
                Console.WriteLine("strRawList len:"+ strRawList.Length);
                XY_val.val_X = -1;
                XY_val.val_Y = -1;
                XY_val.val_I_XY = -1;

                for (int i = 0; i < strRawList.Length; i++)
                {
                    XY_raw = strRawList[i].Split(' ');
                    Console.WriteLine("[{0}]  {1}  {2}   {3} ", i.ToString(), XY_raw[0],ToString(), XY_raw[1], ToString(), XY_raw[2], ToString());
                    if (Int32.Parse(XY_raw[2]) > maxIntensity)
                    {
                        Console.WriteLine("[{0}]", i.ToString());

                        maxIntensity = Int32.Parse(XY_raw[2]);
                        XY_val.val_X = float.Parse(XY_raw[0]);
                        XY_val.val_Y = float.Parse(XY_raw[1]);
                        XY_val.val_I_XY = Int32.Parse(XY_raw[2]);
                    }
                }

                sb_seqBuffer.Clear();
            }
            catch (System.AccessViolationException e)
            {
                errorMsg = e.Message;

                throw new Exception(e.Message);
            }
        }
        

        public void Close()
        {
            FeasaCom_Close(dFeasa_port);
        }
    }
}