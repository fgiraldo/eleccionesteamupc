using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace EleccionesWS
{
    public class ActaDao
    {
        private const string SQL_INSERT_ACTA = @"insert into acta (id_mesa, id_militante_presidente, 
id_militante_secretario, id_militante_tercer_miembro, fecha_registro, id_militante_registro, 
estado, nombre_archivo, observaciones) values(@id_mesa, @id_militante_presidente, 
@id_militante_secretario, @id_militante_tercer_miembro, @fecha_registro, @id_militante_registro, 
@estado, @nombre_archivo, @observaciones)";

        private const string SQL_INSERT_DETALLE_ACTA = @"insert into detalle_acta (id_mesa, id_militante, cant_votos) values(@id_mesa, @id_militante, @cant_votos)";

        public void RegistrarActa(Acta acta)
        {
            using (SqlConnection conn = ConnectionFactory.getConnection())
            { 
                conn.Open();

                SqlTransaction transaction = conn.BeginTransaction();
                try
                { 
                    SqlCommand cmdActa = new SqlCommand(SQL_INSERT_ACTA, conn, transaction);
                    cmdActa.Parameters.AddWithValue("@id_mesa", acta.IdMesa);
                    cmdActa.Parameters.AddWithValue("@id_militante_presidente", acta.IdMilitantePresidente);
                    cmdActa.Parameters.AddWithValue("@id_militante_secretario", acta.IdMilitanteSecretario);
                    cmdActa.Parameters.AddWithValue("@id_militante_tercer_miembro",  acta.IdMilitanteTercerMiembro != null ? (object)acta.IdMilitanteTercerMiembro : DBNull.Value );
                    cmdActa.Parameters.AddWithValue("@fecha_registro", acta.FechaRegistro);
                    cmdActa.Parameters.AddWithValue("@id_militante_registro", acta.IdMilitanteRegistro);
                    cmdActa.Parameters.AddWithValue("@estado", acta.Estado);
                    cmdActa.Parameters.AddWithValue("@nombre_archivo", acta.NombreArchivo);
                    cmdActa.Parameters.AddWithValue("@observaciones",  acta.Observaciones != null ? (object)acta.Observaciones: DBNull.Value );

                    cmdActa.ExecuteNonQuery();

                    foreach (DetalleActa detalle in acta.Detalles)
                    { 
                        SqlCommand cmdDetalle = new SqlCommand(SQL_INSERT_DETALLE_ACTA, conn, transaction);
                        cmdDetalle.Parameters.AddWithValue("@id_mesa", acta.IdMesa);
                        cmdDetalle.Parameters.AddWithValue("@id_militante", detalle.IdMilitante);
                        cmdDetalle.Parameters.AddWithValue("@cant_votos", detalle.CantVotos); 
                        cmdDetalle.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch { /* Ignorar excepción aquí*/  }
                    throw e;
                }  
            }
        }
    }
}