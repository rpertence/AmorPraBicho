using System;
using System.Collections.Generic;
using System.Configuration;
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

        protected void imbCarrinho_Click(object sender, ImageClickEventArgs e)
        {
            string emailCadastroPagSeguro = ConfigurationManager.AppSettings["emailPagSeguro"];

            var context = HttpContext.Current;
            context.Response.Clear();
            context.Response.Write("<html><head>");
            context.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", "pagseguro"));
            context.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", "pagseguro", "post", "https://pagseguro.uol.com.br/v2/checkout/cart.html?action=view"));
            context.Response.Write(string.Format("<input type=\"hidden\" name=\"encoding\" value=\"utf-8\">"));
            context.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", HttpUtility.HtmlEncode("receiverEmail"), HttpUtility.HtmlEncode(emailCadastroPagSeguro)));
            context.Response.Write("</form>");
            context.Response.Write("</body></html>");
            context.Response.End();
        }
    }
}