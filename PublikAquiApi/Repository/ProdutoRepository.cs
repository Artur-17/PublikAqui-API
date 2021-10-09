using Npgsql;
using PublikAquiApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublikAquiApi.Repository
{
    public class ProdutoRepository: IDisposable
    {
        public Exception UltimoErro;
        
        private readonly ConexaoFactory fabrica;
        public ProdutoRepository()
        {
            fabrica = new ConexaoFactory();
        }

        public bool Inserir(Produto produto)
        {
            var produtoTemp = produto;
            return Inserir(ref produtoTemp);
        }
        public bool Inserir(ref Produto produto)
        {
            var comando = "INSERT INTO publikaqui.produto(Descricao, CategoriaNome, CategoriaID, PrecoCusto, PrecoVenda, PrecoPromocional, Foto,, Inativo, Deletado, Datacadastro ) " +
                "VALUES(@Descricao, @CategoriaNome, @CategoriaID, @PrecoCusto, @PrecoVenda, @PrecoPromocional, @Foto) returning id, Datacadastro;";
            
            try
            {
                using var command = new NpgsqlCommand(comando, fabrica.ObterConexao());
                command.Parameters.AddWithValue("ID", produto.Id);
                command.Parameters.AddWithValue("Descricao", produto.Descricao);
                command.Parameters.AddWithValue("CategoriaNome", produto.CategoriaNome);
                command.Parameters.AddWithValue("CategoriaID", produto.CategoriaID);
                command.Parameters.AddWithValue("PrecoCusto", produto.PrecoCusto);
                command.Parameters.AddWithValue("PrecoVenda", produto.PrecoVenda);
                command.Parameters.AddWithValue("PrecoPromocional", produto.PrecoPromocional);
                command.Parameters.AddWithValue("Foto", produto.Foto);
                command.Parameters.AddWithValue("Inativo", false);
                command.Parameters.AddWithValue("Deletado", false); 
                command.Parameters.AddWithValue("DataCadastro", DateTime.Now);



                using var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    produto.Id = Convert.ToInt32(reader["id"]);
                    produto.DataCadastro = Convert.ToDateTime(reader["Datacadastro"]);
                }

                return (produto.Id > 0);
            }
            catch (Exception e)
            {
                UltimoErro = e;
                return false;
            }

        }

        public bool ProdutoExiste(Produto produto)
        {
            var comando = "SELECT id FROM publikaqui.produto WHERE id = @id;";
            try
            {
                using var command = new NpgsqlCommand(comando, fabrica.ObterConexao());
                command.Parameters.AddWithValue("id", produto.Id);

                using var reader = command.ExecuteReader();
                
                return reader.HasRows;
            }
            catch (Exception e)
            {
                UltimoErro = e;
                return false;
            }
        }
        public bool Atualizar(Produto produto)
        {
            var produtoTemp = produto;
            return Atualizar(ref produtoTemp);
        }
        public bool Atualizar(ref Produto produto)
        {
            var comando = "UPDATE publikaqui.produto SET " +
                "Descricao = @Descricao , CategoriaNome = @CategoriaNome, CategoriaID = @CategoriaID, PrecoCusto = @PrecoCusto, " +
                "PrecoVenda = @PrecoVenda, PrecoPromocional = @PrecoPromocional, Foto = @Foto, Inativo = @Inativo, Deletado = @Deletado, Datacadastro = @Datacadastro " +
                "WHERE id = @id";

            try
            {
                using var command = new NpgsqlCommand(comando, fabrica.ObterConexao());
                command.Parameters.AddWithValue("id", produto.Id);
          
                using var reader = command.ExecuteReader();

                return true;
            }
            catch (Exception e)
            {
                UltimoErro = e;
                return false;
            }
        }

        public bool Deletar(Produto produto)
        {
            var produtoTemp = produto;
            return Deletar(ref produtoTemp);
        }

        public bool Deletar(ref Produto produto)
        {
            var comando = "UPDATE publikaqui.usuario SET " +
                        "deletado = @deletado" +
                        "WHERE id = @id";
            try
            {
                using var command = new NpgsqlCommand(comando, fabrica.ObterConexao());
                command.Parameters.AddWithValue("id", produto.Id);
              
               
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
