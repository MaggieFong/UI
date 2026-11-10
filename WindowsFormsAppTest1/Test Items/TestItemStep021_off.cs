using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace WindowsFormsAppTest1.TestItem
{
    using TestItemForm;
 
    class TestItemStep021_P3 : TestItemForm
    {
        public string step = "_021 P3";

        public TestItemStep021_P3()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {
            return WORK_STATUS.PASS;
        }
        public override WORK_STATUS Proccess()
        {

            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UIstandby, 1);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet1, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet2, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet3, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet4, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UISet5, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedError, 0);
            StaticValues.hwTestRPCDevice.SetPin(StaticValues.uC_UILedRTF, 0);

            sMeasured = "True";
            return WORK_STATUS.PASS;
        }

        public override void release()
        {

        }
    }

    class TestItemStep021_P4 : TestItemForm
    {
       
        public string step = "_021 P4";
        private DAQadc taskADC;
        private DAQadc taskADC1;
        private DAQadc taskADC2;
        private DAQadc taskADC3;
        private DAQadc taskADC4;
        private DAQadc taskADC5;
        private DAQadc[] taskList;
        private double[] NIavg;
        private int taskind;
        public TestItemStep021_P4()
        {
            sStep = step;
            LoadSpec();
        }
        private void Init_taskADC()
        {
            taskADC = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);
            taskADC.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDSet1_I2_AI_1);//
            taskADC.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDSet2_I3_AI_2);//
            taskADC.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDSet3_I4_AI_3);//
            taskADC.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDSet4_I5_AI_4);
            taskADC.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDSet5_I6_AI_5);//
        }
        private void Init_taskADC1()
        {
            taskADC1 = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);
            taskADC1.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDSTD_STR1_I7_AI_6);//
            taskADC1.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDSTD_STR2_I8_AI_7);//
            //taskADC1.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDSTB_I12_AI_11);//
        }
        private void Init_taskADC2()
        {
            taskADC2 = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_0_10);
            taskADC2.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDRTF_STR1_I9_AI_8);//
            taskADC2.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDRTF_STR2_I10_AI_9);//
            taskADC2.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDRTF_STR3_I11_AI_10);//
        }
        private void Init_taskADC3()
        {
            taskADC3 = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_N5_5);
            //taskADC3.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDSTB_I12_AI_11);//
            //taskADC3.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_UI_Standby_I13_AI_12);//
            taskADC3.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_UISet1_I14_AI_13);//
            taskADC3.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_UISet2_I15_AI_14);//
            taskADC3.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_UISet3_I16_AI_15);//
            taskADC3.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_UISet4_I17_AI_16);//
            taskADC3.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_UISet5_I18_AI_17);//
            taskADC3.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_UILedError_I19_AI_18);//
            taskADC3.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_UILedRTF_I20_AI_19);//
        }
        private void Init_taskADC4()
        {
            taskADC4 = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_N5_5);
            taskADC4.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_UI_Standby_I13_AI_12);//
            
        }
        private void Init_taskADC5()
        {
            taskADC5 = new DAQadc(
                    10000, 100, DAQadc.VOLTAGE_RANGE.RANGE_N5_5);
            taskADC5.addChannel(DAQ_USB6218_B.deviceName, DAQ_USB6218_B.NI_B_LEDSTB_I12_AI_11);//

        }
        public override WORK_STATUS Init()
        {
            Init_taskADC();
            Init_taskADC1();
            Init_taskADC2();
            Init_taskADC3();
            Init_taskADC4();
            Init_taskADC5();
            taskList = new DAQadc[6];
            NIavg = new double[19];
            taskList[0] = taskADC;
            taskList[1] = taskADC1;
            taskList[2] = taskADC2;
            taskList[3] = taskADC3;
            taskList[4] = taskADC4;
            taskList[5] = taskADC5;
            taskind = 0;
            if (taskADC.start())
                return WORK_STATUS.PASS;
            else
            {
                return WORK_STATUS.FAIL;
            }
        }

        public override WORK_STATUS Proccess()
        {
            int[] chn1 = { 0,1,2,3,4 };
            int[] chn2 = { 5, 6  };
            int[] chn3 = { 7, 8, 9 };
            int[] chn4 = {  12 ,13,14,15,16,17,18};
            int[] chn5 = { 11 };
            int[] chn6 = { 10 };
            int[][] chArray = new int[][] { chn1, chn2 , chn3 , chn4, chn5, chn6 };

            WORK_STATUS re = WORK_STATUS.WORKING;
            if (taskList[taskind] != null)
            {
                if (taskList[taskind].isEnable() == false)
                {
                    if (taskList[taskind].getErrorMsg() == null)      //must check if there were any error when DAQadc.callbackADC()
                    {
                        int cnt = 0;
                        foreach(int i in chArray[taskind])
                        //for (int i = chstart[taskind]; i < chend[taskind] + 1; i++)
                        {
                            NIavg[i] = taskList[taskind].getAverage(cnt);
                            cnt++;
                            Console.WriteLine("i= {0} AI {1} = {2}", Convert.ToString(i),
                                Convert.ToString(cnt), NIavg[i].ToString());
                        }
                        taskList[taskind] = null;
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        taskind++;
                        if (taskind == taskList.Length) {
                            StaticValues.s21_NIdata = NIavg;
                            return WORK_STATUS.PASS;

                        }
                        bool val = taskList[taskind].start();

                    }
                    else
                    {
                        sMeasured = taskList[taskind].getErrorMsg();
                        re = WORK_STATUS.FAIL;
                    }
                }
            }

            return re;
        }

        public override void release()
        {
            //taskADC1 = null;
            NIavg = null;
            taskList = null;

        }


    }

    class TestItemStep021_P4_1 : TestItemForm
    {
        public string step = "_021 P4.1";
        public TestItemStep021_P4_1()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {

            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            sMeasured = StaticValues.s21_NIdata[12].ToString();
            //check limitation
            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                re = WORK_STATUS.PASS;
            }
            else
            {
                re = WORK_STATUS.FAIL;
            }
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P4_1a : TestItemForm
    {
        public string step = "_021 P4.1a";
        public TestItemStep021_P4_1a()
        {
            sStep = step;
            LoadSpec();
        }
        public override WORK_STATUS Init()
        { return WORK_STATUS.PASS; }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            // R1 =1100k , R2 = 1000k
            double val = StaticValues.s21_NIdata[0] * (2100 / 1000);
            sMeasured = val.ToString();
            //check limitation
            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                re = WORK_STATUS.PASS;
            }
            else
            {
                re = WORK_STATUS.FAIL;
            }
            return re;
        }
        public override void release()
        {

        }
    }

    class TestItemStep021_P4_2 : TestItemForm
    {
        public string step = "_021 P4.2";
        public TestItemStep021_P4_2()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {

            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            sMeasured = StaticValues.s21_NIdata[13].ToString();
            //check limitation
            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                re = WORK_STATUS.PASS;
            }
            else
            {
                re = WORK_STATUS.FAIL;
            }
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P4_2a : TestItemForm
    {
        public string step = "_021 P4.2a";
        public TestItemStep021_P4_2a()
        {
            sStep = step;
            LoadSpec();
        }
        public override WORK_STATUS Init()
        { return WORK_STATUS.PASS; }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            // R1 =1100k , R2 = 1000k
            double val = StaticValues.s21_NIdata[1] * (2100 / 1000);
            sMeasured = val.ToString();
            //check limitation
            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                re = WORK_STATUS.PASS;
            }
            else
            {
                re = WORK_STATUS.FAIL;
            }
            return re;
        }
        public override void release()
        {

        }
    }

    class TestItemStep021_P4_3 : TestItemForm
    {
        public string step = "_021 P4.3";
        public TestItemStep021_P4_3()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {

            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            sMeasured = StaticValues.s21_NIdata[14].ToString();
            //check limitation
            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                re = WORK_STATUS.PASS;
            }
            else
            {
                re = WORK_STATUS.FAIL;
            }
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P4_3a : TestItemForm
    {
        public string step = "_021 P4.3a";
        public TestItemStep021_P4_3a()
        {
            sStep = step;
            LoadSpec();
        }
        public override WORK_STATUS Init()
        { return WORK_STATUS.PASS; }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            // R1 =1100k , R2 = 1000k
            double val = StaticValues.s21_NIdata[2] * (2100 / 1000);
            sMeasured = val.ToString();
            //check limitation
            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                re = WORK_STATUS.PASS;
            }
            else
            {
                re = WORK_STATUS.FAIL;
            }
            return re;
        }
        public override void release()
        {

        }
    }

    class TestItemStep021_P4_4 : TestItemForm
    {
        public string step = "_021 P4.4";
        public TestItemStep021_P4_4()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {

            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            sMeasured = StaticValues.s21_NIdata[15].ToString();
            //check limitation
            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                re = WORK_STATUS.PASS;
            }
            else
            {
                re = WORK_STATUS.FAIL;
            }
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P4_4a : TestItemForm
    {
        public string step = "_021 P4.4a";
        public TestItemStep021_P4_4a()
        {
            sStep = step;
            LoadSpec();
        }
        public override WORK_STATUS Init()
        { return WORK_STATUS.PASS; }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            // R1 =1100k , R2 = 1000k
            double val = StaticValues.s21_NIdata[3] * (2100 / 1000);
            sMeasured = val.ToString();
            //check limitation
            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                re = WORK_STATUS.PASS;
            }
            else
            {
                re = WORK_STATUS.FAIL;
            }
            return re;
        }
        public override void release()
        {

        }
    }

    class TestItemStep021_P4_5 : TestItemForm
    {
        public string step = "_021 P4.5";
        public TestItemStep021_P4_5()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {

            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            sMeasured = StaticValues.s21_NIdata[16].ToString();
            //check limitation
            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                re = WORK_STATUS.PASS;
            }
            else
            {
                re = WORK_STATUS.FAIL;
            }
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P4_5a : TestItemForm
    {
        public string step = "_021 P4.5a";
        public TestItemStep021_P4_5a()
        {
            sStep = step;
            LoadSpec();
        }
        public override WORK_STATUS Init()
        { return WORK_STATUS.PASS; }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            // R1 =1100k , R2 = 1000k
            double val = StaticValues.s21_NIdata[4] * (2100 / 1000);
            sMeasured = val.ToString();
            //check limitation
            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                re = WORK_STATUS.PASS;
            }
            else
            {
                re = WORK_STATUS.FAIL;
            }
            return re;
        }
        public override void release()
        {

        }
    }

    class TestItemStep021_P4_6 : TestItemForm
    {
        public string step = "_021 P4.6";
        public TestItemStep021_P4_6()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {

            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            sMeasured = StaticValues.s21_NIdata[17].ToString();
            //check limitation
            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                re = WORK_STATUS.PASS;
            }
            else
            {
                re = WORK_STATUS.FAIL;
            }
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P4_6a : TestItemForm
    {
        public string step = "_021 P4.6a";
        public TestItemStep021_P4_6a()
        {
            sStep = step;
            LoadSpec();
        }
        public override WORK_STATUS Init()
        { return WORK_STATUS.PASS; }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            // R1 =1100k , R2 = 1000k
            double val = StaticValues.s21_NIdata[5] * (2100 / 1000);
            sMeasured = val.ToString();
            //check limitation
            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                re = WORK_STATUS.PASS;
            }
            else
            {
                re = WORK_STATUS.FAIL;
            }
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P4_6b : TestItemForm
    {
        public string step = "_021 P4.6b";
        public TestItemStep021_P4_6b()
        {
            sStep = step;
            LoadSpec();
        }
        public override WORK_STATUS Init()
        { return WORK_STATUS.PASS; }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            // R1 =1100k , R2 = 1000k
            double val = StaticValues.s21_NIdata[6] * (2100 / 1000);
            sMeasured = val.ToString();
            //check limitation
            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                re = WORK_STATUS.PASS;
            }
            else
            {
                re = WORK_STATUS.FAIL;
            }
            return re;
        }
        public override void release()
        {

        }
    }

    class TestItemStep021_P4_7 : TestItemForm
    {
        public string step = "_021 P4.7";
        public TestItemStep021_P4_7()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {

            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            sMeasured = StaticValues.s21_NIdata[18].ToString();
            //check limitation
            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                re = WORK_STATUS.PASS;
            }
            else
            {
                re = WORK_STATUS.FAIL;
            }
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P4_7a : TestItemForm
    {
        public string step = "_021 P4.7a";
        public TestItemStep021_P4_7a()
        {
            sStep = step;
            LoadSpec();
        }
        public override WORK_STATUS Init()
        { return WORK_STATUS.PASS; }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            // R1 =1100k , R2 = 1000k
            double val = StaticValues.s21_NIdata[7] * (2100 / 1000);
            sMeasured = val.ToString();
            //check limitation
            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                re = WORK_STATUS.PASS;
            }
            else
            {
                re = WORK_STATUS.FAIL;
            }
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P4_7b : TestItemForm
    {
        public string step = "_021 P4.7b";
        public TestItemStep021_P4_7b()
        {
            sStep = step;
            LoadSpec();
        }
        public override WORK_STATUS Init()
        { return WORK_STATUS.PASS; }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            // R1 =1100k , R2 = 1000k
            double val = StaticValues.s21_NIdata[8] * (2100 / 1000);
            sMeasured = val.ToString();
            //check limitation
            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                re = WORK_STATUS.PASS;
            }
            else
            {
                re = WORK_STATUS.FAIL;
            }
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P4_7c : TestItemForm
    {
        public string step = "_021 P4.7c";
        public TestItemStep021_P4_7c()
        {
            sStep = step;
            LoadSpec();
        }
        public override WORK_STATUS Init()
        { return WORK_STATUS.PASS; }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            // R1 =1100k , R2 = 1000k
            double val = StaticValues.s21_NIdata[9] * (2100 / 1000);
            sMeasured = val.ToString();
            //check limitation
            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                re = WORK_STATUS.PASS;
            }
            else
            {
                re = WORK_STATUS.FAIL;
            }
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P4_8 : TestItemForm
    {
        public string step = "_021 P4.8";
        public TestItemStep021_P4_8()
        {
            sStep = step;
            LoadSpec();
        }

        public override WORK_STATUS Init()
        {

            return WORK_STATUS.PASS;
        }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            sMeasured = StaticValues.s21_NIdata[11].ToString();
            //check limitation
            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                re = WORK_STATUS.PASS;
            }
            else
            {
                re = WORK_STATUS.FAIL;
            }
            return re;
        }
        public override void release()
        {

        }
    }
    class TestItemStep021_P4_8a : TestItemForm
    {
        public string step = "_021 P4.8a";
        public TestItemStep021_P4_8a()
        {
            sStep = step;
            LoadSpec();
        }
        public override WORK_STATUS Init()
        { return WORK_STATUS.PASS; }

        public override WORK_STATUS Proccess()
        {
            WORK_STATUS re = WORK_STATUS.WORKING;
            // s21_NIdata [19] = {I2,I3,...I20}
            // R1 =1100k , R2 = 1000k
            double val = StaticValues.s21_NIdata[10] * (2000 / 1000);
            sMeasured = val.ToString();
            //check limitation
            if (PassFailJudge())
            {
                if (!Golden.verified && !Golden.empertyData && bNeedOffset)
                {
                    if (!offsetCalculate())
                        return WORK_STATUS.FAIL;
                }

                re = WORK_STATUS.PASS;
            }
            else
            {
                re = WORK_STATUS.FAIL;
            }
            return re;
        }
        public override void release()
        {
            StaticValues.s21_NIdata = null;
        }
    }
}
