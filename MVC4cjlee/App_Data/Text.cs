using System;
using System.Linq;

namespace MVC4cjlee.Utility
{
    public class Text
    {
        /// <summary>
        /// PHP Hex2bin 구현
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] Hex2bin(string value)
        {
            return (from i in Enumerable.Range(0, value.Length / 2) select Convert.ToByte(value.Substring(i * 2, 2), 16)).ToArray();
        }
    }
}