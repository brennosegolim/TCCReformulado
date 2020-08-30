using System;

namespace CantinaCookBook.Models
{

    public class Produto_Venda
    {

        #region Atributos
        /// <summary>
        /// Atributo da classe Produto_Venda referente ao identificador.
        /// </summary>
        public int IdProduto_Venda { get; set; }

        /// <summary>
        /// Atributo da classe Produto_Venda referente ao identificador do produto.
        /// </summary>
        public int IdProduto { get; set; }

        /// <summary>
        /// Atributo da classe Produto_Venda referente ao identificador da venda.
        /// </summary>
        public int IdVenda { get; set; }

        /// <summary>
        /// Atributo da classe Produto_Venda referente ao Valor.
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Atributo da classe Produto_Venda referente a data.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Atributo da classe Produto_Venda referente a quantidade.
        /// </summary>
        public int Quantidade { get; set; }
        #endregion

        #region Métodos
        /// <summary>
        /// Método construtor da classe Produto_Venda.
        /// </summary>
        public Produto_Venda()
        {

            this.IdProduto_Venda = 0;
            this.IdProduto = 0;
            this.IdVenda = 0;
            this.Valor = new Decimal(0);
            this.Data = new DateTime().ToString();
            this.Quantidade = 0;

        }
        #endregion

    }

}