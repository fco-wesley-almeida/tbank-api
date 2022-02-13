namespace Core.Domains.Autenticacao.Dtos
{
    public class LoginResponseDto
    {
        public long UserId { get; set; }
        public string Token { get; set; }
    }
}