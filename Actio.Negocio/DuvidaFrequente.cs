using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actio.Negocio
{
    public class DuvidaFrequente
    {
        public static DataTable SelectAll()
        {
            return conexao.Dados("select * from duvidafrequente");
        }

        public static void Novo(string pergunta, string resposta)
        {
            string sql = string.Format(@"insert into duvidafrequente (pergunta, resposta) values ('{0}', '{1}');", pergunta, resposta);
            conexao.ExecuteNonQuery(sql);
        }

        public static DataTable SelectById(int id)
        {
            string sql = string.Format(@"select * from duvidafrequente where id = {0}", id);
            return conexao.Dados(sql);
        }

        public static void Update(int id, string pergunta, string resposta)
        {
            string sql = string.Format(@"update duvidafrequente
set pergunta = '{0}',
    resposta = '{1}'
where id = {2}", pergunta, resposta, id);

            conexao.ExecuteNonQuery(sql);
        }

        public static void Excluir(int id)
        {
            string sql = string.Format("delete from duvidafrequente where id = {0}", id);
            conexao.ExecuteNonQuery(sql);
        }
    }
}
