using System.Collections.Generic;
using System.Threading.Tasks;
using TestExercise.ViewModels.Catalogs;
using TestExercise.ViewModels.Common;

namespace TestExercise.Application.Catalogs
{
    public interface IMatchConditionService
    {
        Task<ApiResult<MatchConditionVm>> Add(CreateEditMatchCondition request);

        Task<ApiResult<MatchConditionVm>> GetById(int id);

        Task<ApiResult<MatchConditionVm>> Update(CreateEditMatchCondition request);

        Task<ApiResult<bool>> Delete(int id);

        Task<ApiResult<List<MatchConditionVm>>> GetAll();
    }
}