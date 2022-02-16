using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Core.CoreServices;
using Core.Domains.Contas.Dtos;
using Core.Domains.Contas.Services;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Infrastructure;

namespace Business.Services.Contas
{
    public class ContaCadastroPessoaFisicaService: IContaCadastroPessoaFisicaService
    {
        private readonly IContaRepository _contaRepository;
        private readonly IPessoaFisicaRepository _pessoaFisicaRepository;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ICalculoDadosCadastroProximaContaService _calculoDadosCadastroProximaContaService;
        private readonly IValidator<ContaPessoaFisicaCadastroDto> _validator;
        private readonly IPasswordEncoder _passwordEncoder;

        private ContaPessoaFisicaCadastroDto _request;
        private DadosCadastroProximaContaDto _dadosCadastroProximaConta;

        private Conta _conta;
        private PessoaFisica _pessoaFisica;
        private Pessoa _pessoa;
        private Endereco _endereco;
        private PessoaEndereco _pessoaEndereco;
        private Usuario _usuario;

        public ContaCadastroPessoaFisicaService(
            IContaRepository contaRepository,
            ICalculoDadosCadastroProximaContaService calculoDadosCadastroProximaContaService,
            IValidator<ContaPessoaFisicaCadastroDto> validator,
            IPessoaFisicaRepository pessoaFisicaRepository,
            IPessoaRepository pessoaRepository,
            IPasswordEncoder passwordEncoder
        )
        {
            _contaRepository = contaRepository;
            _calculoDadosCadastroProximaContaService = calculoDadosCadastroProximaContaService;
            _validator = validator;
            _pessoaFisicaRepository = pessoaFisicaRepository;
            _pessoaRepository = pessoaRepository;
            _passwordEncoder = passwordEncoder;
        }

        public long Cadastrar(ContaPessoaFisicaCadastroDto request)
        {
            _request = request;
            ValidateFormat();
            FormatRequest();
            ValidatePersistence();
            MapEntities();
            _contaRepository.Create(_conta);
            if (_conta.Id == 0)
            {
                throw new BadRequestException("Não foi possível cadastrar sua conta.");
            }
            return _conta.Id;
        }

        private void FormatRequest()
        {
            _request.Cpf = Regex.Replace(_request.Cpf, @"\D+", "");
            _request.Cep = Regex.Replace(_request.Cep, @"\D+", "");
            _request.Senha = _passwordEncoder.Encode(_request.Senha);
        }

        private void ValidateFormat()
        {
            ValidationResult validationResult = _validator.Validate(_request);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult);
            }
        }

        private void ValidatePersistence()
        {
            ValidateCpf();
            ValidateEmail();
        }

        private void ValidateCpf()
        {
            if (_pessoaFisicaRepository.CpfJaExiste(_request.Cpf))
            {
                throw new BadRequestException(new Tuple<string, string>("cpf", "Esse CPF já existe."));
            }
        }

        private void ValidateEmail()
        {
            if (_pessoaRepository.EmailJaExiste(_request.Email))
            {
                throw new BadRequestException(new Tuple<string, string>("email", "Esse email já existe."));
            }
        }

        private void MapEntities()
        {
            _dadosCadastroProximaConta = _calculoDadosCadastroProximaContaService.Calcular();
            MapConta();
        }

        private void MapConta()
        {
            _conta = new Conta();
            _conta.AgenciaId = _dadosCadastroProximaConta.AgenciaId;
            _conta.Codigo = _dadosCadastroProximaConta.ContaCodigo;
            _conta.DataCadastro = DateTime.Now;
            _conta.Digito = _dadosCadastroProximaConta.ContaDigito;
            _conta.Pessoa = MapPessoa();
        }

        private Pessoa MapPessoa()
        {
            _pessoa = new Pessoa();
            _pessoa.Conta = new List<Conta> {_conta};
            _pessoa.PessoaEnderecos = new List<PessoaEndereco> {MapPessoaEndereco()};
            _pessoa.Email = _request.Email;
            _pessoa.Telefone = _request.Telefone;
            _pessoa.IsPessoaFisica = true;
            _pessoa.IsPessoaJuridica = false;
            _pessoa.DataCadastro = new DateTime();
            _pessoa.PessoaFisica = MapPessoaFisica();
            _pessoa.Usuario = MapUsuario();
            return _pessoa;
        }

        private PessoaFisica MapPessoaFisica()
        {
            _pessoaFisica = new PessoaFisica();
            _pessoaFisica.Pessoa = _pessoa;
            _pessoaFisica.Cpf = Regex.Replace(_request.Cpf, @"\D+", "");
            _pessoaFisica.NomeCompleto = _request.NomeCompleto;
            _pessoaFisica.Rg = _request.Rg;
            return _pessoaFisica;
        }

        private Endereco MapEndereco()
        {
            _endereco = new Endereco();
            _endereco.PessoaEnderecos = new List<PessoaEndereco> {_pessoaEndereco};
            _endereco.Cep = Regex.Replace(_request.Cep, @"\D+", "");
            _endereco.Estado = _request.Estado;
            _endereco.Cidade = _request.Cidade;
            _endereco.Distrito = _request.Distrito;
            _endereco.Logradouro = _request.Logradouro;
            _endereco.Numero = _request.Numero;
            _endereco.Referencias = _request.Referencias;
            return _endereco;
        }

        private PessoaEndereco MapPessoaEndereco()
        {
            _pessoaEndereco = new PessoaEndereco();
            _pessoaEndereco.Pessoa = _pessoa;
            _pessoaEndereco.Endereco = MapEndereco();
            return _pessoaEndereco;
        }

        private Usuario MapUsuario()
        {
            _usuario = new Usuario();
            _usuario.Pessoa = _pessoa;
            _usuario.Senha = _request.Senha;
            return _usuario;
        }
    }
}