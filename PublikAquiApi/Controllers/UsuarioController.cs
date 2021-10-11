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
    public class UsuarioController : ControllerBase
    {
        public UsuarioController()
        {

        }

        /*
        [HttpGet, Route("Obter")]
        public List<Usuario> Obter()
        {
            using var repositorio = new UsuarioRepository();
            return repositorio.Obter;
        }
        */
        [HttpPost, Route("CadastrarOuAtualizar")]
        public RespostaWeb CadastrarOuAtualizar([FromBody] Usuario usuario)
        {
            var Resposta = new RespostaWeb();

            using var repositorio = new UsuarioRepository();
            if ((usuario.Id > 0) && (repositorio.UsuarioExiste(usuario)))
            {
                if (repositorio.Atualizar(usuario)) {
                    Resposta.Sucesso = true;
                    Resposta.Mensagem = "Cliente atualizado com sucesso.";
                }
                else
                {
                    Resposta.Sucesso = false;
                    Resposta.Mensagem = "Não foi possível atualizar o cliente. Detalhes:" + repositorio.UltimoErro;
                }
            }
            else
            {
                if (repositorio.Inserir(ref usuario))
                {
                    Resposta.Sucesso = true;
                    Resposta.Mensagem = "Cliente inserido com sucesso.";
                }
                else
                {
                    Resposta.Sucesso = false;
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

            if (repositorio.Deletar(usuario))
            {
                Resposta.Sucesso = true;
                Resposta.Mensagem = "Cliente Deletado com sucesso.";
            }
            else
            {
                Resposta.Sucesso = false;
                Resposta.Mensagem = "Não foi possível Deletar o cliente. Detalhes:" + repositorio.UltimoErro;
            }

            return Resposta;
        }
    }
}
