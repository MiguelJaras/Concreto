$(document).ready(function () {

    $('#ctl00_BodyContent_txtFechaInicio').datepicker({
        dateFormat: 'dd/mm/yy',
    });

    $('#ctl00_BodyContent_txtFechaFin').datepicker({
        dateFormat: 'dd/mm/yy',
    });


    $('#ctl00_BodyContent_ddlClientes').change(function () {
        BuscarPedidos()
    });
    BuscarPedidos();
});

function BuscarPedidos() {

    var strFechaInicio = $('#ctl00_BodyContent_txtFechaInicio').val();
    var strFechaFin = $('#ctl00_BodyContent_txtFechaFin').val();

    var intCliente = $('#ctl00_BodyContent_ddlClientes').val();
    var urlData = 'PedidoListado.aspx/GetList';
    var dataData = '{ intCliente:"' + intCliente + '", strFechaInicio:\'' + strFechaInicio + '\', strFechaFin: \'' + strFechaFin + '\'}';
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
              { className: "text-center", "targets": [0, 1, 2, 7, 8] },
              { className: "text-left", "targets": [3] },
              { className: "text-right", "targets": [4, 5, 6] },
        ],
        columns: [

            { data: "intPedido", title: "#" },
            { data: "PO_Num", title: "Orden de Compra" },
            { data: "datFechaEntrega", title: "Fecha de Entrega" },
            { data: "strCliente", title: "Cliente" },
            //{ data: "City", title: "Ciudad" },
            { data: "dblSubtotal", title: "SubTotal", render: $.fn.dataTable.render.number(',', '.', 2, '$ ') },
            { data: "dblIva", title: "Iva", render: $.fn.dataTable.render.number(',', '.', 2, '$ ') },
            { data: "dblTotal", title: "Total", render: $.fn.dataTable.render.number(',', '.', 2, '$ ') },
            { data: "datFechaAlta", title: "Fecha Alta" },
            { data: "Estatus", title: "Estatus" },
            {
                data: null,
                bSortable: false,
                render: function (o) {
                    var options = '';
                    if (o.Ordenes > 0) {
                        options = '<a href="#" onclick=JavaScript=ModalOrdenes(' + o.intPedido + ')><span class="glyphicon glyphicon-list-alt"></span></a>&nbsp&nbsp&nbsp'
                    } else {
                        options = '<a href="#" class="invisible"><span class="glyphicon glyphicon-list-alt"></span></a>&nbsp&nbsp&nbsp'
                    }
                    options += '<a href=../Opciones/Pedido.aspx?id=' + o.intPedido + '><span class="glyphicon glyphicon-pencil"></span></a>';
                    return options;
                }
            },
            
        ],
        "createdRow": function( row, data, dataIndex){
            if( data.bRead == false){
                $(row).addClass('danger');
            }
        }
    });
}

function ModalOrdenes(intPedido) {
    var urlData = 'PedidoListado.aspx/GetDetallePedido';
    var dataData = '{ intPedido: ' + intPedido + ' }';
    CallMethod(urlData, dataData, SuccessDetallePedido);
}

function SuccessDetallePedido(response) {
    var message = response.d[0];
    var data = response.d[1];
    if (message == "ok") {
        var data = JSON.parse(data);
        var htmlContent = '<div id="detallePedido" class="white-popup"><table id="tabla_detalle" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%"></table></div>';
        $.magnificPopup.open({
            items: [
                {
                    type: 'inline',
                    src: $(htmlContent)
                }
            ],
        });
        var table = $('#tabla_detalle').DataTable({
            searching: false,
            bLengthChange: false,
            paging: false,
            responsive: true,
            orderCellsTop: false,
            language: {
                url: "../../Scripts/dataTables/js/Spanish.js"
            },
            //order: [[0, "desc"]],
            data: data,
            "columnDefs": [
                { className: "text-center", "targets": [0, 1, 2, 4, 5, 6] },
                { "orderable": false, "targets": [0, 1, 2, 3, 4] }
            ],
            columns: [

                { data: "OC", title: "Orden" },
                { data: "Truck_Code", title: "No. de Camión" },
                { data: "Ticket_Code", title: "Ticket" },
                { data: "Driver_Name", title: "Chofer" },
                { data: "Load_Size", title: "Cargado" },
                { data: "CreateDate", title: "Fecha de Creación" },
                { data: "DeliveredDate", title: "Fecha de Entrega" },
            ],
            "initComplete": function () {
                $('#tabla_detalle_info').html(
                    ''
                );
            }
        });

    } else {
        alert('Error al cargar los datos')
    }
}

function RowGroup(rowData) {
    var row_html = '<tr class="row-group"><td class="hide"></td></td>';
    row_html += '<td colspan="4">Orden: ' + rowData.Order_Code;
    row_html += '</tr>';
    return row_html;
}