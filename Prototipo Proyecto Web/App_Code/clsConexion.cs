using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Prototipo_Proyecto_Web.App_Code
{
    public class clsConexion
    {
        private SqlConnection conn;
        public SqlConnection GetConnection() { return conn; }

        public bool Conectar()
        {
            bool res = false;
            try
            {
                if (conn == null)
                    conn = new SqlConnection();
                if(conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.ConnectionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}",
                    clsUtils.TraeKeyConfig("ConexionBD.Host"), clsUtils.TraeKeyConfig("ConexionBD.DataBase"),
                    clsUtils.TraeKeyConfig("ConexionBD.User"), clsUtils.TraeKeyConfig("ConexionBD.Pass"));
                    conn.Open();
                }

                res = true;
            }
            catch (Exception ex)
            {
                res = false;
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
            }
            return res;
        }

        public void Desconectar()
        {
            try
            {
                if (conn != null)
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();
                }
            }
            catch(Exception ex)
            {
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
            }
        }
    }
}