<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" ValidateRequest="false" AutoEventWireup="True" CodeBehind="Default.aspx.cs" Inherits="DuvidasFrequentes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAdms" runat="Server">
    <asp:MultiView ID="mvProdutos" runat="server">
        <asp:View ID="vList" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="padding: 5px" align="char">
                        <asp:Image ID="ImageRecurso" runat="server" ImageAlign="Middle"
                            ImageUrl="~/LojaVirtual/imagens/OK.png" Width="35px" />
                        <asp:Label ID="Label3" runat="server" Text="Listagem"></asp:Label>
                        <asp:ObjectDataSource ID="odsDuvidas" runat="server" DeleteMethod="Excluir"
                            SelectMethod="SelectAll" TypeName="Actio.Negocio.DuvidaFrequente">
                            <DeleteParameters>
                                <asp:Parameter Name="id" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td align="char" style="padding: 5px">
                        <asp:Button ID="bt_NovaDuvida" runat="server" CausesValidation="False"
                            OnClick="bt_NovaDuvida_Click" Text="+ nova dúvida" />
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding: 5px" valign="top" width="100%">
                        <asp:GridView ID="GridDuvidas" runat="server" AllowPaging="True"
                            AllowSorting="True" AutoGenerateColumns="False" DataSourceID="odsDuvidas"
                            EnableModelValidation="True" OnRowCommand="ListDuvidasRowCommand" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="dúvida" SortExpression="Pergunta">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server"
                                            CommandArgument='<%# Bind("id") %>' CommandName="Alterar"
                                            CssClass="linksPretos" Text='<%# Bind("Pergunta") %>'
                                            ToolTip="Alterar ou atualizar esta dúvida"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Resposta" HeaderText="Resposta"
                                    SortExpression="Resposta">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibt_excluir0" runat="server"
                                            AlternateText="Excluir este item" CausesValidation="False"
                                            CommandArgument='<%# Bind("id") %>' CommandName="Excluir" ImageAlign="Middle"
                                            ImageUrl="~/App_Themes/ActioAdms/botoes/excluir.png"
                                            OnClientClick="if (confirm('você está certo que vai excluir? Exclusões não podem ser desfeitas!') == false) return false;" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Image ID="Image7" runat="server" ImageAlign="Middle"
                                    ImageUrl="~/LojaVirtual/imagens/vazio.png" Width="38px" />
                                Não há dúvidas cadastradas
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vEdit" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td width="150px" style="padding: 5px" align="right">
                        <asp:Image ID="Image1" runat="server" ImageAlign="Middle"
                            ImageUrl="~/LojaVirtual/imagens/detalhe.png" Width="35px" />
                        &nbsp;</td>
                    <td style="padding: 5px">Edição</td>
                </tr>
                <tr>
                    <td align="right" style="padding: 5px" width="150px" valign="top">
                        <asp:ImageButton ID="ibt_listagemDuvidas" runat="server"
                            CausesValidation="False" Height="25px" ImageAlign="Middle"
                            ImageUrl="~/App_Themes/ActioAdms/botoes/retornar.png"
                            ToolTip="Voltar para listagem de Dúvidas"
                            OnClick="ibt_listagemDuvidas_Click" />
                    </td>
                    <td style="padding: 5px">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="right" style="padding: 5px" valign="middle" width="100px">&nbsp;</td>
                                <td align="center" style="padding: 5px" valign="middle">

                                    <asp:ImageButton ID="Atualizar" runat="server" OnClick="Atualizar_Click" SkinID="ibt_EditarItem" />
                                    <asp:ImageButton ID="Salvar" runat="server" OnClick="Salvar_Click" SkinID="ibt_SalvarItem" />

                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding: 5px" valign="middle" width="100px">pergunta:</td>
                                <td align="left" style="padding: 5px" valign="middle">
                                    <asp:TextBox ID="txtPergunta" runat="server" MaxLength="100" Width="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server"
                                        ControlToValidate="txtPergunta" ErrorMessage="pergunta!"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding: 5px" valign="middle" width="100px">resposta:</td>
                                <td align="left" style="padding: 5px" valign="middle">
                                    <asp:TextBox ID="txtResposta" runat="server" MaxLength="100" Rows="4"
                                        TextMode="MultiLine" Width="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server"
                                        ControlToValidate="txtResposta" ErrorMessage="breve descrição"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>

