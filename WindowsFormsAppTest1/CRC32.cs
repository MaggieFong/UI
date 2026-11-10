using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
 
namespace myCRC32
{
	class CRC32
	{
		protected ulong[] Crc32Table;
		//生成CRC32码表
		private void GetCRC32Table() 
		{
			ulong Crc;
			Crc32Table = new ulong[256];
			int i,j;
			for(i = 0;i < 256; i++) 
			{
				Crc = (ulong)i;
				for (j = 8; j > 0; j--)
				{
					if ((Crc & 1) == 1)
						Crc = (Crc >> 1) ^ 0xEDB88320;
					else
						Crc >>= 1;
				}
				Crc32Table[i] = Crc;
			}
		}


		//获取字符串的CRC32校验值
		public string GetCRC32(byte[] data)
		{
			//生成码表
			GetCRC32Table();
			byte[] buffer = data;
            string strCRC = "";
            ulong crc = 0xffffffff;
			int len = buffer.Length;

            for (int i = 0; i < len; i++)
			{
                crc = (crc >> 8) ^ Crc32Table[(crc & 0xff)^ buffer[i]];
            }

            crc = crc ^ 0xffffffff;
            strCRC = String.Format("{0:X00000000}", crc);
            return strCRC;
        }
	}
}

