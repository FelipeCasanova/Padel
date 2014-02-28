using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Padel.Domain.Contracts.Tasks;
using Padel.Infrastructure.Utilities.Emails;
using System.Configuration;

namespace Padel.Tasks
{
    public class EmailTasks : IEmailTasks
    {
        public EmailTasks()
        {
        }

        public void EnviarNotificacion(string asunto, string mensaje, string destinatario)
        {
            EnviarEmail.Instance.Send(ConfigurationManager.AppSettings["RemiteEmail"].ToString() , destinatario, asunto, mensaje, null);
        }
    }
}
