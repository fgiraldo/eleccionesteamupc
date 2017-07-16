using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EleccionesWS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HelloService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select HelloService.svc or HelloService.svc.cs at the Solution Explorer and start debugging.
    public class HelloService : IHelloService
    {
        public ResultadoVotacion obtener(string idDepartamento , string idProvincia , string idDistrito )
        {
            return new ResultadoVotacion() { 
                IdMilitante = 1,
                CantVotos = 3,
                NombreCandidato="Hugo"
            };
        }
    }
}
