using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace EleccionesWS
{
    [DataContract]
    public class ResultadoVotacion
    {

        [DataMember]
        public int IdMilitante { get; set; }


        [DataMember]
        public string NombreCandidato { get; set; }     

        [DataMember]
        public int CantVotos { get; set; } 

    }
}