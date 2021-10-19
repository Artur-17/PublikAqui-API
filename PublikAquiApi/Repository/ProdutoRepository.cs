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
            var comando = "INSERT INTO publikaqui.produto(" +
                "cod_barras, descricao, quantidade, id_estoque, inativo, deletado, data_cadastro, id_usuario, preco_custo, preco_venda, desconto, preco_promocional, foto, categoria_nome, id_categoria) " +
                "VALUES(" +
                "@cod_barras, @descricao, @quantidade, @id_estoque, @inativo, @deletado, @data_cadastro, @id_usuario, @preco_custo, @preco_venda, @desconto, @preco_promocional, @foto, @categoria_nome, @id_categoria) " +
                "returning id, Datacadastro;";

            try
            {
                using var command = new NpgsqlCommand(comando, fabrica.ObterConexao());

                command.Parameters.AddWithValue("cod_barras", produto.CodigoBarras);
                command.Parameters.AddWithValue("descricao", produto.Descricao);
                command.Parameters.AddWithValue("quantidade", produto.Quantidade);
                command.Parameters.AddWithValue("id_estoque", produto.EstoqueId);
                command.Parameters.AddWithValue("inativo", produto.Inativo);
                command.Parameters.AddWithValue("deletado", produto.Deletado);
                command.Parameters.AddWithValue("data_cadastro", produto.DataCadastro);
                command.Parameters.AddWithValue("id_usuario", produto.UsuarioId);
                command.Parameters.AddWithValue("preco_custo", produto.PrecoCusto);
                command.Parameters.AddWithValue("preco_venda", produto.PrecoVenda);
                command.Parameters.AddWithValue("desconto", produto.Desconto);
                command.Parameters.AddWithValue("preco_promocional", produto.PrecoPromocional);
                command.Parameters.AddWithValue("foto", produto.Foto);
                command.Parameters.AddWithValue("categoria_nome", produto.CategoriaNome);
                command.Parameters.AddWithValue("id_categoria", produto.CategoriaID);


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

        public List<Produto> Listar()
        {
            var produtoLista = new List<Produto>();
            var comando = "SELECT * FROM publikaqui.produto WHERE not deletado AND not inativo";

            using var command = new NpgsqlCommand(comando, fabrica.ObterConexao());

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var produto = new Produto
                {
                    Id = Convert.ToInt32(reader["id"]),
                    CodigoBarras = Convert.ToInt32(reader["cod_barras"]),
                    Descricao = Convert.ToString(reader["descricao"]),
                    Quantidade = Convert.ToSingle(reader["quantidade"]),
                    EstoqueId = Convert.ToInt32(reader["id_estoque"]),
                    UsuarioId = Convert.ToInt32(reader["id_usuario"]),
                    CategoriaNome = Convert.ToString(reader["categoria_nome"]),
                    CategoriaID = Convert.ToInt32(reader["id_categoria"]),
                    PrecoCusto = Convert.ToSingle(reader["preco_custo"]),
                    PrecoVenda = Convert.ToSingle(reader["preco_venda"]),
                    Desconto = Convert.ToSingle(reader["desconto"]),
                    PrecoPromocional = Convert.ToSingle(reader["preco_promocional"]),
                    Foto = Convert.ToString(reader["foto"]),
                    Inativo = Convert.ToBoolean(reader["inativo"]),
                    Deletado = Convert.ToBoolean(reader["deletado"]),
                    DataCadastro = Convert.ToDateTime(reader["data_cadastro"])
                };
                produtoLista.Add(produto);
            }


            return produtoLista;
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
                "id = @id, cod_barras = @cod_barras, descricao = @descricao, quantidade = @quantidade, id_estoque = @id_estoque, inativo = @inativo," +
                "deletado = @deletado, data_cadastro = @data_cadastro, id_usuario = @id_usuario, preco_custo = @preco_custo, preco_venda = @preco_venda, " +
                "desconto = @desconto, preco_promocional = @preco_promocional, foto = @foto, categoria_nome = @categoria_nome, id_categoria = @id_categoria " +
                "WHERE id = @id";
            try
            {
                using var command = new NpgsqlCommand(comando, fabrica.ObterConexao());
                command.Parameters.AddWithValue("id", produto.Id);
                command.Parameters.AddWithValue("cod_barras", produto.CodigoBarras);
                command.Parameters.AddWithValue("descricao", produto.Descricao);
                command.Parameters.AddWithValue("quantidade", produto.Quantidade);
                command.Parameters.AddWithValue("id_estoque", produto.EstoqueId);
                command.Parameters.AddWithValue("inativo", produto.Inativo);
                command.Parameters.AddWithValue("deletado", produto.Deletado);
                command.Parameters.AddWithValue("data_cadastro", produto.DataCadastro);
                command.Parameters.AddWithValue("id_usuario", produto.UsuarioId);
                command.Parameters.AddWithValue("preco_custo", produto.PrecoCusto);
                command.Parameters.AddWithValue("preco_venda", produto.PrecoVenda);
                command.Parameters.AddWithValue("desconto", produto.Desconto);
                command.Parameters.AddWithValue("preco_promocional", produto.PrecoPromocional);
                command.Parameters.AddWithValue("foto", produto.Foto);
                command.Parameters.AddWithValue("categoria_nome", produto.CategoriaNome);
                command.Parameters.AddWithValue("id_categoria", produto.CategoriaID);

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
                        "deletado = True" +
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
