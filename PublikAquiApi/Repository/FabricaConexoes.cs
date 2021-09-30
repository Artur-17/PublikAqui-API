using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace PublikAquiApi.Repository
{
    public class FabricaConexoes : IDisposable
    {

        private string StringConexao { get; set; }

        public NpgsqlConnection Conexao { get; set; }

        public FabricaConexoes()
        {
            StringConexao = "Server=127.0.0.1;Port=5433;Database=publikaqui;User Id=postgres;Password=123;";
        }

        public NpgsqlConnection ObterConexao()
        {
            if ((Conexao == null) || (Conexao.State != ConnectionState.Open))
            {
                Conexao = new NpgsqlConnection(StringConexao);
                Conexao.Open();
            }

            return Conexao;
        }

        public void Dispose()
        {
            if ((Conexao == null) && (Conexao.State != ConnectionState.Closed))
                Conexao.Close();
        }


    }
}
