<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="NotaCreditoGenerarListado.aspx.cs" Inherits="Pages_Admin_NotaCreditoGenerarListado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">


    <script>
        $(document).ready(function () {
            GetNotas();
        });

        function GetNotas() {
            var urlData = 'NotaCreditoGenerarListado.aspx/GetList';
            var dataData = '{ }';
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
            if ($.fn.DataTable.isDataTable('#table_notas')) {
                $('#table_notas').dataTable().fnDestroy();
            }
            var data = JSON.parse(data);
            var table = $('#table_notas').DataTable({
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
                columnDefs: [
                      { className: "text-center", "targets": [1, 2, 3, 4, 5, 6, 7, 8] },
                ],
                columns: [
                    { data: "strCliente", title: "Cliente" },
                    { data: "strFactura", title: "Factura" },
                    { data: "strSerie", title: "Serie" },
                    { data: "decFolio", title: "Folio" },
                    { data: "strReferencia", title: "Referencia" },
                    { data: "decImporte", title: "Importe" },
                    { data: "datFechaGen", title: "Fecha Generación" },
                    { data: "strEstatus", title: "Estatus" },
                    { data: "datFechaAlta", title: "Fecha de Alta" },
                    {
                        data: null,
                        bSortable: false,
                        render: function (o) {

                            var btnDetalle = '<a href="NotaCreditoGenerar.aspx?id=' + o.intNotaCredito + '"><span class="glyphicon glyphicon-search"></span></a>'
                            var btnEliminar = '&nbsp&nbsp<a href="JavaScript:Eliminar(' + o.intNotaCredito + ')"><span class="glyphicon glyphicon-trash"></span></a>'
                            var btn = btnDetalle
                            if (o.intEstatus == 1)
                                btn = btn + btnEliminar;
                            return btn;
                        }
                    }
                ],
            });
        }

        function Eliminar(intNotaCredito) {
            if (confirm('Está seguro que desea eliminar el registro?')) {
                var urlData = 'NotaCreditoGenerarListado.aspx/Delete';
                var dataData = '{ intNotaCredito: ' + intNotaCredito + ' }';
                CallMethod(urlData, dataData, SuccessDelete);
            }
        }

        function SuccessDelete(response) {
            var message = response.d[0];
            var data = response.d[1];
            if (message == "ok") {
                alert('Registro eliminado correctamente.')
                GetNotas();
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <h2>Listado de Notas de Crédito</h2>
    <hr />
    <div>
        <a href="NotaCreditoGenerar.aspx" class="btn btn-primary">
            <span>Generar Nota de Crédito</span>
        </a>
    </div>
    <div class="row">
        <div class="col-lg-11">
            <table id="table_notas" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">
                
            </table>
        </div>
    </div>
</asp:Content>

