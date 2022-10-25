using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    /// <summary>
    /// Item da venda.
    /// </summary>
    public class ItemVendaViewModel
    {
        /// <summary>
        /// Descrição do item.
        /// </summary>
        [Required(ErrorMessage = "Informe uma descrição.", AllowEmptyStrings = false)]
        [MaxLength(200, ErrorMessage = "Limite máximo de {0} caracteres.")]
        public string Descricao { get; set; }

        /// <summary>
        /// Quantidade do item.
        /// </summary>
        [Required(ErrorMessage = "Informe a quantidade.")]
        [Range(1, int.MaxValue)]
        public int? Quantidade { get; set; }

        /// <summary>
        /// Preço do item.
        /// </summary>
        [Required(ErrorMessage = "Informe o preço.")]
        public decimal? Preco { get; set; }
    }
}
