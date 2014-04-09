using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.Master
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void imgLupa_Click(object sender, ImageClickEventArgs e)
        {
            if (txtBusca.Text.Trim() != string.Empty)
                Response.Redirect("ResultadoBusca.aspx?p=" + txtBusca.Text.Trim());
        }

        public void AtualizaCampoPesquisa(string valor)
        {
            txtBusca.Text = valor;
        }
    }
}