$(document).ready(function () {
    //$('#ctl00_BodyContent_ddlClientes').change(function () {
    //    GetPedidos()
    //});
    GetPedidos();
    $('#ctl00_BodyContent_txtFechaInicio').datepicker({
        dateFormat: 'dd/mm/yy',
    });

    $('#ctl00_BodyContent_txtFechaFin').datepicker({
        dateFormat: 'dd/mm/yy',
    });

});

function GetPedidos() {
    var intCliente = $('#ctl00_BodyContent_ddlClientes').val();
    var strFechaInicio = $('#ctl00_BodyContent_txtFechaInicio').val();;
    var strFechaFin = $('#ctl00_BodyContent_txtFechaFin').val();;

    var urlData = 'PedidoListadoImpresion.aspx/GetList';
    var dataData = '{ intCliente:"' + intCliente + '", strFechaInicio:\'' + strFechaInicio + '\', strFechaFin: \'' + strFechaFin + '\' }';
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
    if ($.fn.DataTable.isDataTable('#table_pedidos')) {
        $('#table_pedidos').dataTable().fnDestroy();
    }
    var data = JSON.parse(data);
    var table = $('#table_pedidos').DataTable({
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
              { className: "text-center", "targets": [0, 1, 3, 4, 5] },
              {
                  'targets': 0,
                  'searchable': false,
                  'orderable': false,
                  'className': 'dt-body-center',
                  'render': function (data, type, full, meta) {
                      return '<input type="checkbox" name="id[]" checked="" value="'
                         + data + '">';
                  }
              },
        ],
        columns: [

            { data: "intPedido", title: "#" },
            { data: "intPedido", title: "Remisión" },
            { data: "datFechaEntrega", title: "Fecha Entrega" },
            { data: "strCliente", title: "Cliente" },
            { data: "City", title: "Ciudad" },
            { data: "Estatus", title: "Estatus" },
            { data: "datFechaAlta", title: "Fecha Alta" },
            
        ],
        "createdRow": function (row, data, dataIndex) {
            
        }
    });
}

function Print()
{
    var dataTable = $('#table_pedidos').DataTable();


    var values = new Array();
    $.each($("input[name='id[]']:checked"), function () {
        values.push($(this).val());
    });
    if (values != '') {
        var urlData = 'PedidoListadoImpresion.aspx/Imprimir';
        var dataData = '{ strPedidos:"' + values + '" }';        CallMethod(urlData, dataData, SuccessPrint);
    }
}

function SuccessPrint(response) {
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

