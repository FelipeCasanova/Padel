using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Padel.Tasks.CommandResults
{
    public class CommandResult
    {
        public bool Success { get; set; }

        public CommandResult(bool success)
        {
            this.Success = success;
        }
    }
}
