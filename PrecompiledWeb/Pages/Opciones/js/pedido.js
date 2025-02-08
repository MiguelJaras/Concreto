//Controles
var rdbTipoPrecioId = 'ctl00_BodyContent_rdbTipoPrecio';
var txtOrdenCompraId = 'ctl00_BodyContent_txtOrdenCompra';
var txtClienteId = 'ctl00_BodyContent_txtCliente'
var txtEncargadoObraId = 'ctl00_BodyContent_txtEncargadoObra';
var txtTelefonosId = 'ctl00_BodyContent_txtTelefonos';
var txtCalleId = 'ctl00_BodyContent_txtCalle';
var txtColoniaId = 'ctl00_BodyContent_txtColonia';
var txtEntreCallesId = 'ctl00_BodyContent_txtEntreCalles';
var txtEntreCalles2Id = 'ctl00_BodyContent_txtEntreCalles2';
var ddlEstadoId = 'ctl00_BodyContent_ddlEstado';
var ddlCiudadId = 'ctl00_BodyContent_ddlCiudad';
var txtCodigoPostalId = 'ctl00_BodyContent_txtCodigoPostal';
var ddlProductoId = 'ctl00_BodyContent_ddlProducto';
var ddlHorasId = 'ctl00_BodyContent_ddlHoras';
var txtObservacionesId = 'ctl00_BodyContent_txtObservaciones';
var txtFechaEntregaId = 'ctl00_BodyContent_txtFechaEntrega';
var txtHoraEntregaId = 'ctl00_BodyContent_txtHoraEntrega';
var txtCantidadId = 'ctl00_BodyContent_txtCantidad';
var txtPrecioId = 'ctl00_BodyContent_txtPrecio';
var txtTotalProdId = 'ctl00_BodyContent_txtTotalProd';
var txtSubTotalId = 'ctl00_BodyContent_txtSubTotal';
var txtIvaId = 'ctl00_BodyContent_txtIva';
var txtTotalId = 'ctl00_BodyContent_txtTotal';
var hdnServiciosId = 'ctl00_BodyContent_hdnServicios';
var txtRemisionId = 'ctl00_BodyContent_txtRemision';
var hdnIntPartida = 'ctl00_BodyContent_hdnIntPartida';
var txtTotalPartida = 'txtTotalPartida';
var btnEmailId = 'ctl00_BodyContent_btnEmail';
var hdnOrdenStatus = 'ctl00_BodyContent_hdnOrdenStatus';
var btnAceptarId = 'ctl00_BodyContent_btnAceptar';
var btnCancelarId = 'ctl00_BodyContent_btnCancelar';
var btnEnviarId = 'ctl00_BodyContent_btnEnviar';
var btnPreviewId = 'btnPreview';
var hddCantidadTuberia = 'ctl00_BodyContent_hddCantidadTuberia';
var txtEmail = 'ctl00_BodyContent_txtEmail';
var txtVendedor = 'ctl00_BodyContent_txtVendedor';

var insumos = []; 
var servicios = [];
var porcIva = '16.00';
var tablaPartidasId = 'table_partidas';
var intPartidaAjuste = 0;
var intCliente = 0;

$(document).ready(function () {

    insumos = JSON.parse($('#ctl00_BodyContent_hdnArrInsumos').val());
    servicios = JSON.parse($('#ctl00_BodyContent_hdnArrServicios').val());
    intCliente = parseInt($('#ctl00_BodyContent_hdnCliente').val());
    
    $('#' + ddlProductoId).on("change", function (event) {
        CambiarProducto();
    });

    $('#' + txtCantidadId).on("change", function (event) {
        CalcularPrecio();
    });

    $('#' + txtCantidadId).on("change", function (event) {
        ValidCantidad();
    });
    
    $.validator.addMethod("time", function (value, element) {
        return this.optional(element) || /^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$/i.test(value);
    }, "");
    $.validator.addMethod("valueNotEquals", function (value, element, arg) {
        return arg != value;
    }, "Value must not equal arg.");

    $('.precio').priceFormat({
        prefix: '$ ',
        centsSeparator: '.',
        thousandsSeparator: ','
    });
    CheckServicios();
    BuscarPartidas();
    GetListVendedores();
    GetListClientes();
    var strOrdenStatus = $('#' + hdnOrdenStatus).val();
    switch(strOrdenStatus.toLowerCase())
    {
        case 'x':
            //$('#' + btnCancelarId).hide();
            //$('#' + btnAceptarId).hide();
            //$('#' + btnEnviarId).hide();
            break;
        case 's':
            //$('#' + btnEnviarId).hide();
            break;
        case 'o':
            $('#' + txtFechaEntregaId).datepicker({
                dateFormat: 'dd/mm/yy',
            });
            break;
        default:
            $('#' + txtFechaEntregaId).datepicker({
                dateFormat: 'dd/mm/yy',
            });
            $('#' + btnCancelarId).hide();
            $('#' + btnEnviarId).hide();
            //$('#' + btnPreviewId).hide();
            break;
    }
    $('input[type=text], textarea').blur(function () {
        this.value = this.value.toUpperCase();
    })

});

