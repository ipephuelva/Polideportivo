using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetCore.Models
{
    public partial class Reservas
    {
        public int Id { get; set; }
        public string Dni { get; set; }
        public string Sport { get; set; }
        public int NField { get; set; }
        public DateTime Date { get; set; }

        public bool EsValido(Reservas reserva)
        {
            var Errores = new List<string>();
            var valido = true;

            Regex dni = new Regex(@"[a-zA-ZñÑ\s]");

            if (string.IsNullOrEmpty(reserva.Dni))
            {
                Errores.Add("El campo Dni es obligatorio");
                valido = false;
            }
            else
            {
                if (reserva.Dni.Length != 9)
                {
                    Errores.Add("El campo Dni es de 9 digitos");
                    valido = false;
                }

                if (!dni.IsMatch(reserva.Dni.Substring(reserva.Dni.Length - 1, 1)))
                {
                    Errores.Add("El campo Dni es de 8 digitos y 1 letra");
                    valido = false;
                }
            }

            if (string.IsNullOrEmpty(reserva.Sport))
            {
                Errores.Add("El campo Sport es obligatorio");
                valido = false;
            }

            if (reserva.NField.ToString() == null)
            {
                Errores.Add("El campo Nfield es obligatorio");
                valido = false;
            }

            if(reserva.Date.Minute != 00 || reserva.Date.Second != 00)
            {
                Errores.Add("El campo Date no admite tiempos en minutos y segundos, solo maneja horas");
                valido = false;
            }

            return valido;
        }

    }
}
