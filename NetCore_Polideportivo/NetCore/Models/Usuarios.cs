using System;
using System.Collections.Generic;

namespace NetCore.Models
{
    public partial class Usuarios
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public bool EsValido(Usuarios usuario)
        {
            var Errores = new List<string>();
            var valido = true;
            if (string.IsNullOrEmpty(usuario.User))
            {
                Errores.Add("El campo User es obligatorio");
                valido = false;
            }

            if(string.IsNullOrEmpty(usuario.Password))
            { 
                Errores.Add("El campo Password es obligatorio");
                valido = false;
            }

            return valido;
        }
    }


}
