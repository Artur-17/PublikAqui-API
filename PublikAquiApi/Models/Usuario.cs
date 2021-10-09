using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublikAquiApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int PostagensQuantidade { get; set; }
        public DateTime Cadastro { get; set; }
        public bool Inativo { get; set; }
        public bool Deletado { get; set; }
    }
}
