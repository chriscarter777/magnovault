using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace magnovault.Utilities
{
     public class Emailer
     {
          public void SendMail(string host, int port, string password, string from, string to, string subject, string content)
          {
               using (SmtpClient mailClient = new SmtpClient
               {
                    Host = host,
                    Port  = port,
                    EnableSsl = true,
                    Timeout = 100000,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(from, password)
          }
               )
               {
                    MailMessage msg = new MailMessage(from, to, subject, content);
                    msg.IsBodyHtml = true;
                    mailClient.Send(msg);
               }
          }
     }
}
