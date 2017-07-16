using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using EleccionesWS.Dao;
using EleccionesWS.Domain;
namespace EleccionesWS.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMesaService" in both code and config file together.
    [ServiceContract]
    public interface IMesaService
    {
        [OperationContract]
        Mesa BuscarMesa(string idMesa);
         
    }
}
