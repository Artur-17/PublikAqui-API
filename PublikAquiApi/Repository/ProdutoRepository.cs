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
            var comando = "INSERT INTO publikaqui.produto(cod_barras,descricao, categoria_nome, id_categoria, preco_custo, preco_venda, preco_promocional, foto, inativo, deletado, data_cadastro ) " +
                "VALUES(@cod_barras, @descricao, @categoria_nome, @id_categoria, @preco_custo, @preco_venda, @preco_promocional, @foto, @inativo, @deletado) returning id, data_cadastro;";
            
            try
            {
                using var command = new NpgsqlCommand(comando, fabrica.ObterConexao());
                command.Parameters.AddWithValue("id", produto.Id);
                command.Parameters.AddWithValue("cod_barras", produto.CodigoBarras;
                command.Parameters.AddWithValue("Descricao", produto.Descricao);
                command.Parameters.AddWithValue("CategoriaNome", produto.CategoriaNome);
                command.Parameters.AddWithValue("id_categoria", produto.CategoriaID);
                command.Parameters.AddWithValue("preco_custo", produto.PrecoCusto);
                command.Parameters.AddWithValue("preco_venda", produto.PrecoVenda);
                command.Parameters.AddWithValue("preco_promocional", produto.PrecoPromocional);
                command.Parameters.AddWithValue("foto", produto.Foto);
                command.Parameters.AddWithValue("inativo", false);
                command.Parameters.AddWithValue("deletado", false); 
                command.Parameters.AddWithValue("data_cadastro", DateTime.Now);



                using var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    produto.Id = Convert.ToInt32(reader["id"]);
                    produto.DataCadastro = Convert.ToDateTime(reader["data_cadastro"]);
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
                "cod_barras = @cod_barras ,descricao = @descricao, categoria_nome = @categoria_nome, id_categoria = @id_categoria, preco_custo = @preco_custo, preco_venda = @preco_venda, " +
                "preco_promocional = @preco_promocional, foto = @foto, inativo = @inativo, deletado = @deletado, data_cadastro = @data_cadastro  " +
                "WHERE id = @id";

            try
            {
                using var command = new NpgsqlCommand(comando, fabrica.ObterConexao());
                command.Parameters.AddWithValue("id", produto.Id);
                command.Parameters.AddWithValue("cod_barras", produto.CodigoBarras;
                command.Parameters.AddWithValue("Descricao", produto.Descricao);
                command.Parameters.AddWithValue("CategoriaNome", produto.CategoriaNome);
                command.Parameters.AddWithValue("id_categoria", produto.CategoriaID);
                command.Parameters.AddWithValue("preco_custo", produto.PrecoCusto);
                command.Parameters.AddWithValue("preco_venda", produto.PrecoVenda);
                command.Parameters.AddWithValue("preco_promocional", produto.PrecoPromocional);
                command.Parameters.AddWithValue("foto", produto.Foto);
                command.Parameters.AddWithValue("inativo", false);
                command.Parameters.AddWithValue("deletado", false);
                command.Parameters.AddWithValue("data_cadastro", DateTime.Now);
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
                command.Parameters.AddWithValue("deletado", produto.Deletado);


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
