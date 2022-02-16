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

namespace Business.Services.Contas
{
    public class ContaCadastroPessoaJuridicaService: IContaCadastroPessoaJuridicaService
    {
        private readonly IContaRepository _contaRepository;
        private readonly IPessoaJuridicaRepository _pessoaJuridicaRepository;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ICalculoDadosCadastroProximaContaService _calculoDadosCadastroProximaContaService;
        private readonly IValidator<ContaPessoaJuridicaCadastroDto> _validator;
        private readonly IPasswordEncoder _passwordEncoder;

        private ContaPessoaJuridicaCadastroDto _request;
        private DadosCadastroProximaContaDto _dadosCadastroProximaConta;

        private Conta _conta;
        private PessoaJuridica _pessoaJuridica;
        private Pessoa _pessoa;
        private Endereco _endereco;
        private PessoaEndereco _pessoaEndereco;
        private Usuario _usuario;

        public ContaCadastroPessoaJuridicaService(
            IContaRepository contaRepository,
            ICalculoDadosCadastroProximaContaService calculoDadosCadastroProximaContaService,
            IValidator<ContaPessoaJuridicaCadastroDto> validator,
            IPessoaJuridicaRepository pessoaJuridicaRepository,
            IPessoaRepository pessoaRepository,
            IPasswordEncoder passwordEncoder
        )
        {
            _contaRepository = contaRepository;
            _calculoDadosCadastroProximaContaService = calculoDadosCadastroProximaContaService;
            _validator = validator;
            _pessoaJuridicaRepository = pessoaJuridicaRepository;
            _pessoaRepository = pessoaRepository;
            _passwordEncoder = passwordEncoder;
        }

        public long Cadastrar(ContaPessoaJuridicaCadastroDto request)
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
            _request.Cnpj = Regex.Replace(_request.Cnpj, @"\D+", "");
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
            ValidateCnpj();
            ValidateEmail();
        }

        private void ValidateCnpj()
        {
            if (_pessoaJuridicaRepository.CnpjJaExiste(_request.Cnpj))
            {
                throw new BadRequestException(new Tuple<string, string>("cnpj", "Esse CNPJ já existe."));
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
            _pessoa.IsPessoaFisica = false;
            _pessoa.IsPessoaJuridica = true;
            _pessoa.DataCadastro = new DateTime();
            _pessoa.PessoaJuridica = MapPessoaJuridica();
            _pessoa.Usuario = MapUsuario();
            return _pessoa;
        }

        private PessoaJuridica MapPessoaJuridica()
        {
            _pessoaJuridica = new PessoaJuridica();
            _pessoaJuridica.Pessoa = _pessoa;
            _pessoaJuridica.Cnpj = _request.Cnpj;
            _pessoaJuridica.RazaoSocial = _request.RazaoSocial;
            _pessoaJuridica.NomeFantasia = _request.NomeFantasia;
            return _pessoaJuridica;
        }

        private Endereco MapEndereco()
        {
            _endereco = new Endereco();
            _endereco.PessoaEnderecos = new List<PessoaEndereco> {_pessoaEndereco};
            _endereco.Cep = _request.Cep;
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