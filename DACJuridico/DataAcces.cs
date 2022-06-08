using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Entidades;
using JuridicoConstantes;

namespace DACJuridico
{
    public class DataAcces
    {

        #region Campos

        private string conStr;

        #endregion

        #region Constructor

        /// <summary>
        /// Clase para el acceso a datos.
        /// </summary>
        /// <param name="cnStr">Cadena de conexion para el acceso a datos.</param>
        public DataAcces()
        {
            conStr = Constantes.CadenaDeConexion;
        }

        #endregion

        #region Metodos

        public List<Tbl_Menu> GetMenuItems(string perfil)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<Tbl_Menu> list;

                comm = new SqlCommand("GETMENU", con);
                comm.Parameters.Add(new SqlParameter("perfil", perfil) { SqlDbType = System.Data.SqlDbType.Int });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<Tbl_Menu>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Tbl_Menu()
                        {
                            idMenu = (int)reader["idMenu"],
                            Descripcion = (string)reader["Descripcion"],
                            Posicion = reader["Posicion"] == null ? new int?() : (int)reader["Posicion"],
                            PadreId = reader["PadreId"] == null ? new int?() : (int)reader["PadreId"],
                            Url = (string)reader["Url"]
                        });
                    }

                    con.Close();

                    return list;
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
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }
            }
        }

        #endregion

        #region Usuarios

        public List<Usuario> GetUsuarios(ref Exception ex)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<Usuario> list;

                comm = new SqlCommand("SP_Get_User", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<Usuario>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Usuario()
                        {
                            Id = (int)reader["id_usuario"],
                            Nombre = (string)reader["Nombre"],
                            NEmpleado = (int)reader["NEmpleado"],
                            PerfilDesc = (string)reader["Descripcion"],
                            PerfilId = (int)reader["id_nperfil"],
                            Email = reader["email"] == DBNull.Value ? "" : (string)reader["email"]
                        });
                    }

                    con.Close();

                    return list;
                }
                catch (Exception ex1)
                {
                    ex = ex1;
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
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }
            }
        }

        public List<tbl_nperfil> GetPerfiles()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<tbl_nperfil> list;

                comm = new SqlCommand("SP_Get_TPerfiles", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<tbl_nperfil>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new tbl_nperfil()
                        {
                            id_nperfil = (int)reader["id_nperfil"],
                            Nombre = (string)reader["Nombre"]
                        });
                    }

                    con.Close();

                    return list;
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
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }
            }
        }

        public List<tbl_Bitacora> GetBitacoraSolicitud(int idUsuario, int solicitudId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<tbl_Bitacora> list;

                comm = new SqlCommand("SelBitacora", con);
                comm.Parameters.Add(new SqlParameter("id_solicitud", solicitudId) { DbType = System.Data.DbType.Int32 });
                comm.Parameters.Add(new SqlParameter("id_usuario", idUsuario) { DbType = System.Data.DbType.Int32 });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<tbl_Bitacora>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new tbl_Bitacora()
                        {
                            desc_usuario = (string)reader["usuario"],
                            desc_estatus = (string)reader["estatus"],
                            fecha = (DateTime)reader["fecha"],
                            comentarios = (string)reader["comentarios"]
                        });
                    }

                    con.Close();

                    return list;
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
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }
            }
        }

        public List<tblSolicitudEtiquetas> GetEtiquetasJuridico(int idUsuario, int solicitudId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<tblSolicitudEtiquetas> list = new List<tblSolicitudEtiquetas>();

                comm = new SqlCommand("GetEtiquetasById", con);
                comm.Parameters.Add(new SqlParameter("idSolicitud", solicitudId) { DbType = System.Data.DbType.Int32 });
                comm.Parameters.Add(new SqlParameter("soloJuridico", 1) { DbType = System.Data.DbType.Int32 }); //solo obtener etiquetas juridicas
                //comm.Parameters.Add(new SqlParameter("idUsuario", idUsuario) { DbType = System.Data.DbType.Int32 }); //no está en uso
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new tblSolicitudEtiquetas()
                        {
                            IdSolicitudEtiqueta = (int)reader["IdSolicitudEtiqueta"],
                            Pregunta = (string)reader["Pregunta"],
                            id_usuarioLigado = ToInt32_0(reader["id_usuarioLigado"]),
                            Valor = (string)reader["Valor"]
                        });
                    }

                    con.Close();

                    return list;
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
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }
            }
        }

        public List<tblSolicitudEtiquetas> GetEtiquetasAll(int idUsuario, int solicitudId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<tblSolicitudEtiquetas> list = new List<tblSolicitudEtiquetas>();

                comm = new SqlCommand("GetEtiquetasById", con);
                comm.Parameters.Add(new SqlParameter("idSolicitud", solicitudId) { DbType = System.Data.DbType.Int32 });
                //comm.Parameters.Add(new SqlParameter("idUsuario", idUsuario) { DbType = System.Data.DbType.Int32 }); //no está en uso
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new tblSolicitudEtiquetas()
                        {
                            IdSolicitudEtiqueta = (int)reader["IdSolicitudEtiqueta"],
                            Pregunta = (string)reader["Pregunta"],
                            id_usuarioLigado = ToInt32_0(reader["id_usuarioLigado"]),
                            Valor = (string)reader["Valor"]
                        });
                    }

                    con.Close();

                    return list;
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
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }
            }
        }

        public List<Estatus> GetBitacoraStatusDDL(int solicitudId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<Estatus> list;

                comm = new SqlCommand("GetBitacoraStatusDDL", con);
                comm.Parameters.Add(new SqlParameter("solicitudId", solicitudId) { DbType = System.Data.DbType.Int32 });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<Estatus>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Estatus()
                        {
                            id_status = (int)reader["id_status"],
                            Descripcion = (string)reader["Descripcion"]
                        });
                    }

                    con.Close();

                    return list;
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
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }
            }
        }

        public bool DeleteUsuario(int usuarioId, ref Exception ex)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;

                comm = new SqlCommand("SP_Del_Usuario", con);
                comm.Parameters.Add(new SqlParameter("id_usuario", usuarioId) { DbType = System.Data.DbType.Int32 });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                try
                {
                    con.Open();
                    comm.ExecuteNonQuery();

                    con.Close();

                    return true;
                }
                catch (Exception ex1)
                {
                    ex = ex1;
                    return false;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }
            }
        }

        public int CreateUsuario(tbl_usuario usuario)
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                SqlCommand cmd;

                cmd = new SqlCommand("SP_Ins_Users", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@NEmpleado", usuario.NEmpleado) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@Nombre", usuario.Nombre) { DbType = System.Data.DbType.String });
                cmd.Parameters.Add(new SqlParameter("@id_nperfil", usuario.id_nperfil) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@email", usuario.Email) { DbType = System.Data.DbType.String });
                cmd.Parameters.Add(new SqlParameter("@pass", usuario.pass) { DbType = System.Data.DbType.String });

                try
                {
                    conn.Open();

                    int verificator = 0;
                    verificator = Convert.ToInt32(cmd.ExecuteScalar());

                    conn.Close();

                    return verificator;
                }
                catch (Exception ex)
                {
                    return 0;
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
            }
        }

        public int CreateBitacoraRegistro(tbl_Bitacora Bitacora, ref string FolioProceso)
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                SqlCommand cmd;
                SqlDataReader reader;

                cmd = new SqlCommand("InsBitacora", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_solicitud", Bitacora.id_solicitud) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@id_usuario", Bitacora.id_usuario) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@id_status", Bitacora.id_status) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@fecha", Bitacora.fecha) { DbType = System.Data.DbType.DateTime });
                cmd.Parameters.Add(new SqlParameter("@comentarios", Bitacora.comentarios) { DbType = System.Data.DbType.String });

                try
                {
                    conn.Open();

                    int verificator = 0;

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        verificator = (int)reader["Result"];

                        try
                        {
                            FolioProceso = (string)reader["FolioProcesoLetra"] + '-' + Convert.ToInt32(reader["FolioProcesoNum"]).ToString("D5");
                        }
                        catch { }
                    }

                    conn.Close();

                    return verificator;
                }
                catch (Exception ex)
                {
                    return 0;
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
            }
        }

        public bool ManageRecordatorios(int solicitudId, int newStatusId, int mensajeId)
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                SqlCommand cmd;

                cmd = new SqlCommand("InsUpdAvisosRecordatorios", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@solicitudId", solicitudId) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@newStatusId", newStatusId) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@mensajeId", mensajeId) { DbType = System.Data.DbType.Int32 });

                try
                {
                    conn.Open();

                    cmd.ExecuteNonQuery();

                    conn.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
            }
        }

        public Usuario GetUser(int noEmpleado)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                Usuario usuario;

                comm = new SqlCommand("sp_ExistUser", con);
                comm.Parameters.Add(new SqlParameter("empleado", noEmpleado) { DbType = System.Data.DbType.Int32 });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                usuario = null;

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        usuario = new Usuario();
                        usuario.Id = (int)reader["id_usuario"];
                        usuario.NEmpleado = (int)reader["id_usuario"];
                        usuario.Nombre = reader["Nombre"].ToString();
                        usuario.PerfilId = (int)reader["id_nperfil"];
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
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }
            }
        }
        public Usuario GetSpecificUserInfo(int id_usuario)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                Usuario usuario;

                comm = new SqlCommand("GetSpecificUserInfo", con);
                comm.Parameters.Add(new SqlParameter("id_usuario", id_usuario) { DbType = System.Data.DbType.Int32 });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                usuario = null;

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        usuario = new Usuario();
                        usuario.Id = (int)reader["id_usuario"];
                        usuario.NEmpleado = (int)reader["NEmpleado"];
                        usuario.Nombre = reader["Nombre"].ToString();
                        usuario.Email = reader["email"] == DBNull.Value ? "" : (string)reader["email"];
                        usuario.PerfilId = (int)reader["id_nperfil"];
                        usuario.PerfilDesc = (string)reader["Descripcion"];
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
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }
            }
        }

        #endregion

        #region Plantillas

        public List<tbl_TipoPlantilla> GetPlantillas()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<tbl_TipoPlantilla> list;

                comm = new SqlCommand("SP_Get_TPlantilla", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<tbl_TipoPlantilla>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new tbl_TipoPlantilla()
                        {
                            id_tipoplantilla = (int)reader["id_tipoplantilla"],
                            Descripcion = (string)reader["Descripcion"]
                        });
                    }

                    con.Close();

                    return list;
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

        public List<tbl_ClasificacionPlantilla> GetClasificacionPlantilla(int id_tipoplantilla)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<tbl_ClasificacionPlantilla> list;

                comm = new SqlCommand("SP_Get_ClasificacionPlantilla", con);
                comm.Parameters.Add(new SqlParameter("id_tipoPlantilla", id_tipoplantilla) { DbType = System.Data.DbType.Int32 });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<tbl_ClasificacionPlantilla>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new tbl_ClasificacionPlantilla()
                        {
                            id_clasificacionplantilla = (int)reader["id_clasificacionplantilla"],
                            //id_tipoplantilla = (int)reader["id_tipoplantilla"],
                            Descripcion = (string)reader["Descripcion"]
                        });
                    }

                    con.Close();

                    return list;
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

        public List<tbl_Vigencia> GetVigencia()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<tbl_Vigencia> list;

                comm = new SqlCommand("SP_Get_Vigencia", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<tbl_Vigencia>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new tbl_Vigencia()
                        {
                            id_tipovigencia = (int)reader["id_tipovigencia"],
                            Descripcion = (string)reader["Descripcion"]
                        });
                    }

                    con.Close();

                    return list;
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

        public List<tbl_usuario> GetAutorizador()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<tbl_usuario> list;

                comm = new SqlCommand("GetAutorizadoresGral", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<tbl_usuario>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new tbl_usuario()
                        {
                            id_usuario = (int)reader["id_usuario"],
                            Nombre = (string)reader["Nombre"]
                        });
                    }

                    con.Close();

                    return list;
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

        public List<Abogados> GetAbogado()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<Abogados> list;

                comm = new SqlCommand("SP_Get_Abogados", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<Abogados>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Abogados()
                        {
                            Nombre = (string)reader["nombre"],
                            Id1 = (int)reader["id_usuario"]
                        });
                    }

                    con.Close();

                    return list;
                }
                catch (Exception ex)
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

        public List<Usuario> GetOtorgantesTestigos()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<Usuario> list;

                comm = new SqlCommand("SP_Get_Otorgantes", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<Usuario>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Usuario()
                        {
                            Nombre = (string)reader["nombre"],
                            NEmpleado = (int)reader["id_usuario"]
                        });
                    }

                    con.Close();

                    return list;
                }
                catch (Exception ex)
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

        public List<Plantillas> GetPlantillaJuridica()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<Plantillas> list;

                comm = new SqlCommand("SP_Get_Plantillas", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<Plantillas>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Plantillas()
                        {
                            Id = (int)reader["Id_PlantillaJuridica"],
                            Nombre = (string)reader["Nombre"],
                            Descripcion = (string)reader["Descripcion"],
                            VersionDoc = (int)reader["VersionDoc"],
                            Nombrearchivo = (string)reader["NombreArchivo"].ToString()
                        });
                    }

                    con.Close();

                    return list;
                }
                catch (Exception e)
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

        public Plantillas SelPlantillaSolicitudes(int plantillaId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlCommand commAu;
                SqlCommand commEt;
                SqlDataReader reader;
                Plantillas plantilla;

                comm = new SqlCommand("SP_Get_Plantillas_ById", con);
                comm.Parameters.Add(new SqlParameter("Id_PlantillaJuridica", plantillaId) { DbType = System.Data.DbType.Int32 });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                commAu = new SqlCommand("SP_GET_Autorizadores", con);
                commAu.Parameters.Add(new SqlParameter("Id_PlantillaJuridica", plantillaId) { DbType = System.Data.DbType.Int32 });
                commAu.CommandType = System.Data.CommandType.StoredProcedure;

                commEt = new SqlCommand("SP_GET_Etiquetas", con);
                commEt.Parameters.Add(new SqlParameter("Id_PlantillaJuridica", plantillaId) { DbType = System.Data.DbType.Int32 });
                commEt.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;

                plantilla = new Plantillas();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        plantilla.Plantilla = new tbl_PlantillasJuridicas()
                        {
                            Id_PlantillaJuridica = (int)reader["Id_PlantillaJuridica"],
                            id_clasificacionplantilla = (int)reader["id_clasificacionplantilla"],
                            Nombre = (string)reader["Nombre"],
                            id_usuario = (int)reader["id_usuario"],
                            Descripcion = (string)reader["Descripcion"],
                            id_tipoplantilla = (int)reader["id_tipoplantilla"],
                            PathArchivo = (string)reader["PathArchivo"],
                            id_tipovigencia = (int)reader["id_tipovigencia"]
                        };
                    }

                    reader.Close();
                    reader = commAu.ExecuteReader();

                    plantilla.Autorizadores = new List<Autorizador>();

                    while (reader.Read())
                    {
                        plantilla.Autorizadores.Add(new Autorizador()
                        {
                            Id = (int)reader["id_usuario"],
                            Nombre = (string)reader["Nombre"]
                        });
                    }

                    reader.Close();
                    reader = commEt.ExecuteReader();

                    plantilla.Etiquetas = new List<tbl_Etiquetas>();
                    while (reader.Read())
                    {
                        plantilla.Etiquetas.Add(new tbl_Etiquetas()
                        {
                            id_etiquetas = Convert.ToInt32(reader["id_etiquetas"]),
                            Juridica = Convert.ToBoolean(reader["Juridica"]),
                            Pregunta = (string)reader["Pregunta"],
                        });
                    }

                    con.Close();

                    return plantilla;
                }
                catch (Exception ex)
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
                        comm.Dispose();
                        commAu.Dispose();
                        commEt.Dispose();
                    }
                }
            }
        }

        public Plantillas SelPlantilla(int plantillaId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlCommand commAu;
                SqlCommand commEt;
                SqlDataReader reader;
                Plantillas plantilla;

                comm = new SqlCommand("SP_Get_Plantillas_ById", con);
                comm.Parameters.Add(new SqlParameter("Id_PlantillaJuridica", plantillaId) { DbType = System.Data.DbType.Int32 });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                commAu = new SqlCommand("SP_GET_Autorizadores", con);
                commAu.Parameters.Add(new SqlParameter("Id_PlantillaJuridica", plantillaId) { DbType = System.Data.DbType.Int32 });
                commAu.CommandType = System.Data.CommandType.StoredProcedure;

                commEt = new SqlCommand("SelEtiquetasPlantilla", con);
                commEt.Parameters.Add(new SqlParameter("Id_PlantillaJuridica", plantillaId) { DbType = System.Data.DbType.Int32 });
                commEt.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;

                plantilla = new Plantillas();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        plantilla.Plantilla = new tbl_PlantillasJuridicas()
                        {
                            Id_PlantillaJuridica = (int)reader["Id_PlantillaJuridica"],
                            id_clasificacionplantilla = (int)reader["id_clasificacionplantilla"],
                            Nombre = (string)reader["Nombre"],
                            id_usuario = (int)reader["id_usuario"],
                            Descripcion = (string)reader["Descripcion"],
                            id_tipoplantilla = (int)reader["id_tipoplantilla"],
                            PathArchivo = (string)reader["PathArchivo"],
                            id_tipovigencia = (int)reader["id_tipovigencia"],
                            cartaOescritura = ToInt32_0(reader["cartaOescritura"])
                        };
                    }

                    reader.Close();
                    reader = commAu.ExecuteReader();

                    plantilla.Autorizadores = new List<Autorizador>();

                    while (reader.Read())
                    {
                        plantilla.Autorizadores.Add(new Autorizador()
                        {
                            Id = (int)reader["id_usuario"],
                            Nombre = (string)reader["Nombre"]
                        });
                    }

                    reader.Close();
                    reader = commEt.ExecuteReader();

                    plantilla.Etiquetas = new List<tbl_Etiquetas>();
                    while (reader.Read())
                    {
                        plantilla.Etiquetas.Add(new tbl_Etiquetas()
                        {
                            id_etiquetas = Convert.ToInt32(reader["id_etiquetas"]),
                            Juridica = Convert.ToBoolean(reader["Juridica"]),
                            Pregunta = (string)reader["Pregunta"],
                        });
                    }

                    con.Close();

                    return plantilla;
                }
                catch (Exception ex)
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
                        comm.Dispose();
                        commAu.Dispose();
                        commEt.Dispose();
                    }
                }
            }
        }

        public ConsultaSolicitud llenadoConsultaSolicitud(int Solicitud)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;

                SqlCommand commEt;
                SqlDataReader reader;
                ConsultaSolicitud plantilla;

                comm = new SqlCommand("SP_Get_ConsultaSolicitudById", con);
                comm.Parameters.Add(new SqlParameter("idSolicitud", Solicitud) { DbType = System.Data.DbType.Int32 });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                commEt = new SqlCommand("GetEtiquetasById", con);
                commEt.Parameters.Add(new SqlParameter("idSolicitud", Solicitud) { DbType = System.Data.DbType.Int32 });
                commEt.Parameters.Add(new SqlParameter("excluirJuridicas", 1) { DbType = System.Data.DbType.Int32 }); //es para no cargar etiquetas que son "solo juridico"
                commEt.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;

                plantilla = new ConsultaSolicitud();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        plantilla.Tipo = (string)reader["Tipo"];
                        plantilla.Descripcion = (string)reader["Descripcion"];
                        plantilla.Solicitante = (string)reader["Solicitante"];
                        plantilla.FechaSolicitud = (DateTime)reader["FechaSolicitud"];
                        plantilla.Status = (string)reader["Status"];
                    }

                    if (reader.NextResult())
                    {
                        plantilla.Archivos = new List<ArchivoSolicitud>();

                        while (reader.Read())
                        {
                            plantilla.Archivos.Add(new ArchivoSolicitud()
                            {
                                Id = (int)reader["idArchivoSolicitud"],
                                Nombre = (string)reader["Nombre"],
                                IdTipoDocumento = (int)reader["IdTipoDocumento"],
                                TipoDocumento = (string)reader["TipoDocumento"],
                                //Archivo = (byte[])reader["Archivo"],
                                EsNuevo = false
                            });
                        }
                    }

                    reader.Close();
                    reader = commEt.ExecuteReader();

                    plantilla.Etiquetas = new List<EtiquetaConsulta>();
                    while (reader.Read())
                    {
                        plantilla.Etiquetas.Add(new EtiquetaConsulta()
                        {
                            Pregunta = (string)reader["Pregunta"],
                            Valor = (string)reader["Valor"],
                        });
                    }

                    con.Close();

                    return plantilla;
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
                        comm.Dispose();
                        commEt.Dispose();
                    }
                }
            }
        }

        public bool DelPlantilla(int plantillaId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;

                comm = new SqlCommand("SP_Del_Plantillas", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("Id_PlantillaJuridica", plantillaId) { DbType = System.Data.DbType.Int32 });

                try
                {
                    con.Open();

                    comm.ExecuteNonQuery();

                    con.Close();

                    return true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }
            }
        }

        public bool CreatePlantilla(tbl_PlantillasJuridicas pj, List<tbl_Autorizador> autorizadores, List<tbl_Etiquetas> etiquetas, PlantillaArchivo archivo)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                List<SqlCommand> commAu;
                SqlTransaction trans;
                int id;

                id = 0;
                trans = null;

                comm = new SqlCommand("SP_Ins_Plantillas", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter("id_clasificacionplantilla", pj.id_clasificacionplantilla) { DbType = System.Data.DbType.Int32},
                    new SqlParameter("Nombre", pj.Nombre) { DbType = System.Data.DbType.String} ,
                    new SqlParameter("id_usuario", pj.id_usuario) { DbType = System.Data.DbType.Int32},
                    new SqlParameter("Descripcion", pj.Descripcion) { DbType = System.Data.DbType.String} ,
                    new SqlParameter("id_tipoplantilla", pj.id_tipoplantilla) { DbType = System.Data.DbType.Int32} ,
                    new SqlParameter("id_tipovigencia", pj.id_tipovigencia) { DbType = System.Data.DbType.Int32} ,
                    new SqlParameter("guid", pj.GUID) { DbType = System.Data.DbType.String} ,
                    new SqlParameter("pathArchivo", pj.PathArchivo) { DbType = System.Data.DbType.String} ,
                    new SqlParameter("versionDoc", pj.versionDoc) { DbType = System.Data.DbType.Int32},
                    new SqlParameter("cartaOescritura", pj.cartaOescritura) { DbType = System.Data.DbType.Int32}
                });

                commAu = new List<SqlCommand>();

                foreach (tbl_Autorizador au in autorizadores)
                {
                    commAu.Add(new SqlCommand("SP_Ins_Autorizadores", con));
                    commAu.Last().CommandType = System.Data.CommandType.StoredProcedure;
                    commAu.Last().Parameters.AddRange(new SqlParameter[] {
                        new SqlParameter("id_PlantillaJuridica", id) { DbType = System.Data.DbType.Int32},
                        new SqlParameter("id_usuario", au.id_usuario) { DbType = System.Data.DbType.Int32}
                    });
                }

                foreach (tbl_Etiquetas et in etiquetas)
                {
                    commAu.Add(new SqlCommand("SP_Ins_Etiqueta", con));
                    commAu.Last().CommandType = System.Data.CommandType.StoredProcedure;
                    commAu.Last().Parameters.AddRange(new SqlParameter[] {
                        new SqlParameter("id_PlantillaJuridica", id) { DbType = System.Data.DbType.Int32},
                        new SqlParameter("Pregunta", et.Pregunta) { DbType = System.Data.DbType.String},
                        new SqlParameter("Juridica", et.Juridica) { DbType = System.Data.DbType.Boolean}
                    });
                }

                commAu.Add(new SqlCommand("InsPlantillaArchivo", con));
                commAu.Last().CommandType = System.Data.CommandType.StoredProcedure;
                commAu.Last().Parameters.AddRange(new SqlParameter[] {
                        new SqlParameter("idPlantillaJuridica", id) { DbType = System.Data.DbType.Int32},
                        new SqlParameter("Archivo", archivo.Archivo) { SqlDbType = System.Data.SqlDbType.VarBinary },
                        new SqlParameter("Nombre", archivo.Nombre)
                    });

                try
                {
                    con.Open();
                    trans = con.BeginTransaction();

                    comm.Transaction = trans;

                    id = Convert.ToInt32(comm.ExecuteScalar());

                    if (id > 0)
                    {
                        foreach (SqlCommand co in commAu)
                        {
                            co.Transaction = trans;
                            co.Parameters[0].Value = id;
                            co.ExecuteNonQuery();
                        }
                    }

                    trans.Commit();

                    con.Close();

                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        if (trans != null)
                        {
                            trans.Rollback();
                        }

                        con.Close();
                    }
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                    if (commAu != null)
                    {
                        if (commAu.Count > 0)
                        {
                            foreach (SqlCommand c in commAu)
                            {
                                c.Dispose();
                            }
                        }
                    }
                }
            }
        }

        public bool ActualizarPlantilla(int id, string Descripcion, List<tbl_Autorizador> autorizadores)
        {
            bool res = false;
            bool UpdDescripcion = false;
            bool DelAutorizador = false;
            //Actualizo la descripcion
            UpdDescripcion = UpdatePlantilla(id, Descripcion);

            if (UpdDescripcion == true)
            {
                //borro Autorizadores
                {
                    DelAutorizador = DeleteAutorizador(id);
                }
                if (DelAutorizador == true)
                {
                    UpdateAutorizadores(id, autorizadores);
                    res = true;
                }
            }
            return res;

        }

        public bool DeleteAutorizador(int plantilla)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;

                comm = new SqlCommand("SP_Del_Autorizadores", con);
                comm.Parameters.Add(new SqlParameter("id_PlantillaJuridica", plantilla) { DbType = System.Data.DbType.Int32 });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                try
                {
                    con.Open();
                    comm.ExecuteNonQuery();

                    con.Close();

                    return true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }
            }
        }

        public bool UpdateSolicitudDocument(int SolicitudId, byte[] Archivo)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;

                comm = new SqlCommand("InsArchivoEditadoSolicitudEdit", con);
                comm.Parameters.Add(new SqlParameter("Archivo", Archivo) { SqlDbType = System.Data.SqlDbType.VarBinary });
                comm.Parameters.Add(new SqlParameter("SolicitudId", SolicitudId) { DbType = System.Data.DbType.Int32 });
                comm.Parameters.Add(new SqlParameter("Nombre", DateTime.Now.ToString("ddMMyymmss") + ".docx") { DbType = System.Data.DbType.String });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                try
                {
                    con.Open();
                    comm.ExecuteNonQuery();

                    con.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }
            }
        }

        public bool UpdatePlantilla(int plantilla, string Descripcion)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;

                comm = new SqlCommand("SP_Update_Plantillas", con);
                comm.Parameters.Add(new SqlParameter("Descripcion", Descripcion) { DbType = System.Data.DbType.String });
                comm.Parameters.Add(new SqlParameter("id_plantilla", plantilla) { DbType = System.Data.DbType.Int32 });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                try
                {
                    con.Open();
                    comm.ExecuteNonQuery();

                    con.Close();

                    return true;
                }
                catch (Exception ex)
                {

                    return false;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }
            }
        }

        public bool UpdateAutorizadores(int plantilla, List<tbl_Autorizador> autorizadores)
        {
            bool res = false;
            foreach (tbl_Autorizador au in autorizadores)
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    SqlCommand comm;

                    comm = new SqlCommand("SP_Ins_Autorizadores", con);
                    comm.Parameters.Add(new SqlParameter("id_usuario", au.id_usuario) { DbType = System.Data.DbType.Int32 });
                    comm.Parameters.Add(new SqlParameter("id_PlantillaJuridica", plantilla) { DbType = System.Data.DbType.Int32 });
                    comm.CommandType = System.Data.CommandType.StoredProcedure;

                    try
                    {
                        con.Open();
                        comm.ExecuteNonQuery();

                        con.Close();

                        res = true;

                    }
                    catch
                    {

                        return res;
                    }
                    finally
                    {
                        if (con.State == System.Data.ConnectionState.Open)
                        {
                            con.Close();
                        }
                        if (comm != null)
                        {
                            comm.Dispose();
                        }
                    }
                }

            }
            return res;
        }

        //Catálogo Clasificación Plantillas
        public int CreateTipoPlantilla(tbl_TipoPlantilla tp)
        {
            //CREATE
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlTransaction trans;
                int id;

                id = 0;
                trans = null;

                comm = new SqlCommand("sp_CRUD_TPlantilla", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddRange(new SqlParameter[]
                {
                    new SqlParameter("@OpCRUD", "C") { DbType = System.Data.DbType.String },
                    new SqlParameter("@DescripPlantilla", tp.Descripcion) { DbType = System.Data.DbType.String }

                });

                try
                {
                    con.Open();
                    trans = con.BeginTransaction();

                    comm.Transaction = trans;

                    id = Convert.ToInt32(comm.ExecuteScalar());

                    trans.Commit();
                    con.Close();

                    return id;
                }
                catch
                {
                    return 0;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        if (trans != null)
                        {
                            trans.Rollback();
                        }

                        con.Close();
                    }
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }
            }
        }

        public int UpdateDeleteTipoPlantilla(tbl_TipoPlantilla tp, Char opCRUD)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlTransaction trans;
                int indicador;

                indicador = 0;
                trans = null;

                comm = new SqlCommand("sp_CRUD_TPlantilla", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddRange(new SqlParameter[]
                {
                    new SqlParameter("@OpCRUD", opCRUD) { DbType = System.Data.DbType.String },
                    new SqlParameter("@idPlantilla", tp.id_tipoplantilla) { DbType = System.Data.DbType.Int32 },
                    new SqlParameter("@DescripPlantilla", tp.Descripcion) { DbType = System.Data.DbType.String }
                });

                try
                {
                    con.Open();
                    trans = con.BeginTransaction();

                    comm.Transaction = trans;

                    indicador = Convert.ToInt32(comm.ExecuteScalar());

                    trans.Commit();
                    con.Close();

                    return indicador;
                }
                catch
                {
                    return 0;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        if (trans != null)
                        {
                            trans.Rollback();
                        }

                        con.Close();
                    }
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }

            }
        }

        public List<tbl_TipoPlantilla> GetTipoPlantilla(int idPlantilla)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<tbl_TipoPlantilla> list;

                comm = new SqlCommand("sp_CRUD_TPlantilla", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddRange(new SqlParameter[]
                {
                    new SqlParameter("@OpCRUD", 'R') { DbType = System.Data.DbType.String },
                    new SqlParameter("@idPlantilla", idPlantilla) { DbType = System.Data.DbType.Int32 }
                });

                reader = null;
                list = new List<tbl_TipoPlantilla>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new tbl_TipoPlantilla()
                        {
                            id_tipoplantilla = (int)reader["id_tipoplantilla"],
                            Descripcion = (string)reader["Descripcion"]
                        });
                    }

                    con.Close();
                    return list;
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
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Inserta en catalogo tbl_ClasificacionPlantilla
        /// </summary>
        /// <param name="cp"></param>
        /// <returns></returns>
        public int CreateClasificacionPlantilla(tbl_ClasificacionPlantilla cp)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlTransaction trans;
                int id;

                id = 0;
                trans = null;
                comm = new SqlCommand("SP_CRUD_tbl_ClasificacionPlantilla", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddRange(new SqlParameter[]
                {
                    new SqlParameter("@opCRUD", "C") { DbType = System.Data.DbType.String },
                    new SqlParameter("@idTipoPlantilla", cp.id_tipoplantilla) { DbType = System.Data.DbType.Int32 },
                    new SqlParameter("@DesPlantilla", cp.Descripcion) { DbType = System.Data.DbType.String },
                });

                try
                {
                    con.Open();
                    trans = con.BeginTransaction();

                    comm.Transaction = trans;

                    id = Convert.ToInt32(comm.ExecuteScalar());

                    trans.Commit();
                    con.Close();

                    return id;
                }
                catch
                {
                    return 0;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        if (trans != null)
                        {
                            trans.Rollback();
                        }

                        con.Close();
                    }
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }
            }
        }

        public int UpdateDeleteClasificacionPlantilla(tbl_ClasificacionPlantilla cp, char opCRUD)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlTransaction trans;
                int indicador;

                indicador = 0;
                trans = null;

                comm = new SqlCommand("SP_CRUD_tbl_ClasificacionPlantilla", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddRange(new SqlParameter[]
                {
                    new SqlParameter("@opCRUD", opCRUD) { DbType = System.Data.DbType.String },
                    new SqlParameter("@idClasPlantilla", cp.id_clasificacionplantilla) { DbType = System.Data.DbType.Int32 },
                    new SqlParameter("@idTipoPlantilla", cp.id_tipoplantilla) { DbType = System.Data.DbType.Int32 },
                    new SqlParameter("@DesPlantilla", cp.Descripcion) { DbType = System.Data.DbType.String }
                });

                try
                {
                    con.Open();
                    trans = con.BeginTransaction();

                    comm.Transaction = trans;

                    indicador = Convert.ToInt32(comm.ExecuteScalar());

                    trans.Commit();
                    con.Close();

                    return indicador;
                }
                catch
                {
                    return 0;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        if (trans != null)
                        {
                            trans.Rollback();
                        }

                        con.Close();
                    }
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }

            }
        }

        public List<ClasificacionPlantillas> GetClasificacionTipoPlantilla(int idTipo, int idClasificacion)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<ClasificacionPlantillas> list;

                comm = new SqlCommand("SP_CRUD_tbl_ClasificacionPlantilla", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddRange(new SqlParameter[]
                {
                    new SqlParameter("@opCRUD", "R") { DbType = System.Data.DbType.String },
                    new SqlParameter("@idClasPlantilla", idClasificacion) { DbType = System.Data.DbType.Int32 },
                    new SqlParameter("@idTipoPlantilla", idTipo) { DbType = System.Data.DbType.Int32 }
                });

                reader = null;
                list = new List<ClasificacionPlantillas>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new ClasificacionPlantillas()
                        {
                            IDClasificacion = (int)reader["id_clasificacionplantilla"],
                            IDTipoPlantilla = (int)reader["id_tipoplantilla"],
                            ClasificacionPlantilla = (string)reader["ClasificacionPlantilla"],
                            TipoPlantilla = (string)reader["TipoPlantilla"]
                        });
                    }

                    con.Close();
                    return list;
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
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
            }

            return null;
        }

        #endregion

        #region Solicitudes

        public List<Plantillas> GetPlantillasPorTipo(int tipo)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<Plantillas> list;

                comm = new SqlCommand("SelPlantillasByType", con);
                comm.Parameters.Add(new SqlParameter("TipoPlantilla", tipo) { SqlDbType = System.Data.SqlDbType.Int });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<Plantillas>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Plantillas()
                        {
                            Id = (int)reader["Id_PlantillaJuridica"],
                            Nombre = (string)reader["Nombre"]
                        });
                    }

                    con.Close();

                    return list;
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

        public bool RechazarSolicitud(int solicitudId, int newStatusId, int IdMotivoRechazo, string RechazoDescripcion)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlTransaction trans;
                trans = null;

                comm = new SqlCommand("UpdRechazoSolicitud", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("IdMotivoRechazo", IdMotivoRechazo) { DbType = System.Data.DbType.Int32 });
                comm.Parameters.Add(new SqlParameter("RechazoDescripcion", RechazoDescripcion) { DbType = System.Data.DbType.String });
                comm.Parameters.Add(new SqlParameter("newStatusId", newStatusId) { DbType = System.Data.DbType.Int32 });
                comm.Parameters.Add(new SqlParameter("solicitudId", solicitudId) { DbType = System.Data.DbType.Int32 });

                try
                {
                    con.Open();

                    trans = con.BeginTransaction();

                    comm.Transaction = trans;

                    comm.ExecuteNonQuery();

                    trans.Commit();

                    con.Close();

                    return true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        if (trans != null)
                        {
                            trans.Rollback();
                        }

                        con.Close();
                    }
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }
            }
        }

        public List<MotivoRechazo> GetMotivosRechazo()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<MotivoRechazo> list;

                comm = new SqlCommand("SelMotivoRechazo", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<MotivoRechazo>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new MotivoRechazo()
                        {
                            IdMotivoRechazo = (byte)reader["IdMotivoRechazo"],
                            Descripcion = (string)reader["Descripcion"]
                        });
                    }

                    //list.Add(new MotivoRechazo() //agregar opcion default
                    //{
                    //    IdMotivoRechazo = 0,
                    //    Descripcion = "-- Elige un motivo para rechazar --"
                    //});


                    con.Close();

                    return list;
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

        public List<TipoDocumento> GetTipoDocumento(int idTipoPlanilla)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<TipoDocumento> list;

                comm = new SqlCommand("SelTipoDocumento", con);
                comm.Parameters.Add(new SqlParameter("TipoPlantilla", idTipoPlanilla) { SqlDbType = System.Data.SqlDbType.Int });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<TipoDocumento>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new TipoDocumento()
                        {
                            Id = (int)reader["idTipoDocumento"],
                            Descripcion = (string)reader["TipoDocumento"]
                        });
                    }

                    con.Close();

                    return list;
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

        public PlantillaArchivo GetPlantillaArchivo(int plantillaId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                PlantillaArchivo archivo;

                comm = new SqlCommand("GetPlantillaArchivo", con);
                comm.Parameters.Add(new SqlParameter("PlantillaId", plantillaId) { SqlDbType = System.Data.SqlDbType.Int });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                archivo = null;

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        archivo = new PlantillaArchivo()
                        {
                            IdPlantilla = plantillaId,
                            Archivo = (byte[])reader["PlantillaArchivo"],
                            Nombre = (string)reader["Nombre"]
                        };
                    }

                    con.Close();

                    return archivo;
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

        public PlantillaArchivo GetSolicitudArchivo(int SolicitudId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                PlantillaArchivo archivo;

                comm = new SqlCommand("GetSolicitudArchivo", con);
                comm.Parameters.Add(new SqlParameter("idSolicitud", SolicitudId) { SqlDbType = System.Data.SqlDbType.Int });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                archivo = null;

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        archivo = new PlantillaArchivo()
                        {
                            IdPlantilla = SolicitudId,
                            Archivo = (byte[])reader["Archivo"],
                            Nombre = (string)reader["Nombre"]
                        };
                    }

                    con.Close();

                    return archivo;
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

        public PlantillaArchivo GetArchivosSolicitud(int idArchivo)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                PlantillaArchivo archivo;

                comm = new SqlCommand("GetArchivosSolicitud", con);
                comm.Parameters.Add(new SqlParameter("idArchivo", idArchivo) { SqlDbType = System.Data.SqlDbType.Int });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                archivo = null;

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        archivo = new PlantillaArchivo()
                        {
                            IdPlantilla = idArchivo,
                            Archivo = (byte[])reader["Archivo"],
                            Nombre = (string)reader["Nombre"]
                        };
                    }

                    con.Close();

                    return archivo;
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

        public bool CreateSolicitud(Solicitud sol, List<SolicitudEtiqueta> etiquetas, List<ArchivoSolicitud> archivos, byte[] archivo, ref Exception ex)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm = null;
                List<SqlCommand> commAu = null;
                SqlTransaction trans;
                int id;

                id = 0;
                trans = null;

                try
                {
                    comm = new SqlCommand("InsSolicitud", con);
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter("idPlantilla", sol.IdPlantilla) { DbType = System.Data.DbType.Int32},
                    new SqlParameter("idStatus", sol.IdStatus) { DbType = System.Data.DbType.Int32} ,
                    //new SqlParameter("idAutorizador", sol.IdAutorizador) { DbType = System.Data.DbType.Int32},
                    new SqlParameter("idUsuario",sol.IdUsuario){DbType=System.Data.DbType.Int32},
                    new SqlParameter("folio",sol.Folio){DbType=System.Data.DbType.String}
                    });

                    commAu = new List<SqlCommand>();

                    foreach (SolicitudEtiqueta et in etiquetas)
                    {
                        commAu.Add(new SqlCommand("InsEtiquetaSolicitud", con));
                        commAu.Last().CommandType = System.Data.CommandType.StoredProcedure;
                        commAu.Last().Parameters.AddRange(new SqlParameter[] {
                        new SqlParameter("SolicitudId", id) { DbType = System.Data.DbType.Int32},
                        new SqlParameter("EtiquetaId", et.IdEtiqueta) { DbType = System.Data.DbType.Int32},
                        new SqlParameter("Valor", et.Valor) { DbType = System.Data.DbType.String}
                        });
                    }


                    if (archivo != null)
                    {
                        foreach (ArchivoSolicitud ar in archivos)
                        {
                            commAu.Add(new SqlCommand("InsArchivoSolicitud", con));
                            commAu.Last().CommandType = System.Data.CommandType.StoredProcedure;
                            commAu.Last().Parameters.AddRange(new SqlParameter[] {
                            new SqlParameter("SolicitudId", id) { DbType = System.Data.DbType.Int32},
                            new SqlParameter("TipoArchivoId", ar.IdTipoDocumento) { DbType = System.Data.DbType.Int32 },
                            new SqlParameter("Nombre", ar.Nombre) { DbType = System.Data.DbType.String} ,
                            new SqlParameter("Ruta", ar.Ruta) { DbType = System.Data.DbType.String},
                            new SqlParameter("Archivo", ar.Archivo) // { SqlDbType = System.Data.SqlDbType.VarBinary }
                            });
                        }
                    }

                    commAu.Add(new SqlCommand("InsArchivoEditadoSolicitud", con));
                    commAu.Last().CommandType = System.Data.CommandType.StoredProcedure;
                    commAu.Last().Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter("id_SolicitudId", id) { DbType = System.Data.DbType.Int32},
                    new SqlParameter("Archivo", archivo),
                    new SqlParameter("Nombre", DateTime.Now.ToString("ddMMyymmss") + ".docx") { DbType = System.Data.DbType.String}
                    });


                    con.Open();
                    trans = con.BeginTransaction();

                    comm.Transaction = trans;
                    id = Convert.ToInt32(comm.ExecuteScalar().ToString());
                    sol.Id = id;

                    if (id > 0)
                    {
                        foreach (SqlCommand co in commAu)
                        {
                            co.Parameters[0].Value = id;
                            co.Transaction = trans;
                            co.ExecuteNonQuery();
                        }
                    }

                    trans.Commit();
                    con.Close();

                    return true;
                }
                catch (Exception e)
                {
                    ex = e;
                    return false;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        if (trans != null)
                        {
                            trans.Rollback();
                        }

                        con.Close();
                    }
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                    if (commAu != null)
                    {
                        if (commAu.Count > 0)
                        {
                            foreach (SqlCommand c in commAu)
                            {
                                c.Dispose();
                            }
                        }
                    }
                }
            }
        }

        public List<Solicitud> GetSolicitudesPorUsuario(int userId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<Solicitud> list;

                comm = new SqlCommand("SelSolicitudUsuario", con);
                comm.Parameters.Add(new SqlParameter("UsuarioId", userId) { SqlDbType = System.Data.SqlDbType.Int });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<Solicitud>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Solicitud()
                        {
                            Id = (int)reader["id_Solicitud"],
                            Clasificacion = (string)reader["TipoPlantilla"],
                            Folio = (string)reader["Folio"],
                            Tipo = (string)reader["Nombre"],
                            Status = (string)reader["Status"],
                            IdStatus = (int)reader["id_Status"],
                            Fecha = reader["fechaCreacion"] == DBNull.Value ? new DateTime?() : (DateTime)reader["fechaCreacion"],
                            IdPlantilla = (int)reader["Id_PlantillaJuridica"],
                            Solicitante = reader["NEmpleado"].ToString() + " - " + (string)reader["Usuario"],
                            Correo = (string)reader["Correo"],
                            EstatusAutPrev = (string)reader["EstatusAutPrev"],
                            Id_voBoSol = (int)reader["Id_voBoSol"],
                        });
                    }

                    con.Close();

                    return list;
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
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
            }

            return null;
        }

        public List<Solicitud> GetSolicitudesRecep(int userId, int id_nperfil)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<Solicitud> list;

                comm = new SqlCommand("SelSolicitudRecep", con);
                comm.Parameters.Add(new SqlParameter("UsuarioId", userId) { SqlDbType = System.Data.SqlDbType.Int });
                comm.Parameters.Add(new SqlParameter("id_nperfil", id_nperfil) { SqlDbType = System.Data.SqlDbType.Int });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<Solicitud>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Solicitud()
                        {
                            Id = (int)reader["id_Solicitud"],
                            Clasificacion = (string)reader["TipoPlantilla"],
                            Folio = (string)reader["Folio"],
                            Tipo = (string)reader["Nombre"],
                            Status = (string)reader["Status"],
                            IdStatus = (int)reader["id_Status"],
                            Fecha = reader["fechaCreacion"] == DBNull.Value ? new DateTime?() : (DateTime)reader["fechaCreacion"],
                            IdPlantilla = (int)reader["Id_PlantillaJuridica"],
                            FechaAutorizacion = reader["fechaAutorizacion"] == DBNull.Value ? new DateTime?() : (DateTime)reader["fechaAutorizacion"],
                            Solicitante = reader["NEmpleado"].ToString() + " - " + (string)reader["Usuario"],
                            NombreAutorizador = reader["NombreAutorizador"] == DBNull.Value ? "" : (string)reader["NombreAutorizador"]
                        });
                    }

                    con.Close();

                    return list;
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
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
            }

            return null;
        }

        public List<Solicitud> GetSolicitudesAutorizablesPorUsuario(int userId, int id_nperfil)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<Solicitud> list;

                comm = new SqlCommand("SelSolicitudUsuarioAut", con);
                comm.Parameters.Add(new SqlParameter("UsuarioId", userId) { SqlDbType = System.Data.SqlDbType.Int });
                comm.Parameters.Add(new SqlParameter("id_nperfil", id_nperfil) { SqlDbType = System.Data.SqlDbType.Int });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<Solicitud>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Solicitud()
                        {
                            Id = (int)reader["id_Solicitud"],
                            Clasificacion = (string)reader["TipoPlantilla"],
                            Folio = (string)reader["Folio"],
                            Tipo = (string)reader["Nombre"],
                            Status = (string)reader["Status"],
                            IdStatus = (int)reader["id_Status"],
                            Fecha = reader["fechaCreacion"] == DBNull.Value ? new DateTime?() : (DateTime)reader["fechaCreacion"],
                            IdPlantilla = (int)reader["Id_PlantillaJuridica"],
                            Solicitante = reader["NEmpleado"].ToString() + " - " + (string)reader["Usuario"]
                        });
                    }

                    con.Close();

                    return list;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
            }

            return null;
        }

        public List<Solicitud> GetSolicitudesBitacora(int userId, int id_nperfil)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<Solicitud> list;

                comm = new SqlCommand("SelSolicitudBitacora", con);
                comm.Parameters.Add(new SqlParameter("UsuarioId", userId) { SqlDbType = System.Data.SqlDbType.Int });
                comm.Parameters.Add(new SqlParameter("id_nperfil", id_nperfil) { SqlDbType = System.Data.SqlDbType.Int });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<Solicitud>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Solicitud()
                        {
                            Id = (int)reader["id_Solicitud"],
                            Clasificacion = (string)reader["TipoPlantilla"],
                            Folio = (string)reader["Folio"],
                            FolioProceso = (reader["FolioProcesoLetra"] == DBNull.Value ? "" : (string)reader["FolioProcesoLetra"]) + '-' + (reader["FolioProcesoNum"] == DBNull.Value ? "" : Convert.ToInt32(reader["FolioProcesoNum"]).ToString("D5")),
                            Tipo = (string)reader["Nombre"],
                            Status = (string)reader["Status"],
                            Fecha = reader["fechaCreacion"] == DBNull.Value ? new DateTime?() : (DateTime)reader["fechaCreacion"],
                            FechaAutorizacion = reader["FechaAutorizacion"] == DBNull.Value ? new DateTime?() : (DateTime)reader["FechaAutorizacion"],
                            FechaAsignacion = reader["FechaAsignada"] == DBNull.Value ? new DateTime?() : (DateTime)reader["FechaAsignada"],
                            IdPlantilla = (int)reader["Id_PlantillaJuridica"],
                            Solicitante = reader["NombreSolicitador"].ToString(),
                            NombreAutorizador = reader["NombreAutorizador"] == DBNull.Value ? "" : (string)reader["NombreAutorizador"],
                            NombreAbogado = reader["NombreAbogado"] == DBNull.Value ? "" : (string)reader["NombreAbogado"]
                        });
                    }

                    con.Close();

                    return list;
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
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
            }

            return null;
        }

        public List<Solicitud> GetSolicitudesFiltradas(int userId, int id_nperfil, int? tipoPlantilla, int? abogadoId, DateTime? fechaInicio, DateTime? fechaFin)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<Solicitud> list;

                comm = new SqlCommand("SelSolicitudFiltradas", con);
                comm.Parameters.Add(new SqlParameter("UsuarioId", userId) { SqlDbType = System.Data.SqlDbType.Int });
                comm.Parameters.Add(new SqlParameter("IdAbogado", abogadoId) { SqlDbType = System.Data.SqlDbType.Int });
                comm.Parameters.Add(new SqlParameter("IdTipoSolicitud", tipoPlantilla) { SqlDbType = System.Data.SqlDbType.Int });
                comm.Parameters.Add(new SqlParameter("FechaIni", fechaInicio) { SqlDbType = System.Data.SqlDbType.DateTime });
                comm.Parameters.Add(new SqlParameter("FechaFin", fechaFin) { SqlDbType = System.Data.SqlDbType.DateTime });
                comm.Parameters.Add(new SqlParameter("id_nperfil", id_nperfil) { SqlDbType = System.Data.SqlDbType.Int });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<Solicitud>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Solicitud()
                        {
                            Id = (int)reader["id_Solicitud"],
                            Clasificacion = (string)reader["TipoPlantilla"],
                            Folio = (string)reader["Folio"],
                            FolioProceso = (reader["FolioProcesoLetra"] == DBNull.Value ? "" : (string)reader["FolioProcesoLetra"]) + '-' + (reader["FolioProcesoNum"] == DBNull.Value ? "" : Convert.ToInt32(reader["FolioProcesoNum"]).ToString("D5")),
                            Tipo = (string)reader["Nombre"],
                            Status = (string)reader["Status"],
                            Fecha = reader["fechaCreacion"] == DBNull.Value ? new DateTime?() : (DateTime)reader["fechaCreacion"],
                            FechaAutorizacion = reader["FechaAutorizacion"] == DBNull.Value ? new DateTime?() : (DateTime)reader["FechaAutorizacion"],
                            FechaAsignacion = reader["FechaAsignada"] == DBNull.Value ? new DateTime?() : (DateTime)reader["FechaAsignada"],
                            IdPlantilla = (int)reader["Id_PlantillaJuridica"],
                            Solicitante = reader["NombreSolicitador"].ToString(),
                            NombreAutorizador = reader["NombreAutorizador"] == DBNull.Value ? "" : (string)reader["NombreAutorizador"],
                            NombreAbogado = reader["NombreAbogado"] == DBNull.Value ? "" : (string)reader["NombreAbogado"]
                        });
                    }

                    con.Close();

                    return list;
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
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
            }

            return null;
        }

        public Solicitud GetSolicitud(int solicitudId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                Solicitud sol;

                comm = new SqlCommand("GetSolicitud", con);
                comm.Parameters.Add(new SqlParameter("SolicitudId", solicitudId) { SqlDbType = System.Data.SqlDbType.Int });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                sol = null;

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        sol = new Solicitud()
                        {
                            Id = (int)reader["id_Solicitud"],
                            Clasificacion = (string)reader["TipoPlantilla"],
                            IdTipoPlantilla = (int)reader["IdTipoPlantilla"],
                            Folio = (string)reader["Folio"],
                            Consecutivo = (string)reader["Consecutivo"],
                            Tipo = (string)reader["Nombre"],
                            Status = (string)reader["Status"],
                            IdStatus = (int)reader["id_Status"],
                            Fecha = reader["fechaCreacion"] == DBNull.Value ? new DateTime?() : (DateTime)reader["fechaCreacion"],
                            Descripcion = (string)reader["Descripcion"],
                            Solicitante = reader["NEmpleado"].ToString() + " - " + (string)reader["Usuario"],
                            IdPlantilla = (int)reader["Id_PlantillaJuridica"]
                        };
                    }

                    if (reader.NextResult())
                    {
                        sol.Etiquetas = new List<SolicitudEtiqueta>();
                        while (reader.Read())
                        {
                            sol.Etiquetas.Add(new SolicitudEtiqueta()
                            {
                                IdEtiqueta = (int)reader["IdSolicitudEtiqueta"],
                                Etiqueta = (string)reader["Pregunta"],
                                Valor = (string)reader["Valor"]
                            });
                        }
                    }

                    con.Close();

                    return sol;
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
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
            }

            return null;
        }

        public string GetFolio(int idtipo) //obtiene el siguiente folio a utilizar
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                string folio = null;

                comm = new SqlCommand("GetFolio", con);
                comm.Parameters.Add(new SqlParameter("idTipo", idtipo) { SqlDbType = System.Data.SqlDbType.Int });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;


                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {

                        if (idtipo == 1) // 1 = poderes
                            folio = "P-" + Convert.ToInt32(reader["Folio"]).ToString("D4");

                        if (idtipo == 2) // 2 = contratos
                            folio = "C-" + Convert.ToInt32(reader["Folio"]).ToString("D4");

                        if (idtipo == 3) // 2 = contratos
                            folio = "S-" + Convert.ToInt32(reader["Folio"]).ToString("D4");

                    }

                    con.Close();

                    return folio;
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

        public Folio SelFolioCompleto(int solicitudId) //obtiene el folio completo existente
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader = null;
                Folio Folio = null;

                comm = new SqlCommand("SelFolioCompleto", con);
                comm.Parameters.Add(new SqlParameter("solicitudId", solicitudId) { SqlDbType = System.Data.SqlDbType.Int });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        Folio = new Folio()
                        {
                            NumFolio = (string)reader["NumFolio"],
                            Consecutivo = (short)reader["Consecutivo"],
                            Año = (short)reader["Año"],
                            FolioCompleto = (string)reader["FolioCompleto"]
                        };
                    }

                    con.Close();

                    return Folio;
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

        public bool UpdEtiquetas(List<tblSolicitudEtiquetas> etiquetas)
        {
            bool res = false;

            foreach (tblSolicitudEtiquetas e in etiquetas)
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    SqlCommand comm;

                    comm = new SqlCommand("UpdEtiquetas", con);
                    comm.Parameters.Add(new SqlParameter("IdSolicitudEtiqueta", e.IdSolicitudEtiqueta) { DbType = System.Data.DbType.Int32 });
                    comm.Parameters.Add(new SqlParameter("id_usuarioLigado", e.id_usuarioLigado) { DbType = System.Data.DbType.Int32 });
                    comm.CommandType = System.Data.CommandType.StoredProcedure;

                    try
                    {
                        con.Open();

                        comm.ExecuteNonQuery();

                        con.Close();

                        res = true;
                    }

                    catch (Exception ex)
                    {
                        return res;
                    }

                    finally
                    {
                        if (con.State == System.Data.ConnectionState.Open)
                        {
                            con.Close();
                        }
                        if (comm != null)
                        {
                            comm.Dispose();
                        }
                    }
                }
            }

            return res;
        }

        public int UpdSolicitudAsignado(int solicitudId, int asignadoId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                int statusId;

                comm = new SqlCommand("UpdSolicitudAsignado", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("asignadoId", asignadoId) { DbType = System.Data.DbType.Int32 });
                comm.Parameters.Add(new SqlParameter("solicitudId", solicitudId) { DbType = System.Data.DbType.Int32 });

                try
                {
                    con.Open();

                    statusId = Convert.ToInt32(comm.ExecuteScalar().ToString());

                    con.Close();

                    return statusId;
                }
                catch
                {
                    return -1;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }
            }
        }

        public bool UpdSolicitudStatusId(int solicitudId, int newStatusId, int usuarioId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;

                comm = new SqlCommand("UpdSolicitudStatusId", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("newStatusId", newStatusId) { DbType = System.Data.DbType.Int32 });
                comm.Parameters.Add(new SqlParameter("solicitudId", solicitudId) { DbType = System.Data.DbType.Int32 });
                comm.Parameters.Add(new SqlParameter("usuarioId", usuarioId) { DbType = System.Data.DbType.Int32 });

                try
                {
                    con.Open();

                    comm.ExecuteNonQuery();

                    con.Close();

                    return true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }
            }
        }

        public bool DelSolicitud(int solicitudId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;

                comm = new SqlCommand("DelSolicitud", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("SolicitudId", solicitudId) { DbType = System.Data.DbType.Int32 });

                try
                {
                    con.Open();

                    comm.ExecuteNonQuery();

                    con.Close();

                    return true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }
            }
        }

        public List<Solicitud> GetSolicitudesAsignar()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<Solicitud> list;

                comm = new SqlCommand("GetRecepcionAsignacion", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<Solicitud>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Solicitud()
                        {
                            Id = (int)reader["id_Solicitud"],
                            Fecha = reader["Fecha"] == DBNull.Value ? new DateTime?() : (DateTime)reader["Fecha"],
                            Folio = (string)reader["Folio"],
                            Tipo = (string)reader["tipoplantilla"],
                            Solicitante = (string)reader["nombreSol"],
                            Clasificacion = (string)reader["clasificacionplantilla"],
                            FechaAutorizacion = reader["fechaAutorizacion"] == DBNull.Value ? new DateTime?() : (DateTime)reader["fechaAutorizacion"],
                            NombreAutorizador = (string)reader["nombreAut"],
                            Status = (string)reader["status"],

                        });
                    }

                    con.Close();

                    return list;
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
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
            }

            return null;
        }
        #endregion

        #region Reportes

        public List<ReporteStatus> GetReportePoderesStatus()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<ReporteStatus> list;
                comm = new SqlCommand("GetReportePoderesporEstatus", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<ReporteStatus>();
                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new ReporteStatus()
                        {
                            Solicitudes = (int)reader["Solicitudes"],
                            Nombre = (string)reader["Nombre"].ToString()
                        });
                    }

                    con.Close();

                    return list;
                }
                catch (Exception ex)
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

        public List<ReporteStatus> GetReporteContratosStatus()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<ReporteStatus> list;
                comm = new SqlCommand("GetReporteContratosporEstatus", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<ReporteStatus>();
                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new ReporteStatus()
                        {
                            Solicitudes = (int)reader["Solicitudes"],
                            Nombre = (string)reader["Nombre"].ToString()
                        });
                    }

                    con.Close();

                    return list;
                }
                catch (Exception ex)
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

        public List<ReporteCartasPoder> GetReporteCartasPoder()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<ReporteCartasPoder> list;

                comm = new SqlCommand("GetCartasPoderReporte", con);

                comm.Parameters.Add(new SqlParameter("fechaIni", DateTime.Now) { DbType = System.Data.DbType.Date });
                comm.Parameters.Add(new SqlParameter("fechaFin", DateTime.Now) { DbType = System.Data.DbType.Date });

                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<ReporteCartasPoder>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new ReporteCartasPoder()
                        {
                            Solicitudes = (int)reader["Solicitudes"],
                            Nombre = (string)reader["Nombre"].ToString()
                        });
                    }

                    con.Close();

                    return list;
                }
                catch (Exception ex)
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

        public int ComplementarModificarSolicitud(int action, int solicitudId, List<SolicitudEtiqueta> etiquetas, byte[] archivo, List<ArchivoSolicitud> archivosNuevos, List<ArchivoSolicitud> archivosEliminados)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                List<SqlCommand> commAu;
                SqlTransaction trans;
                int id;

                id = 0;
                trans = null;

                comm = new SqlCommand("UpdSolicitud2", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter("solicitudId", solicitudId) { DbType = System.Data.DbType.Int32},
                    new SqlParameter("action", action) { DbType = System.Data.DbType.Int32}
                });

                commAu = new List<SqlCommand>();

                foreach (SolicitudEtiqueta et in etiquetas)
                {
                    commAu.Add(new SqlCommand("UpdEtiquetaSolicitud", con));
                    commAu.Last().CommandType = System.Data.CommandType.StoredProcedure;
                    commAu.Last().Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter("IdSolicitudEtiqueta", et.IdEtiqueta) { DbType = System.Data.DbType.Int32},
                        new SqlParameter("Valor", et.Valor) { DbType = System.Data.DbType.String}
                    });
                }


                if (archivosNuevos != null)
                {
                    foreach (ArchivoSolicitud archivoNuevo in archivosNuevos)
                    {
                        commAu.Add(new SqlCommand("InsArchivoSolicitud", con));
                        commAu.Last().CommandType = System.Data.CommandType.StoredProcedure;
                        commAu.Last().Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter("SolicitudId", solicitudId) { DbType = System.Data.DbType.Int32},
                            new SqlParameter("TipoArchivoId", archivoNuevo.IdTipoDocumento) { DbType = System.Data.DbType.Int32 },
                            new SqlParameter("Nombre", archivoNuevo.Nombre) { DbType = System.Data.DbType.String},
                            new SqlParameter("Archivo", archivoNuevo.Archivo)
                        });
                    }
                }


                if (archivosEliminados != null)
                {
                    foreach (ArchivoSolicitud ae in archivosEliminados)
                    {
                        commAu.Add(new SqlCommand("DelArchivoSolicitud", con));
                        commAu.Last().CommandType = System.Data.CommandType.StoredProcedure;
                        commAu.Last().Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter("IdArchivoSolicitud", ae.IdArchivoSolicitud) { DbType = System.Data.DbType.Int32}
                        });
                    }
                }


                commAu.Add(new SqlCommand("InsArchivoEditadoSolicitudEdit", con));
                commAu.Last().CommandType = System.Data.CommandType.StoredProcedure;
                commAu.Last().Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter("Archivo", archivo) { SqlDbType = System.Data.SqlDbType.VarBinary },
                    new SqlParameter("SolicitudId", solicitudId) { DbType = System.Data.DbType.Int32},
                    new SqlParameter("Nombre", DateTime.Now.ToString("ddMMyymmss") + ".docx") { DbType = System.Data.DbType.String}
                    });


                try
                {
                    con.Open();
                    trans = con.BeginTransaction();

                    comm.Transaction = trans;
                    id = Convert.ToInt32(comm.ExecuteScalar().ToString());

                    if (id > 0)
                    {
                        foreach (SqlCommand co in commAu)
                        {
                            //co.Parameters[0].Value = id;
                            co.Transaction = trans;
                            co.ExecuteNonQuery();
                        }
                    }

                    trans.Commit();
                    con.Close();

                    return id;
                }
                catch
                {
                    return -1;
                }

                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        if (trans != null)
                        {
                            trans.Rollback();
                        }

                        con.Close();
                    }

                    if (comm != null)
                    {
                        comm.Dispose();
                    }

                    if (commAu != null)
                    {
                        if (commAu.Count > 0)
                        {
                            foreach (SqlCommand c in commAu)
                            {
                                c.Dispose();
                            }
                        }
                    }
                }
            }
        }

        public List<ReporteCartasPoder> GetReporteContratos()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<ReporteCartasPoder> list;

                comm = new SqlCommand("GetGeneralContratosReporte", con);

                comm.Parameters.Add(new SqlParameter("fechaIni", DateTime.Now) { DbType = System.Data.DbType.Date });
                comm.Parameters.Add(new SqlParameter("fechaFin", DateTime.Now) { DbType = System.Data.DbType.Date });

                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;
                list = new List<ReporteCartasPoder>();

                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new ReporteCartasPoder()
                        {
                            Solicitudes = (int)reader["Solicitudes"],
                            Nombre = (string)reader["Nombre"].ToString()
                        });
                    }

                    con.Close();

                    return list;
                }
                catch (Exception ex)
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

        public int ToInt32_0(object input) //remove if possible, use the one in PaginaBase instead ****
        {
            try
            {
                if (input == null)
                    return 0;
                else
                    return Convert.ToInt32(input);
            }

            catch (Exception ex)
            {
                return 0;
            }
        }

        #endregion


        #region ConfigPlantilasVobo
        public List<PlantillasVoBo> GetPlantillasVobo()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<PlantillasVoBo> list = new List<PlantillasVoBo>();

                comm = new SqlCommand("PlantillasVoBo_sUp", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;


                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new PlantillasVoBo()
                        {
                            Id_PlantillaJuridica = (int)reader["Id_PlantillaJuridica"],
                            Nombre = (string)reader["Nombre"],
                            Descripcion = (string)reader["Descripcion"],
                            DescClas = (string)reader["DescClas"],
                            voBo = (bool)reader["voBo"],

                        });
                    }

                    con.Close();

                    return list;
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
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
            }
        }

        public int PlantillasVoBo_uUp(int Id_PlantillaJuridica, bool voBo)
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                SqlCommand cmd;

                cmd = new SqlCommand("PlantillasVoBo_uUp", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id_PlantillaJuridica", Id_PlantillaJuridica) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@voBo", voBo) { DbType = System.Data.DbType.Boolean });


                try
                {
                    conn.Open();

                    int verificator = 0;
                    verificator = Convert.ToInt32(cmd.ExecuteScalar());

                    conn.Close();

                    return verificator;
                }
                catch (Exception ex)
                {
                    return 0;
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
            }
        }

        public int? ValidaFolioSolicitud_sUp(string folio)
        {
            int? verificador = 0;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<PlantillasVoBo> list = new List<PlantillasVoBo>();

                comm = new SqlCommand("ValidaFolioSolicitud_sUp", con);
                comm.Parameters.Add(new SqlParameter("@folio", folio) { DbType = System.Data.DbType.String });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;


                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        verificador = (int)reader["id_Solicitud"];
                    }

                    con.Close();

                    return verificador;
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
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
            }
        }

        public bool tbl_VoBoSolicitudes_iUp(tbl_VoBoSolicitudes voBo, List<PlantillaArchivo> archivos, List<string> correos)
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                SqlCommand cmd;
                SqlTransaction trans = null;
                List<SqlCommand> commAu = new List<SqlCommand>();

                cmd = new SqlCommand("tbl_VoBoSolicitudes_iUp", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idSolicitud", voBo.idSolicitud) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@comentarios", voBo.comentarios) { DbType = System.Data.DbType.String });
                cmd.Parameters.Add(new SqlParameter("@correo1", voBo.correo1) { DbType = System.Data.DbType.String });
                cmd.Parameters.Add(new SqlParameter("@correo2", voBo.correo2) { DbType = System.Data.DbType.String });
                cmd.Parameters.Add(new SqlParameter("@correo3", voBo.correo3) { DbType = System.Data.DbType.String });
                cmd.Parameters.Add(new SqlParameter("@correo4", voBo.correo4) { DbType = System.Data.DbType.String });
                cmd.Parameters.Add(new SqlParameter("@correo5", voBo.correo5) { DbType = System.Data.DbType.String });
                cmd.Parameters.Add(new SqlParameter("@id_user", voBo.id_user) { DbType = System.Data.DbType.Int32 });

                try
                {
                    conn.Open();

                    trans = conn.BeginTransaction();

                    cmd.Transaction = trans;

                    int id = 0;
                    id = Convert.ToInt32(cmd.ExecuteScalar());

                    foreach (var archivo in archivos)
                    {
                        commAu.Add(new SqlCommand("tbl_VoBoSolicitudesArchivos_iUp", conn));
                        commAu.Last().CommandType = System.Data.CommandType.StoredProcedure;
                        commAu.Last().Parameters.AddRange(new SqlParameter[] {
                        new SqlParameter("Id_voBoSol", id) { DbType = System.Data.DbType.Int32},
                        new SqlParameter("Archivo", archivo.Archivo) { SqlDbType = System.Data.SqlDbType.VarBinary },
                        new SqlParameter("Nombre", archivo.Nombre)
                    });
                    }

                    foreach (string item in correos)
                    {
                        if (item.Length > 0)
                        {
                            commAu.Add(new SqlCommand("tbl_VoBoSolicitudesRetro_iUp", conn));
                            commAu.Last().CommandType = System.Data.CommandType.StoredProcedure;
                            commAu.Last().Parameters.AddRange(new SqlParameter[] {
                            new SqlParameter("Id_voBoSol", id) { DbType = System.Data.DbType.Int32},
                            new SqlParameter("correo", item) { SqlDbType = System.Data.SqlDbType.VarChar },
                            new SqlParameter("id_user", voBo.id_user)});
                        }
                    }


                    if (id > 0)
                    {
                        foreach (SqlCommand co in commAu)
                        {
                            co.Transaction = trans;
                            co.Parameters[0].Value = id;
                            co.ExecuteNonQuery();
                        }
                    }

                    trans.Commit();

                    conn.Close();

                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        if (trans != null)
                        {
                            trans.Rollback();
                        }

                        conn.Close();
                    }
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                    if (commAu != null)
                    {
                        if (commAu.Count > 0)
                        {
                            foreach (SqlCommand c in commAu)
                            {
                                c.Dispose();
                            }
                        }
                    }
                }
            }
        }



        public List<tbl_VoBoSolicitudesRetro> tbl_VoBoSolicitudesRetro_sUp(int Id_voBoSol)
        {

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<tbl_VoBoSolicitudesRetro> list = new List<tbl_VoBoSolicitudesRetro>();

                comm = new SqlCommand("tbl_VoBoSolicitudesRetro_sUp", con);
                comm.Parameters.Add(new SqlParameter("@Id_voBoSol", Id_voBoSol) { DbType = System.Data.DbType.Int32 });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;


                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        var item = new tbl_VoBoSolicitudesRetro();




                        item.Id_voBoSol = (int)reader["Id_voBoSol"];
                        item.Id_voBoSolRetro = (int)reader["Id_voBoSolRetro"];
                        item.correo = (string)reader["correo"];
                        item.comentariosNegocio = (string)reader["comentariosNegocio"];
                        item.riesgosDestacados = (string)reader["riesgosDestacados"];
                        item.id_user = (int)reader["id_user"];
                        if (reader["autorizado"] != DBNull.Value)
                        {
                            item.autorizado = (bool?)reader["autorizado"];
                        }
                        if (reader["fec_autorizado"] != DBNull.Value)
                        {
                            item.fec_autorizado = (DateTime)reader["fec_autorizado"];
                        }

                        list.Add(item);
                    }

                    con.Close();

                    return list;
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
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
            }
        }

        public List<SolicitudRetro> tbl_VoBoSolicitudesRetroById_sUp(int Id_voBoSolRetro)
        {

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<SolicitudRetro> list = new List<SolicitudRetro>();

                comm = new SqlCommand("tbl_VoBoSolicitudesRetroById_sUp", con);
                comm.Parameters.Add(new SqlParameter("@Id_voBoSolRetro", Id_voBoSolRetro) { DbType = System.Data.DbType.Int32 });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;


                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        var item = new SolicitudRetro();




                        item.Id_voBoSol = (int)reader["Id_voBoSol"];
                        item.Id_voBoSolRetro = (int)reader["Id_voBoSolRetro"];
                        item.correo = (string)reader["correo"];
                        item.comentariosNegocio = (string)reader["comentariosNegocio"];
                        item.riesgosDestacados = (string)reader["riesgosDestacados"];
                        item.id_user = (int)reader["id_user"];
                        if (reader["autorizado"] != DBNull.Value)
                        {
                            item.autorizado = (bool?)reader["autorizado"];
                        }
                        if (reader["fec_autorizado"] != DBNull.Value)
                        {
                            item.fec_autorizado = (DateTime)reader["fec_autorizado"];
                        }

                        item.Detalle = (string)reader["comentarios"];
                        list.Add(item);
                    }

                    con.Close();

                    return list;
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
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
            }
        }

        public int tbl_VoBoSolicitudesRetro_uUp(tbl_VoBoSolicitudesRetro solicitud)
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                SqlCommand cmd;

                cmd = new SqlCommand("tbl_VoBoSolicitudesRetro_uUp", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id_voBoSolRetro", solicitud.Id_voBoSolRetro) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@comentariosNegocio", solicitud.comentariosNegocio) { DbType = System.Data.DbType.String });
                cmd.Parameters.Add(new SqlParameter("@riesgosDestacados", solicitud.riesgosDestacados) { DbType = System.Data.DbType.String });
                cmd.Parameters.Add(new SqlParameter("@autorizado", solicitud.autorizado) { DbType = System.Data.DbType.Boolean });


                try
                {
                    conn.Open();

                    int verificator = 0;
                    verificator = Convert.ToInt32(cmd.ExecuteScalar());

                    conn.Close();

                    return verificator;
                }
                catch (Exception ex)
                {
                    return 0;
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
            }
        }

        public List<tbl_VoBoSolicitudes> tbl_VoBoSolicitudes_sUp()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<tbl_VoBoSolicitudes> list = new List<tbl_VoBoSolicitudes>();

                comm = new SqlCommand("tbl_VoBoSolicitudes_sUp", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;


                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new tbl_VoBoSolicitudes()
                        {
                            idSolicitud = (int)reader["idSolicitud"],
                            Id_voBoSol = (int)reader["Id_voBoSol"],
                            folio = (string)reader["folio"],
                            correo1 = (string)reader["correo1"],
                            correo2 = (string)reader["correo2"],
                            correo3 = (string)reader["correo3"],
                            correo4 = (string)reader["correo4"],
                            correo5 = (string)reader["correo5"],
                            fec_movto = (DateTime)reader["fec_movto"],


                        });
                    }

                    con.Close();

                    return list;
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
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
            }
        }

        //ValidaSolicitudVoboPlantilla_sUp

        public string ValidaSolicitudVoboPlantilla_sUp(int SolicitudId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                var mensaje = string.Empty;

                comm = new SqlCommand("ValidaSolicitudVoboPlantilla_sUp", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@SolicitudId", SolicitudId) { DbType = System.Data.DbType.Int32 });

                reader = null;


                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {

                        mensaje = (string)reader["Message"];

                    }

                    con.Close();

                    return mensaje;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
            }
        }
        #endregion


        #region Solicitudes Temporal


        public int tbl_SolicitudesTemp_iUp(int idUsuario, int idTipoSolicitud, int idPlantilla)
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                SqlCommand cmd;

                cmd = new SqlCommand("tbl_SolicitudesTemp_iUp", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@idTipoSolicitud", idTipoSolicitud) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@idPlantilla", idPlantilla) { DbType = System.Data.DbType.Int32 });


                try
                {
                    conn.Open();

                    int verificator = 0;
                    verificator = Convert.ToInt32(cmd.ExecuteScalar());

                    conn.Close();

                    return verificator;
                }
                catch (Exception ex)
                {
                    return 0;
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
            }
        }


        public int tbl_SolicitudesTemp_dUp(int idUsuario, int idTipoSolicitud, int idPlantilla)
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                SqlCommand cmd;

                cmd = new SqlCommand("tbl_SolicitudesTemp_dUp", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@idTipoSolicitud", idTipoSolicitud) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@idPlantilla", idPlantilla) { DbType = System.Data.DbType.Int32 });


                try
                {
                    conn.Open();

                    int verificator = 0;
                    verificator = Convert.ToInt32(cmd.ExecuteScalar());

                    conn.Close();

                    return verificator;
                }
                catch (Exception ex)
                {
                    return 0;
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
            }
        }

        public int tbl_EtiquetasTemp_iUp(int idUsuario, int idTipoSolicitud, int id_etiquetas, int idPlantilla, int id_PlantillaJuridica, string respuesta)
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                SqlCommand cmd;

                cmd = new SqlCommand("tbl_EtiquetasTemp_iUp", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@idTipoSolicitud", idTipoSolicitud) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@idPlantilla", idPlantilla) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@id_etiquetas", id_etiquetas) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@id_PlantillaJuridica", id_PlantillaJuridica) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@respuesta", respuesta) { DbType = System.Data.DbType.String });


                try
                {
                    conn.Open();

                    int verificator = 0;
                    verificator = Convert.ToInt32(cmd.ExecuteScalar());

                    conn.Close();

                    return verificator;
                }
                catch (Exception ex)
                {
                    return 0;
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
            }
        }


        public int tbl_EtiquetasTemp_uUp(int idUsuario, int idTipoSolicitud, int id_etiquetas, int idPlantilla, int id_PlantillaJuridica, string respuesta)
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                SqlCommand cmd;

                cmd = new SqlCommand("tbl_EtiquetasTemp_uUp", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@idTipoSolicitud", idTipoSolicitud) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@idPlantilla", idPlantilla) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@id_etiquetas", id_etiquetas) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@id_PlantillaJuridica", id_PlantillaJuridica) { DbType = System.Data.DbType.Int32 });
                cmd.Parameters.Add(new SqlParameter("@respuesta", respuesta) { DbType = System.Data.DbType.String });


                try
                {
                    conn.Open();

                    int verificator = 0;
                    verificator = Convert.ToInt32(cmd.ExecuteScalar());

                    conn.Close();

                    return verificator;
                }
                catch (Exception ex)
                {
                    return 0;
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
            }
        }

        public string tbl_EtiquetasTemp_sUp(int idUsuario, int idTipoSolicitud, int id_etiquetas, int idPlantilla, int id_PlantillaJuridica)
        {
            string verificador = string.Empty;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<PlantillasVoBo> list = new List<PlantillasVoBo>();

                comm = new SqlCommand("tbl_EtiquetasTemp_sUp", con);

                comm.Parameters.Add(new SqlParameter("@idUsuario", idUsuario) { DbType = System.Data.DbType.Int32 });
                comm.Parameters.Add(new SqlParameter("@idTipoSolicitud", idTipoSolicitud) { DbType = System.Data.DbType.Int32 });
                comm.Parameters.Add(new SqlParameter("@idPlantilla", idPlantilla) { DbType = System.Data.DbType.Int32 });
                comm.Parameters.Add(new SqlParameter("@id_etiquetas", id_etiquetas) { DbType = System.Data.DbType.Int32 });
                comm.Parameters.Add(new SqlParameter("@id_PlantillaJuridica", id_PlantillaJuridica) { DbType = System.Data.DbType.Int32 });
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;


                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        verificador = (string)reader["respuesta"];
                    }

                    con.Close();

                    return verificador;
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
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
            }
        }

        public int InsArchivoSolicitudTemp(ArchivoSolicitud archivo, int idUsuario, int idTipoSolicitud, int idPlantilla)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;


                comm = new SqlCommand("InsArchivoSolicitudTemp", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter("idTipoDocumento", archivo.IdTipoDocumento) { DbType = System.Data.DbType.Int32},
                    new SqlParameter("Archivo", archivo.Archivo) { SqlDbType = System.Data.SqlDbType.VarBinary },
                    new SqlParameter("Nombre", archivo.Nombre),
                    new SqlParameter("idUsuario", idUsuario) { DbType = System.Data.DbType.Int32},
                    new SqlParameter("idTipoSolicitud", idTipoSolicitud) { DbType = System.Data.DbType.Int32} ,
                    new SqlParameter("idPlantilla", idPlantilla) { DbType = System.Data.DbType.Int32},

                });




                try
                {
                    con.Open();

                    int verificator = 0;
                    verificator = Convert.ToInt32(comm.ExecuteScalar());

                    con.Close();

                    return verificator;
                }
                catch (Exception ex)
                {
                    return 0;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }
            }
        }

        public int DelArchivoSolicitudTemp(ArchivoSolicitud archivo, int idUsuario, int idTipoSolicitud, int idPlantilla)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;


                comm = new SqlCommand("DelArchivoSolicitudTemp", con);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter("idTipoDocumento", archivo.IdTipoDocumento) { DbType = System.Data.DbType.Int32},                
                    new SqlParameter("Nombre", archivo.Nombre),
                    new SqlParameter("idUsuario", idUsuario) { DbType = System.Data.DbType.Int32},
                    new SqlParameter("idTipoSolicitud", idTipoSolicitud) { DbType = System.Data.DbType.Int32} ,
                    new SqlParameter("idPlantilla", idPlantilla) { DbType = System.Data.DbType.Int32},

                });




                try
                {
                    con.Open();

                    int verificator = 0;
                    verificator = Convert.ToInt32(comm.ExecuteScalar());

                    con.Close();

                    return verificator;
                }
                catch (Exception ex)
                {
                    return 0;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    if (comm != null)
                    {
                        comm.Dispose();
                    }
                }
            }
        }



        public List<ArchivoSolicitud> SelArchivoSolicitudTemp(int idUsuario, int idTipoSolicitud, int idPlantilla)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand comm;
                SqlDataReader reader;
                List<ArchivoSolicitud> list = new List<ArchivoSolicitud>();

                comm = new SqlCommand("SelArchivoSolicitudTemp", con);
                comm.Parameters.AddRange(new SqlParameter[] {               
                    new SqlParameter("idUsuario", idUsuario) { DbType = System.Data.DbType.Int32},
                    new SqlParameter("idTipoSolicitud", idTipoSolicitud) { DbType = System.Data.DbType.Int32} ,
                    new SqlParameter("idPlantilla", idPlantilla) { DbType = System.Data.DbType.Int32},

                });


                comm.CommandType = System.Data.CommandType.StoredProcedure;

                reader = null;


                try
                {
                    con.Open();
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new ArchivoSolicitud()
                        {
                            Nombre = (string)reader["Nombre"],
                            IdTipoDocumento = (int)reader["IdTipoDocumento"],
                            TipoDocumento = (string)reader["TipoDocumento"],
                            Archivo = (byte[])reader["Archivo"],
                            EsNuevo = false

                        });
                    }

                    con.Close();

                    return list;
                }
                catch(Exception ex)
                {
                    return null;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    if (comm != null)
                    {
                        comm.Dispose();
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
