using System;
using System.Diagnostics;
using WindowsFormsAppTest1;

public class StaticValues
{
    public static HardwareTestRPCSerialDevice.Device hwTestRPCDevice;
    public static RPCSerialDevice.Device rpcSerialDevice ;
    public static HardwareTestRPCSerialDevice.Device nordicTestRPCDevice ;
    public static string sSerialPort;
    public static string sBLESerialPort;
    public static long dStartTime;
    public static long dStepStartTime;
    public static string sBarCode;
    public static string sFixtureId = null;
    public static long S22_StartTime;
    public static string MODE;

    public static bool goldenTest;

    // step compare
    public static double step135_P1;
    public static double step135_P2;
    public static double step135_P4;
    public static double step135_P5;
    public static double step080_P3;
    public static double step080_P4;
    public static double step085_P1;
    public static double step085_P2;
    public static double step085_P4;
    public static double step085_P5;
    public static double step82_P2;
    public static double step115_P1;
    public static double step115_P2;
    public static double step115_P4;
    public static double step115_P5;


    public static double step15_P1;
    public static double step15_P2;
    public static double step15_P3;
    public static double step15_P4;
    public static double step15_P5;

    //mcu gpio
    public static byte uC_MCU_DBG_TX = 0;
    public static byte uC_MCU_DBG_RX = 1;
    public static byte uC_SCA_I_DAC = 2;
    public static byte uC_Touch1 = 3;
    public static byte uC_CONV_ON = 4;
    public static byte uC_SpareP14 = 5;
    public static byte uC_SCATemp = 6;
    public static byte uC_STS_Sensor_Filtered = 7;
    public static byte uC_BLE_RX  = 8;  
    public static byte uC_BLE_TX  = 9;  
    public static byte uC_BLE_GPIO0 = 10; 
    public static byte uC_BLE_GPIO1  = 11; 
    //public static byte uC_AirOutTemp = 11;
    public static byte uC_UISet1 = 12;
    public static byte uC_UISet2 = 13;
    public static byte uC_UISet3 = 14;
    public static byte uC_Awake = 15;
    public static byte uC_UIButtonFlash = 16;
    public static byte uC_UIButtonON = 17;
    public static byte uC_UI_LED_BLE_B = 18;

    public static byte uC_SpareP38 = 19;
    public static byte uC_UILedRTF = 20;
    public static byte uC_UILedError = 21;
    public static byte uC_I2C_sda_Attach = 22;
    public static byte uC_I2C_scl_Attach = 23;
    public static byte uC_Trigger_Lamp = 24;
    public static byte uC_Enable_STS_Red = 25;
    public static byte uC_NC_27 = 27;


    public static byte uC_LampVoltage = 32;
    public static byte uC_CPDischargerCurrent = 33;
    public static byte uC_FlashCapVoltage = 34;
    public static byte uC_Enable_FC_Charge = 35;
    public static byte uC_Enable_FC_Discharge = 36;
    public static byte uC_UI_LED_BLE_R = 37;
    public static byte uC_SupplyVoltage14V = 38;
    public static byte uC_Touch2 = 39;
    public static byte uC_AirOutTemp = 40;
    public static byte uC_Lampframe_TC = 41;
    public static byte uC_UI_LED_BLE_G = 42;
    public static byte uC_BLE_Force_MCU_BL = 43;
    public static byte uC_MCU_Reset_BLE = 44;
    public static byte uC_BLE_Awake = 45;
    public static byte uC_12V_Ena = 46;
    public static byte uC_UIstandby = 47;
    public static byte uC_Attachment_EN = 48;
    public static byte uC_Enable_STS_IR = 49;
    public static byte uC_AttachmentConnected = 54;
    public static byte uC_UISet4 = 55;
    public static byte uC_UISet5 = 62;
    public static byte uC_Cooling = 63;

    //public static byte uC_I2CLEDdriverAddress = 0x60;
    public static byte uC_I2CEepromAddress = 0x50;

