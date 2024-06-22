using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Prototipo_Proyecto_Web.App_Code
{
    public class clsUtils
    {
        public enum TipoAlerta
        {
            Error = 1,
            Informacion = 2,
            Satisfactorio = 3,
            Alerta = 4
        }

        public static string TraeKeyConfig(string key)
        {
            string conf = string.Empty;
            try
            {
                string mappingpath = System.Web.HttpContext.Current.Server.MapPath("~/Config/VariablesConf.json");
                JObject config = JObject.Parse(File.ReadAllText(mappingpath));
                JToken confs = config.SelectToken(key);
                conf = confs.Value<string>();
            }
            catch
            {
                conf = string.Empty;
            }

            return conf;
        }

        public static string GetCurrentMethodName()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);

            return stackFrame.GetMethod().Name;
        }

        public static string HTML_Alert(TipoAlerta pAlerta, string pHeader, string pMessage)
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                switch (pAlerta)
                {
                    case TipoAlerta.Error:
                        stringBuilder.Append("<div class=\"alert alert-danger alert-dismissible fade show\" role=\"alert\">");
                        stringBuilder.Append("<h2 class=\"alert-heading text-center\"><strong><i class=\"fa-solid fa-circle-xmark\"></i></strong></h2>");
                        stringBuilder.Append("<h4 class=\"alert-heading text-center\"><strong>" + pHeader + "</strong></h4>");
                        break;
                    case TipoAlerta.Informacion:
                        stringBuilder.Append("<div class=\"alert alert-info alert-dismissible fade show\" role=\"alert\">");
                        stringBuilder.Append("<h2 class=\"alert-heading text-center\"><strong><i class=\"fa-solid fa-circle-info\"></i></strong></h2>");
                        stringBuilder.Append("<h4 class=\"alert-heading text-center\"><strong>" + pHeader + "</strong></h4>");
                        break;
                    case TipoAlerta.Satisfactorio:
                        stringBuilder.Append("<div class=\"alert alert-success alert-dismissible fade show\" role=\"alert\">");
                        stringBuilder.Append("<h2 class=\"alert-heading text-center\"><strong><i class=\"fa-solid fa-circle-check\"></i></strong></h2>");
                        stringBuilder.Append("<h4 class=\"alert-heading text-center\"><strong>" + pHeader + "</strong></h4>");
                        break;
                    case TipoAlerta.Alerta:
                        stringBuilder.Append("<div class=\"alert alert-warning alert-dismissible fade show\" role=\"alert\">");
                        stringBuilder.Append("<h2 class=\"alert-heading text-center\"><strong><i class=\"fa-solid fa-triangle-exclamation\"></i></strong></h2>");
                        stringBuilder.Append("<h4 class=\"alert-heading text-center\"><strong>" + pHeader + "</strong></h4>");
                        break;
                }
                stringBuilder.Append("<p class=\"text-center\">" + pMessage + "</p>");
                stringBuilder.Append("<button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"Close\"></button>");
                stringBuilder.Append("</div>");

                
            }
            catch(Exception ex)
            {
                clsLog.EscribeLogErr(ex, GetCurrentMethodName());
            }
            return stringBuilder.ToString();
        }
    }
}