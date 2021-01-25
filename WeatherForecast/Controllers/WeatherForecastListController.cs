using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherForecast.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeatherForecast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastListController : ControllerBase
    {
        [HttpGet("{city}")]
        public async Task<IActionResult> GetList(string city)
        {
            try
            {
                string key = "3e0e7f2908304974a7e206223a8e3e63";
                string cnt = "5";
                string url = $"api.openweathermap.org/data/2.5/forecast?q={city}&units=metric&cnt={cnt}&appid={key}";
                HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create("https://" + url);
                HttpWebResponse httpWebResponse =  (HttpWebResponse) await httpWebRequest.GetResponseAsync();

                string response;
                using (var reader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                }
                WeatherListModel weatherModel = JsonConvert.DeserializeObject<WeatherListModel>(response);
                return Ok(weatherModel);
            }
            catch (HttpRequestException httpRequestException)
            {
                return BadRequest($"Error: {httpRequestException.Message}");
            }
        }
    }
}
