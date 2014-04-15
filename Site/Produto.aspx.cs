using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Actio.Negocio;

namespace Site
{
    public partial class Produto : BasePage
    {
        #region Propriedades
        /// <summary>
        /// Armazena o código do produto exibido na página.
        /// </summary>
        public int? CodigoProduto
        {
            get
            {
                if (ViewState["CodigoProduto"] != null)
                    return Convert.ToInt32(ViewState["CodigoProduto"]);

                return null;
            }
            set
            {
                ViewState["CodigoProduto"] = value;
            }
        }

        /// <summary>
        /// Armazena o código da categoria do produto exibido na página.
        /// </summary>
        public int? CodigoCategoria
        {
            get
            {
                if (ViewState["CodigoCategoria"] != null)
                    return Convert.ToInt32(ViewState["CodigoCategoria"]);

                return null;
            }
            set
            {
                ViewState["CodigoCategoria"] = value;
            }
        }

        /// <summary>
        /// Armazena o código da subcategoria do produto exibido na página.
        /// </summary>
        public int? CodigoSubcategoria
        {
            get
            {
                if (ViewState["CodigoSubcategoria"] != null)
                    return Convert.ToInt32(ViewState["CodigoSubcategoria"]);

                return null;
            }
            set
            {
                ViewState["CodigoSubcategoria"] = value;
            }
        }

        /// <summary>
        /// Armazena o código da marca do produto exibido na página.
        /// </summary>
        public int? CodigoMarca
        {
            get
            {
                if (ViewState["CodigoMarca"] != null)
                    return Convert.ToInt32(ViewState["CodigoMarca"]);

                return null;
            }
            set
            {
                ViewState["CodigoMarca"] = value;
            }
        }

        /// <summary>
        /// Armazena todas as avaliações feitas para o produto.
        /// </summary>
        public DataTable DtAvaliacoes
        {
            get
            {
                if (this.CodigoProduto.HasValue)
                {
                    if (ViewState["DtAvaliacoes"] == null)
                        ViewState["DtAvaliacoes"] = Produtos_Avaliacao.BuscaTodasAvaliacoes(this.CodigoProduto.Value);

                    return (DataTable)ViewState["DtAvaliacoes"];
                }

                return null;
            }
            set
            {
                ViewState["DtAvaliacoes"] = value;
            }
        }
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.MaintainScrollPositionOnPostBack = true;

