namespace CantinaCookBook.Models
{
    public class Acesso
    {

        #region Atributos
        /// <summary>
        /// Atributo da classe acesso referente ao identificador.
        /// </summary>
        public int IdAcesso { get; set; }

        /// <summary>
        /// Atributo da classe acesso referente a string do Login.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Atributo da classe acesso referente a Senha.
        /// </summary>
        public string Senha { get; set; }

        /// <summary>
        /// Atributo da classe acesso referente ao nível de acesso do usuário.
        /// </summary>
        public string Nivel { get; set; }

        /// <summary>
        /// Atributo da classe acesso que aponta ao cliente vinculado.
        /// </summary>
        public int IdCliente { get; set; }
        #endregion

        #region Métodos
        public Acesso()
        {

            this.IdAcesso = 0;
            this.Login = "";
            this.Senha = "";
            this.Nivel = "";
            this.IdCliente = 0;

        }
        #endregion

    }
}