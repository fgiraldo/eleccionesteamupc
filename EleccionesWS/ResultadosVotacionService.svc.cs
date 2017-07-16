using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EleccionesWS
{  
    public class ResultadosVotacionService : IResultadosVotacionService
    {
        ActaDao actaDao = new ActaDao();

        public ResultadoVotacion[] listarResultadosVotacion(string idDepartamento, string idProvincia, string idDistrito)
        {
            ResultadoVotacion[] resultados = actaDao.listarResultadosVotacion(idDepartamento, idProvincia, idDistrito);
            if (resultados.Length == 0)
            {
                throw new WebFaultException(HttpStatusCode.Unauthorized);
            }
            return resultados;
        } 
    
    }
}
