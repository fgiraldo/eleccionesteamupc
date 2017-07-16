using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TestEleccionesWS.MilitanteWs;
using TestEleccionesWS.ActaWs;

using System.Linq;
using System.ServiceModel;
namespace TestEleccionesWS
{
    [TestClass]
    public class MilitanteServiceTest
    {

        MilitanteServiceClient client = new MilitanteServiceClient();

        [TestMethod]
        public void DeberiaListarCandidatos()
        {
            Militante[] militantes = client.ListarMilitantes(new FiltroListarMilitante() { IdCargo = 4});
           
            Assert.IsTrue(militantes.Length == 9, "Deberían haber candidatos");
        }
    }
}
