using System.Collections.Generic;
using System.Threading.Tasks;
using TestExercise.ViewModels;
using TestExercise.ViewModels.Catalogs;
using TestExercise.ViewModels.Common;

namespace TestExercise.Application.Catalogs
{
    public interface IOperatorService
    {
        Task<ApiResult<OperatorVm>> Add(CreateEditOperatorRequest request);

        Task<ApiResult<OperatorVm>> GetById(int id);

        Task<ApiResult<OperatorVm>> Update(CreateEditOperatorRequest request);

        Task<ApiResult<bool>> Delete(int id);

        Task<ApiResult<List<OperatorVm>>> GetAll();
    }
}