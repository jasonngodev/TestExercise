namespace TestExercise.ViewModels.Common
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T resultObj)
        {
            IsSuccessed = true;
            Data = resultObj;
            Message = "success";
        }

        public ApiSuccessResult()
        {
            IsSuccessed = true;
            Message = "success";
        }

        public ApiSuccessResult(string message)
        {
            IsSuccessed = false;
            Message = message;
        }
    }
}