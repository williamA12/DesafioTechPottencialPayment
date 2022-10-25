using Dominio.Enum;
using Dominio.Exceptions;
using System;
using System.Collections.Generic;

namespace Dominio.Entidades
{
    /// <summary>
    /// Venda
    /// </summary>
    public class Venda
    {
        /// <summary>
        /// Id da venda.
        /// </summary>
        public int VendaId { get; set; }

        /// <summary>
        /// Id do vendedor.
        /// </summary>
        public int VendedorId { get; set; }

        /// <summary>
        /// Data da venda.
        /// </summary>
        public DateTime DataVenda { get; set; }

        /// <summary>
        /// Objeto vendedor.
        /// </summary>
        public Vendedor Vendedor { get; set; }

        /// <summary>
        /// Status da venda.
        /// </summary>
        public StatusVendaEnum Status { get; internal set; }

        /// <summary>
        /// Lista de itens da venda.
        /// </summary>
        public virtual ICollection<ItemVenda> Itens { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        public Venda()
        {
            Itens = new List<ItemVenda>();
            DataVenda = DateTime.Now;
            Status = StatusVendaEnum.AguardandoPagamento;
        }

        /// <summary>
        /// Adiciona um novo item na venda
        /// </summary>
        /// <param name="itemVenda">Item a ser adicionado</param>
        public void AdicionarItem(ItemVenda itemVenda)
        {
            Itens.Add(itemVenda);
        }

        /// <summary>
        /// Altera o status da venda para Aguardando Pagamento
        /// </summary>
        public void AlterarStatusPagamentoAprovado()
        {
            if (Status != StatusVendaEnum.AguardandoPagamento)
            {
                throw new AlterarStatusException();
            }
            Status = StatusVendaEnum.PagamentoAprovado;
        }

        /// <summary>
        /// Altera o status da venda para Enviado Para Transportadora
        /// </summary>
        public void AlterarStatusEnviarTransportadora()
        {
            if (Status != StatusVendaEnum.PagamentoAprovado)
            {
                throw new AlterarStatusException();
            }
            Status = StatusVendaEnum.EnviadoParaTransportadora;
        }

        /// <summary>
        /// Altera o status da venda para Entregue
        /// </summary>
        public void AlterarStatusEntregar()
        {
            if (Status != StatusVendaEnum.EnviadoParaTransportadora)
            {
                throw new AlterarStatusException();
            }
            Status = StatusVendaEnum.Entregue;
        }

        /// <summary>
        /// Altera o status da venda para Cancelada
        /// </summary>
        public void AlterarStatusCancelar()
        {
            Status = StatusVendaEnum.Cancelada;
        }
    }
}
