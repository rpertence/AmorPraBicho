<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="Caes.aspx.cs" Inherits="Site.Caes" %>

<%@ Register Src="~/Controles/Banners.ascx" TagPrefix="uc1" TagName="Banners" %>
<%@ Register Src="~/Controles/Busca.ascx" TagPrefix="uc1" TagName="Busca" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Banners runat="server" ID="Banners" Bicho="Cachorro" Tipo="PaginaDoBicho" />
    <uc1:Busca runat="server" ID="Busca" />
</asp:Content>
