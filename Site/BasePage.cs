using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Site
{
    public class BasePage : Page
    {
        public string CaminhoADMS
        {
            get
            {
                string caminho;
                if (string.IsNullOrEmpty(caminho = ConfigurationManager.AppSettings["CaminhoADMS"]))
                    throw new Exception("A chave 'CaminhoADMS' não foi configurada corretamente.");

                return caminho;
            }
        }
    }
}