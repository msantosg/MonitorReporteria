<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_MonitorReportes.aspx.cs" Inherits="Prototipo_Proyecto_Web.frm_MonitorReportes" EnableEventValidation="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Monitor</title>
    <link rel="stylesheet" type="text/css" href="Content/bootstrap.min.css"/>
<link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css"/>
<link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/css/all.min.css"/>
<link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.0.0/sweetalert.min.css" />
<link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.13.3/themes/base/jquery-ui.min.css" />
<style>
    .ui-datepicker .ui-datepicker-header
    {
        font-size: 12px;
        background-color:darkblue;
        color: white;
    }

    .ui-datepicker .ui-state-active
    {
        background-color:orangered;
        color: white;
    }
</style>
<script type="text/javascript" src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
<script type="text/javascript" src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>  
<script type="text/javascript" src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.0.0/sweetalert.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.13.3/jquery-ui.min.js"></script>
<script type="text/javascript" src="Scripts/MascaraDate.js"></script>
<script type="text/javascript" src="Scripts/alertas.js"></script>
<script type="text/javascript">
    function levantar_modal() {
        
        $("#exampleModal").show();
    }

    function cerrar_modal() {

        $("#exampleModal").hide();
    }
</script>
  <script type="text/javascript">
      function MostrarPregunta(strMensaje) {
          swal({
              title: "¡Alerta!",
              text: strMensaje,
              type: "warning",
              showCancelButton: true,
              confirmButtonColor: "#DD6B55",
              confirmButtonText: "Confirmar",
              cancelButtonText: "Cancelar",
              closeOnConfirm: false,
              closeOnCancel: true
          },
              function (isConfirm) {
                  if (isConfirm) {
                      console.log('vamos a confirmar')
                      __doPostBack("<%= btnOculto.UniqueID %>", "OnClick");
                  }
              }
          );
      }
  </script>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <div class="container shadow p-3 mb-5 bg-white rounded">
            <br />
            <h2 class="text-center">Monitor de Ejecución</h2>
            <div class="row d-flex justify-content-center">
                <div class="col-sm-3">
                    <div class="card">
                        <div class="card-body bg-info" style="color: white;">
                            <div class="row">
                                <div class="col-2"><i class="fa-solid fa-clock-rotate-left" style="font-size: 40px;"></i></div>
                                <div class="col-8">&nbsp;&nbsp;Pendientes de completar</div>
                                <div class="col-2"><asp:label runat="server" ID="lblPendientes"></asp:label></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-3">
                    <div class="card">
                        <div class="card-body bg-danger" style="color: white;">
                            <div class="row">
                                <div class="col-2"><i class="fa-solid fa-clock" style="font-size: 40px;"></i></div>
                                <div class="col-8">&nbsp;&nbsp;Vencidos</div>
                                <div class="col-2"><asp:Label runat="server" ID="lblVencidos"></asp:Label></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-3">
                    <div class="card">
                        <div class="card-body bg-success" style="color: white;">
                            <div class="row">
                                <div class="col-2"><i class="fa-solid fa-check" style="font-size: 40px;"></i></div>
                                <div class="col-8">&nbsp;&nbsp;Completados</div>
                                <div class="col-2"><asp:Label runat="server" ID="lblCompletados"></asp:Label></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-3">
                    <div class="card">
                        <div class="card-body"style="background-color:orange;color:white;">
                            <div class="row">
                                <div class="col-2"><i class="fa-solid fa-file-circle-check" style="font-size: 40px;"></i></div>
                                <div class="col-8">&nbsp;&nbsp;Total</div>
                                <div class="col-2"><asp:Label runat="server" ID="lblTotal"></asp:Label></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
           <div class="border border-primary" id="dvRptEjec" runat="server" visible="false">
                 <br />
                 <h2 class="text-center">Cargar Evidencia</h2>
                 <br />
                 <asp:TextBox runat="server" ID="txtIDEjec" Visible="false"></asp:TextBox>
                 <br />
                 <div class="row">
                     <div class="col-sm-4">
                         <div class="input-group input-group-sm">
                             <span class="input-group-text" id="basicaddon8">ID CONFIGURACIÓN</span>
                             <asp:TextBox runat="server" ID="txtIDConf" CssClass="form-control" aria-label="Descripción Reporte" aria-describedby="basicaddon8" Enabled="false"></asp:TextBox>
                         </div>
                     </div>
                     <div class="col-sm-8">
                         <div class="input-group input-group-sm">
                             <span class="input-group-text" id="basicaddon9">IMAGEN</span>
                             <asp:FileUpload runat="server" ID="upf" CssClass="form-control" accept=".jpg" aria-label="Descripción Reporte" aria-describedby="basicaddon9" />
                             <asp:LinkButton runat="server" ID="btnUpf" CssClass="btn-outline-secondary btn btn-sm" OnClick="btnUpf_Click"><i class="fa-solid fa-upload"></i> subir</asp:LinkButton>
                         </div>
                         <asp:Label runat="server" ID="lblMsg" Visible ="false"></asp:Label>
                     </div>
                 </div>
               <br />
               <div class="row">
                   <div class="col-sm-6">
                       <asp:LinkButton runat="server" ID="btnCancelar" CssClass="btn btn-danger btn-sm" OnClick="btnCancelar_Click"><i class="fa-solid fa-delete-left"></i> Cancelar</asp:LinkButton>
                       <asp:LinkButton runat="server" ID="btnGuardar" CssClass="btn btn-success btn-sm" OnClick="btnGuardar_Click"><i class="fa-solid fa-add"></i> Guardar</asp:LinkButton>
                       <asp:Button runat="server" ID="btnOculto" Visible="false" OnClick="btnOculto_Click"/>
                   </div>
               </div>
                 <br />
                
            </div>
            <br>
            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="input-group mb-3">
                                <input id="InputBx" type="text" class="form-control form-control-sm" placeholder="Buscar" aria-label="Recipient's username" aria-describedby="button-addon2" onkeyup="Search_Grid(this)"/>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="d-flex justify-content-end"><a href="frm_AsignarReporte.aspx" class="btn btn-success btn-sm">Ir a asignación de reporte <i class="fa-solid fa-arrow-circle-right"></i></a></div>
                     <div class="row">
                         <div class="col-sm-12">
                             <div class="table-responsive">
                                 <asp:GridView runat="server" ID="gvEjecRpt" AutoGenerateColumns="false" AllowPaging="True" PageSize="10" ClientIDMode="Static"
                                     BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                     GridLines="Horizontal" CssClass="table table-sm text-nowrap mt-5" Width="1020px" OnPageIndexChanging="gvEjecRpt_PageIndexChanging"
                                     Style="border: 1px solid black; overflow: scroll; max-height: 200px;" OnRowDataBound="gvEjecRpt_RowDataBound" OnRowCommand="gvEjecRpt_RowCommand"
                                     DataKeyNames="ID_CONFIGURACION,ID_EJECUCION">
                                     <Columns>
                                         <asp:BoundField HeaderText="ID CONFIGURACIÓN" DataField="ID_CONFIGURACION" />
                                         <asp:BoundField HeaderText="ID EJECUCIÓN" DataField="ID_EJECUCION" />
                                         <asp:BoundField HeaderText="DESCRIPCIÓN" DataField="DESCRIPCION" />
                                         <asp:BoundField HeaderText="FECHA COMPROMISO" DataField="FECHA_COMPROMISO" DataFormatString="{0:dd/MM/yyyy}"/>
                                         <asp:BoundField HeaderText="IMAGEN" DataField="IMAGEN" />
                                         <asp:BoundField HeaderText="FECHA REGISTRO IMAGEN" DataField="FECHA_REG_IMG" DataFormatString="{0:dd/MM/yyyy}"/>
                                         <asp:BoundField HeaderText="ESTADO" DataField="ESTADO" />
                                         <asp:BoundField HeaderText="FECHA REGISTRO" DataField="FECHA_REGISTRO" DataFormatString="{0:dd/MM/yyyy}"/>
                                         <asp:TemplateField>
                                             <ItemTemplate>
                                                 <asp:LinkButton runat="server" ID="btnCargaImg" CssClass="btn btn-sm" BackColor="Orange" ForeColor="White" CommandName="Select" 
                                                     CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ><i class="fa-solid fa-upload"></i> Subir Imagen</asp:LinkButton>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField>
                                             <ItemTemplate>
                                                 <asp:LinkButton runat="server" ID="btnVerImg" CssClass="btn btn-sm btn-success" CommandName="Detail" 
                                                     CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class="fa-solid fa-search"></i> Ver Evidencia</asp:LinkButton>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                     </Columns>
                                     <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                     <HeaderStyle BackColor="DarkBlue" Font-Bold="True" ForeColor="White" />
                                     <PagerSettings FirstPageText="Inicio" LastPageText="Ultimo" NextPageText="Siguiente" PreviousPageText="Anterior" PageButtonCount="10" Mode="Numeric" />
                                     <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                     <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                     <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                     <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                     <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                     <SortedDescendingHeaderStyle BackColor="#242121" />
                                 </asp:GridView>
                             </div>
                         </div>
                     </div>
                </div>
            </div>
           
            <br />
            
        </div>
        <div class="modal modal-lg" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Evidencia de Ejecución de Reporte</h5>
                    </div>
                    <div class="modal-body">
                        <asp:TextBox runat="server" ID="txtIdEjecDown" Visible="false"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtIdConfDown" Visible="false"></asp:TextBox>
                        <div runat="server" id="dvImg">
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton runat="server" ID="btnCerrarModal" CssClass="btn btn-sm btn-danger" OnClick="btnCerrarModal_Click"><i class="fa-solid fa-trash"></i> Cerrar</asp:LinkButton>
                        <asp:LinkButton runat="server" ID="btnDescargarImg" CssClass="btn btn-sm btn-success" OnClick="btnDescargarImg_Click"><i class="fa-solid fa-download"></i> Descargar Evidencia</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </form>    
    
     <script type="text/javascript">
         function Search_Grid(strKey) {
             var value = strKey.value.toLowerCase().split(" ");

             var tblData = document.getElementById("<%= gvEjecRpt.ClientID%>");

             var rowData;

             for (var i = 1; i < tblData.rows.length; i++) {
                 rowData = tblData.rows[i].innerHTML;
                 var styleDisplay = 'none';

                 for (var j = 0; j < value.length; j++) {
                     if (rowData.toLowerCase().indexOf(value[j]) >= 0)
                         styleDisplay = '';
                     else {
                         styleDisplay = 'none';
                         break;
                     }
                 }
                 tblData.rows[i].style.display = styleDisplay;
             }
         }
     </script>
</body>
</html>
