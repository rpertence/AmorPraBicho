﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="Peixes.aspx.cs" Inherits="Site.Peixes" %>

<%@ Register Src="~/Controles/Banners.ascx" TagPrefix="uc1" TagName="Banners" %>
<%@ Register Src="~/Controles/Busca.ascx" TagPrefix="uc1" TagName="Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Banners runat="server" ID="Banners" Bicho="Peixe" Tipo="PaginaDoBicho" />
    <uc1:Busca runat="server" ID="Busca" />
</asp:Content>
