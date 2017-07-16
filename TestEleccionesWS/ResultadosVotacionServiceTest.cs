using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using System.Collections.Generic;

namespace TestEleccionesWS
{
    [TestClass]
    public class ResultadosVotacionServiceTest
    {
        [TestMethod]
        public void DeberiaNoPermitirAccesoCuandoNoHayActasRegistradas()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            String distritoSinActasRegistradas = "20";
            HttpWebRequest request = (HttpWebRequest)WebRequest
                .Create("http://localhost:56837/ResultadosVotacionService.svc/resultados-votacion?idDepartamento=15&idProvincia=15&idDistrito="+distritoSinActasRegistradas);
            request.Method = "GET";
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Assert.Fail("Debería haber arrojado una excepción");
            }
            catch (WebException e)
            { 
                Assert.IsTrue(e.Message.Contains("401"));
            }                   
        }

        [TestMethod]
        public void DeberiaRetornarResultados()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            String distritoConActasRegistradas = "15";
            HttpWebRequest request = (HttpWebRequest)WebRequest
                .Create("http://localhost:56837/ResultadosVotacionService.svc/resultados-votacion?idDepartamento=15&idProvincia=15&idDistrito=" + distritoConActasRegistradas);
            request.Method = "GET";
            
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream() );
            String tramaJson = reader.ReadToEnd();
            List<ResultadoVotacion> resultados = js.Deserialize<List<ResultadoVotacion>>(tramaJson); 
            Assert.IsTrue(resultados.Count > 0);
           
        }



    }
}