/*Funciones*/

function Validate() {
    if ($("#aspnetForm").valid()) {
        return true;
    }
    else {
        return false;
    }
}

function ValidCheckBox(object) {
    var id = object.value;
    
    var result = $.grep(servicios, function (e) { return e.intServicio == id; });
    if (result.length > 0) {
        var servicio = result[0];
        var dblPrecio = servicio.dblPrecio;
        var txtPrecioServicio = $('#txtPrecioServicio_' + id)
        var txtTotalServicio = $('#txtTotalServicio_' + id)
        var intCantidad = $('#' + txtCantidadId).val();
        var intGrupo = servicio.intGrupo;
        intCantidad = parseFloat(intCantidad);
        txtPrecioServicio.val(0);
        txtTotalServicio.val(0);

        if (id == 1) {
            var radios = document.getElementsByName('rdbBombeo');
            var radiosTam = document.getElementsByName('rdbBombeoTam');
            for (var i = 0; i < radios.length; i++) {
                radios[i].disabled = !object.checked;
                radiosTam[i].disabled = !object.checked;
            }
        }

        if (object.checked) {

            if (intGrupo > 0) {
                var resultgrp = $.grep(servicios, function (e) { return e.intGrupo == intGrupo; });
                for (var i = 0; i < resultgrp.length; i++) {
                    if (id != resultgrp[i].intServicio) {
                        $('#chkServicio_' + resultgrp[i].intServicio).prop('checked', false);
                        $('#txtPrecioServicio_' + resultgrp[i].intServicio).val(0);
                        $('#txtTotalServicio_' + resultgrp[i].intServicio).val(0);
                    }
                }
            }
            
            if (id == 1) {
                

                var intBombeo = $('input[name="rdbBombeo"]:checked').val();
                $('#ctl00_BodyContent_hdnGrua').val(intBombeo);

                var intTipoBombeo = $('input[name="rdbBombeoTam"]:checked').val();
                $('#ctl00_BodyContent_hddTipoBombeo').val(intTipoBombeo);

                var intCantidadMinimaBombeo = 10;
                var decTotalCantidadPartidas = GetTotalPartidas() + intCantidad;
                if (intTipoBombeo == 2)
                    intCantidadMinimaBombeo = 15;

                //Si es bomba se calcula precio
                if (intBombeo == 1) {
                    var totalProdBombeo = GetTotalBombeo();
                    var intCantidadProdBombeo = totalProdBombeo + intCantidad;
                    if (intCantidadProdBombeo < intCantidadMinimaBombeo) {
                        //$('#' + txtCantidadId).val(10);
                        dblPrecio = (dblPrecio * intCantidadMinimaBombeo) / intCantidadProdBombeo;
                        //CheckServicios();
                    }
                }
            }
            var intCantidad = $('#' + txtCantidadId).val();
            var dblTotalServicio = dblPrecio * intCantidad;
            dblPrecio = dblPrecio.toFixed(2);
            dblTotalServicio = dblTotalServicio.toFixed(2);
            txtPrecioServicio.val(dblPrecio);
            txtTotalServicio.val(dblTotalServicio);
        }
    }
    CalcularPrecio();
}

//function ValidTextBox(id)
//{
    
//    var object = $('#txtCantidadProd_' + id);
//    var objectTotal = $('#' + hddCantidadTuberia)
//    var intCantidadTubieria = object.val();
//    var intCantidadPartida = $('#' + txtCantidadId).val(); //Cantidad mt partida
//    var dblTotalPartidas = GetTotalPartidas();

//    var result = $.grep(servicios, function (e) { return e.intServicio == id; });
//    if (result.length > 0) {
//        var servicio = result[0];
//        var dblPrecio = servicio.dblPrecio;
//        var txtPrecioServicio = $('#txtPrecioServicio_' + id)
//        var txtTotalServicio = $('#txtTotalServicio_' + id)
//        var dblTotalServicio = dblPrecio * parseFloat(intCantidadTubieria) * parseFloat(intCantidadPartida);
//        dblTotalPartidas = dblTotalPartidas + parseFloat(intCantidadPartida);
        
