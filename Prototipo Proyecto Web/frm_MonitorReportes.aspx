<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_MonitorReportes.aspx.cs" Inherits="Prototipo_Proyecto_Web.frm_MonitorReportes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Monitor</title>
    <link rel="stylesheet" type="text/css" href="Content/bootstrap.min.css"/>
    <link rel="stylesheet" type="text/css" href="Content/fontawesome-free-6.5.2-web/css/all.min.css"/>
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
                 <div class="row">
                     <div class="col-sm-4">
                         <div class="input-group input-group-sm">
                             <span class="input-group-text" id="basicaddon8">ID CONFIGURACIÓN</span>
                             <asp:TextBox runat="server" ID="TextBox2" CssClass="form-control" aria-label="Descripción Reporte" aria-describedby="basicaddon8" Enabled="false"></asp:TextBox>
                         </div>
                     </div>
                     <div class="col-sm-8">
                         <div class="input-group input-group-sm">
                             <span class="input-group-text" id="basicaddon9">IMAGEN</span>
                             <asp:FileUpload runat="server" ID="upf" CssClass="form-control" aria-label="Descripción Reporte" aria-describedby="basicaddon9" />
                             <asp:LinkButton runat="server" ID="btnUpf" CssClass="btn-outline-secondary btn btn-sm"><i class="fa-solid fa-upload"></i> subir</asp:LinkButton>
                         </div>
                     </div>
                 </div>
               <br />
               <div class="row">
                   <div class="col-sm-6">
                       <asp:LinkButton runat="server" ID="LinkButton1" CssClass="btn btn-danger btn-sm"><i class="fa-solid fa-delete-left"></i> Cancelar</asp:LinkButton>
                       <asp:LinkButton runat="server" ID="LinkButton2" CssClass="btn btn-success btn-sm"><i class="fa-solid fa-add"></i> Guardar</asp:LinkButton>
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
                                <input id="InputBx" type="text" class="form-control form-control-sm" placeholder="Buscar" aria-label="Recipient's username" aria-describedby="button-addon2" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                     <div class="row">
                         <div class="col-sm-12">
                             <div class="table-responsive">
                                 <asp:GridView runat="server" ID="gvEjecRpt" AutoGenerateColumns="false" AllowPaging="True" PageSize="10" ClientIDMode="Static"
                                     BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                     GridLines="Horizontal" CssClass="table table-sm text-nowrap mt-5" Width="1020px" OnPageIndexChanging="gvEjecRpt_PageIndexChanging"
                                     Style="border: 1px solid black; overflow: scroll; max-height: 200px;" OnRowDataBound="gvEjecRpt_RowDataBound">
                                     <Columns>
                                         <asp:BoundField HeaderText="ID CONFIGURACIÓN" DataField="ID_CONFIGURACION" />
                                         <asp:BoundField HeaderText="ID EJECUCIÓN" DataField="ID_EJECUCION" />
                                         <asp:BoundField HeaderText="DESCRIPCIÓN" DataField="DESCRIPCION" />
                                         <asp:BoundField HeaderText="FECHA COMPROMISO" DataField="FECHA_COMPROMISO" />
                                         <asp:BoundField HeaderText="IMAGEN" DataField="IMAGEN" />
                                         <asp:BoundField HeaderText="FECHA REGISTRO IMAGEN" DataField="FECHA_REG_IMG" />
                                         <asp:BoundField HeaderText="ESTADO" DataField="ESTADO" />
                                         <asp:BoundField HeaderText="FECHA REGISTRO" DataField="FECHA_REGISTRO" />
                                         <asp:TemplateField>
                                             <ItemTemplate>
                                                 <asp:LinkButton runat="server" ID="btnCargaImg" CssClass="btn btn-sm" BackColor="Orange" ForeColor="White"><i class="fa-solid fa-upload"></i> Subir Imagen</asp:LinkButton>
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
    </form>    
    <script type="text/javascript" src="Scripts/jquery-3.4.1.min.js"></script>
    <script type="text/javascript" src="Scripts/bootstrap.min.js"></script>     
    <script type="text/javascript">
        $(document).ready(function () {

            $("#InputBx").on("keyup", function () {
                var value = $(this).val().toLowerCase();

                $("#gvEjecRpt tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
    </script>
</body>
</html>
