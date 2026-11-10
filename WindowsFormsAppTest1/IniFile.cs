using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using System.Runtime.InteropServices;

namespace IniFileAccess
{
    internal class TIniFile
    {
        public string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);

        public TIniFile(string INIPath)
        {
            path = INIPath;
        }

        private ArrayList ReadKeys(string sectionName) //Read ini section key
        {
            ArrayList keyArray = new ArrayList();
            keyArray.Clear();
            byte[] byteBuffer = new byte[1024];//設定Buffer長度
            string tmpStr = null;
            if (GetPrivateProfileString(sectionName, null, null, byteBuffer, 1024, this.path) > 0)
            {
                foreach (byte abyte in byteBuffer)
                {
                    if (abyte == 0)
                    {
                        if (tmpStr == null)
                        {
                            break;
                        }
                        else
                        {
                            keyArray.Add(tmpStr);
                            tmpStr = null;
                        }
                    }
                    else
                    {
                        tmpStr = tmpStr + Convert.ToChar(abyte);
                    }
                }

                return (ArrayList)keyArray.Clone();
            }
            else
            {
                return null;
            }
        }

        private ArrayList ReadSections() //Read Section
        {
            byte[] byteBuffer = new byte[1024];//設定Buffer長度
            string tmpStr = null;
            ArrayList sectionArray = new ArrayList();
            sectionArray.Clear();
            if (GetPrivateProfileString(null, null, null, byteBuffer, 1024, this.path) > 0)
            {
                foreach (byte abyte in byteBuffer)
                {
                    if (abyte == 0)
                    {
                        if (tmpStr == null)
                        {
                            break;
                        }
                        else
                        {
                            sectionArray.Add(tmpStr);
                            tmpStr = null;
                        }
                    }
                    else
                    {
                        tmpStr = tmpStr + Convert.ToChar(abyte);
                    }
                }
                return (ArrayList)sectionArray.Clone();
            }
            else
            {
                return null;
            }
        }

        public ArrayList iniGetSections()
        {
            ArrayList sectionArray = new ArrayList();
            sectionArray.Clear();
            sectionArray = ReadSections();
            return (ArrayList)sectionArray.Clone();
        }

        public ArrayList iniGetKeys(string sectionName)
        {
            ArrayList keyArray = new ArrayList();
            keyArray.Clear();
            keyArray = ReadKeys(sectionName);
            return (ArrayList)keyArray.Clone();
        }

        public string iniReadValue(string sectionName, string keyName, string defaultVal) //read key value
        {
            StringBuilder retStr = new StringBuilder(1024);
            if (GetPrivateProfileString(sectionName, keyName, defaultVal, retStr, 1024, this.path) != 0)
            {
                string[] retVal = retStr.ToString().Split(';');
                return retVal[0].Trim();
            }
            else
            {
                return defaultVal;
            }
        }

        public string iniReadValue(string sectionName, string keyName, int column, string defaultVal) //read key value
        {
            StringBuilder retStr = new StringBuilder(1024);
            if (GetPrivateProfileString(sectionName, keyName, defaultVal, retStr, 1024, this.path) != 0)
            {
                string[] retVal = retStr.ToString().Split(';');
                Console.WriteLine(retStr.ToString());
                if (column < retVal.Length)
                {
                    return retVal[column].Trim();
                }
                else
                {
                    return defaultVal;
                }
            }
            else
            {
                return defaultVal;
            }
        }
        public bool iniWriteValue(string sectionName, string keyName, string setVal) //write key value
        {
            if (WritePrivateProfileString(sectionName, keyName, setVal, this.path) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
