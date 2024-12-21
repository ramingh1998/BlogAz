using Common.Application.Functions;
using System;
using System.Globalization;

namespace Common.Application.Extentions
{
    public static class DateTimeExtension
    {
        //===================================================================
        public static string ToDateShortFormatString(this DateTime date)
        {
            string Year = string.Empty, Month = string.Empty, Day = string.Empty;
            switch (AppConfigProvider.GetCalendarType())
            {
                case 1:
                    var persianCalendar = new PersianCalendar();
                    Year = persianCalendar.GetYear(date).ToString();
                    Month = persianCalendar.GetMonth(date).ToString("00");
                    Day = persianCalendar.GetDayOfMonth(date).ToString("00");
                    break;
                case 2:
                    Year = date.Year.ToString();
                    Month = date.Month.ToString("00");
                    Day = date.Day.ToString("00");
                    break;
            }
            return string.Format("{0:D4}/{1:D2}/{2:D2}", Year, Month, Day); ;
        }
        //======================================================================
        public static DateTime ToLocalDateTime(this DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.FindSystemTimeZoneById(AppConfigProvider.GetTimeZone()));
        }
        //===================================================================

        public static DateTime ToUtcDateTime(this DateTime dateTime)
        {
            dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
            return TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZoneInfo.FindSystemTimeZoneById(AppConfigProvider.GetTimeZone()));
        }
        //===================================================================
        public static string ToLocalDateLongFormatString(this DateTime date)
        {
            date = TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.FindSystemTimeZoneById(AppConfigProvider.GetTimeZone()));
            int Year = 0, Month = 0, Day = 0;
            string MonthName = string.Empty, DayName = string.Empty;
            switch (AppConfigProvider.GetCalendarType())
            {
                case 1:
                    var persianCalendar = new PersianCalendar();
                    Year = persianCalendar.GetYear(date);
                    Month = persianCalendar.GetMonth(date);
                    Day = persianCalendar.GetDayOfMonth(date);
                    DayName = date.DayOfWeek.ToPersianDayName();
                    MonthName = Month.ToPersianMonthName();
                    break;
                case 2:
                    Year = date.Year;
                    Month = date.Month;
                    Day = date.Month;
                    DayName = date.DayOfWeek.ToGregorianDayName();
                    MonthName = Month.ToGregorianMonthName();
                    break;
            }
            var result = string.Format("{0} {1} {2} {3}", DayName, Day, MonthName, Year);
            return result;
        }
        //===================================================================
        public static string ToLocalDateTimeLongFormatString(this DateTime dateTime)
        {
            dateTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.FindSystemTimeZoneById(AppConfigProvider.GetTimeZone()));
            int Year = 0, Month = 0, Day = 0;
            string MonthName = string.Empty, DayName = string.Empty;
            switch (AppConfigProvider.GetCalendarType())
            {
                case 1:
                    var persianCalendar = new PersianCalendar();
                    Year = persianCalendar.GetYear(dateTime);
                    Month = persianCalendar.GetMonth(dateTime);
                    Day = persianCalendar.GetDayOfMonth(dateTime);
                    DayName = dateTime.DayOfWeek.ToPersianDayName();
                    MonthName = Month.ToPersianMonthName();
                    break;
                case 2:
                    Year = dateTime.Year;
                    Month = dateTime.Month;
                    Day = dateTime.Month;
                    DayName = dateTime.DayOfWeek.ToGregorianDayName();
                    MonthName = Month.ToGregorianMonthName();
                    break;
            }
            int Hour = dateTime.TimeOfDay.Hours;
            int Minute = dateTime.TimeOfDay.Minutes;
            var result = string.Format("{0} {1} {2} {3} - {4:D2}:{5:D2}", DayName, Day, MonthName, Year, Hour, Minute);
            return result;
        }
        //===================================================================
        public static string ToLocalDateTimeShortFormatString(this DateTime dateTime)
        {
            dateTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.FindSystemTimeZoneById(AppConfigProvider.GetTimeZone()));
            int Year = 0, Month = 0, Day = 0;
            string MonthName = string.Empty, DayName = string.Empty;
            switch (AppConfigProvider.GetCalendarType())
            {
                case 1:
                    var persianCalendar = new PersianCalendar();
                    Year = persianCalendar.GetYear(dateTime);
                    Month = persianCalendar.GetMonth(dateTime);
                    Day = persianCalendar.GetDayOfMonth(dateTime);
                    break;
                case 2:
                    Year = dateTime.Year;
                    Month = dateTime.Month;
                    Day = dateTime.Month;
                    break;
            }
            int Hour = dateTime.TimeOfDay.Hours;
            int Minute = dateTime.TimeOfDay.Minutes;
            var result = string.Format("{0:D4}/{1:D2}/{2:D2} ساعت {3:D2}:{4:D2}", Year, Month, Day, Hour, Minute);
            return result;
        }
        //===================================================================
        /// <summary>
        /// برگشت نتیجه به ثانیه می باشد
        /// </summary>
        public static int DifferenceOfCurrentTime(this DateTime date)
        {
            var value = Convert.ToInt32(Math.Floor(date.Subtract(DateTime.UtcNow).TotalSeconds));
            return value > 0 ? value : 0;
        }
        //===================================================================
        public static DateTime GetDayStartUTC(this DateTime DateUTC)
        {
            var localDateTime = DateUTC.ToLocalDateTime();
            return new DateTime(localDateTime.Year, localDateTime.Month, localDateTime.Day, 0, 0, 0).ToUtcDateTime();
        }

        //===================================================================
        public static DateTime GetDayEndUTC(this DateTime DateUTC)
        {
            var localDateTime = DateUTC.ToLocalDateTime();
            return new DateTime(localDateTime.Year, localDateTime.Month, localDateTime.Day, 23, 59, 59).ToUtcDateTime();
        }
        //===================================================================
        public static string ToHijriDateTime(this DateTime dateTime)
        {
            dateTime = dateTime.ToLocalTime();

            var hc = new HijriCalendar();
            var year = hc.GetYear(dateTime);
            var month = hc.GetMonth(dateTime);
            var day = hc.GetDayOfMonth(dateTime);
            var hour = hc.GetHour(dateTime);
            var minute = hc.GetMinute(dateTime);
            var second = hc.GetSecond(dateTime);

            string monthName;

            switch (month)
            {
                case 1:
                    monthName = "مُحَرَّم";
                    break;

                case 2:
                    monthName = "صَفَر";
                    break;

                case 3:
                    monthName = "رَبيع الأوّل";
                    break;

                case 4:
                    monthName = "رَبيع الثاني";
                    break;

                case 5:
                    monthName = "جُمادى الأولى";
                    break;

                case 6:
                    monthName = "جُمادى الآخرة";
                    break;

                case 7:
                    monthName = "رَجَب";
                    break;

                case 8:
                    monthName = "شَعْبان";
                    break;

                case 9:
                    monthName = "رَمَضان";
                    break;

                case 10:
                    monthName = "شَوّال";
                    break;

                case 11:
                    monthName = "ذی‌القعدة";
                    break;

                case 12:
                    monthName = "ذی‌الحجة";
                    break;

                default:
                    monthName = "خطا";
                    break;
            }

            string dayName;

            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    dayName = "السبت";
                    break;

                case DayOfWeek.Sunday:
                    dayName = "الأحد";
                    break;

                case DayOfWeek.Monday:
                    dayName = "الإثنين";
                    break;

                case DayOfWeek.Tuesday:
                    dayName = "الثلاثاء";
                    break;

                case DayOfWeek.Wednesday:
                    dayName = "الأربعاء";
                    break;

                case DayOfWeek.Thursday:
                    dayName = "الخميس";
                    break;

                case DayOfWeek.Friday:
                    dayName = "الجمعة";
                    break;

                default:
                    dayName = "خطا";
                    break;
            }

            return dayName + " - " + day + " " + monthName + " " + year;
        }
        //===================================================================
        //public static int ToUnixDateTime(this DateTime date)
        //{
        //    int unixTimestamp = (Int32)(date.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
        //    return unixTimestamp;
        //}
        //===================================================================
        //public static string ConvertUnixToDateTime(this long Milliseconds)
        //{
        //    DateTime dateUnix = new DateTime(1970, 1, 1, 0, 0, 0);
        //    return dateUnix.AddMilliseconds(Milliseconds).ToLocalDate();
        //}
        public static long ToUnixDateTime(this DateTime date)
        {
            long unixTimestamp = (long)date.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
            return unixTimestamp;
        }
    }
}
