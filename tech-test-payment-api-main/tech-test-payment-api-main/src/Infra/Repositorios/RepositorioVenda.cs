using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.Repositorios
{
    public class RepositorioVenda : RepositorioBase<Venda>, IRepositorioVenda
    {
        public RepositorioVenda(AppDbContexto context) : base(context)
        {
        }

        public new async Task<Venda> ObterPorIdAsync(Expression<Func<Venda, bool>> predicado)
        {
            return await _context.Set<Venda>()
                .Include("Vendedor")
                .Include("Itens")
                .SingleOrDefaultAsync(predicado);
        }
    }
}
