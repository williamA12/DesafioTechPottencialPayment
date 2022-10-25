using Dominio.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.ViewModels
{
    /// <summary>
    /// Objeto contendo os dados para registrar a venda.
    /// </summary>
    public class VendaViewModel
    {
        /// <summary>
        /// Vendedor
        /// </summary>
        public VendedorViewModel Vendedor { get; set; }

        /// <summary>
        /// Itens da venda.
        /// </summary>
        public virtual ICollection<ItemVendaViewModel> Itens { get; set; }

        public Vendedor ObterVendedor()
        {
            return new Vendedor
            {
                Nome = Vendedor.Nome,
                Cpf = Vendedor.Cpf,
                Email = Vendedor.Email,
                Telefone = Vendedor.Telefone
            };
        }

        public List<ItemVenda> ObterItensVenda()
        {
            return Itens.Select(x=> new ItemVenda
            {
                Descricao = x.Descricao,
                Preco = x.Preco,
                Quantidade = x.Quantidade
            }).ToList();
        }
    }
}
