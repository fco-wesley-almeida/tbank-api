using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class Conta
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int Digito { get; set; }
        public int AgenciaId { get; set; }
        public int PessoaId { get; set; }
        public DateTime DataCadastro { get; set; }

        public virtual Agencia Agencia { get; set; }
        public virtual Pessoa Pessoa { get; set; }
    }
}
