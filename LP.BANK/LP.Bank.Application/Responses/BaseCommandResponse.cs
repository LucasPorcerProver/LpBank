using System;
using System.Collections.Generic;
using System.Text;

namespace LP.Bank.Application.Responses
{
    public class BaseCommandResponse
    {
        public int Id { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public List<string>? Errors { get; set; }

        public static BaseCommandResponse ThrowNewErrorResponse(string message, List<string> Errors = null)
        {
            return new BaseCommandResponse()
            {
                Success = false,
                Message = message,
                Errors = Errors,
            };
        }

        public static BaseCommandResponse ThrowNewSuccessResponse(string message)
        {
            return new BaseCommandResponse()
            {
                Success = true,
                Message = message
            };
        }
    }
}