    // eeprom content file
    public static string eeprom_file0 = "01-0B-0C-0D-BC-03-01-00-40-E2-01-00-19-00-19-00-00-00-A5-01-88-FA-00-00-00-00-00-00-00-00-00-00";
    public static string eeprom_file1 = "00-00-70-00-64-00-CF-65-00-02-00-00-19-00-00-00-00-00-00-00-00-00-D4-04-58-04-E8-03-E8-03-00-00";
    public static string eeprom_file2 = "0F-0F-00-00-00-00-00-00-46-00-B6-03-B9-03-BA-03-BC-03-BF-03-00-00-00-00-00-00-00-00-00-00-00-00";
    public static string eeprom_file3 = "00-00-00-00-00-00-00-00-00-00-E8-03-E8-03-E8-03-E8-03-E8-03-84-03-B0-04-40-06-08-07-D0-07-00-00";
    public static string eeprom_file4 = "64-64-40-06-34-08-F0-0A-80-0C-AC-0D-0A-0A-0A-0A-0A-64-00-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF";
    public static string eeprom_file5 = "FF-FF-FF-FF-FF-FF-41-C6-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF";
    public static string eeprom_file6 = "FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF";
    public static string eeprom_file7 = "FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF";
    public static string[] EEprom_File = { eeprom_file0, eeprom_file1 , eeprom_file2 , eeprom_file3 , eeprom_file4 , eeprom_file5,
    eeprom_file6,eeprom_file7};

    public static string P2_eeprom_file0 = "00-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF";
    public static string P2_eeprom_file1 = "20-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF";
    public static string P2_eeprom_file2 = "40-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF";
    public static string P2_eeprom_file3 = "60-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF";
    public static string P2_eeprom_file4 = "80-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF";
    public static string P2_eeprom_file5 = "A0-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF";
    public static string P2_eeprom_file6 = "C0-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF";
    public static string P2_eeprom_file7 = "E0-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF";
    public static string[] P2_EEprom_File = { P2_eeprom_file0, P2_eeprom_file1 , P2_eeprom_file2 , P2_eeprom_file3 , P2_eeprom_file4 , P2_eeprom_file5,
    P2_eeprom_file6,P2_eeprom_file7};
    // log data

    public static double[] s100_VoltFlashCap;
    public static double[] s100_VoltLamp;
    public static Array s100_NI_VoltFlashCap;
    public static Array s100_NI_P_VoltLamp;
    public static Array s100_NI_N_VoltLamp;
    public static Array s100_NI_TC;
    public static double[] s110_VoltFlashCap;
    public static double[] s110_CPDischargCURR;
    public static Array s316_NI_Bat_VOLT;
    public static Array s316_NI_Bat_GND;
    public static string[] s316_Loader_data;
    public static Array s130_NIdata;
    public static double[] s21_NIdata;
    public static Array s22_LED_NIdata;
    public static int s22_samplerate;
    public StaticValues()
    {
    }

    public static void setTestTimer()
    {
        dStartTime = DateTime.Now.Ticks;
    }
    public static void setS22TestTimer()
    {
        S22_StartTime = DateTime.Now.Ticks;
    }
    public static long getTestTime()
    {
        long delta = DateTime.Now.Ticks - dStartTime;

        return delta;

    }
}
public class Step20_OutPut
{
    public static bool[] Btn_On = new bool[4]; //{init status,pressed status,release status,test result}
    public static bool[] Btn_Flash = new bool[4];
    public static string UI_button_on_V;
    public static string UI_button_off_V;
    public static string Flash_button_on_V;
    public static string Flash_button_off_V;
    //public static bool[] Btn_A = new bool[4];
    //public static bool[] Btn_Confirm = new bool[4];
    //public static bool[] Btn_Up = new bool[4];
    //public static bool[] Btn_Down = new bool[4];
}

public class Step100_OutPut
{

    public static string s100_P1_re;
    public static string s100_P2_re;
    public static string s100_P3_re;
    public static string s100_P4_re;
    public static string s100_P5_re;
    public static string s100_P6_re;
    public static string s100_P7_re;
    public static string s100_P8_re;
    public static string s100_P9_re;
    public static string s100_P10_re;
    public static string s100_P11_re;
    public static string s100_P12_re;
    public static string s100_P13_re;
    public static bool Savelog_Enable;
}
public class Step110_OutPut
{
    public static string s110_P1_re;
    public static string s110_P2_re;
    public static string s110_P3_re;
    public static string s110_P4_re;
    public static string s110_P5_re;
    public static string s110_P6_re;
    public static string s110_P8_re;
    public static bool Savelog_Enable;
}
public class ADC_OutPut
{
    public static double s15_P7;
    public static ushort s117_P1;
    public static ushort s117_P5;
    public static ushort s117_P9;
    public static int s137_P1;
    public static int s137_P2;
}
public class NI_OutPut
{