//        if (dblTotalPartidas > 30) {
//            dblPrecio = 600;
//            dblTotalServicio = dblPrecio * parseFloat(intCantidadTubieria);
//        }

//        dblPrecio = dblPrecio.toFixed(2);
//        dblTotalServicio = dblTotalServicio.toFixed(2);
//        txtPrecioServicio.val(dblPrecio);
//        txtTotalServicio.val(dblTotalServicio);
//        // JavaScript
//        $('.precio').priceFormat({
//            prefix: '$ ',
//            centsSeparator: '.',
//            thousandsSeparator: ','
//        });
//        objectTotal.val(intCantidadTubieria);
//        CalcularPrecio();
//    }
//}

function ValidTextBox(id)
    {
    
        var object = $('#txtCantidadProd_' + id);
        var objectTotal = $('#' + hddCantidadTuberia)
        var intCantidadTubieria = object.val();
        var intCantidadPartida = $('#' + txtCantidadId).val(); //Cantidad mt partida
        var dblTotalPartidas = GetTotalPartidas();

        var result = $.grep(servicios, function (e) { return e.intServicio == id; });
        if (result.length > 0) {
            var servicio = result[0];
            var dblPrecio = servicio.dblPrecio;
            var txtPrecioServicio = $('#txtPrecioServicio_' + id)
            var txtTotalServicio = $('#txtTotalServicio_' + id)
            var dblTotalServicio = dblPrecio * parseFloat(intCantidadTubieria);// * parseFloat(intCantidadPartida);
            dblTotalPartidas = dblTotalPartidas + parseFloat(intCantidadPartida);
        
            //if (dblTotalPartidas > 30) {
            //    dblPrecio = 600;
            //    dblTotalServicio = dblPrecio * parseFloat(intCantidadTubieria);
            //}

            dblPrecio = dblPrecio.toFixed(2);
            dblTotalServicio = dblTotalServicio.toFixed(2);
            txtPrecioServicio.val(dblPrecio);
            txtTotalServicio.val(dblTotalServicio);
            // JavaScript
            $('.precio').priceFormat({
                prefix: '$ ',
                centsSeparator: '.',
                thousandsSeparator: ','
            });
            objectTotal.val(intCantidadTubieria);
            CalcularPrecio();
        }
    }



function ValidCantidad() {
    var txtCantidad = $('#' + txtCantidadId);
    if (txtCantidad.val() < 1) {
        txtCantidad.val(1);
    }
    //Se vuelven a calcular precios de servicios con la nueva cantidad
    CheckServicios();
    ValidTextBox(7);
}

function CalcularPrecio() {
    var id = $("#" + ddlProductoId).val();
    var totalProd = GetPrecioProducto(id);
    var totalServicios = GetTotalServicios();
    var totalList = GetTotalList();
    var totalPartida = totalProd + totalServicios;
    var subtotal = totalProd + totalServicios + totalList;
    var iva = subtotal * (porcIva / 100);
    var total = subtotal + iva;
  
   

    //Fix decimal
    subtotal = subtotal.toFixed(2);
    iva = iva.toFixed(2);
    total = total.toFixed(2);
    totalPartida = totalPartida.toFixed(2);

    //$('#' + txtSubTotalId).val(subtotal);
    //$('#' + txtIvaId).val(iva);
    //$('#' + txtTotalId).val(total);
    
    $('#' + txtTotalPartida).val(totalPartida);
    // JavaScript
    $('.precio').priceFormat({
        prefix: '$ ',
        centsSeparator: '.',
        thousandsSeparator: ','
    });
    $('#' + hdnServiciosId).val(GetValuesServicios());
}

function GetTotalList()
{
    var id = $('#' + hdnIntPartida).val();
    if (id == "")
    {
        id = 0;
    }
    var total = 0;
    if ($.fn.DataTable.isDataTable('#' + tablaPartidasId)) {
        var table = $('#' + tablaPartidasId).DataTable();
        table.rows().eq(0).each(function (index) {
            var row = table.row(index);
            var data = row.data();
            if (id != data.intPartida) {
                var totalProd = parseFloat(data.Total);
                var totalServicios = parseFloat(data.TotalServicios);
                total += (totalProd + totalServicios);
            }
        });
    }
    return parseFloat(total);
}

