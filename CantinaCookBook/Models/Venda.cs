using System;

namespace CantinaCookBook.Models
{
    public class Venda
    {

        #region Atributos
        /// <summary>
        /// Atributo da classe Venda referente ao identificador.
        /// </summary>
        public int IdVenda { get; set; }

        /// <summary>
        /// Atributo da classe Venda referente a data da venda.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Atributo da classe Venda referente ao valor total da venda.
        /// </summary>
        public decimal ValorTotal { get; set; }

        /// <summary>
        /// Atributo da classe Venda refernete ao valor identificador do cliente.
        /// </summary>
        public int IdCliente { get; set; }
        #endregion

        #region Métodos
        /// <summary>
        /// Método construtor da classe Venda
        /// </summary>
        public Venda()
        {

            this.IdVenda = 0;
            this.Data = new DateTime().ToString();
            this.ValorTotal = new Decimal(0);
            this.IdCliente = 0;

        }
        #endregion

    }
}