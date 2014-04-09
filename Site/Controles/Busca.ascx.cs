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
            Resultado = Produtos.SelectByIdCategoria(idCategoria);

            rptResultado.DataSource = Resultado;
            rptResultado.DataBind();
        }

        private void BuscaProdutosPorSubCategoria(int idSubCategoria)
        {
            Resultado = Produtos.SelectByIdSubCategoria(idSubCategoria);

            rptResultado.DataSource = Resultado;
            rptResultado.DataBind();
        }

        private void BuscaProdutosPorMarca(int idMarca, int idSubCategoria)
        {
            Resultado = Produtos.SelectByIdMarca(idMarca, idSubCategoria);

            rptResultado.DataSource = Resultado;
            rptResultado.DataBind();
        }

        private void ExibeMensagemResultadoBusca(string valor)
        {
            int qtde = ((DataTable)rptResultado.DataSource).Rows.Count;

            lblResultadoBusca.Text = string.Format("A busca por <span style='color: rgb(117, 108, 108);'>\"{0}\"</span> teve <span style='color: rgb(117, 108, 108);'>{1}</span> resultado{2}.",
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

        private DataTable Resultado
        {
            get { return (DataTable)ViewState["Resultado"]; }
            set { ViewState["Resultado"] = value; }
        }

        protected void ddlOrdenacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlOrdenacao.SelectedIndex)
            {
                case 0:
                    Resultado = Resultado.AsEnumerable().OrderBy(o => decimal.Parse(o["ProdValor_"].ToString())).CopyToDataTable();
                    break;
                case 1:
                    Resultado = Resultado.AsEnumerable().OrderByDescending(o => decimal.Parse(o["ProdValor_"].ToString())).CopyToDataTable();
                    break;
                case 2:
                    throw new NotImplementedException("Ordenação por 'mais vendidos' ainda não foi implementada.");
                case 3:
                    throw new NotImplementedException("Ordenação por 'melhores avaliados' ainda não foi implementada.");
            }

            rptResultado.DataSource = Resultado;
            rptResultado.DataBind();
        }
    }
}