using System;
using System.Collections.Generic;
using System.Linq;
using Core.Data;
using Core.Domains.Contas.Dtos;
using Core.Domains.Contas.Services;
using Core.Enums;

namespace Business.Services.Contas
{
    public class CalculoDadosCadastroProximaContaService: ICalculoDadosCadastroProximaContaService
    {
        private IContaCodigoDb _contaCodigoDb;
        private DadosCadastroProximaContaDto _dadosCadastroProximaConta;

        public CalculoDadosCadastroProximaContaService(IContaCodigoDb contaCodigoDb)
        {
            _contaCodigoDb = contaCodigoDb;
        }

        public DadosCadastroProximaContaDto Calcular()
        {
            _dadosCadastroProximaConta = new DadosCadastroProximaContaDto();
            CalcularContaCodigo();
            CalcularContaDigito();
            CalcularAgenciaId();
            return _dadosCadastroProximaConta;
        }

        private void CalcularContaCodigo()
        {
            int lastCodigo = _contaCodigoDb.FindLastContaCodigo();
            if (lastCodigo >= 999999999)
            {
                throw new Exception("O sistema chegou ao número máximo de contas.");
            }
            int nextCodigo = lastCodigo + 1;
            _dadosCadastroProximaConta.ContaCodigo = nextCodigo.ToString("00000000");
        }

        private void CalcularContaDigito()
        {
            /*
             * Nesse método foi implementado um algoritmo de cálculo de dígito verificador
             * inspirado no do Banco do Brasil, com algumas simplificações.
             * Fonte: http://177.153.6.25/ercompany.com.br/boleto/laravel-boleto-master/manuais/Regras%20Validacao%20Conta%20Corrente%20VI_EPS.pdf
            */

            List<int> multiplicadores = new List<int> {9, 8, 7, 6, 5, 4, 3, 2, 1};
            string codigo = _dadosCadastroProximaConta.ContaCodigo;
            int somaAlgarismos = multiplicadores
                .Select((multiplicador, i) => multiplicador * codigo[i])
                .Aggregate((acc, n) => acc + n)
            ;
            int digito = 11 - somaAlgarismos % 11;
            _dadosCadastroProximaConta.ContaDigito = digito >= 10 ? 0 : digito;
        }

        private void CalcularAgenciaId()
        {
            _dadosCadastroProximaConta.AgenciaId = (int)AgenciasEnum.Default;
        }
    }
}