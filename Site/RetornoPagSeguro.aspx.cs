using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Actio.Negocio;
using UOL.PagSeguro;

namespace Site
{
    public partial class RetornoPagSeguro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //TODO:Esta primeira linha deve ser removida para que seja utilizado o ambiente real do PagSeguro
            //this.RetornoPagSeguro1.UrlNPI = "http://localhost:9090/pagseguro-ws/checkout/NPI.jhtml";
            //this.RetornoPagSeguro1.Token = ConfigurationManager.AppSettings["tokenPagSeguro"];

            if (Request.HttpMethod == "POST")
            {
                //o método POST indica que a requisição é o retorno da validação NPI.

                string Token = ConfigurationManager.AppSettings["tokenPagSeguro"];
                string Pagina = "https://pagseguro.uol.com.br/pagseguro-ws/checkout/NPI.jhtml";
                string Dados = HttpContext.Current.Request.Form.ToString() + "&Comando=validar" + "&Token=" + Token;

                System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(Pagina);

                req.Method = "POST";
                req.ContentLength = Dados.Length;
                req.ContentType = "application/x-www-form-urlencoded";

                System.IO.StreamWriter stOut = new System.IO.StreamWriter(req.GetRequestStream(), System.Text.Encoding.GetEncoding("ISO-8859-1"));
                stOut.Write(Dados);
                stOut.Close();

                System.IO.StreamReader stIn = new System.IO.StreamReader(req.GetResponse().GetResponseStream(), System.Text.Encoding.GetEncoding("ISO-8859-1"));
                string Result = stIn.ReadToEnd();
                stIn.Close();

                #region Recuperando dados retornados pelo PagSeguro

                #region Detalhes da Transação
                //Obtendo o número do Pedido
                string codigo_pedido = Request.Params["Referencia"];

                //Obtendo o código da transação no PagSeguro
                string codigo_transacao_pagseguro = Request.Params["TransacaoID"];

                //Obtendo o novo status da transação
                string statusDescricao = Request.Params["StatusTransacao"];

                //Obtendo a data da transação
                DateTime dataTransacao = Convert.ToDateTime(Request.Params["DataTransacao"]);
                #endregion

                #region Detalhes do Pedido
                //Obtendo a forma de pagamento utilizada
                string tipo_pagamento_descricao = Request.Params["TipoPagamento"];

                //Obtendo o tipo do frete
                string tipoFreteDescricao = Request.Params["TipoFrete"];

                //Obtendo o valor pago pelo frete
                double frete_cobrado = Convert.ToDouble(Request.Params["ValorFrete"]);

                //Obtendo extras
                double extras = Convert.ToDouble(Request.Params["Extras"]);

                //Obtendo a anotação deixada pelo cliente no momento do pagamento
                string anotacao_cliente = Request.Params["Anotacao"];

                //Obtendo o número de itens vendidos
                int numItens = Convert.ToInt32(Request.Params["NumItens"]);

                //Configura lista de produtos pedidos
                List<UOL.PagSeguro.Produto> listaProdutos = RetornaListaProdutos(numItens);
                #endregion

                #region Detalhes do Cliente
                string cliBairro = Request.Params["CliBairro"];
                string cliCep = Request.Params["CliCEP"];
                string cliCidade = Request.Params["CliCidade"];
                string cliComplementoEndereco = Request.Params["CliComplemento"];
                string cliEmail = Request.Params["CliEmail"];
                string cliEndereco = Request.Params["CliEndereco"];
                string cliNome = Request.Params["CliNome"];
                string cliNumero = Request.Params["CliNumero"];
                string cliPais = Request.Params["CliPais"];
                string cliTelefone = Request.Params["CliTelefone"];
                string cliUf = Request.Params["CliEstado"];
                #endregion

                string emailVendedor = ConfigurationManager.AppSettings["emailPagSeguro"];
                #endregion

                if (Result == "VERIFICADO")
                {
                    #region Atualiza registros no banco de dados
                    bool erro = false;
                    StringBuilder msgErro = new StringBuilder();
                    int? idVenda = null;

                    #region Tabela produtos_vendas
                    try
                    {
                        DataTable dtVendaExistente = Produtos_Vendas.selectByIdTransacao(codigo_transacao_pagseguro);
                        if (dtVendaExistente != null && dtVendaExistente.Rows.Count == 0)
                        {
                            //Nova venda
                            idVenda = Produtos_Vendas.Inserir(null, codigo_transacao_pagseguro, tipoFreteDescricao, statusDescricao, tipo_pagamento_descricao, frete_cobrado.ToString("f2"), anotacao_cliente,
                                                                        cliEmail, numItens.ToString(), listaProdutos);
                        }
                        else
                        {
                            //Atualizar dados da venda
                            DataRow drVenda = dtVendaExistente.Rows[0];
                            idVenda = Convert.ToInt32(drVenda["id"]);
                            string statusTransacaoAntigo = drVenda["status_descricao"].ToString().ToLower();

                            Produtos_Vendas.UpdateById(idVenda.ToString(), null, codigo_transacao_pagseguro, statusDescricao, tipo_pagamento_descricao, frete_cobrado.ToString("f2"), anotacao_cliente, cliEmail,
                                listaProdutos, statusTransacaoAntigo);
                        }
                    }
                    catch (Exception ex)
                    {
                        erro = true;
                        msgErro.Append(@"Ocorreu um erro ao gravar os dados da transação:<br>
                                         Verifique o status da transação e principalmente o seu estoque.<br>
                                         Informe a Actio Comunicação sobre o seguinte erro:<br> " + ex.ToString());
                    }
                    #endregion

                    #region Tabela Cliente
                    if (!erro)
                    {
                        try
                        {
                            DataTable dtClienteExistente = Clientes.SelectByEmail(cliEmail);
                            if (dtClienteExistente != null && dtClienteExistente.Rows.Count == 0)
                            {
                                //Novo Cliente
                                Clientes.Inserir(cliNome, cliEmail, cliEndereco, cliNumero, cliComplementoEndereco, cliBairro, cliCidade, cliUf, cliCep, cliTelefone, "1", "1");
                            }
                            else
                            {
                                //Atualizar Dados do Cliente
                                DataRow drcliente = dtClienteExistente.Rows[0];
                                string idCliente = drcliente["cliente_id"].ToString();
                                Clientes.Atualizar(idCliente, cliNome, cliEmail, cliEndereco, cliNumero, cliComplementoEndereco, cliBairro, cliCidade, cliUf, cliCep, cliTelefone, "1", "1");
                            }
                        }
                        catch (Exception ex)
                        {
                            msgErro.Append("Ocorreu um erro ao gravar os dados do cliente:<br><br>" + ex.ToString());
                        }
                    }
                    #endregion
                    #endregion

                    #region Notifica vendedor
                    Mail.EnviarEmail(cliNome, cliEmail, cliTelefone, statusDescricao, codigo_transacao_pagseguro, dataTransacao, numItens, msgErro.ToString(), emailVendedor, Mail.TipoNotificacao.Movimentacao);
                    #endregion
                }
                else if (Result == "FALSO")
                {
                    //o post nao foi validado

                    #region Notifica vendedor
                    string mensagem = @"Houve uma falha no processamento automático de retorno do PagSeguro, verifique o status da transação e principalmente o seu estoque. 
                                        <br><br>Informe a Actio Comunicação sobre o erro:
                                        <br>O POST não foi validado.";

                    //Notifica a loja
                    Mail.EnviarEmail(cliNome, cliEmail, cliTelefone, statusDescricao, codigo_transacao_pagseguro, dataTransacao, numItens, mensagem, emailVendedor, Mail.TipoNotificacao.PostNaoValidado);
                    #endregion
                }
                else
                {
                    //erro na integração com PagSeguro.

                    #region Notifica vendedor
                    string mensagem = @"Houve uma falha na integração com o PagSeguro, verifique o status da transação e principalmente o seu estoque. 
                                        <br><br>Informe a Actio Comunicação sobre o erro:
                                        <br>Erro não integração com o PagSeguro.";

                    //Notifica a loja
                    Mail.EnviarEmail(cliNome, cliEmail, cliTelefone, statusDescricao, codigo_transacao_pagseguro, dataTransacao, numItens, mensagem, emailVendedor, Mail.TipoNotificacao.ErroIntegracaoPagSeguro);
                    #endregion
                }
            }
            else if (Request.HttpMethod == "GET")
            {
                //o método GET indica que a requisição é o retorno do Checkout PagSeguro para o site vendedor.
                //no término do checkout o usuário é redirecionado para este bloco.
            }
        }

