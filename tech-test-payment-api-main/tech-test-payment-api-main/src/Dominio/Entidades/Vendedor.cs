using System;
using System.Collections.Generic;

namespace Dominio.Entidades
{
    /// <summary>
    /// Vendedor
    /// </summary>
    public class Vendedor
    {
        #region Constantes
        
        public const int TamanhoMaximoNome = 200;
        public const int TamanhoMaximoEmail = 200;
        public const int TamanhoMaximoCpf = 11;
        public const int TamanhoMaximoTelefone = 11;

        #endregion

        #region Campos privados

        private string _nome;
        private string _email;
        private string _cpf;
        private string _telefone;

        #endregion

        #region Propriedades
        
        /// <summary>
        /// Id do vendedor.
        /// </summary>
        public int VendedorId { get; set; }

        /// <summary>
        /// Nome do vendedor.
        /// </summary>
        public string Nome
        {
            get { return _nome;}
            set
            {
                var erro = ValidarNome(value);
                if (erro != string.Empty)
                    throw new Exception(erro);
                _nome = value;
            }
        }
        
        /// <summary>
        /// E-mail do vendedor.
        /// </summary>
        public string Email
        {
            get { return _email; }
            set
            {
                var erro = ValidarEmail(value);
                if (erro != string.Empty)
                    throw new Exception(erro);
                _email = value;
            }
        }
        
        /// <summary>
        /// CPF do vendedor.
        /// </summary>
        public string Cpf
        {
            get { return _cpf; }
            set
            {
                var erro = ValidarCpf(value);
                if (erro != string.Empty)
                    throw new Exception(erro);
                _cpf = value;
            }
        }
        
        /// <summary>
        /// DDD Telefone do vendedor.
        /// </summary>
        public string Telefone
        {
            get { return _telefone; }
            set
            {
                var erro = ValidarTelefone(value);
                if (erro != string.Empty)
                    throw new Exception(erro);
                _telefone = value;
            }
        }
        
        /// <summary>
        /// Lista das vendas realizadas.
        /// </summary>
        public virtual ICollection<Venda> Vendas { get; set; }

        #endregion

        #region Métodos
        
        private static string ValidarNome(string nome)
        {
            if (nome == null || nome.Length > TamanhoMaximoNome)
            {
                return "'Nome' inválido: '" + (nome ?? "null") + "'.";
            }
            return string.Empty;
        }

        private static string ValidarEmail(string email)
        {
            if (email == null || email.Length > TamanhoMaximoEmail || !email.Contains("@"))
            {
                return "'E-mail' inválido: '" + (email ?? "null") + "'.";
            }
            return string.Empty;
        }

        private static string ValidarCpf(string cpf)
        {
            if (cpf == null || cpf.Length > TamanhoMaximoCpf || cpf.Length < TamanhoMaximoCpf)
            {
                return "'CPF' inválido: '" + (cpf ?? "null") + "'.";
            }
            if (!long.TryParse(cpf, out long result))
            {
                return "'CPF' inválido: '" + cpf + "'.";
            }
            return string.Empty;
        }

        private static string ValidarTelefone(string telefone)
        {
            if (telefone == null || telefone.Length > TamanhoMaximoCpf || telefone.Length < TamanhoMaximoCpf)
            {
                return "'Telefone' inválido: '" + (telefone ?? "null") + "'.";
            }
            if (!long.TryParse(telefone, out long result))
            {
                return "'Telefone' inválido: '" + telefone + "'.";
            }
            return string.Empty;
        }

        #endregion
    }
}
