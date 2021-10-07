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
        public Exception UltimoErro;
        
        private readonly ConexaoFactory fabrica;
        public UsuarioRepository()
        {
            fabrica = new ConexaoFactory();
        }

        public bool Inserir(Usuario usuario)
        {
            var usuarioTemp = usuario;
            return Inserir(ref usuarioTemp);
        }
        public bool Inserir(ref Usuario usuario)
        {
            var comando = "INSERT INTO publikaqui.usuario(nome, email, senha, postagens_qtd, cadastro, inativo) " +
                "VALUES(@nome, @email, @senha, @postagens_qtd, @cadastro, @inativo) returning id, cadastro;";
            
            try
            {
                using var command = new NpgsqlCommand(comando, fabrica.ObterConexao());
                command.Parameters.AddWithValue("nome", usuario.Nome);
                command.Parameters.AddWithValue("email", usuario.Email);
                command.Parameters.AddWithValue("senha", usuario.Senha);
                command.Parameters.AddWithValue("postagens_qtd", usuario.PostagensQuantidade);
                command.Parameters.AddWithValue("cadastro", DateTime.Now);
                command.Parameters.AddWithValue("inativo", false);

                using var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.Cadastro = Convert.ToDateTime(reader["cadastro"]);
                }

                return (usuario.Id > 0);
            }
            catch (Exception e)
            {
                UltimoErro = e;
                return false;
            }

        }

        public bool UsuarioExiste(Usuario usuario)
        {
            var comando = "SELECT id FROM publikaqui.usuario WHERE id = @id OR email = @email;";
            try
            {
                using var command = new NpgsqlCommand(comando, fabrica.ObterConexao());
                command.Parameters.AddWithValue("id", usuario.Id);
                command.Parameters.AddWithValue("email", usuario.Email); 

                using var reader = command.ExecuteReader();
                
                return reader.HasRows;
            }
            catch (Exception e)
            {
                UltimoErro = e;
                return false;
            }
        }
        public bool Atualizar(Usuario usuario)
        {
            var usuarioTemp = usuario;
            return Atualizar(ref usuarioTemp);
        }
        public bool Atualizar(ref Usuario usuario)
        {
            var comando = "UPDATE publikaqui.usuario SET " +
                "nome = @nome, email = @email, senha = @senha, postagens_qtd = @postagens_qtd, inativo = @inativo  " +
                "WHERE id = @id";

            try
            {
                using var command = new NpgsqlCommand(comando, fabrica.ObterConexao());
                command.Parameters.AddWithValue("id", usuario.Id);
                command.Parameters.AddWithValue("nome", usuario.Nome);
                command.Parameters.AddWithValue("email", usuario.Email);
                command.Parameters.AddWithValue("senha", usuario.Senha);
                command.Parameters.AddWithValue("postagens_qtd", usuario.PostagensQuantidade);
                command.Parameters.AddWithValue("inativo", usuario.Inativo);

                using var reader = command.ExecuteReader();

                return true;
            }
            catch (Exception e)
            {
                UltimoErro = e;
                return false;
            }
        }

        public void Dispose()
        {
            fabrica.Dispose();
        }
    }
}
