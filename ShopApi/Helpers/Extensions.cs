using System;
using Microsoft.AspNetCore.Http;

namespace ShopApi.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Header", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static int CalculateAge(this DateTime theDateTime)
        {
            var age = DateTime.Today.Year - theDateTime.Year;

            if (age == DateTime.Today.Year || theDateTime.Year == 1)
            {
                return 0;
            }

            if (theDateTime.AddYears(age) > DateTime.Today)
            {
                age--;
            }
            return age;
        }
    }
}