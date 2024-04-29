using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Realiser
    {
        public int AcNum { get; set; }
        public string VisMatricule { get; set; } = null!;
        public float? ReaMttfrais { get; set; }
        public byte[] SsmaTimeStamp { get; set; } = null!;

        public virtual ActiviteCompl AcNumNavigation { get; set; } = null!;
        public virtual Visiteur VisMatriculeNavigation { get; set; } = null!;
    }
}
