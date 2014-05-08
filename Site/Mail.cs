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
        /// <summary>
        /// Envia e-mail com indicação de um produto para um amigo.
        /// </summary>
        /// <param name="nomeRemetente"></param>
        /// <param name="emailRemetente"></param>
        /// <param name="nomeDestinatario"></param>
        /// <param name="emailDestinatario"></param>
        /// <param name="mensagem"></param>
        /// <param name="nomeProduto"></param>
        /// <param name="descricaoProduto"></param>
        /// <param name="linkProduto"></param>
        /// <returns></returns>
        public static string EnviarEmail(string nomeRemetente, string emailRemetente, string nomeDestinatario, string emailDestinatario,
            string mensagem, string nomeProduto, string descricaoProduto, string linkProduto)
        {
            MailMessage email = FormataEmailIndicacaoProduto(nomeRemetente, emailRemetente, nomeDestinatario, emailDestinatario, mensagem, nomeProduto, descricaoProduto, linkProduto);

            return EnviarEmail(email);
        }

        /// <summary>
        /// Envia e-mail de notificação quando ocorre uma movimentação do pagseguro.
        /// </summary>
        /// <param name="nomeCliente"></param>
        /// <param name="emailCliente"></param>
        /// <param name="telefoneCliente"></param>
        /// <param name="statusTransacao"></param>
        /// <param name="codigoTransacao"></param>
        /// <param name="dataTransacao"></param>
        /// <param name="numItens"></param>
        /// <param name="observacao"></param>
        /// <returns></returns>
        public static string EnviarEmail(string nomeCliente, string emailCliente, string telefoneCliente, string statusTransacao, string codigoTransacao,
                                                                DateTime dataTransacao, int numItens, string observacao, string emailVendedor, TipoNotificacao tipoNotificacao)
        {
            MailMessage email = FormataEmailNotificacao(nomeCliente, emailCliente, telefoneCliente, statusTransacao, codigoTransacao, dataTransacao, numItens, observacao, emailVendedor, tipoNotificacao);

            return EnviarEmail(email);
        }

        /// <summary>
        /// Envia mensagem de e-mail já formatada.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private static string EnviarEmail(MailMessage email)
        {
            //cria objeto com os dados do SMTP
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            // descomentar para usar o gmail
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("suporte@actiocubic.com.br", "sw0rdfish");

            try
            {
                //enviamos o e-mail através do método .Send()
                smtp.Send(email);

                //excluímos o objeto de e-mail da memória
                email.Dispose();

                return "sucesso";
            }
            catch
            {
                throw;
            }
        }

        private static MailMessage FormataEmailNotificacao(string nomeCliente, string emailCliente, string telefoneCliente, string statusTransacao, string codigoTransacao,
                                                                DateTime dataTransacao, int numItens, string observacao, string emailVendedor, TipoNotificacao tipoNotificacao)
        {
            //cria objeto com dados do e-mail
            MailMessage objEmail = new MailMessage();

            // Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
            objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

            //remetente do e-mail
            objEmail.From = new System.Net.Mail.MailAddress("suporte@actiocubic.com.br", "Contato - Loja Virtual Actio");

            //destinatários do e-mail
            objEmail.To.Add(new MailAddress(emailVendedor));
            objEmail.Bcc.Add(new MailAddress("contato@actio.net.br", "Leo"));

            //prioridade do e-mail
            objEmail.Priority = System.Net.Mail.MailPriority.High;

            //formato do e-mail HTML (caso não queira HTML alocar valor false)
            objEmail.IsBodyHtml = true;

            //título do e-mail
            objEmail.Subject = string.Format("Pet Shop Amor Pra Bicho - Aviso de {0} no PagSeguro - Pedido: {1}",
                tipoNotificacao == TipoNotificacao.Movimentacao ? "Movimentação" : tipoNotificacao == TipoNotificacao.PostNaoValidado ? "Post Não Validado" : "Erro na Integração",
                codigoTransacao);

            //responder para
            objEmail.ReplyToList.Add(new MailAddress(emailCliente));

            //corpo do e-mail
            objEmail.Body = string.Format(@"<br>Movimentação realizada pelo PagSeguro
                            <br><br>Cliente: {0}
                            <br><br>Email do Cliente: {1}
                            <br><br>Telefone do Cliente: {2}
                            <br><br><EM><FONT color=#ff0000 size=5>Status do Pedido: {3}</FONT></EM>
                            <br><br>Código da Transação PagSeguro: {4}
                            <br><br>Data da Transação: {5}
                            <br><br>Quantidade de Itens: {6}
                            <br><br><span style='font-size:20pt; color:red;'>OBSERVAÇÕES:</span>
                            <br><div style='background-color:#ECECEC; color:#000;'>{7}</div>
                            <br><br>Você pode verificar o status deste pedido no PagSeguro. 
                            <br><br>Caso queira fazer contato com o cliente clique em responder esta mensagem.",
                            nomeCliente, emailCliente, telefoneCliente, statusTransacao, codigoTransacao, dataTransacao.ToString("dd/MM/yyyy HH:mm:ss"), numItens, observacao);

            return objEmail;
        }

        /// <summary>
        /// Formata o objeto de e-mail com os parâmetros necessários.
        /// </summary>
        /// <returns></returns>
        private static MailMessage FormataEmailIndicacaoProduto(string nomeRemetente, string emailRemetente, string nomeDestinatario, string emailDestinatario,
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

<span style='font-weight:bold;font-size:14pt;'>{3}</span><br />
{4}<br /><br />

Link para visualizar o produto:<br />
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

        public enum TipoNotificacao
        {
            Movimentacao,
            PostNaoValidado,
            ErroIntegracaoPagSeguro
        }
    }
}