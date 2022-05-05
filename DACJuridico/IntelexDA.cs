using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Entidades;
using JuridicoConstantes;

namespace DACJuridico
{
    public class IntelexDA
    {

        #region Campos

        private string conStr;

        #endregion

        #region Constructor

        public IntelexDA()
        {
            this.conStr = Constantes.CadenaDeConexionEslavon;
        }

        #endregion

        #region Metodos

        public Usuario GetEslavonUsuario(int nEmpleado, ref int resultId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                Usuario usuario;
                int result;

                comm = new SqlCommand("sp_ConsultaEmpleado_pUP", con);
                comm.Parameters.Add(new SqlParameter("pEmpleado", nEmpleado) { DbType = System.Data.DbType.Int32 });
                comm.Parameters.Add(new SqlParameter("pClave", "") { DbType = System.Data.DbType.String });
                comm.Parameters.Add(new SqlParameter("pOpcion", 1) { DbType = System.Data.DbType.Int16 });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                usuario = null;

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        //codigos de estatus devueltos por Intelexion "NumError":
                        //NumError == 0 sin error
                        //NumError == 1 La clave no es correcta.
                        //NumError == 2 El empleado no existe.

                        result = (int)reader["NumError"];

                        if (result == 1 | result == 0)
                        {
                            usuario = new Usuario();
                            usuario.Nombre = reader["Nombre_Completo"].ToString();
                        }

                        resultId = result; //mandar id a pantalla
                    }

                    con.Close();

                    return usuario;
                }
                catch
                {
                    return null;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
            }
        }

        #endregion

    }
}
