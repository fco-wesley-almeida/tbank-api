using Core.Domains.Autenticacao.Dtos;

namespace Core.Data
{
    public interface ILoginDb
    {
        UsuarioParaAutenticarDto FindUsuarioParaAutenticar(LoginRequestDto request);
    }
}