using Actio.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MasterPage = Site.Master.Site;

namespace Site.Controles
{
    public partial class Busca : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string p;
                if (!string.IsNullOrEmpty(p = Request.QueryString["p"]))
                {
                    Resultado = Produtos.SelectByPesquisa(p);

                    BindResultados();

                    ((MasterPage)this.Page.Master).AtualizaCampoPesquisa(p);
                    ExibeMensagemResultadoBusca(p);
                }
            }
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

            BindResultados();
        }

        private void BuscaProdutosPorSubCategoria(int idSubCategoria)
        {
            Resultado = Produtos.SelectByIdSubCategoria(idSubCategoria);

            BindResultados();
        }

        private void BuscaProdutosPorMarca(int idMarca, int idSubCategoria)
        {
            Resultado = Produtos.SelectByIdMarca(idMarca, idSubCategoria);

            BindResultados();
        }

        private void BindResultados()
        {
            AplicaOrdenacao();

            rptResultado.DataSource = Resultado;
            rptResultado.DataBind();
        }

        private void ExibeMensagemResultadoBusca(string valor)
        {
            int qtde = Resultado.Rows.Count;

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
            BindResultados();
        }

        private void AplicaOrdenacao()
        {
            if (Resultado.Rows.Count > 0)
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
            }
        }
    }
}