//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace receitando.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comentarios
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public Nullable<int> ReceitaId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> Estrelas { get; set; }
    
        public virtual Receita Receita { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
