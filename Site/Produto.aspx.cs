using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Actio.Negocio;
using AjaxControlToolkit;
using Site.Controles;

namespace Site
{
    public partial class Produto : BasePage
    {
        #region Propriedades
        /// <summary>
        /// Armazena o código do produto exibido na página.
        /// </summary>
        public int? CodigoProduto
        {
            get
            {
                if (ViewState["CodigoProduto"] != null)
                    return Convert.ToInt32(ViewState["CodigoProduto"]);

                return null;
            }
            set
            {
                ViewState["CodigoProduto"] = value;
            }
        }

        /// <summary>
        /// Armazena o código da categoria do produto exibido na página.
        /// </summary>
        public int? CodigoCategoria
        {
            get
            {
                if (ViewState["CodigoCategoria"] != null)
                    return Convert.ToInt32(ViewState["CodigoCategoria"]);

                return null;
            }
            set
            {
                ViewState["CodigoCategoria"] = value;
            }
        }

        /// <summary>
        /// Armazena o código da subcategoria do produto exibido na página.
        /// </summary>
        public int? CodigoSubcategoria
        {
            get
            {
                if (ViewState["CodigoSubcategoria"] != null)
                    return Convert.ToInt32(ViewState["CodigoSubcategoria"]);

                return null;
            }
            set
            {
                ViewState["CodigoSubcategoria"] = value;
            }
        }

        /// <summary>
        /// Armazena o código da marca do produto exibido na página.
        /// </summary>
        public int? CodigoMarca
        {
            get
            {
                if (ViewState["CodigoMarca"] != null)
                    return Convert.ToInt32(ViewState["CodigoMarca"]);

                return null;
            }
            set
            {
                ViewState["CodigoMarca"] = value;
            }
        }

        /// <summary>
        /// Armazena os dados do produto exibido na tela.
        /// </summary>
        public DataTable DtProduto
        {
            get
            {
                if (this.CodigoProduto.HasValue)
                {
                    if (ViewState["DtProduto"] == null)
                        ViewState["DtProduto"] = Produtos.SelectById(this.CodigoProduto.Value);

                    return (DataTable)ViewState["DtProduto"];
                }

                return null;
            }
            set
            {
                ViewState["DtProduto"] = value;
            }
        }

        /// <summary>
        /// Armazena todas as avaliações feitas para o produto.
        /// </summary>
        public DataTable DtAvaliacoes
        {
            get
            {
                if (this.CodigoProduto.HasValue)
                {
                    if (ViewState["DtAvaliacoes"] == null)
                        ViewState["DtAvaliacoes"] = Produtos_Avaliacao.BuscaAvaliacoesProduto(this.CodigoProduto.Value);

                    return (DataTable)ViewState["DtAvaliacoes"];
                }

                return null;
            }
            set
            {
                ViewState["DtAvaliacoes"] = value;
            }
        }

        /// <summary>
        /// Armazena o endereço do vídeo do produto.
        /// </summary>
        public string EnderecoVideo
        {
            get
            {
                if (ViewState["EnderecoVideo"] != null)
                    return ViewState["EnderecoVideo"].ToString();

                return "www.youtube.com";
            }
            set
            {
                ViewState["EnderecoVideo"] = value;
            }
        }
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.MaintainScrollPositionOnPostBack = true;

