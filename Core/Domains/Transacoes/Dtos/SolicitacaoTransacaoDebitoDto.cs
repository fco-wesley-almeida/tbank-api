namespace Core.Domains.Transacoes.Dtos
{
    public class SolicitacaoTransacaoDebitoDto
    {
        public long ContaId { get; set; }
        public float Valor { get; set; }
        public string Descricao { get; set; }
    }
}