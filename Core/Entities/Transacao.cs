using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class Transacao
    {
        public Transacao()
        {
            PagamentoFaturas = new HashSet<PagamentoFatura>();
            TransacaoCreditos = new HashSet<TransacaoCredito>();
            TransacaoDebitos = new HashSet<TransacaoDebito>();
            TransacaoReceita = new HashSet<TransacaoReceitum>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Time { get; set; }
        public int ContaId { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }

        public virtual Conta Conta { get; set; }
        public virtual ICollection<PagamentoFatura> PagamentoFaturas { get; set; }
        public virtual ICollection<TransacaoCredito> TransacaoCreditos { get; set; }
        public virtual ICollection<TransacaoDebito> TransacaoDebitos { get; set; }
        public virtual ICollection<TransacaoReceitum> TransacaoReceita { get; set; }
    }
}
