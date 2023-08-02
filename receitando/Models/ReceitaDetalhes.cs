using receitando.Models;
using System.Collections.Generic;

namespace receitando.Models
{
    public class ReceitaDetalhes
    {
        public Receita receita { get; set; }
        public Comentarios comment{ get; set; }
        public List<Comentarios> comentario { get; set; }
    }
}