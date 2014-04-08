using Actio.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.Controles
{
    public partial class Busca : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void rptCategorias_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Repeater rptSubCategoria = (Repeater)e.Item.FindControl("rptSubCategorias");
            int idCategoria = int.Parse(e.CommandArgument.ToString());

            rptSubCategoria.DataSource = Produtos_Sub_Categoria.SelectByIDCategoria(idCategoria);
            rptSubCategoria.DataBind();

            BuscaProdutosPorCategoria(idCategoria);

            ExibeMensagemResultadoBusca(((LinkButton)e.CommandSource).Text);
        }

        protected void rptSubCategorias_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Repeater rptMarcas = (Repeater)e.Item.FindControl("rptMarcas");
            IdSubCategoria = int.Parse(e.CommandArgument.ToString());

            rptMarcas.DataSource = Marca.SelectBySubCategoria(IdSubCategoria);
            rptMarcas.DataBind();

            BuscaProdutosPorSubCategoria(IdSubCategoria);
            ExibeMensagemResultadoBusca(((LinkButton)e.CommandSource).Text);
        }

        protected void rptMarcas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            BuscaProdutosPorMarca(int.Parse(e.CommandArgument.ToString()), IdSubCategoria);
            ExibeMensagemResultadoBusca(((LinkButton)e.CommandSource).Text);
        }

        private void BuscaProdutosPorCategoria(int idCategoria)
        {
            rptResultado.DataSource = Produtos.SelectByIdCategoria(idCategoria);
            rptResultado.DataBind();
        }

        private void BuscaProdutosPorSubCategoria(int idSubCategoria)
        {
            rptResultado.DataSource = Produtos.SelectByIdSubCategoria(idSubCategoria);
            rptResultado.DataBind();
        }

        private void BuscaProdutosPorMarca(int idMarca, int idSubCategoria)
        {
            rptResultado.DataSource = Produtos.SelectByIdMarca(idMarca, idSubCategoria);
            rptResultado.DataBind();
        }

        private void ExibeMensagemResultadoBusca(string valor)
        {
            int qtde = ((DataTable)rptResultado.DataSource).Rows.Count;

            lblResultadoBusca.Text = string.Format("A busca por \"{0}\" teve {1} resultado{2}.",
                valor,
                qtde,
                qtde == 1 ? "" : "s");
        }

        protected decimal Valor(object item)
        {
            return decimal.Parse(((DataRowView)item)["ProdValor_"].ToString());
        }

        private int IdSubCategoria
        {
            get { return (int)ViewState["IdSubCategoria"]; }
            set { ViewState["IdSubCategoria"] = value; }
        }
    }
}