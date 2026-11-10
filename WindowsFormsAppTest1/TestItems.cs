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

            /* Step 001 read barcode */
            arrayTestItems.Add(new TestItemStep001());

            ///* Step 002 Close Jig */
            arrayTestItems.Add(new TestItemStep002_P1());
            arrayTestItems.Add(new TestItemStep002_P2());

            ///* Step 006 Switch on 19.5 V power supply, Set current limit to 1.0A */
            arrayTestItems.Add(new TestItemStep006_P1());
            arrayTestItems.Add(new TestItemStep006_P2());

            /////* Step 007 Program hardware test binary */
            arrayTestItems.Add(new TestItemStep007_P1());
            arrayTestItems.Add(new TestItemStep007_P2());

            ///* Step 008 Connect DUT serial port */
            //arrayTestItems.Add(new TestItemStep008());
            arrayTestItems.Add(new TestItemStep008_P1());
            //arrayTestItems.Add(new TestItemStep008_P2());



            ///* Step 010 Initialization of uC */
            arrayTestItems.Add(new TestItemStep010_P1());
            arrayTestItems.Add(new TestItemStep010_P2());
            arrayTestItems.Add(new TestItemStep010_P3());

            ///* Step 020 Button test */
            //arrayTestItems.Add(new TestItemStep020_P1());
            //arrayTestItems.Add(new TestItemStep020_P2());

            ///* Step 015 Supply voltages test */
            arrayTestItems.Add(new TestItemStep015_P1());
            arrayTestItems.Add(new TestItemStep015_P2());
            arrayTestItems.Add(new TestItemStep015_P3());
            arrayTestItems.Add(new TestItemStep015_P4());
            arrayTestItems.Add(new TestItemStep015_P5());
            arrayTestItems.Add(new TestItemStep015_P6());
            arrayTestItems.Add(new TestItemStep015_P7());
            
            arrayTestItems.Add(new TestItemStep020_P1());
            arrayTestItems.Add(new TestItemStep020_P2());

            /////* Step 021 LED test */
            arrayTestItems.Add(new TestItemStep021_P1());
            arrayTestItems.Add(new TestItemStep021_P2());
            arrayTestItems.Add(new TestItemStep021_P3());
            arrayTestItems.Add(new TestItemStep021_P4());
            arrayTestItems.Add(new TestItemStep021_P5());
            arrayTestItems.Add(new TestItemStep021_P6());
            arrayTestItems.Add(new TestItemStep021_P7());
            arrayTestItems.Add(new TestItemStep021_P8());
            arrayTestItems.Add(new TestItemStep021_P9());
            arrayTestItems.Add(new TestItemStep021_P10());
            arrayTestItems.Add(new TestItemStep021_P11());
            arrayTestItems.Add(new TestItemStep021_P12());
            arrayTestItems.Add(new TestItemStep021_P13());
            arrayTestItems.Add(new TestItemStep021_P14());
            arrayTestItems.Add(new TestItemStep021_P15());
            arrayTestItems.Add(new TestItemStep021_P16());
            arrayTestItems.Add(new TestItemStep021_P17());
            arrayTestItems.Add(new TestItemStep021_P18());
            arrayTestItems.Add(new TestItemStep021_P19());
            arrayTestItems.Add(new TestItemStep021_P20());
            arrayTestItems.Add(new TestItemStep021_P21());
            arrayTestItems.Add(new TestItemStep021_P22());
            arrayTestItems.Add(new TestItemStep021_P23());
            arrayTestItems.Add(new TestItemStep021_P24());
            arrayTestItems.Add(new TestItemStep021_P25());
            arrayTestItems.Add(new TestItemStep021_P26());
            arrayTestItems.Add(new TestItemStep021_P27());
            arrayTestItems.Add(new TestItemStep021_P28());
            arrayTestItems.Add(new TestItemStep021_P29());
            arrayTestItems.Add(new TestItemStep021_P30());
            arrayTestItems.Add(new TestItemStep021_P31());
            arrayTestItems.Add(new TestItemStep021_P32());
            arrayTestItems.Add(new TestItemStep021_P33());
            arrayTestItems.Add(new TestItemStep021_P34());
            arrayTestItems.Add(new TestItemStep021_P35());
            arrayTestItems.Add(new TestItemStep021_P36());
            arrayTestItems.Add(new TestItemStep021_P37());
            arrayTestItems.Add(new TestItemStep021_P38());
            arrayTestItems.Add(new TestItemStep021_P39());
            arrayTestItems.Add(new TestItemStep021_P40());
            arrayTestItems.Add(new TestItemStep021_P41());
            arrayTestItems.Add(new TestItemStep021_P42());
            arrayTestItems.Add(new TestItemStep021_P43());
            arrayTestItems.Add(new TestItemStep021_P44());
            arrayTestItems.Add(new TestItemStep021_P45());
            arrayTestItems.Add(new TestItemStep021_P46());
            arrayTestItems.Add(new TestItemStep021_P47());
            arrayTestItems.Add(new TestItemStep021_P48());
           

            ///* Step 082 Check Fan output */
            arrayTestItems.Add(new TestItemStep082_P1());
            arrayTestItems.Add(new TestItemStep082_P2());
            arrayTestItems.Add(new TestItemStep082_P3());

            /* Step 085 Check board temperature sensor */
            arrayTestItems.Add(new TestItemStep085_P1());
            arrayTestItems.Add(new TestItemStep085_P2());
            arrayTestItems.Add(new TestItemStep085_P3());
            arrayTestItems.Add(new TestItemStep085_P4());
            arrayTestItems.Add(new TestItemStep085_P5());
            arrayTestItems.Add(new TestItemStep085_P6());
            arrayTestItems.Add(new TestItemStep085_P7());
            arrayTestItems.Add(new TestItemStep085_P8());

            /* Step 097 Discharge flashcap; set initial condition */
            arrayTestItems.Add(new TestItemStep097());

            ///* Step 100 */
            arrayTestItems.Add(new TestItemStep100_P1());
            arrayTestItems.Add(new TestItemStep100_P2());
            arrayTestItems.Add(new TestItemStep100_P3());
            arrayTestItems.Add(new TestItemStep100_P4());
            arrayTestItems.Add(new TestItemStep100_P5());
            arrayTestItems.Add(new TestItemStep100_P6());
            arrayTestItems.Add(new TestItemStep100_P7());
            arrayTestItems.Add(new TestItemStep100_P8());
            arrayTestItems.Add(new TestItemStep100_P13());
            arrayTestItems.Add(new TestItemStep100_P9());
            arrayTestItems.Add(new TestItemStep100_P10());
            arrayTestItems.Add(new TestItemStep100_P11());
            arrayTestItems.Add(new TestItemStep100_P12());

            ///* Step 110 */
            arrayTestItems.Add(new TestItemStep110_P1());
            arrayTestItems.Add(new TestItemStep110_P2());
            arrayTestItems.Add(new TestItemStep110_P3());



            /* Step 117 Attachment connection test */
            arrayTestItems.Add(new TestItemStep117_P1());
            arrayTestItems.Add(new TestItemStep117_P2());
            arrayTestItems.Add(new TestItemStep117_P3());
            arrayTestItems.Add(new TestItemStep117_P4());
            arrayTestItems.Add(new TestItemStep117_P5());
            arrayTestItems.Add(new TestItemStep117_P6());
            arrayTestItems.Add(new TestItemStep117_P7());
            arrayTestItems.Add(new TestItemStep117_P8());
            arrayTestItems.Add(new TestItemStep117_P9());
            arrayTestItems.Add(new TestItemStep117_P10());
            arrayTestItems.Add(new TestItemStep117_P11());
            arrayTestItems.Add(new TestItemStep117_P12());
            arrayTestItems.Add(new TestItemStep117_P13());
            ///* Step 120 STS RED LED test */

            arrayTestItems.Add(new TestItemStep120_P3());
            arrayTestItems.Add(new TestItemStep120_P4());


            ///////* Step 130 STS IR LED LED test */

            arrayTestItems.Add(new TestItemStep130_P3());
            arrayTestItems.Add(new TestItemStep130_P4());


            ///* Step 135 STS photo/ temperature sensor test */
            arrayTestItems.Add(new TestItemStep135_P1());
            arrayTestItems.Add(new TestItemStep135_P2());
            arrayTestItems.Add(new TestItemStep135_P3());
            arrayTestItems.Add(new TestItemStep135_P4());
            arrayTestItems.Add(new TestItemStep135_P5());
            arrayTestItems.Add(new TestItemStep135_P6());
            ///* Step 137 Attachment EEPROM test */
            arrayTestItems.Add(new TestItemStep137_P1());
            arrayTestItems.Add(new TestItemStep137_P2());



            ///////* 22.18 Program application firmware and finalize test */
            arrayTestItems.Add(new TestItemStep22_18_1());
            arrayTestItems.Add(new TestItemStep22_18_2());
            arrayTestItems.Add(new TestItemStep22_18_3());
            arrayTestItems.Add(new TestItemStep22_18_4());
            arrayTestItems.Add(new TestItemStep22_18_5());
            arrayTestItems.Add(new TestItemStep22_18_6());
            arrayTestItems.Add(new TestItemStep22_18_7());
            arrayTestItems.Add(new TestItemStep22_18_8());

            /* Teest end and reset for next DUT */
            arrayTestItems.Add(new TestItemEnd());
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
