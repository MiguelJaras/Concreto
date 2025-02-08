<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="PlantaCargaRemisiones.aspx.cs" Inherits="Pages_Admin_PlantaCargaRemisiones" %>
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
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">

     <h2>Carga Remisiones</h2>
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
            <div class="col-md-2"><input type="button" onclick="GetReporteMensual();" value="Reporte Mensual" class="btn btn-primary btn-block" /></div>
            
        </div>
        
    </div>
    <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <table id="table_ordenes" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">
                
                </table>
            </div>
        </div>
 <script>
     $(document).ready(function () {

         $("#ctl00_BodyContent_fileExcel").fileinput({
             'showRemove': false,
             'showUpload': false,
             'showPreview': false,
             'allowedFileExtensions': ["xls", "xlsx"],
             'required': true,
             'language': "es",
         });

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

         var urlData = 'PlantaCargaRemisiones.aspx/GetList';
         var dataData = '{ strFechaInicio:\'' + strFechaInicio + '\', strFechaFin: \'' + strFechaFin + '\'}';
         CallMethod(urlData, dataData, SuccessGetListOrdenes);
     }

     function GetReporteMensual() {
         var strFecha = $('#ctl00_BodyContent_txtFechaInicio').val();

         var urlData = 'PlantaCargaRemisiones.aspx/ReporteMensual';
         var dataData = '{ strFecha:\'' + strFecha + '\'}';
         CallMethod(urlData, dataData, SuccessReporteMensual);
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

     function SuccessReporteMensual(response) {
         var message = response.d[0];
         var data = response.d[1];
         if (message == "ok") {
             ShowModalPDF(data);
         } else {
             alert('Error al generar el reporte')
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
             language: {
                 url: "../../Scripts/dataTables/js/Spanish.js"
             },
             order: [],
             data: data,
             columnDefs: [
                 { className: "text-center", "targets": [0, 1, 2, 3, 4, 5] },
             ],
             columns: [
                 { data: "intPlanta", title: "Planta" },
                 { data: "strRemision", title: "Remisión" },
                 { data: "decCantidad", title: "Cantidad" },
                 { data: "strStatus", title: "Estatus" },
                 {
                     data: "datFecha", title: "Fecha Carga",
                     "render": function (data, type, object, row) {
                         data = new Date(data);
                         data = [data.getDate(), data.getMonth() + 1,
                         data.getFullYear()].join('/')
                         return data;
                     }
                 },
                 { data: "intFolioOC", title: "Orden de Compra" },             
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
                 },
                 {
                     extend: "copyHtml5",
                     text: '<img src="../../Img/dcpy.png" height="30" width="30" />',
                 },
             ],
             dom: "Bfrtip",
         });
     }

     function ShowModalPDF(fileName) {

         var htmlContent = "<div id=detallePedido class=white-popup><object data=\"{FileName}\" type=\"application/pdf\" width=\"100%\" height=\"100%\">";
         htmlContent += "If you are unable to view file, you can download from <a href = \"{FileName}\">here</a>";
         htmlContent += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
         htmlContent += "</div></object>";
         htmlContent = htmlContent.replace(/{FileName}/g, "../../Temp/" + fileName);

         $.magnificPopup.open({
             items: [
                 {
                     type: 'inline',
                     src: $(htmlContent)
                 }
             ],
         });

     }
 </script>
</asp:Content>
