using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
namespace EleccionesWS.Domain
{
    [DataContract]
    public class Militante
    {
        public const int CARGO_REGISTRADOR_OED = 1;

        public const int CARGO_DIRECTIVO_CENA = 2;

        public const int CARGO_DIRECTIVO_OED = 3;

        public const int CARGO_CANDIDATO = 4;
         
        [DataMember]
        public int? IdMilitante { get; set; }

        [DataMember]
        public string Nombres { get; set; }


        [DataMember]
        public string Apellidos { get; set; }


        [DataMember]
        public string Correo { get; set; }


        [DataMember]
        public int? IdCargo { get; set; }


        [DataMember]
        public string NroDocIdentidad { get; set; }


    }
}