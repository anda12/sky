
using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Sky.Common
{
    /// <summary>
    /// 加密
    /// </summary>
    public class xEncrypt
    {
        public xEncrypt()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        private const string ENCRYPT_KEY = "zhongguo";

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string EncryptText(string strText)
        {
            return Encrypt(strText, ENCRYPT_KEY);
        }
        /// <summary>
        ///  加密
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string EncryptText(string strText, string strKey)
        {
            return Encrypt(strText, strKey);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string DecryptText(string strText)
        {
            return Decrypt(strText, ENCRYPT_KEY);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="strText">要解密的字串</param>
        /// <param name="strKey">解密密匙</param>
        /// <returns>解密后的字符串</returns>
        public static string DecryptText(string strText, string strKey)
        {
            return Decrypt(strText, strKey);
        }

        /// <summary>
        /// 加密函数
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="strEncrKey"></param>
        /// <returns></returns>
        private static string Encrypt(string strText, string strEncrKey)
        {
            byte[] byKey = { };
            byte[] IV = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary>
        /// 解密函数
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="sDecrKey"></param>
        /// <returns></returns>
        private static string Decrypt(string strText, string sDecrKey)
        {
            byte[] byKey = { };
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            byte[] inputByteArray = new byte[strText.Length];

            byKey = System.Text.Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }

    }
}
