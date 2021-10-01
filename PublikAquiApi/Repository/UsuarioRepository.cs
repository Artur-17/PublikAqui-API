using Npgsql;
using PublikAquiApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublikAquiApi.Repository
{
    public class UsuarioRepository: IDisposable
    {
        private readonly FabricaConexoes fabrica;
        public UsuarioRepository()
        {
            fabrica = new FabricaConexoes();

        }

        public bool Inserir(ref Usuario usuario)
        {
            var comando = "INSERT INTO publikaqui.usuario(nome, email, senha, planoId, dataCadastro, inativo) " +
                "VALUES(@nome, @email, @senha, @planoId, @dataCadastro, @inativo) returning id, dataCadastro;";


            using var command = new NpgsqlCommand(comando, fabrica.ObterConexao());
            command.Parameters.AddWithValue("nome", usuario.Nome);
            command.Parameters.AddWithValue("email", usuario.Email);
            command.Parameters.AddWithValue("senha", usuario.Senha);
            command.Parameters.AddWithValue("planoId", usuario.PlanoId);
            command.Parameters.AddWithValue("dataCadastro", DateTime.Now);
            command.Parameters.AddWithValue("inativo", false);

            using var reader = command.ExecuteReader();
            usuario.Id = Convert.ToInt32(reader["id"]);
            usuario.DataCadastro = (DateTime)reader["dataCadastro"]; 

            return (usuario.Id > 0);
        }
        public void Dispose()
        {
            fabrica.Dispose();
        }
    }
}
