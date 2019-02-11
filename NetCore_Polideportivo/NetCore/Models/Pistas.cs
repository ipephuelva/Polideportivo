using System;
using System.Collections.Generic;

namespace NetCore.Models
{
    public partial class Pistas
    {
        public int Id { get; set; }
        public string Sport { get; set; }
        public int NField { get; set; }

        public bool EsValido(Pistas pista)
        {
            var Errores = new List<string>();
            var valido = true;
            if (string.IsNullOrEmpty(pista.Sport))
            {
                Errores.Add("El campo Sport es obligatorio");
                valido = false;
            }

            if (pista.NField.ToString() == null)
            {
                Errores.Add("El campo Nfield es obligatorio");
                valido = false;
            }

            return valido;
        }
    }
}