                string codProduto = Request.QueryString["codigoProduto"];
                int codigoProduto;
                if (!string.IsNullOrEmpty(codProduto) && int.TryParse(codProduto, out codigoProduto))
                {
                    CodigoProduto = codigoProduto;

                    PreencheDadosProduto();
                }
                else
                    mvwProduto.SetActiveView(viewProdutoInexistente);
            }
        }

        protected void rptProdutos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView drFoto = (DataRowView)e.Item.DataItem;
            Image img = (Image)e.Item.FindControl("imgFotoProduto");
            if (img != null)
            {
                img.ImageUrl = string.Format("{0}App_Themes\\ActioAdms\\hd\\produtos\\album\\{1}\\{2}", this.CaminhoADMS, this.CodigoProduto.Value, drFoto["arquivo"]);
            }
        }

        protected void btnSalvarAvaliacao_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Métodos auxiliares
        /// <summary>
        /// Preenche os dados do produto na página, a partir do código.
        /// </summary>
        private void PreencheDadosProduto()
        {
            DataTable dtProduto = Produtos.SelectById(CodigoProduto.Value);
            if (dtProduto != null && dtProduto.Rows.Count > 0)
            {
                #region Dados básicos do produto
                DataRow drDetalhesProduto = dtProduto.Rows[0];

                CodigoCategoria = Convert.ToInt32(drDetalhesProduto["id_categoria"]);
                CodigoSubcategoria = Convert.ToInt32(drDetalhesProduto["id_subcategoria"]);
                CodigoMarca = Convert.ToInt32(drDetalhesProduto["id_marca"]);
                string nomeProduto = drDetalhesProduto["ProdDescricao_"].ToString();
                string resumoProduto = drDetalhesProduto["resumo"].ToString();
                decimal valor = drDetalhesProduto.IsNull("ProdValor_") ? 0 : Convert.ToDecimal(drDetalhesProduto["ProdValor_"]);
                decimal valorParcela = valor / 3;
                #endregion

                #region Preenche campos da página
                lblNomeProduto.Text = nomeProduto;
                lblResumoProduto.Text = resumoProduto;
                lblPreco.Text = valor.ToString("c2", new CultureInfo("pt-BR"));
                lblCondicoesPagto.Text = valorParcela.ToString("c2", new CultureInfo("pt-BR"));
                #endregion

                #region Busca Fotos do Produto
                DataTable dtFotos = Produtos_Fotos.FotosDoProduto(this.CodigoProduto.Value);
                rptFotosProduto.DataSource = dtFotos;
                rptFotosProduto.DataBind();

                if (dtFotos != null && dtFotos.Rows.Count > 0)
                {
                    DataRow drFoto1 = dtFotos.Rows[0];
                    string arquivo = string.Format("{0}App_Themes\\ActioAdms\\hd\\produtos\\album\\{1}\\{2}", this.CaminhoADMS, this.CodigoProduto.Value, drFoto1["arquivo"]);
                    imgFotoAmpliada.ImageUrl = arquivo;
                }
                #endregion

                #region Busca Descrições do Produto
                DataTable dtDescricoes = Produtos_Descricao.SelectByIDProduto(CodigoProduto.Value);
                if (dtDescricoes != null && dtDescricoes.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append("<table cellspacing='10' border='1' style='border-collapse:separate; border-spacing:0 10px;'>");
                    foreach (DataRow dr in dtDescricoes.Rows)
                    {
                        string titulo = dr["titulo"].ToString();
                        string descricao = dr["descricao"].ToString();

                        sb.Append("<tr>");

                        sb.Append(string.Format("<td style='border-bottom: dashed 2px #ECECEC; padding:0 30px 10px 0;'>{0}</td>", titulo));
                        sb.Append(string.Format("<td style='border-bottom: dashed 2px #ECECEC;'>{0}</td>", descricao));

                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");

                    spanConteudoDescricao.InnerHtml = sb.ToString();
                }
                #endregion

                #region Configura exibição de sugestões
                ucSugestoes.CodigoCategoria = this.CodigoCategoria;
                ucMesmaMarca.CodigoMarca = this.CodigoMarca;
                #endregion

                #region Busca avaliações do Produto
                int mediaNotas = Produtos_Avaliacao.BuscaMediaAvaliacoes(this.CodigoProduto.Value);
                rateReadOnly.CurrentRating = ratingCabecalho.CurrentRating = mediaNotas;

                if (DtAvaliacoes != null && DtAvaliacoes.Rows.Count > 0)
                {
                    #region Número de avaliações e Quantidade de estrelas
                    lblNumAvaliacoes.Text = DtAvaliacoes.Rows.Count.ToString();
                    #endregion
                }
                #endregion
            }
            else
            {
                mvwProduto.SetActiveView(viewProdutoInexistente);
            }
        }

        /// <summary>
        /// Retorna a quantidade de avaliações atribuídas com o número de estrelas informado.
        /// </summary>
        /// <param name="numEstrelas">Informar null para retornar a quantidade total de avaliações.</param>
        /// <returns></returns>
        public decimal RetornaQtdeAvaliacoes(int? numEstrelas)
        {
            string consulta = "1 = 1";

            if (numEstrelas.HasValue)
                consulta = string.Format("nota = {0}", numEstrelas.Value);

            return ((DataRow[])DtAvaliacoes.Select(consulta)).Length;
        }
        #endregion
    }
}