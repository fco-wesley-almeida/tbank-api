using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class Pessoa
    {
        public Pessoa()
        {
            Conta = new HashSet<Conta>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public bool IsPessoaFisica { get; set; }
        public bool IsPessoaJuridica { get; set; }
        public DateTime DataCadastro { get; set; }

        public virtual PessoaFisica PessoaFisica { get; set; }
        public virtual PessoaJuridica PessoaJuridica { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Conta> Conta { get; set; }
    }
}