    public static double s15_P5;
    public static double s15_P6;
    public static double s15_P8;
    public static double s120_P4;
    public static double s117_P2;
    public static double s117_P3;
    public static double s117_P4;
    public static double s117_P6;
    public static double s117_P7;
    public static double s117_P8;
    public static double s117_P10;
    public static double s117_P12;
    public static double s137_P2;
}
public class Config
{
    // Path
    public static string pathLog;
    public static string fileProdConfig;
    public static string fileSpec;
    public static string fileCounter;
    public static string fileGolden;
    public static string fileCalibration;

    // SFIS
    public static string sfisIp;
    public static int sfisPort;
    public static string sfisLine;
    public static bool sfisSendFail;
    public static bool sfisSendStep1;
    public static bool sfisEnable;

    // Debug tool
    public static bool debugEnable;

    // Tester deviation
    public static bool testerCalibration;
    public static bool measuredOffset;
}

public class ProdConfig
{
    public static string PartNumber;

    public static string snPrefix;
    public static int snLength;

    public static string fileProgrammer;
    public static string fileScript;
    public static string fileTestCode;
    public static string fileProductCode;
    public static string fileBleTestCode;
    public static string fileBleProductCode;
    public static string fileSpiCode;
    public static string sfisnameScript;
    public static string sfisnameTestCode;
    public static string sfisnameProductCode;
    public static string sfisnameBleTestCode;
    public static string sfisnameBleProductCode;
    public static string sfisnameSpiCode;

    public static bool autoRun;
    public static bool savePassRawData;
    public static bool saveFailRawData;
    public static bool failContinueTest;

    public static string mcu_jlinksn;
    public static string ble_jlinksn;
    public static string flash_jlinksn;
}

public class Counter
{
    public static int maxNumber;
    public static string resetPassword;
    public static int currentCount;
    public static bool passwordMatch = false;
}

public class DCLoad_8601
{
    public static string usbId;
    public static double Current = 16;
}

public class Power_DR2002MO
{
    public static string comPort;
    public static int baudRate;
    public static double V195 = 19.5;    //Set Voltage to 19.5V
    public static double CURR1 = 1;       //Set Current to 1A
    public static double FLASHCAP_CURR5 = 6;       //Set Current to 5A
    public static double OCP = 7;
    public static double OVP = 22;
    public static double FLASHCAP_OCP = 7;
    
}

public class Power_IT6512C
{
    public static string usbId;
    public static double V72 = 7.2;    //Set Voltage to 7.2V
    public static double V84 = 8.45;
    public static double V45 = 4.5;    //Set Voltage to 4.5V
    public static double V65 = 6.5;    //Set Voltage to 6.5V
    public static double V8 = 8;    //Set Voltage to 8V
    public static double CURR1 = 1;       //Set Current to 1A
}
public class Power_LPS505N
{
    public static string comPort;
    public static int baudRate;
    public static double V5 = 5;    //Set Voltage to 5V
    public static double V24 = 24;    //Set Voltage to 24V
    public static double V10 = 10;    //Set Voltage to 10V
    public static double V15 = 15;    //Set Voltage to 15V
    public static double V36 = 3.6;    //Set Voltage to 3.6V
    public static double CURR1 = 1;       //Set Current to 1A
    public static double CURR2 = 2;       //Set Current to 2A
    public static double CURR3 = 3;       //Set Current to 3A
    public static double CURRd5 = 0.5;       //Set Current to 0.5A
}
public class DAQ_USB6218_A
{
    public static string deviceName;

