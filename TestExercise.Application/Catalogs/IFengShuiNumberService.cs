using System.Collections.Generic;
using System.Threading.Tasks;
using TestExercise.ViewModels.Catalogs;
using TestExercise.ViewModels.Common;

namespace TestExercise.Application.Catalogs
{
    public interface IFengShuiNumberService
    {
        Task<ApiResult<FengShuiNumberVm>> Add(CreateEditFengShuiNumberRequest request);

        Task<ApiResult<FengShuiNumberVm>> GetById(int id);

        Task<ApiResult<FengShuiNumberVm>> Update(CreateEditFengShuiNumberRequest request);

        Task<ApiResult<bool>> Delete(int id);

        Task<ApiResult<List<FengShuiNumberVm>>> GetAll();
    }
}