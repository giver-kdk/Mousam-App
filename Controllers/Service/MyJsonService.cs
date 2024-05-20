using Mousam_App.Models;

namespace Mousam_App.Controllers.Service
{
    public class MyJsonService
    {
        private HttpClient _httpClient;
        public MyJsonService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<JSONResponseModel?> GetAsyncWeather(string cityName = "Mumbai")
        {
            string apiKey = "aafba97f9cdd6225a97072d8297ea264";

            string endpoint = "/data/2.5/weather?";
            string parameters = $"q={cityName}&appid={apiKey}&units=metric";
            string requestURI = endpoint + parameters;
            try
            {
                var result = await _httpClient.GetFromJsonAsync<JSONResponseModel>(requestURI);
                return result;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
