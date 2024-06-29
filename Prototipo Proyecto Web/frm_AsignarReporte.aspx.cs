using Newtonsoft.Json;
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
        private Regex regDescripRPT = new Regex(@"^[a-zA-Z\s0-9íÍáÁéÉóÓúÚü\(\)]*$");
        private Regex regCorreo = new Regex(@"^(([^<>()\[\]\\.,;:\s@”]+(\.[^<>()\[\]\\.,;:\s@”]+)*)|(“.+”))@((\[[0–9]{1,3}\.[0–9]{1,3}\.[0–9]{1,3}\.[0–9]{1,3}])|(([a-zA-Z\-0–9]+\.)+[a-zA-Z]{2,}))$");
        private Regex regAnticipa = new Regex("^[0-9]*$");
        private Regex regSancion = new Regex(@"^[0-9]+([.][0-9]{2})?$");
        private Regex regFePublica = new Regex("^([0-2][0-9]|3[0-1])(\\/|-)(0[1-9]|1[0-2])\\2(\\d{4})$");

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {               
                Cargar_ConfRpt();
                carga_usuarios();
                carga_areasres();
                carga_areassoli();
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

        private void carga_usuarios()
        {
            try
            {
                DataTable dt = new DataTable();
                clsCon = new clsConsultas();

                dt = clsCon.GetUsuarios();

                if (dt != null && dt.Rows.Count > 0)
                {
                    dropUsuario.DataSource = dt;
                    dropUsuario.DataTextField = "valor";
                    dropUsuario.DataValueField = "llave";
                    dropUsuario.DataBind();
                }
                else
                {
                    dropUsuario.DataSource = null;
                    dropUsuario.DataBind();
                }
            }
            catch (Exception ex)
            {
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
            }
        }

        private void carga_areasres()
        {
            try
            {
                DataTable dt = new DataTable();
                clsCon = new clsConsultas();

                dt = clsCon.GetAreasRes();

                if (dt != null && dt.Rows.Count > 0)
                {
                    dropAreasR.DataSource = dt;
                    dropAreasR.DataTextField = "valor";
                    dropAreasR.DataValueField = "llave";
                    dropAreasR.DataBind();
                }
                else
                {
                    dropAreasR.DataSource = null;
                    dropAreasR.DataBind();
                }
            }
            catch (Exception ex)
            {
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
            }
        }

        private void carga_areassoli()
        {
            try
            {
                DataTable dt = new DataTable();
                clsCon = new clsConsultas();

                dt = clsCon.GetAreasSoli();

                if (dt != null && dt.Rows.Count > 0)
                {
                    dropAreasSol.DataSource = dt;
                    dropAreasSol.DataTextField = "valor";
                    dropAreasSol.DataValueField = "llave";
                    dropAreasSol.DataBind();
                }
                else
                {
                    dropAreasSol.DataSource = null;
                    dropAreasSol.DataBind();
                }
            }
            catch (Exception ex)
            {
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
            }
        }

        protected void btnNConfig_Click(object sender, EventArgs e)
        {
            dvNConfig.Visible = true;
            lst_errores.Visible = false;
            errores.InnerHtml = string.Empty;
            Session["InsOUpd"] = 1;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "GeneraPicker2", clsUtils.DatePickerUI(), false);
        }

        protected void gvConfigMonitor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[10].Text.Equals("PENDIENTE"))
                        e.Row.Cells[10].ForeColor = System.Drawing.Color.Orange;
                    else if (e.Row.Cells[10].Text.Equals("FINALIZADO"))
                        e.Row.Cells[10].ForeColor = System.Drawing.Color.DarkGreen;
                    else
                        e.Row.Cells[10].ForeColor = System.Drawing.Color.Red;
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
            txtSancion.Text = string.Empty;
            txtFePublica.Text = string.Empty;
            dropAreasR.SelectedValue = "-1";
            dropAreasSol.SelectedValue = "-1";
            dropPeriodo.SelectedValue = "---";
            dropUsuario.SelectedValue = "...";
            dvNConfig.Visible = false;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "GeneraPreguntaDelete", "MostrarPregunta('¿Desea guardar la información ingresada?');", true);
            }
            catch(Exception ex)
            {
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
            }
        }

        protected void gvConfigMonitor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if(e.CommandName == "Select")
                {
                    Session["InsOUpd"] = 2;
                    dvNConfig.Visible = true;
                    errores.InnerHtml = string.Empty;
                    lst_errores.Visible = false;
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gvConfigMonitor.Rows[index];
                    txtIdConf.Text = gvConfigMonitor.DataKeys[row.RowIndex].Values["ID_CONFIGURACION"].ToString();
                    dropAreasR.SelectedValue = gvConfigMonitor.DataKeys[row.RowIndex].Values["AREA_RESPONSABLE"].ToString();
                    dropAreasSol.SelectedValue = gvConfigMonitor.DataKeys[row.RowIndex].Values["AREA_SOLICITANTE"].ToString();
                    txtDescReport.Text = gvConfigMonitor.DataKeys[row.RowIndex].Values["DESCRIPCION"].ToString();
                    txtCorreo.Text = gvConfigMonitor.DataKeys[row.RowIndex].Values["CORREO"].ToString();
                    dropUsuario.SelectedValue = gvConfigMonitor.DataKeys[row.RowIndex].Values["USUARIO"].ToString();
                    dropPeriodo.SelectedValue = gvConfigMonitor.DataKeys[row.RowIndex].Values["PERIODICIDAD"].ToString();
                    txtAnticipacion.Text = gvConfigMonitor.DataKeys[row.RowIndex].Values["ANTICIPACION"].ToString();
                    txtFePublica.Text = gvConfigMonitor.DataKeys[row.RowIndex].Values["FECHA_PUBLICACION"].ToString();
                    txtSancion.Text = gvConfigMonitor.DataKeys[row.RowIndex].Values["SANCION"].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "GeneraPicker2", clsUtils.DatePickerUI(), false);
                }
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msgAlert", "MostrarMensaje('Ocurrió un error al momento de mostrar el registro seleccionado, por favor comuniquese con sistemas.', ErrorMessage_Enum.Error);", true);
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
            }
        }

        protected void btnOculto_Click(object sender, EventArgs e)
        {
            List<string> lst_err = new List<string>();
            lst_errores.Visible = false;
            bool continua = true;
            clsCon = new clsConsultas();

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

                #region validaciones fecha publicación
                if (string.IsNullOrEmpty(txtFePublica.Text))
                {
                    lst_err.Add("Campo Fecha publicación es requerido.");
                    continua = false;
                }
                else
                {
                    if (!regFePublica.IsMatch(txtFePublica.Text))
                    {
                        lst_err.Add("Campo Fecha publicación contiene caracteres inválidos.");
                        continua = false;
                    }
                }
                #endregion

                #region validaciones sanción
                if (string.IsNullOrEmpty(txtSancion.Text))
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
                    foreach (string err in lst_err)
                    {
                        strErr.Append("<li>" + err + "</li>");
                    }
                    strErr.Append("</ul>");

                    lst_errores.Visible = true;
                    errores.InnerHtml = strErr.ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msgAlert", "MostrarMensaje('Existen errores en los campos que debe validar, por favor verifique la sección de errores.', ErrorMessage_Enum.Error);", true);

                    return;
                }

                string json = JsonConvert.SerializeObject(new
                {
                    id_conf = (Session["InsOUpd"].ToString().Equals("1") ? 0 : Convert.ToInt32(txtIdConf.Text)),
                    usuario = dropUsuario.SelectedValue,
                    area_responsable = dropAreasR.SelectedValue,
                    area_solicitante = dropAreasSol.SelectedValue,
                    correo = txtCorreo.Text,
                    descripcion = txtDescReport.Text.ToUpper(),
                    periodicidad = dropPeriodo.SelectedValue,
                    anticipacion = txtAnticipacion.Text,
                    diapub = DateTime.ParseExact(txtFePublica.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"),
                    sancion = txtSancion.Text,
                    estado = 0,
                    usuariomodifica = string.Empty,
                    tipotransaccion = Convert.ToInt32(Session["InsOUpd"].ToString())
                });

                int res = clsCon.InsUpConfRPT(json);

                if (res == 0)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msgAlert", "MostrarMensaje('Registro guardado correctamente.', ErrorMessage_Enum.Success);", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msgAlert", "MostrarMensaje('Ocurrió un error al momento de guardar el registro, por favor comuniquese con sistemas.', ErrorMessage_Enum.Error);", true);
                              

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msgAlert", "MostrarMensaje('Ocurrió un error al momento de guardar el registro, por favor comuniquese con sistemas.', ErrorMessage_Enum.Error);", true);
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
            }

            txtDescReport.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtAnticipacion.Text = string.Empty;
            txtSancion.Text = string.Empty;
            txtFePublica.Text = string.Empty;
            dropAreasR.SelectedValue = "-1";
            dropAreasSol.SelectedValue = "-1";
            dropPeriodo.SelectedValue = "---";
            dropUsuario.SelectedValue = "...";
            dvNConfig.Visible = false;
            Cargar_ConfRpt();
        }
    }
}