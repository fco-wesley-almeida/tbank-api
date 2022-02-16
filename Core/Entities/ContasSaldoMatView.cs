using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class ContasSaldoMatView
    {
        public int? ContaId { get; set; }
        public string ContaCodigo { get; set; }
        public decimal? Saldo { get; set; }
    }
}
