using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using MauiPractice.Models;

namespace MauiPractice.Services
{
    public static class ApiService
    {
        public static async Task<Root> GetWeather(double latitude, double longitude)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(string.Format("https://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units=metric&appid=f964ec4ab7bffd3545a916fca1858e4f", latitude,longitude));
           return JsonConvert.DeserializeObject<Root>(response);


        }    
        public static async Task<Root> GetWeatherByCity(string cityName)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(string.Format("https://api.openweathermap.org/data/2.5/forecast?q={0}&units=metric&appid=f964ec4ab7bffd3545a916fca1858e4f", cityName));
           return JsonConvert.DeserializeObject<Root>(response);
        } 



    }
}
 