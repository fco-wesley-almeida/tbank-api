using Core.Domains.Transacoes.Dtos;

namespace Core.Domains.Transacoes.Services
{
    public interface ISolicitacaoTransacaoCreditoService
    {
        long Solicitar(SolicitacaoTransacaoCreditoDto request);
    }
}