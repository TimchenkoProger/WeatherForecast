using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecast.Models
{
    public class WeatherModel
    {
        public string Name { get; set; }
        public string dt;
        public string Dt
        {
            get
            {
                DateTime temp = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                temp = temp.AddSeconds(uint.Parse(dt)).ToLocalTime();
                dt = temp.ToShortDateString();
                return dt;
            }
            set { dt = value; }
        }
        public TemperatureInfo Main { get; set; }
        public WindInfo Wind { get; set; }
        public CloudsInfo Clouds { get; set; }
    }
}
