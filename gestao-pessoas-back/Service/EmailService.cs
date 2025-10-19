using System.Net.Mail;
using System.Net;
using gestao_pessoas_back.Exceptions;

namespace gestao_pessoas_back.Service
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> EnviarEmailAsync(
             string destinatario,
             string assunto,
             string corpo
        )
        {
            string remetente = _configuration["EmailSettings:Usuario"];
            string smtpAddress = _configuration["EmailSettings:SmtpServer"];
            int portNumber = 587;
            bool enableSSL = true;
            string emailPassword = _configuration["EmailSettings:Senha"];

            try
            {
                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress(remetente);

                    // destinatário principal (pode ser vários separados por ; ou ,)
                    AddAddresses(mail.To, destinatario);


                    mail.Subject = assunto;
                    mail.Body = corpo;
                    mail.IsBodyHtml = true;

                    // Anexos: mantenha streams vivos até o envio terminar
                    var streams = new List<System.IO.MemoryStream>();
                    try
                    {
                        using (var smtp = new SmtpClient(smtpAddress, portNumber))
                        {
                            smtp.Credentials = new NetworkCredential(remetente, emailPassword);
                            smtp.EnableSsl = enableSSL;
                            await smtp.SendMailAsync(mail);
                        }
                    }
                    finally
                    {
                        // fecha streams manualmente (Attachment.Dispose será chamado ao finalizar mail)
                        foreach (var s in streams)
                            s.Dispose();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                var mensagemErro = ex.ToString();
                _logger.LogError($"Erro ao enviar e-mail: {mensagemErro}");
                throw new BusinessException("Não foi possível enviar o e-mail");
            }

            // local function para adicionar múltiplos endereços
            void AddAddresses(MailAddressCollection collection, string addresses)
            {
                if (string.IsNullOrWhiteSpace(addresses)) return;

                // aceita formatos: "a@x.com", "a@x.com;b@y.com", "a@x.com, b@y.com"
                var separators = new[] { ',', ';' };
                var parts = addresses.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                foreach (var part in parts.Select(p => p.Trim()))
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(part))
                            collection.Add(new MailAddress(part));
                    }
                    catch (FormatException fx)
                    {
                        _logger.LogWarning($"Endereço de email inválido ignorado: {part}. Erro: {fx.Message}");
                    }
                }
            }
        }

    }
}
