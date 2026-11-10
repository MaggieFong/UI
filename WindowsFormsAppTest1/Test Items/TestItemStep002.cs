using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;

    class TestItemStep002_P1 : TestItemForm
    {
        public string step = "_002 P1";
        private DAQdio dio;
        public TestItemStep002_P1()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {

            //DIO setting
            try
            {
                Btn_status_init();
                dio = new DAQdio(IO_USB6525_B.deviceName);
                dio.addAllLineReadCH(null);
            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            try
            {
                uint jigClosesState = dio.getAllLineDatas();
                //Console.WriteLine(jigClosesState.ToString());
                if (jigClosesState == 0x3f)//if the jig is correctly closed
                {
                    sMeasured = "True";
                    return WORK_STATUS.PASS;
                }
                else
                {
                    sMeasured = "False";
                    return WORK_STATUS.FAIL;
                }
            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
        }

        public override void release()
        {
            dio = null;
        }
        private void Btn_status_init()
        {
            bool[] Btn_default = { false, false, false, false };
            Array.Copy(Btn_default, Step20_OutPut.Btn_On, 4);
            Array.Copy(Btn_default, Step20_OutPut.Btn_Flash, 4);

        }
    }

	class TestItemStep002_P2 : TestItemForm
    {
        public string step = "_002 P2";
        private DAQdio dioA, dioB;

        public TestItemStep002_P2()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            //DIO setting
            try
            {
                dioA = new DAQdio(IO_USB6525_A.deviceName);
                dioB = new DAQdio(IO_USB6525_B.deviceName);
                dioA.addAllLineWriteCH();
                dioB.addAllLineWriteCH();
            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {
            try
            {
                if (!dioA.setAllLineDatas(IO_USB6525_A.StartDefault) || !dioB.setAllLineDatas(IO_USB6525_B.StartDefault))
                {
                    sMeasured = "False";
                    return WORK_STATUS.FAIL;
                }

                sMeasured = "True";
                System.Threading.Thread.Sleep(200);
                return WORK_STATUS.PASS;
            }
            catch (Exception e)
            {
                sMeasured = e.Message;
                return WORK_STATUS.FAIL;
            }
        }

        public override void release()
        {
            dioA = null;
            dioB = null;
        }
    }
}
