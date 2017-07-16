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
                    cmdActa.Parameters.AddWithValue("@id_militante_tercer_miembro", acta.IdMilitanteTercerMiembro != null ? (object)acta.IdMilitanteTercerMiembro : DBNull.Value);
                    cmdActa.Parameters.AddWithValue("@fecha_registro", acta.FechaRegistro);
                    cmdActa.Parameters.AddWithValue("@id_militante_registro", acta.IdMilitanteRegistro);
                    cmdActa.Parameters.AddWithValue("@estado", acta.Estado);
                    cmdActa.Parameters.AddWithValue("@nombre_archivo", acta.NombreArchivo);
                    cmdActa.Parameters.AddWithValue("@observaciones", acta.Observaciones != null ? (object)acta.Observaciones : DBNull.Value);

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

        public ResultadoVotacion[] listarResultadosVotacion(string idDepartamento, string idProvincia, string idDistrito)
        {
            using (SqlConnection conn = ConnectionFactory.getConnection())
            {
                conn.Open();
                string sql = @" select
        da.id_militante, (mil.nombres + ' ' + mil.apellidos) as nombre_candidato, 
        sum(da.cant_votos) as cant_votos
        from detalle_acta da
		join acta a on da.id_mesa = a.id_mesa
        join mesa m  on da.id_mesa = m.id_mesa
        join centro_votacion cv on m.id_centro_votacion = cv.id_centro_votacion 
        join militante mil on da.id_militante = mil.id_militante   
	    where a.estado = 1 "; 
                if (idDepartamento != null && idProvincia != null && idDistrito != null)
                {
                    sql += " and cv.id_departamento = @id_departamento and cv.id_provincia = @id_provincia and cv.id_distrito = @id_distrito"; 
                }
                else if (idDepartamento != null && idProvincia != null && idDistrito == null)
                {
                    sql += " and cv.id_departamento = @id_departamento and cv.id_provincia = @id_provincia"; 
                 }
                else if (idDepartamento != null && idProvincia == null && idDistrito == null)
                {
                    sql += " and cv.id_departamento = @id_departamento"; 
                }

                sql += " group by da.id_militante,  (mil.nombres + ' ' + mil.apellidos) order by cant_votos desc";

                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    if (idDepartamento != null && idProvincia != null && idDistrito != null)
                    {
                       command.Parameters.AddWithValue("@id_departamento", idDepartamento);
                       command.Parameters.AddWithValue("@id_provincia", idProvincia);
                       command.Parameters.AddWithValue("@id_distrito", idDistrito);
                    }
                    else if (idDepartamento != null && idProvincia != null && idDistrito == null)
                    {
                        command.Parameters.AddWithValue("@id_departamento", idDepartamento);
                        command.Parameters.AddWithValue("@id_provincia", idProvincia);
                    }
                    else if (idDepartamento != null && idProvincia == null && idDistrito == null)
                    {
                        command.Parameters.AddWithValue("@id_departamento", idDepartamento);
                    }

                    IList<ResultadoVotacion> resultados = new List<ResultadoVotacion>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultados.Add(LeerResultadoVotacion(reader));
                        }
                        return resultados.ToArray();
                    }
                }
            }     
        }

        private ResultadoVotacion LeerResultadoVotacion(SqlDataReader reader)
        {

            return new ResultadoVotacion()
            {
                IdMilitante = (int)reader["id_militante"],
                NombreCandidato = (string)reader["nombre_candidato"],
                CantVotos = (int)reader["cant_votos"] 
            };

        }
    }
}