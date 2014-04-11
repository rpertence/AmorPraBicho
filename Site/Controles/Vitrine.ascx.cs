using Actio.Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.Controles
{
    public partial class Vitrine : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgBicho.ImageUrl = BuscaImagemBicho();
                imgBicho.NavigateUrl = BuscaUrlBicho();

                rptProdutos.DataSource = Produtos.SelectByDestaque(BuscaCategoria(), QtdeProdutos);
                rptProdutos.DataBind();
            }
        }

        private string BuscaUrlBicho()
        {
            switch (Tipo)
            {
                case TipoBicho.Cachorro:
                    return "../Caes.aspx";
                case TipoBicho.Gato:
                    return "../Gatos.aspx";
                case TipoBicho.Passaro:
                    return "../Passaros.aspx";
                case TipoBicho.Roedor:
                    return "../Roedores.aspx";
                default:
                    throw new NotImplementedException("Tipo de bicho não implementado.");
            }
        }

        private string BuscaImagemBicho()
        {
            switch (Tipo)
            {
                case TipoBicho.Cachorro:
                    return "../App_Themes/Padrao/Imagens/para-o-seu-caozinho.png";
                case TipoBicho.Gato:
                    return "../App_Themes/Padrao/Imagens/para-o-seu-gatinho.png";
                case TipoBicho.Passaro:
                    return "../App_Themes/Padrao/Imagens/para-o-seu-passaro.png";
                case TipoBicho.Roedor:
                    return "../App_Themes/Padrao/Imagens/para-o-seu-roedor.png";
                default:
                    throw new NotImplementedException("Tipo de bicho não implementado.");
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