function GetTotalBombeo() {
    var id = $('#' + hdnIntPartida).val();
    if (id == "") {
        id = 0;
    }
    var total = 0;
    if ($.fn.DataTable.isDataTable('#' + tablaPartidasId)) {
        var table = $('#' + tablaPartidasId).DataTable();
        table.rows().eq(0).each(function (index) {
            var row = table.row(index);
            var data = row.data();
            if (id != data.intPartida) {
                var totalBombeo = parseFloat(data.dblCantidadBombeo);
                total += (totalBombeo);
            }
        });
    }
    return parseFloat(total);
}

function GetTotalPartidas() {
    var id = $('#' + hdnIntPartida).val();
    if (id == "") {
        id = 0;
    }
    var total = 0;
    if ($.fn.DataTable.isDataTable('#' + tablaPartidasId)) {
        var table = $('#' + tablaPartidasId).DataTable();
        table.rows().eq(0).each(function (index) {
            var row = table.row(index);
            var data = row.data();
            if (data.intProducto != 24 && data.intProducto != 22) {
                if (id != data.intPartida) {
                    var cantidadPartida = parseFloat(data.dblCantidad);
                    total += (cantidadPartida);
                }
            }
        });
    }
    
    return parseFloat(total);
}


function CambiarProducto()
{
    var id = $("#" + ddlProductoId).val();
    var result = $.grep(insumos, function (e) { return e.intProducto == id; });
    var tipoPrecio = 3;
    var dblPrecio = 0;
    if (result.length > 0) {
        var product = result[0];
        switch (tipoPrecio) {
            case 1:
                dblPrecio = product.dblMenudeo
                break;
            case 2:
                dblPrecio = product.dblMedioMayoreo;
                break;
            case 3:
                dblPrecio = product.dblMayoreo;
                break;
            default:
                dblPrecio = product.dblMenudeo;
        }
    }
    dblPrecio = dblPrecio.toFixed(2);
    $('#' + txtPrecioId).val(dblPrecio);
    CalcularPrecio();
}

function GetPrecioProducto(id)
{
    var totalProd = 0;
    var dblPrecio = $('#' + txtPrecioId).val();
    dblPrecio = dblPrecio.replace(/[^0-9\.]+/g, "");
    var cantidad = $('#' + txtCantidadId).val();
    dblPrecio = parseFloat(dblPrecio);
    cantidad = parseFloat(cantidad);

    totalProd = dblPrecio * cantidad;
    dblPrecio = dblPrecio.toFixed(2);
    totalProd = totalProd.toFixed(2);
    $('#' + txtTotalProdId).val(totalProd);
    return parseFloat(totalProd);
}

function GetTotalServicios() {
    var totalServicios = 0;
    var cantidad = $('#' + txtCantidadId).val();
    
    $.each(servicios, function (i) {
        var object = servicios[i];
        
        if (object.intServicio != 7) {
            var checkbox = '#chkServicio_' + object.intServicio;
            if ($(checkbox).is(':checked')) {
                totalServicios += object.dblPrecio * cantidad;
            }
        }
        else {
            var dblTotalPartidas = GetTotalPartidas() + parseFloat(cantidad);;

            var cantidadTub = $('#txtCantidadProd_7').val();
            
            //if (dblTotalPartidas > 30) {
            //    dblPrecio = 600;
            totalServicios += (object.dblPrecio * parseFloat(cantidadTub));
            //}
            //else {
            //    totalServicios += (object.dblPrecio * parseFloat(cantidadTub) * cantidad);
            //}
            
        }
    });
    return parseFloat(totalServicios);
}

function GetValuesServicios() {
    var values = new Array();
    $.each($("input[name='chkServicio']:checked"), function () {
        values.push($(this).val());
    });
    var strValues = values.join(",");
    return strValues;
}

function DatosGuardados()
{
    Clear();
    BuscarPartidas();
    alert('Datos guardados correctamente.')
    $('.precio').priceFormat({
        prefix: '$ ',
        centsSeparator: '.',
        thousandsSeparator: ','
    });
}
/*Funciones*/


//function Message(message) {
//    switch (message)
//    {
//        case 1:
//            alert('Favor de seleccionar un producto.');
//            break;
//        case 2:
//            alert('No se puede guardar el pedido, la fecha tiene que ser mayor al día de hoy.');
//            break;
//        case 3:
//            alert('No se puede guardar el pedido, cuando se realiza un pedido despues de las 12:00 p.m. \ndebe ser 2 días posteriores al día de hoy.');
//            break;
//        default:
//            break;
//    }
//    return;
//}




