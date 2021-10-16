using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestExercise.Entities;
using TestExercise.Models;
using TestExercise.ViewModels;
using TestExercise.ViewModels.Catalogs;
using TestExercise.ViewModels.Common;

namespace TestExercise.Application.Catalogs
{
    public class FengShuiNumberService : IFengShuiNumberService
    {
        private readonly TestDbContext _context;

        public FengShuiNumberService(TestDbContext context)
        {
            _context = context;
        }

        private async Task<ApiResult<int>> FindFengShui(CreateEditFengShuiNumberRequest request)
        {
            //0:not feng shui;1: feng shui; 2: feng shui with nice pair number;
            var _stat = 0;
            var _2num = request.PhoneNumber.Substring(request.PhoneNumber.Length - 2);
            //1. check list of bad numbers:
            var _badlist = _context.Last2NumCases;
            bool _ckNum = false;
            foreach (var item in _badlist)
            {
                //2.Checking the last 2 num
                var _arr = Regex.Replace(item.chars, @"\s+", "");
                if (_arr.Split(',').Where(x => x == _2num).Count() > 0)
                {
                    _ckNum = true;
                    break;
                }
            }

            if (!_ckNum)
            {
                //2. check feng shui and comparision
                //convert string to array
                char[] charArr = request.PhoneNumber.ToCharArray();
                int _s1 = 0, _s2 = 0, _count = 1;
                foreach (var i in charArr)
                {
                    if (_count <= 5)
                        _s1 += int.Parse(i.ToString());
                    else
                        _s2 += int.Parse(i.ToString());
                    _count += 1;
                }
                var _str = _s1.ToString() + "/" + _s2.ToString();
                //string[] conditions = File.ReadAllLines(textFileCondition);
                var _conditions = _context.MatchConditions;
                if (_conditions.Where(x => x.Conditions == _str).Count() > 0)
                    _stat = 1;

                if (_stat == 1)
                {
                    //3.Is this the beautiful number?
                    //string[] _beauties = File.ReadAllLines(textFileBeautiy);
                    var _beauties = _context.BeautyNumbers;
                    foreach (var j in _beauties)
                    {
                        var _arr = Regex.Replace(j.Numbers, @"\s+", "");
                        if (_arr.Split(',').Where(x => x == _2num).Count() > 0)
                        {
                            _stat = 2;
                            break;
                        }
                    }
                    //4.Final
                    //4.1 Print screen
                    //4.2 Save into table FengShuiNumbers
                    //get Operator
                    //var getOperator = _context.PrefixNumbers.Where(x=>x.PrefixNumber == request.PhoneNumber.Substring(0, 3));
                    //AddFengShuiNumber(conn, _mobile, isBeauty ? _2num : null, getOperator.OperatorID);
                }
                else
                    _stat = 0;
            }

            return new ApiSuccessResult<int>(_stat);
        }

        public async Task<ApiResult<FengShuiNumberVm>> Add(CreateEditFengShuiNumberRequest request)
        {
            var _result = FindFengShui(request).Result;
            var getOperator = _context.PrefixNumbers.Where(x => x.PrefixNumber == request.PhoneNumber.Substring(0, 3));
            if (_result.Data == 0)
                return new ApiSuccessResult<FengShuiNumberVm>($"Your mobile {request.PhoneNumber} is not feng shui");

            var _ck = _context.FengShuiNumbers.Where(x => x.PhoneNumber == request.PhoneNumber);
            if (_ck.Count() > 0)
            {
                var _str = _ck.FirstOrDefault().LastNum == null ? "it is feng shui" : "it is feng shui and nice pair";
                return new ApiErrorResult<FengShuiNumberVm>("Already in database, "+ _str);
            }
            var _new = new FengShuiNumber()
            {
                PhoneNumber = request.PhoneNumber,
                LastNum = _result.Data == 1?null: request.PhoneNumber.Substring(request.PhoneNumber.Length - 2),
                OperatorID = getOperator.FirstOrDefault().OperatorId
            };
            _context.FengShuiNumbers.Add(_new);
            await _context.SaveChangesAsync();

            if (_result.Data == 1)
                return new ApiSuccessResult<FengShuiNumberVm>($"Your mobile {request.PhoneNumber} is feng shui");

            return new ApiSuccessResult<FengShuiNumberVm>($"Your mobile {request.PhoneNumber} is feng shui and is a nice number");
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var _detail = await _context.FengShuiNumbers.FindAsync(id);
            //checking exist
            if (_detail == null) return new ApiErrorResult<bool>($"Cannot find any: {id}");

            _context.FengShuiNumbers.Remove(_detail);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<List<FengShuiNumberVm>>> GetAll()
        {
            var _all = await _context.FengShuiNumbers.Include(x => x.Operator)
                .Select(x => new FengShuiNumberVm()
                {
                    Id = x.Id,
                    PhoneNumber = x.PhoneNumber,
                    LastNum = x.LastNum,
                    OperatorID = x.OperatorID,
                    Operator = new OperatorVm()
                    {
                        Id = x.Operator.Id,
                        ProviderName = x.Operator.ProviderName
                    }
                }).ToListAsync();

            return new ApiSuccessResult<List<FengShuiNumberVm>>(_all);
        }

        public async Task<ApiResult<FengShuiNumberVm>> GetById(int id)
        {
            var _detail = await _context.FengShuiNumbers.FindAsync(id);
            if (_detail == null)
            {
                return new ApiSuccessResult<FengShuiNumberVm>("Not found");
            }
            else
            {
                var opVm = new FengShuiNumberVm()
                {
                    Id = _detail.Id,
                    PhoneNumber = _detail.PhoneNumber,
                    LastNum = _detail.LastNum,
                    OperatorID = _detail.OperatorID,
                    Operator = new OperatorVm()
                    {
                        Id = _detail.Operator.Id,
                        ProviderName = _detail.Operator.ProviderName
                    }
                };

                return new ApiSuccessResult<FengShuiNumberVm>(opVm);
            }
        }

        public async Task<ApiResult<FengShuiNumberVm>> Update(CreateEditFengShuiNumberRequest request)
        {
            var _update = await _context.FengShuiNumbers.FindAsync(request.Id);
            //checking exist
            var _ck = _context.FengShuiNumbers.Where(s => s.Id != request.Id && s.PhoneNumber == request.PhoneNumber);
            if (_ck.Count() > 0)
                return new ApiErrorResult<FengShuiNumberVm>("Similar");

            _update.PhoneNumber = request.PhoneNumber;
            _update.LastNum = request.LastNum;
            _update.OperatorID = request.OperatorID;

            await _context.SaveChangesAsync();

            return new ApiSuccessResult<FengShuiNumberVm>();
        }
    }
}