<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="PedidoListado.aspx.cs" Inherits="Pages_Opciones_PedidoListado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    
    <script>
        $(document).ready(function () {

            $('#ctl00_BodyContent_txtFechaInicio').datepicker({
                dateFormat: 'dd/mm/yy',
            });

            $('#ctl00_BodyContent_txtFechaFin').datepicker({
                dateFormat: 'dd/mm/yy',
            });

            BuscarPedidos();
        });

        function BuscarPedidos() {
            var strFechaInicio = $('#ctl00_BodyContent_txtFechaInicio').val();
            var strFechaFin = $('#ctl00_BodyContent_txtFechaFin').val();
            var urlData = 'PedidoListado.aspx/GetList';
            var dataData = '{ strFechaInicio:\'' + strFechaInicio + '\', strFechaFin: \'' + strFechaFin + '\'}';
            CallMethod(urlData, dataData, SuccessData);
        }

        function SuccessData(response) {
            var message = response.d[0];
            var data = response.d[1];
            if (message == "ok") {
                CreateTable(data);
            } else {
                alert('Error al cargar los datos')
            }
        }

        function CreateTable(data) {
            if ($.fn.DataTable.isDataTable('#table_pedidos')) {
                $('#table_pedidos').dataTable().fnDestroy();
            }
            var data = JSON.parse(data);
            var table = $('#table_pedidos').DataTable({
                searching: false,
                bLengthChange: false,
                paging: true,
                pageLength: 20,
                responsive: true,
                orderCellsTop: true,
                language: {
                    url: "../../Scripts/dataTables/js/Spanish.js"
                },
                order: [[ 0, "desc" ]],
                data: data,
                columnDefs: [
                      { className: "text-center", "targets": [0, 1, 2, 4, 5, 6, 7] },
                      //{ "width": "35%", "targets": 1 },
                      //{ "width": "12%", "targets": [2,3,4] },
                ],
                columns: [
                    
                    { data: "intPedido", title: "#" },
                    { data: "PO_Num", title: "Orden de Compra" },
                    { data: "datFechaEntrega", title: "Fecha de Entrega" },
                    { data: "strCliente",   title: "Cliente" },
                    { data: "City",         title: "Ciudad" },
                    { data: "Estatus",      title: "Estatus" },
                    { data: "datFechaAlta", title: "Fecha Alta" },
                    {
                        data: null,
                        bSortable: false,
                        render: function (o) {
                            var options = '';
                            if (o.Ordenes > 0) {
                                options = '<a href="#" onclick=JavaScript=ModalOrdenes(' + o.intPedido + ')><span class="glyphicon glyphicon-list-alt"></span></a>&nbsp&nbsp&nbsp'
                            } else {
                                options = '<a href="#" class="invisible"><span class="glyphicon glyphicon-list-alt"></span></a>&nbsp&nbsp&nbsp'
                            }
                            if (o.Order_Status != "C") {
                                options += '<a href=Pedido.aspx?id=' + o.intPedido + '><span class="glyphicon glyphicon-pencil"></span></a>';
                            }
                            return options;
                        }
                    }
                ],

            });

        }

        function EnviarEmail(intPedido) {
            $('#<%=hddIntPedido.ClientID%>').val(intPedido);
            $('#<%=btnEmail.ClientID%>').click();
        }

        function ModalOrdenes(intPedido)
        {
            var urlData = 'PedidoListado.aspx/GetDetallePedido';
            var dataData = '{ intPedido: ' + intPedido + ' }';
            CallMethod(urlData, dataData, SuccessDetallePedido);
        }

        function SuccessDetallePedido(response) {
            var message = response.d[0];
            var data = response.d[1];
            if (message == "ok") {
                var data = JSON.parse(data);
                var htmlContent = '<div id="detallePedido" class="white-popup"><table id="tabla_detalle" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%"></table></div>';
                $.magnificPopup.open({
                    items: [
                        {
                            type: 'inline',
                            src: $(htmlContent)
                        }
                    ],
                });

                var table = $('#tabla_detalle').DataTable({
                    
                    searching: false,
                    bLengthChange: false,
                    paging: false,
                    responsive: true,
                    orderCellsTop: false,
                    language: {
                        url: "../../Scripts/dataTables/js/Spanish.js"
                    },
                    //order: [[0, "desc"]],
                    data: data,
                    "columnDefs": [
                        { className: "text-center", "targets": [0, 1, 2, 4, 5, 6] },
                        { "orderable": false, "targets": [0,1,2,3] }
                    ],
                    columns: [
                        { data: "OC", title: "Orden" },
                        { data: "Truck_Code", title: "No. de Camión" },
                        { data: "Ticket_Code", title: "Ticket" },
                        { data: "Driver_Name", title: "Chofer" },
                        { data: "Load_Size", title: "Cargado" },
                        { data: "CreateDate", title: "Fecha de Creación" },
                        { data: "DeliveredDate", title: "Fecha de Entrega" },
                    ],
                    //"drawCallback": function (settings) {
                    //    var api = this.api();
                    //    var rows = api.rows({ page: 'current' }).nodes();
                    //    var last = null;

                    //    api.column(0, { page: 'current' }).data().each(function (group, i) {
                    //        var rowData = table.row(i).data();
                    //        if (last !== group) {
                    //            $(rows).eq(i).before(
                    //                RowGroup(rowData)
                    //                //'<tr class="row-group"><td colspan="5"><b>Orden:</b>' + group + '</td></tr><tr><td>aa</td></tr>'
                    //            );

                    //            last = group;
                    //        }
                    //    });
                    //},
                    "initComplete": function () {
                        $('#tabla_detalle_info').html(
                            ''
                        );
                    }
                });

            } else {
                alert('Error al cargar los datos')
            }
        }

        function RowGroup(rowData)
        {
            var row_html = '<tr class="row-group"><td class="hide"></td></td>';
            row_html += '<td colspan="4">Orden: ' + rowData.Order_Code;
            row_html += '</tr>';
            return row_html;
        }

        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">

    <h2>Listado de Pedidos</h2>
    <hr />

    <div class="row form-horizontal">
        <div class="col-lg-12">
            <div class="form-group">
                <label class="col-md-1 control-label">Fecha Inicial</label>
                <div class="input-group col-md-2">
                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></span>
                    <asp:textbox ID="txtFechaInicio" runat="server" CssClass="form-control"></asp:textbox>
                </div>
            
            </div>
            <div class="form-group">
                <label class="col-md-1 control-label">Fecha Final</label>
                <div class="input-group col-md-2">
                    <span class="input-group-addon" id="Span1"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></span>
                    <asp:textbox ID="txtFechaFin" runat="server" CssClass="form-control"></asp:textbox>
                </div>

            </div>
            <div class="form-group ">
                <label class="col-md-1 control-label"></label>
                <div class="col-md-2">
                    <input type="button" class="btn btn-primary btn-block" value="Filtrar" onclick="BuscarPedidos();" />
                </div>
                <div class="col-md-2">
                    <input type="button" class="btn btn-primary btn-block" value="Nuevo" onclick="window.location = 'Pedido.aspx'" />
                </div>

            </div>
        </div>
    </div>


    <div>
        <asp:Button runat="server" ID="btnEmail" CssClass="hide" OnClick="btnEmail_Click" />
        <asp:HiddenField runat="server" ID="hddIntPedido" Value="0" />
    </div>
    <div class="row">
        <div class="col-lg-12">
            <table id="table_pedidos" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">
                
            </table>
        </div>
    </div>

</asp:Content>

