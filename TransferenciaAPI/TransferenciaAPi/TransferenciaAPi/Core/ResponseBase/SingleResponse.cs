using Microsoft.AspNetCore.Mvc;

namespace TransferenciaAPi.Core.ResponseBase
{
    public class SingleResponse
    {
        protected IActionResult SuccessResponse<T>(T data)
        {
            return new OkObjectResult(data);
        }

        protected IActionResult SuccessResponse()
        {
            return new OkResult();
        }

        protected IActionResult ErrorResponse(string message)
        {
            return new BadRequestObjectResult(new ErrorResponse(message));
        }
    }

    public class ErrorResponse
    {
        public ErrorResponse()
        {

        }

        public ErrorResponse(string msg)
        {
            this.message = msg;
        }

        public string message { get; set; }
    }
}