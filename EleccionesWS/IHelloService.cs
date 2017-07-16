using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace EleccionesWS
{
     [ServiceContract]
    public interface IHelloService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/hello?idDepartamento={idDepartamento}&idProvincia={idProvincia}&idDistrito={idDistrito}", ResponseFormat=WebMessageFormat.Json)]
        ResultadoVotacion obtener(string idDepartamento = null, string idProvincia = null, string idDistrito = null); 
    }
}
