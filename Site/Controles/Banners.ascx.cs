using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.Controles
{
    public partial class Banners : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public TipoBanner Tipo { get; set; }
        public TipoBicho Bicho { get; set; }

        public enum TipoBanner
        {
            Principal,
            PaginaDoBicho
        }

        public enum TipoBicho
        {
            Cachorro,
            Gato,
            Passaro,
            Peixe,
            Roedor
        }

        protected void odsBanner_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (Tipo == TipoBanner.PaginaDoBicho)
                e.InputParameters["categoria"] = BuscaCategoria();
        }

        private int BuscaCategoria()
        {
            string nomeChave = string.Format("CodigoCategoria{0}", this.Bicho);
            string valorChave;

            if (string.IsNullOrEmpty(valorChave = ConfigurationManager.AppSettings[nomeChave]))
                throw new Exception(string.Format("A chave '{0}' não foi configurada corretamente", nomeChave));

            int valorChaveDec;
            if (!int.TryParse(valorChave, out valorChaveDec))
                throw new Exception(string.Format("A chave '{0}' não foi configurada corretamente", nomeChave));

            return valorChaveDec;
        }
    }
}