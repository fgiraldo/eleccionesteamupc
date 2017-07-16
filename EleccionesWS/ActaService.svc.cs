using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using System.ServiceModel.Web;
using System.Net;

namespace EleccionesWS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ActaService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ActaService.svc or ActaService.svc.cs at the Solution Explorer and start debugging.
    public class ActaService : IActaService
    {
        private const string RUTA_ARCHIVOS_ACTA = "C:/elecciones/archivos_acta";
        ActaDao actaDao = new ActaDao();
        IMilitanteService militanteService = new MilitanteService();
        IMesaService mesaService = new MesaService();

        public void RegistrarActa( Acta  acta)
        { 

            ValidarBasicoActa(acta);
            
            if (acta.Archivo == null)
            {
                throw new FaultException("El archivo es requerido", new FaultCode(Constantes.ERROR_ARCHIVO_ACTA_REQUERIDO));
            } 
            if (acta.NombreArchivo == null)
            {
                throw new FaultException("NombreArchivo requerido", new FaultCode(Constantes.ERROR_ARCHIVO_ACTA_REQUERIDO));
            }
            if (acta.NombreArchivo != null && acta.NombreArchivo.Length > 200)
            {
                throw new FaultException("NombreArchivo no debe tener más de 200 caracteres de longitud", new FaultCode(Constantes.ERROR_NOMBRE_ARCHIVO_ACTA_LONGITUD_INVALIDA));
            }


            Militante militanteRegistro;
            try
            {
                 
                militanteRegistro = militanteService.BuscarMilitante(acta.IdMilitanteRegistro);

            }
            catch (FaultException e)
            {
                if (e.Code.Name == Constantes.ERROR_MILITANTE_NO_HALLADO)
                {
                    throw new FaultException("El militante de registro no existe", new FaultCode(Constantes.ERROR_MILITANTE_REGISTRO_ACTA_NO_HALLADO));
                }
                else
                {
                    throw e;
                }     
            }

            
            if (militanteRegistro.IdCargo != Militante.CARGO_REGISTRADOR_OED)
            {
                throw new FaultException("El cargo del militante de registro debe ser Registrador de OED", new FaultCode(Constantes.ERROR_REGISTRO_ACTA_CARGO_INVALIDO));
            }
 
            Mesa mesa;
            try
            {
               mesa  = mesaService.BuscarMesa(acta.IdMesa);
            }
            catch (FaultException e)
            {
                throw e; 
            }
                if (militanteService.MilitanteAsignadoAOed(acta.IdMilitanteRegistro, mesa.IdOed) == false )
            {
                throw new FaultException("El militante de registro no tiene asignado la OED de la mesa", new FaultCode(Constantes.ERROR_MILITANTE_REGISTRO_ACTA_NO_ASIGNADO_OED));
            }
             

            acta.Estado = Acta.ESTADO_ENVIADO;
            acta.FechaRegistro = DateTime.Now.ToString("yyyy-MM-dd HH':'mm':'ss") ;
            
            actaDao.RegistrarActa(acta);
             
            Directory.CreateDirectory(RUTA_ARCHIVOS_ACTA ); 
            File.WriteAllBytes(RUTA_ARCHIVOS_ACTA+"/"+acta.IdMesa, acta.Archivo);
        
        }

        private void ValidarBasicoActa(Acta acta)
        {
            if (acta.IdMesa == null)
            {
                throw new FaultException("IdMesa requerido", new FaultCode(Constantes.ERROR_ID_MESA_REQUERIDO));
            }
            if (acta.IdMilitantePresidente == null)
            {
                throw new FaultException("IdMilitantePresidente requerido", new FaultCode(Constantes.ERROR_ID_MILITANTE_PRESIDENTE_REQUERIDO));
            }
            if (acta.IdMilitanteSecretario == null)
            {
                throw new FaultException("IdMilitanteSecretario requerido", new FaultCode(Constantes.ERROR_ID_MILITANTE_SECRETARIO_REQUERIDO));
            }
            
            if (acta.IdMilitanteRegistro == null)
            {
                throw new FaultException("IdMilitanteRegistro requerido", new FaultCode(Constantes.ERROR_ID_MILITANTE_REGISTRO_REQUERIDO));
            }
          
            if (acta.Observaciones != null && acta.Observaciones.Length > 1000)
            {
                throw new FaultException("Observaciones no debe tener más de 1000 caracteres de longitud", new FaultCode(Constantes.ERROR_OBSERVACIONES_ACTA_EXCEDE_1000_CARACTERES));
            }
            if (acta.FechaRegistro != null)
            {
                throw new FaultException("No es posible asignar la FechaRegistro",  new FaultCode(Constantes.ERROR_OBSERVACIONES_ACTA_EXCEDE_1000_CARACTERES));
            }
            if (acta.Estado != null && acta.Estado != 0)
            {
                throw new FaultException("No es posible asignar el Estado", new FaultCode(Constantes.ERROR_NO_ES_POSIBLE_ASIGNAR_ESTADO_ACTA));
            }

            if (acta.Detalles == null)
            {

                throw new FaultException("Detalles de acta requeridos", new FaultCode(Constantes.ERROR_DETALLES_ACTA_REQUERIDOS));
            }
         
            foreach (DetalleActa detalle in acta.Detalles)
            {
                if (detalle.IdMilitante == null)
                {

                    throw new FaultException("IdMilitante Candidato en detalle requerido", new FaultCode(Constantes.ERROR_ID_MILITANTE_CANDIDATO_EN_DETALLE_ACTA_REQUERIDO));
                }
                if (detalle.CantVotos == null)
                {

                    throw new FaultException("CantVotos en detalle requerido", new FaultCode(Constantes.ERROR_CANT_VOTOS_EN_DETALLE_ACTA_REQUERIDO));
                }
                if (detalle.CantVotos < 0)
                {

                    throw new FaultException("El valor mínimo para la cantidad de votos es 0", new FaultCode(Constantes.ERROR_CANT_VOTOS_EN_DETALLE_DEBE_MAYOR_0));
                }

            }
            
        }

       
    }
}
