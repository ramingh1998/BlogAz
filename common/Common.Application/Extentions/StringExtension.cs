using Newtonsoft.Json;
using System;
using System.Text;
using System.Globalization;
using System.Security.Cryptography;
using System.IO;
using Common.Application.Functions;
using Common.Application.Objects;

namespace Common.Application.Extentions
{
    public static class stringExtension
    {
        //=====================================================================================
        public static string HashSet(this string Value)
        {
            byte[] Buffer = Encoding.UTF8.GetBytes(Value);
            MD5 md = MD5.Create();
            return Convert.ToBase64String(md.ComputeHash(Buffer));
        }
        //=====================================================================================
        public static DateTime ToDate(this string date)
        {
            var dt = DateTime.UtcNow;
            var splittedDate = date.Split("/");
            int Year = int.Parse(splittedDate[0]);
            int Month = int.Parse(splittedDate[1]);
            int Day = int.Parse(splittedDate[2]);
            switch (AppConfigProvider.GetCalendarType())
            {
                case 1:
                    var persianDate = new PersianCalendar();
                    dt = persianDate.ToDateTime(Year, Month, Day, 12, 0, 0, 0);
                    break;
                case 2:
                    dt = new DateTime(Year, Month, Day, 12, 0, 0);
                    break;
            }
            return dt;
        }
        //=====================================================================================
        public static TimeSpan ToTimeSpan(this string data)
        {
            string[] time = data.Split(":");
            int hour = Convert.ToInt32(time[0]);
            int minutes = Convert.ToInt32(time[1]);
            var timeSpan = new TimeSpan(hour, minutes, 0);
            return timeSpan;
        }
        //=====================================================================================
        public static string ToTimeString(this TimeSpan data)
        {
            return string.Format("{0:D2}:{1:D2}", data.Hours, data.Minutes);
        }
        //=====================================================================================
        public static int ToIntegerIdentifier(this string value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch (Exception)
            {
                throw new CustomException(SystemCommonMessage.IdentifierIsNotValid);
            }
        }
        //=====================================================================================
        public static string EncriptWithDESAlgoritm(this string data)
        {
            byte[] buffer_key = Encoding.UTF8.GetBytes("EZSRTYyu");
            byte[] buffer_IV = Encoding.UTF8.GetBytes("DNJPOfcq");
            byte[] buffer_Message = Encoding.UTF8.GetBytes(data);
            MemoryStream memoryStream = new MemoryStream();
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, DES.CreateEncryptor(buffer_key, buffer_IV), CryptoStreamMode.Write);
            cryptoStream.Write(buffer_Message, 0, buffer_Message.Length);
            cryptoStream.FlushFinalBlock();
            cryptoStream.Dispose();
            return Convert.ToBase64String(memoryStream.ToArray()).Replace("/", "-").Replace("+", "_");
        }
        //=====================================================================================
        public static string DecriptWithDESAlgoritm(this string data)
        {
            try
            {
                byte[] buffer_Message = Convert.FromBase64String(data.Replace("-", "/").Replace("_", "+"));
                byte[] buffer_key = Encoding.UTF8.GetBytes("EZSRTYyu");
                byte[] buffer_IV = Encoding.UTF8.GetBytes("DNJPOfcq");
                var memoryStream = new MemoryStream();
                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                var cryptoStream = new CryptoStream(memoryStream, DES.CreateDecryptor(buffer_key, buffer_IV), CryptoStreamMode.Write);
                cryptoStream.Write(buffer_Message, 0, buffer_Message.Length);
                cryptoStream.FlushFinalBlock();
                cryptoStream.Dispose();
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            catch (Exception)
            {
                throw new CustomException("شناسه رمز شده صحیح نمی باشد");
            }

        }
        //=====================================================================================
        public static dynamic ToFilterExpression(this string data)
        {
            try
            {
                dynamic result = JsonConvert.DeserializeObject(data);
                return result;
            }
            catch { return null; }

        }
        //=====================================================================================
        public static string CharacterAnalysis(this string data)
        {
            if (string.IsNullOrEmpty(data))
                return data;
            var str = data.Trim();
            str = str.Replace("۰", "0");
            str = str.Replace("۱", "1");
            str = str.Replace("۲", "2");
            str = str.Replace("۳", "3");
            str = str.Replace("۴", "4");
            str = str.Replace("۵", "5");
            str = str.Replace("۶", "6");
            str = str.Replace("۷", "7");
            str = str.Replace("۸", "8");
            str = str.Replace("۹", "9");
            str = str.Replace("ك", "ک");
            str = str.Replace("ي", "ی");
            str = str.Replace("ى", "ی");
            return str;
        }
        //=====================================================================================
        public static string Truncate(this string str, int maxLength, string suffix = "...")
        {
            if (str == null || str.Length <= maxLength)
                return str;
            int strLength = maxLength - suffix.Length;
            return str.Substring(0, strLength) + suffix;
        }
        //=====================================================================================
        public static string GetKavemoId(this string str)
        {
            return str.Split("/")[5];
        }
        //=====================================================================================

    }
}
