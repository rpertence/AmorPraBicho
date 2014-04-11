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
            }
            else
            {
                mvwProduto.SetActiveView(viewProdutoInexistente);
            }
        }

        public int CalculaMediaNotas()
        {
            return 3;
        }
        #endregion

    }
}