using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublikAquiApi.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string CategoriaNome { get; set; }
        public int CategoriaID { get; set; }
        public double PrecoCusto { get; set; }
        public double PrecoVenda { get; set; }
        public double PrecoPromocional { get; set; }
        public string Foto { get; set; }
        public bool Inativo { get; set; }
        public bool Deletado { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
