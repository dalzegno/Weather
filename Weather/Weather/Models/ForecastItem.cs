using System;

namespace Weather.Models
{
    public class ForecastItem
    {
        public DateTime dateTime { get; set; }
        public double Temperature { get; set; }
        public double WindSpeed { get; set; }
        public string Description { get; set; } 
        public string Icon { get; set; }

        //Capitalise first letter of Description
        string Desc => CapitalizeFirst(Description);
        public override string ToString() => $"{Temperature}°C,  {Desc} - Windspeed: {WindSpeed} m/s";

        private static string CapitalizeFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
    
}
