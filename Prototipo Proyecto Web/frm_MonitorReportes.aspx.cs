using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Prototipo_Proyecto_Web.App_Code;

namespace Prototipo_Proyecto_Web
{
    public partial class frm_MonitorReportes : System.Web.UI.Page
    {
        private clsConsultas clsCon;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargar_monitor();
            }
        }

        private void cargar_monitor()
        {
            DataTable dt = new DataTable();
            clsCon = new clsConsultas();
            try
            {

                dt = clsCon.GetMonitor();
                if (dt != null && dt.Rows.Count > 0)
                {
                    Session["MonitorDt"] = dt;
                    var pendientes = dt.AsEnumerable().Where(x => x["ESTADO"].Equals("PENDIENTE")).ToList().Count;
                    var vencidos = dt.AsEnumerable().Where(x => x["ESTADO"].Equals("VENCIDO")).ToList().Count;
                    var completado = dt.AsEnumerable().Where(x => x["ESTADO"].Equals("FINALIZADO")).ToList().Count;
                    var total = dt.Rows.Count;

                    lblPendientes.Text = pendientes.ToString();
                    lblVencidos.Text = vencidos.ToString();
                    lblCompletados.Text = completado.ToString();
                    lblTotal.Text = total.ToString();
                    gvEjecRpt.DataSource = dt;
                    gvEjecRpt.DataBind();
                }
                else
                {
                    gvEjecRpt.DataSource = null;
                    gvEjecRpt.DataBind();
                    lblPendientes.Text = "0";
                    lblVencidos.Text = "0";
                    lblCompletados.Text = "0";
                    lblTotal.Text = "0";
                }
            }
            catch(Exception ex)
            {
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
            }
            
        }

        protected void gvEjecRpt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[6].Text.Equals("PENDIENTE"))
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.Orange;
                    else if (e.Row.Cells[6].Text.Equals("FINALIZADO"))
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.DarkGreen;
                    else
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex) 
            {
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
            }
        }

        protected void gvEjecRpt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvEjecRpt.PageIndex = e.NewPageIndex;
                gvEjecRpt.DataSource = (DataTable)Session["MonitorDt"];
                gvEjecRpt.DataBind();
            }
            catch(Exception ex)
            {
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
            }
        }
    }
}