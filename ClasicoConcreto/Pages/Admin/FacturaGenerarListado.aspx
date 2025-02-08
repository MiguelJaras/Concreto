<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="FacturaGenerarListado.aspx.cs" Inherits="Pages_Admin_FacturaGenerarListado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
   <script src="../../Scripts/datatables/JSZip-2.5.0/jszip.min.js"></script>
    <script src="../../Scripts/datatables/pdfmake-0.1.36/pdfmake.min.js"></script>
    <script src="../../Scripts/datatables/pdfmake-0.1.36/vfs_fonts.js"></script>
    <script src="../../Scripts/datatables/Buttons-1.5.6/js/dataTables.buttons.min.js"></script>
    <script src="../../Scripts/datatables/Buttons-1.5.6/js/buttons.bootstrap.min.js"></script>
    <script src="../../Scripts/datatables/Buttons-1.5.6/js/buttons.html5.min.js"></script>
    <script src="../../Scripts/datatables/Buttons-1.5.6/js/buttons.print.min.js"></script>
    <script src="../../Scripts/datatables/Buttons-1.5.6/js/buttons.flash.min.js"></script>

    <script>
       var interval;
       interval = setInterval(function () {

           GetFacturas();
        }, 20000);

        $(document).ready(function () {

            $('#ctl00_BodyContent_txtFechaInicio').datepicker({
                dateFormat: 'dd/mm/yy',
            });

            $('#ctl00_BodyContent_txtFechaFin').datepicker({
                dateFormat: 'dd/mm/yy',
            });

            GetFacturas();
        });

        function GetFacturas() {
            var strFechaInicio = $('#ctl00_BodyContent_txtFechaInicio').val();
            var strFechaFin = $('#ctl00_BodyContent_txtFechaFin').val();

            var urlData = 'FacturaGenerarListado.aspx/GetList';
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
            if ($.fn.DataTable.isDataTable('#table_facturas')) {
                $('#table_facturas').dataTable().fnDestroy();
            }
            var data = JSON.parse(data);
            var table = $('#table_facturas').DataTable({
                autoWidth: false,
                searching: true,
                bLengthChange: false,
                paging: true,
                pageLength: 20,
                responsive: true,
                //orderCellsTop: true,
                language: {
                    url: "../../Scripts/dataTables/js/Spanish.js"
                },
                order: [[6, "desc"]],
                data: data,

                dom: "Bfrtip",
                buttons: [
                    {
                        text: 'Generar Factura',
                        action: function (e, dt, node, config) {
                            window.location = 'FacturaGenerar.aspx';
                        },
                        className: 'btn btn-primary'
                    },
                ],


                columnDefs: [
                      { className: "text-center", "targets": [1, 2, 4, 5, 6] },
                      { "width": "10%", "targets": 1 },
                      {
                          render: function (data, type, full, meta) {
                              data = data.replace(new RegExp(",", "g"), ', ')
                              return "<div class='text-wrap width-400'>" + data + "</div>";
                          }, "targets": [1]
                      },
                ],
                columns: [
                    //{ data: "intFactura", title: "intFactura" },
                    { data: "strCliente", title: "Cliente" },
                    { data: "strPedidos", title: "Pedidos" },
                    { data: "strSerie", title: "Serie" },
                    { data: "decFolio", title: "Folio" },
                    { data: "datFechaGen", title: "Fecha Generación" },
                    { data: "strEstatus", title: "Estatus" },
                    { data: "datFechaAlta", title: "Fecha de Alta" },
                    {
                        data: null,
                        bSortable: false,
                        render: function (o) {
                            
                            var btnDetalle = '<a href="FacturaGenerarDetalle.aspx?id=' + o.intFactura + '"><span class="glyphicon glyphicon-search"></span></a>'
                            var btnEliminar = '&nbsp&nbsp<a href="JavaScript:Eliminar(' + o.intFactura + ')"><span class="glyphicon glyphicon-trash"></span></a>'
                            var btn = btnDetalle
                            if (o.intEstatus == 1)
                                btn = btn + btnEliminar;
                            return btn;
                        }
                    }
                ],
                
            });
        }

        function Eliminar(intFactura)
        {
            if (confirm('Está seguro que desea eliminar el registro?')) {
                var urlData = 'FacturaGenerarListado.aspx/Delete';
                var dataData = '{ intFactura: ' + intFactura + ' }';
                CallMethod(urlData, dataData, SuccessDelete);
            }
        }

        function SuccessDelete(response) {
            var message = response.d[0];
            var data = response.d[1];
            if (message == "ok") {
                alert('Registro eliminado correctamente.')
                GetFacturas();
            } else {
                alert(data);
            }
        }


    </script>

     <style>
        .text-wrap{
            white-space:normal !important;
        }
        .width-400{
            width:300px;
        }
        .btn-group {
          position:relative;
          vertical-align:middle;
          display:block;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <h2>Listado de Facturas</h2>
    <hr />
    <div class="row">
        <div class="col-md-11">
             <div class="panel panel-primary">
                <div class="panel-heading">Filtros</div>
                <div class="panel-body form-horizontal">

                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-3">
                            <div class="row">
                                <label class="col-md-5 control-label">Fecha Inicial</label>
                                <div class="input-group col-md-7">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></span>
                                    <asp:textbox ID="txtFechaInicio" runat="server" CssClass="form-control"></asp:textbox>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-3">
                            <div class="row">
                                <label class="col-md-5 control-label">Fecha Final</label>
                                <div class="input-group col-md-7">
                                    <span class="input-group-addon" id="Span1"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></span>
                                    <asp:textbox ID="txtFechaFin" runat="server" CssClass="form-control"></asp:textbox>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-1">
                             <div class="row">
                                <div class="col-md-12">
                                    <input type="button" class="btn btn-primary btn-block" value="Filtrar" onclick="GetFacturas();" />
                                </div>
                            </div>
                        </div>
       
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <br />
    <div class="row">
        <div class="col-md-11">
            <table id="table_facturas" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">
                
            </table>
        </div>
    </div>

</asp:Content>

