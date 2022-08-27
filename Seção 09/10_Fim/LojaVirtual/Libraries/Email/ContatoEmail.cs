using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Email
{
    public class ContatoEmail
    {
        public static void EnviarContatoPorEmail(Contato con)
        {
            /*
             * SMTP - serviço de envio
             */
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential("lucaoxt@gmail.com", "");
            smtpClient.EnableSsl = true;

            string bodyMsg = string.Format(@"
                <h2>Contato - LojaVirtual</h2> <br />
                <b>Nome:</b> {0} <br />
                <b>E-mail:</b> {1} <br />
                <b>Texto:</b> {2} <br />
                <br />
                E-mail enviado automaticamente do site LojaVirtual.",
                con.Nome, con.Email, con.Texto);

            /*
             * MailMessage - Constrói a mensagem
             */
            MailMessage message = new MailMessage();
            message.From = new MailAddress("lucaoxt@gmail.com");
            message.To.Add("lucaoxt@gmail.com");
            message.Subject = "Contato - LojaVirtual - Email: " + con.Email;
            message.IsBodyHtml = true;
            message.Body = bodyMsg;

            smtpClient.Send(message);
        }
    }
}
