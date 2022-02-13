using System.Threading.Tasks;
using Core.Domains.Autenticacao.Dtos;

namespace Core.Domains.Autenticacao.Services
{
    public interface IAutenticacaoService
    {
        LoginResponseDto Login(LoginRequestDto request);
    }
}