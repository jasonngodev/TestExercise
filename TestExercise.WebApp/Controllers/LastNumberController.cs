using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
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
    public class LastNumberController : Controller
    {
        private TestAPI _api = new TestAPI();
        public async Task<IActionResult> Index()
        {
            List<Last2NumCaseVm> operators = new List<Last2NumCaseVm>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Last2Num/getall");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                var _result = JObject.Parse(json);
                var getlist = _result["data"].Select(x => new Last2NumCaseVm()
                {
                    Id = int.Parse(x["id"].ToString()),
                    chars = x["chars"].ToString(),
                });
                return View(getlist);
            }

            return View(operators);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var _operator = new Last2NumCaseVm();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/Last2Num/detail/{Id}");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                var _result = JObject.Parse(json);
                var getdetail = _result["data"];
                _operator.Id = int.Parse(getdetail["id"].ToString());
                _operator.chars = getdetail["chars"].ToString();
                return View(_operator);
            }

            return View(_operator);
        }

        public ActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEditLast2NumCaseRequest Last2NumCaseVm)
        {
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<CreateEditLast2NumCaseRequest>("api/Last2Num", Last2NumCaseVm);
            postTask.Wait();
            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Error = "Error";

            return View();
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var _operator = new Last2NumCaseVm();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/Last2Num/{Id}");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var _operator = new Last2NumCaseVm();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/Last2Num/detail/{Id}");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                var _result = JObject.Parse(json);
                var getdetail = _result["data"];
                _operator.Id = int.Parse(getdetail["id"].ToString());
                _operator.chars = getdetail["chars"].ToString();
                return View(_operator);
            }

            return View(_operator);
        }

        [HttpPost]
        public IActionResult Edit(Last2NumCaseVm Last2NumCaseVm)
        {
            var _update = new CreateEditLast2NumCaseRequest();
            _update.Id = Last2NumCaseVm.Id;
            _update.chars = Last2NumCaseVm.chars;

            HttpClient client = _api.Initial();
            var puttTask = client.PutAsJsonAsync<CreateEditLast2NumCaseRequest>("api/Last2Num", _update);
            puttTask.Wait();
            var result = puttTask.Result;

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View();
        }
    }
}
