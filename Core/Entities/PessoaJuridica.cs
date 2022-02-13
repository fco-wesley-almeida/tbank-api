using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class PessoaJuridica
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public int PessoaId { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}
