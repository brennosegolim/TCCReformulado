namespace CantinaCookBook.Models
{
    public class Produto
    {

        #region Atributos
        /// <summary>
        /// Atributo da classe Produto referente ao identificador.
        /// </summary>
        public int IdProduto { get; set; }

        /// <summary>
        /// Atributo da classe Produto referente ao Código do produto.
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Atributo da classe Produto referente a Descrição do produto.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Atributo da classe Produto referente ao preço do produto.
        /// </summary>
        public decimal Preco { get; set; }

        /// <summary>
        /// Atributod da classe Produto referente a Observações do produto.
        /// </summary>
        public string Observacao { get; set; }
        #endregion

        #region Métodos
        public Produto()
        {

            this.IdProduto = 0;
            this.Codigo = "";
            this.Descricao = "";
            this.Preco = new decimal(0);
            this.Observacao = null;

        }
        #endregion

    }
}