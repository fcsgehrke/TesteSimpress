using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TesteSimpress.Models
{
    public partial class Produto
    {
        public Produto ()
        {
            Ativo = true;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public bool Perecivel { get; set; }
        public int CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}
