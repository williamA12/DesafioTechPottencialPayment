using System;

namespace Dominio.Exceptions
{
    /// <summary>
    /// Classe de exception de alteração de status.
    /// </summary>
    [Serializable]
    public class AlterarStatusException : Exception
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="mensagem">Mensagem de erro</param>
        public AlterarStatusException(string mensagem = null) : this(mensagem, null)
        {
            if (mensagem == null)
            {
                throw new AlterarStatusException("Alteração de status da venda não permitida.");
            }
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="mensagem">Mensagem de erro</param>
        /// <param name="innerException">Inner exception</param>
        public AlterarStatusException(string mensagem, Exception innerException) : base(mensagem, innerException) { }
    }
}
