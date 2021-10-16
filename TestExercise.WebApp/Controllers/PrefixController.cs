using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    public class PrefixController : Controller
    {
        private TestAPI _api = new TestAPI();
        public async Task<IActionResult> Index()
        {
            List<PrefixNumbersVm> operators = new List<PrefixNumbersVm>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Prefix/getall");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                var _result = JObject.Parse(json);
                var getlist = _result["data"].Select(x => new PrefixNumbersVm()
                {
                    PrefixId = int.Parse(x["prefixId"].ToString()),
                    OperatorId = int.Parse(x["operatorId"].ToString()),
                    PrefixNumber = x["prefixNumber"].ToString(),
                    Operator = JsonConvert.DeserializeObject<OperatorVm>(x["operator"].ToString())
                });
                return View(getlist);
            }

            return View(operators);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var _operator = new PrefixNumbersVm();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/Prefix/detail/{Id}");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                var _result = JObject.Parse(json);
                var getdetail = _result["data"];
                _operator.PrefixId = int.Parse(getdetail["prefixId"].ToString());
                _operator.OperatorId = int.Parse(getdetail["operatorId"].ToString());
                _operator.PrefixNumber = getdetail["prefixNumber"].ToString();
                Object _operatorDetail = getdetail["operator"];
                var _a1 = JsonConvert.DeserializeObject<OperatorVm>(_operatorDetail.ToString());
                _operator.Operator = _a1;
                return View(_operator);
            }

            return View(_operator);
        }

        private async Task<List<OperatorVm>> GetOperators()
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
                return getlist.ToList();
            }
            return operators;
        }
        public ActionResult Create()
        {
            var _new = new CreateEditPrefixNumbersRequest();
            var getlist = GetOperators().Result;
            _new.Operators = getlist;
            return View(_new);
        }

        [HttpPost]
        public IActionResult Create(CreateEditPrefixNumbersRequest PrefixNumbersVm)
        {
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<CreateEditPrefixNumbersRequest>("api/Prefix", PrefixNumbersVm);
            postTask.Wait();
            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View();
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var _operator = new PrefixNumbersVm();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/Prefix/{Id}");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var getlist = GetOperators().Result;

            var _operator = new CreateEditPrefixNumbersRequest();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/Prefix/detail/{Id}");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                var _result = JObject.Parse(json);
                var getdetail = _result["data"];
                _operator.PrefixId = int.Parse(getdetail["prefixId"].ToString());
                _operator.OperatorId = int.Parse(getdetail["operatorId"].ToString());
                _operator.PrefixNumber = getdetail["prefixNumber"].ToString();
                _operator.Operators = getlist.ToList();
                return View(_operator);
            }

            return View(_operator);
        }

        [HttpPost]
        public IActionResult Edit(PrefixNumbersVm prefixNumbersVm)
        {
            var _update = new CreateEditPrefixNumbersRequest();
            _update.PrefixId = prefixNumbersVm.PrefixId;
            _update.OperatorId = prefixNumbersVm.OperatorId;
            _update.PrefixNumber = prefixNumbersVm.PrefixNumber;

            HttpClient client = _api.Initial();
            var puttTask = client.PutAsJsonAsync<CreateEditPrefixNumbersRequest>("api/Prefix", _update);
            puttTask.Wait();
            var result = puttTask.Result;

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View();
        }
    }
}
