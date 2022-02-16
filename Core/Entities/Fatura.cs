using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class Fatura
    {
        public Fatura()
        {
            PagamentoFaturas = new HashSet<PagamentoFatura>();
        }

        public int Id { get; set; }
        public int ContaId { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public bool Pago { get; set; }
        public int Mes { get; set; }

        public virtual Conta Conta { get; set; }
        public virtual ICollection<PagamentoFatura> PagamentoFaturas { get; set; }
    }
}
