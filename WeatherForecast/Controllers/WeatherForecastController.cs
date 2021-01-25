using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherForecast.Models;

namespace WeatherForecast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> Get(string city)
        {
            try
            {
                string key = "3e0e7f2908304974a7e206223a8e3e63";
                string url = $"api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={key}";
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://" + url);
                HttpWebResponse httpWebResponse = (HttpWebResponse) await httpWebRequest.GetResponseAsync();

                string response;
                using (var reader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                }
                WeatherModel weatherModel = JsonConvert.DeserializeObject<WeatherModel>(response);
                return Ok(weatherModel);
            }
            catch (HttpRequestException httpRequestException)
            {
                return BadRequest($"Error: {httpRequestException.Message}");
            }
        }
        
    }
}
