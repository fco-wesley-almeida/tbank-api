using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class Endereco
    {
        public Endereco()
        {
            PessoaEnderecos = new HashSet<PessoaEndereco>();
        }

        public int Id { get; set; }
        public string Cep { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Distrito { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Referencias { get; set; }

        public virtual ICollection<PessoaEndereco> PessoaEnderecos { get; set; }
    }
}
