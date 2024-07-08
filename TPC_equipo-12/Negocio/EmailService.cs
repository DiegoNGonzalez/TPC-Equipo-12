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
        private SmtpClient server;
        private MailMessage email;
        
        public EmailService()
        {
            server = new SmtpClient();
            server.Credentials = new NetworkCredential("diegonzalezdev@gmail.com", "qsxdogtxdkkselps");
            server.EnableSsl = true;
            server.Port = 587;
            server.Host = "smtp.gmail.com";
        }
        public void EnviarEmail(string destinatario, string asunto, string mensaje)
        {
            email = new MailMessage();
            email.From = new MailAddress("noresponder@cursosdiegonzalezdev.com");
            email.To.Add(destinatario);
            email.Subject = asunto;
            email.IsBodyHtml = true;
            email.Body = mensaje;
            try
            {
                server.Send(email);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void EnviarEmailConToken(string destinatario, string token)
        {
            string asunto = "Restablecer Contraseña";
            string mensaje = $"Haga clic aquí para restablecer su contraseña: <a href='http://localhost:44344/CambioContrasenia.aspx?token={token}'>Cambio de contraseña</a> <br> Si no fue usted el que solicito el cambio, contactese con su profesor";
            EnviarEmail(destinatario, asunto, mensaje);
        }
    }
    
}
