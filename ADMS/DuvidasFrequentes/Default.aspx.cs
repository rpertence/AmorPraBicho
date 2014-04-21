using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Net;
using System.Net.Mail;
using System.Text;
using Actio.Negocio;

public partial class DuvidasFrequentes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        Credencial();
    }
    
    private void PadraoDoEnter(ImageButton botao)
    {
        this.Page.Form.DefaultButton = botao.UniqueID;
    }
    
    public void Credencial()
    {
        try
        {
            Usuario usuarioLogado = new Usuario(int.Parse(Page.User.Identity.Name));
            int Tipo = int.Parse(usuarioLogado.Tipo);

            if (Tipo == 2)
            {
                Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                LabelTituloPagina.Text = "Administração da Loja Virtual";
                mvProdutos.ActiveViewIndex = 0;
            }
            if (Tipo == 1)
            {
                try
                {
                    DataTable dt = Usuario_Recursos.SelectByIdRecursoIdUsuario("18", Page.User.Identity.Name);
                    DataRow dr = dt.Rows[0];
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração da Loja Virtual";
                    mvProdutos.ActiveViewIndex = 0;
                }
                catch
                {
                    mvProdutos.ActiveViewIndex = -1;
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de Artigos e Autores - <STRONG>Sua credencial não permite a utilização deste painel!</STRONG><br /> Caso tenha interesse em adquir esta ferramenta para seu site<br />Entre em contato com a Actio Comunicação:<br />3317-0794 | 88226710 - contato@actio.net.br";
                }
            }
            if (Tipo == 0)
            {
                mvProdutos.ActiveViewIndex = -1;
                Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                LabelTituloPagina.Text = "Administração da Loja Virtual  - <STRONG>Sua credencial não permite a utilização deste painel!</STRONG><br /> Caso tenha interesse em adquir esta ferramenta para seu site<br />Entre em contato com a Actio Comunicação:<br />3317-0794 | 88226710 - contato@actio.net.br";
            }
        }
        catch
        {
            Response.Redirect("~/");
        }
    }
    
    protected void ibt_listagemDuvidas_Click(object sender, ImageClickEventArgs e)
    {
        mvProdutos.ActiveViewIndex = 0;
        GridDuvidas.DataBind();
    }
    
    //protected void lk_List_Click(object sender, EventArgs e)
    //{
    //    mvProdutos.ActiveViewIndex = 0;
    //    GridProdutos.DataBind();
    //    LabelTitulo.Text = "Listagem de Produtos";
    //}
    
    protected void bt_NovaDuvida_Click(object sender, EventArgs e)
    {
        mvProdutos.ActiveViewIndex = 1;
        Salvar.Visible = true;
        Atualizar.Visible = false;
        
        txtPergunta.Text = txtResposta.Text = string.Empty;
    }
    
    protected void Salvar_Click(object sender, ImageClickEventArgs e)
    {
        DuvidaFrequente.Novo(txtPergunta.Text, txtResposta.Text);
        
        mvProdutos.ActiveViewIndex = 0;
        GridDuvidas.DataBind();
        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item inserido com sucesso!');window.location.src = window.location.src;", true);
    }
    
    public void ListDuvidasRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int id = int.Parse(e.CommandArgument.ToString());
            Session["id"] = id;
            if (e.CommandName == "Alterar")
                CarregaDuvida();
            if (e.CommandName == "Excluir")
                DeletaDuvida();
        }
        catch { }
    }

    public void CarregaDuvida()
    {
        Salvar.Visible = false;
        Atualizar.Visible = true;

        DataTable dt = DuvidaFrequente.SelectById(int.Parse(Session["id"].ToString()));
        DataRow dr = dt.Rows[0];

        txtPergunta.Text = dr["Pergunta"].ToString();
        txtResposta.Text = dr["Resposta"].ToString();

        mvProdutos.ActiveViewIndex = 1;
    }
    
    protected void Atualizar_Click(object sender, ImageClickEventArgs e)
    {
        DuvidaFrequente.Update(int.Parse(Session["id"].ToString()), txtPergunta.Text, txtResposta.Text);
        
        mvProdutos.ActiveViewIndex = 0;
        GridDuvidas.DataBind();
        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item atualizado com sucesso!');window.location.src = window.location.src;", true);
    }

    public void DeletaDuvida()
    {
        DuvidaFrequente.Excluir(int.Parse(Session["id"].ToString()));
        
        mvProdutos.ActiveViewIndex = 0;
        GridDuvidas.DataBind();
        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item excluido com sucesso!');window.location.src = window.location.src;", true);
    }
}