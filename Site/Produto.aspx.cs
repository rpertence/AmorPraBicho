using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site
{
    public partial class Produto : BasePage
    {
        public int CodigoProduto { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.MaintainScrollPositionOnPostBack = true;

                int codigoProduto;
                if (!string.IsNullOrEmpty(Request.QueryString["codigoProduto"]) && int.TryParse(Request.QueryString["codigoProduto"], out codigoProduto))
                {
                    CodigoProduto = codigoProduto;

                    PreencheDadosProduto();
                }
            }
        }

        /// <summary>
        /// Preenche os dados do produto na página, a partir do código.
        /// </summary>
        private void PreencheDadosProduto()
        {
            //DataTable
        }

        public int CalculaMediaNotas()
        {
            return 3;
        }

        protected void btnSalvarAvaliacao_Click(object sender, EventArgs e)
        {
            
        }
    }
}