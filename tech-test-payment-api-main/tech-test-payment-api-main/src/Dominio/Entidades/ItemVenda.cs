namespace Dominio.Entidades
{
    /// <summary>
    /// Item da venda.
    /// </summary>
    public class ItemVenda
    {
        public const int TamanhoMaximoDescricao = 200;
        /// <summary>
        /// Id do item de venda.
        /// </summary>
        public int ItemVendaId { get; set; }

        /// <summary>
        /// Id da venda.
        /// </summary>
        public int VendaId { get; set; }

        /// <summary>
        /// Objeto venda.
        /// </summary>
        public Venda Venda { get; set; }

        /// <summary>
        /// Descrição do item da venda.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Quantidade do item da venda.
        /// </summary>
        public int? Quantidade { get; set; }

        /// <summary>
        /// Preço do item da venda.
        /// </summary>
        public decimal? Preco { get; set; }
    }
}
