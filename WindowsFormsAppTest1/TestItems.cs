using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest1
{
    using System.Collections;
    using TestItemForm;
    using WindowsFormsAppTest1.TestItem;

    class TestItems
    {
        private ArrayList arrayTestItems;
        //private  long dStartTime;
        public TestItems()
        {
            arrayTestItems = new ArrayList();

            // Sunstone Smart (simulation): the test steps are the [TestStep/...] sections of Spec.ini, in file order.
            foreach (string step in SpecSteps(Config.fileSpec))
                arrayTestItems.Add(new SpecSimTestItem(step));
        }

        private static List<string> SpecSteps(string specFile)
        {
            List<string> steps = new List<string>();
            foreach (string raw in System.IO.File.ReadAllLines(specFile))
            {
                string line = raw.Trim();
                if (line.StartsWith("[TestStep/") && line.EndsWith("]"))
                    steps.Add(line.Substring("[TestStep/".Length, line.Length - "[TestStep/".Length - 1));
            }
            return steps;
        }

        public int getTestItemsCount()
        {
            return arrayTestItems.Count;
        }

        public TestItemForm getTestItem(int index)
        {
            return (TestItemForm)arrayTestItems[index];
        }

        public String getStepString(uint idx)
        {
            if (idx < arrayTestItems.Count)
            {
                int index = (int)idx;
                TestItemForm item = (TestItemForm)arrayTestItems[index];
                return item.sStep;
            }
            else
                return null;
        }

        public String getTestString(uint idx)
        {
            if (idx < arrayTestItems.Count)
            {
                int index = (int)idx;
                TestItemForm item = (TestItemForm)arrayTestItems[index];
                return item.sTest;
            }
            else
                return null;
        }

        public String getLowerLimitString(uint idx)
        {
            if (idx < arrayTestItems.Count)
            {
                int index = (int)idx;
                TestItemForm item = (TestItemForm)arrayTestItems[index];
                return item.sLowerLimit;
            }
            else
                return null;
        }

        public String getUpperLimitString(uint idx)
        {
            if (idx < arrayTestItems.Count)
            {
                int index = (int)idx;
                TestItemForm item = (TestItemForm)arrayTestItems[index];
                return item.sUpperLimit;
            }
            else
                return null;
        }

        public String getErrorCodeString(uint idx)
        {
            if (idx < arrayTestItems.Count)
            {
                int index = (int)idx;
                TestItemForm item = (TestItemForm)arrayTestItems[index];
                return item.sErrorCode;
            }
            else
                return null;
        }

        public String getMeasuredString(uint idx)
        {
            if (idx < arrayTestItems.Count)
            {
                int index = (int)idx;
                TestItemForm item = (TestItemForm)arrayTestItems[index];
                return item.sMeasured;
            }
            else
                return null;
        }

        public String getOffsetString(uint idx)
        {
            if (idx < arrayTestItems.Count)
            {
                int index = (int)idx;
                TestItemForm item = (TestItemForm)arrayTestItems[index];
                return item.sOffset;
            }
            else
                return null;
        }

        public String getExpectedString(uint idx)
        {
            if (idx < arrayTestItems.Count)
            {
                int index = (int)idx;
                TestItemForm item = (TestItemForm)arrayTestItems[index];
                return item.sExpected;
            }
            else
                return null;
        }

        public void setCalibratedString(uint idx, string fixture)
        {
            if (idx < arrayTestItems.Count)
            {
                int index = (int)idx;
                TestItemForm item = (TestItemForm)arrayTestItems[index];
                item.LoadCalibratedValue(fixture);
            }
        }

        public void setNoNeedCalibration(uint idx)
        {
            if (idx < arrayTestItems.Count)
            {
                int index = (int)idx;
                TestItemForm item = (TestItemForm)arrayTestItems[index];
                item.bNeedOffset = false;
            }
        }
        private int setProductFWLed_testTime(long starttime) 
        {
            long delta = DateTime.Now.Ticks - starttime;
            float tmpT = (float)(delta) / 10000; // ms
            int testTime =Convert.ToInt32 (tmpT % 675 +100);
            Console.WriteLine("Set Checking LED Setting 1 time: " + testTime);
            return testTime;

        }
    }
}
