$(document).ready(function () {

    $('#ctl00_BodyContent_txtFecha').datepicker({
        dateFormat: 'dd/mm/yy',
    });
    GetProgramacion();

});

function GetProgramacion() {
    var strFecha = $('#ctl00_BodyContent_txtFecha').val();
    var urlData = 'ProgramacionConcreto.aspx/GetList';
    var dataData = '{ strFecha:"' + strFecha + '"}';
    CallMethod(urlData, dataData, SuccessDataProgramacion);
}

function SuccessDataProgramacion(response) {
    var message = response.d[0];
    var data = response.d[1];
    if (message == "ok") {
        CreateTable(data);
    } else {
        alert('Error al cargar los datos')
    }
}

function CreateTable(data) {

    data = JSON.parse(data);
    if ($.fn.DataTable.isDataTable('#table_programacion')) {
        var datatable = $('#table_programacion').DataTable();
        datatable.clear();
        datatable.rows.add(data);
        datatable.draw();
    }
    else {

        $('#table_programacion').DataTable({
            paging: true,
            language: {
                url: "../../Scripts/dataTables/js/Spanish.js"
            },
            order: [],
            data: data,
            "iDisplayLength": 15,
            columnDefs: [
                  { className: "text-center", "targets": [0, 1, 2, 3, 4, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22] },
            ],
            columns: [
                { data: "intRequisicionDet", title: "intRequisicionDet", visible: false },
                { data: "strObra", title: "Obra" },
                { data: "strColonia", title: "Colonia" },
                { data: "strManzana", title: "Manzana" },
                { data: "strLote", title: "Lote" },

                { data: "strComentario", title: "Comentario" },


                { data: "strRecibe", title: "Recibe" },


                { data: "strElemento", title: "Elemento" },
                { data: "Bombeo", title: "Bombeo" },

                { data: "Requisicion", title: "Requisicion" },
                { data: "OrdenCompra", title: "OrdenCompra" },
                { data: "FechaPedido", title: "FechaPedido" },
                { data: "FechaColado", title: "FechaColado" },
                { data: "07:00", title: "07:00" },
                { data: "07:30", title: "07:30" },
                { data: "08:00", title: "08:00" },
                { data: "08:30", title: "08:30" },
                { data: "09:00", title: "09:00" },
                { data: "09:30", title: "09:30" },
                { data: "10:00", title: "10:00" },
                { data: "10:30", title: "10:30" },
                { data: "11:00", title: "11:00" },
                { data: "11:30", title: "11:30" },
                { data: "12:00", title: "12:00" },
                { data: "12:30", title: "12:30" },
                { data: "13:00", title: "13:00" },
                { data: "13:30", title: "13:30" },
                { data: "14:00", title: "14:00" },
                { data: "14:30", title: "14:30" },
                { data: "15:00", title: "15:00" },
                { data: "15:30", title: "15:30" },
                { data: "16:00", title: "16:00" },
                { data: "16:30", title: "16:30" },
                { data: "17:00", title: "17:00" },
                { data: "17:30", title: "17:30" },
            ],
            responsive: false,
            "scrollX": true,

            buttons: [
                {
                    extend: "excelHtml5",
                    text: '<img src="../../Img/dxls.png" height="30" width="30"/>',
                    exportOptions: {
                        columns: [0,1,2,3]
                    }
                },
                {
                    extend: "pdfHtml5",
                    text: '<img src="../../Img/dpdf.png" height="30" width="30"/>',
                    //exportOptions: {
                    //    columns: [0]
                    //}
                },
                {
                    extend: "copyHtml5",
                    text: '<img src="../../Img/dcpy.png" height="30" width="30" />',
                },
            ],
            dom: "Bfrtip",
        });

        $('#table_programacion tbody').on('dblclick', 'tr td', function () {

            var column = $('#table_programacion thead tr th').eq($(this).index()).text().trim();
            PopUpEdit(this, column);
        })
    }

}

function CerrarPopUp() {
    var magnificPopup = $.magnificPopup.instance;
    magnificPopup.close();
}

function PopUpEdit(indexRow, column) {

    arrColumn = column.split(':');
    if (arrColumn.length == 2) {
        var table = $('#table_programacion').DataTable();
        var row = table.row(indexRow);
        var data = row.data();

        if (data[column] != null) {
            $('#hdnReqDet').val(data.intRequisicionDet);
            $('#txtObra').val(data.strObra);
            $('#txtColonia').val(data.strColonia);
            $('#txtManzana').val(data.strManzana);
            $('#txtLote').val(data.strLote);
            $('#txtElemento').val(data.strElemento);
            $('#ctl00_BodyContent_ddlHoras').val(column);
            $('#hdnHoraAnt').val(column);
            $.magnificPopup.open({
                items: [
                    {
                        type: 'inline',
                        src: $('#PopUp')
                    }
                ],
            });

        }

    }

}

function Guardar() {

    var intReqDet = $('#hdnReqDet').val();
    var strHoraAnt = $('#hdnHoraAnt').val();
    var strHoraNueva = $('#ctl00_BodyContent_ddlHoras').val();

    var urlData = 'ProgramacionConcreto.aspx/Save';
    var dataData = '{ intReqDet:' + intReqDet + ', strHoraAnt:\'' + strHoraAnt + '\', strHoraNueva: \'' + strHoraNueva + '\'}';
    CallMethod(urlData, dataData, SuccessGuardar);

}

function SuccessGuardar(response)
{
    var message = response.d[0];
    var data = response.d[1];
    if (message == "ok") {
        alert('Datos guardados correctamente.');
        GetProgramacion();
        CerrarPopUp();
    } else {
        alert('Error al guardar los datos')
    }
}

