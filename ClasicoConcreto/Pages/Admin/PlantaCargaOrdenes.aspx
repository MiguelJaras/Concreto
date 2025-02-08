<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="PlantaCargaOrdenes.aspx.cs" Inherits="Pages_Admin_PlantaCargaOrdenes" %>
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

    
    <script>

        $(document).ready(function () {

            $("#ctl00_BodyContent_fileExcel").fileinput({
                'showRemove': false,
                'showUpload': false,
                'showPreview': false,
                'allowedFileExtensions': ["xls"],
                'required': true,
                'language': "es",
            });


           
            $('#ctl00_BodyContent_txtFechaDiario').datepicker({
                dateFormat: 'dd/mm/yy',
                onSelect: function (dateText, inst) {                
                  
                    $("#ctl00_BodyContent_hddFechDiaria").val(dateText);
                                   
                }


            });

            $('#ctl00_BodyContent_txtFechaInicio').datepicker({
                dateFormat: 'dd/mm/yy',
            });

            $('#ctl00_BodyContent_txtFechaFin').datepicker({
                dateFormat: 'dd/mm/yy',
            });
            GetListOrdenes();
        });



        function Exportar() {
          
            var strDia = $('#ctl00_BodyContent_hddFechDiaria').val();
            alert(strDia);
            ShowPDF(strDia);

        }

        function ShowPDF(strDia) {
            var urlData = 'PlantaCargaOrdenes.aspx/ShowReport';
            var dataData = '{ datFechaDia:\'' + strDia + '\'}';
            CallMethod(urlData, dataData, SuccessShowPDF);
        }


        function SuccessShowPDF(response) {
            var message = response.d[0];
            var data = response.d[1];
            if (message == "ok") {
                ShowModalPDF(data);
            } else {

            }
        }


        function ShowModalPDF(fileName) {

            var htmlContent = "<div id=detalle class=white-popup><object data=\"{FileName}\" type=\"application/pdf\" width=\"100%\" height=\"100%\">";
            htmlContent += "If you are unable to view file, you can download from <a href = \"{FileName}\">here</a>";
            htmlContent += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            htmlContent += "</div></object>";
            htmlContent = htmlContent.replace(/{FileName}/g, "../../Temp/ReporteRemisionDiarioCmdBatch/" + fileName);

            $.magnificPopup.open({
                items: [
                    {
                        type: 'inline',
                        src: $(htmlContent)
                    }
                ],
            });

        }

        function GetListOrdenes() {
            var strFechaInicio = $('#ctl00_BodyContent_txtFechaInicio').val();
            var strFechaFin = $('#ctl00_BodyContent_txtFechaFin').val();

            var urlData = 'PlantaCargaOrdenes.aspx/GetList';
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
                      { className: "text-center", "targets": [0, 1, 2, 4, 5, 6] },
                ],

                columns: [
                    { data: "intPlanta", title: "Planta" },
                    { data: "intPlantaOrden", title: "Orden Planta" },
                    { data: "intFolioOC", title: "Orden de Compra" },
                    { data: "strRemision", title: "Remisión" },
                    {
                        data: "datFecha", title: "Fecha Carga",
                        "render": function (data, type, object, row) {
                            data = new Date(data);
                            data = [data.getDate(), data.getMonth() + 1,
                            data.getFullYear()].join('/')
                                  
                            return data;
                        }
                    },
                    { data: "decCarga", title: "Carga" },
                    {
                        data: "datFechaAlta", title: "Fecha de Alta",
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
                            columns: [0,1,2,3]
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
        function mostrarModalEspera() {
            // Mostrar el modal
            document.getElementById("modal-espera").style.display = "flex";
        }

        function ocultarModalEspera() {
            // Ocultar el modal
            document.getElementById("modal-espera").style.display = "none";
        }

    </script>
    <style>
        .btn-group {
          position:relative;
          vertical-align:middle;
          display:block;
        }
        .modal-overlay {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background: rgba(0, 0, 0, 0.5);
        display: flex;
        align-items: center;
        justify-content: center;
        z-index: 9999;
        }

        .modal-content {
        background: white;
        padding: 20px;
        border-radius: 8px;
        font-size: 18px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    
    <h2>Carga Command Batch</h2>
    <hr />
      <asp:HiddenField ID="hddFechDiaria" runat="server"></asp:HiddenField>


    <div class="row form-horizontal">

        <div class="form-group">
            
            <label class="col-md-2 control-label">Archivo</label>
            <div class="col-md-6">
                <Anthem:FileUpload runat="server" ID="fileExcel" CssClass="fileUpload" />
                <asp:Label runat="server" ID="lblExcel"></asp:Label>
            </div>

            <div class="col-md-2">
                <asp:LinkButton runat="server" ID="btnGuardar" CssClass="btn btn-primary btn-block" Text="Guardar" OnClick="btnGuardar_Click" OnClientClick="mostrarModalEspera();"  EnableCallBack="true" >

                </asp:LinkButton>
               <%-- <button onclick="Exportar();">PDF TEST</button>--%>
            </div>       

        </div>
        
    </div>

          <div class="row form-horizontal">
            <div class="form-group">
                           <label class="col-md-2 control-label">Día</label>
                 <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtFechaDiario" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                </div>
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
    <div id="modal-espera" class="modal-overlay" style="display: none;">
        <div class="modal-content">
            <p>Procesando, por favor espera...</p>
           <%-- <img src="../../Img/loading2.gif" alt="Cargando..." style="width: 50px; height: 50px; margin-bottom: 10px;"/>--%>
        </div>
    </div>
</asp:Content>

