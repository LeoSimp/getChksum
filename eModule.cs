using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace getChkSum
{
    ///   <summary> 
    ///   用于被外部函数引用的类 
    ///   主要包含LRC()方法，请参考注释
    ///   </summary> 
    public class EModule
    {
        static void Main(string[] args)
        {
            LRC(args[0]);
            BCC(args[0]);
            //Console.ReadKey();
        }
        ///   <summary> 
        /// 获取LCR CheckSum    
        ///   </summary> 
        ///   <param name="Data">表示要处理的字符串</param>
        ///   <returns>返回CheckSum值</returns>
        public static string LRC(string Data)
        {
            //求双Byte(字)和
            int sum = 0;
            foreach (byte b in HexStringToByteArray(Data))
            {
                sum += b;
            }
            sum = sum % 255;//255=FF,大于255时直接溢出掉，只算小于255的部分，表示checksum值用后两位就可以表示(FFFFFFXX)
            //取反加1
            sum = ~sum + 1;
            string hexstr = sum.ToString("X2");//等价于 Convert.ToString(sum, 16)，即转化为16进制,大写X有大写的意思，covert默认为小写
            hexstr = hexstr.Substring(hexstr.Length-2,2);
            Console.WriteLine("Calculated LRC = " + hexstr);
            return hexstr;
        }

        /// <summary>
        /// 获取BCC CheckSum
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static string BCC(string Data)
        {
            byte[] DataByte = HexStringToByteArray(Data);
            byte CheckCode = 0;
            int len = DataByte.Length;
            for (int i = 0; i < len; i++)
            {
                CheckCode ^= DataByte[i];
            }
            string hexstr =Convert.ToString(CheckCode, 16).PadLeft(2, '0').ToUpper();          
            Console.WriteLine("Calculated BBC = " + hexstr);
            return hexstr;

        }

        private static byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            //Console.WriteLine(s.Length / 2);
            for (int i = 0; i < s.Length; i += 2)
            {
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            }
            return buffer;
        }
    }
}
