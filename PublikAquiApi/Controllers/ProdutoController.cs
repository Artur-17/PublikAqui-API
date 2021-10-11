using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublikAquiApi.Models;
using PublikAquiApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublikAquiApi.Controllers
{
    [Route("api/produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        public ProdutoController()
        {

        }

        [HttpGet]
        public Produto Get()
        {
            return new Produto();
        }

        [HttpPost, Route("CadastrarOuAtualizar")]
        public RespostaWeb CadastrarOuAtualizar([FromBody] Produto produto)
        {
            var Resposta = new RespostaWeb();

            using var repositorio = new ProdutoRepository();
            if ((produto.Id > 0) && (repositorio.ProdutoExiste(produto)))
            {
                if (repositorio.Atualizar(produto)) {
                    Resposta.Sucesso = true;
                    Resposta.Mensagem = "Produto atualizado com sucesso.";
                }
                else
                {
                    Resposta.Sucesso = false;
                    Resposta.Mensagem = "Não foi possível atualizar o produto. Detalhes:" + repositorio.UltimoErro;
                }
            }
            else
            {
                if (repositorio.Inserir(ref produto))
                {
                    Resposta.Sucesso = true;
                    Resposta.Mensagem = "Produto inserido com sucesso.";
                }
                else
                {
                    Resposta.Sucesso = false;
                    Resposta.Mensagem = "Não foi possível inserir o produto. Detalhes:" + repositorio.UltimoErro;
                }
            }

            return Resposta;
        }

        [HttpDelete, Route("Deletar")]
        public RespostaWeb Deletar([FromBody] Produto produto)
        {
            var Resposta = new RespostaWeb();

            using var repositorio = new ProdutoRepository();
            if (repositorio.Deletar(produto))
            {
                Resposta.Sucesso = true;
                Resposta.Mensagem = "Produto deletado com sucesso.";
            }
            else
            {
                Resposta.Sucesso = false;
                Resposta.Mensagem = "Não foi possível deletar o produto. Detalhes:" + repositorio.UltimoErro;
            }
   
            return Resposta;
        }
    }
}
