﻿using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Specialite
    {
        public Specialite()
        {
            Posseders = new HashSet<Posseder>();
        }

        public string SpeCode { get; set; } = null!;
        public string? SpeLibelle { get; set; }

        public virtual ICollection<Posseder> Posseders { get; set; }
    }
}
