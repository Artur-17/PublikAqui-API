using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublikAquiApi.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        public UsuarioController()
        {

        }

        [HttpGet]
        public Object Get()
        {
            return new
            {
                Id = 1,
                Nome = "Seu nome aqui",
                Email = "seu-email@provedor.com",
                Ativo = true
            };
        }
    }
}
