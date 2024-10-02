using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBookShopProject.Assets.Common
{
    public class Common_Functions
    {
        public static string Encrypt(string plainText)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainBytes);
        }

        public static string Decrypt(string encryptedText)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            return Encoding.UTF8.GetString(encryptedBytes);
        }
    }
}