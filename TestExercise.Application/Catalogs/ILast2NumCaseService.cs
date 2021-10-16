using System.Collections.Generic;
using System.Threading.Tasks;
using TestExercise.ViewModels.Catalogs;
using TestExercise.ViewModels.Common;

namespace TestExercise.Application.Catalogs
{
    public interface ILast2NumCaseService
    {
        Task<ApiResult<Last2NumCaseVm>> Add(CreateEditLast2NumCaseRequest request);

        Task<ApiResult<Last2NumCaseVm>> GetById(int id);

        Task<ApiResult<Last2NumCaseVm>> Update(CreateEditLast2NumCaseRequest request);

        Task<ApiResult<bool>> Delete(int id);

        Task<ApiResult<List<Last2NumCaseVm>>> GetAll();
    }
}