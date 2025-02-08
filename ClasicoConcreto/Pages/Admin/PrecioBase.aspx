<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="PrecioBase.aspx.cs" Inherits="Pages_Admin_PrecioBase" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

     <link href="../../Scripts/file/fileinput.css" rel="stylesheet" />
    <script src="../../Scripts/file/fileinput.js"></script>
    <script src="../../Scripts/file/es.js"></script>
    
    <script src="../../Scripts/datatables/JSZip-2.5.0/jszip.min.js"></script>
    <script src="../../Scripts/datatables/pdfmake-0.1.36/pdfmake.min.js"></script>
    <script src="../../Scripts/datatables/pdfmake-0.1.36/vfs_fonts.js"></script>
    <script src="../../Scripts/datatables/Buttons-1.5.6/js/dataTables.buttons.min.js"></script>
    <script src="../../Scripts/datatables/Buttons-1.5.6/js/buttons.bootstrap.min.js"></script>
    <script src="../../Scripts/datatables/Buttons-1.5.6/js/buttons.html5.min.js"></script>
    <script src="../../Scripts/datatables/Buttons-1.5.6/js/buttons.print.min.js"></script>
    <script src="../../Scripts/datatables/Buttons-1.5.6/js/buttons.flash.min.js"></script>
    
    <script>
        $(document).ready(function () {

            $("#ctl00_BodyContent_fileExcel").fileinput({
                'showRemove': false,
                'showUpload': false,
                'showPreview': false,
                'allowedFileExtensions': ["xls","xlsx"],
                'required': true,
                'language': "es",
            });


            //$('.fileUpload').on('fileselect', function (event, numFiles, label) {
            //    Upload(this.id);
            //});


            $('#ctl00_BodyContent_txtFechaInicio').datepicker({
                dateFormat: 'dd/mm/yy',
            });

            $('#ctl00_BodyContent_txtFechaFin').datepicker({
                dateFormat: 'dd/mm/yy',
            });
            GetListOrdenes();
        });

        function GetListOrdenes() {
            var strFechaInicio = $('#ctl00_BodyContent_txtFechaInicio').val();
            var strFechaFin = $('#ctl00_BodyContent_txtFechaFin').val();

            var urlData = 'PlantaCargaExterna.aspx/GetList';
            var dataData = '{ strFechaInicio:\'' + strFechaInicio + '\', strFechaFin: \'' + strFechaFin + '\'}';
            CallMethod(urlData, dataData, SuccessGetListOrdenes);
        }

        function SuccessGetListOrdenes(response) {
            var message = response.d[0];
            var data = response.d[1];
            if (message == "ok") {
                CreateTable(data);
            } else {
                alert('Error al cargar los datos')
            }
        }

        function CreateTable(data) {
            if ($.fn.DataTable.isDataTable('#table_ordenes')) {
                $('#table_ordenes').dataTable().fnDestroy();
            }
            var data = JSON.parse(data);
            var table = $('#table_ordenes').DataTable({
                autoWidth: true,
                searching: true,
                bLengthChange: false,
                paging: true,
                pageLength: 20,
                responsive: true,
                //orderCellsTop: true,
                language: {
                    url: "../../Scripts/dataTables/js/Spanish.js"
                },
                order: [],
                data: data,
                columnDefs: [
                    { className: "text-center", "targets": [0, 1, 2, 3, 4, 5, 6] },
                ],

                columns: [
                    { data: "intPlanta", title: "Planta" },
                    { data: "strRemision", title: "Remisión" },
                    { data: "strCliente", title: "Cliente" },
                    {
                        data: "datFecha", title: "Fecha Carga",
                        "render": function (data, type, object, row) {
                            data = new Date(data);
                            data = [data.getDate(), data.getMonth() + 1,
                            data.getFullYear()].join('/')
                            return data;
                        }
                    },
                    { data: "decCantidad", title: "Cantidad" },
                    { data: "decBombeable", title: "Bomb." },
                    //{ data: "decPorcIva", title: "% Iva" },
                    //{ data: "decSubtotal", title: "Subtotal", render: $.fn.dataTable.render.number(',', '.', 2, '$ ') },
                    //{ data: "decIva", title: "Iva", render: $.fn.dataTable.render.number(',', '.', 2, '$ ') },
                    //{ data: "decTotal", title: "Total", render: $.fn.dataTable.render.number(',', '.', 2, '$ ') },
                    {
                        data: "datFechaAlta", title: "Fecha Alta",
                        "render": function (data, type, object, row) {
                            data = new Date(data);
                            data = [data.getDate(), data.getMonth() + 1,
                            data.getFullYear()].join('/')

                            return data;
                        }
                    },
                ],
                buttons: [
                    {
                        extend: "excelHtml5",
                        text: '<img src="../../Img/dxls.png" height="30" width="30"/>',
                        exportOptions: {
                            columns: [0, 1, 2, 3]
                        }
                    },
                    {
                        extend: "pdfHtml5",
                        text: '<img src="../../Img/dpdf.png" height="30" width="30"/>',
                        //exportOptions: {
                        //    columns: [0]
                        //}
                    },
                    {
                        extend: "copyHtml5",
                        text: '<img src="../../Img/dcpy.png" height="30" width="30" />',
                    },
                ],
                dom: "Bfrtip",

            });
        }


    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">

     <h2>Carga Venta Externa</h2>
    <hr />
     
    <div class="row form-horizontal">
        <div class="form-group">
            <label class="col-md-2 control-label">Archivo</label>
            <div class="col-md-6">
                <Anthem:FileUpload runat="server" ID="fileExcel" CssClass="fileUpload" />
                <asp:Label runat="server" ID="lblExcel"></asp:Label>
            </div>
            <div class="col-md-2"><asp:LinkButton runat="server" ID="btnGuardar" CssClass="btn btn-primary btn-block" Text="Guardar" OnClick="btnGuardar_Click"></asp:LinkButton></div>
        </div>
        
    </div>

    <div class="row form-horizontal">
        <div class="form-group ">
            <label class="col-md-2 control-label">Fecha Inicio</label>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtFechaInicio" ReadOnly="true" CssClass="form-control"></asp:TextBox>
            </div>
            <label class="col-md-2 control-label">Fecha Fin</label>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtFechaFin" ReadOnly="true" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2"><input type="button" onclick="GetListOrdenes();" value="Filtrar" class="btn btn-primary btn-block" /></div>
            
        </div>
        
    </div>
    <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <table id="table_ordenes" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">
                
                </table>
            </div>
        </div>

</asp:Content>

