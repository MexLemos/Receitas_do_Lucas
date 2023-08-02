using receitando.Models;
using System.Collections.Generic;

namespace receitando.Models
{
    public class CategoriaDetalhes
    {
        public Categoria categoria { get; set; }
        public List<Receita> receita { get; set; }
    }
}