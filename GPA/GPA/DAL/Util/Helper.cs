using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace GPA.DAL.Util
{
    public class Helper
    {
        public string EncryptPassword(String password)
        {
            UnicodeEncoding AE = new UnicodeEncoding();
            //string sha1 = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha1");
            return CalculateSha1(password, AE);

        }

        /// <summary>
        /// Calculates SHA1 hash
        /// </summary>
        /// <param name="text">input string</param>
        /// <param name="enc">Character encoding</param>
        /// <returns>SHA1 hash</returns>
        public string CalculateSha1(string text, Encoding enc)
        {
            byte[] buffer = enc.GetBytes(text);
            SHA1CryptoServiceProvider cryptoTransformSha1 =
            new SHA1CryptoServiceProvider();
            string hash = BitConverter.ToString(
            cryptoTransformSha1.ComputeHash(buffer)).Replace("-", "");
            return hash;
        }

        public string GenerageRandomPassword(int length)
        {
            string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string res = "";
            Random rnd = new Random();
            while (0 < length--)
                res += valid[rnd.Next(valid.Length)];
            return res;

        }

        public string GenerageVerificationCode(int length)
        {
            string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string res = "";
            Random rnd = new Random();
            while (0 < length--)
                res += valid[rnd.Next(valid.Length)];
            return res;

        }
    }
}