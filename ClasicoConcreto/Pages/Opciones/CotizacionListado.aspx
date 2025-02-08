<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="CotizacionListado.aspx.cs" Inherits="Pages_Opciones_CotizacionListado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script>
        $(document).ready(function () {

            $('#ctl00_BodyContent_txtFechaInicio').datepicker({
                dateFormat: 'dd/mm/yy',
            });

            $('#ctl00_BodyContent_txtFechaFin').datepicker({
                dateFormat: 'dd/mm/yy',
            });
            GetListado();
        });


        function GetListado() {
            
            var strFechaInicio = $('#ctl00_BodyContent_txtFechaInicio').val();
            var strFechaFin = $('#ctl00_BodyContent_txtFechaFin').val();
            var urlData = 'CotizacionListado.aspx/GetList';
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
            if ($.fn.DataTable.isDataTable('#table_cotizacion')) {
                $('#table_cotizacion').dataTable().fnDestroy();
            }
            var data = JSON.parse(data);
            var table = $('#table_cotizacion').DataTable({
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
                      { className: "text-center", "targets": [0, 1, 2, 4, 5] },
                ],
                columns: [

                    { data: "intCotizacion", title: "#" },
                    { data: "strCliente", title: "Cliente" },
                    { data: "datFechaColado", title: "Fecha de Colado" },
                    { data: "strObra", title: "Obra" },
                    { data: "datFechaAlta", title: "Fecha Alta" },
                    
                    {
                        data: null,
                        bSortable: false,
                        render: function (o) {
                            var options = '';
                            options += '<a href=Cotizacion.aspx?id=' + o.intCotizacion + '><span class="glyphicon glyphicon-pencil"></span></a>';
                            return options;
                        }
                    }
                ],

            });

        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <h2>Listado de Cotizaciones</h2>
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
                    <input type="button" class="btn btn-primary btn-block" value="Nuevo" onclick="window.location = 'Cotizacion.aspx'" />
                </div>
            </div>
        </div>
    </div>



    <div class="row">
        <div class="col-lg-12">
            <table id="table_cotizacion" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">
                
            </table>
        </div>
    </div>
</asp:Content>