                string codProduto = Request.QueryString["codigoProduto"];
                int codigoProduto;
                if (!string.IsNullOrEmpty(codProduto) && int.TryParse(codProduto, out codigoProduto))
                {
                    CodigoProduto = codigoProduto;

                    PreencheDadosProduto();
                }
                else
                    mvwProduto.SetActiveView(viewProdutoInexistente);
            }
        }

        protected void rptFotosProduto_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView drFoto = (DataRowView)e.Item.DataItem;
            Image img = (Image)e.Item.FindControl("imgFotoProduto");
            if (img != null)
            {
                img.ImageUrl = string.Format("{0}App_Themes\\ActioAdms\\hd\\produtos\\album\\{1}\\{2}", this.CaminhoADMS, this.CodigoProduto.Value, drFoto["arquivo"]);
            }
        }

        protected void rptCores_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView drCor = (DataRowView)e.Item.DataItem;
            HtmlControl div = (HtmlControl)e.Item.FindControl("divCor");
            if (div != null)
            {
                div.Style["background-color"] = drCor["cor"].ToString();
            }
        }

        protected void btnSalvarAvaliacao_Click(object sender, EventArgs e)
        {
            #region Valida e configura dados inseridos pelo usuário
            int nota = rateEnabled.CurrentRating;
            if (nota == 0)
            {
                this.ExibeAlerta("Preencha uma nota para o produto.");
                return;
            }

            string depoimento = string.IsNullOrEmpty(txtOpiniaoProduto.Text) ? null : txtOpiniaoProduto.Text;
            #endregion

            #region Salva avaliação
            try
            {
                Produtos_Avaliacao.SalvarAvaliacao(this.CodigoProduto.Value, nota, depoimento);
                rateEnabled.CurrentRating = 0;
                txtOpiniaoProduto.Text = string.Empty;
                BuscaAvaliacoesProduto();
            }
            catch
            {
                this.ExibeAlerta("Erro ao salvar avaliação do produto. Tente novamente mais tarde.");
                return;
            }
            #endregion

            this.ExibeAlerta("Avaliação cadastrada com sucesso.");
        }

        protected void imbComprar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (DtProduto != null && DtProduto.Rows.Count > 0)
                {
                    //Configurando dados do produto para o Carrinho de compras do PagSeguro
                    DataRow drDetalhesProduto = DtProduto.Rows[0];

                    //O id do produto enviado para o PagSeguro pode conter o nome da Cor selecionada no final.
                    string idProduto = drDetalhesProduto["id"].ToString();

                    string nomeProduto = drDetalhesProduto["ProdDescricao_"].ToString();
                    Encoding iso = Encoding.GetEncoding("ISO-8859-1");
                    Encoding utf8 = Encoding.UTF8;
                    byte[] utfBytes = utf8.GetBytes(nomeProduto);
                    byte[] isoBytes = Encoding.Convert(utf8, iso, utfBytes);
                    string nomeProdutoISO = iso.GetString(isoBytes);

                    decimal valor = drDetalhesProduto.IsNull("ProdValor_") ? 0 : decimal.Parse(drDetalhesProduto["ProdValor_"].ToString().Replace('.', ','));

                    long peso = drDetalhesProduto.IsNull("peso") ? 0 : Convert.ToInt64(drDetalhesProduto["peso"]);

                    //Configurando cor do produto
                    if (!string.IsNullOrEmpty(hdfCor.Value))
                    {
                        idProduto = idProduto + " - " + hdfCor.Value;
                        nomeProduto = nomeProduto + " - Cor " + hdfCor.Value;
                    }

                    Produto_Finaliza.Comprar(idProduto, nomeProduto, 1, valor, peso, 0);

                    #region Criando Requisição de Pagamento do PagSeguro (não utilizado pois não direciona para o carrinho)
                    //PaymentRequest p = new PaymentRequest();

                    ////Cria objeto do tipo Item para adicionar na requisição
                    //DataRow drDetalhesProduto = DtProduto.Rows[0];

                    //string nomeProduto = drDetalhesProduto["ProdDescricao_"].ToString();
                    //Encoding iso = Encoding.GetEncoding("ISO-8859-1");
                    //Encoding utf8 = Encoding.UTF8;
                    //byte[] utfBytes = utf8.GetBytes(nomeProduto);
                    //byte[] isoBytes = Encoding.Convert(utf8, iso, utfBytes);
                    //string nomeProdutoISO = iso.GetString(isoBytes);

                    //decimal valor = drDetalhesProduto.IsNull("ProdValor_") ? 0 : decimal.Parse(drDetalhesProduto["ProdValor_"].ToString().Replace('.', ','));
                    //long? peso = drDetalhesProduto.IsNull("peso") ? null : (long?)Convert.ToInt64(drDetalhesProduto["peso"]);
                    //Item i = new Item(this.CodigoProduto.Value.ToString(), nomeProdutoISO, 1, valor, peso, 0);

                    ////Adiciona o produto na requisição.
                    //p.Items.Add(i);

                    ////Cria parâmetro de cor do produto para ser adicionado ao item.
                    //Uol.PagSeguro.Domain.Parameter param = new Uol.PagSeguro.Domain.Parameter();
                    //p.AddIndexedParameter("itemColor", hdfCor.Value, this.CodigoProduto.Value);

                    ////Cria objeto do tipo Sender para identificar o comprador.
                    //string nome = "João Batista";
                    //string email = "joao.batista@email.com.br";
                    //Phone phone = new Phone("31", "84771166");
                    //Sender s = new Sender(nome, email, phone);

                    ///* CPF está com problema no momento de enviar a requisição de pagamento
                    ////Adiciona CPF do comprador
                    //string cpf = "12345678901";
                    //SenderDocument doc = new SenderDocument(Documents.GetDocumentByType("CPF"), cpf);
                    //s.Documents.Add(doc);
                    //*/

                    ////Associa comprador à requisição de pagamento.
                    //p.Sender = s;

                    ////Informa moeda da transação (R$)
                    //p.Currency = Currency.Brl;

                    ////Insere venda na tabela 'produtos_vendas'
                    //int idVenda = Produtos_Vendas.Inserir(string.Empty, string.Empty, string.Empty, "Aguardando Pagto", string.Empty, string.Empty, "Usuário redirecionado para o PagSeguro", email);

                    ////Cria referência para a compra na base de dados do site.
                    //p.Reference = idVenda.ToString();

                    ////URL que o Pagseguro redirecionará o usuario depois da transação
                    //p.RedirectUri = new Uri("http://www.google.com");

                    ////Configura credenciais de acesso
                    //AccountCredentials credentials = PagSeguroConfiguration.Credentials;

                    ////Faz a chamada ao método Register, que retorna a URL necessária para direcionar o comprador ao PagSeguro
                    //Uri redirectURL = p.Register(credentials);

                    ////Redireciona o comprador para a página do PagSeguro.
                    //Response.Redirect(redirectURL.AbsoluteUri);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                this.ExibeAlerta(string.Format("Ocorreu um erro ao enviar a requisição para o PagSeguro. \\n{0}", ex.Message));
            }
        }

        protected void rptAvaliacoes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            int nota = Convert.ToInt32(drv["nota"]);

            LeituraAvaliacao ucAvaliacoes = (LeituraAvaliacao)((Repeater)sender).Controls[((Repeater)sender).Controls.Count - 1].FindControl("ucLeituraAvaliacao");
            if (ucAvaliacoes != null)
            {
                ucAvaliacoes.Nota = nota;

                Rating rating = (Rating)ucAvaliacoes.FindControl("rateReadOnly");
                if (rating != null)
                    rating.CurrentRating = nota;
            }
        }

        #endregion

        #region Métodos auxiliares
        /// <summary>
        /// Preenche os dados do produto na página, a partir do código.
        /// </summary>
        private void PreencheDadosProduto()
        {
            DtProduto = Produtos.SelectById(CodigoProduto.Value);
            if (DtProduto != null && DtProduto.Rows.Count > 0)
            {
                #region Dados básicos do produto
                DataRow drDetalhesProduto = DtProduto.Rows[0];

                CodigoCategoria = Convert.ToInt32(drDetalhesProduto["id_categoria"]);
                CodigoSubcategoria = Convert.ToInt32(drDetalhesProduto["id_subcategoria"]);
                CodigoMarca = Convert.ToInt32(drDetalhesProduto["id_marca"]);
                string nomeProduto = drDetalhesProduto["ProdDescricao_"].ToString();
                string resumoProduto = drDetalhesProduto["resumo"].ToString();
                decimal valor = drDetalhesProduto.IsNull("ProdValor_") ? 0 : decimal.Parse(drDetalhesProduto["ProdValor_"].ToString().Replace('.', ','));
                decimal valorParcela = valor / 3;
                #endregion

                #region Preenche campos da página
                lblNomeProduto.Text = nomeProduto;
                lblResumoProduto.Text = resumoProduto;
                lblPreco.Text = valor.ToString("c2", new CultureInfo("pt-BR"));
                lblCondicoesPagto.Text = valorParcela.ToString("c2", new CultureInfo("pt-BR"));
                #endregion

                #region Busca Fotos e vídeo do Produto
                //Fotos
                DataTable dtFotos = Produtos_Fotos.FotosDoProduto(this.CodigoProduto.Value);
                rptFotosProduto.DataSource = dtFotos;
                rptFotosProduto.DataBind();

                if (dtFotos != null && dtFotos.Rows.Count > 0)
                {
                    DataRow drFoto1 = dtFotos.Rows[0];
                    string arquivo = string.Format("{0}App_Themes\\ActioAdms\\hd\\produtos\\album\\{1}\\{2}", this.CaminhoADMS, this.CodigoProduto.Value, drFoto1["arquivo"]);
                    imgFotoAmpliada.ImageUrl = arquivo;
                }

                //Vídeo
                string enderecoVideo = drDetalhesProduto.IsNull("endereco_video") ? null : drDetalhesProduto["endereco_video"].ToString();
                if (enderecoVideo == null)
                    iconeVideo.Visible = false;
                else
                {
                    //EnderecoVideo = "http://www.youtube.com/v/9z2zvcD6hoE?version=3&enablejsapi=1";
                    EnderecoVideo = MontaLinkVideo(enderecoVideo);
                }
                #endregion

                #region Busca Cores do Produto
                DataTable dtCores = Produtos_Cores.SelectCoresByIDProduto(this.CodigoProduto.Value);
                if (dtCores != null && dtCores.Rows.Count > 0)
                {
                    rptCores.DataSource = dtCores;
                    rptCores.DataBind();
                }
                else
                    lblEscolhaCor.Visible = false;
                #endregion

                #region Busca Descrições do Produto
                DataTable dtDescricoes = Produtos_Descricao.SelectByIDProduto(CodigoProduto.Value);
                if (dtDescricoes != null && dtDescricoes.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append("<table cellspacing='10' border='1' style='border-collapse:separate; border-spacing:0 10px;'>");
                    foreach (DataRow dr in dtDescricoes.Rows)
                    {
                        string titulo = dr["titulo"].ToString();
                        string descricao = dr["descricao"].ToString();

                        sb.Append("<tr>");

                        sb.Append(string.Format("<td style='border-bottom: dashed 2px #ECECEC; padding:0 30px 10px 0;'>{0}</td>", titulo));
                        sb.Append(string.Format("<td style='border-bottom: dashed 2px #ECECEC;'>{0}</td>", descricao));

                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");

                    spanConteudoDescricao.InnerHtml = sb.ToString();
                }
                #endregion

                #region Configura exibição de sugestões
                ucSugestoes.CodigoProduto = this.CodigoProduto;
                ucSugestoes.CodigoCategoria = this.CodigoCategoria;
                ucMesmaMarca.CodigoProduto = this.CodigoProduto;
                ucMesmaMarca.CodigoMarca = this.CodigoMarca;
                #endregion

                #region Busca avaliações do Produto
                BuscaAvaliacoesProduto();
                #endregion

                #region Cria meta tags para compartilhamento em redes sociais
                //Title
                HtmlMeta metaTitle = new HtmlMeta();
                string propriedade = "title";
                metaTitle.Name = string.Format("og:{0}", propriedade);
                metaTitle.Attributes.Add("property", string.Format("og:{0}", propriedade));
                metaTitle.Content = nomeProduto;
                MetaPlaceHolder.Controls.Add(metaTitle);

                //Description
                HtmlMeta metaDescription = new HtmlMeta();
                propriedade = "description";
                metaDescription.Name = string.Format("og:{0}", propriedade);
                metaDescription.Attributes.Add("property", string.Format("og:{0}", propriedade));
                metaDescription.Content = resumoProduto;
                MetaPlaceHolder.Controls.Add(metaDescription);

                //Image
                HtmlMeta metaImage = new HtmlMeta();
                propriedade = "image";
                metaImage.Name = string.Format("og:{0}", propriedade);
                metaImage.Attributes.Add("property", string.Format("og:{0}", propriedade));
                metaImage.Content = imgFotoAmpliada.ImageUrl;
                MetaPlaceHolder.Controls.Add(metaImage);
                #endregion
            }
            else
            {
                mvwProduto.SetActiveView(viewProdutoInexistente);
            }
        }

        /// <summary>
        /// Monta a URL correta com o link para o vídeo escolhido de forma que
        /// não haja erro na exibição de vídeo e fotos.
        /// </summary>
        /// <param name="enderecoVideo"></param>
        /// <returns></returns>
        private string MontaLinkVideo(string enderecoVideo)
        {
            string link = "http://www.youtube.com/v/{0}?version=3&enablejsapi=1";
            string idVideo = string.Empty;
            if (enderecoVideo.Contains("watch?v="))
                idVideo = enderecoVideo.Substring(enderecoVideo.LastIndexOf("watch?v="), 19).Replace("watch?v=", "");
            else if (enderecoVideo.Contains("youtu.be/"))
                idVideo = enderecoVideo.Substring(enderecoVideo.LastIndexOf("youtu.be/"), 20).Replace("youtu.be/", "");

            if (!string.IsNullOrEmpty(idVideo))
                return string.Format(link, idVideo);

            return "www.youtube.com";
        }

        /// <summary>
        /// Busca o número de avaliações feitas para o produto, a média das notas dadas pelos usuários
        /// e lista os depoimentos na parte inferior da tela.
        /// </summary>
        private void BuscaAvaliacoesProduto()
        {
            int mediaNotas = Produtos_Avaliacao.BuscaMediaAvaliacoes(this.CodigoProduto.Value);
            rateReadOnly.CurrentRating = ratingCabecalho.CurrentRating = mediaNotas;

            DtAvaliacoes = Produtos_Avaliacao.BuscaAvaliacoesProduto(this.CodigoProduto.Value);
            if (DtAvaliacoes != null && DtAvaliacoes.Rows.Count > 0)
            {
                lblNumAvaliacoes.Text = DtAvaliacoes.Rows.Count.ToString();

                #region Preenche as quatro avaliações mais recentes dos usuários
                DataTable dtNew = DtAvaliacoes.AsEnumerable().Take(4).CopyToDataTable();
                rptAvaliacoes.DataSource = dtNew;
                rptAvaliacoes.DataBind();
                #endregion
            }
        }

        /// <summary>
        /// Retorna a quantidade de avaliações atribuídas com o número de estrelas informado.
        /// </summary>
        /// <param name="numEstrelas">Informar null para retornar a quantidade total de avaliações.</param>
        /// <returns></returns>
        public decimal RetornaQtdeAvaliacoes(int? numEstrelas)
        {
            string consulta = "1 = 1";

            if (numEstrelas.HasValue)
                consulta = string.Format("nota = {0}", numEstrelas.Value);

            return ((DataRow[])DtAvaliacoes.Select(consulta)).Length;
        }

        /// <summary>
        /// Envia e-mail de recomendação do produto a um amigo.
        /// </summary>
        /// <param name="nomeRemetente"></param>
        /// <param name="emailRemetente"></param>
        /// <param name="nomeDestinatario"></param>
        /// <param name="emailDestinatario"></param>
        /// <param name="mensagem"></param>
        /// <param name="nomeProduto"></param>
        /// <param name="descricaoProduto"></param>
        /// <param name="linkProduto"></param>
        /// <returns></returns>
        [WebMethod]
        public static string EnviarEmail(string nomeRemetente, string emailRemetente, string nomeDestinatario, string emailDestinatario, string mensagem,
            string nomeProduto, string descricaoProduto, string linkProduto)
        {
            return Mail.EnviarEmail(nomeRemetente, emailRemetente, nomeDestinatario, emailDestinatario, mensagem, nomeProduto, descricaoProduto, linkProduto);
        }
        #endregion
    }
}