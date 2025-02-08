$(document).ready(function () {

    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());


    $('#mdlConsumo').on("hidden.bs.modal", function () {
        $('#aspnetForm').parsley().destroy();
        $('#aspnetForm').parsley(); 
    });

    $('#ctl00_BodyContent_txtFechaInicio').datepicker({
        dateFormat: 'dd/mm/yy',     
    });

    $('#ctl00_BodyContent_txtFechaFin').datepicker({
        dateFormat: 'dd/mm/yy',
     
    });


    $('#txtFechaConsumo').datepicker({
        autoHide: true,
        zIndex: 2048,
        dateFormat: 'dd/mm/yy',   
    });

    $('#hdnIntMaterial').val(0);
    FiltroConsumo();
});



function FiltroConsumo() {

    var strFechaInicio = $('#ctl00_BodyContent_txtFechaInicio').val();
    var strFechaFin = $('#ctl00_BodyContent_txtFechaFin').val();

    var urlData = 'CapturaConsumo.aspx/GetList';
    var dataData = '{strFechaInicio:\'' + strFechaInicio + '\', strFechaFin: \'' + strFechaFin + '\'}';
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

    if ($.fn.DataTable.isDataTable('#table_consumo')) {
        $('#table_consumo').dataTable().fnDestroy();
    }

    var table = $('#table_consumo').DataTable({
        paging: true,
        responsive: true,
        language: {
            url: "../../Scripts/dataTables/js/Spanish.js"
        },
        order: [6, "desc"],
        data: data,

        columns: [

            { data: "datFecha", title: "Fecha" },
            { data: "intPlanta", title: "Planta" },
            { data: "decConcPremM3", title: "CONC_PREM M3" },
            { data: "decAguaLto", title: "AGUA LTO" },
            { data: "decArena4Ton", title: "ARENA 4 TON" },
            { data: "decArena5Ton", title: "ARENA 5 TON" },
            { data: "decCal", title: "CAL" },
            { data: "decCalFlow100", title: "CAL FLOW 100" },
            { data: "decCampOx4060", title: "CAMP 0X40/60" },
            { data: "decCamcret", title: "CAMCRET" },
            { data: "decCamptard", title: "CAMP TARD" },
            { data: "decCementoTon", title: "CEMENTO TON" },
            { data: "decFibraBolsa", title: "FIBRA BOLSA" },
            { data: "decColorByFerrox", title: "COLOR BY FERROX" },
            { data: "decGrava1Ton", title: "GRAVA 1 TON" },
            { data: "decGrava2Ton", title: "GRAVA 2 TON" },
            { data: "decImper", title: "IMPER" },
            { data: "decMortardELto", title: "MORTARD E LTO" },
            {
                data: null,
                bSortable: false,
                render: function (data, type, object, row) {
                    var index = row.row;
                    return '<a href="Javascript:Editar(' + index + ');"><span class="glyphicon glyphicon-pencil"></span></span></a>';
                }
            },
        ],
        responsive: true,


    });
}