    //public const int TP104_AI0 = 2;
    //public const int TP101_AI1 = 1;
    //public const int TP103_AI2 = 0;
    public const int TP104_AI0 = 0;
    public const int TP101_AI1 = 1;
    public const int TP103_AI2 = 2;
    public const int TP316_AI3 = 3; // no use
    public const int TP603_AI4 = 4; // step1 120  RED LED
    public const int TP604_AI5 = 5;// step1 130  IR LED
    public const int TP328_AI6 = 6; // 3V3 
    public const int TP317_AI7 = 7; // vmain 19V
    public const int TP307_AI8 = 8; // 015 P5 V support 19.5V
    public const int TP425_AI9 = 9; // LampFrame_TC 085 P5
    public const int TP612_AI10 = 10; // STS_Sensor_Filtered
    public const int TP321_AI11 = 11;// no use
    public const int TP605_AI12 = 12; // diff ch (step 120/130)
    public const int TP504_AI13 = 13;// no use
    public const int TP627_AI14 = 14; // attachment_coonect
    public const int TP102_AI15 = 15; // no use
    public const int TP100_AI16 = 16;// no use
    public const int TP315_AI17 = 17; // van- 082P2
    public const int TP215_AI18 = 18; // air_out_temp  085 P2
    public const int TP625_AI19 = 19; // atchamnet_Conn2
    public const int TP623_AI20 = 20; //STS_TEMP 136P2
    public const int TP611_AI21 = 21;//STS_Sensor 135P2
    public const int TP739_AI22 = 22; // Flash button volt
    public const int TP300_AI23 = 23; // 19V
    public const int TP313_AI24 = 24; //12V
    public const int TP314_AI25 = 25;// van+ 082P1
    public const int TP624_AI26 = 26; //atchamnet_Conn3
    public const int TP626_AI27 = 27;//atchamnet_Conn1
    public const int TP108_AI28 = 28;// no use
    public const int TP736_AI29 = 29;
    //public const int TP219_AI29 = 29; //2020-11-12 TP505_AI30
    public const int TP615_AI30 = 30; // 3V3_switch 015P4
    public const int AI31 = 31;
}
public class DAQ_USB6218_B
{
    public static string deviceName;

    public const int NI_B_LEDSet1_I2_AI_1 = 1;
    //public const int NI_B_AI1_dummy = 1;
    public const int NI_B_LEDSet2_I3_AI_2 = 2;
    public const int NI_B_LEDSet3_I4_AI_3 = 3;
    public const int NI_B_LEDSet4_I5_AI_4 = 4;
    public const int NI_B_LEDSet5_I6_AI_5 = 5;
    public const int NI_B_LEDSTD_STR1_I7_AI_6 = 6;
    public const int NI_B_LEDSTD_STR2_I8_AI_7 = 7;
    public const int NI_B_LEDRTF_STR1_I9_AI_8 = 8;
    public const int NI_B_LEDRTF_STR2_I10_AI_9 = 9;
    public const int NI_B_LEDRTF_STR3_I11_AI_10 = 10;
    public const int NI_B_LEDSTB_I12_AI_11 = 11;
    public const int NI_B_UI_Standby_I13_AI_12 = 12;
    public const int NI_B_UISet1_I14_AI_13 = 13;
    public const int NI_B_UISet2_I15_AI_14 = 14;
    public const int NI_B_UISet3_I16_AI_15 = 15;
    public const int NI_B_UISet4_I17_AI_16 = 16;
    public const int NI_B_UISet5_I18_AI_17 = 17;
    public const int NI_B_UILedError_I19_AI_18 = 18;
    public const int NI_B_UILedRTF_I20_AI_19 = 19;
    //debug
    public const int AI24 = 24;
}
public class IO_USB6525_A
{
    public static string deviceName;

    public static bool[] OutDefault = { false, false, false, false, false, false, false, false };
    public static bool[] StartDefault = { false, false, false, true, false, false, false, false };
    //public static bool[] StartDefault = { false, false, false, false, false, false, false, false };
    // Output
    public const int Relay_Att_Conn1_TP512_K1_P00 = 0;
    public const int Relay_Att_Conn2_TP517_K2_P01 = 1;
    public const int Relay_Att_Conn3_TP518_K3_P02 = 2;
    public const int Relay_I2C_K4_K5_P03 = 3;
    public const int Relay_Discharge_Res_K12_K13_P04 = 4;
    public const int Relay_FlashButton_TP515_K6_P05 = 5;
    public const int Relay_Support3V3_TP322_K7_P06 = 6;
    public const int Relay_TCO_K14_P07 = 7;
}

public class IO_USB6525_B
{
    public static string deviceName;

    public static bool[] OutDefault = { false, false, false, false, false, false, false, true };
    public static bool[] StartDefault = { false, false, false, true, false, false, false, true };

