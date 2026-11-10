using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppTest1
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        //[DllImport("RPCSerialDevice.dll")]
        //public static extern RPCSerialDevice.Device Device(String com);
        static void Main(string[] args)
        {
            string mode = "";
            if (args.Length == 0)
            {
                MessageBox.Show("[WARNING] 人員手動開啟,進入Dubug Mode");
                mode = "debug";
                
            }
            else
            {
                if (args[0] == "VC") //Structure: Arg[0] = "VC", Arg[1]="";
                    mode = "production";
            }

            switch (mode)
            {
                case "debug":
                    StaticValues.MODE = "debug";
                    RunDebugMode();     //執行Debug模式4個指定步驟
                    break;
                case "production":
                    StaticValues.MODE = "production";
                    RunProductionMode();       //執行Normal模式程式
                    break;
                default:
                    Console.WriteLine("無效的運行模式,請使用 'debug' 或 'production'");
                    break;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PhilipsEmeraldTestTool());
        }
        static void RunDebugMode()
        {
            // Debug 模式下的程式碼
        }

        static void RunProductionMode()
        {
            // Production 模式下的程式碼
        }
    }
}
