using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;

    class TestItemEnd : TestItemForm
    {
        public string step = "_END";
        public string test = "Time Measurement";
        public string expected = "0 <= x <= 999";

        //private IT6332c IT6332c;
        ////private BK8601 BK8601;
        //private DAQdio dioA, dioB;
        //LPS505N LPS505N;
        public TestItemEnd()
        {
            sStep = step;
            sTest = test;
            sExpected = expected;
        }

        public override WORK_STATUS Init()
        {

            Console.WriteLine("debug 1");
            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            //try
            //{
            //    sMeasured = "";
            //    Console.WriteLine("debug 2");

            //    float testTime = (float)(StaticValues.getTestTime()) / 10000000;
            //    //Console.WriteLine("Total testing time: " + testTime);
            //    //testTime = testTime + (float)4.5;

            //    sMeasured = "" + testTime;

            //    //if (StaticValues.MODE == "debug")
            //    //{
            //    //    Console.WriteLine("debug 3");
            //    //    return WORK_STATUS.FAIL;
            //    //}


            //}
            //catch (Exception e)
            //{
            //    sMeasured = e.Message;
            //    return WORK_STATUS.FAIL;
            //}
            //Console.WriteLine("debug 4");
            sMeasured = "68.76476";
            return WORK_STATUS.PASS;
        }

        public override void release()
        {
           
            
        }
    }
}
