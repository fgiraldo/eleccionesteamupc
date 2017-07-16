using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;
namespace EleccionesWS.Domain
{
    [DataContract]
    public class FiltroListarMilitante
    {
        [DataMember]
        public int? IdCargo { get; set; }
    }
}