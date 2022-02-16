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
    public class SolicitacaoTransacaoDebitoService: ISolicitacaoTransacaoDebitoService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IValidator<SolicitacaoTransacaoDebitoDto> _validator;
        private readonly IContaSaldoDb _contaSaldoDb;

        private SolicitacaoTransacaoDebitoDto _request;
        private Transacao _transacao;
        private TransacaoDebito _transacaoDebito;
        public SolicitacaoTransacaoDebitoService(ITransacaoRepository transacaoRepository, IValidator<SolicitacaoTransacaoDebitoDto> validator, IContaSaldoDb contaSaldoDb)
        {
            _transacaoRepository = transacaoRepository;
            _validator = validator;
            _contaSaldoDb = contaSaldoDb;
        }

        public long Solicitar(SolicitacaoTransacaoDebitoDto request)
        {
            _request = request;
            ValidateFormat();
            ValidateSaldo();
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

        private void ValidateSaldo()
        {
            float saldo = _contaSaldoDb.FindSaldoConta(_request.ContaId);
            if (saldo < _request.Valor)
            {
                throw new BadRequestException("Você não tem saldo suficiente para realizar essa operação.");
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
            _transacao.TransacaoDebitos = new List<TransacaoDebito> { MapTransacaoDebito() };
        }

        private TransacaoDebito MapTransacaoDebito()
        {
            _transacaoDebito = new TransacaoDebito();
            _transacaoDebito.Transacao = _transacao;
            return _transacaoDebito;
        }

        private void Persist()
        {
            _transacaoRepository.Create(_transacao);
        }
    }
}