/*PARTIDAS*/
function BuscarPartidas() {

    var intPedido = $('#' + txtRemisionId).val();
    if (intPedido == '') {
        intPedido = 0;
    }
    var urlData = 'Pedido.aspx/GetListPartidas';
    var dataData = '{ intPedido:"' + intPedido + '" }';
    CallMethod(urlData, dataData, SuccessGetPartidas);
}

function SuccessGetPartidas(response) {

    var message = response.d[0];
    var data = response.d[1];


    if (message == "ok") {
        var count = Object.keys(JSON.parse(data)).length;

        if (count >= 1) {
            var dataPartida = JSON.parse(data);
            intPartidaAjuste = dataPartida[0].intPartida;
            $('#rowAjuste').removeClass('hide');
           
        } else {
            $('#rowAjuste').addClass('hide');
            
        }

        if (count > 0) {

            //$("#ctl00_BodyContent_ddlProducto").rules("remove");
            //$("#ctl00_BodyContent_txtHoraEntrega").rules("remove");

            $("#aspnetForm").validate({
                rules: {
                    ctl00$BodyContent$txtCliente: "required",
                    ctl00$BodyContent$txtEncargadoObra: "required",
                    ctl00$BodyContent$txtTelefonos: { "required": true, minlength: 8, min: 1 },
                    ctl00$BodyContent$ddlEstado: "required",
                    ctl00$BodyContent$ddlCiudad: "required",
                    ctl00$BodyContent$txtCalle: "required",
                    ctl00$BodyContent$txtColonia: "required",
                    ctl00$BodyContent$txtObservaciones: "required",
                    //ctl00$BodyContent$txtEmail: { "required": true, email: true},
                    //ctl00$BodyContent$txtVendedor: "required",
                },
                messages: {
                    ctl00$BodyContent$txtCliente: "Requerido",
                    ctl00$BodyContent$txtEncargadoObra: "Requerido",
                    ctl00$BodyContent$txtTelefonos: "Requerido, se deben capturar al menos 8 digitos",
                    ctl00$BodyContent$ddlEstado: "Requerido",
                    ctl00$BodyContent$ddlCiudad: "Requerido",
                    ctl00$BodyContent$txtCalle: "Requerido",
                    ctl00$BodyContent$txtColonia: "Requerido",
                    ctl00$BodyContent$txtObservaciones: "Requerido",
                    //ctl00$BodyContent$txtEmail: "Requerido",
                    //ctl00$BodyContent$txtVendedor: "Requerido",
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

        } else {

            $("#aspnetForm").validate({
                rules: {
                    ctl00$BodyContent$txtHoraEntrega: { "required": true, "time": true },
                    ctl00$BodyContent$txtCliente: "required",
                    ctl00$BodyContent$txtEncargadoObra: "required",
                    ctl00$BodyContent$txtTelefonos: { "required": true, minlength: 8, min: 1 },
                    ctl00$BodyContent$ddlProducto: { "valueNotEquals": "0" },
                    ctl00$BodyContent$ddlHoras: { "valueNotEquals": "0" },
                    ctl00$BodyContent$ddlEstado: "required",
                    ctl00$BodyContent$ddlCiudad: "required",
                    ctl00$BodyContent$txtCantidad: "required",
                    ctl00$BodyContent$txtCalle: "required",
                    ctl00$BodyContent$txtColonia: "required",
                    ctl00$BodyContent$txtObservaciones: "required",
                    ctl00$BodyContent$txtEmail: { "required": true, email: true },
                    ctl00$BodyContent$txtVendedor: "required",
                },
                messages: {
                    ctl00$BodyContent$txtHoraEntrega: "Ingresar Hora Válida",
                    ctl00$BodyContent$txtCliente: "Requerido",
                    ctl00$BodyContent$txtEncargadoObra: "Requerido",
                    ctl00$BodyContent$txtTelefonos: "Requerido, se deben capturar al menos 8 digitos",
                    ctl00$BodyContent$ddlProducto: "Requerido",
                    ctl00$BodyContent$ddlHoras: "Requerido",
                    ctl00$BodyContent$ddlEstado: "Requerido",
                    ctl00$BodyContent$ddlCiudad: "Requerido",
                    ctl00$BodyContent$ddlProducto: "Requerido",
                    ctl00$BodyContent$txtCalle: "Requerido",
                    ctl00$BodyContent$txtColonia: "Requerido",
                    ctl00$BodyContent$txtObservaciones: "Requerido",
                    ctl00$BodyContent$txtEmail: "Requerido, se debe ingresar un email válido.",
                    ctl00$BodyContent$txtVendedor: "Requerido",
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

        }
        CreateTable(data);
    } else {
        alert('Error al cargar los datos')
    }
}

function EditarPartida(intPartida) {
    if (intPartida > 0) {
        Clear();
        $('#' + hdnIntPartida).val(intPartida);        var intPedido = $('#' + txtRemisionId).val();        var urlData = 'Pedido.aspx/GetPartida';
        var dataData = '{ intPedido:"' + intPedido + '", intPartida:"' + intPartida + '" }';
        CallMethod(urlData, dataData, SuccessEditarPartida);
    }
}function SuccessEditarPartida(response) {
    var message = response.d[0];
    var data = JSON.parse(response.d[1]);
    var dataServicios = JSON.parse(response.d[2]);
    var strServicios = '';
    var arrServicios = [];
    if (message == "ok") {

        $.each(dataServicios, function (index) {
            arrServicios.push(dataServicios[index].intServicio);
        });

        var resultGrua = $.grep(dataServicios, function (e) { return e.intServicio == 1; });
        if (resultGrua.length > 0) {
            var servicioGrua = resultGrua[0];
            if (servicioGrua.bGrua) {
                $("input[name=rdbBombeo][value=2]").attr('checked', 'checked');
            } else {
                $("input[name=rdbBombeo][value=1]").attr('checked', 'checked');
            }
            
            if (servicioGrua.intTipoBomba == 2) {
                $("input[name=rdbBombeoTam][value=2]").attr('checked', 'checked');
            } else {
                $("input[name=rdbBombeoTam][value=1]").attr('checked', 'checked');
            }
        }
        strServicios = arrServicios.join(',');
        var dblPrecio = data[0].dblPrecio;
        dblPrecio = dblPrecio.toFixed(2);
        $('#' + ddlProductoId).val(data[0].intProducto)        $('#' + ddlHorasId).val(data[0].strHoraEntrega)        $('#' + txtCantidadId).val(data[0].dblCantidad)        $('#' + txtPrecioId).val(dblPrecio);        $('#' + hdnServiciosId).val(strServicios);
        CheckServicios();
        CalcularPrecio();

        var result = $.grep(dataServicios, function (e) { return e.intServicio == 7; });
        if (result.length > 0) {
            var servicio = result[0];
            var object = $('#txtCantidadProd_7');
            object.val(servicio.dblCantidad);
            ValidTextBox(7);
        }

        //Si el pedido está en estatus enviado, se inhabilita la hora de entrega
        var strOrdenStatus = $('#' + hdnOrdenStatus).val();
        $('#' + txtHoraEntregaId).attr('readonly', true);
        

    } else {
        alert('Error al cargar los datos')
    }
}
function EliminarPartida(intPartida, strEstatus, dblCantidadBombeo) {
    if (confirm('¿Está seguro que desea eliminar el producto?')) {
        $('#' + hdnIntPartida).val(intPartida);        var intPedido = $('#' + txtRemisionId).val();        var urlData = 'Pedido.aspx/EliminarPartida';
        var dataData = '{ intPedido:"' + intPedido + '", intPartida:"' + intPartida + '" }';        CallMethod(urlData, dataData, SuccessDelPartida);
    } else {
        return false;
    }
}function SuccessDelPartida(response) {
    var id = $('#' + txtRemisionId).val();
    var message = response.d[0];
    var data = response.d[1];
    if (message == "ok") {
        alert(data)
        window.location = "Pedido.aspx?id=" + id;
        //BuscarPartidas();        //Clear();
    } else {
        alert(data)
    }
}
function CheckAjuste(object) {
    if (object.checked) {
        $('#btnLimpiar').addClass('hide');
        if (intPartidaAjuste > 0) {
            Clear();
            $('#' + hdnIntPartida).val(0);            var intPedido = $('#' + txtRemisionId).val();            var urlData = 'Pedido.aspx/GetPartida';
            var dataData = '{ intPedido:"' + intPedido + '", intPartida:"' + intPartidaAjuste + '" }';
            CallMethod(urlData, dataData, SuccessAjuste);
            
        }
    } else {
        $('#btnLimpiar').removeClass('hide');
        Clear();
    }

}

function SuccessAjuste(response) {
    var message = response.d[0];
    var data = JSON.parse(response.d[1]);
    var dataServicios = JSON.parse(response.d[2]);
    var strServicios = '';
    var arrServicios = [];
    if (message == "ok") {

        $.each(dataServicios, function (index) {
            arrServicios.push(dataServicios[index].intServicio);
        });

        var resultGrua = $.grep(dataServicios, function (e) { return e.intServicio == 1; });
        if (resultGrua.length > 0) {
            var servicioGrua = resultGrua[0];
            if (servicioGrua.bGrua) {
                $("input[name=rdbBombeo][value=2]").attr('checked', 'checked');
            } else {
                $("input[name=rdbBombeo][value=1]").attr('checked', 'checked');
            }

        }
        strServicios = arrServicios.join(',');
        var dblPrecio = data[0].dblPrecio;
        dblPrecio = dblPrecio.toFixed(2);
        $('#' + ddlProductoId).val(data[0].intProducto)        $('#' + ddlHorasId).val("0")        $('#' + txtCantidadId).val("6")        $('#' + txtPrecioId).val(dblPrecio);        $('#' + hdnServiciosId).val(strServicios);
        CheckServicios();
        CalcularPrecio();

        var result = $.grep(dataServicios, function (e) { return e.intServicio == 7; });
        if (result.length > 0) {
            var servicio = result[0];
            var object = $('#txtCantidadProd_7');
            object.val(servicio.dblCantidad);
            ValidTextBox(7);
        }

    } else {
        alert('Error al cargar los datos')
    }
}

function Clear() {
    $('#' + hdnIntPartida).val(0);
    $('#' + ddlProductoId).val("0");    $('#' + ddlHorasId).val("0");    $('#' + txtCantidadId).val("6");    $('#' + txtPrecioId).val("$ 0.00");    $('#' + txtTotalProdId).val("$ 0.00");    $('#' + txtTotalPartida).val("$ 0.00");    servicios = JSON.parse($('#ctl00_BodyContent_hdnArrServicios').val());    for (var i = 0; i < servicios.length; i++) {
        $('#chkServicio_' + servicios[i].intServicio).prop('checked', false);
        $('#chkServicio_' + servicios[i].intServicio).change();
    }    //$("#state").attr("readonly", false);    $('#txtCantidadProd_7').val(0);    ValidTextBox(7);
}
function CheckServicios() {
    var servicios = $('#ctl00_BodyContent_hdnServicios').val();
    if (servicios != '') {
        var arrServicios = servicios.split(',');
        for (var i = 0; i <= arrServicios.length; i++) {
            $('#chkServicio_' + arrServicios[i]).prop('checked', true);
            $('#chkServicio_' + arrServicios[i]).change();
        }
    }
}
function CreateTable(data) {

    if ($.fn.DataTable.isDataTable('#' + tablaPartidasId)) {
        $('#' + tablaPartidasId).dataTable().fnDestroy();
    }

    var data = JSON.parse(data);
    var table = $('#' + tablaPartidasId).DataTable({
        searching: false,
        bLengthChange: false,
        paging: false,
        responsive: true,
        orderCellsTop: false,
        language: {
            url: "../../Scripts/dataTables/js/Spanish.js"
        },
        order: [[4, "asc"]],
        data: data,
        columnDefs: [
              { className: "text-center", "targets": [0, 2, 3, 4, 5] },
              { "width": "20%", "targets": 1 },
              { "width": "10%", "targets": [2, 3, 4] }
        ],
        columns: [
            { data: "intPartida", title: "#", bSortable: false },
            { data: "strProducto", title: "Producto", bSortable: false },
            { data: "dblCantidad", title: "Cantidad", bSortable: false },
            { data: "dblPrecio", title: "Precio", bSortable: false },
            { data: "strHoraEntrega", title: "Hora de Entrega", bSortable: false },
            {
                data: null,
                bSortable: false,
                render: function (o) {
                    var btnEditar = '<a href="Javascript:EditarPartida(' + o.intPartida + ');"><span class="glyphicon glyphicon-pencil"></span></a>';
                    var btnEliminar = '&nbsp&nbsp&nbsp<a href="#table_partidas" onclick="JavaScript:(EliminarPartida(' + o.intPartida + ',\'' + o.Order_Status + '\',' + o.dblCantidadBombeo + '))"><span class="glyphicon glyphicon-trash"></span></a>';
                    
                    if (intCliente == 16){
                        btnEliminar = '';
                    }
                    return btnEditar + btnEliminar;
                }
            }
        ],
        "initComplete": function () {
            $('#table_partidas_info').html(
                ''
            );

            var strFechaEntrega = $('#' + txtFechaEntregaId).datepicker("getDate")
            var datFechaEntrega = new Date(strFechaEntrega);
            var datFechaActual = new Date();

        }
    });

}


/*PARTIDAS*/

/*PEDIDO*/
function EnviarPedido() {
    if (confirm('¿Está seguro que desea enviar el pedido?')) {
        var intPedido = $('#' + txtRemisionId).val();        if (intPedido != '') {
            var urlData = 'Pedido.aspx/EnviarPedido';
            var dataData = '{ intPedido:"' + intPedido + '" }';            CallMethod(urlData, dataData, SuccessEnviarPedido);
        }
    } else {
        return false;
    }
}

function SuccessEnviarPedido(response) {
    var message = response.d[1];
    //if (response.d[0] == "ok") {
    //    var intPedido = $('#' + txtRemisionId).val();
    //    EnviarEmail();
    //} 
    alert(message)
}

function CancelarPedido() {
    if (confirm('¿Está seguro que desea cancelar el pedido?')) {
        var intPedido = $('#' + txtRemisionId).val();        if (intPedido != '') {
            var urlData = 'Pedido.aspx/CancelarPedido';
            var dataData = '{ intPedido:"' + intPedido + '" }';            CallMethod(urlData, dataData, SuccessCancelarPedido);
        }
    } else {
        return false;
    }
}
function SuccessCancelarPedido(response) {
    var message = response.d[1];
    alert(message)
    if (response.d[0] == "ok") {
        window.location = 'PedidoListado.aspx';
    } 
}

function EnviarEmail(intPedido) {
    $('#' + btnEmailId).click();
}

function GuardarPedido() {

    var intPedido = $('#' + txtRemisionId).val();
    var PO_Num = $('#' + txtOrdenCompraId).val();
    var intTipoPrecio = 3;
    var strCliente = $('#' + txtClienteId).val();
    var strEncargado = $('#' + txtEncargadoObraId).val();
    var strTelefonos = $('#' + txtTelefonosId).val();
    var strCalle = $('#' + txtCalleId).val();
    var strColonia = $('#' + txtColoniaId).val();
    var strCalleEntre = $('#' + txtEntreCallesId).val();
    var strCalleEntre2 = $('#' + txtEntreCalles2Id).val();
    var State_Code = $('#' + ddlEstadoId).val();
    var City = $('#' + ddlCiudadId).val();
    var Postal_Code = $('#' + txtCodigoPostalId).val();
    var Delivery_Instructions = $('#' + txtObservacionesId).val();
    var datFechaEntrega = $('#' + txtFechaEntregaId).val();


    var ent = {
        intPedido: intPedido,
        PO_Num: PO_Num,
        strCliente: strCliente,
        strEncargado: strEncargado,
        strTelefonos: strTelefonos,
        strCalle: strCalle,
        strColonia: strColonia,
        strCalleEntre: strCalleEntre,
        strCalleEntre2: strCalleEntre2,
        State_Code: State_Code,
        City: City,
        Postal_Code: Postal_Code,
        Delivery_Instructions: Delivery_Instructions,
        datFechaEntrega: datFechaEntrega
    };
    
    
    var urlData = 'Pedido.aspx/GuardarPedido';
    var dataData = '{ ent:' + JSON.stringify(ent) +  '}';    CallMethod(urlData, dataData, SuccessGuardarPedido);
}

function SuccessGuardarPedido()
{
    
}

function PreviewPedido() {
    var intPedido = $('#' + txtRemisionId).val();    if (intPedido != '') {
        var urlData = 'Pedido.aspx/PreviewPedido';
        var dataData = '{ intPedido:"' + intPedido + '" }';        CallMethod(urlData, dataData, SuccessPreviewPedido);
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

function GetListVendedores()
{
    var urlData = 'Pedido.aspx/GetListVendedores';
    var dataData = '';
    CallMethod(urlData, dataData, SuccessGetVendedores);
}

function SuccessGetVendedores(response)
{
    var message = response.d[0];
    if (message == "ok") {
        var data = response.d[1];
        $("#" + txtVendedor).autocomplete({
            source: JSON.parse(data),
        });

    }
}




function GetListClientes() {
    var urlData = 'Pedido.aspx/GetListClientes';
    var dataData = '';
    CallMethod(urlData, dataData, SuccessGetClientes);
}

function SuccessGetClientes(response) {
    var message = response.d[0];
    if (message == "ok") {
        var data = response.d[1];
        $("#" + txtClienteId).autocomplete({
            source: JSON.parse(data),
        });

    }
}



function ChangeFacturaEmail(option)
{
    if (option == 1) {
        $('#ctl00_BodyContent_txtEmail').prop('disabled', false);
    } else {
        $('#ctl00_BodyContent_txtEmail').val('EXAMPLE@MARFIL.COM');
        $('#ctl00_BodyContent_txtEmail').prop('disabled', true);
    }
}


/*PEDIDO*/