    // Output
    public const int Relay_VBAT_TP100_TP109_K9_P00 = 0;
    public const int Relay_VBAT_GND_TP109_TP102_K10_P01 = 1;
    public const int AISENSE_K11_P02 = 2;
    //public const int Relay_SCA_SENSOR_SWITCH_K12_P03 = 3;
    public const int Relay_SCA_K12_K13_P03 = 3;
    public const int CTL_BUTTON_P04 = 4;
    public const int CTL_HV_RELAY_P05 = 5;
    public const int Relay_SCA_Temp_Switch_DAC_DO_0_K14_P06 = 6;
    public const int CTL_JIG_BREAK_P06 = 7;


    public const int CHK_JIG_P10 = 0; //INPUT P1.0
    public const int CHK_JIG_P12 = 1; //INPUT P1.1
    public const int CHK_JIG_P13 = 2; //INPUT P1.2
    public const int CHK_JIG_P14 = 3; //INPUT P1.3
    public const int CHK_EMG_P14 = 4; //INPUT P1.4
}
public class IO_USB6525_C
{
    public static string deviceName;

    public static bool[] OutDefault = { false, false, false, false, false, false, false, false };

    // Output
    public const int Relay_SCA_Temp_Switch_K13_P02 = 0;
    public const int Relay_SCA_Temp_Switch_DAC_DO_0_K14_P01 = 1;
    public const int Relay_Air_Out_Temp_Switch_DAC_DO_1_K15_P00 = 2;
    public const int Relay_SCA_SENSOR_SWITCH_K12_P03 = 3;
}
public class Feasa_10F_A
{
    public static int comPort;
    public static int baudRate;

    public static float OLED_X_LLimit = 0.63f;//orange led
    public static float OLED_X_ULimit = 0.68f;
    public static float OLED_Y_LLimit = 0.32f;
    public static float OLED_Y_ULimit = 0.37f;

    public static int OLED_WL_LLimit = 598;
    public static int OLED_WL_ULimit = 615;

    public static float WLED_X_LLimit = 0.28f; //while led
    public static float WLED_X_ULimit = 0.35f;
    public static float WLED_Y_LLimit = 0.28f;
    public static float WLED_Y_ULimit = 0.35f;
    // old-------
    //public static XY_Data ch7_D502w;
    //public static XY_Data ch6_D612o;
    //public static XY_Data ch5_D501w;
    //public static XY_Data ch4_D510w;
    //public static XY_Data ch3_D500w;
    //public static XY_Data ch1_D630o;
    //public static XY_Data ch2_D631o;
    //public static XY_Data ch8_D511w;

    //public static XY_Data ch5_D502w;
    //public static XY_Data ch3_D501w;
    //public static XY_Data ch2_D510w;
    //public static XY_Data ch1_D500w;
    //public static XY_Data ch4_D511w;
    //public static XY_Data ch6_D631o;
    //-------------
    public static int CCT_ch5_D502w;
    public static int CCT_ch3_D501w;
    public static int CCT_ch2_D510w;
    public static int CCT_ch1_D500w;
    public static int CCT_ch4_D511w;
    public static int I_ch5_D502w;
    public static int I_ch3_D501w;
    public static int I_ch2_D510w;
    public static int I_ch1_D500w;
    public static int I_ch4_D511w;
    public static int I_ch6_D631o;
    public static int W_ch6_D631o;
    public static int I_ch7_D701;//RGB
    public static int R_ch7_D701;//RGB
    public static int G_ch7_D701;//RGB
    public static int B_ch7_D701;//RGB
    public static int W_ch7_D701;//RGB
    public static XY_Data XY_ch0;
}

public class Feasa_10F_B
{
    public static int comPort;
    public static int baudRate;

    public static float OLED_X_LLimit = 0.63f;//orange led
    public static float OLED_X_ULimit = 0.68f;
    public static float OLED_Y_LLimit = 0.32f;
    public static float OLED_Y_ULimit = 0.37f;

    public static int OLED_WL_LLimit = 595;
    public static int OLED_WL_ULimit = 625;

    public static float WLED_X_LLimit = 0.28f;//while led
    public static float WLED_X_ULimit = 0.33f;
    public static float WLED_Y_LLimit = 0.28f;
    public static float WLED_Y_ULimit = 0.33f;
    // OLD----------
    //public static XY_Data ch1_D507w;
    //public static XY_Data ch2_D516w;
    //public static XY_Data ch3_D525w;
    //public static XY_Data ch4_D521w;
    //public static XY_Data ch5_D520w;
    //public static XY_Data ch6_D522w;
    //public static XY_Data ch7_D526w;
    //public static XY_Data ch8_D517w;
    //public static XY_Data ch9_D508w;

