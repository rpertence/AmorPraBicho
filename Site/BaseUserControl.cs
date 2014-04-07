using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Site
{
    public class BaseUserControl : UserControl
    {
        public BasePage Pagina
        {
            get
            {
                if (!(this.Page is BasePage))
                    throw new Exception("A página deve herdar de BasePage.");

                return (BasePage)this.Page;
            }
        }
    }
}