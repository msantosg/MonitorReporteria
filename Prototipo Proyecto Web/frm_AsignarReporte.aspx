<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_AsignarReporte.aspx.cs" Inherits="Prototipo_Proyecto_Web.frm_AsignarReporte" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Asignaciones</title>
    <link rel="stylesheet" type="text/css" href="Content/bootstrap.min.css"/>
    <link rel="stylesheet" type="text/css" href="Content/fontawesome-free-6.5.2-web/css/all.min.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <div class="container shadow p-3 mb-5 bg-white rounded">
            <br />
            <h2 class="text-center">Configuración de Reportes</h2>
            <br />
            <div id="dvAlerta" runat="server" visible="false">
            </div>
            <br />
            <div class="row d-flex justify-content-end">
                <div class="col-sm-3">
                    <asp:LinkButton runat="server" ID="btnNConfig" CssClass="btn btn-sm btn-primary" OnClick="btnNConfig_Click"><i class="fa-solid fa-add"></i> Nueva Configuración</asp:LinkButton>
                </div>   
            </div>
            <br />
            <div class="border border-primary" runat="server" id="dvNConfig" visible="false">
                <br />   
                <div class="row">
                    <div class="col-sm-6">
                        <div class="input-group input-group-sm">
                            <span class="input-group-text" id="basicaddon1">Descripción Reporte</span>
                            <asp:TextBox runat="server" ID="txtDescReport" CssClass="form-control" aria-label="Descripción Reporte" aria-describedby="basicaddon1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-6"> 
                        <div class="input-group input-group-sm">
                            <span class="input-group-text" id="basicaddon2">Área Responsable</span>
                            <asp:DropDownList runat="server" ID="dropAreasR" CssClass="form-control" aria-label="Descripción Reporte" aria-describedby="bassicaddon2">
                                <asp:ListItem Value="---">Seleccione Área</asp:ListItem>
                                <asp:ListItem Value="1">Área Número 1</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-6">
                        <div class="input-group input-group-sm">
                            <span class="input-group-text" id="basicaddon3">Área Solicitante</span>
                            <asp:DropDownList runat="server" ID="dropAreasSol" CssClass="form-control" aria-label="Descripción Reporte" aria-describedby="bassicaddon3">
                                <asp:ListItem Value="---">Seleccione Área</asp:ListItem>
                                <asp:ListItem Value="1">Área Número 1</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="input-group input-group-sm">
                            <span class="input-group-text" id="basicaddon5">Correo electrónico</span>
                            <asp:TextBox runat="server" ID="txtCorreo" CssClass="form-control" aria-label="Descripción Reporte" aria-describedby="basicaddon4"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-6">
                        <div class="input-group input-group-sm">
                            <span class="input-group-text" id="basicaddon5">Usuario</span>
                            <asp:DropDownList runat="server" ID="dropUsuario" CssClass="form-control" aria-label="Descripción Reporte" aria-describedby="bassicaddon5">
                                <asp:ListItem Value="---">Seleccione Usuario</asp:ListItem>
                                <asp:ListItem Value="1">Usuario Número 1</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="input-group input-group-sm">
                            <span class="input-group-text" id="basicaddon6">Periodicidad</span>
                            <asp:DropDownList runat="server" ID="dropPeriodo" CssClass="form-control" aria-label="Descripción Reporte" aria-describedby="bassicaddon6">
                                <asp:ListItem Value="---">Seleccione Periodicidad</asp:ListItem>
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
                             <asp:TextBox runat="server" ID="txtAnticipacion" CssClass="form-control" aria-label="Descripción Reporte" aria-describedby="basicaddon7"></asp:TextBox>
                         </div>
                     </div>
                    <div class="col-sm-6">
                        <asp:LinkButton runat="server" ID="btnCancelar" CssClass="btn btn-danger btn-sm" OnClick="btnCancelar_Click"><i class="fa-solid fa-delete-left"></i> Cancelar</asp:LinkButton>
                        <asp:LinkButton runat="server" ID="btnGuardar" CssClass="btn btn-success btn-sm" OnClick="btnGuardar_Click"><i class="fa-solid fa-add"></i> Guardar</asp:LinkButton>
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
                                <input id="InputBx" type="text" class="form-control form-control-sm" placeholder="Buscar" aria-label="Recipient's username" aria-describedby="button-addon2" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="table-responsive">
                                <asp:GridView runat="server" ID="gvConfigMonitor" AutoGenerateColumns="false" AllowPaging="True" ClientIDMode="Static"
                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                    GridLines="Horizontal" CssClass="table table-sm text-nowrap mt-5" Width="1020px"
                                    Style="border: 1px solid black; overflow: scroll; max-height: 200px;" OnRowDataBound="gvConfigMonitor_RowDataBound">
                                    <Columns>
                                        <asp:BoundField HeaderText="ID" DataField="ID_CONFIGURACION" />
                                        <asp:BoundField HeaderText="USUARIO" DataField="USUARIO" />
                                        <asp:BoundField HeaderText="ÁREA RESPONSABLE" DataField="AREA_RESPONSABLE" />
                                        <asp:BoundField HeaderText="ÁREA SOLICITANTE" DataField="AREA_SOLICITANTE" />
                                        <asp:BoundField HeaderText="CORREO" DataField="CORREO" />
                                        <asp:BoundField HeaderText="DESCRIPCIÓN" DataField="DESCRIPCION" />
                                        <asp:BoundField HeaderText="PERIODICIDAD" DataField="PERIODICIDAD" />
                                        <asp:BoundField HeaderText="ANTICIPACIÓN" DataField="ANTICIPACION" />
                                        <asp:BoundField HeaderText="ESTADO" DataField="ESTADO" />
                                        <asp:BoundField HeaderText="FECHA REGISTRO" DataField="FECHA_REGISTRO" />
                                        <asp:BoundField HeaderText="USUARIO MODIFICA" DataField="USUARIO_MODIFICACION" />
                                        <asp:BoundField HeaderText="FECHA MODIFICACIÓN" DataField="FECHA_MODIFICACION" />
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
    <script type="text/javascript" src="Scripts/jquery-3.4.1.min.js"></script>
    <script type="text/javascript" src="Scripts/bootstrap.min.js"></script>  
    <script type="text/javascript">
    $(document).ready(function () {

        $("#InputBx").on("keyup", function () {
            var value = $(this).val().toLowerCase();

            $("#gvConfigMonitor tr").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            });
        });
    });
    </script>
</body>
</html>
