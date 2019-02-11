using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetCore.Models
{
    public partial class Socios
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? Telephone { get; set; }
        public string Dni { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public bool EsValido(Socios socio)
        {
            var Errores = new List<string>();
            var valido = true;

            Regex dni = new Regex(@"[a-zA-ZñÑ\s]");
            Regex telefono = new Regex(@"^[+-]?\d+(\.\d+)?$");


            if (string.IsNullOrEmpty(socio.Name))
            {
                Errores.Add("El campo Name es obligatorio");
                valido = false;
            }

            if (string.IsNullOrEmpty(socio.Surname))
            {
                Errores.Add("El campo Surname es obligatorio");
                valido = false;
            }

            if (string.IsNullOrEmpty(socio.Dni))
            {
                Errores.Add("El campo Dni es obligatorio");
                valido = false;
            }
            else
            {
                if (socio.Dni.Length != 9)
                {
                    Errores.Add("El campo Dni es de 9 digitos");
                    valido = false;
                }

                if (!dni.IsMatch(socio.Dni.Substring(socio.Dni.Length - 1, 1)))
                {
                    Errores.Add("El campo Dni es de 8 digitos y 1 letra");
                    valido = false;
                }
            }

            if(socio.Telephone != null)
            {
                if(socio.Telephone.ToString().Length != 9)
                {
                    Errores.Add("El campo Telefono es de 9 digitos");
                    valido = false;
                }
                if (!telefono.IsMatch(socio.Telephone.ToString()))
                {
                    Errores.Add("El campo Telefono es solo numerico");
                    valido = false;
                }
            }

            if (string.IsNullOrEmpty(socio.User))
            {
                Errores.Add("El campo User es obligatorio");
                valido = false;
            }

            if (string.IsNullOrEmpty(socio.Password))
            {
                Errores.Add("El campo Password es obligatorio");
                valido = false;
            }

            return valido;
        }
    }
}
