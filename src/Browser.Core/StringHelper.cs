using System;
using System.Collections.Generic;
using System.Text;

namespace Browser.Core
{
    public static class StringHelper
    {
        public static string ToArgFriendlyString(this string str)
        {
            return str.Replace(@"\", @"\\");
        }

        public static byte[] ToBytes(this string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string ToString(this byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}
