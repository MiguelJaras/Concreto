<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="FacturaEmail.aspx.cs" Inherits="Pages_Admin_FacturaEmail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            GetFacturas();
        });

        function GetFacturas() {
            var urlData = 'FacturaEmail.aspx/GetList';
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
            if ($.fn.DataTable.isDataTable('#table_facturas')) {
                $('#table_facturas').dataTable().fnDestroy();
            }
            var data = JSON.parse(data);
            var table = $('#table_facturas').DataTable({
                searching: false,
                bLengthChange: false,
                paging: true,
                pageLength: 20,
                responsive: true,
                orderCellsTop: true,
                language: {
                    url: "../../Scripts/dataTables/js/Spanish.js"
                },
                order: [[0, "desc"]],
                data: data,
                columnDefs: [
                      { className: "text-center", "targets": [0, 1, 3, 4, 5, 6] },
                      { className: "text-right", "targets": [2] },
                ],
                columns: [
                    {
                        data: null, title: '<input type="checkbox" id="chkSel" onchange="CheckAll(this.checked);"></input>',
                        render: function (data, type, object, row) {
                            return '<input type="checkbox" class="chkNss" name="chkNSS" id="chkNSS_' + data.strFactura + '" value="' + data.strFactura + '"></input>';
                        }
                    },
                    { data: "strEmpresa", title: "Empresa" },
                    { data: "strFactura", title: "Factura" },
                    { data: "dblImporte", title: "Importe", render: $.fn.dataTable.render.number(',', '.', 2, '$ ') },
                    {
                        title: 'Factura',
                        data: null,
                        bSortable: false,
                        render: function (o) {
                            return '<a class="link" onclick="Javascript:OpenFilePDF(\'' + o.strFactura + '\',\'' + o.strPDF + '\',\'factura\')">' + o.strPDF + '</a>';
                        }
                    },
                    {
                        title: 'Factura XML',
                        data: null,
                        bSortable: false,
                        render: function (o) {
                            return '<a class="link" onclick="Javascript:OpenFilePDF(\'' + o.strFactura + '\',\'' + o.strXML + '\',\'xml\')">' + o.strXML + '</a>';
                        }
                    },
                    { data: "datFechaAlta", title: "Fecha de Alta" },
                    {
                        data: "bEnvioEmail", title: "Envío de email",
                        bSortable: false,
                        render: function (data, type, row) {
                            return (data == true) ? '<span class="glyphicon glyphicon-ok"></span>' : '<span class="glyphicon glyphicon-remove"></span>';
                        }
                    },
                ],
            });
        }


        function CheckAll(checked)
        {
            var dataTable = $('#table_facturas').DataTable();
            dataTable.rows().nodes().to$().find('input[type="checkbox"]').each(function () {
                $(this).prop('checked', checked);
            });
        }
        function Enviar()        {
            var lstEnviar = [];
            var dataTable = $('#table_facturas').DataTable();
            dataTable.rows().nodes().to$().find('input[type="checkbox"]').each(function () {
                if (this.checked) {
                    lstEnviar.push(this.value);
                }
            });            alert(lstEnviar);        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <h2>Facturas Sin Enviar Email</h2>
    <hr />
    <div>
        <input type="button" class="btn btn-primary" onclick="Enviar();" value="Enviar" />
        
    </div>
    <div class="row">
        <div class="col-lg-12">
            <table id="table_facturas" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">
                
            </table>
        </div>
    </div>
</asp:Content>

