using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.Controles
{
    public partial class LeituraAvaliacao : System.Web.UI.UserControl
    {
        public int Nota { get; set; }
        public string TituloAvaliacao { get; set; }
        public string NomeUsuario { get; set; }
        public DateTime DataAvaliacao { get; set; }
        public string Depoimento { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                rateReadOnly.CurrentRating = this.Nota;
        }
    }
}