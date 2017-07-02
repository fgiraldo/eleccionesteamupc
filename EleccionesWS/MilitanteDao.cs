using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace EleccionesWS
{
    public class MilitanteDao
    {
        public Militante BuscarMilitante(int? idMilitante)
        {
            string sql = "select id_militante, nombres, apellidos, correo, id_cargo, nro_doc_identidad from militante where id_militante=@id_militante";
            using (SqlConnection conn = ConnectionFactory.getConnection())
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@id_militante", idMilitante);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.Read() ? LeerMilitante(reader) : null;
                    }
                }
            }
        }


        public bool MilitanteAsignadoAOed(int? idMilitante, int? idOed)
        {
            string sql = "select count(*) from oed_militante where id_militante=@id_militante and id_oed = @id_oed";
            using (SqlConnection conn = ConnectionFactory.getConnection())
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@id_militante", idMilitante); 
                    command.Parameters.AddWithValue("@id_oed", idOed);
                     using (SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.Read() ?  (int)reader[0] > 0  : false;
                    }
                }
            }  
        }

         public Militante[] ListarMilitantes(FiltroListarMilitante filtro)
        {
            string sql = "select id_militante, nombres, apellidos, correo, id_cargo, nro_doc_identidad from militante";
            if (filtro != null)
            {
                sql += " where ";
            }
            if (filtro.IdCargo != null)
            {
                sql+=" id_cargo = @id_cargo";
            }
            using (SqlConnection conn = ConnectionFactory.getConnection())
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    if(filtro != null ) {
                        if(filtro.IdCargo != null) {
                            command.Parameters.AddWithValue("@id_cargo", filtro.IdCargo);
                        }
                    }
  
                    IList<Militante> militantes = new List<Militante>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read()) {
                            militantes.Add(LeerMilitante(reader));
                        }
                        return militantes.ToArray();
                    }
                }
            }     
        }

        private Militante LeerMilitante(SqlDataReader reader)   
        {
             
            return new Militante()
            {
                IdMilitante = (int)reader["id_militante"],
                Nombres = (string)reader["nombres"],
                Apellidos = (string)reader["apellidos"],
                Correo = (string)reader["correo"],
                IdCargo = reader["id_cargo"] != DBNull.Value ? (int?)reader["id_cargo"] : null,
                NroDocIdentidad = (string)reader["nro_doc_identidad"]
            }; 

        }
    }
}