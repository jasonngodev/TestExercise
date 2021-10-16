using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TestExercise.ViewModels;
using TestExercise.ViewModels.Catalogs;
using TestExercise.WebApp.Helpers;

namespace TestExercise.WebApp.Controllers
{
    public class OperatorsController : Controller
    {
        private TestAPI _api = new TestAPI();

        public async Task<IActionResult> Index()
        {
            List<OperatorVm> operators = new List<OperatorVm>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/operator/getall");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                var _result = JObject.Parse(json);
                var getlist = _result["data"].Select(x => new OperatorVm()
                {
                    Id = int.Parse(x["id"].ToString()),
                    ProviderName = x["providerName"].ToString(),
                });
                return View(getlist);
            }

            return View(operators);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var _operator = new OperatorVm();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/operator/detail/{Id}");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                var _result = JObject.Parse(json);
                var getdetail = _result["data"];
                _operator.Id = int.Parse(getdetail["id"].ToString());
                _operator.ProviderName = getdetail["providerName"].ToString();
                return View(_operator);
            }

            return View(_operator);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEditOperatorRequest operatorVm)
        {
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<CreateEditOperatorRequest>("api/operator", operatorVm);
            postTask.Wait();
            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View();
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var _operator = new OperatorVm();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/operator/{Id}");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var _operator = new OperatorVm();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/operator/detail/{Id}");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                var _result = JObject.Parse(json);
                var getdetail = _result["data"];
                _operator.Id = int.Parse(getdetail["id"].ToString());
                _operator.ProviderName = getdetail["providerName"].ToString();
                return View(_operator);
            }

            return View(_operator);
        }

        [HttpPost]
        public IActionResult Edit(OperatorVm operatorVm)
        {
            var _update = new CreateEditOperatorRequest();
            _update.Id = operatorVm.Id;
            _update.ProviderName = operatorVm.ProviderName;

            HttpClient client = _api.Initial();
            var puttTask = client.PutAsJsonAsync<CreateEditOperatorRequest>("api/operator", _update);
            puttTask.Wait();
            var result = puttTask.Result;

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View();
        }
    }
}