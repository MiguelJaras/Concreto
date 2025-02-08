<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="FacturaClienteListado.aspx.cs" Inherits="Pages_Admin_FacturaClienteListado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            GetClientes();

        });

        function GetClientes() {
            
            var urlData = 'FacturaClienteListado.aspx/GetList';
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
            if ($.fn.DataTable.isDataTable('#table_clientes')) {
                $('#table_clientes').dataTable().fnDestroy();
            }
            var data = JSON.parse(data);
            var table = $('#table_clientes').DataTable({
                searching: true,
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
                      { className: "text-center", "targets": [0, 1, 3,4,5,6] },
                ],
                columns: [

                    {
                        data: "intCliente", title: "#",
                    },
                    { data: "strCodigo", title: "Codigo" },
                    { data: "strNombre", title: "Nombre" },
                    
                    { data: "strRFC", title: "RFC" },

                    { data: "strEstatus", title: "Estatus" },
                    { data: "datFechaAlta", title: "Fecha Alta" },
                    {
                        data: null,
                        bSortable: false,
                        render: function (o) {

                            return '<a href="FacturaCliente.aspx?id=' + o.intCliente + '"><span class="glyphicon glyphicon-search"></span></a>';
                        }
                    }
                ],
                "createdRow": function (row, data, dataIndex) {

                }
            });
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <h2>Clientes</h2>
    <hr />
    <div>
        <a href="FacturaCliente.aspx" class="btn btn-primary">
            <span>Alta Cliente</span>
        </a>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12 col-lg-12">
            <table id="table_clientes" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">
                
            </table>
        </div>
    </div>
</asp:Content>

