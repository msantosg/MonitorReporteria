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
            }
            catch (Exception ex)
            {
                dt = null;  
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
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
            }
            catch (Exception ex)
            {
                dt = null;
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
            }
            finally
            {
                clsCon.Desconectar();
            }

            return dt;
        }
    }
}