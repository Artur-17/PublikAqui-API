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
    [Route("api/usuario")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        public ProdutoController()
        {

        }

        [HttpGet]
        public Usuario Get()
        {
            return new Usuario();
        }

        [HttpPost, Route("CadastrarOuAtualizar")]
        public RespostaWeb CadastrarOuAtualizar([FromBody] Usuario usuario)
        {
            var Resposta = new RespostaWeb();

            using var repositorio = new UsuarioRepository();
            if (repositorio.UsuarioExiste(usuario))
            {
                if (repositorio.Atualizar(usuario)) {
                    Resposta.Status = 200;
                    Resposta.Mensagem = "Cliente atualizado com sucesso.";
                }
                else
                {
                    Resposta.Status = 500;
                    Resposta.Mensagem = "Não foi possível atualizar o cliente. Detalhes:" + repositorio.UltimoErro;
                }
            }
            else
            {
                if (repositorio.Inserir(usuario))
                {
                    Resposta.Status = 200;
                    Resposta.Mensagem = "Cliente inserido com sucesso.";
                }
                else
                {
                    Resposta.Status = 500;
                    Resposta.Mensagem = "Não foi possível inserir o cliente. Detalhes:" + repositorio.UltimoErro;
                }
            }

            return Resposta;
        }

        [HttpDelete, Route("Deletar")]
        public RespostaWeb Deletar([FromBody] Usuario usuario)
        {
            var Resposta = new RespostaWeb();

            using var repositorio = new UsuarioRepository();
            if (repositorio.UsuarioExiste(usuario))
            {
                if (repositorio.Deletar(usuario))
                {
                    Resposta.Status = 200;
                    Resposta.Mensagem = "Cliente Deletado com sucesso.";
                }
                else
                {
                    Resposta.Status = 500;
                    Resposta.Mensagem = "Não foi possível Deletar o cliente. Detalhes:" + repositorio.UltimoErro;
                }
            }
            else
            {
                if (repositorio.Inserir(usuario))
                {
                    Resposta.Status = 200;
                    Resposta.Mensagem = "Cliente Deletado com sucesso.";
                }
                else
                {
                    Resposta.Status = 500;
                    Resposta.Mensagem = "Não foi possível Deletar o cliente. Detalhes:" + repositorio.UltimoErro;
                }
            }

            return Resposta;
        }
    }
}
