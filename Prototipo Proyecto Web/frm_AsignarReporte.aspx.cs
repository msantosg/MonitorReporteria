using Prototipo_Proyecto_Web.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Prototipo_Proyecto_Web
{
    public partial class frm_AsignarReporte : System.Web.UI.Page
    {
        private clsConsultas clsCon;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Cargar_ConfRpt();
            }
        }

        private void Cargar_ConfRpt()
        {
            try
            {
                DataTable dt = new DataTable();
                clsCon = new clsConsultas();

                dt = clsCon.GetConfigRPT();

                if (dt != null && dt.Rows.Count > 0)
                {
                    gvConfigMonitor.DataSource = dt;
                    gvConfigMonitor.DataBind();
                }
                else
                {
                    gvConfigMonitor.DataSource = null;
                    gvConfigMonitor.DataBind();
                }
            }
            catch(Exception ex)
            {
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
            }
        }


        protected void btnNConfig_Click(object sender, EventArgs e)
        {
            dvNConfig.Visible = true;
        }

        protected void gvConfigMonitor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[8].Text.Equals("PENDIENTE"))
                        e.Row.Cells[8].ForeColor = System.Drawing.Color.Orange;
                    else if (e.Row.Cells[8].Text.Equals("FINALIZADO"))
                        e.Row.Cells[8].ForeColor = System.Drawing.Color.DarkGreen;
                    else
                        e.Row.Cells[8].ForeColor = System.Drawing.Color.Red;
                }
            }
            catch(Exception ex)
            {
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtDescReport.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtAnticipacion.Text = string.Empty;
            dropAreasR.SelectedIndex = 0;
            dropAreasSol.SelectedIndex = 0;
            dropPeriodo.SelectedIndex = 0;
            dropUsuario.SelectedIndex = 0;
            dvNConfig.Visible = false;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            
        }
    }
}