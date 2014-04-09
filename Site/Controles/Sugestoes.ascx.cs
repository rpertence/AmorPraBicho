using System;
using System.Collections.Generic;
using System.Configuration;
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
                rptProdutos.DataSource = Produtos.SelectByDestaque(BuscaCategoria(), QtdeProdutos);
                rptProdutos.DataBind();
            }
        }

        private int BuscaCategoria()
        {
            string nomeChave = string.Format("CodigoCategoria{0}", this.Tipo);
            string valorChave;

            if (string.IsNullOrEmpty(valorChave = ConfigurationManager.AppSettings[nomeChave]))
                throw new Exception(string.Format("A chave '{0}' não foi configurada corretamente", nomeChave));

            int valorChaveDec;
            if (!int.TryParse(valorChave, out valorChaveDec))
                throw new Exception(string.Format("A chave '{0}' não foi configurada corretamente", nomeChave));

            return valorChaveDec;
        }

        public enum TipoBicho
        {
            Cachorro,
            Gato,
            Passaro,
            Roedor
        }

        public TipoBicho Tipo { get; set; }
        public int QtdeProdutos { get; set; }
    }
}