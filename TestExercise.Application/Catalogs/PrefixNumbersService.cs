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
    public class PrefixNumbersService : IPrefixNumbersService
    {
        private readonly TestDbContext _context;

        public PrefixNumbersService(TestDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<PrefixNumbersVm>> Add(CreateEditPrefixNumbersRequest request)
        {
            var _ck = _context.PrefixNumbers.Where(x => x.PrefixNumber == request.PrefixNumber);
            if (_ck.Count() > 0)
                return new ApiErrorResult<PrefixNumbersVm>("Similar");

            var _new = new PrefixNumbers()
            {
                OperatorId = request.OperatorId,
                PrefixNumber = request.PrefixNumber
            };
            _context.PrefixNumbers.Add(_new);
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<PrefixNumbersVm>();
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var _detail = await _context.PrefixNumbers.FindAsync(id);
            //checking exist
            if (_detail == null) return new ApiErrorResult<bool>($"Cannot find any: {id}");

            _context.PrefixNumbers.Remove(_detail);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<List<PrefixNumbersVm>>> GetAll()
        {
            var _all = await _context.PrefixNumbers.Include(x => x.Operator)
                .Select(x => new PrefixNumbersVm()
                {
                    PrefixId = x.PrefixId,
                    OperatorId = x.OperatorId,
                    PrefixNumber = x.PrefixNumber,
                    Operator = new OperatorVm()
                    {
                        Id = x.Operator.Id,
                        ProviderName = x.Operator.ProviderName
                    }
                }).ToListAsync();

            return new ApiSuccessResult<List<PrefixNumbersVm>>(_all);
        }

        public async Task<ApiResult<PrefixNumbersVm>> GetById(int id)
        {
            var _detail = _context.PrefixNumbers.Include(x=>x.Operator).Where(x=>x.PrefixId == id).FirstOrDefault();
            if (_detail == null)
            {
                return new ApiSuccessResult<PrefixNumbersVm>("Not found");
            }
            else
            {
                var opVm = new PrefixNumbersVm()
                {
                    PrefixId = _detail.PrefixId,
                    OperatorId = _detail.OperatorId,
                    PrefixNumber = _detail.PrefixNumber,
                    Operator = new OperatorVm()
                    {
                        Id = _detail.Operator.Id,
                        ProviderName = _detail.Operator.ProviderName
                    }
                };

                return new ApiSuccessResult<PrefixNumbersVm>(opVm);
            }
        }

        public async Task<ApiResult<PrefixNumbersVm>> Update(CreateEditPrefixNumbersRequest request)
        {
            var _update = await _context.PrefixNumbers.FindAsync(request.PrefixId);
            //checking exist
            var _ck = _context.PrefixNumbers.Where(s => s.PrefixId != request.PrefixId && s.PrefixNumber == request.PrefixNumber);
            if (_ck.Count() > 0)
                return new ApiErrorResult<PrefixNumbersVm>("Similar");

            _update.OperatorId = request.OperatorId;
            _update.PrefixNumber = request.PrefixNumber;

            await _context.SaveChangesAsync();

            return new ApiSuccessResult<PrefixNumbersVm>();
        }
    }
}