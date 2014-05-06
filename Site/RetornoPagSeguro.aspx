<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RetornoPagSeguro.aspx.cs" Inherits="Site.RetornoPagSeguro" MasterPageFile="~/Master/Site.Master" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <script runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "POST")
            {
                //o método POST indica que a requisição é o retorno da validação NPI.

                string Token = ConfigurationManager.AppSettings["tokenPagSeguro"];
                string Pagina = "https://pagseguro.uol.com.br/pagseguro-ws/checkout/NPI.jhtml";
                string Dados = HttpContext.Current.Request.Form.ToString() + "&Comando=validar" + "&Token=" + Token;

                System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(Pagina);

                req.Method = "POST";
                req.ContentLength = Dados.Length;
                req.ContentType = "application/x-www-form-urlencoded";

                System.IO.StreamWriter stOut = new System.IO.StreamWriter(req.GetRequestStream(), System.Text.Encoding.GetEncoding("ISO-8859-1"));
                stOut.Write(Dados);
                stOut.Close();

                System.IO.StreamReader stIn = new System.IO.StreamReader(req.GetResponse().GetResponseStream(), System.Text.Encoding.GetEncoding("ISO-8859-1"));
                string Result = stIn.ReadToEnd();
                stIn.Close();

                if (Result == "VERIFICADO")
                {
                    //o post foi validado

                    string tipoPagamento = Request.Params["TipoPagamento"];

                    lblTipoPagamento.Text = tipoPagamento;
                }
                else if (Result == "FALSO")
                {
                    //o post nao foi validado
                }
                else
                {
                    //erro na integração com PagSeguro.
                }
            }
            else if (Request.HttpMethod == "GET")
            {
                //o método GET indica que a requisição é o retorno do Checkout PagSeguro para o site vendedor.
                //no término do checkout o usuário é redirecionado para este bloco.

                divRetornoSucesso.Visible = true;

                string tipoPagamento = Request.Params["TipoPagamento"];

                lblTipoPagamento.Text = tipoPagamento;
            }
        }
    
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divRetornoSucesso" runat="server" visible="false">
        <p>Seu pagamento foi concluído com sucesso! </p>
        <p>Uma mensagem com os detalhes desta transação foi enviada para o seu e-mail. </p>
        <p>Você também poderá acessar sua conta PagSeguro no endereço https://pagseguro.uol.com.br/ para mais informações.</p>

        <p>Tipo de Pagamento:
            <asp:Label ID="lblTipoPagamento" runat="server"></asp:Label></p>
    </div>
</asp:Content>
