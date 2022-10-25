using Dominio.Entidades;
using Dominio.Enum;
using Dominio.Exceptions;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Servicos
{
    /// <summary>
    /// Venda Serviço
    /// </summary>
    public class VendaServico
    {
        private readonly IRepositorioVenda _repositorioVenda;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repositorioVenda">Repositório Venda</param>
        public VendaServico(IRepositorioVenda repositorioVenda)
        {
            _repositorioVenda = repositorioVenda;
        }
        
        /// <summary>
        /// Registrar venda: Recebe os dados do vendedor + itens vendidos.
        /// Registra venda com status "Aguardando pagamento"
        /// </summary>
        /// <param name="vendedor">Dados do vendedor.</param>
        /// <param name="itensVenda">Lista com dados dos itens da venda.</param>
        /// <returns></returns>
        public async Task RegistrarVenda(Vendedor vendedor, List<ItemVenda> itensVenda)
        {
            if (vendedor == null)
            {
                throw new ArgumentException("Deve ser informado um 'vendedor'.");
            }

            if (itensVenda == null || !itensVenda.Any())
            {
                throw new ArgumentException("Deve ser infomado pelo meno um 'item da venda'.");
            }

            var venda = new Venda { Vendedor = vendedor };

            foreach (var itemVenda in itensVenda)
            {
                venda.AdicionarItem(itemVenda);
            }

            await _repositorioVenda.AdicionarAsync(venda);
        }
        
        /// <summary>
        /// Busca pelo Id da venda.
        /// </summary>
        /// <param name="vendaId">Id da venda.</param>
        /// <returns>
        /// Dados da venda de id informado.
        /// Exception caso não seja encontrada.
        /// </returns>
        public async Task<Venda> BuscarVenda(int? vendaId)
        {
            if (vendaId == null)
            {
                throw new ArgumentException("Deve ser informado o 'id' de venda.");
            }
            var venda = await _repositorioVenda.ObterPorIdAsync(x => x.VendaId == vendaId);
            if (venda == null)
            {
                throw new Exception("Não existe venda com o 'id' informado.");
            }
            return venda;
        }

        // Atualizar venda: 

        /// <summary>
        /// Permite que seja atualizado o status da venda
        /// De: Aguardando pagamento Para: Pagamento Aprovado
        /// De: Aguardando pagamento Para: Cancelada
        /// De: Pagamento Aprovado Para: Enviado para Transportadora
        /// De: Pagamento Aprovado Para: Cancelada
        /// De: Enviado para Transportador.Para: Entregue
        /// </summary>
        /// <param name="venda">Dados da venda.</param>
        /// <param name="status">Novo status da venda.</param>
        /// <returns></returns>
        public async Task AtualizarVenda(Venda venda, StatusVendaEnum status)
        {
            switch (status)
            {
                case StatusVendaEnum.PagamentoAprovado:
                    venda.AlterarStatusPagamentoAprovado();
                    break;
                case StatusVendaEnum.EnviadoParaTransportadora:
                    venda.AlterarStatusEnviarTransportadora();
                    break;
                case StatusVendaEnum.Entregue:
                    venda.AlterarStatusEntregar();
                    break;
                case StatusVendaEnum.Cancelada:
                    venda.AlterarStatusCancelar();
                    break;
                default:
                    throw new AlterarStatusException();
            }
            await _repositorioVenda.EditarAsync(venda);
        }
    }
}
