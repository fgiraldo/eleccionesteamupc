using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EleccionesWS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MilitanteService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MilitanteService.svc or MilitanteService.svc.cs at the Solution Explorer and start debugging.
    public class MilitanteService : IMilitanteService
    { 

        MilitanteDao militanteDao = new MilitanteDao();

        public Militante BuscarMilitante(int? idMilitante)
        {
            if (idMilitante == null)
            {
                throw new FaultException("Id militante requerido", new FaultCode(Constantes.ERROR_ID_MILITANTE_REQUERIDO ));
            }
            Militante militante = militanteDao.BuscarMilitante(idMilitante);
            if (militante == null)
            {
                throw new FaultException("Militante no hallado", new FaultCode(Constantes.ERROR_MILITANTE_NO_HALLADO));
            }
            return militante;
        }

        public bool MilitanteAsignadoAOed(int? idMilitante, int? idOed)
        {

            if (idMilitante == null)
            {
                throw new FaultException("Id militante requerido", new FaultCode(Constantes.ERROR_ID_MILITANTE_REQUERIDO));
            }

            if (idOed == null)
            {
                throw new FaultException("Id oed requerido", new FaultCode(Constantes.ERROR_ID_OED_REQUERIDO));
            }
            return militanteDao.MilitanteAsignadoAOed(idMilitante, idOed);
        }

        public Militante[] ListarMilitantes(FiltroListarMilitante filtro)
        {
            if (filtro == null)
            {
                throw new FaultException("Filtro requerido", new FaultCode(Constantes.ERROR_FILTRO_LISTAR_MILITANTE_REQUERIDO));
            }
           /*if (filtro.IdCargo == null)
            {
                throw new FaultException("Filtro requerido", new FaultCode(Constantes.ERROR_FILTRO_LISTAR_MILITANTE_REQUERIDO));
            }
            * */
            return militanteDao.ListarMilitantes(filtro);
        }
    }
}
