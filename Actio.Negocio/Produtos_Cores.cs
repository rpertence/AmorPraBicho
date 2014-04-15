using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actio.Negocio
{
    public class Produtos_Cores
    {
        #region Seleciona cores de um produto pelo ID
        public static DataTable SelectCoresByIDProduto(int idProduto)
        {
            string SQL = string.Format(@"SELECT id, id_produto, cor
                            FROM produtos_cores pc
                            WHERE pc.id_produto = {0}", idProduto);
            return conexao.Dados(SQL);
        }
        #endregion
        #region Salva cores cadastradas para um produto
        public static void InserirCores(int idProduto, List<string> listaCores)
        {
            //Primeiro apaga as cores cadastradas atualmente.
            StringBuilder sql = new StringBuilder(string.Format("DELETE FROM produtos_cores WHERE id_produto = {0};", idProduto));

            //Insere as novas cores cadastradas.
            foreach (string cor in listaCores)
            {
                sql.Append(string.Format(@"INSERT INTO produtos_cores(id_produto, cor)
                                            VALUES({0}, '{1}');", idProduto, cor));
            }

            conexao.ExecuteNonQuery(sql.ToString());
        }
        #endregion
        #region Excluir cores cadastradas para um produto
        public static void ExcluirCoresByIdProduto(int idProduto)
        {
            string SQL = string.Format(@"DELETE FROM produtos_cores
                                        WHERE id_produto = {0}", idProduto);
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Excluir cores cadastradas para todos os produtos de uma categoria
        public static void ExcluirCoresByIdCategoria(int idCategoria)
        {
            string sql = string.Format(@"
                                        DELETE pc
                                        FROM produtos_cores pc, produtos p
                                        WHERE pc.id_produto = p.id
	                                        and p.id_categoria = {0};", idCategoria);
            conexao.ExecuteNonQuery(sql);
        }
        #endregion
        #region Excluir cores cadastradas para todos os produtos de uma subcategoria
        public static void ExcluirCoresByIdSubcategoria(int idSubcategoria)
        {
            string sql = string.Format(@"
                                        DELETE pc
                                        FROM produtos_cores pc, produtos p
                                        WHERE pc.id_produto = p.id
	                                        and p.id_subcategoria = {0};", idSubcategoria);
            conexao.ExecuteNonQuery(sql);
        }
        #endregion
    }
}
