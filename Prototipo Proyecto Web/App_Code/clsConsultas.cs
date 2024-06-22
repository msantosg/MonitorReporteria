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

                    res = Convert.ToInt32(cmd.Parameters["@ID_CNF"].Value.ToString());
                }

                clsLog.EscribeLogInOut("Finalicé correctamente la insercíón o actualización la información en la configuración de reportes", clsUtils.GetCurrentMethodName(), clsLog.tInOut.OUT, guid);
            }
            catch(Exception ex)
            {
                res = 0;
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
                clsLog.EscribeLogInOut("No se inserto o actualizo la información en la configuración de reportes", clsUtils.GetCurrentMethodName(), clsLog.tInOut.OUT, guid);
            }
            return res;
        }
    }
}