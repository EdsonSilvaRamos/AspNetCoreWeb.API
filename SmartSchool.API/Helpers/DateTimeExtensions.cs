using System;

namespace SmartSchool.API.Helpers
{
    public static class DateTimeExtensions
    {
        public static int GetCurrentAge(this DateTime dateTime)
        {
            var currentDate = DateTime.UtcNow;
            var age = currentDate.Year - dateTime.Year;
            
            if(currentDate < dateTime.AddYears(age))
            {
                age--;
            }

            return age;
        }
    }
}