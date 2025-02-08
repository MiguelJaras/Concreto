var arrProductos = [];
var intCotizacion = 0;
var intPartida = 0;
var intTipo = 1;
$(document).ready(function () {

    var id = getParameterByName("id");
    if (id == null || id == '') {
        id = 0;
    }
    intCotizacion = id;
    
    
    $('#txtFechaColado').datepicker({
        dateFormat: 'dd/mm/yy',
    });


    var d = new Date();
    var month = d.getMonth();
    var day = d.getDate();
  
    $('#txtFechaColado').datepicker("setDate", new Date(d.getFullYear(), month, day));


    GetListProductos();
    $('#ddlProductos').on("change", function (event) {
        CambiarProducto();
        
    });

    $("#aspnetForm").validate({
        rules: {
            txtCliente: "required",
            txtFechaColado: "required",
        },
        messages: {
            txtCliente: "Requerido",
            txtFechaColado: "required",
        },
        errorElement: "em",
        errorPlacement: function (error, element) {
            error.addClass("help-block");

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

    $('.precio').priceFormat({
        prefix: '$ ',
        centsSeparator: '.',
        thousandsSeparator: ','
    });

    if (intCotizacion > 0) {
        GetCotizacion();
        GetCotizacionDetalle();
    }

});
function GetListProductos() {

    var urlData = 'Cotizacion.aspx/GetListProductos';
    var dataData = '{ }';
    CallMethod(urlData, dataData, SuccessProductos);
}

function SuccessProductos(response) {
    var message = response.d[0];
    var data = response.d[1];
    if (message == "ok") {
        var dataProductos = JSON.parse(data);
        arrProductos = dataProductos;
        FillSelect('ddlProductos', dataProductos, 'intProducto', 'strNombre', true, '', '--Seleccione--');
        $("#ddlProductos").select2({

        });
    } else {
        alert('Error al cargar los datos');
    }
}


function GetCotizacion() {

    var urlData = 'Cotizacion.aspx/GetCotizacion';
    var dataData = '{ id:' + intCotizacion + ' }';
    CallMethod(urlData, dataData, SuccessGetCot);
}

function SuccessGetCot(response) {
    var message = response.d[0];
    var data = response.d[1];
    if (message == "ok") {
        var data = JSON.parse(response.d[1]);
        if (data.length > 0) {
            $('#txtCliente').val(data[0].strCliente);
            $('#txtObra').val(data[0].strObra);
            $('#txtElemento').val(data[0].strElemento);
            $('#txtFechaColado').val(data[0].datFechaColado);
            $('#txtTipoConcreto').val(data[0].strTipoConcreto);
            $('#txtResistencia').val(data[0].strResistencia);
            $('#txtRevenimiento').val(data[0].intRevenimiento);
            $('#txtAgregado').val(data[0].intAgregado);
            $('#txtTipo').val(data[0].intTipo);
            $('#txtExtras').val(data[0].strExtras);
            $('#txtExtras2').val(data[0].strExtras2);
            $('#txtTiro').val(data[0].intTiro);
            
            var decSubTotal = parseFloat(data[0].decSubTotal).toFixed(2);
            $('#txtSubTotal').val(decSubTotal);

            var decIva = parseFloat(data[0].decIva).toFixed(2);
            $('#txtIva').val(decIva);

            var decTotal = parseFloat(data[0].decTotal).toFixed(2);
            $('#txtTotalPartida').val(decTotal);

        }
    } else {
        alert('Error al cargar los datos');
    }
}

function GetCotizacionDetalle() {
    var urlData = 'Cotizacion.aspx/GetCotizacionDet';
    var dataData = '{ id:' + intCotizacion + ' }';
    CallMethod(urlData, dataData, SuccessGetCotDet);
}

function SuccessGetCotDet(response) {
    var message = response.d[0];
    var data = response.d[1];
    if (message == "ok") {
        var data = JSON.parse(response.d[1]);
        CreateTable(data);
    } else {
        alert('Error al cargar los datos');
    }
}


function CreateTable(data) {
    if ($.fn.DataTable.isDataTable('#table_productos')) {
        $('#table_productos').dataTable().fnDestroy();
    }
    var table = $('#table_productos').DataTable({
        searching: false,
        bLengthChange: false,
        paging: false,
        pageLength: 20,
        responsive: true,
        orderCellsTop: true,
        language: {
            url: "../../Scripts/dataTables/js/Spanish.js"
        },
        order: [[0, "desc"]],
        data: data,
        columnDefs: [
              { className: "text-left", "targets": [0] },
              { className: "text-center", "targets": [1, 4] },
              { className: "text-right", "targets": [2, 3] },
              

        ],
        columns: [

            { data: "strProducto", title: "Producto" },
            { data: "decCantidad", title: "Cantidad" },
            { data: "decPrecio", title: "Precio", render: $.fn.dataTable.render.number(',', '.', 2, '$ ') },
            { data: "decTotal", title: "Total", render: $.fn.dataTable.render.number(',', '.', 2, '$ ') },
            {
                data: null,
                bSortable: false,
                render: function (o) {
                    var btnEliminar = '&nbsp&nbsp&nbsp<a href="#table_productos" onclick="JavaScript:(EliminarPartida(' + o.intPartida + '))"><span class="glyphicon glyphicon-trash"></span></a>';
                    return btnEliminar;
                }
            }
            
        ],
        "initComplete": function () {
            $('#table_productos_info').html(
                ''
            );
            $('.precio').priceFormat({
                prefix: '$ ',
                centsSeparator: '.',
                thousandsSeparator: ','
            });
        }
    });

}




function Guardar() {
    if ($("#aspnetForm").valid()) {

        //var intCotizacion = $('#intCotizacion').val();
        var strCliente = $('#txtCliente').val();
        var strObra = $('#txtObra').val();
        var strElemento = $('#txtElemento').val();
        var datFechaColado = $('#txtFechaColado').val();
        var strTipoConcreto = $('#txtTipoConcreto').val();
        var strResistencia = $('#txtResistencia').val();
        var intRevenimiento = $('#txtRevenimiento').val() == '' ? '0' : $('#txtRevenimiento').val();
        var intAgregado = $('#txtAgregado').val() == '' ? '0' : $('#txtAgregado').val();
        var intTipo = $('#txtTipo').val() == '' ? '0' : $('#txtTipo').val();
        var strExtras = $('#txtExtras').val();
        var strExtras2 = $('#txtExtras2').val();
        var intTiro = $('#txtTiro').val() == '' ? '0' : $('#txtTiro').val();

        var ent = {
            intCotizacion: intCotizacion,
            strCliente: strCliente,
            strObra: strObra,
            strElemento: strElemento,
            datFechaColado: datFechaColado,
            strTipoConcreto: strTipoConcreto,
            strResistencia: strResistencia,
            intRevenimiento: intRevenimiento,
            intAgregado: intAgregado,
            intTipo: intTipo,
            strExtras: strExtras,
            strExtras2: strExtras2,
            intTiro: intTiro
            
        }
        var urlData = 'Cotizacion.aspx/Save';
        var dataData = '{ ent:' + JSON.stringify(ent) + '}';        CallMethod(urlData, dataData, SuccessGuardar);
    }
}

function SuccessGuardar(response) {
    var message = response.d[0];
    if (message == "ok") {
        intCotizacion = response.d[1];
        alert('Datos guardados correctamente.');
        GetCotizacion();
        GuardarProducto();
    }
    else {
        alert('Error al guardar los datos.')
    }
}

function GuardarProducto() {
    var intProducto = $('#ddlProductos').val();
    if (intProducto == '' || intProducto == null)
        intProducto = 0;

    if (intProducto > 0) {
        var decPrecio = $('#txtPrecio').val();
        var decCantidad = $('#txtCantidad').val();
        decCantidad = decCantidad.replace(/[^0-9\.]+/g, "");
        decPrecio = decPrecio.replace(/[^0-9\.]+/g, "");
        decCantidad = parseFloat(decCantidad);
        decPrecio = parseFloat(decPrecio);
        var entDet = {
            intPartida: intPartida,
            intCotizacion: intCotizacion,
            intProducto: intProducto,
            intTipo: intTipo,
            decCantidad: decCantidad,
            decPrecio: decPrecio,
        }

        var urlData = 'Cotizacion.aspx/SaveProducto';
        var dataData = '{ entDet:' + JSON.stringify(entDet) + '}';        CallMethod(urlData, dataData, SuccessGuardarProducto);
    }

}

function SuccessGuardarProducto(response) {
    var message = response.d[0];
    if (message == "ok") {
        GetCotizacion();
        GetCotizacionDetalle();
        Clear();
    }
    else {
        //alert('Error al generar las facturas.')
    }
}


function EliminarPartida(intPartida) {
    if (confirm('¿Está seguro que desea eliminar el producto?')) {
        var urlData = 'Cotizacion.aspx/EliminarProducto';
        var dataData = '{ id:"' + intCotizacion + '", partida:"' + intPartida + '" }';        CallMethod(urlData, dataData, SuccessDelProd);
    } else {
        return false;
    }
}

function SuccessDelProd(response) {
    var message = response.d[0];
    var data = response.d[1];
    if (message == "ok") {
        alert("Producto eliminado correctamente")
        GetCotizacion();
        GetCotizacionDetalle();
    } else {
        alert(data)
    }
}


function CambiarProducto() {
    var id = $("#ddlProductos").val();
    var result = $.grep(arrProductos, function (e) { return e.intProducto == id; });
    var decPrecio = 0;
    if (result.length > 0) {
        var product = result[0];
        decPrecio = product.dblPrecio;
        intTipo = product.intTipo;
    }
    decPrecio = decPrecio.toFixed(2);
    $('#txtPrecio').val(decPrecio);
    CalcularTotalProducto();
}

function CalcularTotalProducto()
{
    var decPrecio = $('#txtPrecio').val();
    var decCantidad = $('#txtCantidad').val();
    
    decCantidad = decCantidad.replace(/[^0-9\.]+/g, "");
    decPrecio = decPrecio.replace(/[^0-9\.]+/g, "");
    decCantidad = parseFloat(decCantidad);
    decPrecio = parseFloat(decPrecio);
    
    var decTotal = decPrecio * decCantidad;
    decTotal = decTotal.toFixed(2);

    $('#txtTotal').val(decTotal);
    $('.precio').priceFormat({
        prefix: '$ ',
        centsSeparator: '.',
        thousandsSeparator: ','
    });

}



function Clear()
{
    intPartida = 0;
    GetListProductos();
    $('#txtPrecio').val('0');
    $('#txtCantidad').val('0');
    $('#txtTotal').val('0');
    $('.precio').priceFormat({
        prefix: '$ ',
        centsSeparator: '.',
        thousandsSeparator: ','
    });
}


function Preview() {
    if (intCotizacion > 0) {
        var urlData = 'Cotizacion.aspx/Preview';
        var dataData = '{ intCotizacion:"' + intCotizacion + '" }';        CallMethod(urlData, dataData, SuccessPreview);
    }
}

function SuccessPreview(response) {
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
