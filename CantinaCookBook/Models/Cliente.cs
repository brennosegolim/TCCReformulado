namespace CantinaCookBook.Models
{
    public class Cliente
    {

        #region Atributos
        /// <summary>
        /// Atributo da classe cliente referente ao identificador.
        /// </summary>
        public int IdCliente { get; set; }

        /// <summary>
        /// Atributo da classe cliente referente ao nome. 
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Atributo da classe cliente referente ao Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Atributo da classe cliente referente ao CPF.
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// Atributo da classe cliente referente ao cliente responsável.
        /// </summary>
        public int IdResponsavel { get; set; }
        #endregion

        #region Métodos
        /// <summary>
        /// Método Construtor da classe cliente.
        /// quando instanciada, seus atributos virão com valores padrão.
        /// </summary>
        public Cliente()
        {

            this.IdCliente = 0;
            this.Nome = "";
            this.Email = null;
            this.CPF = null;
            this.IdCliente = 0;

        }
        #endregion

    }
}