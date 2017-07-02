using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using  TestEleccionesWS.Elecciones;
using  TestEleccionesWS.Elecciones2;

using System.Linq;
using System.ServiceModel;
namespace TestEleccionesWS
{
    [TestClass]
    public class ActaServiceTest
    { 

        ActaServiceClient client = new ActaServiceClient();

        [TestMethod]
        public void DeberiaValidarQueIngreseLaMesa()
        {
            Acta acta = CrearActa();
            acta.IdMesa = null;  

            AssertErrorRegistrandoActa(acta, Constantes.ERROR_ID_MESA_REQUERIDO);
        }

        [TestMethod]
        public void DeberiaValidarQueMesaExista()
        {
            Acta acta = CrearActa();
            acta.IdMesa = "000_no_existo";

            AssertErrorRegistrandoActa(acta, Constantes.ERROR_MESA_NO_HALLADA);
        }

        [TestMethod]
        public void DeberiaValidarQueRegistradoEsteAsignadoAOed()
        {
            Acta acta = CrearActa();
            acta.IdMilitanteRegistro = 11; 
            AssertErrorRegistrandoActa(acta, Constantes.ERROR_MILITANTE_REGISTRO_ACTA_NO_ASIGNADO_OED);
        }

        [TestMethod]
        public void DeberiaValidarQueSubaArchivo()
        {
            Acta acta = CrearActa();
            acta.Archivo = null;
            acta.NombreArchivo = null;
            AssertErrorRegistrandoActa(acta, Constantes.ERROR_ARCHIVO_ACTA_REQUERIDO);
        }


        [TestMethod]
        public void DeberiaValidarQueIngresePresidenteMesa()
        {
            Acta acta = CrearActa();
            acta.IdMilitantePresidente = null; 
            AssertErrorRegistrandoActa(acta, Constantes.ERROR_ID_MILITANTE_PRESIDENTE_REQUERIDO);
        }
         

        [TestMethod]
        public void DeberiaRegistrarActaValida()
        {
            Acta acta = CrearActa(); 
            client.RegistrarActa(acta); 
        }

        private void AssertErrorRegistrandoActa(Acta acta, string codigoError)
        {
            try {
                client.RegistrarActa(acta);
                Assert.Fail("Debería haber ocurrido un error");
            } catch(FaultException e) {
                if (e.Code.Name == "InternalServiceFault")
                { 
                    throw e;
                }
                Assert.AreEqual( codigoError, e.Code.Name);
            }
        }

        private Acta CrearActa()
        {
            IList<DetalleActa> detalles = new List<DetalleActa>();
            detalles.Add(new DetalleActa() { IdMilitante = 1, CantVotos = 30});
            detalles.Add(new DetalleActa() { IdMilitante = 2, CantVotos = 35 });
            detalles.Add(new DetalleActa() { IdMilitante = 3, CantVotos = 15 });
            detalles.Add(new DetalleActa() { IdMilitante = 4, CantVotos = 10 });
            detalles.Add(new DetalleActa() { IdMilitante = 7, CantVotos = 0 });
            detalles.Add(new DetalleActa() { IdMilitante = 8, CantVotos = 6 });
            detalles.Add(new DetalleActa() { IdMilitante = 9, CantVotos = 1 });

            Acta acta = new Acta() {
                IdMesa = "1501",
                IdMilitantePresidente = 12,
                IdMilitanteSecretario = 13,
                NombreArchivo = "acta.jpg",
                IdMilitanteRegistro = 10,
                Archivo = File.ReadAllBytes("c:/acta.jpg"),
                Detalles = detalles.ToArray()
            };
            return acta;

        }

        [TestInitialize]
        public void AntesEjecutar() {

            EjecutarSQL("delete from detalle_acta");
            EjecutarSQL("delete from acta");
        } 

        public static void EjecutarSQL(string s)
        { 
            using (SqlConnection conn = new SqlConnection("Data Source=(local);Initial Catalog=elecciones;Integrated Security=SSPI;"))
            {      
                conn.Open(); 
                 SqlCommand cmdActa = new SqlCommand(s, conn );
                 cmdActa.ExecuteNonQuery(); 
            }
        }
    }
}
