using System;
using System.Collections.Generic;
using Core.Domains.Contas.Dtos;
using Core.Domains.Contas.Services;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;

namespace Business.Services.Contas
{
    public class ContaCadastroPessoaFisicaService: IContaCadastroPessoaFisicaService
    {
        private readonly IContaRepository _contaRepository;
        private readonly ICalculoDadosCadastroProximaContaService _calculoDadosCadastroProximaContaService;

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
            ICalculoDadosCadastroProximaContaService calculoDadosCadastroProximaContaService
        )
        {
            _contaRepository = contaRepository;
            _calculoDadosCadastroProximaContaService = calculoDadosCadastroProximaContaService;
        }

        public long Cadastrar(ContaPessoaFisicaCadastroDto request)
        {
            _request = request;
            MapEntities();
            _contaRepository.Create(_conta);
            if (_conta.Id == 0)
            {
                throw new BadRequestException();
            }
            return _conta.Id;
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
            _conta.DataCadastro = new DateTime();
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
            _pessoaFisica.Cpf = _request.Cpf;
            _pessoaFisica.NomeCompleto = _request.NomeCompleto;
            _pessoaFisica.Rg = _request.Rg;
            return _pessoaFisica;
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