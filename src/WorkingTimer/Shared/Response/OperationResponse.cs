using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingTimer.Shared.Response
{
    public class ApiBaseResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }

    }

    public class OperationResponse<T> : ApiBaseResponse
    {

        public OperationResponse()
        {
            OperationDate = DateTime.UtcNow;
        }

        public T Record { get; set; }
        public DateTime OperationDate { get; set; }
    }

    public class OperationResponse : ApiBaseResponse
    {

    }
}
