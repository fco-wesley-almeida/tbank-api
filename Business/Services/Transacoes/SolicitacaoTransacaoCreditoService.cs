using System;
using System.Collections.Generic;
using Core.Data;
using Core.Domains.Transacoes.Dtos;
using Core.Domains.Transacoes.Services;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using FluentValidation;
using FluentValidation.Results;

namespace Business.Services.Transacoes
{
    public class SolicitacaoTransacaoCreditoService: ISolicitacaoTransacaoCreditoService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IFaturaRepository _faturaRepository;
        private readonly IContaRepository _contaRepository;
        private readonly IValidator<SolicitacaoTransacaoCreditoDto> _validator;
        private readonly IContaSaldoDb _contaSaldoDb;

        private SolicitacaoTransacaoCreditoDto _request;
        private Transacao _transacao;
        private TransacaoCredito _transacaoCredito;
        private long _faturaId;
        public SolicitacaoTransacaoCreditoService(ITransacaoRepository transacaoRepository, IValidator<SolicitacaoTransacaoCreditoDto> validator, IContaSaldoDb contaSaldoDb, IFaturaRepository faturaRepository, IContaRepository contaRepository)
        {
            _transacaoRepository = transacaoRepository;
            _validator = validator;
            _contaSaldoDb = contaSaldoDb;
            _faturaRepository = faturaRepository;
            _contaRepository = contaRepository;
        }

        public long Solicitar(SolicitacaoTransacaoCreditoDto request)
        {
            _request = request;
            ValidateFormat();
            ValidateSaldo();
            CadastrarFaturaSeNecessario();
            MapEntities();
            Persist();
            return _transacao.Id;
        }

        private void ValidateFormat()
        {
            ValidationResult validationResult = _validator.Validate(_request);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult);
            }
        }

        private void CadastrarFaturaSeNecessario()
        {
            Fatura fatura = _faturaRepository.FindByContaId(_request.ContaId);
            if (fatura is null)
            {
                Fatura novaFatura = new Fatura
                {
                    ContaId = (int) _request.ContaId,
                    Valor = (decimal) _request.Valor,
                    DataVencimento = DateTime.Today,
                    Pago = false,
                    Mes = DateTime.Today.Month
                };
                _faturaRepository.Create(novaFatura);
                _faturaId = novaFatura.Id;
                return;
            }

            _faturaId = fatura.Id;

            if (fatura.Pago)
            {
                fatura.Pago = false;
                fatura.Valor = (decimal) _request.Valor;
                if (fatura.Valor <= _contaRepository.FindById(fatura.ContaId).LimiteDisponivel)
                {
                    _faturaRepository.Update(fatura);
                }
                else
                {
                    throw new BadRequestException("Você não limite suficiente para realizar essa operação.");
                }
                return;
            }

            decimal novoValorFatura = fatura.Valor + (decimal) _request.Valor;
            if (novoValorFatura > _contaRepository.FindById(fatura.ContaId).LimiteDisponivel)
            {
                throw new BadRequestException("Você não limite suficiente para realizar essa operação.");
            }

            fatura.Valor = novoValorFatura;
            _faturaRepository.Update(fatura);
        }

        private void ValidateSaldo()
        {
            float saldo = _contaSaldoDb.FindLimiteDisponivel(_request.ContaId);
            if (saldo < _request.Valor)
            {
                throw new BadRequestException("Você não tem créditos suficientes para realizar essa operação.");
            }
        }

        private void MapEntities()
        {
            MapTransacao();
        }

        private void MapTransacao()
        {
            _transacao = new Transacao();
            _transacao.ContaId = (int)_request.ContaId;
            _transacao.Codigo = Guid.NewGuid().ToString();
            _transacao.Data = DateTime.Now;
            _transacao.Time = DateTime.Now.TimeOfDay;
            _transacao.Valor = (decimal) _request.Valor;
            _transacao.Descricao = _request.Descricao;
            _transacao.TransacaoCreditos = new List<TransacaoCredito> { MapTransacaoCredito() };
        }

        private TransacaoCredito MapTransacaoCredito()
        {
            _transacaoCredito = new TransacaoCredito();
            _transacaoCredito.Transacao = _transacao;
            _transacaoCredito.FaturaId = (int) _faturaId;
            return _transacaoCredito;
        }

        private void Persist()
        {
            _transacaoRepository.Create(_transacao);
        }
    }
}