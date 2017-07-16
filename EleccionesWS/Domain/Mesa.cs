using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
namespace EleccionesWS.Domain
{

    [DataContract]
    public class Mesa
    {
        [DataMember]
        public string IdMesa { get; set; }

        [DataMember]
        public int? IdCentroVotacion { get; set; }

        [DataMember]
        public int? IdOed { get; set; }
    }
}