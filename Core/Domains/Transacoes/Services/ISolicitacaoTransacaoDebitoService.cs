using Core.Domains.Transacoes.Dtos;

namespace Core.Domains.Transacoes.Services
{
    public interface ISolicitacaoTransacaoDebitoService
    {
        long Solicitar(SolicitacaoTransacaoDebitoDto request);
    }
}