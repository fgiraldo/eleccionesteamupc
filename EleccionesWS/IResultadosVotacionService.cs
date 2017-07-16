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
    public interface IResultadosVotacionService
    {

        //http://localhost:56837/ResultadosVotacionService.svc/resultados-votacion
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/resultados-votacion?idDepartamento={idDepartamento}&idProvincia={idProvincia}&idDistrito={idDistrito}", 
            ResponseFormat=WebMessageFormat.Json)]
        ResultadoVotacion[] listarResultadosVotacion(string idDepartamento = null, string idProvincia = null, string idDistrito = null); 
    }
}