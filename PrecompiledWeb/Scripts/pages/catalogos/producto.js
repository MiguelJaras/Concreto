$(document).ready(function () {
    BuscarProductos();
});

function BuscarProductos() {
    var urlData = 'Producto.aspx/GetList';
    var dataData = '{ }';
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
    if ($.fn.DataTable.isDataTable('#table_productos')) {
        $('#table_productos').dataTable().fnDestroy();
    }
    var data = JSON.parse(data);
    var table = $('#table_productos').DataTable({
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
              { className: "text-center", "targets": [0, 2, 3] },
              { "width": "10%", "targets": [0, 2, 3] },
        ],
        columns: [

            { data: "intProducto", title: "#" },
            { data: "strNombre", title: "Producto" },
            {
                data: "bEstatus", title: "Estatus",
                bSortable: false,
                render: function (data, type, row) {
                    return (data == true) ? '<span class="glyphicon glyphicon-ok"></span>' : '<span class="glyphicon glyphicon-remove"></span>';
                }
            },

            {
                data: null,
                bSortable: false,
                render: function (data, type, object, row) {
                    var index = row.row;
                    return '<a href="Javascript:Editar(' + index + ');"><span class="glyphicon glyphicon-pencil"></span></a>&nbsp&nbsp&nbsp' + '<a href="#table_partidas" onclick="JavaScript:Eliminar(' + data.intProducto + ')"><span class="glyphicon glyphicon-trash"></span></a>';
                }
            }
        ],
    });
}

function Clear() {
    $('#txtProducto').val('');
    $('#chkActivo').prop('checked', false);
    $('#hdnIntProducto').val(0);
}

function Agregar() {
    $('#titPopUp').text('Agregar Producto');
    $.magnificPopup.open({
        items: [
            {
                type: 'inline',
                src: $('#PopUpProducto')
            }
        ],
    });
}

function Editar(index) {

    var table = $('#table_productos').DataTable();
    var row = table.row(index);
    var data = row.data();

    var intProducto = data.intProducto;
    var strNombre = data.strNombre;
    var bEstatus = data.bEstatus;
    Clear();
    $('#titPopUp').text('Editar Producto');
    $('#txtProducto').val(strNombre);
    $('#chkActivo').prop('checked', bEstatus);
    $('#hdnIntProducto').val(intProducto);
    $.magnificPopup.open({
        items: [
            {
                type: 'inline',
                src: $('#PopUpProducto')
            }
        ],
    });
}

function CerrarPopUp() {
    Clear();
    var magnificPopup = $.magnificPopup.instance;
    magnificPopup.close();
}

function Guardar() {
    try {
        var intProducto = $('#hdnIntProducto').val();
        var strProducto = escape($('#txtProducto').val());
        var bActivo = $('#chkActivo').prop('checked');
        var urlData = 'Producto.aspx/Guardar';
        var dataData = '{ intProducto:' + intProducto + ', strProducto:\'' + strProducto + '\', bActivo:' + bActivo + ' }';
        CallMethod(urlData, dataData, SuccessGuardar);
    }
    catch (e) {
        alert(e);
    }
}

function SuccessGuardar(response)
{
    var message = response.d[0];
    var data = response.d[1];
    if (message == "ok") {
        CerrarPopUp();
        alert('Datos guardados correctamente.');
        BuscarProductos();
    } else {
        alert('Error al guardar los datos')
    }
}

function Eliminar(intProducto) {
    try {
        if (confirm('¿Está seguro de que desea eliminar el producto?')) {
            var urlData = 'Producto.aspx/Eliminar';
            var dataData = '{ intProducto:' + intProducto + '}';
            CallMethod(urlData, dataData, SuccessEliminar);
        }
        else {
            return false;
        }
    }
    catch (e) {
        alert(e);
    }
}

function SuccessEliminar(response) {
    var message = response.d[0];
    var data = response.d[1];
    if (message == "ok") {
        alert('Producto eliminado correctamente.');
        CerrarPopUp();
        BuscarProductos();
    } else {
        alert('Error al eliminar el producto')
    }
}