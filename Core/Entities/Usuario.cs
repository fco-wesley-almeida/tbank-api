#nullable disable

namespace Core.Entities
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string Senha { get; set; }
        public int PessoaId { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}
