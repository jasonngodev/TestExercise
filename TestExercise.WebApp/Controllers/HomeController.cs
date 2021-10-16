using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TestExercise.ViewModels;
using TestExercise.ViewModels.Catalogs;
using TestExercise.WebApp.Helpers;
using TestExercise.WebApp.Models;

namespace TestExercise.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private TestAPI _api = new TestAPI();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FindFengShui(CreateEditFengShuiNumberRequest request)
        {
            ViewBag.Error = "";
            if (request.PhoneNumber.Length <= 9 || request.PhoneNumber.Length > 10 || request.PhoneNumber.Substring(0, 1) != "0")
            {
                ViewBag.Error = "Your mobile is not incorrect format";
                return View("Index");
            }
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<CreateEditFengShuiNumberRequest>("api/FengShui", request);
            postTask.Wait();
            var result = postTask.Result;
            ViewBag.Result = "";
            if (result.IsSuccessStatusCode)
            {
                var json = result.Content.ReadAsStringAsync().Result;
                var _result = JObject.Parse(json);
                ViewBag.Result = _result["message"];
                return View("Index");
            }

            ViewBag.Result = $"Your mobile {request.PhoneNumber} is not feng shui";
            return View("Index");
        }

        public async Task<IActionResult> List()
        {
            List<FengShuiNumberVm> operators = new List<FengShuiNumberVm>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/FengShui/getall");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                var _result = JObject.Parse(json);
                var getlist = _result["data"].Select(x => new FengShuiNumberVm()
                {
                    Id = int.Parse(x["id"].ToString()),
                    OperatorID = int.Parse(x["operatorID"].ToString()),
                    PhoneNumber = x["phoneNumber"].ToString(),
                    LastNum = x["lastNum"].ToString(),
                    Operator = JsonConvert.DeserializeObject<OperatorVm>(x["operator"].ToString())
                });
                return View(getlist);
            }

            return View(operators);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}