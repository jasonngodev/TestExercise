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
    public class BeautyController : ControllerBase
    {
        private readonly IBeautyNumberService _beautyNumberService;

        public BeautyController(IBeautyNumberService beautyNumberService)
        {
            _beautyNumberService = beautyNumberService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var getlist = await _beautyNumberService.GetAll();
            return Ok(getlist);
        }

        [HttpGet]
        [Route("detail/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var detail = await _beautyNumberService.GetById(id);
            if (detail == null)
                return BadRequest("Cannot find Operator");
            return Ok(detail);
        }

        [HttpPost]
        //[Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromBody] CreateEditBeautyNumberRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _beautyNumberService.Add(request);

            return Ok(result);
        }

        [HttpPut]//{id}
        //[Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromBody] CreateEditBeautyNumberRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _beautyNumberService.Update(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _beautyNumberService.Delete(id);
            return Ok(result);
        }
    }
}
