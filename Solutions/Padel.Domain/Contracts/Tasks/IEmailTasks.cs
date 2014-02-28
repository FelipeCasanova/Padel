using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Padel.Domain.Contracts.Tasks
{
    public interface IEmailTasks
    {
        void EnviarNotificacion(string asunto, string mensaje, string destinatario);
    }
}
