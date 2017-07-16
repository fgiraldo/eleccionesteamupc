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
