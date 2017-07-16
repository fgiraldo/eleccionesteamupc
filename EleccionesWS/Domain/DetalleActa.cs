using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
namespace EleccionesWS.Domain
{

    [DataContract]
    public class DetalleActa
    {

        [DataMember]
        public string IdMesa { get; set; }


        [DataMember]
        public int? IdMilitante { get; set; }

        [DataMember]
        public int? CantVotos { get; set; }
    }
}