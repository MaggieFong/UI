using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ShopFlowInformationSystem
{
    internal class TSfis
    {
        public string Send(string ipAddress, Int32 port, string message)
        {
            TcpClient tcpClient = new TcpClient();

            try
            {
                tcpClient.Connect(ipAddress, port);

                tcpClient.NoDelay = false;

                NetworkStream netStream = tcpClient.GetStream();

                string output = "";

                WriteLine(netStream, message);

                output = ReadLine(tcpClient, netStream, output);

                tcpClient.Close();

                return output;
            }
            catch (Exception ex)
            {
                tcpClient.Close();
                MessageBox.Show(ex.Message);
                return "SFIS Error!";
            }
        }

        private static void WriteLine(NetworkStream netStream, string write)
        {
            if (netStream.CanWrite)
            {
                byte[] writeData = Encoding.ASCII.GetBytes(write);

                netStream.Write(writeData, 0, writeData.Length);

                // 需等待資料真的已寫入 NetworkStream
                Thread.Sleep(100);
            }
        }


        private static string ReadLine(TcpClient tcpClient, NetworkStream netStream,
            string output)
        {
            if (netStream.CanRead)
            {
                byte[] bytes = new byte[tcpClient.ReceiveBufferSize];

                int numBytesRead = netStream.Read(bytes, 0,
                    (int)tcpClient.ReceiveBufferSize);

                byte[] bytesRead = new byte[numBytesRead];
                Array.Copy(bytes, bytesRead, numBytesRead);

                string returndata = Encoding.ASCII.GetString(bytesRead);

                output = returndata.Trim();
            }

            return output;
        }

    }
}
