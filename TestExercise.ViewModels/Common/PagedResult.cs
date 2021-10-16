using System.Collections.Generic;

namespace TestExercise.ViewModels.Common
{
    public class PagedResult<T> : PagedResultBase
    {
        public List<T> Data { set; get; }
    }
}