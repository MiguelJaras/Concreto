$(document).ready(function () {
    GetNotas();
});


function GetNotas() {
    var urlData = 'NotaCreditoListado.aspx/GetList';
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
    var data = JSON.parse(data);

    if ($.fn.DataTable.isDataTable('#table_notas')) {
        //$('#table_notas').dataTable().fnDestroy();
        var datatable = $('#table_notas').DataTable();
        datatable.clear();
        datatable.rows.add(data);
        datatable.draw();
    }
    else {
        $('#table_notas').DataTable({
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
                  { className: "text-center", "targets": [0, 1, 3, 4, 5] },
            ],
            columns: [
                { data: "strEmpresa", title: "Empresa" },
                { data: "strFolio", title: "Folio" },
                { data: "dblImporte", title: "Importe" },
                {
                    title: 'Nota de Crédito',
                    data: null,
                    bSortable: false,
                    render: function (o) {
                        return '<a class="link" onclick="Javascript:OpenFilePDF(\'' + o.strPDF + '\',\'ncpdf\')">' + o.strPDF + '</a>';
                    }
                },
                {
                    title: 'Nota de Crédito XML',
                    data: null,
                    bSortable: false,
                    render: function (o) {
                        return '<a class="link" onclick="Javascript:OpenFilePDF(\'' + o.strXML + '\',\'ncxml\')">' + o.strXML + '</a>';
                    }
                },
                { data: "datFechaAlta", title: "Fecha de Alta" },
                {
                    data: null,
                    bSortable: false,
                    render: function (o) {
                        return '<a href="#table_partidas" onclick="JavaScript:Eliminar(\'' + o.strFolio + '\')"><span class="glyphicon glyphicon-trash"></span></a>';
                    }
                }
            ],
        });
    }
}

function OpenFilePDF(fileName, type) {
    window.open("../../Utils/DisplayFile.aspx?filename=" + fileName + '&type=' + type);
}


function Eliminar(strFolio) {
    try {
        if (confirm('¿Está seguro de que desea eliminar la nota de crédito (' + strFolio + ')?.')) {
            var urlData = 'NotaCreditoListado.aspx/Eliminar';
            var dataData = '{ strFolio:\'' + strFolio + '\'}';
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
        alert('Registro eliminado correctamente');
        GetNotas();
    } else {
        alert(data);
    }

}
