using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EmailService
    {
        private readonly string emailRemitente;
        private readonly string contrasenia;
        public EmailService()
        {
            emailRemitente = "programacionIII@outlook.com";
            contrasenia = "contraseniasegura123";
        }

        public async Task<bool> EnviarMail(string destinatario, string asunto, string mensaje)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp-mail.outlook.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(emailRemitente, contrasenia),
                    EnableSsl = true,
                };

                var mensajeEmail = new MailMessage
                {
                    From = new MailAddress(emailRemitente),
                    Subject = asunto,
                    Body = mensaje,
                    IsBodyHtml = true
                };
                mensajeEmail.To.Add(destinatario);

                await smtpClient.SendMailAsync(mensajeEmail);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                
            }
        }
        public void EnviarEmailRegistroExitoso(string destinatario)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("noresponder.Programacion3@MaxiPrograma.com", "Maxi Programa");
                mail.To.Add(destinatario);
                mail.Subject = "Registro Exitoso";
                mail.Body = "Hola, tu registro ha sido exitoso. ¡Bienvenido!";

                using (SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(emailRemitente, contrasenia);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}
