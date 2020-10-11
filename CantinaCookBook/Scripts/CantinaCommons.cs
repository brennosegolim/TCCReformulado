using System;
using System.Security.Cryptography;
using System.Text;

namespace CantinaCookBook.Scripts
{
    public class CantinaCommons
    {

        #region Atributos
        #endregion

        #region Métodos

        #region Métodos construtores
        public CantinaCommons()
        {

        }
        #endregion

        #region Métodos referente a criptografia.
        public string RetornarMD5(string Senha)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                return RetonarHash(md5Hash, Senha);
            }
        }

        public bool ComparaMD5(string senhabanco, string Senha_MD5)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                var senha = RetornarMD5(senhabanco);
                if (VerificarHash(md5Hash, Senha_MD5, senha))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private string RetonarHash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        private bool VerificarHash(MD5 md5Hash, string input, string hash)
        {
            StringComparer compara = StringComparer.OrdinalIgnoreCase;

            if (0 == compara.Compare(input, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Métodos para auxilar o desenvolvimento
        /// <summary>
        /// Método que verifica se a String (texto) está vazia
        /// </summary>
        /// <param name="valor">Parâmetro do tipo String cotendo o texto a ser verificado.</param>
        /// <returns>true se a string for vazia ou false se string for diferente de "".</returns>
        public bool isEmpty(string valor)
        {

            return valor.Equals("");

        }

        public bool isInterger(object valor) {

            bool retorno = true;

            try{

                int.Parse(valor.ToString());

            }
            catch
            {

                retorno = false;

            }

            return retorno;

        }

        #endregion

        #endregion

    }
}