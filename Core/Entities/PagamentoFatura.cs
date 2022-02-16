using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class PagamentoFatura
    {
        public int Id { get; set; }
        public int TransacaoId { get; set; }
        public int FaturaId { get; set; }

        public virtual Fatura Fatura { get; set; }
        public virtual Transacao Transacao { get; set; }
    }
}
