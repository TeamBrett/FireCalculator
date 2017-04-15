using System;

namespace FireCalculator {
    public static class DateTimeExtensions { 
        public static bool IsToday(this DateTime dateTime, Period period) {
            switch (period) {
                case Period.Annually:
                    return dateTime.Month == 12 && dateTime.Day == 31;
                case Period.Quarterly:
                    return dateTime.Month % 3 == 0 && dateTime.IsLastFridayOfMonth();
                case Period.Monthly:
                    return dateTime.IsLastFridayOfMonth();
                case Period.Fortnightly:
                    return dateTime.IsMidMonthFriday() || dateTime.IsLastFridayOfMonth();
                case Period.Weekly:
                    return dateTime.DayOfWeek == DayOfWeek.Friday;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static bool IsLastFridayOfMonth(this DateTime dateTime) {
            var daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
            var theDay = new DateTime(dateTime.Year, dateTime.Month, daysInMonth);

            while (theDay.DayOfWeek != DayOfWeek.Friday) {
                theDay = theDay.AddDays(-1);
            }

            return theDay == dateTime;
        }

        public static bool IsMidMonthFriday(this DateTime dateTime) {
            var theDay = new DateTime(dateTime.Year, dateTime.Month, 15);

            while (theDay.DayOfWeek != DayOfWeek.Friday) {
                theDay = theDay.AddDays(-1);
            }

            return theDay == dateTime;
        }
    }
}