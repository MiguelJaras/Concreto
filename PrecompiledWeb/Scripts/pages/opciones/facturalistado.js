$(document).ready(function () {
    BuscarFacturas();
});
function BuscarFacturas() {
    var urlData = 'FacturaListado.aspx/GetList';
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
    if ($.fn.DataTable.isDataTable('#table_facturas')) {
        $('#table_facturas').dataTable().fnDestroy();
    }
    var data = JSON.parse(data);
    var table = $('#table_facturas').DataTable({
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
              { className: "text-center", "targets": [0, 1, 2, 4, 5, 6] },
        ],
        columns: [
            { data: "strEmpresa", title: "Empresa" },
            { data: "strFactura", title: "Factura" },
            { data: "Pedidos", title: "Pedidos" },
            { data: "dblImporte", title: "Importe" },
            {
                title: 'Factura',
                data: null,
                bSortable: false,
                render: function (o) {
                    return '<a href="Javascript:OpenFilePDF(\'' + o.strPDF + '\',\'factura\')">' + o.strPDF + '</a>';
                }
            },
            {
                title: 'Factura XML',
                data: null,
                bSortable: false,
                render: function (o) {
                    return '<a href="Javascript:OpenFilePDF(\'' + o.strXML + '\',\'xml\')">' + o.strXML + '</a>';
                }
            },
            {
                title: 'Remisión',
                data: null,
                bSortable: false,
                render: function (o) {
                    return '<a href="Javascript:OpenFilePDF(\'' + o.strRemision + '\',\'factura\')">' + o.strRemision + '</a>';
                }
            },
            { data: "datFechaAlta", title: "Fecha de Alta" },
        ],
    });
}

function OpenFilePDF(fileName, type) {
    window.open("../../Utils/DisplayFile.aspx?filename=" + fileName + '&type=' + type);
}