        /// <summary>
        /// Retorna uma lista de objetos do tipo UOL.PagSeguro.Produto para atualização de estoque
        /// </summary>
        /// <param name="numItens"></param>
        /// <returns></returns>
        private List<UOL.PagSeguro.Produto> RetornaListaProdutos(int numItens)
        {
            List<UOL.PagSeguro.Produto> listaProdutos = new List<UOL.PagSeguro.Produto>();

            for (int i = 0; i < numItens; i++)
            {
                UOL.PagSeguro.Produto p = new UOL.PagSeguro.Produto();
                p.Codigo = Request.Params[string.Format("ProdID_{0}", i + 1)];
                p.Quantidade = Convert.ToInt32(Request.Params[string.Format("ProdQuantidade_{0}", i + 1)]);

                listaProdutos.Add(p);
            }

            return listaProdutos;
        }

        protected void RetornoPagSeguro1_VendaEfetuada(UOL.PagSeguro.RetornoVenda retornoVenda)
        {
            #region Recuperando dados retornados pelo PagSeguro
            /*
            #region Detalhes da Transação
            //Obtendo o número do Pedido
            string codigo_pedido = retornoVenda.CodigoReferencia;

            //Obtendo o código da transação no PagSeguro
            string codigo_transacao_pagseguro = retornoVenda.CodigoTransacao;

            //Obtendo o novo status da transação
            StatusTransacao statusTransacao = retornoVenda.StatusTransacao;
            string statusDescricao = retornoVenda.StatusTransacaoDescricao;

            //Obtendo a data da transação
            DateTime dataTransacao = retornoVenda.Data;
            #endregion

            #region Detalhes do Pedido
            //Obtendo a forma de pagamento utilizada
            TipoPagamento tipo_pagamento = retornoVenda.TipoPagamento;
            string tipo_pagamento_descricao = retornoVenda.TipoPagamentoDescricao;

            //Obtendo o tipo do frete
            UOL.PagSeguro.TipoFreteRetorno tipoFrete = retornoVenda.TipoFrete;
            string tipoFreteDescricao = retornoVenda.TipoFreteDescricao;

            //Obtendo o valor pago pelo frete
            double frete_cobrado = retornoVenda.ValorFrete;

            //Obtendo extras
            double extras = retornoVenda.Extras;

            //Obtendo a anotação deixada pelo cliente no momento do pagamento
            string anotacao_cliente = retornoVenda.Anotacao;

            //Obtendo o número de itens vendidos
            int numItens = retornoVenda.Produtos.Sum(p => p.Quantidade);
            #endregion

            #region Detalhes do Cliente
            string cliBairro = retornoVenda.Cliente.Bairro;
            string cliCep = retornoVenda.Cliente.Cep;
            string cliCidade = retornoVenda.Cliente.Cidade;
            string cliComplementoEndereco = retornoVenda.Cliente.ComplementoEndereco;
            int cliDdd = retornoVenda.Cliente.DDD;
            string cliEmail = retornoVenda.Cliente.Email;
            string cliEndereco = retornoVenda.Cliente.Endereco;
            string cliNome = retornoVenda.Cliente.Nome;
            string cliNumero = retornoVenda.Cliente.Numero;
            string cliPais = retornoVenda.Cliente.Pais;
            int cliTelefone = retornoVenda.Cliente.Telefone;
            string cliUf = retornoVenda.Cliente.Uf;
            #endregion
            */
            #endregion

            #region Atualiza registros no banco de dados
            /*
            bool erro = false;
            #region Tabela produtos_vendas
            try
            {
                DataTable dtVendaExistente = Produtos_Vendas.selectByIdTransacao(codigo_transacao_pagseguro);
                if (dtVendaExistente != null && dtVendaExistente.Rows.Count == 0)
                {
                    //Nova venda
                    Produtos_Vendas.Inserir(null, codigo_transacao_pagseguro, tipoFreteDescricao, statusDescricao, tipo_pagamento_descricao, frete_cobrado.ToString("f2"), anotacao_cliente,
                                                                cliEmail, numItens.ToString(), retornoVenda);
                }
                else
                {
                    //Atualizar dados da venda
                    DataRow drVenda = dtVendaExistente.Rows[0];
                    string idVenda = drVenda["id"].ToString();
                    string statusTransacaoAntigo = drVenda["status_descricao"].ToString().ToLower();

                    Produtos_Vendas.UpdateById(idVenda, null, codigo_transacao_pagseguro, statusDescricao, tipo_pagamento_descricao, frete_cobrado.ToString("f2"), anotacao_cliente, cliEmail,
                        retornoVenda, statusTransacaoAntigo);
                }
            }
            catch
            {
                erro = true;
            }
            #endregion

            #region Tabela Cliente
            if (!erro)
            {
                DataTable dtClienteExistente = Clientes.SelectByEmail(cliEmail);
                if (dtClienteExistente != null && dtClienteExistente.Rows.Count == 0)
                {
                    //Novo Cliente
                    Clientes.Inserir(cliNome, cliEmail, cliEndereco, cliNumero, cliComplementoEndereco, cliBairro, cliCidade, cliUf, cliCep, string.Format("{0} {1}", cliDdd, cliTelefone), "1", "1");
                }
                else
                {
                    //Atualizar Dados do Cliente
                    DataRow drcliente = dtClienteExistente.Rows[0];
                    string idCliente = drcliente["cliente_id"].ToString();
                    Clientes.Atualizar(idCliente, cliNome, cliEmail, cliEndereco, cliNumero, cliComplementoEndereco, cliBairro, cliCidade, cliUf, cliCep, string.Format("{0} {1}", cliDdd, cliTelefone), "1", "1");
                }
            }
            #endregion
            */
            #endregion
        }

        /*
        protected void RetornoPagSeguro1_VendaNaoAutenticada(object sender, UOL.PagSeguro.VendaNaoAutenticadaEventArgs e)
        {
            //Aqui dispara quando o PagSeguro retorna algo diferente de verificado
        }

        protected void RetornoPagSeguro1_FalhaProcessarRetorno(object sender, UOL.PagSeguro.FalhaProcessarRetornoEventArgs e)
        {
            //Aqui dispara quando dá algum problema de parse nos dados
        }

        protected void RetornoPagSeguro1_RetornoVerificado(object sender, UOL.PagSeguro.VendaAutenticadaEventArgs e)
        {
            //Aqui dispara quando é obtido o retorno VERIFICADO do PagSeguro. Este método é disparado antes do RetornoPagSeguro1_VendaEfetuada
        }
         * */
    }
}