using Prototipo_Proyecto_Web.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Prototipo_Proyecto_Web
{
    public partial class frm_AsignarReporte : System.Web.UI.Page
    {
        private clsConsultas clsCon;
        private Regex regDescripRPT = new Regex(@"^[a-zA-Z\s0-9]*$");
        private Regex regCorreo = new Regex(@"^(([^<>()\[\]\\.,;:\s@”]+(\.[^<>()\[\]\\.,;:\s@”]+)*)|(“.+”))@((\[[0–9]{1,3}\.[0–9]{1,3}\.[0–9]{1,3}\.[0–9]{1,3}])|(([a-zA-Z\-0–9]+\.)+[a-zA-Z]{2,}))$");
        private Regex regAnticipa = new Regex("^[0-9]*$");
        private Regex regSancion = new Regex(@"^[0-9]+([.][0-9]{2})?$");

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
            List<string> lst_err = new List<string>();
            dvAlerta.Visible = false;
            lst_errores.Visible = false;
            bool continua = true;
            try
            {
                #region validaciones descripción
                if (string.IsNullOrEmpty(txtDescReport.Text))
                {
                    lst_err.Add("Campo Descripción Reporte es requerido.");
                    continua = false;
                }
                else
                {
                    if (!regDescripRPT.IsMatch(txtDescReport.Text))
                    {
                        lst_err.Add("Campo Descripción Reporte contiene caracteres inválidos.");
                        continua = false;
                    }
                }
                #endregion

                #region validaciones correo electrónico
                if (string.IsNullOrEmpty(txtCorreo.Text))
                {
                    lst_err.Add("Campo Correo Electrónico es requerido.");
                    continua = false;
                }
                else
                {
                    if (!regCorreo.IsMatch(txtCorreo.Text))
                    {
                        lst_err.Add("Campo Correo Electrónico contiene caracteres inválidos.");
                        continua = false;
                    }
                }
                #endregion

                #region validaciones anticipación
                if (string.IsNullOrEmpty(txtAnticipacion.Text))
                {
                    lst_err.Add("Campo Anticipación es requerido.");
                    continua = false;
                }
                else
                {
                    if (!regAnticipa.IsMatch(txtAnticipacion.Text))
                    {
                        lst_err.Add("Campo Anticipación contiene caracteres inválidos.");
                        continua = false;
                    }
                }
                #endregion

                #region validaciones sanción
                if(string.IsNullOrEmpty(txtSancion.Text))
                {
                    lst_err.Add("Campo Sanción es requerido.");
                    continua = false;
                }
                else
                {
                    if (!regSancion.IsMatch(txtSancion.Text))
                    {
                        lst_err.Add("Campo Sanción contiene caracteres inválidos.");
                        continua = false;
                    }
                }
                #endregion

                #region validaciones campos desplegables
                if (dropAreasR.SelectedIndex == 0)
                {
                    lst_err.Add("Campo Áreas Responsables no seleccionado.");
                    continua = false;
                }

                if (dropAreasSol.SelectedIndex == 0)
                {
                    lst_err.Add("Campo Áreas Solicitantes no seleccionado.");
                    continua = false;
                }

                if (dropPeriodo.SelectedIndex == 0)
                {
                    lst_err.Add("Campo Periodicidad no seleccionado.");
                    continua = false;
                }

                if (dropUsuario.SelectedIndex == 0)
                {
                    lst_err.Add("Campo Usuario no seleccionado.");
                    continua = false;
                }
                #endregion

                if (continua == false)
                {
                    StringBuilder strErr = new StringBuilder();
                    strErr.Append("<ul>");
                    foreach(string err in lst_err)
                    {
                        strErr.Append("<li>" + err + "</li>");
                    }
                    strErr.Append("</ul>");

                    lst_errores.Visible = true;
                    errores.InnerHtml = strErr.ToString();
                    dvAlerta.Visible = true;
                    dvAlerta.InnerHtml = clsUtils.HTML_Alert(clsUtils.TipoAlerta.Error, "Errores de validación", "Existen errores en los campos que debe validar, por favor verifique la sección de errores.");

                    return;
                }
            }
            catch(Exception ex)
            {
                dvAlerta.Visible = true;
                dvAlerta.InnerHtml = clsUtils.HTML_Alert(clsUtils.TipoAlerta.Error, "Error General", "Ocurrió un error al momento de guardar el registro, por favor comuniquese con sistemas.");
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
            }
        }
    }
}