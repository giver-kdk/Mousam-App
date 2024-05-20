using Microsoft.AspNetCore.Mvc;
using Mousam_App.Controllers.Service;

namespace Mousam_App.Controllers
{
    public class WeatherController : Controller
    {
        private MyJsonService _jsonService;
        //public WeatherController() { }
        // ************ DI for HTTP Client **************
        public WeatherController(MyJsonService jsonService)
        {
            _jsonService = jsonService;
        }
        // GET: api/<WeatherController>
        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            // ****** Await for asynchronous operation ***********
            var result = await _jsonService.GetAsyncWeather();
            return View(result);
        }
        [HttpPost("/")]
        public async Task<IActionResult> SetCity(IFormCollection collections)
        {
            var cityName = Convert.ToString(collections["city"]);
            var result = await _jsonService.GetAsyncWeather(cityName);
            return View("Index", result);
        }

    }
}
