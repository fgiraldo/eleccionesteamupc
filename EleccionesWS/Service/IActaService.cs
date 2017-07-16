using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using EleccionesWS.Dao;
using EleccionesWS.Domain;

namespace EleccionesWS.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IActaService" in both code and config file together.
    [ServiceContract]
    public interface IActaService
    {
        [OperationContract]
        void RegistrarActa( Acta acta);

    }
}
