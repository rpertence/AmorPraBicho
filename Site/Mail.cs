using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

namespace Site
{
    public class Mail
    {
        public static string EnviarEmail(string nomeRemetente, string emailRemetente, string nomeDestinatario, string emailDestinatario,
            string mensagem, string nomeProduto, string descricaoProduto, string linkProduto)
        {
            MailMessage email = FormataEmail(nomeRemetente, emailRemetente, nomeDestinatario, emailDestinatario, mensagem, nomeProduto, descricaoProduto, linkProduto);

            //cria objeto com os dados do SMTP
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            // descomentar para usar o gmail
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("suporte@actiocubic.com.br", "sw0rdfish");

            string retorno = string.Empty;

            //enviamos o e-mail através do método .Send()
            try
            {
                smtp.Send(email);

                retorno = "sucesso";
            }
            catch
            {
                retorno = "erro";
            }

            //excluímos o objeto de e-mail da memória
            email.Dispose();

            return retorno;
        }

        /// <summary>
        /// Formata o objeto de e-mail com os parâmetros necessários.
        /// </summary>
        /// <returns></returns>
        public static MailMessage FormataEmail(string nomeRemetente, string emailRemetente, string nomeDestinatario, string emailDestinatario,
            string mensagem, string nomeProduto, string descricaoProduto, string linkProduto)
        {
            //cria objeto com dados do e-mail
            MailMessage objEmail = new MailMessage();

            // Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
            objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

            //remetente do e-mail
            objEmail.From = new System.Net.Mail.MailAddress("suporte@actiocubic.com.br", "Pet Shop Amor Pra Bicho");

            //destinatários do e-mail
            objEmail.To.Add(emailDestinatario);

            //prioridade do e-mail
            objEmail.Priority = System.Net.Mail.MailPriority.High;

            //formato do e-mail HTML (caso não queira HTML alocar valor false)
            objEmail.IsBodyHtml = true;

            //título do e-mail
            objEmail.Subject = "Produto indicado para você";

            //responder para
            objEmail.ReplyToList.Add(new MailAddress(emailRemetente));

            //corpo do e-mail
            objEmail.Body = string.Format(@"Olá, {0}.<br />
{1} recomendou este produto para você. Veja a mensagem que ele escreveu:<br /><br />

<div style='background-color:#ECECEC; color:#000;'>
{2}
</div><br /><br />

{3}<br />
{4}<br /><br />

Link para visualizar o produto:
{5}<br /><br />

Atenciosamente,<br /><br />

Atendimento Pet Shop Amor Pra Bicho<br /><br />
<a href='{5}'><img src='http://i59.tinypic.com/2vdmvrl.png' border='0' alt='Pet Shop Amor Pra Bicho' /></a>", nomeDestinatario, nomeRemetente, string.IsNullOrEmpty(mensagem) ? "&nbsp;" : mensagem, 
                                                                                        nomeProduto, descricaoProduto, linkProduto);

            return objEmail;
        }

        bool invalid;
        public bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names. 
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format. 
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,24}))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
    }
}