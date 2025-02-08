<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="Factura.aspx.cs" Inherits="Pages_Admin_Factura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../../Scripts/file/fileinput.css" rel="stylesheet" />
    <script src="../../Scripts/file/fileinput.js"></script>
    <script src="../../Scripts/file/es.js"></script>
    
    <script type="text/javascript">

        $(document).ready(function () {

            $("#ctl00_BodyContent_fileFacturaPDF, #ctl00_BodyContent_fileRemision").fileinput({
                'showRemove': false,
                'showUpload': false,
                'showPreview': false,
                'allowedFileExtensions': ["pdf"],
                'required': true,
                'language': "es",
            });

            $("#ctl00_BodyContent_fileFacturaXML").fileinput({
                'showRemove': false,
                'showUpload': false,
                'showPreview': false,
                'allowedFileExtensions': ["xml"],
                'required': true,
                'language': "es",
            });
            $('.fileUpload').on('fileselect', function (event, numFiles, label) {
                Upload(this.id);
            });

            $('.precio').priceFormat({
                prefix: '$ ',
                centsSeparator: '.',
                thousandsSeparator: ','
            });

            
            $('#ctl00_BodyContent_txtFechaInicio').datepicker({
                dateFormat: 'dd/mm/yy',
            });

            $('#ctl00_BodyContent_txtFechaFin').datepicker({
                dateFormat: 'dd/mm/yy',
            });
            BuscarPedidos();
        });

        function StorePedidos()
        {
            $('#ctl00_BodyContent_hdnPedidos').val(GetValuesPedidos());
        }

        function BuscarPedidos() {

            
            var strFechaInicio = $('#ctl00_BodyContent_txtFechaInicio').val();;
            var strFechaFin = $('#ctl00_BodyContent_txtFechaFin').val();;
            var urlData = 'Factura.aspx/GetList';
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
            var data = JSON.parse(data);


            if ($.fn.DataTable.isDataTable('#table_pedidos')) {
                var datatable = $('#table_pedidos').DataTable();
                datatable.clear();
                datatable.rows.add(data);
                datatable.draw();
            }
            else {
                $('#table_pedidos').DataTable({
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
                          { className: "text-center", "targets": [0, 1, 2, 6] },
                          { className: "text-right", "targets": [7] },
                          { width: "10%", "targets": 3 }
                    ],
                    columns: [
                        {
                            data: null,
                            bSortable: false,
                            render: function (o) {
                                var options = '<input type="checkbox" name="chkPedido" id="chkPedido" onchange="StorePedidos();" value="' + o.intPedido + '">';
                                return options;
                            }
                        },
                        { data: "intPedido", title: "#" },
                        { data: "PO_Num", title: "OC" },
                        { data: "strCliente", title: "Cliente" },
                        { data: "City", title: "Ciudad" },
                        { data: "Estatus", title: "Estatus" },
                        { data: "datFechaAlta", title: "Fecha Alta" },
                        { data: "dblTotal", title: "Total", render: $.fn.dataTable.render.number(',', '.', 2, '$ ') },

                    ],
                    "initComplete": function () {
                        $('#table_pedidos_info').html(
                            ''
                        );
                        CheckPedidos();
                    }
                });
            }
        }

        function Upload(Id)
        {
            switch (Id)
            {
                case 'ctl00_BodyContent_fileFacturaPDF':
                    __doPostBack('FacturaPDF', '');
                    break;
                case 'ctl00_BodyContent_fileFacturaXML':
                    __doPostBack('FacturaXML', '');
                    break;
                case 'ctl00_BodyContent_fileRemision':
                    __doPostBack('Remision', '');
                    break;
                default:
                    break;
            }
        }
        
        function GetValuesPedidos() {
            var values = new Array();
            var dataTable = $('#table_pedidos').DataTable();
            dataTable.rows().nodes().to$().find('input[type="checkbox"]').each(function () {
                if (this.checked) {
                    values.push($(this).val());
                }
            });

            var strValues = values.join(",");
            return strValues;
        }

        function CheckPedidos() {
            var dataTable = $('#table_pedidos').DataTable();
            var pedidos = $('#ctl00_BodyContent_hdnPedidos').val();
            if (pedidos != '') {
                var arrPedidos = pedidos.split(',');
                for (var i = 0; i <= arrPedidos.length; i++) {
                    dataTable.rows().nodes().to$().find('input[value="' + arrPedidos[i] + '"]').prop("checked", true);
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <asp:HiddenField runat="server" ID="hdnPedidos" />
    <%--<input type="button" value="GetValuesPedidos" onclick="alert(GetValuesPedidos());" />--%>
    <%--<asp:Button runat="server" ID="btn" Text="Enviar" OnClick="txt_Click" />--%>
    <h2>Alta de Facturas</h2>
    <hr />
     
    <div class="row form-horizontal">
        <div class="form-group">
            <label class="col-md-2 control-label">Factura (PDF)</label>
            <div class="col-md-6">
                <asp:FileUpload runat="server" ID="fileFacturaPDF" CssClass="fileUpload" />
                <asp:Label runat="server" ID="lblFacturaPDF"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-2 control-label">Factura (XML)</label>
            <div class="col-md-6">
                <asp:FileUpload runat="server" ID="fileFacturaXML" CssClass="fileUpload" />
                <asp:Label runat="server" ID="lblFacturaXML"></asp:Label>
            </div>
        </div>
        <div class="form-group hide">
            <label class="col-md-2 control-label">Remisión</label>
            <div class="col-md-6">
                <asp:FileUpload runat="server" ID="fileRemision" CssClass="fileUpload" />
                <asp:Label runat="server" ID="lblFacturaRemision"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-2 control-label">Factura</label>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtFactura" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-2 control-label">SubTotal</label>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtSubtotal" CssClass="form-control precio"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-2 control-label">Iva</label>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtIva" CssClass="form-control precio"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-2 control-label">Total</label>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtTotal" CssClass="form-control precio"></asp:TextBox>
            </div>
        </div>
    </div>

    <div class="row form-horizontal">
        <div class="form-group ">
            <label class="col-md-2 control-label">Fecha Inicio</label>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtFechaInicio" ReadOnly="true" CssClass="form-control"></asp:TextBox>
            </div>
            <label class="col-md-1 control-label">Fecha Fin</label>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtFechaFin" ReadOnly="true" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2"><input type="button" onclick="$('#ctl00_BodyContent_hdnPedidos').val(''); BuscarPedidos();" value="Filtrar" class="btn btn-primary btn-block" /></div>
            <div class="col-md-2"><asp:LinkButton runat="server" ID="btnGuardar" CssClass="btn btn-primary btn-block" Text="Guardar" OnClick="btnGuardar_Click"></asp:LinkButton></div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <table id="table_pedidos" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">
                
                </table>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-2">
            
        </div>
    </div>
</asp:Content>

