using System.Collections.Generic;
using System.Threading.Tasks;
using TestExercise.ViewModels.Catalogs;
using TestExercise.ViewModels.Common;

namespace TestExercise.Application.Catalogs
{
    public interface IBeautyNumberService
    {
        Task<ApiResult<BeautyNumberVm>> Add(CreateEditBeautyNumberRequest request);

        Task<ApiResult<BeautyNumberVm>> GetById(int id);

        Task<ApiResult<BeautyNumberVm>> Update(CreateEditBeautyNumberRequest request);

        Task<ApiResult<bool>> Delete(int id);

        Task<ApiResult<List<BeautyNumberVm>>> GetAll();
    }
}