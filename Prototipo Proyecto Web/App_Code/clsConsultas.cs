using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace Prototipo_Proyecto_Web.App_Code
{
    public class clsConsultas
    {
        private clsConexion clsCon;
        private SqlCommand cmd;
        private SqlDataReader reader;
        private SqlDataAdapter adapter;
        
        public DataTable GetMonitor()
        {
            DataTable dt = new DataTable();
            clsCon = new clsConexion();
            var guid = clsLog.EscribeLogInOut("Entro a consultar los datos del monitor", clsUtils.GetCurrentMethodName(), clsLog.tInOut.IN);

            try
            {
                if(clsCon.Conectar() )
                {
                    cmd = new SqlCommand();
                    cmd.Connection = clsCon.GetConnection();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_vermonitor";
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }

                clsLog.EscribeLogInOut("Finalizo consulta de datos del monitor exitosamente", clsUtils.GetCurrentMethodName(), clsLog.tInOut.OUT, guid);
            }
            catch (Exception ex)
            {
                dt = null;  
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
                clsLog.EscribeLogInOut("Finalizo consulta de datos del monitor con error", clsUtils.GetCurrentMethodName(), clsLog.tInOut.OUT, guid);
            }
            finally
            {
                clsCon.Desconectar();
            }

            return dt;
        }

        public DataTable GetConfigRPT()
        {
            DataTable dt = new DataTable();
            clsCon = new clsConexion();
            var guid = clsLog.EscribeLogInOut("Entro a consultar los datos del configurador", clsUtils.GetCurrentMethodName(), clsLog.tInOut.IN);

            try
            {
                if (clsCon.Conectar())
                {
                    cmd = new SqlCommand();
                    cmd.Connection = clsCon.GetConnection();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_verconfiguracionrpt";
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }

                clsLog.EscribeLogInOut("Finalizo consulta de datos del configurador exitosamente", clsUtils.GetCurrentMethodName(), clsLog.tInOut.OUT, guid);
            }
            catch (Exception ex)
            {
                dt = null;
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
                clsLog.EscribeLogInOut("Finalizo consulta de datos del configurador con error", clsUtils.GetCurrentMethodName(), clsLog.tInOut.OUT, guid);
            }
            finally
            {
                clsCon.Desconectar();
            }

            return dt;
        }

        public DataTable GetUsuarios()
        {
            DataTable dt = new DataTable();
            clsCon = new clsConexion();
            var guid = clsLog.EscribeLogInOut("Entro a consultar los datos de usuario", clsUtils.GetCurrentMethodName(), clsLog.tInOut.IN);

            try
            {
                if (clsCon.Conectar())
                {
                    cmd = new SqlCommand();
                    cmd.Connection = clsCon.GetConnection();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_verusuarios";
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }

                clsLog.EscribeLogInOut("Finalizo consulta de datos de usuario exitosamente", clsUtils.GetCurrentMethodName(), clsLog.tInOut.OUT, guid);
            }
            catch (Exception ex)
            {
                dt = null;
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
                clsLog.EscribeLogInOut("Finalizo consulta de datos de usuario con error", clsUtils.GetCurrentMethodName(), clsLog.tInOut.OUT, guid);
            }
            finally
            {
                clsCon.Desconectar();
            }

            return dt;
        }

        public DataTable GetAreasRes()
        {
            DataTable dt = new DataTable();
            clsCon = new clsConexion();
            var guid = clsLog.EscribeLogInOut("Entro a consultar los datos de areas responsables", clsUtils.GetCurrentMethodName(), clsLog.tInOut.IN);

            try
            {
                if (clsCon.Conectar())
                {
                    cmd = new SqlCommand();
                    cmd.Connection = clsCon.GetConnection();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_verareas_res";
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }

                clsLog.EscribeLogInOut("Finalizo consulta de datos de areas responsables exitosamente", clsUtils.GetCurrentMethodName(), clsLog.tInOut.OUT, guid);
            }
            catch (Exception ex)
            {
                dt = null;
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
                clsLog.EscribeLogInOut("Finalizo consulta de datos de areas responsables con error", clsUtils.GetCurrentMethodName(), clsLog.tInOut.OUT, guid);
            }
            finally
            {
                clsCon.Desconectar();
            }

            return dt;
        }

        public DataTable GetAreasSoli()
        {
            DataTable dt = new DataTable();
            clsCon = new clsConexion();
            var guid = clsLog.EscribeLogInOut("Entro a consultar los datos de areas solicitantes", clsUtils.GetCurrentMethodName(), clsLog.tInOut.IN);

            try
            {
                if (clsCon.Conectar())
                {
                    cmd = new SqlCommand();
                    cmd.Connection = clsCon.GetConnection();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_verareas_soli";
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }

                clsLog.EscribeLogInOut("Finalizo consulta de datos de areas solicitantes exitosamente", clsUtils.GetCurrentMethodName(), clsLog.tInOut.OUT, guid);
            }
            catch (Exception ex)
            {
                dt = null;
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
                clsLog.EscribeLogInOut("Finalizo consulta de datos de areas solicitantes con error", clsUtils.GetCurrentMethodName(), clsLog.tInOut.OUT, guid);
            }
            finally
            {
                clsCon.Desconectar();
            }

            return dt;
        }

        public int InsUpConfRPT(string jsonRegistros)
        {
            int res = 0;
            clsCon = new clsConexion();
            var guid = clsLog.EscribeLogInOut("Insertando o actualizando configuración de reportes", clsUtils.GetCurrentMethodName(), clsLog.tInOut.IN);
            try
            {
                if (clsCon.Conectar())
                {
                    cmd = new SqlCommand();
                    cmd.Connection = clsCon.GetConnection();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_insoact_configuracion";
                    cmd.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@JSONREGISTROS",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = jsonRegistros,
                        Size = 32000,
                        Direction = ParameterDirection.Input
                    });

                    cmd.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@ID_CNF",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output,
                        Size = 8
                    });

                    cmd.ExecuteNonQuery();

                    res = 0;
                }

                clsLog.EscribeLogInOut("Finalicé correctamente la insercíón o actualización la información en la configuración de reportes", clsUtils.GetCurrentMethodName(), clsLog.tInOut.OUT, guid);
            }
            catch(Exception ex)
            {
                res = -1;
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
                clsLog.EscribeLogInOut("No se inserto o actualizo la información en la configuración de reportes", clsUtils.GetCurrentMethodName(), clsLog.tInOut.OUT, guid);
            }
            return res;
        }

        public int InsEvidencia(int idejec, int idcnf, string b64img)
        {
            int res = 0;
            clsCon = new clsConexion();
            var guid = clsLog.EscribeLogInOut("Insertando evidencia de cumplimiento de ejecución", clsUtils.GetCurrentMethodName(), clsLog.tInOut.IN);
            try
            {
                if (clsCon.Conectar())
                {
                    cmd = new SqlCommand();
                    cmd.Connection = clsCon.GetConnection();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_carga_evidencia";
                    cmd.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@idconf",
                        SqlDbType = SqlDbType.Int,
                        Value = idcnf,
                        Direction = ParameterDirection.Input,
                        Size = 8
                    });

                    cmd.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@idejec",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input,
                        Size = 8,
                        Value = idejec
                    });

                    cmd.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@img",
                        SqlDbType = SqlDbType.NVarChar,
                        Size = int.MaxValue,
                        Value = b64img,
                        Direction = ParameterDirection.Input
                    });

                    cmd.ExecuteNonQuery();

                    res = 0;
                }

                clsLog.EscribeLogInOut("Finalicé correctamente la insercíón de evidencia de ejecución", clsUtils.GetCurrentMethodName(), clsLog.tInOut.OUT, guid);
            }
            catch (Exception ex)
            {
                res = -1;
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
                clsLog.EscribeLogInOut("No se inserto la evidencia de ejecución", clsUtils.GetCurrentMethodName(), clsLog.tInOut.OUT, guid);
            }
            return res;
        }

        public string VerEvidencia(int idejec, int idcnf)
        {
            string res = string.Empty;
            clsCon = new clsConexion();
            var guid = clsLog.EscribeLogInOut("Verificando evidencia de cumplimiento de ejecución", clsUtils.GetCurrentMethodName(), clsLog.tInOut.IN);
            try
            {
                if (clsCon.Conectar())
                {
                    cmd = new SqlCommand();
                    cmd.Connection = clsCon.GetConnection();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_ver_evidencia";
                    cmd.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@idconf",
                        SqlDbType = SqlDbType.Int,
                        Value = idcnf,
                        Direction = ParameterDirection.Input,
                        Size = 8
                    });

                    cmd.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@idejec",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input,
                        Size = 8,
                        Value = idejec
                    });


                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            res = reader.GetString(0);
                        }
                    }
                    else
                    {
                        res = string.Empty;
                    }

                    
                }

                clsLog.EscribeLogInOut("Finalicé correctamente la verificación de evidencia de ejecución", clsUtils.GetCurrentMethodName(), clsLog.tInOut.OUT, guid);
            }
            catch (Exception ex)
            {
                res = string.Empty;
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
                clsLog.EscribeLogInOut("No se pudo verificar la evidencia de ejecución", clsUtils.GetCurrentMethodName(), clsLog.tInOut.OUT, guid);
            }
            return res;
        }
    }
}