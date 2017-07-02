using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;

namespace EleccionesWS
{
    public class MesaDao
    {
        public bool ExisteMesa(string idMesa)
        {
            string sql = "select count(*) from mesa where id_mesa=@id_mesa";
            using (SqlConnection conn = ConnectionFactory.getConnection())
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@id_mesa", idMesa);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.Read() ? (int) reader[0] > 0: false ;
                    }
                }
            }
        }


        public Mesa BuscarMesa(string idMesa)
        {
            string sql = @"select m.id_mesa, m.id_centro_votacion, cv.id_oed from mesa m
join centro_votacion cv on cv.id_centro_votacion = m.id_centro_votacion 
where m.id_mesa = @id_mesa";
            using (SqlConnection conn = ConnectionFactory.getConnection())
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@id_mesa", idMesa);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.Read() ? LeeMesa(reader) : null;
                    }
                }
            }
        }

        private Mesa LeeMesa(SqlDataReader reader)
        {
            return new Mesa()
            {
                IdMesa = (string)reader["id_mesa"],
                IdOed = (int)reader["id_oed"], 
                IdCentroVotacion = (int)reader["id_centro_votacion"]
            };

        }

    }
}