using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace Infrastructure
{
    public partial class TBankDbContext : DbContext
    {
        private readonly IConfiguration Configuration;
        public TBankDbContext()
        {
        }

        public TBankDbContext(DbContextOptions<TBankDbContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public virtual DbSet<Agencia> Agencia { get; set; }
        public virtual DbSet<ContasSaldoMatView> ContasSaldoMatViews { get; set; }
        public virtual DbSet<Conta> Conta { get; set; }
        public virtual DbSet<Endereco> Enderecos { get; set; }
        public virtual DbSet<Fatura> Faturas { get; set; }
        public virtual DbSet<PagamentoFatura> PagamentoFaturas { get; set; }
        public virtual DbSet<Pessoa> Pessoas { get; set; }
        public virtual DbSet<PessoaEndereco> PessoaEnderecos { get; set; }
        public virtual DbSet<PessoaFisica> PessoaFisicas { get; set; }
        public virtual DbSet<PessoaJuridica> PessoaJuridicas { get; set; }
        public virtual DbSet<Transacao> Transacaos { get; set; }
        public virtual DbSet<TransacaoCredito> TransacaoCreditos { get; set; }
        public virtual DbSet<TransacaoDebito> TransacaoDebitos { get; set; }
        public virtual DbSet<TransacaoReceita> TransacaoReceita { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(Configuration.GetConnectionString("TBankContext"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agencia>(entity =>
            {
                entity.ToTable("agencia");

                entity.HasIndex(e => e.Codigo, "uk_agencia_codigo")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(10)
                    .HasColumnName("codigo");

                entity.Property(e => e.Digito).HasColumnName("digito");
            });

            modelBuilder.Entity<ContasSaldoMatView>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("contas_saldo_mat_view");

                entity.Property(e => e.ContaCodigo)
                    .HasMaxLength(20)
                    .HasColumnName("conta_codigo");

                entity.Property(e => e.ContaId).HasColumnName("conta_id");

                entity.Property(e => e.Saldo)
                    .HasColumnType("money")
                    .HasColumnName("saldo");
            });

            modelBuilder.Entity<Conta>(entity =>
            {
                entity.ToTable("conta");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AgenciaId).HasColumnName("agencia_id");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("codigo")
                    .HasDefaultValueSql("''::character varying");

                entity.Property(e => e.DataCadastro)
                    .HasColumnType("date")
                    .HasColumnName("data_cadastro");

                entity.Property(e => e.Digito).HasColumnName("digito");

                entity.Property(e => e.LimiteDisponivel)
                    .HasColumnType("money")
                    .HasColumnName("limite_disponivel");

                entity.Property(e => e.PessoaId).HasColumnName("pessoa_id");

                entity.HasOne(d => d.Agencia)
                    .WithMany(p => p.Conta)
                    .HasForeignKey(d => d.AgenciaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_conta_agencia_id");

                entity.HasOne(d => d.Pessoa)
                    .WithMany(p => p.Conta)
                    .HasForeignKey(d => d.PessoaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_conta_pessoa_id");
            });

            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.ToTable("endereco");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cep)
                    .IsRequired()
                    .HasMaxLength(8)
                    .HasColumnName("cep");

                entity.Property(e => e.Cidade)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("cidade");

                entity.Property(e => e.Distrito)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("distrito");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("estado");

                entity.Property(e => e.Logradouro)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("logradouro");

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("numero");

                entity.Property(e => e.Referencias)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("referencias");
            });

            modelBuilder.Entity<Fatura>(entity =>
            {
                entity.ToTable("fatura");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ContaId).HasColumnName("conta_id");

                entity.Property(e => e.DataVencimento)
                    .HasColumnType("date")
                    .HasColumnName("data_vencimento");

                entity.Property(e => e.Mes).HasColumnName("mes");

                entity.Property(e => e.Pago).HasColumnName("pago");

                entity.Property(e => e.Valor)
                    .HasColumnType("money")
                    .HasColumnName("valor");

                entity.HasOne(d => d.Conta)
                    .WithMany(p => p.Faturas)
                    .HasForeignKey(d => d.ContaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_fatura_conta_id");
            });

            modelBuilder.Entity<PagamentoFatura>(entity =>
            {
                entity.ToTable("pagamento_fatura");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FaturaId).HasColumnName("fatura_id");

                entity.Property(e => e.TransacaoId).HasColumnName("transacao_id");

                entity.HasOne(d => d.Fatura)
                    .WithMany(p => p.PagamentoFaturas)
                    .HasForeignKey(d => d.FaturaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_pagamento_fatura_fatura_id");

                entity.HasOne(d => d.Transacao)
                    .WithMany(p => p.PagamentoFaturas)
                    .HasForeignKey(d => d.TransacaoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_pagamento_fatura_transacao_id");
            });

            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.ToTable("pessoa");

                entity.HasIndex(e => e.Email, "uk_pessoa_email")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DataCadastro)
                    .HasColumnType("date")
                    .HasColumnName("data_cadastro");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("email")
                    .HasDefaultValueSql("''::character varying");

                entity.Property(e => e.IsPessoaFisica).HasColumnName("is_pessoa_fisica");

                entity.Property(e => e.IsPessoaJuridica).HasColumnName("is_pessoa_juridica");

                entity.Property(e => e.Telefone)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("telefone")
                    .HasDefaultValueSql("''::character varying");
            });

            modelBuilder.Entity<PessoaEndereco>(entity =>
            {
                entity.HasKey(e => new { e.PessoaId, e.EnderecoId })
                    .HasName("pessoa_endereco_pkey");

                entity.ToTable("pessoa_endereco");

                entity.Property(e => e.PessoaId).HasColumnName("pessoa_id");

                entity.Property(e => e.EnderecoId).HasColumnName("endereco_id");

                entity.HasOne(d => d.Endereco)
                    .WithMany(p => p.PessoaEnderecos)
                    .HasForeignKey(d => d.EnderecoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_pessoa_endereco_endereco_id");

                entity.HasOne(d => d.Pessoa)
                    .WithMany(p => p.PessoaEnderecos)
                    .HasForeignKey(d => d.PessoaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_pessoa_endereco_pessoa_id");
            });

            modelBuilder.Entity<PessoaFisica>(entity =>
            {
                entity.ToTable("pessoa_fisica");

                entity.HasIndex(e => e.Cpf, "uk_pessoa_fisica_cpf")
                    .IsUnique();

                entity.HasIndex(e => e.PessoaId, "uk_pessoa_fisica_pessoa_id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("cpf");

                entity.Property(e => e.NomeCompleto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nome_completo");

                entity.Property(e => e.PessoaId).HasColumnName("pessoa_id");

                entity.Property(e => e.Rg)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("rg");

                entity.HasOne(d => d.Pessoa)
                    .WithOne(p => p.PessoaFisica)
                    .HasForeignKey<PessoaFisica>(d => d.PessoaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_pessoa_fisica_pessoa_id");
            });

            modelBuilder.Entity<PessoaJuridica>(entity =>
            {
                entity.ToTable("pessoa_juridica");

                entity.HasIndex(e => e.Cnpj, "uk_pessoa_juridica_cnpj")
                    .IsUnique();

                entity.HasIndex(e => e.PessoaId, "uk_pessoa_juridica_pessoa_id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cnpj)
                    .IsRequired()
                    .HasMaxLength(14)
                    .HasColumnName("cnpj");

                entity.Property(e => e.NomeFantasia)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nome_fantasia");

                entity.Property(e => e.PessoaId).HasColumnName("pessoa_id");

                entity.Property(e => e.RazaoSocial)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("razao_social");

                entity.HasOne(d => d.Pessoa)
                    .WithOne(p => p.PessoaJuridica)
                    .HasForeignKey<PessoaJuridica>(d => d.PessoaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_pessoa_juridica_pessoa_id");
            });

            modelBuilder.Entity<Transacao>(entity =>
            {
                entity.ToTable("transacao");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("codigo");

                entity.Property(e => e.ContaId).HasColumnName("conta_id");

                entity.Property(e => e.Data)
                    .HasColumnType("date")
                    .HasColumnName("data");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("descricao")
                    .HasDefaultValueSql("''::character varying");

                entity.Property(e => e.Time)
                    .HasColumnType("time without time zone")
                    .HasColumnName("time");

                entity.Property(e => e.Valor)
                    .HasColumnType("money")
                    .HasColumnName("valor");

                entity.HasOne(d => d.Conta)
                    .WithMany(p => p.Transacaos)
                    .HasForeignKey(d => d.ContaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_transacao_conta_id");
            });

            modelBuilder.Entity<TransacaoCredito>(entity =>
            {
                entity.ToTable("transacao_credito");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TransacaoId).HasColumnName("transacao_id");

                entity.HasOne(d => d.Transacao)
                    .WithMany(p => p.TransacaoCreditos)
                    .HasForeignKey(d => d.TransacaoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_transacao_credito_transacao_id");
            });

            modelBuilder.Entity<TransacaoDebito>(entity =>
            {
                entity.ToTable("transacao_debito");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TransacaoId).HasColumnName("transacao_id");

                entity.HasOne(d => d.Transacao)
                    .WithMany(p => p.TransacaoDebitos)
                    .HasForeignKey(d => d.TransacaoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_transacao_debito_transacao_id");
            });

            modelBuilder.Entity<TransacaoReceita>(entity =>
            {
                entity.ToTable("transacao_receita");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TransacaoId).HasColumnName("transacao_id");

                entity.HasOne(d => d.Transacao)
                    .WithMany(p => p.TransacaoReceita)
                    .HasForeignKey(d => d.TransacaoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_transacao_receita_transacao_id");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");

                entity.HasIndex(e => e.PessoaId, "uk_usuario_pessoa_id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PessoaId).HasColumnName("pessoa_id");

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("senha");

                entity.HasOne(d => d.Pessoa)
                    .WithOne(p => p.Usuario)
                    .HasForeignKey<Usuario>(d => d.PessoaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_usuario_pessoa_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
