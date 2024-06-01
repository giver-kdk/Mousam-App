using Mousam_App.Models;
using System.Text.Json;

namespace Mousam_App.Controllers.Service
{
    public class MyJsonService
    {
        private HttpClient _httpClient;
        public MyJsonService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<JsonModel?> GetAsyncWeather(string cityName = "Kathmandu")
        {
            string apiKey = "aafba97f9cdd6225a97072d8297ea264";
            // Request 5 days of weather data at once
            string endpoint = "/data/2.5/forecast?";
            string parameters = $"q={cityName}&appid={apiKey}&units=metric";
            string requestURI = endpoint + parameters;
            try
            {
                //var result = await _httpClient.GetFromJsonAsync<JSONResponseModel>(requestURI);
                var result = await _httpClient.GetFromJsonAsync<JsonModel>(requestURI);
                Console.WriteLine(result.ToString());
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
