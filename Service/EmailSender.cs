using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace IdentityNetCore.Service
{
    public class EmailSender : IEmailSender
    {
        private ConfiguracoesEmail _configuracoesEmail;

        public EmailSender(IOptions<ConfiguracoesEmail> configuracoesEmail)
        {
            _configuracoesEmail = configuracoesEmail.Value;
        }

        public async Task EnviarEmail(string email, string assunto, string mensagem)
        {
            string destinatario = string.IsNullOrEmpty(email) ? _configuracoesEmail.Destinatario : email;

            MailMessage mail = new MailMessage
            {
                From = new MailAddress(_configuracoesEmail.Email, "Sistema de Login")
            };

            mail.To.Add(new MailAddress(destinatario));
            mail.Subject = assunto;
            mail.Body = mensagem;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            using (SmtpClient smtpClient = new SmtpClient(_configuracoesEmail.Endereco, _configuracoesEmail.Porta))
            {
                smtpClient.Credentials = new NetworkCredential(_configuracoesEmail.Email, _configuracoesEmail.Senha);
                smtpClient.EnableSsl = true;
                await smtpClient.SendMailAsync(mail);
            }
        }
    }
}