using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CantinaCookBook.Models
{
    public class ClienteLimite
    {

        #region Atributos
        /// <summary>
        /// Atributo da classe ClienteLimite referente ao identificador.
        /// </summary>
        public int IdClienteLimite { get; set; }

        /// <summary>
        /// Atributo da classe ClienteLimite referente a identificado do cliente.
        /// </summary>
        public int IdCliente { get; set; }

        /// <summary>
        /// Atributo da classe ClienteLimite referente ao valor limite.
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Atributo da classe ClienteLimite refernete a data de alteração.
        /// </summary>
        public string Data { get; set; }
        #endregion

        #region Métodos
        /// <summary>
        /// Método construtor da classe ClienteLimite
        /// </summary>
        public ClienteLimite()
        {

            this.IdClienteLimite = 0;
            this.IdCliente = 0;
            this.Valor = new Decimal(0);
            this.Data = new DateTime().ToString();

        }
        #endregion

    }
}