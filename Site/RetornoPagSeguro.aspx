<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RetornoPagSeguro.aspx.cs" Inherits="Site.RetornoPagSeguro" MasterPageFile="~/Master/Site.Master" %>

<%@ Register Assembly="UOL.PagSeguro" Namespace="UOL.PagSeguro" TagPrefix="cc1" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divRetornoSucesso">
        <p style="font-weight: bold; line-height: 50px; font-size: 18pt;">Obrigado por comprar na Pet Shop Amor Pra Bicho!</p>
        <p>Seu pedido foi realizado com sucesso! </p>
        <p>Uma mensagem com os detalhes desta transação foi enviada para o seu e-mail. </p>
        <p>Você também poderá acessar sua conta no PagSeguro para mais informações.</p>
        <p style="line-height: 50px;"><a href="Home.aspx">Clique aqui para retornar à página inicial</a></p>
    </div>
    <%--<cc1:RetornoPagSeguro ID="RetornoPagSeguro1" runat="server"
        OnVendaEfetuada="RetornoPagSeguro1_VendaEfetuada"
        OnVendaNaoAutenticada="RetornoPagSeguro1_VendaNaoAutenticada"
        OnFalhaProcessarRetorno="RetornoPagSeguro1_FalhaProcessarRetorno"
        OnRetornoVerificado="RetornoPagSeguro1_RetornoVerificado"
        UrlNPI="https://pagseguro.uol.com.br/pagseguro-ws/checkout/NPI.jhtml">
    </cc1:RetornoPagSeguro>--%>
</asp:Content>
