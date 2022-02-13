using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class Agencia
    {
        public Agencia()
        {
            Conta = new HashSet<Conta>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }

        public virtual ICollection<Conta> Conta { get; set; }
    }
}
