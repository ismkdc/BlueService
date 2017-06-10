using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BlueService
{
    public class Extras
    {
        public static string GetMd5Hash(string metin)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] btr = Encoding.UTF8.GetBytes(metin);
            btr = md5.ComputeHash(btr);
            StringBuilder sb = new StringBuilder();
            foreach (byte ba in btr)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }
            return sb.ToString();
        }
    }
    public class Response
    {
        public string Result { get; set; }
        public Response(string result)
        {
            Result = result;
        }
    }
    public static class StringExtensions
    {
        public static string ToSystemString(this IEnumerable<char> source)
        {
            return new string(source.ToArray());
        }
    }
}