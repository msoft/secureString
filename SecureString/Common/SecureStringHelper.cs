using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SecureStringExample.Common
{
    public static class SecureStringHelper
    {
        public static SecureString ConvertToSecureString(char[] clearString)
        {
            var secureString = new SecureString();
            foreach (char caracter in clearString)
            {
                secureString.AppendChar(caracter);
            }

            return secureString;
        }

        public static SecureString ConvertToSecureString(string clearString)
        {
            return ConvertToSecureString(clearString.ToArray());
        }

        public static TReturn UseSecureStringContent<TReturn>(SecureString secureString,
            Func<char[], TReturn> stringUse)
        {
            int stringLength = secureString.Length;
            char[] bytes = new char[stringLength];
            IntPtr ptr = IntPtr.Zero;

            try
            {
                ptr = Marshal.SecureStringToBSTR(secureString);
                bytes = new char[stringLength];
                Marshal.Copy(ptr, bytes, 0, stringLength);
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Marshal.ZeroFreeBSTR(ptr);
            }

            // Utiliser le chaine de caractères sous forme de tableau de caractères 
            TReturn returnValue = stringUse(bytes);

            for (int i = 0; i < stringLength; i++)
                bytes[i] = '*';

            return returnValue;
        }

        public static TReturn UseSecureString<TReturn>(SecureString secureString,
            Func<string, TReturn> useString)
        {
            string simpleString = ConvertToString(secureString);
            var returnValue = useString(simpleString);
            EraseStringContent(simpleString);
            return returnValue;
        }

        public static char[] ConvertToCharArray(SecureString secureString)
        {
            int stringLength = secureString.Length;
            var charArray = new char[stringLength];

            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                for (int i = 0; i < stringLength; i++)
                {
                    charArray[i] = (char)Marshal.ReadInt16(valuePtr, i * 2);                    
                }
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }

            return charArray;
        }

        public static string ConvertToString(SecureString secureString)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        public static void EraseStringContent(string stringToErase)
        {
            unsafe
            {
                fixed (char* stringContent = stringToErase)
                {
                    for (int i = 0; i < stringToErase.Length; i++)
                        stringContent[i] = '*';
                }
            }
        }
    }
}
