using System;
using System.Collections.Generic;
using System.Text;

namespace ValidaBrasil.Interfaces
{
    public interface IOperacao
    {
        /// <summary>
        /// Valida o valor fornecido de acordo com regras específicas.
        /// </summary>
        /// <param name="valor">O valor a ser validado.</param>
        /// <returns>Retorna verdadeiro se o valor for válido, caso contrário, retorna falso.</returns>
        bool Validar(string valor);

        /// <summary>
        /// Aplica uma formatação específica ao valor fornecido.
        /// </summary>
        /// <param name="valor">O valor a ser formatado.</param>
        /// <returns>Retorna o valor formatado.</returns>
        string Formatar(string valor);

        /// <summary>
        /// Remove qualquer formatação existente do valor fornecido.
        /// </summary>
        /// <param name="valor">O valor do qual a formatação será removida.</param>
        /// <returns>Retorna o valor sem formatação.</returns>
        string RemoverFormatação(string valor);
    }

}
