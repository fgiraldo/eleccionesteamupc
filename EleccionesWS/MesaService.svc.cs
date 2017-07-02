using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EleccionesWS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MesaService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MesaService.svc or MesaService.svc.cs at the Solution Explorer and start debugging.
    public class MesaService : IMesaService
    { 

        MesaDao mesaDao = new MesaDao();

        public Mesa BuscarMesa(string idMesa)
        {
            Mesa mesa = mesaDao.BuscarMesa(idMesa);
            if (mesa == null)
            {
                throw new FaultException("Mesa no hallada", new FaultCode(Constantes.ERROR_MESA_NO_HALLADA));
            }
            return mesa;
        }
    }
}
