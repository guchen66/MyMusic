﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Music.ToolKit.Extensions
{
    public class MD5Extension
    {
        public static string GetMD5Provider(string source, string salt, [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {

            string result = "";
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(source + salt));
                result = BitConverter.ToString(bytes).Replace("-", "");
            }
            return result;
        }
    }





}
