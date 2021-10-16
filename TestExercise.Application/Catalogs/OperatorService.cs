using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestExercise.Entities;
using TestExercise.Models;
using TestExercise.ViewModels;
using TestExercise.ViewModels.Catalogs;
using TestExercise.ViewModels.Common;

namespace TestExercise.Application.Catalogs
{
    public class OperatorService : IOperatorService
    {
        private readonly TestDbContext _context;

        public OperatorService(TestDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<OperatorVm>> Add(CreateEditOperatorRequest request)
        {
            var _ck = _context.Operators.Where(x => x.ProviderName == request.ProviderName);
            if (_ck.Count() > 0)
                return new ApiErrorResult<OperatorVm>("Similar");

            var _new = new Operator()
            {
                ProviderName = request.ProviderName
            };
            _context.Operators.Add(_new);
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<OperatorVm>();
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var _detail = await _context.Operators.FindAsync(id);
            //checking exist
            if (_detail == null) return new ApiErrorResult<bool>($"Cannot find any: {id}");

            _context.Operators.Remove(_detail);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<List<OperatorVm>>> GetAll()
        {
            var _all = await _context.Operators
                .Select(x => new OperatorVm()
                {
                    Id = x.Id,
                    ProviderName = x.ProviderName
                }).ToListAsync();

            return new ApiSuccessResult<List<OperatorVm>>(_all);
        }

        public async Task<ApiResult<OperatorVm>> GetById(int id)
        {
            var _detail = await _context.Operators.FindAsync(id);
            if (_detail == null)
            {
                return new ApiSuccessResult<OperatorVm>("Not found");
            }
            else
            {
                var opVm = new OperatorVm()
                {
                    Id = _detail.Id,
                    ProviderName = _detail.ProviderName,
                };

                return new ApiSuccessResult<OperatorVm>(opVm);
            }
        }

        public async Task<ApiResult<OperatorVm>> Update(CreateEditOperatorRequest request)
        {
            var _update = await _context.Operators.FindAsync(request.Id);
            //checking exist
            var _ck = _context.Operators.Where(s => s.Id != request.Id && s.ProviderName == request.ProviderName);
            if (_ck.Count() > 0)
                return new ApiErrorResult<OperatorVm>("Similar");

            _update.ProviderName = request.ProviderName;

            await _context.SaveChangesAsync();

            return new ApiSuccessResult<OperatorVm>();
        }
    }
}