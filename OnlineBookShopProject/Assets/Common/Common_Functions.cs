using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

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

        // Helper method to check if a string is a valid Base64 string
        public static bool IsBase64String(string base64)
        {
            // Check for invalid characters or padding issues
            return !string.IsNullOrEmpty(base64) && (base64.Length % 4 == 0) && Regex.IsMatch(base64, @"^[a-zA-Z0-9+/]*={0,2}$");
        }
    }
}