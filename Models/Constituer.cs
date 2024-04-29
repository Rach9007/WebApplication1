using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Constituer
    {
        public string MedDepotlegal { get; set; } = null!;
        public string CmpCode { get; set; } = null!;
        public float? CstQte { get; set; }
        public byte[] SsmaTimeStamp { get; set; } = null!;

        public virtual Composant CmpCodeNavigation { get; set; } = null!;
        public virtual Medicament MedDepotlegalNavigation { get; set; } = null!;
    }
}
