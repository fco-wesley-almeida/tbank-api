using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class PessoaFisica
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string NomeCompleto { get; set; }
        public string Rg { get; set; }
        public int PessoaId { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}
