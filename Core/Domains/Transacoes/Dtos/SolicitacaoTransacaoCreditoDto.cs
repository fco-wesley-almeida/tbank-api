namespace Core.Domains.Transacoes.Dtos
{
    public class SolicitacaoTransacaoCreditoDto
    {
        public long ContaId { get; set; }
        public float Valor { get; set; }
        public string Descricao { get; set; }
    }
}