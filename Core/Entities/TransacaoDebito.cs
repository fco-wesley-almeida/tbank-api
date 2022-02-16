using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class TransacaoDebito
    {
        public int Id { get; set; }
        public int TransacaoId { get; set; }

        public virtual Transacao Transacao { get; set; }
    }
}
