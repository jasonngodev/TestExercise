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
    public class MatchConditionService: IMatchConditionService
    {
        private readonly TestDbContext _context;

        public MatchConditionService(TestDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<MatchConditionVm>> Add(CreateEditMatchCondition request)
        {
            var _ck = _context.MatchConditions.Where(x => x.Conditions == request.Conditions);
            if (_ck.Count() > 0)
                return new ApiErrorResult<MatchConditionVm>("Similar");

            var _new = new MatchCondition()
            {
                Conditions = request.Conditions
            };
            _context.MatchConditions.Add(_new);
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<MatchConditionVm>();
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var _detail = await _context.MatchConditions.FindAsync(id);
            //checking exist
            if (_detail == null) return new ApiErrorResult<bool>($"Cannot find any: {id}");

            _context.MatchConditions.Remove(_detail);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<List<MatchConditionVm>>> GetAll()
        {
            var _all = await _context.MatchConditions
                .Select(x => new MatchConditionVm()
                {
                    Id = x.Id,
                    Conditions = x.Conditions
                }).ToListAsync();

            return new ApiSuccessResult<List<MatchConditionVm>>(_all);
        }

        public async Task<ApiResult<MatchConditionVm>> GetById(int id)
        {
            var _detail = await _context.MatchConditions.FindAsync(id);
            if (_detail == null)
            {
                return new ApiSuccessResult<MatchConditionVm>("Not found");
            }
            else
            {
                var opVm = new MatchConditionVm()
                {
                    Id = _detail.Id,
                    Conditions = _detail.Conditions
                };

                return new ApiSuccessResult<MatchConditionVm>(opVm);
            }
        }

        public async Task<ApiResult<MatchConditionVm>> Update(CreateEditMatchCondition request)
        {
            var _update = await _context.MatchConditions.FindAsync(request.Id);
            //checking exist
            var _ck = _context.MatchConditions.Where(s => s.Id != request.Id && s.Conditions == request.Conditions);
            if (_ck.Count() > 0)
                return new ApiErrorResult<MatchConditionVm>("Similar");

            _update.Conditions = request.Conditions;

            await _context.SaveChangesAsync();

            return new ApiSuccessResult<MatchConditionVm>();
        }
    }
}
