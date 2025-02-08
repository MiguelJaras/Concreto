<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="NotaCreditoGenerar.aspx.cs" Inherits="Pages_Admin_NotaCreditoGenerar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript">

        var interval;
        var intNotaCredito = 0;

        $(document).ready(function () {
            intNotaCredito = getParameterByName("id");
            if (intNotaCredito == '' || intNotaCredito == null)
                intNotaCredito = 0;

            if (intNotaCredito > 0) {
                GetNotaCredito();
            }
        });

        function GetNotaCredito() {
            var urlData = 'NotaCreditoGenerar.aspx/Get';
            var dataData = '{ intNotaCredito: ' + intNotaCredito + '}';
            CallMethod(urlData, dataData, SuccessGet);
        }

        function SuccessGet(response) {
            var message = response.d[0];
            if (message == "ok") {

                var data = JSON.parse(response.d[1]);
                if (data.length > 0) {
                    $('#ctl00_BodyContent_ddlCliente').val(data[0].strCliente);
                    $('#txtImporte').val(data[0].decImporte);
                    $('#txtReferencia').val(data[0].strReferencia);
                    $('#txtSerieFactura').val(data[0].strSerieFactura);
                    $('#txtFolioFactura').val(data[0].decFolioFactura);
                    $('#lblSerie').text(data[0].strSerie);
                    $('#lblFolio').text(data[0].decFolio);
                    $('#lblEstatus').text(data[0].strEstatus);
                    $('#lblError').text(data[0].strError);
                    $('#ddlFormaPago').val(data[0].strFormaPago);
                    $('#ddlMetodoPago').val(data[0].strMetodopago);


                    if (data[0].intEstatus == 1 || data[0].intEstatus == 4) {
                        $('#btnProcesar').removeClass('hide');
                    } else {
                        $('#btnProcesar').addClass('hide');
                    }

                    if (data[0].intEstatus == 1 || data[0].intEstatus == 4) {
                        $('#btnGuardar').removeClass('hide');
                    } else {
                        $('#btnGuardar').addClass('hide');
                    }

                    //si no esta en proceso se detiene el timer
                    if (data[0].intEstatus != 2 && data[0].intEstatus != 5) {
                        if (interval) {
                            clearInterval(interval);
                            interval = null;
                        }
                    }
                }

            } else {
                alert('Error al cargar los datos')
            }
        }


        function GenerarNotaCredito()
        {
            try {
                
                var strCliente = $('#ctl00_BodyContent_ddlCliente').val();
                var decImporte = $('#txtImporte').val();
                var strReferencia = $('#txtReferencia').val();
                var strSerieFactura = $('#txtSerieFactura').val();
                var decFolioFactura = $('#txtFolioFactura').val();

                var strFormaPago = $('#ddlFormaPago').val();
                var strMetodoPago = $('#ddlMetodoPago').val();

                var ent = {
                    strCliente: strCliente,
                    decImporte: decImporte,
                    strReferencia: strReferencia,
                    strSerieFactura: strSerieFactura,
                    decFolioFactura: decFolioFactura,
                    strFormaPago: strFormaPago,
                    strMetodoPago: strMetodoPago
                }


                var url = 'NotaCreditoGenerar.aspx/GenerarNotaCredito';
                var data = '{';
                data += 'ent:' + JSON.stringify(ent)
                data += '}';

                CallMethod(url, data, SuccessGenerarNotaCredito);
            }
            catch (e) {
                alert(e);
            }
        }


        function SuccessGenerarNotaCredito(response) {
            var message = response.d[0];
            if (message == "ok") {
                alert('Datos guardados correctamente.');
                var data = response.d[1];
                window.location = 'NotaCreditoGenerar.aspx?id=' + data;
            }
            else {
                alert('Error al generar la nota de crédito.')
            }
        }


        function Procesar() {
            var url = 'NotaCreditoGenerar.aspx/Procesar';
            var data = '{';
            data += 'intNotaCredito:' + intNotaCredito
            data += '}';

            CallMethod(url, data, SuccessProcesar);
        }

        function SuccessProcesar(response) {
            var message = response.d[0];
            if (message == "ok") {

                alert('Datos guardados correctamente.');
                GetNotaCredito();

                interval = setInterval(function () {
                    GetNotaCredito();
                }, 15000);

            }
            else {
                alert('Error al procesar el cliente.')
            }
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <h2>Generar Nota de Crédito</h2>
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
                <label class="col-md-2 control-label">Serie Factura</label>
                <div class="col-md-4">
                    <input type="text" class="form-control" id="txtSerieFactura" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">Folio Factura</label>
                <div class="col-md-4">
                    <input type="text" class="form-control" id="txtFolioFactura" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">Importe</label>
                <div class="col-md-4">
                    <input type="text" class="form-control" id="txtImporte" onkeypress="return KeyPressOnlyDecimal(event, this, 10, 4)"/>
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
                <label class="col-md-2 control-label">Método de Pago</label>
                <div class="col-md-2">
                    <select id="ddlMetodoPago" class="btn dropdown-toggle btn-default">
                        <option value=""></option>
                        <option value="PUE">(PUE) Pago en una Sola Exhibición</option>
                        <option value="PPD">(PPD) Pago en Parcialidades o Diferido</option>

                    </select>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">Observaciones</label>
                <div class="col-md-4">
                    <input type="text" class="form-control" id="txtReferencia" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">Estatus</label>
                <div class="col-md-4">
                    <label id="lblEstatus" class="form-control"></label>
                </div>
                <div class="col-md-4">
                    <label id="lblError" class="form-control danger"></label>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">Folio</label>
                <div class="col-md-1">
                    <label id="lblSerie" class="form-control"></label>
                </div>
                <div class="col-md-2">
                    <label id="lblFolio" class="form-control"></label>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label"></label>
                <div class="col-md-4">
                    <input id="btnGuardar" type="button" class="btn btn-primary" value="Generar Nota" onclick="GenerarNotaCredito();" />
                    <input id="btnProcesar" type="button" class="btn btn-primary hide" value="Procesar" onclick="Procesar();" />
                    <input id="btnRegresar" type="button" class="btn btn-primary" value="Regresar al listado" onclick="window.location='NotaCreditoGenerarListado.aspx'" />
                </div>
            </div>


        </div>
    </div>
</asp:Content>

