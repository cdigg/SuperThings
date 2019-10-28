using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SuperThings.Dashboard.Models;
using System.Net.Http;

namespace SuperThings.Dashboard.Controllers
{
    //server side proxy since I'm not sure if the api and monitoring app would be on the same domain or not, and don't want to run into CORS issues
    [Route("apiProxy")]
    public class ApiProxyController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        private string ApiBaseUrl = "<getMeFromConfig>";

        public ApiProxyController(IOptions<Config> config)
        {
            ApiBaseUrl = config.Value.SuperThingsApiBaseUrl + "/api";
        }

        [HttpGet]
        [Route("count")]
        public IActionResult GetCount()
        {
            var response = client.GetAsync(ApiBaseUrl + "/monitoring/count");
            var responseString = response.Result.Content.ReadAsStringAsync().Result;
            var count = int.Parse(responseString);

            return Json(count);
        }

        [HttpGet]
        [Route("favorites")]
        public IActionResult GetFavoriteIntegers()
        {
            var response = client.GetAsync(ApiBaseUrl + "/monitoring/favorites");
            var responseString = response.Result.Content.ReadAsStringAsync().Result;

            return Ok(responseString);
        }

        [HttpGet]
        [Route("recent")]
        public IActionResult GetMostRecentRegistrants()
        {
            var response = client.GetAsync(ApiBaseUrl + "/registration/recent");
            var responseString = response.Result.Content.ReadAsStringAsync().Result;

            return Ok(responseString);
        }
    }
}