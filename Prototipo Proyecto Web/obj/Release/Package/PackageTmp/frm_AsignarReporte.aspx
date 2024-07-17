<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_AsignarReporte.aspx.cs" Inherits="Prototipo_Proyecto_Web.frm_AsignarReporte" EnableEventValidation="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Asignaciones</title>
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
    <script type="text/javascript" src="Scripts/alertas.js"></script>
    <script type="text/javascript" src="Scripts/MascaraDate.js"></script>
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
            <h2 class="text-center">Configuración de Reportes</h2>
            <br />
            <div class="row d-flex justify-content-end">
                <div class="col-sm-3">
                    <asp:LinkButton runat="server" ID="btnNConfig" CssClass="btn btn-sm btn-primary" OnClick="btnNConfig_Click"><i class="fa-solid fa-add"></i> Nueva Configuración</asp:LinkButton>
                </div>   
            </div>
            <br />
            <div class="border border-primary" runat="server" id="dvNConfig" visible="false">
                <asp:TextBox runat="server" ID="txtIdConf" Visible="false"></asp:TextBox>
                <br />
                <div class="row">
                    <div class="col-sm-6">                        
                            
                        <div class="input-group input-group-sm">
                            <span class="input-group-text" id="basicaddon1">Descripción Reporte</span>
                            <asp:TextBox runat="server" ID="txtDescReport" CssClass="form-control" style="text-transform:uppercase;" aria-label="Descripción Reporte" aria-describedby="basicaddon1" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-6"> 
                        <div class="input-group input-group-sm">
                            <span class="input-group-text" id="basicaddon2">Área Responsable</span>
                            <asp:DropDownList runat="server" ID="dropAreasR" CssClass="form-control" aria-label="Área Responsable" aria-describedby="bassicaddon2">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-6">
                        <div class="input-group input-group-sm">
                            <span class="input-group-text" id="basicaddon3">Área Solicitante</span>
                            <asp:DropDownList runat="server" ID="dropAreasSol" CssClass="form-control" aria-label="Área Solicitante" aria-describedby="bassicaddon3">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="input-group input-group-sm">
                            <span class="input-group-text" id="basicaddon5">Correo electrónico</span>
                            <asp:TextBox runat="server" ID="txtCorreo" CssClass="form-control" aria-label="Correo Electrónico" aria-describedby="basicaddon4" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-6">
                        <div class="input-group input-group-sm">
                            <span class="input-group-text" id="basicaddon5">Usuario</span>
                            <asp:DropDownList runat="server" ID="dropUsuario" CssClass="form-control" aria-label="Descripción Reporte" aria-describedby="bassicaddon5">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="input-group input-group-sm">
                            <span class="input-group-text" id="basicaddon6">Periodicidad</span>
                            <asp:DropDownList runat="server" ID="dropPeriodo" CssClass="form-control" aria-label="Descripción Reporte" aria-describedby="bassicaddon6">
                                <asp:ListItem Value="---">Seleccione Periodicidad</asp:ListItem>
                                <asp:ListItem Value="DIARIO">DIARIO</asp:ListItem>
                                <asp:ListItem Value="MENSUAL">MENSUAL</asp:ListItem>
                                <asp:ListItem Value="TRIMESTRAL">TRIMESTRAL</asp:ListItem>
                                <asp:ListItem Value="SEMESTRAL">SEMESTRAL</asp:ListItem>
                                <asp:ListItem Value="ANUAL">ANUAL</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>                   
                </div>
                <br />
                <div class="row">                    
                     <div class="col-sm-6">
                         <div class="input-group input-group-sm">
                             <span class="input-group-text" id="basicaddon7">Anticipación</span>
                             <asp:TextBox runat="server" ID="txtAnticipacion" CssClass="form-control" aria-label="Descripción Reporte" aria-describedby="basicaddon7" autocomplete="off"></asp:TextBox>
                         </div>
                     </div>
                    <div class="col-sm-6">
                        <div class="input-group input-group-sm">
                            <span class="input-group-text" id="basicaddon8">Sanción</span>
                            <asp:TextBox runat="server" ID="txtSancion" CssClass="form-control" aria-label="Sanción" aria-describedby="basicaddon8" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-6">
                        <div class="input-group input-group-sm">
                            <span class="input-group-text" id="basicaddon9">Fecha Publicación</span>
                            <asp:TextBox runat="server" ID="txtFePublica"  CssClass="form-control DtPicker" onkeyup="formataData(this,event)" aria-label="Descripción Reporte" aria-describedby="basicaddon9" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-6">
                        <asp:LinkButton runat="server" ID="btnCancelar" CssClass="btn btn-danger btn-sm" OnClick="btnCancelar_Click"><i class="fa-solid fa-delete-left"></i> Cancelar</asp:LinkButton>
                        <asp:LinkButton runat="server" ID="btnGuardar" CssClass="btn btn-success btn-sm" OnClick="btnGuardar_Click"><i class="fa-solid fa-add"></i> Guardar</asp:LinkButton>
                        <asp:Button runat="server" ID="btnOculto" CssClass="btnOculto" Visible="false" OnClick="btnOculto_Click"/>
                    </div>
                    <div class="col-sm-6">
                        <div runat="server" id="lst_errores" visible="false" class="text-danger">
                            <fieldset style="all:revert;" class="text-danger">
                                <legend style="all:revert;">Errores de validación de campos</legend>
                                <div runat="server" id="errores"></div>
                            </fieldset>
                        </div>
                    </div>
                </div>
                <br />                
            </div>  
            <br />
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
                    <div class="d-flex justify-content-end"><a href="frm_MonitorReportes.aspx" class="btn btn-success btn-sm">Ir a monitor reporte <i class="fa-solid fa-arrow-circle-right"></i></a></div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="table-responsive">
                                <asp:GridView runat="server" ID="gvConfigMonitor" AutoGenerateColumns="false" AllowPaging="True" ClientIDMode="Static"
                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                    DataKeyNames="ID_CONFIGURACION, USUARIO, AREA_RESPONSABLE, AREA_SOLICITANTE, CORREO, DESCRIPCION, PERIODICIDAD, ANTICIPACION, FECHA_PUBLICACION, SANCION, ESTADO, FECHA_REGISTRO, USUARIO_MODIFICACION, FECHA_MODIFICACION"
                                    GridLines="Horizontal" CssClass="table table-sm text-nowrap mt-5" Width="1020px"
                                    Style="border: 1px solid black; overflow: scroll; max-height: 200px;" OnRowDataBound="gvConfigMonitor_RowDataBound" OnRowCommand="gvConfigMonitor_RowCommand" OnPageIndexChanging="gvConfigMonitor_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField HeaderText="ID" DataField="ID_CONFIGURACION" />
                                        <asp:BoundField HeaderText="USUARIO" DataField="USUARIO" />
                                        <asp:BoundField HeaderText="ÁREA RESPONSABLE" DataField="AREA_RESPONSABLE" />
                                        <asp:BoundField HeaderText="ÁREA SOLICITANTE" DataField="AREA_SOLICITANTE" />
                                        <asp:BoundField HeaderText="CORREO" DataField="CORREO" />
                                        <asp:BoundField HeaderText="DESCRIPCIÓN" DataField="DESCRIPCION" />
                                        <asp:BoundField HeaderText="PERIODICIDAD" DataField="PERIODICIDAD" />
                                        <asp:BoundField HeaderText="ANTICIPACIÓN" DataField="ANTICIPACION" />
                                        <asp:BoundField HeaderText="FECHA PUBLICACIÓN" DataField="FECHA_PUBLICACION" DataFormatString="{0:dd/MM/yyyy}"/>
                                        <asp:BoundField HeaderText="SANCIÓN" DataField="SANCION"/>
                                        <asp:BoundField HeaderText="ESTADO" DataField="ESTADO" />
                                        <asp:BoundField HeaderText="FECHA REGISTRO" DataField="FECHA_REGISTRO" DataFormatString="{0:dd/MM/yyyy}"/>
                                        <asp:BoundField HeaderText="USUARIO MODIFICA" DataField="USUARIO_MODIFICACION" />
                                        <asp:BoundField HeaderText="FECHA MODIFICACIÓN" DataField="FECHA_MODIFICACION" DataFormatString="{0:dd/MM/yyyy}"/>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="btnEditarCNF" CssClass="btn btn-sm btn-success" CommandName="Select" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class="fa-solid fa-edit"></i> Editar Registro</asp:LinkButton>
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
        </div>
        
    </form>
    
    <script type="text/javascript">
        function Search_Grid(strKey) {
            var value = strKey.value.toLowerCase().split(" ");

            var tblData = document.getElementById("<%= gvConfigMonitor.ClientID%>");

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
