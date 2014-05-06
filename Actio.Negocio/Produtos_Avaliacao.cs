using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actio.Negocio
{
    public class Produtos_Avaliacao
    {
        #region Seleciona por ID do Produto
        /// <summary>
        /// Retorna todas as avaliações cadastradas para o produto.
        /// </summary>
        /// <param name="idProduto"></param>
        /// <returns></returns>
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable BuscaAvaliacoesProduto(int idProduto)
        {
            string SQL = string.Format(@"SELECT id, id_produto, nota, 'titulo' as `titulo`, 'usuario' as `nomeUsuario`, depoimento, `data`
                                        FROM produtos_avaliacao p
                                        WHERE p.id_produto = {0}
                                        ORDER BY `data` DESC, `id` DESC;", idProduto);
            return conexao.Dados(SQL);
        }
        #endregion

        #region Seleciona Média de Notas das avaliações por ID do Produto
        /// <summary>
        /// Retorna as médias das notas atribuídas na avaliação do produto pelos usuários.
        /// </summary>
        /// <param name="idProduto"></param>
        /// <returns></returns>
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static int BuscaMediaAvaliacoes(int idProduto)
        {
            string SQL = string.Format(@"SELECT p.id_produto, sum(nota)/count(nota) as `media`
                                        FROM produtos_avaliacao p
                                        WHERE p.id_produto = {0};", idProduto);
            DataTable dt = conexao.Dados(SQL);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                int media = dr.IsNull("media") ? 0 : Convert.ToInt32(dr["media"]);
                return media;
            }

            return 0;
        }
        #endregion

        #region Nova Avaliação
        public static void SalvarAvaliacao(int idProduto, int nota, string depoimento)
        {
            string SQL = string.Format(@"INSERT INTO produtos_avaliacao(id_produto, nota, depoimento, `Data`)
                                         VALUES({0}, {1}, '{2}', CURDATE());", idProduto, nota, depoimento);
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
    }
}
