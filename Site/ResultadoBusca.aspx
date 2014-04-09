<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="ResultadoBusca.aspx.cs" Inherits="Site.ResultadoBusca" %>

<%@ Register Src="~/Controles/Busca.ascx" TagPrefix="uc1" TagName="Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Busca runat="server" id="Busca" />
</asp:Content>
