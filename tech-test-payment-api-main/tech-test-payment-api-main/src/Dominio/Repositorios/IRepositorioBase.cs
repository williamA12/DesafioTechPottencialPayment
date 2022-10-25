using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IRepositorioBase<T> where T : class
    {
        Task<T> ObterPorIdAsync(Expression<Func<T, bool>> predicado);
        Task<IEnumerable<T>> ObterTodosAsync();
        Task AdicionarAsync(T entidade);
        Task EditarAsync(T entidade);
        Task ExcluirAsync(T entidade);
    }
}
