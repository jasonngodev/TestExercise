using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TestExercise.ViewModels.Catalogs;
using TestExercise.WebApp.Helpers;

namespace TestExercise.WebApp.Controllers
{
    public class MatchController : Controller
    {
        private TestAPI _api = new TestAPI();
        public async Task<IActionResult> Index()
        {
            List<MatchConditionVm> operators = new List<MatchConditionVm>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Match/getall");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                var _result = JObject.Parse(json);
                var getlist = _result["data"].Select(x => new MatchConditionVm()
                {
                    Id = int.Parse(x["id"].ToString()),
                    Conditions = x["conditions"].ToString(),
                });
                return View(getlist);
            }

            return View(operators);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var _operator = new MatchConditionVm();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/Match/detail/{Id}");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                var _result = JObject.Parse(json);
                var getdetail = _result["data"];
                _operator.Id = int.Parse(getdetail["id"].ToString());
                _operator.Conditions = getdetail["conditions"].ToString();
                return View(_operator);
            }

            return View(_operator);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEditMatchCondition MatchConditionVm)
        {
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<CreateEditMatchCondition>("api/Match", MatchConditionVm);
            postTask.Wait();
            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View();
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var _operator = new MatchConditionVm();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/Match/{Id}");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var _operator = new MatchConditionVm();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/Match/detail/{Id}");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                var _result = JObject.Parse(json);
                var getdetail = _result["data"];
                _operator.Id = int.Parse(getdetail["id"].ToString());
                _operator.Conditions = getdetail["conditions"].ToString();
                return View(_operator);
            }

            return View(_operator);
        }

        [HttpPost]
        public IActionResult Edit(MatchConditionVm MatchConditionVm)
        {
            var _update = new CreateEditMatchCondition();
            _update.Id = MatchConditionVm.Id;
            _update.Conditions = MatchConditionVm.Conditions;

            HttpClient client = _api.Initial();
            var puttTask = client.PutAsJsonAsync<CreateEditMatchCondition>("api/Match", _update);
            puttTask.Wait();
            var result = puttTask.Result;

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View();
        }
    }
}
