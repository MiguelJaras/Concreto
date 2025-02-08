// Decompiled with JetBrains decompiler
// Type: ClasicoConcreto.Bussines.Email
// Assembly: ClasicoConcreto.Bussines, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 738E42E7-E7AA-4142-A88C-A56E90652464
// Assembly location: C:\Users\jsoto\Desktop\Recuperacion Concreto\ClasicoConcreto.Bussines.dll

using ClasicoConcreto.Entity;
using System;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace ClasicoConcreto.Bussines
{
    public class Email
    {
        public bool Send(Entity_Email entEmail)
        {
            bool flag = true;
            try
            {
                string str = "50.97.68.227";
                int num = 587;
                DataTable host = new Parametros().GetHost();
                if (host.Rows.Count > 0)
                {
                    str = host.Rows[0][0].ToString();
                    num = int.Parse(host.Rows[0][1].ToString());
                }
                NetworkCredential networkCredential = new NetworkCredential("SEM@marfil.com", "M1rf3l01");
                MailMessage message = new MailMessage();
                message.From = entEmail.strFrom;
                foreach (MailAddress mailAddress in entEmail.lstTo)
                {
                    message.To.Add(mailAddress);
                }
                foreach (MailAddress mailAddress in entEmail.lstBBC)
                {
                    message.Bcc.Add(mailAddress);
                }
                foreach (MailAddress mailAddress in entEmail.lstCC)
                {
                    message.CC.Add(mailAddress);
                }
                message.Subject = entEmail.strSubject;
                message.Body = entEmail.strContenido;
                message.IsBodyHtml = true;
                foreach (string fileName in entEmail.lstAttachmentPath)
                {
                    message.Attachments.Add(new Attachment(fileName));
                }
                new SmtpClient()
                {
                    Port = num,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Host = str,
                    UseDefaultCredentials = false,
                    Credentials = ((ICredentialsByHost) networkCredential)
                }.Send(message);
                message.Attachments.Dispose();
                message.Dispose();
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }
    }
}
