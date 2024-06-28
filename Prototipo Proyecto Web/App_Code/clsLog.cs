using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Prototipo_Proyecto_Web.App_Code
{
    public class clsLog
    {
        public const string NewLine = "\r\n";

        public enum tInOut
        {
            IN = 1,
            OUT = 2
        }

        public static void EscribeLogErr(Exception ex, string metodo, bool alt = false)
        {
            try
            {
                string path = clsUtils.TraeKeyConfig(alt ? "Log.LogPathAlt" : "Log.LogPathErr");
                string LogFile = path + clsUtils.TraeKeyConfig("Log.LogErr") + DateTime.Now.ToString("yyyyMMdd") + ".log";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                var sw = new StreamWriter(LogFile, true)                
                sw.Write(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "|" + metodo + "|" + ex.Message + NewLine + ex.StackTrace + NewLine);
                sw.Close();
            }
            catch (Exception e)
            {
                if (alt == false)
                    EscribeLogErr(e, clsUtils.GetCurrentMethodName(), true);
            }
        }

        public static string EscribeLogInOut(string trama, string metodo, tInOut tLog, string guidLog = "")
        {
            string guidlog = string.Empty;
            try
            {
                string vArchivo = clsUtils.TraeKeyConfig("Log.LogTrazas") + DateTime.Now.ToString("yyyyMMdd") + ".log";
                string vDirectorio = clsUtils.TraeKeyConfig("Log.LogPathTrazas") + DateTime.Now.ToString("yyyyMMdd") + "/";
                guidlog = (tLog == tInOut.IN ? Guid.NewGuid().ToString() : guidLog);
                string vPath = vDirectorio + vArchivo;
                if (!Directory.Exists(vDirectorio)) Directory.CreateDirectory(vDirectorio);

                using (StreamWriter sw = new StreamWriter(vPath, true))
                {
                    string tipoTrama = tLog == tInOut.IN ? "ENTRADA" : "SALIDA";
                    sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "|" + tipoTrama + "|" + guidlog + "|" + metodo + "|" + trama);
                }
            }
            catch (Exception ex)
            {
                EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
                guidLog = string.Empty;
            }

            return guidlog;
        }
    }
}