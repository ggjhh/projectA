using System;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace MVC4cjlee.Utility
{
    public class Decrypt
    {
        private static string strKey = "";
        private static string strIV  = "";

        public static string DecryptCookie(string value)
        {
            string retVal = "";

            byte[] message = Text.Hex2bin(value);

            byte[] Key = Encoding.UTF8.GetBytes(strKey);
            byte[] IV = Encoding.UTF8.GetBytes(strIV);

            RijndaelManaged rijn = new RijndaelManaged();
            rijn.Mode = CipherMode.CBC;
            rijn.Padding = PaddingMode.Zeros;
            using (MemoryStream msDecrypt = new MemoryStream(message))
            {
                using (ICryptoTransform decryptor = rijn.CreateDecryptor(Key, IV))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader swDecrypt = new StreamReader(csDecrypt))
                        {
                            retVal = swDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            rijn.Clear();
            return retVal;
        }
    }
}