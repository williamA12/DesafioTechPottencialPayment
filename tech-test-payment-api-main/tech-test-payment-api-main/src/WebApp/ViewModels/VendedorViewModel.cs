using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    /// <summary>
    /// Vendedor
    /// </summary>
    public class VendedorViewModel
    {
        /// <summary>
        /// Nome do vendedor.
        /// </summary>
        [Required(ErrorMessage = "Informe o nome do vendedor.", AllowEmptyStrings = false)]
        [MaxLength(200, ErrorMessage = "Tamanho máximo de {0} caracters.")]
        public string Nome { get; set; }

        /// <summary>
        /// Email do vendedor.
        /// </summary>
        [Required(ErrorMessage = "Informe o e-mail do vendedor.", AllowEmptyStrings = false)]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um e-mail válido.")]
        public string Email { get; set; }

        /// <summary>
        /// CPF do vendedor.
        /// </summary>
        [Required(ErrorMessage = "Informe o CPF do vendedor.", AllowEmptyStrings = false)]
        public string Cpf { get; set; }

        /// <summary>
        /// DDD Telefone do Vendedor.
        /// </summary>
        [Required(ErrorMessage = "Informe o DDD Telefone do vendedor.", AllowEmptyStrings = false)]
        public string Telefone { get; set; }
    }
}
