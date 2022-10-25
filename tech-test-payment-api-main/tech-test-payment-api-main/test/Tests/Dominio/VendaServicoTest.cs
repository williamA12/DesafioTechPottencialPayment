using Dominio.Entidades;
using Dominio.Enum;
using Dominio.Exceptions;
using Dominio.Repositorios;
using Dominio.Servicos;
using Infra.Data;
using Infra.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Dominio
{
    public class VendaServicoTest
    {
        #region [[ Campos privados ]]

        private readonly AppDbContexto _appDbContexto;
        private readonly VendaServico _vendaServico;
        private readonly IRepositorioVenda _repositorioVenda;

        #endregion

        #region [[ Ctor ]]
        
        public VendaServicoTest()
        {
            _appDbContexto = new AppDbContexto(ObterDatabaseInMemory());
            _appDbContexto.Database.EnsureDeleted();
            _repositorioVenda = new RepositorioVenda(_appDbContexto);
            _vendaServico = new VendaServico(_repositorioVenda);
        }

        #endregion

        #region [[ Registrar Venda Tests ]]
        
        [Fact]
        public async Task DeveRegistrarVendaSucesso()
        {
            var vendedor = ObterVendedor();
            var itemVenda = ObterItemVenda();

            await _vendaServico.RegistrarVenda(vendedor, new List<ItemVenda> { itemVenda });
            var venda = await _vendaServico.BuscarVenda(1);

            Assert.Equal(StatusVendaEnum.AguardandoPagamento, venda.Status);
            Assert.Equal(vendedor.Cpf, venda.Vendedor.Cpf);
            Assert.Equal(1, venda.Itens.Count);
        }

        [Fact]
        public async Task DeveFalharRegistrarVendaSemQuantidadeMinimaItemVenda()
        {
            var vendedor = ObterVendedor();

            var argumentException = await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _vendaServico.RegistrarVenda(vendedor, new List<ItemVenda>());
            });
            Assert.Equal("Deve ser infomado pelo meno um 'item da venda'.", argumentException.Message);
        }

        #endregion

        #region [[ Buscar Venda Tests ]]

        [Fact]
        public async Task DeveBuscarVendaPorIdSucesso()
        {
            var esperado = 1;
            var vendedor = ObterVendedor();
            var itemVenda = ObterItemVenda();
            var venda = new Venda {Vendedor = vendedor};
            venda.AdicionarItem(itemVenda);

            await _repositorioVenda.AdicionarAsync(venda);
            var atual = await _vendaServico.BuscarVenda(esperado);

            Assert.Equal(esperado, atual.VendaId);
            Assert.Equal(StatusVendaEnum.AguardandoPagamento, atual.Status);
            Assert.Equal(vendedor.Cpf, atual.Vendedor.Cpf);
            Assert.Equal(1, venda.Itens.Count);
        }

        [Fact]
        public async Task DeveFalharBuscarVendaPorId()
        {
            var vendedor = ObterVendedor();
            var itemVenda = ObterItemVenda();
            var venda = new Venda { Vendedor = vendedor };
            venda.AdicionarItem(itemVenda);

            await _repositorioVenda.AdicionarAsync(venda);

            var argumentException = await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _vendaServico.BuscarVenda(null);
            });
            Assert.Equal("Deve ser informado o 'id' de venda.", argumentException.Message);
        }

        #endregion
        
        #region [[ Atualizar Venda Tests ]]
        
        [Fact]
        public async Task DeveAlterarStatusAguardandoPagamentoParaPagamentoAprovadoSucesso()
        {
            var esperado = 1;
            var venda = await ObterVenda();

            await _vendaServico.AtualizarVenda(venda, StatusVendaEnum.PagamentoAprovado);

            Assert.Equal(esperado, venda.VendaId);
            Assert.Equal(StatusVendaEnum.PagamentoAprovado, venda.Status);
        }

        [Fact]
        public async Task DeveFalharAlterarStatusVendaCancelada()
        {
            var venda = await ObterVenda();
            venda.AlterarStatusCancelar();
            
            var alterarStatusException = await Assert.ThrowsAsync<AlterarStatusException>(async () =>
            {
                await _vendaServico.AtualizarVenda(venda, StatusVendaEnum.PagamentoAprovado);
            });
            Assert.Equal("Alteração de status da venda não permitida.", alterarStatusException.Message);
        }

        #endregion

        #region [[ Helper ]]

        public Vendedor ObterVendedor()
        {
            return new Vendedor { Cpf = "06583392705", Email = "email@provedor.com.br", Nome = "Maria do Carmo", Telefone = "31986688587" };
        }

        public ItemVenda ObterItemVenda()
        {
            return new ItemVenda { Descricao = "Mouse USB", Preco = 29.99M, Quantidade = 1 };
        }

        public async Task<Venda> ObterVenda()
        {
            var id = 1;
            var vendedor = ObterVendedor();
            var itemVenda = ObterItemVenda();
            var venda = new Venda { Vendedor = vendedor};
            venda.AdicionarItem(itemVenda);

            await _repositorioVenda.AdicionarAsync(venda);

            return await _repositorioVenda.ObterPorIdAsync(x=>x.VendaId == id);
        }

        public DbContextOptions<AppDbContexto> ObterDatabaseInMemory()
        {
            var builder = new DbContextOptionsBuilder<AppDbContexto>();
            builder.UseInMemoryDatabase("InMemoryDatabase");
            var options = builder.Options;
            return options;
        }
        
        #endregion
    }
}
