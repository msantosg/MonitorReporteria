using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
                    {
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.Orange;
                        LinkButton btnCarga = e.Row.FindControl("btnCargaImg") as LinkButton;
                        btnCarga.Text = "<i class=\"fa-solid fa-upload\"></i> Subir Imagen";
                        btnCarga.Visible = true;
                        LinkButton btnVer = e.Row.FindControl("btnVerImg") as LinkButton;
                        btnVer.Visible = false;
                    }
                    else if (e.Row.Cells[6].Text.Equals("FINALIZADO"))
                    {
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.DarkGreen;
                        LinkButton btnCarga = e.Row.FindControl("btnCargaImg") as LinkButton;
                        btnCarga.Text = "<i class=\"fa-solid fa-upload\"></i> Cambiar Imagen";
                        btnCarga.Visible = true;
                        LinkButton btnVer = e.Row.FindControl("btnVerImg") as LinkButton;
                        btnVer.Visible = true;
                    }    
                    else
                    {
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
                        LinkButton btnCarga = e.Row.FindControl("btnCargaImg") as LinkButton;
                        btnCarga.Visible = false;
                        LinkButton btnVer = e.Row.FindControl("btnVerImg") as LinkButton;
                        btnVer.Visible = false;
                    }
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

        protected void gvEjecRpt_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    int index = 0;
                    index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow r = gvEjecRpt.Rows[index];
                    string id = gvEjecRpt.DataKeys[r.RowIndex].Values["ID_CONFIGURACION"].ToString();
                    string idejec = gvEjecRpt.DataKeys[r.RowIndex].Values["ID_EJECUCION"].ToString();
                    txtIDConf.Text = id;
                    txtIDEjec.Text = idejec;
                    dvRptEjec.Visible = true;
                }

                if(e.CommandName == "Detail")
                {
                    clsCon = new clsConsultas();
                    int index = 0;
                    index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow r = gvEjecRpt.Rows[index];
                    string id = gvEjecRpt.DataKeys[r.RowIndex].Values["ID_CONFIGURACION"].ToString();
                    string idejec = gvEjecRpt.DataKeys[r.RowIndex].Values["ID_EJECUCION"].ToString();
                    string b64 = clsCon.VerEvidencia(Convert.ToInt32(idejec), Convert.ToInt32(id));
                    txtIdConfDown.Text = id;
                    txtIdEjecDown.Text = idejec;
                    dvImg.InnerHtml = "<img src=\"data:image/jpg;base64," + b64 + "\" class=\"img-fluid\"/>";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modal", "levantar_modal();", true);
                }
            }
            catch(Exception ex)
            {
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
            }
        }

        protected void btnOculto_Click(object sender, EventArgs e)
        {
            try
            {
                clsCon = new clsConsultas();
                string path = clsUtils.TraeKeyConfig("ConfAplicacion.PathImg");
                string b64 = string.Empty;
                if (File.Exists(path + Session["NomImgEvidencia"].ToString()))
                {
                    b64 = clsUtils.ConvertB64File(Session["NomImgEvidencia"].ToString());
                    File.Delete(path + Session["NomImgEvidencia"].ToString());
                }


                int res = clsCon.InsEvidencia(Convert.ToInt32(txtIDEjec.Text), Convert.ToInt32(txtIDConf.Text), b64);

                if (res == 0)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msgAlert", "MostrarMensaje('Evidencia guardada correctamente.', ErrorMessage_Enum.Success);", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msgAlert", "MostrarMensaje('Ocurrió un error al momento de guardar la evidencia, por favor comuniquese con sistemas.', ErrorMessage_Enum.Error);", true);

                
            }
            catch(Exception ex)
            {
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msgAlert", "MostrarMensaje('Ocurrió un error al momento de guardar la evidencia, por favor comuniquese con sistemas.', ErrorMessage_Enum.Error);", true);
            }

            txtIDConf.Text = string.Empty;
            txtIDEjec.Text = string.Empty;
            cargar_monitor();
            dvRptEjec.Visible = false;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtIDConf.Text = string.Empty;
            dvRptEjec.Visible = false;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "GeneraPreguntaDelete", "MostrarPregunta('¿Desea guardar la evidencia ingresada?');", true);
        }

        protected void btnUpf_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Visible = false;

                if (!upf.HasFile)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msgAlert", "MostrarMensaje('Debe cargar una imagen de evidencia.', ErrorMessage_Enum.Error);", true);
                    return;
                }

                if (!clsUtils.validar_tama_img(upf.FileBytes))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msgAlert", "MostrarMensaje('La imagen cargada excede el tamaño permitido, por favor verifique.', ErrorMessage_Enum.Error);", true);
                    return;
                }

                string path = clsUtils.TraeKeyConfig("ConfAplicacion.PathImg");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (File.Exists(path + upf.FileName))
                    File.Delete(path + upf.FileName);

                upf.SaveAs(path + upf.FileName);

                Session["NomImgEvidencia"] = upf.FileName;

                lblMsg.Text = "Evidencia Cargada.";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                lblMsg.Visible = true;

            }
            catch(Exception ex)
            {
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
                lblMsg.Text = "Evidencia no fue cargada.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Visible = true;
            }
        }

        protected void btnDescargarImg_Click(object sender, EventArgs e)
        {
            try
            {
                clsCon = new clsConsultas();
                string b64 = clsCon.VerEvidencia(Convert.ToInt32(txtIdEjecDown.Text), Convert.ToInt32(txtIdConfDown.Text));
                string urlimg = clsUtils.Base64ToImage(b64);

                Response.ContentType = "image/jpeg";
                Response.AppendHeader("Content-Disposition", "attachment; filename=imgEvidencia.jpeg");
                Response.WriteFile(urlimg);
                Response.Flush();
                Response.End();
            }
            catch(Exception ex)
            {
                clsLog.EscribeLogErr(ex, clsUtils.GetCurrentMethodName());
            }
        }

        protected void btnCerrarModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modal", "cerrar_modal();", true);
        }
    }
}