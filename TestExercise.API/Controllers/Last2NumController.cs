using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestExercise.Application.Catalogs;
using TestExercise.ViewModels.Catalogs;

namespace TestExercise.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Last2NumController : ControllerBase
    {
        private readonly ILast2NumCaseService _last2NumCaseService;

        public Last2NumController(ILast2NumCaseService last2NumCaseService)
        {
            _last2NumCaseService = last2NumCaseService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var getlist = await _last2NumCaseService.GetAll();
            return Ok(getlist);
        }

        [HttpGet]
        [Route("detail/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var detail = await _last2NumCaseService.GetById(id);
            if (detail == null)
                return BadRequest("Cannot find Operator");
            return Ok(detail);
        }

        [HttpPost]
        //[Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromBody] CreateEditLast2NumCaseRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _last2NumCaseService.Add(request);

            return Ok(result);
        }

        [HttpPut]//{id}
        //[Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromBody] CreateEditLast2NumCaseRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _last2NumCaseService.Update(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _last2NumCaseService.Delete(id);
            return Ok(result);
        }
    }
}
