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

            // Sunstone Smart (simulation): the test steps are the rows of the tester log (config.ini
            // [Simulation] CSV_File), in log order; name, limits and error code come from the log.
            string log = Config.fileSimCsv;
            if (string.IsNullOrEmpty(log) || !System.IO.File.Exists(log))
            {
                // config.ini [Simulation] CSV_File not set: ask for the tester log
                using (System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog())
                {
                    dlg.Title = "Select tester log (simulation CSV)";
                    dlg.Filter = "Tester log (*.csv)|*.csv";
                    if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    {
                        System.Windows.Forms.MessageBox.Show("No tester log selected: the test item list is empty.\n" +
                            "Set config.ini [Simulation] CSV_File to the tester log.");
                        return;
                    }
                    log = dlg.FileName;
                    Config.fileSimCsv = log;
                }
            }
            foreach (string[] row in SimData.ReadLogSteps(log))
                arrayTestItems.Add(new LogSimTestItem(row));
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
