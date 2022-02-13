namespace Core.Domains.Autenticacao.Dtos
{
    public class UsuarioParaAutenticarDto
    {
        public long UsuarioId { get; set; }
        public string Senha { get; set; }
    }
}