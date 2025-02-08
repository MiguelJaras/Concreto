$(document).ready(function () {
    GetList();
});


function GetList() {
    var urlData = 'Clientes.aspx/GetList';
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
    if ($.fn.DataTable.isDataTable('#table_clientes')) {
        $('#table_clientes').dataTable().fnDestroy();
    }
    var data = JSON.parse(data);
    var table = $('#table_clientes').DataTable({
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
              { className: "text-center", "targets": [0, 2, 3, 4, 5] },
              { "width": "10%", "targets": [0, 2, 3] },
        ],
        columns: [

            { data: "intCliente", title: "#" },
            { data: "strNombre", title: "Producto" },
            { data: "strEmail", title: "Email" },
            { data: "ListaNombre", title: "Lista de Precios" },
            {
                data: "intActivo", title: "Estatus",
                bSortable: false,
                render: function (data, type, row) {
                    return (data == 1) ? '<span class="glyphicon glyphicon-ok"></span>' : '<span class="glyphicon glyphicon-remove"></span>';
                }
            },

            {
                data: null,
                bSortable: false,
                render: function (data, type, object, row) {
                    var index = row.row;
                    return '<a href="Javascript:Editar(' + index + ');"><span class="glyphicon glyphicon-pencil"></span></a>&nbsp&nbsp&nbsp' + '<a href="#table_partidas" onclick="JavaScript:Delete(' + data.intCliente + ')"><span class="glyphicon glyphicon-trash"></span></a>';
                }
            }
        ],
    });
}

function Editar(index) {

    var table = $('#table_clientes').DataTable();
    var row = table.row(index);
    var data = row.data();
    var intCliente = data.intCliente;
    var intPrecioLista = data.intLista;
    var strNombre = data.strNombre;
    var strEmail = data.strEmail;
    var bEstatus = true;
    var bPrecioEditable = data.bPrecioEditable;
    if (data.intActivo == 0) {
        bEstatus = false;
    }
    Clear();
    $('#titPopUp').text('Editar Cliente');
    $('#txtNombre').val(strNombre);
    $('#txtEmail').val(strEmail);
    $('#ddlListaPrecio').val(intPrecioLista);
    $('#chkActivo').prop('checked', bEstatus);
    $('#chkEditable').prop('checked', bPrecioEditable);
    $('#hdnIntCliente').val(intCliente);
    $.magnificPopup.open({
        items: [
            {
                type: 'inline',
                src: $('#PopUpCliente')
            }
        ],
    });
}


function Clear() {
    $('#txtNombre').val('');
    $('#txtEmail').val('');
    $('#ddlListaPrecio').val('2');
    $('#chkActivo').prop('checked', true);
    $('#chkEditable').prop('checked', false);
    $('#hdnIntCliente').val(0);
}

function Agregar() {
    $('#titPopUp').text('Agregar Cliente');
    $.magnificPopup.open({
        items: [
            {
                type: 'inline',
                src: $('#PopUpCliente')
            }
        ],
    });
}

function CerrarPopUp() {
    Clear();
    var magnificPopup = $.magnificPopup.instance;
    magnificPopup.close();
}


function Save() {
    try {
        var intCliente = $('#hdnIntCliente').val();
        var strNombre = escape($('#txtNombre').val());
        var strEmail = escape($('#txtEmail').val());
        var intLista = escape($('#ddlListaPrecio').val());
        var bEditable = $('#chkEditable').prop('checked');
        var bActivo = $('#chkActivo').prop('checked');
        var intActivo = 0;
        if(bActivo)
            intActivo = 1;

        var urlData = 'Clientes.aspx/Save';
        var objCliente = {
            intCliente: intCliente,
            strNombre: strNombre,
            strEmail: strEmail,
            intLista: intLista,
            bPrecioEditable: bEditable,
            intActivo: intActivo

        }

        var dataData = '{ ent: ' + JSON.stringify(objCliente) + '}';
        CallMethod(urlData, dataData, SuccessDataSave);
    }
    catch (e) {
        alert(e);
    }
}

function SuccessDataSave(response) {
    var message = response.d[0];
    if (message == "ok") {
        CerrarPopUp();
        alert('Datos guardados correctamente.');
        GetList();
    } else {
        alert('Error al guardar los datos')
    }
}




function Delete(intCliente) {
    try {
        if (confirm('¿Está seguro de que desea eliminar el cliente?')) {

            var urlData = 'Clientes.aspx/Delete';
            var dataData = '{ intCliente: ' + intCliente + '}';
            CallMethod(urlData, dataData, SuccessDataDelete);
        }
        else {
            return false;
        }
    }
    catch (e) {
        alert(e);
    }
}


function SuccessDataDelete(response) {
    var message = response.d[0];
    if (message == "ok") {
        alert('Cliente eliminado correctamente.');
        GetList();
    } else {
        alert('Error al guardar los datos')
    }
}
