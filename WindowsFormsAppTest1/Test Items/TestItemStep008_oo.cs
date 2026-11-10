using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;

    // 008
    class TestItemStep008 : TestItemForm
    {
        public string step = "_008";

        public TestItemStep008()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //Console.WriteLine(sTest + " is initial");
            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            //Console.WriteLine(sTest + " is proccessing");
            //StaticValues.sSerialPort = "COM" + Dut.comPort;
            
            if (Dut.comPort != null)
            {
                try
                {
                    if (StaticValues.hwTestRPCDevice == null)
                    {
                        System.Threading.Thread.Sleep(100);
                        StaticValues.hwTestRPCDevice = new HardwareTestRPCSerialDevice.Device(StaticValues.sSerialPort);
                        if (StaticValues.hwTestRPCDevice == null)
                            return WORK_STATUS.FAIL;
                    }

                    // Get uC interface version
                    //System.Threading.Thread.Sleep(100);
                    byte value = StaticValues.hwTestRPCDevice.GetInterfaceVersion();
                    
                    sMeasured = Convert.ToString(value);

                    //check limitation
                    if (PassFailJudge())
                    {
                        return WORK_STATUS.PASS;
                    }
                    else
                    {
                        return WORK_STATUS.FAIL;
                    }
                }
                catch (System.Exception e)
                {
                    sMeasured = e.Message;
                    return WORK_STATUS.FAIL;
                }
            }
            else
            {
                sMeasured = "Serial port error";
                return WORK_STATUS.FAIL;
            }
        }

        public override void release()
        {

        }
    }
}
