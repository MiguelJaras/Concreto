$(document).ready(function () {
    Buscar(0);
});

function Buscar(idNotificacion) {
    var url = 'Notificacion.aspx/Data';
    var data = '';
    var arrDatos = BuscarDatos(url, data);
    if (arrDatos.message == "ok")
        GenerarGrid('Notificaciones', JSON.parse(arrDatos.data));
}
function GenerarGrid(caption, data) {
    try {
        var grid = $("#grid");
        grid.jqGrid('GridUnload');
        $grid = $("#grid").jqGrid
        (
            {
                datatype: 'local',
                data: data, //[{ "Factura": "62157", "Fecha": "2015-09-10T00:00:00", "Folio": 69420, "Total": 6994.8000, "Estatus": "Alta" }, { "Factura": "61138", "Fecha": "2015-09-10T00:00:00", "Folio": 69420, "Total": 489.6400, "Estatus": "Ingresa"}],                 
                colNames: [
                '#',
                'Notificacion',
                'Fecha Alta',
                'Opciones',
                '',
                ''
                ],
                colModel:
                    [
                        { 'index': 'intNotificacion', 'name': 'intNotificacion', 'width': 20, align: 'center', formatter: '', key: true, search: false },
                        { 'index': 'strNotificacion', 'name': 'strNotificacion', 'width': 200, align: 'left', search: true },
                        { 'index': 'datFechaAlta', 'name': 'datFechaAlta', 'width': 100, align: 'Center', sorttype: 'date', formatter: 'date', formatoptions: { srcformat: 'ISO8601Long', newformat: 'd/m/Y' }, search: false },
                        { name: '', index: 'Opciones', formatter: 'actions', width: '40', align: 'Center', formatoptions: { keys: true, editbutton: false, delbutton: false, }, search: false },
                        { 'index': 'strContenido', 'name': 'strContenido', 'hidden': true },
                        { 'index': 'intCantidadConfirm', 'name': 'intCantidadConfirm', 'hidden': true }
                    ],
                pager: "#pager", //Pager.
                loadtext: 'Loading...',
                recordtext: "{0} - {1} de {2} elements",
                emptyrecords: 'No hay resultados',
                pgtext: 'Pág: {0} de {1}', //Paging input control text format.
                rowNum: "20", // PageSize.
                rowList: [20, 50, 100], //Variable PageSize DropDownList. 
                viewrecords: true, //Show the RecordCount in the pager.
                multiselect: false,
                multiboxonly: true,
                hoverrows: false,
                //sortname: "Folio", //Default SortColumn
                //sortorder: "asc", //Default SortOrder.
                width: "600",
                //height: "400",
                caption: caption,
                gridview: true,
                ignoreCase: true,
                rownumbers: false,

                actionsNavOptions:
                {
                    deleteicon: "ui-icon-trash",
                    deletetitle: "Eliminar",
                    editaricon: "ui-icon-pencil",
                    editartitle: "Editar",
                    showicon: "ui-icon-search",
                    showtitle: "Ver",

                    custom:
                    [
                        {
                            action: "delete", onClick: function (options) {

                                var rowData = grid.jqGrid('getRowData', options.rowid);
                                var intCantidad = rowData.intCantidadConfirm;
                                
                                if (intCantidad == 0) {
                                    if (confirm('¿Desea eliminar el registro?')) {
                                        if (Eliminar(options.rowid)) {
                                            alert('Registro eliminado correctamente.');
                                        }
                                    }
                                } else {
                                    alert('No se puede eliminar el registro.');
                                }
                            }
                        },
                        {
                            action: "editar", onClick: function (options) {
                                location = 'NotificacionForm.aspx?idNot=' + options.rowid;
                            }
                        },
                        {
                            action: "show", onClick: function (options) {

                                var rowData = grid.jqGrid('getRowData', options.rowid);
                                var rowID = rowData.intNotificacion;
                                var rowHTMLContent = rowData.strContenido;
                                rowHTMLContent = unescape(rowHTMLContent);
                                rowHTMLTitle = rowData.strNotificacion;
                                var htmlContent = '<div id="notificaciones" class="white-popup"><div class="popup-scroll"><div class="not"><h2 class="tit">' + rowHTMLTitle + '</h2><hr>' + rowHTMLContent + '</div></div></div>';

                                $.magnificPopup.open({
                                    items: [
                                        {
                                            type: 'inline',
                                            src: $(htmlContent)
                                        }
                                    ],

                                });

                            }
                        }
                    ]

                },
                
            }
        ).navGrid('#pager', { edit: false, add: false, del: false, search: false })
        .navButtonAdd('#pager', {
            caption: "Agregar",
            buttonicon: "ui-icon-plus",
            onClickButton: function () {
                location = 'NotificacionForm.aspx';
            },
        })

        setSearchSelect('Estatus', grid);
        grid.jqGrid('filterToolbar', { stringResult: true, searchOnEnter: false, defaultSearch: "cn" });

    } //end try
    catch (oerror) {
        alert('Error Obtener ' + oerror);
    }
}

function Eliminar(idNot) {
    var grid = $('#grid');
    var process = false;
    var message, data, colmodel;
    
    try {
        $.ajax({
            type: "POST",
            url: "Notificacion.aspx/Eliminar",
            data: "{ 'idNotificacion':'" + idNot + "'}",
            contentType: "application/json; chartset:utf-8",
            dataType: "json",
            success:
            function (result) {
                if (result.d == 'ok') {
                    grid.jqGrid('delRowData', idNot);
                    if (grid[0].p.lastpage > 1) {
                        grid.trigger("reloadGrid", [{ page: grid[0].p.page }]);
                    }
                    process = true;
                } else {
                    alert(result.d);
                    process = false;
                }
            },
            error:
                    function (XmlHttpError, error, description) {
                        alert(XmlHttpError.responseText);
                    },
        });
    }
    catch (e) {
        alert('Error al eliminar ' + e);
        process = false;
    }

    return process;
}