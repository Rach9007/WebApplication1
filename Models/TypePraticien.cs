using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class TypePraticien
    {
        public TypePraticien()
        {
            Praticiens = new HashSet<Praticien>();
        }

        public string TypCode { get; set; } = null!;
        public string? TypLibelle { get; set; }
        public string? TypLieu { get; set; }

        public virtual ICollection<Praticien> Praticiens { get; set; }
    }
}
