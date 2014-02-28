using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Configuration;

namespace Padel.Infrastructure.Utilities.Emails
{

    public class EnviarEmail
    {
        private static EnviarEmail instance = null;

        private EnviarEmail()
        {
        }

        public bool Send(string de, string para, string asunto, string cuerpo, System.Collections.Generic.List<string> adjuntos)
        {
            string[] strArray = para.Split((char[])new char[] { ';' });
            string to = null;
            string cc = null;
            string bcc = null;
            string[] strArray2 = strArray;
            for (int i = 0; i < strArray2.Length; i = (int)(i + 1))
            {
                string str4 = strArray2[i];
                if (str4.Equals(strArray[0]))
                {
                    to = str4;
                }
                else
                {
                    bcc = bcc + str4 + ";";
                }
            }
            return this.Send(de, to, cc, bcc, asunto, cuerpo, adjuntos);
        }

        public bool Send(string de, string to, string cc, string bcc, string asunto, string cuerpo, System.Collections.Generic.List<string> adjuntos)
        {
            string str;
            string[] strArray4;
            int num;
            bool flag = false;
            string[] strArray = (to == null) ? null : to.Split((char[])new char[] { ';' });
            string[] strArray2 = (cc == null) ? null : cc.Split((char[])new char[] { ';' });
            string[] strArray3 = (bcc == null) ? null : bcc.Split((char[])new char[] { ';' });
            if ((((strArray == null) || (strArray.Length <= 0)) && ((strArray2 == null) || (strArray2.Length <= 0))) && ((strArray3 == null) || (strArray3.Length <= 0)))
            {
                return flag;
            }
            MailMessage message = new MailMessage();
            message.From = new MailAddress(de);
            if (strArray != null)
            {
                strArray4 = strArray;
                for (num = 0; num < strArray4.Length; num = (int)(num + 1))
                {
                    str = strArray4[num];
                    message.To.Add(str);
                }
            }
            if (strArray2 != null)
            {
                strArray4 = strArray2;
                for (num = 0; num < strArray4.Length; num = (int)(num + 1))
                {
                    str = strArray4[num];
                    message.CC.Add(str);
                }
            }
            if (strArray3 != null)
            {
                strArray4 = strArray3;
                for (num = 0; num < strArray4.Length; num = (int)(num + 1))
                {
                    str = strArray4[num];
                    message.Bcc.Add(str);
                }
            }
            message.Subject = asunto;
            message.Body = cuerpo;
            message.IsBodyHtml = true;
            message.Priority = MailPriority.Normal;
            message.Sender = new MailAddress(de);
            if (adjuntos != null)
            {
                foreach (string str2 in adjuntos)
                {
                    message.Attachments.Add(new Attachment(str2));
                }
            }
            SmtpClient client = new SmtpClient();
            //client.set_Credentials(new NetworkCredential(ConfigurationManager.AppSettings["SmtpServerUserName"], ConfigurationManager.AppSettings["SmtpServerPassword"]));
            client.EnableSsl = false;
            try
            {
                client.Send(message);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public static EnviarEmail Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EnviarEmail();
                }
                return instance;
            }
        }

    }
}


