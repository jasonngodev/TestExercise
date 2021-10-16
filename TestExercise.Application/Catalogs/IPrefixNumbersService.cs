using System.Collections.Generic;
using System.Threading.Tasks;
using TestExercise.ViewModels.Catalogs;
using TestExercise.ViewModels.Common;

namespace TestExercise.Application.Catalogs
{
    public interface IPrefixNumbersService
    {
        Task<ApiResult<PrefixNumbersVm>> Add(CreateEditPrefixNumbersRequest request);

        Task<ApiResult<PrefixNumbersVm>> GetById(int id);

        Task<ApiResult<PrefixNumbersVm>> Update(CreateEditPrefixNumbersRequest request);

        Task<ApiResult<bool>> Delete(int id);

        Task<ApiResult<List<PrefixNumbersVm>>> GetAll();
    }
}