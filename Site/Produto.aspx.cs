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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.MaintainScrollPositionOnPostBack = true;
            }
        }

        public int CalculaMediaNotas()
        {
            return 3;
        }

        protected void rating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {

        }
    }
}