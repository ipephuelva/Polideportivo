using System;
using System.Collections.Generic;

namespace NetCore.Models
{
    public partial class Deportes
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool EsValido(Deportes deporte)
        {
            var Errores = new List<string>();
            var valido = true;
            if (string.IsNullOrEmpty(deporte.Name))
            {
                Errores.Add("El campo Name es obligatorio");
                valido = false;
            }

            return valido;
        }
    }
}
