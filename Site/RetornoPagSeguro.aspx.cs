using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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
            RetornoPagSeguro1.Token = ConfigurationManager.AppSettings["tokenPagSeguro"];

            //Esta linha deve ser removida para que seja utilizado o ambiente real do PagSeguro
            this.RetornoPagSeguro1.UrlNPI = "http://localhost:9090/pagseguro-ws/checkout/NPI.jhtml";
        }

        protected void RetornoPagSeguro1_VendaEfetuada(UOL.PagSeguro.RetornoVenda retornoVenda)
        {
            #region Recuperando dados retornados pelo PagSeguro

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
            int numItens = retornoVenda.Produtos.Count;
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

            #endregion

            #region Atualiza registros no banco de dados
            #region Tabela produtos_vendas
            DataTable dtVendaExistente = Produtos_Vendas.selectByIdTransacao(codigo_transacao_pagseguro);
            if (dtVendaExistente != null && dtVendaExistente.Rows.Count == 0)
            {
                //Nova venda
                Produtos_Vendas.Inserir(null, codigo_transacao_pagseguro, tipoFreteDescricao, statusDescricao, tipo_pagamento_descricao, frete_cobrado.ToString("f2"), anotacao_cliente,
                                            cliEmail, numItens.ToString());
            }
            else
            {
                //Atualizar venda
                DataRow drVenda = dtVendaExistente.Rows[0];
                string idVenda = drVenda["id"].ToString();

                Produtos_Vendas.UpdateById(idVenda, null, codigo_transacao_pagseguro, statusDescricao, tipo_pagamento_descricao, frete_cobrado.ToString("f2"), anotacao_cliente, cliEmail);
            }
            #endregion

            #region Tabela Cliente
            DataTable dtClienteExistente = Clientes.SelectByEmail(cliEmail);
            if (dtClienteExistente != null && dtClienteExistente.Rows.Count == 0)
            {
                //Novo Cliente
                Clientes.Inserir(cliNome, cliEmail, cliEndereco, cliNumero, cliComplementoEndereco, cliBairro, cliCidade, cliUf, cliCep, cliTelefone.ToString(), "1", "1");
            }
            else
            {
                //Atualizar Dados do Cliente
                DataRow drcliente = dtClienteExistente.Rows[0];
                string idCliente = drcliente["cliente_id"].ToString();
                Clientes.Atualizar(idCliente, cliNome, cliEmail, cliEndereco, cliNumero, cliComplementoEndereco, cliBairro, cliCidade, cliUf, cliCep, cliTelefone.ToString(), "1", "1");
            }
            #endregion
            #endregion
        }

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
    }
}