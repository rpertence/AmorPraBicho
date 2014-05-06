<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeituraAvaliacao.ascx.cs" Inherits="Site.Controles.LeituraAvaliacao" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<div id="avaliacaoLeituraContainer">
    <div id="tituloAvaliacao" style="margin-bottom: 10px;">
        <div style="float: left; margin: 5px 10px 0 0;">
            <asp:Rating ID="rateReadOnly" runat="server" ReadOnly="true" MaxRating="5" CssClass="ratingStar" StarCssClass="ratingItem"
                WaitingStarCssClass="Saved" FilledStarCssClass="Filled" EmptyStarCssClass="Empty" AutoPostBack="false">
            </asp:Rating>
        </div>
        <div style="color: #36A9E1; font-size: 15pt; font-weight: bold;">
            <span><%= this.TituloAvaliacao %></span>
        </div>
    </div>
    <div id="nomeUsuarioAvaliacao" style="margin-bottom: 20px;">
        <span style="font-weight: bold;">Por: </span><span><%= this.NomeUsuario %> em <%= this.DataAvaliacao.ToString("dd/MM/yyyy") %></span>
    </div>
    <div id="depoimentoAvaliacao">
        <span><%= this.Depoimento %></span>
    </div>
</div>
