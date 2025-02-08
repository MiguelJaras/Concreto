<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="FacturaGenerar.aspx.cs" Inherits="Pages_Admin_FacturaGenerar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript">
        
        $(document).ready(function () {
            GetPedidos();

            $("#ctl00_BodyContent_ddlCliente").on("change", function (event) {
                GetPedidos();
            });

        });

        function GetPedidos() {
            var strCliente = $('#ctl00_BodyContent_ddlCliente').val();
            var urlData = 'FacturaGenerar.aspx/GetList';
            var dataData = '{ strCliente:"' + strCliente + '"}';
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
                      { className: "text-center", "targets": [0, 1, 3, 4, 5, 6] },
                      {
                          'targets': 0,
                          'searchable': false,
                          'orderable': false,
                          'className': 'dt-body-center',
                          'render': function (data, type, full, meta) {
                              return '<input type="checkbox" name="id[]" value="'
                                 + data + '">';
                          }
                      },
                ],
                columns: [

                    {
                        data: "intPedido", title: "#",
                    },
                    {
                        data: "intPedido"
                        , title: "Remisión"
                        , render: function (data, type, full, meta) {
                            return '<a href="#" onclick="PreviewPedido(' + data + ')">' + data + '</a>';
                        }

                    },
                    { data: "datFechaEntrega", title: "Fecha Entrega" },
                    { data: "strCliente", title: "Cliente" },
                    { data: "City", title: "Ciudad" },
                    { data: "Estatus", title: "Estatus" },
                    { data: "datFechaAlta", title: "Fecha Alta" },

                ],
                "createdRow": function (row, data, dataIndex) {

                }
            });
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

        function GenerarFactura() {
            try {
                var strValues = GetValuesPedidos();
                if (strValues == '')
                {
                    alert('Favor de seleccionar un pedido');
                    return;
                }

                var strCliente = $('#ctl00_BodyContent_ddlCliente').val();
                var strConcepto = $('#txtConcepto').val();
                var strUsoCFDI = $('#ddlUsoCFDI').val();
                var strFormaPago = $('#ddlFormaPago').val();
                var strMetodoPago = $('#ddlMetodoPago').val();
                var decDescuento = $('#txtDescuento').val();
                
                if (decDescuento == '')
                    decDescuento = 0;

                var ent = {
                    strPedidos: strValues,
                    strCliente: strCliente,
                    strConcepto: strConcepto,
                    strUsoCFDI: strUsoCFDI,
                    strFormaPago: strFormaPago,
                    strMetodoPago: strMetodoPago,
                    decDescuento: decDescuento
                }


                var url = 'FacturaGenerar.aspx/GenerarFactura';
                var data = '{';
                data += 'ent:' + JSON.stringify(ent)
                data += '}';

                CallMethod(url, data, SuccessGenerarFactura);
            }
            catch (e) {
                alert(e);
            }
    
        }

        function SuccessGenerarFactura(response) {
            var message = response.d[0];
            if (message == "ok") {
                alert('Datos guardados correctamente.');
                var data = response.d[1];
                window.location = 'FacturaGenerarDetalle.aspx?id=' + data;
            }
            else {
                alert('Error al generar las facturas.')
            }
        }

        function PreviewPedido(intPedido) {
            if (intPedido != '') {
                var urlData = 'FacturaGenerar.aspx/PreviewPedido';
                var dataData = '{ intPedido:"' + intPedido + '" }';
                CallMethod(urlData, dataData, SuccessPreviewPedido);
            }
        }

        function SuccessPreviewPedido(response) {
            var message = response.d[0];
            var data = response.d[1];
            if (message == "ok") {
                ShowModalPDF(data);
            } else {

            }
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
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <h2>Generar Factura</h2>
    <hr />
    <!-- DATOS GENERALES -->
    <div class="panel panel-primary">
        <div class="panel-heading">Datos Generales</div>
        <div class="panel-body form-horizontal">
            <div class="form-group">
                <label class="col-md-2 control-label">Cliente</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlCliente" CssClass="btn dropdown-toggle btn-default">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">Concepto</label>
                <div class="col-md-4">
                    <input type="text" class="form-control" id="txtConcepto" />
                </div>
            </div>


            <div class="form-group">
                <label class="col-md-2 control-label">Uso CFDI</label>
                <div class="col-md-4">
                    <select id="ddlUsoCFDI" class="btn dropdown-toggle btn-default">
                        <option value=""></option>
                        <option value="G01">(G01) Adquisición de mercancias</option>
                        <option value="G02">(G02) Devoluciones, descuentos o bonificaciones</option>
                        <option value="G03">(G03) Gastos en general</option>
                        <option value="I01">(I01) Construcciones</option>
                        <option value="I02">(I02) Mobilario y equipo de oficina por inversiones</option>
                        <option value="I03">(I03) Equipo de transporte</option>
                        <option value="I04">(I04) Equipo de computo y accesorios</option>
                        <option value="I05">(I05) Dados, troqueles, moldes, matrices y herramental</option>
                        <option value="I06">(I06) Comunicaciones telefónicas</option>
                        <option value="I07">(I07) Comunicaciones satelitales</option>
                        <option value="I08">(I08) Otra maquinaria y equipo</option>
                        <option value="D01">(D01) Honorarios médicos, dentales y gastos hospitalarios.</option>
                        <option value="D02">(D02) Gastos médicos por incapacidad o discapacidad</option>
                        <option value="D03">(D03) Gastos funerales.</option>
                        <option value="D04">(D04) Donativos.</option>
                        <option value="D05">(D05) Intereses reales efectivamente pagados por créditos hipotecarios (casa habitación).</option>
                        <option value="D06">(D06) Aportaciones voluntarias al SAR.</option>
                        <option value="D07">(D07) Primas por seguros de gastos médicos.</option>
                        <option value="D08">(D08) Gastos de transportación escolar obligatoria.</option>
                        <option value="D09">(D09) Depósitos en cuentas para el ahorro, primas que tengan como base planes de pensiones.</option>
                        <option value="D10">(D10) Pagos por servicios educativos (colegiaturas)</option>
                        <option value="P01">(P01) Por definir</option>
                    </select>
                </div>
            </div>

            
            <div class="form-group">
                <label class="col-md-2 control-label">Forma de Pago</label>
                <div class="col-md-2">
                    <select id="ddlFormaPago" class="btn dropdown-toggle btn-default">
                        <option value=""></option>
                        <option value="01">(01) Efectivo</option>
                        <option value="02">(02) Cheque nominativo</option>
                        <option value="03">(03) Transferencia electrónica de fondos</option>
                        <option value="04">(04) Tarjeta de crédito</option>
                        <option value="05">(05) Monedero electrónico</option>
                        <option value="06">(06) Dinero electrónico</option>
                        <option value="08">(08) Vales de despensa</option>
                        <option value="12">(12) Dación en pago</option>
                        <option value="13">(13) Pago por subrogación</option>
                        <option value="14">(14) Pago por consignación</option>
                        <option value="15">(15) Condonación</option>
                        <option value="17">(17) Compensación</option>
                        <option value="23">(23) Novación</option>
                        <option value="24">(24) Confusión</option>
                        <option value="25">(25) Remisión de deuda</option>
                        <option value="26">(26) Prescripción o caducidad</option>
                        <option value="27">(27) A satisfacción del acreedor</option>
                        <option value="28">(28) Tarjeta de débito</option>
                        <option value="29">(29) Tarjeta de servicios</option>
                        <option value="30">(30) Aplicación de anticipos</option>
                        <option value="31">(31) Intermediario pagos</option>
                        <option value="99">(99) Por definir</option>

                    </select>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">Metodo de Pago</label>
                <div class="col-md-2">
                    <select id="ddlMetodoPago" class="btn dropdown-toggle btn-default">
                        <option value=""></option>
                        <option value="PUE">(PUE) Pago en una Sola Exhibición</option>
                        <option value="PPD">(PPD) Pago en Parcialidades o Diferido</option>

                    </select>
                </div>
            </div>


            <div class="form-group">
                <label class="col-md-2 control-label">% Descuento</label>
                <div class="col-md-2">
                    <input type="text" class="form-control" id="txtDescuento"  onkeypress="return KeyPressOnlyDecimal(event, this, 10, 4)"/>
                </div>
            </div>


            <div class="form-group">
                <label class="col-md-2 control-label"></label>
                <div class="col-md-4">
                    <a href="JavaScript:GenerarFactura()" class="btn btn-primary">Generar Factura</a>
                    <a href="JavaScript:location='FacturaGenerarListado.aspx'" class="btn btn-primary">Regresar al listado</a>
                </div>
                
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12 col-lg-12">
            <table id="table_pedidos" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">
                
            </table>
        </div>
    </div>


</asp:Content>

