using System;
using System.Collections.Generic;

#nullable disable

namespace TesteSimpress.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            TblProdutos = new HashSet<Produto>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        public virtual ICollection<Produto> TblProdutos { get; set; }
    }
}
