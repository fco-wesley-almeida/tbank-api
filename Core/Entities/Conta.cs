using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class Conta
    {
        public Conta()
        {
            Faturas = new HashSet<Fatura>();
            Transacaos = new HashSet<Transacao>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public int Digito { get; set; }
        public int AgenciaId { get; set; }
        public int PessoaId { get; set; }
        public DateTime DataCadastro { get; set; }
        public decimal LimiteDisponivel { get; set; }

        public virtual Agencia Agencia { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual ICollection<Fatura> Faturas { get; set; }
        public virtual ICollection<Transacao> Transacaos { get; set; }
    }
}