function Guardar() {
    try {
        if ($('#aspnetForm').parsley().validate()) {
            var _intMaterial = $('#hdnIntMaterial').val();
            var arr = $('#txtFechaConsumo').val().split('/');
           // var _datFecha = $('#txtFechaConsumo').val();
            var _datFecha = arr[2] + "/" + arr[0] + "/" + arr[1];;          
            var _intPlanta = $('#numPlanta').val();
            var _decConcPremM3 = $('#txtConcPremM3').val();
            var _decAguaLto = $('#txtAguaLto').val();
            var _decArena4Ton = $('#txtArena4Ton').val();
            var _decArena5Ton = $('#txtArena5Ton').val();
            var _decCal = $('#txtCal').val();
            var _decCalFlow100 = $('#txtCalFlow100').val();
            var _decCampOx4060 = $('#txtCampOx4060').val();
            var _decCamcret = $('#txtCamcret').val();
            var _decCamptard = $('#txtCamptard').val();
            var _decCementoTon = $('#txtCementoTon').val();
            var _decFibraBolsa = $('#txtFibraBolsa').val();
            var _decColorByFerrox = $('#txtColorByFerrox').val();
            var _decGrava1Ton = $('#txtGrava1Ton').val();
            var _decGrava2Ton = $('#txtGrava2Ton').val();
            var _decImper = $('#txtImper').val();
            var _decMortardELto = $('#txtMortardELto').val();

            ent = {
                IntMaterial: _intMaterial,
                StrDatFecha: _datFecha,
                IntPlanta: _intPlanta,
                DecConcPremM3: _decConcPremM3,
                DecAgualto: _decAguaLto,
                DecArena4Ton: _decArena4Ton,
                DecArena5Ton: _decArena5Ton,
                DecCal: _decCal,
                DecCalFlow100: _decCalFlow100,
                DecCampOx4060: _decCampOx4060,
                DecCamcret: _decCamcret,
                DecCamptard: _decCamptard,
                DecCementoTon: _decCementoTon,
                DecFibraBolsa: _decFibraBolsa,
                DecColorByFerrox: _decColorByFerrox,
                DecGrava1Ton: _decGrava1Ton,
                DecGrava2Ton: _decGrava2Ton,
                DecImper: _decImper,
                DecMortardELto: _decMortardELto
            }


            var urlData = 'CapturaConsumo.aspx/Save';
            var dataData = '{ ent: ' + JSON.stringify(ent) + '}';
            CallMethod(urlData, dataData, ScsSave);

        }
    }
    catch (e) {
        alert(e);
    }
}


function ScsSave(response) {
    var message = response.d[0];
    var data = response.d[1];
    if (message == "ok") {
        ClearForm();
        $('#mdlConsumo').modal('toggle');
        alert('Datos guardados correctamente.');
        FiltroConsumo();


    } else {
        alert('Error al guardar los datos')
    }
}

function Editar(index) {

    var table = $('#table_consumo').DataTable();
    var row = table.row(index);
    var data = row.data();
    $('#hdnIntMaterial').val(data.intMaterial);
    $('#txtFechaConsumo').val(data.datFecha);
    $('#numPlanta').val(data.intPlanta);
    $('#txtConcPremM3').val(data.decConcPremM3);
    $('#txtAguaLto').val(data.decAguaLto);
    $('#txtArena4Ton').val(data.decArena4Ton);
    $('#txtArena5Ton').val(data.decArena5Ton);
    $('#txtCal').val(data.decCal);
    $('#txtCalFlow100').val(data.decCalFlow100);
    $('#txtCampOx4060').val(data.decCampOx4060);
    $('#txtCamcret').val(data.decCamcret);
    $('#txtCamptard').val(data.decCamptard);
    $('#txtCementoTon').val(data.decCementoTon);
    $('#txtFibraBolsa').val(data.decFibraBolsa);
    $('#txtColorByFerrox').val(data.decColorByFerrox);
    $('#txtGrava1Ton').val(data.decGrava1Ton);
    $('#txtGrava2Ton').val(data.decGrava2Ton);
    $('#txtImper').val(data.decImper);
    $('#txtMortardELto').val(data.decMortardELto); 
    $('#mdlConsumo').modal('show');
}


function ClearForm() {
    $('#hdnIntMaterial').val(0);
    $('#txtFechaConsumo').val('');
    $('#numPlanta').val('');
    $('#txtConcPremM3').val('');
    $('#txtAguaLto').val('');
    $('#txtArena4Ton').val('');
    $('#txtArena5Ton').val('');
    $('#txtCal').val('');
    $('#txtCalFlow100').val('');
    $('#txtCampOx4060').val('');
    $('#txtCamcret').val('');
    $('#txtCamptard').val('');
    $('#txtCementoTon').val('');
    $('#txtFibraBolsa').val('');
    $('#txtColorByFerrox').val('');
    $('#txtGrava1Ton').val('');
    $('#txtGrava2Ton').val('');
    $('#txtImper').val('');
    $('#txtMortardELto').val('');
}