    //public static XY_Data ch1_D505o;
    //public static XY_Data ch2_D514o;
    //public static XY_Data ch3_D523o;
    //public static XY_Data ch4_D527o;

    //public static XY_Data ch6_D528o;
    public static XY_Data ch7_D524o;
    //public static XY_Data ch8_D515o;
    //public static XY_Data ch9_D506o;
    // ---------------
    public static int CCT_ch1_D507w;
    public static int CCT_ch2_D516w;
    public static int CCT_ch3_D525w;
    public static int CCT_ch4_D521w;
    public static int CCT_ch5_D520w;
    public static int CCT_ch6_D522w;
    public static int CCT_ch7_D526w;
    public static int CCT_ch8_D517w;
    public static int CCT_ch9_D508w;
    public static int I_ch1_D507w;
    public static int I_ch2_D516w;
    public static int I_ch3_D525w;
    public static int I_ch4_D521w;
    public static int I_ch5_D520w;
    public static int I_ch6_D522w;
    public static int I_ch7_D526w;
    public static int I_ch8_D517w;
    public static int I_ch9_D508w;

    public static int I_ch1_D505o;
    public static int I_ch2_D514o;
    public static int I_ch3_D523o;
    public static int I_ch4_D527o;
    public static int W_ch1_D505o;
    public static int W_ch2_D514o;
    public static int W_ch3_D523o;
    public static int W_ch4_D527o;

    public static int W_ch6_D528o;
    public static int W_ch7_D524o;
    public static int W_ch8_D515o;
    public static int W_ch9_D506o;
    public static int I_ch6_D528o;
    public static int I_ch7_D524o;
    public static int I_ch8_D515o;
    public static int I_ch9_D506o;
}
public class Power_IT6332A
{
    public static string usbId;

    public static double V195 = 19.5;    //Set Voltage to 19.5V
    public static double CURR1 = 1;       //Set Current to 1A
    public static double CURR2 = 2;       //Set Current to 5A


}
public class DMM_IT6332A
{
    public static string usbId;


}
public class ControlledDLLs
{
    public static Code DLL1;
    public static Code DLL2;
    public static Code DLL3;
    public static Code DLL4;
    public static Code DLL5;
    public static Code DLL6;
}

public class Programming
{
    public static Code Script;
    public static Code testFW;
    public static Code productFW;
    public static Code externalFlash;
    public static Code bleDtmFW;
    public static Code bleProductFW;
}

public class Dut
{
    public static string comPort;
    public static int baudRate;
}
public class bleDut
{
    public static string comPort;
    public static int baudRate;
}
public class Golden
{
    public static bool verified = false;
    public static bool empertyData = false;
}

public class Code
{
    public string name;
    public string crc32;
}
public class cmdShell
{
    private Process shellProcess;

    public delegate void onDataHandler(cmdShell sender, string e);
    public event onDataHandler onData;

    public cmdShell()
    {
        try
        {
            shellProcess = new Process();
            ProcessStartInfo si = new ProcessStartInfo("cmd.exe");
            si.Arguments = "/k";
            si.RedirectStandardInput = true;
            si.RedirectStandardOutput = true;
            si.RedirectStandardError = true;
            si.UseShellExecute = false;
            si.CreateNoWindow = true;
            //si.WorkingDirectory = Environment.GetEnvironmentVariable("windir");
            si.WorkingDirectory = @"D:";
            shellProcess.StartInfo = si;
            shellProcess.OutputDataReceived += shellProcess_OutputDataReceived;
            shellProcess.ErrorDataReceived += shellProcess_ErrorDataReceived;
            shellProcess.Start();
            shellProcess.BeginErrorReadLine();
            shellProcess.BeginOutputReadLine();




        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex.Message);
        }
    }

    void shellProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
    {
        doOnData(e.Data);
    }

    void shellProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
    {
        doOnData(e.Data);

    }

    private void doOnData(string data)
    {
        //if (onData != null) onData(this, data);
        onData(this, data);
    }

    public void write(string data)
    {
        try
        {
            shellProcess.StandardInput.WriteLine(data);
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex.Message);
        }

    }

}
