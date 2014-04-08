using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.Controles
{
    public partial class Produto : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string ImageURL { get; set; }
        public string NomeProduto { get; set; }
        public decimal ValorProduto { get; set; }
    }
}