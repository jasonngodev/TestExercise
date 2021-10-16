using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestExercise.Entities;
using TestExercise.Models;
using TestExercise.ViewModels.Catalogs;
using TestExercise.ViewModels.Common;

namespace TestExercise.Application.Catalogs
{
    public class BeautyNumberService : IBeautyNumberService
    {
        private readonly TestDbContext _context;

        public BeautyNumberService(TestDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<BeautyNumberVm>> Add(CreateEditBeautyNumberRequest request)
        {
            var _ck = _context.BeautyNumbers.Where(x => x.Numbers == request.Numbers);
            if (_ck.Count() > 0)
                return new ApiErrorResult<BeautyNumberVm>("Similar");

            var _new = new BeautyNumber()
            {
                Numbers = request.Numbers
            };
            _context.BeautyNumbers.Add(_new);
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<BeautyNumberVm>();
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var _detail = await _context.BeautyNumbers.FindAsync(id);
            //checking exist
            if (_detail == null) return new ApiErrorResult<bool>($"Cannot find any: {id}");

            _context.BeautyNumbers.Remove(_detail);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<List<BeautyNumberVm>>> GetAll()
        {
            var _all = await _context.BeautyNumbers
                .Select(x => new BeautyNumberVm()
                {
                    Id = x.Id,
                    Numbers = x.Numbers
                }).ToListAsync();

            return new ApiSuccessResult<List<BeautyNumberVm>>(_all);
        }

        public async Task<ApiResult<BeautyNumberVm>> GetById(int id)
        {
            var _detail = await _context.BeautyNumbers.FindAsync(id);
            if (_detail == null)
            {
                return new ApiSuccessResult<BeautyNumberVm>("Not found");
            }
            else
            {
                var opVm = new BeautyNumberVm()
                {
                    Id = _detail.Id,
                    Numbers = _detail.Numbers,
                };

                return new ApiSuccessResult<BeautyNumberVm>(opVm);
            }
        }

        public async Task<ApiResult<BeautyNumberVm>> Update(CreateEditBeautyNumberRequest request)
        {
            var _update = await _context.BeautyNumbers.FindAsync(request.Id);
            //checking exist
            var _ck = _context.BeautyNumbers.Where(s => s.Id != request.Id && s.Numbers == request.Numbers);
            if (_ck.Count() > 0)
                return new ApiErrorResult<BeautyNumberVm>("Similar");

            _update.Numbers = request.Numbers;

            await _context.SaveChangesAsync();

            return new ApiSuccessResult<BeautyNumberVm>();
        }
    }
}