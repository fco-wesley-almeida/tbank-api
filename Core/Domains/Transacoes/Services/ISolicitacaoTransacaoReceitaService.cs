using Core.Domains.Transacoes.Dtos;

namespace Core.Domains.Transacoes.Services
{
    public interface ISolicitacaoTransacaoReceitaService
    {
        long Solicitar(SolicitacaoTransacaoReceitaDto request);
    }
}