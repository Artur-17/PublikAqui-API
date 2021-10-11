using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublikAquiApi.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string CodigoBarras { get; set; }
        public string Descricao { get; set; }
        public double Quantidade { get; set; } // criado para facilitar criação, caso sobre tempo criar outra entidade
        public int IdEstoque { get; set; } //Campo já mapeado para caso sobre tempo
        public bool Inativo { get; set; }
        public bool Deletado { get; set; }
        public DateTime DataCadastro { get; set; }
        public string CategoriaNome { get; set; } // criado para facilitar criação, caso sobre tempo criar outra entidade
        public int CategoriaID { get; set; } //Campo já mapeado para caso sobre tempo
        public double PrecoCusto { get; set; }
        public double PrecoVenda { get; set; }
        public double Desconto { get; set; }
        public double PrecoPromocional { get; set; }
        public string Foto { get; set; }
    }
}
