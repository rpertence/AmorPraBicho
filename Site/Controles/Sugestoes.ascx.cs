using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Actio.Negocio;

namespace Site.Controles
{
    public partial class Sugestoes : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = null;

                switch (this.Tipo)
                {
                    case TipoSugestao.MesmaCategoria:
                        if (this.CodigoCategoria.HasValue)
                            dt = Produtos.SelectByDestaque(this.CodigoCategoria.Value, this.QtdeProdutos);
                        break;

                    case TipoSugestao.MesmaMarca:
                        if (this.CodigoMarca.HasValue)
                            dt = Produtos.SelectByDestaqueMarca(this.CodigoMarca.Value, this.QtdeProdutos);
                        break;

                    case TipoSugestao.MesmaSubcategoria:
                        if (this.CodigoSubcategoria.HasValue)
                            dt = Produtos.SelectByDestaqueSubcategoria(this.CodigoSubcategoria.Value, this.QtdeProdutos);
                        break;

                    default:
                        if (this.CodigoCategoria.HasValue)
                            dt = Produtos.SelectByDestaque(this.CodigoCategoria.Value, this.QtdeProdutos);
                        break;
                }

                rptProdutos.DataSource = dt;
                rptProdutos.DataBind();
            }
        }

        public enum TipoSugestao
        {
            MesmaCategoria,
            MesmaSubcategoria,
            MesmaMarca
        }

        public TipoSugestao Tipo { get; set; }
        public int QtdeProdutos { get; set; }
        public int? CodigoCategoria { get; set; }
        public int? CodigoSubcategoria { get; set; }
        public int? CodigoMarca { get; set; }
    }
}