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
    public class BeautyController : Controller
    {
        private TestAPI _api = new TestAPI();
        public async Task<IActionResult> Index()
        {
            List<BeautyNumberVm> operators = new List<BeautyNumberVm>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Beauty/getall");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                var _result = JObject.Parse(json);
                var getlist = _result["data"].Select(x => new BeautyNumberVm()
                {
                    Id = int.Parse(x["id"].ToString()),
                    Numbers = x["numbers"].ToString(),
                });
                return View(getlist);
            }

            return View(operators);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var _operator = new BeautyNumberVm();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/Beauty/detail/{Id}");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                var _result = JObject.Parse(json);
                var getdetail = _result["data"];
                _operator.Id = int.Parse(getdetail["id"].ToString());
                _operator.Numbers = getdetail["numbers"].ToString();
                return View(_operator);
            }

            return View(_operator);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEditBeautyNumberRequest BeautyNumberVm)
        {
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<CreateEditBeautyNumberRequest>("api/Beauty", BeautyNumberVm);
            postTask.Wait();
            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View();
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var _operator = new BeautyNumberVm();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/Beauty/{Id}");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var _operator = new BeautyNumberVm();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/Beauty/detail/{Id}");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                var _result = JObject.Parse(json);
                var getdetail = _result["data"];
                _operator.Id = int.Parse(getdetail["id"].ToString());
                _operator.Numbers = getdetail["numbers"].ToString();
                return View(_operator);
            }

            return View(_operator);
        }

        [HttpPost]
        public IActionResult Edit(BeautyNumberVm BeautyNumberVm)
        {
            var _update = new CreateEditBeautyNumberRequest();
            _update.Id = BeautyNumberVm.Id;
            _update.Numbers = BeautyNumberVm.Numbers;

            HttpClient client = _api.Initial();
            var puttTask = client.PutAsJsonAsync<CreateEditBeautyNumberRequest>("api/Beauty", _update);
            puttTask.Wait();
            var result = puttTask.Result;

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View();
        }
    }
}
