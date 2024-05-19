using Microsoft.AspNetCore.Mvc;
using Mousam_App.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mousam_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private IHttpClientFactory _httpClientFactory;
        //public WeatherController() { }
        // ************ DI for HTTP Client **************
        public WeatherController(IHttpClientFactory httpClientFactory) { 
            _httpClientFactory = httpClientFactory;
        }
        // GET: api/<WeatherController>
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            string parameters = "v1/forecast?latitude=52.52&longitude=13.41&current=temperature_2m,wind_speed_10m&hourly=temperature_2m,relative_humidity_2m,wind_speed_10m";
            // ****** Create HTTP Client from name provided during DI in Program.cs ******
            var weatherClient = _httpClientFactory.CreateClient("weather");
            // ****** Await for asynchronous operation ***********
            var response = await weatherClient.GetAsync(parameters);
            if(response.IsSuccessStatusCode)
            {
                return Ok(response.Content.ReadAsStringAsync());
            }
            else{
                return BadRequest("Failed to get weather data!");
            }
        }

        // GET api/<WeatherController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            
            return "value";
        }

        // POST api/<WeatherController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<WeatherController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WeatherController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
