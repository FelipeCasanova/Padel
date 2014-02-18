using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Padel.Tasks.CommandResults
{
    public class CommandResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }

        public CommandResult(bool success, string message, object result = null)
        {
            this.Success = success;
            this.Message = message;
            this.Result = result;
        }
    }
}
