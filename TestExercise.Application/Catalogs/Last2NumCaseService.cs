using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestExercise.Entities;
using TestExercise.Models;
using TestExercise.ViewModels.Catalogs;
using TestExercise.ViewModels.Common;

namespace TestExercise.Application.Catalogs
{
    public class Last2NumCaseService: ILast2NumCaseService
    {
        private readonly TestDbContext _context;

        public Last2NumCaseService(TestDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<Last2NumCaseVm>> Add(CreateEditLast2NumCaseRequest request)
        {
            var _ck = _context.Last2NumCases.Where(x => x.chars == request.chars);
            if (_ck.Count() > 0)
                return new ApiErrorResult<Last2NumCaseVm>("Similar");

            var _new = new Last2NumCase()
            {
                chars = request.chars
            };
            _context.Last2NumCases.Add(_new);
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<Last2NumCaseVm>();
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var _detail = await _context.Last2NumCases.FindAsync(id);
            //checking exist
            if (_detail == null) return new ApiErrorResult<bool>($"Cannot find any: {id}");

            _context.Last2NumCases.Remove(_detail);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<List<Last2NumCaseVm>>> GetAll()
        {
            var _all = await _context.Last2NumCases
                .Select(x => new Last2NumCaseVm()
                {
                    Id = x.Id,
                    chars = x.chars
                }).ToListAsync();

            return new ApiSuccessResult<List<Last2NumCaseVm>>(_all);
        }

        public async Task<ApiResult<Last2NumCaseVm>> GetById(int id)
        {
            var _detail = await _context.Last2NumCases.FindAsync(id);
            if (_detail == null)
            {
                return new ApiSuccessResult<Last2NumCaseVm>("Not found");
            }
            else
            {
                var opVm = new Last2NumCaseVm()
                {
                    Id = _detail.Id,
                    chars = _detail.chars
                };

                return new ApiSuccessResult<Last2NumCaseVm>(opVm);
            }
        }

        public async Task<ApiResult<Last2NumCaseVm>> Update(CreateEditLast2NumCaseRequest request)
        {
            var _update = await _context.Last2NumCases.FindAsync(request.Id);
            //checking exist
            var _ck = _context.Last2NumCases.Where(s => s.Id != request.Id && s.chars == request.chars);
            if (_ck.Count() > 0)
                return new ApiErrorResult<Last2NumCaseVm>("Similar");

            _update.chars = request.chars;

            await _context.SaveChangesAsync();

            return new ApiSuccessResult<Last2NumCaseVm>();
        }
    }
}
