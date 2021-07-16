﻿using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Ardalis.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Truncates string so that it is no longer than the specified number of characters. 
        /// 
        /// Source: https://stackoverflow.com/a/17249604/13729
        /// </summary>
        /// <param name="input">String to truncate.</param>
        /// <param name="length">Maximum string length.</param>
        /// <returns>Original string or a truncated one if the original was too long.</returns>
        public static string Truncate(this string input, int length)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), "Length must be >= 0");
            }

            int maxLength = Math.Min(input.Length, length);
            return input.Substring(0, maxLength);
        }

        /// <summary>
        /// Converts string to int.
        /// </summary>
        /// <param name="input">String to int.</param>
        /// <returns>int.</returns>
        public static int ToInt(this string input)
        {
            int.TryParse(input, out var result);

            return result;
        }

        /// <summary>
        /// Converts string to nullable int.
        /// If cannot convert to int then return null.
        /// </summary>
        /// <param name="input">String to nullable int.</param>
        /// <returns>nullable int.</returns>
        public static int? ToMaybeInt(this string input)
        {
            if (!int.TryParse(input, out var result))
            {
                return null;
            }

            return result;
        }


        /// <summary>
        /// Encodes string to base64.
        /// 
        /// Source: https://stackoverflow.com/questions/11743160/how-do-i-encode-and-decode-a-base64-string
        /// </summary>
        /// <param name="plainText">String to encode.</param>
        /// <param name="encoding">System.Text.Encoding to use; default to Encoding.Default.</param>
        /// <returns>The base64 encoded version of the string</returns>        
        public static string Base64Encode(this string plainText,
            Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                return string.Empty;
            }

            encoding = encoding ?? Encoding.Default;
            var plainTextBytes = encoding.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Decodes string from base64 to its normal representation.
        /// 
        /// Source: https://stackoverflow.com/questions/11743160/how-do-i-encode-and-decode-a-base64-string
        /// Source: https://www.aspforums.net/Threads/567945/Check-if-Base64-string-is-Valid-or-not-using-C-and-VBNet-in-ASPNet/
        /// </summary>
        /// <param name="encodedString">String to encode.</param>
        /// <param name="encoding">System.Text.Encoding to use; default to Encoding.Default.</param>
        /// <returns>The decoded version of the string</returns>        
        public static string DecodeBase64String(this string encodedString,
            Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(encodedString))
            {
                return string.Empty;
            }

            // Check for valid base64
            encodedString = encodedString.Trim();
            var isValidBase64 = (encodedString.Length % 4 == 0) &&
                                Regex.IsMatch(encodedString, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

            if (!isValidBase64)
            {
                throw new ArgumentException($"{encodedString} is not valid base64");
            }

            var data = Convert.FromBase64String(encodedString);
            encoding = encoding ?? Encoding.Default;
            return encoding.GetString(data);
        }
    }
}