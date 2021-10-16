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
    public class PrefixController : ControllerBase
    {
        private readonly IPrefixNumbersService _prefixNumbersService;

        public PrefixController(IPrefixNumbersService prefixNumbersService)
        {
            _prefixNumbersService = prefixNumbersService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var getlist = await _prefixNumbersService.GetAll();
            return Ok(getlist);
        }

        [HttpGet]
        [Route("detail/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var detail = await _prefixNumbersService.GetById(id);
            if (detail == null)
                return BadRequest("Cannot find Operator");
            return Ok(detail);
        }

        [HttpPost]
        //[Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromBody] CreateEditPrefixNumbersRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _prefixNumbersService.Add(request);

            return Ok(result);
        }

        [HttpPut]//{id}
        //[Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromBody] CreateEditPrefixNumbersRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _prefixNumbersService.Update(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _prefixNumbersService.Delete(id);
            return Ok(result);
        }
    }
}
