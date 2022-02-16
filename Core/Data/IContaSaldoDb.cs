namespace Core.Data
{
    public interface IContaSaldoDb
    {
        float FindSaldoConta(long contaId);
        float FindLimiteDisponivel(long contaId);
    }
}