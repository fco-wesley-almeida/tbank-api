using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class PessoaEndereco
    {
        public int PessoaId { get; set; }
        public int EnderecoId { get; set; }

        public virtual Endereco Endereco { get; set; }
        public virtual Pessoa Pessoa { get; set; }
    }
}
