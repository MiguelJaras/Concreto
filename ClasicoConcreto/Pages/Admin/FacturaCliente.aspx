<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="FacturaCliente.aspx.cs" Inherits="Pages_Admin_FacturaCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script>
        var interval;
        var intCliente;
        $(document).ready(function () {
            intCliente = getParameterByName("id");
            if (intCliente == '' || intCliente == null)
                intCliente = 0;

            if (intCliente > 0) {
                GetCliente();
            }


            $("#aspnetForm").validate({
                rules: {
                    txtCodigo: "required",
                    txtNombre: "required",
                    txtRfc: "required",
                    ddlUsoCFDI: "required",
                    ddlFormaPago: "required",
                    ctl00$BodyContent$ddlCiudad: "required",
                },
                messages: {
                    txtCodigo: "Requerido",
                    txtNombre: "Requerido",
                    txtRfc: "Requerido",
                    ddlUsoCFDI: "Requerido",
                    ddlFormaPago: "Requerido",
                    ctl00$BodyContent$ddlCiudad: "Requerido",
                },
                errorElement: "em",
                errorPlacement: function (error, element) {
                    // Add the `help-block` class to the error element
                    error.addClass("help-block");

                    // Add `has-feedback` class to the parent div.form-group
                    // in order to add icons to inputs
                    element.parents(".col-sm-2").addClass("has-feedback");

                    if (element.prop("type") === "checkbox") {
                        error.insertAfter(element.parent("label"));
                    } else {
                        error.insertAfter(element);
                    }
                },
                highlight: function (element, errorClass, validClass) {
                    $(element).parents(".col-md-2").addClass("has-error").removeClass("has-success");
                    $(element).parents(".col-md-4").addClass("has-error").removeClass("has-success");
                    $(element).parents(".col-md-5").addClass("has-error").removeClass("has-success");
                    $(element).parents(".col-md-6").addClass("has-error").removeClass("has-success");
                },
                unhighlight: function (element, errorClass, validClass) {

                    $(element).parents(".col-md-2").addClass("has-success").removeClass("has-error");
                    $(element).parents(".col-md-4").addClass("has-success").removeClass("has-error");
                    $(element).parents(".col-md-5").addClass("has-success").removeClass("has-error");
                    $(element).parents(".col-md-6").addClass("has-success").removeClass("has-error");
                }
            });

        });


        function GetCliente() {
            var urlData = 'FacturaCliente.aspx/Get';
            var dataData = '{ intCliente: ' + intCliente + '}';
            CallMethod(urlData, dataData, SuccessGet);
        }

        function SuccessGet(response) {
            var message = response.d[0];
            if (message == "ok") {

                var data = JSON.parse(response.d[1]);
                if (data.length > 0) {
                    $('#txtCodigo').val(data[0].strCodigo);
                    $('#txtNombre').val(data[0].strNombre);
                    $('#txtRfc').val(data[0].strRFC);
                    $('#ctl00_BodyContent_ddlEstado').val(data[0].strEstado);
                    $('#ctl00_BodyContent_ddlCiudad').val(data[0].strCiudad);
                    $('#txtColonia').val(data[0].strColonia);
                    $('#txtCalle').val(data[0].strCalle);
                    $('#txtNumExt').val(data[0].strNumExt);
                    $('#txtNumInt').val(data[0].strNumInt);
                    $('#txtCodigoPostal').val(data[0].strCodigoPostal);
                    $('#txtTelefono').val(data[0].strTelefono);
                    $('#txtEmail').val(data[0].strEmail);
                    $('#ddlUsoCFDI').val(data[0].strUsoCFDI);
                    $('#ddlFormaPago').val(data[0].strFormaPago);

                    $('#lblEstatus').text(data[0].strEstatus);
                    $('#lblError').text(data[0].strError);

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



        function Save() {
            try {
                if ($("#aspnetForm").valid()) {

                    var strCodigo = $('#txtCodigo').val();
                    var strNombre = $('#txtNombre').val();
                    var strRfc = $('#txtRfc').val();
                    var strPais = $('#ddlPais').val();
                    var strEstado = $('#ctl00_BodyContent_ddlEstado').val();
                    var strCiudad = $('#ctl00_BodyContent_ddlCiudad').val();
                    var strColonia = $('#txtColonia').val();
                    var strCalle = $('#txtCalle').val();
                    var strNumExt = $('#txtNumExt').val();
                    var strNumInt = $('#txtNumInt').val();
                    var strCodigoPostal = $('#txtCodigoPostal').val();
                    var strTelefono = $('#txtTelefono').val();
                    
                    var strEmail = $('#txtEmail').val();
                    var strUsoCFDI =  $('#ddlUsoCFDI').val();
                    var strFormaPago = $('#ddlFormaPago').val();


                    var ent = {
                        intCliente: intCliente,
                        strCodigo: strCodigo,
                        strNombre: strNombre,
                        strRfc: strRfc,
                        strPais: strPais,
                        strEstado: strEstado,
                        strCiudad: strCiudad,
                        strColonia: strColonia,
                        strCalle: strCalle,
                        strNumExt: strNumExt,
                        strNumInt: strNumInt,
                        strCodigoPostal: strCodigoPostal,
                        strTelefono: strTelefono,
                        strEmail: strEmail,
                        strUsoCFDI: strUsoCFDI,
                        strFormaPago: strFormaPago
                    }


                    var url = 'FacturaCliente.aspx/Save';
                    var data = '{';
                    data += 'ent:' + JSON.stringify(ent)
                    data += '}';

                    CallMethod(url, data, SuccessGenerarCliente);
                }
            }
            catch (e) {
                alert(e);
            }

        }

        function SuccessGenerarCliente(response) {
            var message = response.d[0];
            if (message == "ok") {
                alert('Datos guardados correctamente.');
                var data = response.d[1];
                window.location = 'FacturaCliente.aspx?id=' + data;
            }
            else {
                alert('Error al guardar la información.')
            }
        }

        function Procesar() {
            var url = 'FacturaCliente.aspx/Procesar';
            var data = '{';
            data += 'intCliente:' + intCliente
            data += '}';

            CallMethod(url, data, SuccessProcesar);
        }

        function SuccessProcesar(response) {
            var message = response.d[0];
            if (message == "ok") {

                alert('Datos guardados correctamente.');
                GetCliente();

                interval = setInterval(function () {
                    GetCliente();
                }, 15000);


                //var data = response.d[1];
            }
            else {
                alert('Error al procesar el cliente.')
            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <h2>Cliente</h2>
    
    <hr />
    <!-- DATOS GENERALES -->
    <div class="panel panel-primary">
        <div class="panel-heading">Datos Generales</div>
        <div class="panel-body form-horizontal">
            <div class="form-group">
                <label class="col-md-2 control-label">Código</label>
                <div class="col-md-2">
                    <input id="txtCodigo" name="txtCodigo" class="form-control" required="" />
                </div>
                
            </div>
            <div class="form-group">
                 <label class="col-md-2 control-label">Razón Social</label>
                <div class="col-md-4">
                    <input id="txtNombre" name="txtNombre" class="form-control" required=""/>
                </div>
            </div>
            <div class="form-group">
                 <label class="col-md-2 control-label">RFC</label>
                <div class="col-md-2">
                    <input id="txtRfc" name="txtRfc" class="form-control" required=""/>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">País</label>
                <div class="col-md-4">
                    <select id="ddlPais"  name="ddlPais" class="btn dropdown-toggle btn-default">
                        <option id="MEXICO">México</option>
                    </select>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">Estado</label>
                <div class="col-md-1">
                    <asp:DropDownList runat="server" ID="ddlEstado" CssClass="btn dropdown-toggle btn-default">
                    </asp:DropDownList>
                </div>
                <div class="col-md-1"></div>
                <label class="col-md-2 control-label">Municipio</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlCiudad" CssClass="btn dropdown-toggle btn-default">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">Colonia</label>
                <div class="col-md-2">
                    <input id="txtColonia"  name="txtColonia" class="form-control" />
                </div>
                <label class="col-md-2 control-label">Calle</label>
                <div class="col-md-2">
                    <input id="txtCalle" name="txtCalle" class="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">Num. Ext.</label>
                <div class="col-md-1">
                     <input id="txtNumExt" name="txtNumExt" class="form-control" />
                </div>
                <div class="col-md-1"></div>
                <label class="col-md-2 control-label">Num. Int.</label>
                <div class="col-md-1">
                     <input id="txtNumInt" name="txtNumInt" class="form-control" />
                </div>
            </div>


            

            <div class="form-group">
                <label class="col-md-2 control-label">Codigo Postal</label>
                <div class="col-md-1">
                     <input id="txtCodigoPostal" name="txtCodigoPostal" class="form-control" />
                </div>
                <div class="col-md-1"></div>
                <label class="col-md-2 control-label">Telefono</label>
                <div class="col-md-2">
                     <input id="txtTelefono" name="txtTelefono" class="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">Email</label>
                <div class="col-md-2">
                     <input id="txtEmail" name="txtEmail" class="form-control" />
                </div>
            </div>


            <div class="form-group">
                <label class="col-md-2 control-label">Uso CFDI</label>
                <div class="col-md-4">
                    <select id="ddlUsoCFDI" name="ddlUsoCFDI" class="btn dropdown-toggle btn-default">
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
                    <select id="ddlFormaPago" name="ddlFormaPago" class="btn dropdown-toggle btn-default">
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
                <label class="col-md-2 control-label">Estatus</label>
                <div class="col-md-4">
                    <label id="lblEstatus" class="form-control"></label>
                </div>
                <div class="col-md-4">
                    <label id="lblError" class="form-control danger"></label>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label"></label>
                <div class="col-md-2">
                     <input id="btnGuardar" type="button" class="btn btn-primary" value="Guardar" onclick="Save();" />
                     <input id="btnProcesar" type="button" class="btn btn-primary hide" value="Procesar" onclick="Procesar();" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

