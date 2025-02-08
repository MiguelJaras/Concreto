$(document).ready(function () {
    BuscarPrecios();
    var intempresa = 0;
    GetInsumos(intempresa);
});
function BuscarPrecios() {
    var urlData = 'Precios.aspx/GetList';
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
function formatPriceInput(input) {
    let cursorPosition = input.selectionStart; // Guarda la posición del cursor
    let value = input.value.replace(/[^0-9.]/g, ''); // Elimina caracteres no válidos

    // Permite solo un punto decimal
    const parts = value.split('.');
    if (parts.length > 2) {
        value = parts[0] + '.' + parts[1]; // Elimina puntos adicionales
    }

    // Actualiza el valor formateado solo si no está vacío
    input.value = value ? parseFloat(value).toFixed(2) : '';

    // Restaura la posición del cursor
    input.setSelectionRange(cursorPosition, cursorPosition);
}
function CreateTable(data) {
    if ($.fn.DataTable.isDataTable('#table_precios')) {
        $('#table_precios').dataTable().fnDestroy();
    }
    var data = JSON.parse(data);
    var table = $('#table_precios').DataTable({
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

            { data: "strInsumo", title: "Insumo" },
            { data: "strNombre", title: "Producto" },
            { data: "dblPrecio", title: "Precio" },
            //{
            //    data: "bEstatus", title: "Estatus",
            //    bSortable: false,
            //    render: function (data, type, row) {
            //        return (data == true) ? '<span class="glyphicon glyphicon-ok"></span>' : '<span class="glyphicon glyphicon-remove"></span>';
            //    }
            //},

            {
                data: null,
                bSortable: false,
                render: function (data, type, object, row) {
                    var index = row.row;
                    return '<a href="Javascript:Editar(' + index + ');"><span class="glyphicon glyphicon-pencil"></span></a>&nbsp&nbsp&nbsp' + '<a href="#table_partidas" onclick="JavaScript:Eliminar(' +index + ')"><span class="glyphicon glyphicon-trash"></span></a>';
                }
            }
        ],
    });
}

function Clear() {
    $('#ddlEmpresa').val('');
    $('#ddlInsumo').val('');
    $('#ddlProducto').val('');
    $('#txtPrecio').val('$ 0.00');
    document.getElementById("ddlEmpresa").disabled = true;
    document.getElementById("ddlInsumo").disabled = true;
    document.getElementById("ddlProducto").disabled = true;
    document.getElementById("txtPrecio").disabled = true;

}

function Agregar() {
    isEditMode = false;
    $('#titPopUp').text('Agregar Precio');
    Clear();
    document.getElementById("ddlEmpresa").disabled = false;
    $.magnificPopup.open({
        items: [
            {
                type: 'inline',
                src: $('#PopUpPrecio')
            }
        ],
    });
}

function Editar(index) {

    var table = $('#table_precios').DataTable();
    var row = table.row(index);
    var data = row.data();
    var intEmpresa = data.intEmpresa;
    var strInsumo = data.strInsumo;
    var intProducto = data.intProducto;
    var dblPrecio = data.dblPrecio;
    Clear();
    $('#titPopUp').text('Editar Producto');
    $('#ddlEmpresa').val(intEmpresa);
    GetInsumos(intEmpresa);
    $('#ddlInsumo').val(strInsumo);
    $('#ddlProducto').val(intProducto);
    $('#txtPrecio').val(dblPrecio);
    document.getElementById("txtPrecio").disabled = false;
    $.magnificPopup.open({
        items: [
            {
                type: 'inline',
                src: $('#PopUpPrecio')
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
        var intEmpresa = $('#ddlEmpresa').val();
        var strInsumo = $('#ddlInsumo').val();
        var intProducto = $('#ddlProducto').val();
        var dblPrecio = $('#txtPrecio').val();
        var dblPrecio = escape($('#txtPrecio').val());
        var urlData = 'Precios.aspx/Guardar';
        
        var dataData = '{ intEmpresa:' + intEmpresa + ', strInsumo:\'' + strInsumo + '\', intProducto:' + intProducto + ', dblPrecio:' + dblPrecio +'}';
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
        BuscarPrecios();
    } else {
        alert('Error al guardar los datos');
    }
}

function Eliminar(index) {
    try {
        if (confirm('¿Está seguro de que desea eliminar el precio del producto?')) {
            var table = $('#table_precios').DataTable();
            var row = table.row(index);
            var data = row.data();
            var intEmpresa = data.intEmpresa;
            var strInsumo = data.strInsumo;
            var intProducto = data.intProducto;
            var urlData = 'Precios.aspx/Eliminar';
            var dataData = '{ intEmpresa:' + intEmpresa + ', strInsumo:\'' + strInsumo + '\', intProducto:' + intProducto + '}';
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
        BuscarPrecios();
    } else {
        alert('Error al eliminar el producto');
    }
}
function habilitar(current) {
    // Obtén el valor del dropdown actual
    const selectedValue = current.value;

    // Si el valor es válido (no vacío), habilita el siguiente dropdown
    if (selectedValue) {
        // Busca el siguiente sibling que sea un dropdown y habilítalo
        const siguiente = current.closest('.form-group').nextElementSibling;
        if (siguiente) {
            const dropdown = siguiente.querySelector('.form-control');
            if (dropdown) {
                dropdown.disabled = false;
                if (current.id === 'ddlEmpresa') {
                    var intempresa = $('#ddlEmpresa').val();
                    GetInsumos(intempresa);
                }
                if (current.id === 'ddlProducto') {
                    document.getElementById("txtPrecio").disabled = false;
                }

            }
        }
    }
}
function GetInsumos(empresa) {
    var intempresa = empresa;
    var dataData = GetData("Precios.aspx/GetInsumo", '{intEmpresa:"' + intempresa + '"}', false);
    FillSelect('ddlInsumo', dataData, 'strInsumo', 'strNombre', true, '', '-- Seleccione --', '');
}

