<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AvaliacaoLeitura.ascx.cs" Inherits="Site.Controles.AvaliacaoLeitura" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<div id="avaliacaoLeituraContainer">
    <div id="tituloAvaliacao">
        <asp:Rating ID="rateReadOnly" runat="server" ReadOnly="true" MaxRating="5" CssClass="ratingStar" StarCssClass="ratingItem"
            WaitingStarCssClass="Saved" FilledStarCssClass="Filled" EmptyStarCssClass="Empty" AutoPostBack="false">
        </asp:Rating>
        <div>
            <asp:Label ID="lblTituloAvaliacao" runat="server"></asp:Label>
        </div>
    </div>
    <div id="depoimentoAvaliacao">
        <asp:Label ID="lblDepoimentoAvaliacao" runat="server"></asp:Label>
    </div>
</div>
