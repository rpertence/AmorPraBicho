<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RetornoPagSeguro.aspx.cs" Inherits="Site.RetornoPagSeguro" MasterPageFile="~/Master/Site.Master" %>

<%@ Register Assembly="UOL.PagSeguro" Namespace="UOL.PagSeguro" TagPrefix="cc1" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <%--<script runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "POST")
            {
                //o m�todo POST indica que a requisi��o � o retorno da valida��o NPI.
                
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
                    
                    //Recupera valores enviados pelo pagseguro
                    //string idTransacao = Request.Params["TransacaoID"];
                    //string tipoPagamento = Request.Params["TipoPagamento"];
                    //string statusTransacao = Request.Params["StatusTransacao"];
                    //string tipoFrete = Request.Params["TipoFrete"];
                    //string valorFrete = Request.Params["ValorFrete"];
                    //string anotacao = Request.Params["Anotacao"];
                    //string emailCliente = Request.Params["CliEmail"];
                    //string numItens = Request.Params["NumItens"];
                    
                    //Insere registro na tabela 'produtos_vendas'
                    //Actio.Negocio.Produtos_Vendas.Inserir(null, idTransacao, tipoFrete, statusTransacao, tipoPagamento, valorFrete, anotacao, emailCliente, numItens);
                    
                    //Recupera produtos vendidos e debita do estoque
                    //if (Request.Params.AllKeys.Contains("ProdID_"))
                    //{
                    //    foreach (var pair in Request.Params.AllKeys.)
                    //    {
                            
                    //    }
                    //}
                }
                else if (Result == "FALSO")
                {
                    //o post nao foi validado
                }
                else
                {
                    //erro na integra��o com PagSeguro.
                }
            }
            else if (Request.HttpMethod == "GET")
            {
                //o m�todo GET indica que a requisi��o � o retorno do Checkout PagSeguro para o site vendedor.
                //no t�rmino do checkout o usu�rio � redirecionado para este bloco.

                divRetornoSucesso.Visible = true;

                string tipoPagamento = Request.Params["TipoPagamento"];

                
            }
        }
    
    </script>--%>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divRetornoSucesso">
        <p style="font-weight: bold; line-height: 50px; font-size: 18pt;">Obrigado por comprar na Pet Shop Amor Pra Bicho!</p>
        <p>Seu pedido foi realizado com sucesso! </p>
        <p>Uma mensagem com os detalhes desta transa��o foi enviada para o seu e-mail. </p>
        <p>Voc� tamb�m poder� acessar sua conta no PagSeguro para mais informa��es.</p>
        <p style="line-height: 50px;"><a href="Home.aspx">Clique aqui para retornar � p�gina inicial</a></p>
    </div>
    <cc1:RetornoPagSeguro ID="RetornoPagSeguro1" runat="server"
        OnVendaEfetuada="RetornoPagSeguro1_VendaEfetuada"
        OnVendaNaoAutenticada="RetornoPagSeguro1_VendaNaoAutenticada"
        OnFalhaProcessarRetorno="RetornoPagSeguro1_FalhaProcessarRetorno"
        OnRetornoVerificado="RetornoPagSeguro1_RetornoVerificado"
        UrlNPI="https://pagseguro.uol.com.br/pagseguro-ws/checkout/NPI.jhtml">
    </cc1:RetornoPagSeguro>
</asp:Content>
