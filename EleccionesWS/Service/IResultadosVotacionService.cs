using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using EleccionesWS.Dao;
using EleccionesWS.Domain;
namespace EleccionesWS.Service
{ 
    [ServiceContract]
    public interface IResultadosVotacionService
    {

        //http://localhost:56837/Service/ResultadosVotacionService.svc/resultados-votacion
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/resultados-votacion?idDepartamento={idDepartamento}&idProvincia={idProvincia}&idDistrito={idDistrito}", 
            ResponseFormat=WebMessageFormat.Json)]
        ResultadoVotacion[] listarResultadosVotacion(string idDepartamento = null, string idProvincia = null, string idDistrito = null); 
    }
}