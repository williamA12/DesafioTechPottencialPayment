namespace Dominio.Enum
{
    /// <summary>
    /// Staus da venda enum
    /// </summary>
    public enum StatusVendaEnum
    {
        /// <summary>
        /// Aguardando Pagamento
        /// </summary>
        AguardandoPagamento = 1,

        /// <summary>
        /// Pagamento Aprovado
        /// </summary>
        PagamentoAprovado = 2,

        /// <summary>
        /// Enviado Para Transportadora
        /// </summary>
        EnviadoParaTransportadora = 3,

        /// <summary>
        /// Entregue
        /// </summary>
        Entregue = 4,

        /// <summary>
        /// Cancelada
        /// </summary>
        Cancelada = 5
    }
}
