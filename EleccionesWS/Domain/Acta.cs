using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
namespace EleccionesWS.Domain
{
    [DataContract]
    public class Acta
    { 
        public const int ESTADO_ENVIADO = 1;
        
        public const int ESTADO_POR_CORREGIR = 2;

        public const int ESTADO_CON_SOLICITUD_ANULACION = 3;


        [DataMember]
        public string IdMesa { get; set; }

        [DataMember]
        public int? IdMilitantePresidente { get; set; }

        [DataMember]
        public int? IdMilitanteSecretario { get; set; }

        [DataMember]
        public int? IdMilitanteTercerMiembro { get; set; }

        [DataMember]
        public int? IdMilitanteRegistro { get; set; }

        [DataMember]
        public int? Estado { get; set; }

        [DataMember]
        public string FechaRegistro { get; set; }


        [DataMember]
        public string NombreArchivo { get; set; }

        [DataMember]
        public string Observaciones { get; set; }

        [DataMember]
        public byte[] Archivo { get; set; }

        [DataMember]
        public IList<DetalleActa> Detalles { get; set; }
    }
}