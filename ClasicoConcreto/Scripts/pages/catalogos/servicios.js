$(document).ready(function () {
    BuscarServicios();
});

function BuscarServicios() {
    var urlData = 'Servicio.aspx/GetList';
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
    if ($.fn.DataTable.isDataTable('#table_servicios')) {
        $('#table_servicios').dataTable().fnDestroy();
    }
    var data = JSON.parse(data);
    var table = $('#table_servicios').DataTable({
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
              { className: "text-center", "targets": [0, 2] },
              { "width": "10%", "targets": [0, 2] },
        ],
        columns: [

            { data: "intServicio", title: "#" },
            { data: "strNombre", title: "Servicio" },
            {
                data: null,
                bSortable: false,
                render: function (data, type, object, row) {
                    var index = row.row;
                    return '<a href="Javascript:Editar(' + index + ');"><span class="glyphicon glyphicon-pencil"></span></a>&nbsp&nbsp&nbsp' + '<a href="#table_partidas" onclick="JavaScript:Eliminar(' + data.intServicio + ')"><span class="glyphicon glyphicon-trash"></span></a>';
                }
            }
        ],
    });
}

function Agregar() {
    $('#titPopUp').text('Agregar Servicio');
    $.magnificPopup.open({
        items: [
            {
                type: 'inline',
                src: $('#PopUpServicio')
            }
        ],
    });
}
function Editar(index) {

    var table = $('#table_servicios').DataTable();
    var row = table.row(index);
    var data = row.data();

    
    Clear();
    $('#titPopUp').text('Editar Servicio');
    $('#txtServicio').val(data.strNombre);
    $('#hdnIntServicio').val(data.intServicio);
    $.magnificPopup.open({
        items: [
            {
                type: 'inline',
                src: $('#PopUpServicio')
            }
        ],
    });
}
function CerrarPopUp() {
    Clear();
    var magnificPopup = $.magnificPopup.instance;
    magnificPopup.close();
}
function Clear() {
    $('#txtServicio').val('');
    $('#hdnIntServicio').val(0);
}


function Guardar() {
    try {
        var intServicio = $('#hdnIntServicio').val();
        var strServicio = $('#txtServicio').val();
        strServicio = escape(strServicio);
        var strPrecioBase = $('#txtPrecioBase').val();
        var urlData = 'Servicio.aspx/Guardar';
        var dataData = "{ intServicio:" + intServicio + ", strServicio:\'" + strServicio + "\', strPrecioBase:" + strPrecioBase + "}";
        CallMethod(urlData, dataData, SuccessGuardar);
    }
    catch (e) {
        alert(e);
    }
}

function SuccessGuardar(response) {
    var message = response.d[0];
    var data = response.d[1];
    if (message == "ok") {
        CerrarPopUp();
        alert('Datos guardados correctamente.');
        BuscarServicios();
    } else {
        alert('Error al guardar los datos')
    }
}


function Eliminar(intServicio) {
    try {
        if (confirm('¿Está seguro de que desea eliminar el servicio?')) {
            var urlData = 'Servicio.aspx/Eliminar';
            var dataData = '{ intServicio:' + intServicio + '}';
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
        alert('Servicio eliminado correctamente.');
        CerrarPopUp();
        BuscarServicios();
    } else {
        alert('Error al eliminar el servicio')
    }
}