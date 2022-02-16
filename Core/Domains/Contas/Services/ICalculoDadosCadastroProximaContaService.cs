using Core.Domains.Contas.Dtos;

namespace Core.Domains.Contas.Services
{
    public interface ICalculoDadosCadastroProximaContaService
    {
        DadosCadastroProximaContaDto Calcular();
    }
}