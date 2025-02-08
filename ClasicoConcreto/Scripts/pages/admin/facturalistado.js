$(document).ready(function () {
    GetFacturas();
});

function GetFacturas() {
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
        autoWidth: false,
        searching: true,
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
              { className: "text-center", "targets": [0, 1, 2, 4, 5, 6, 7] },
              { className: "text-right", "targets": [3] },
              { "width": "10%", "targets": 2 },
              {
                  render: function (data, type, full, meta) {
                      return "<div class='text-wrap width-400'>" + data + "</div>";
                  }, "targets": [2]
              },
        ],
        columns: [
            { data: "strEmpresa", title: "Empresa" },
            { data: "strFactura", title: "Factura" },
            { data: "Pedidos", title: "Remisiones" },

            { data: "dblImporte", title: "Importe", render: $.fn.dataTable.render.number(',', '.', 2, '$ ') },
            {
                title: 'Factura',
                data: null,
                bSortable: false,
                render: function (o) {
                    return '<a class="link" onclick="Javascript:OpenFilePDF(\'' + o.strFactura + '\',\'' + o.strPDF + '\',\'factura\')">' + o.strPDF + '</a>';
                }
            },
            {
                title: 'Factura XML',
                data: null,
                bSortable: false,
                render: function (o) {
                    return '<a class="link" onclick="Javascript:OpenFilePDF(\'' + o.strFactura + '\',\'' + o.strXML + '\',\'xml\')">' + o.strXML + '</a>';
                }
            },
            //{
            //    title: 'Remisión',
            //    data: null,
            //    bSortable: false,
            //    render: function (o) {
            //        return '<a class="link" onclick="Javascript:OpenFilePDF(\'' + o.strFactura + '\',\'' + o.strRemision + '\',\'factura\')">' + o.strRemision + '</a>';
            //    }
            //},
            { data: "datFechaAlta", title: "Fecha de Alta" },
            //{
            //    data: "bEnvioEmail", title: "Envío de email",
            //    bSortable: false,
            //    render: function (data, type, row) {
            //        return (data == true) ? '<span class="glyphicon glyphicon-ok"></span>' : '<span class="glyphicon glyphicon-remove"></span>';
            //    }
            //},
            {
                data: null,
                bSortable: false,
                render: function (o) {
                    return '<a href="#table_partidas" onclick="JavaScript:Eliminar(' + o.intEmpresa + ',\'' + o.strFactura + '\', \'' + o.strSerie + '\')"><span class="glyphicon glyphicon-trash"></span></a>';
                }
            }
        ],
    });
}

function OpenFilePDF(factura, fileName, type) {
    var urlData = 'FacturaListado.aspx/SaveFacturaDescarga';
    var dataData = '{ strFactura: \'' + factura + '\', strPDF: \'' + fileName + '\'}';
    CallMethod(urlData, dataData, SuccessDataOpenFile);
    window.open("../../Utils/DisplayFile.aspx?filename=" + fileName + '&type=' + type);
}

function SuccessDataOpenFile(response) {
    var message = response.d[0];
    var data = response.d[1];
    if (message == "ok") {
        
    } else {
        alert('Error al cargar los datos')
    }
}

function Eliminar(intEmpresa, strFactura, strSerie)
{
    try {
        if (confirm('¿Está seguro de que desea eliminar la factura (' + strFactura + ')?')) {
            var urlData = 'FacturaListado.aspx/Eliminar';
            var dataData = '{ intEmpresa:' + intEmpresa + ', strFactura: \'' + strFactura + '\', strSerie:\'' + strSerie + '\'}';
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
function SuccessEliminar(response)
{
    var message = response.d[0];
    var data = response.d[1];
    if (message == "ok") {
        alert('Registro eliminado correctamente');
        GetFacturas();
    } else {
        alert(data);
    }
   
}
