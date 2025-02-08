<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="FacturaAnticipadaPedidos.aspx.cs" Inherits="Pages_Admin_FacturaAnticipadaPedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

    <link href="../../Scripts/select2/css/select2.min.css" rel="stylesheet" />
    <link href="../../Scripts/select2/css/select2-bootstrap.min.css" rel="stylesheet" />
    <script src="../../Scripts/select2/js/select2.min.js"></script>
    <script>
        var dataFacturas = [];
        $(document).ready(function () {
            GetFacturas();
            $("#ddlFactura").select2({

            });
            $('#ctl00_BodyContent_txtFechaInicio').datepicker({
                dateFormat: 'dd/mm/yy',
            });

            $('#ctl00_BodyContent_txtFechaFin').datepicker({
                dateFormat: 'dd/mm/yy',
            });
            BuscarPedidos();

        });

        function GetFacturas() {
            
            var urlData = 'FacturaAnticipadaPedidos.aspx/GetListFacturas';
            var dataData = '{ }';
            CallMethod(urlData, dataData, SuccessGetFacturas);
        }

        function SuccessGetFacturas(response) {
            var message = response.d[0];
            var data = response.d[1];
            if (message == "ok") {
                dataFacturas = JSON.parse(data);
                FillSelect('ddlFactura', dataFacturas, 'strFactura', 'strFactura', true, '', '--Seleccione--');
            } else {
                alert('Error al cargar los datos')
            }
        }
        
        function GetData(strFactura)
        {
            var obj = $.grep(dataFacturas, function (e) { return e.strFactura == strFactura; });
        
            var intEmpresa = obj[0].intEmpresa;
            var strSerie = obj[0].strSerie;
            var strFactura = obj[0].strFactura;
            var dblSubTotal = obj[0].dblSubTotal.toFixed(2);
            var dblIva = obj[0].dblIva.toFixed(2);
            var dblImporte = obj[0].dblImporte.toFixed(2);
            
            $('#hdnEmpresa').val(intEmpresa);
            $('#hdnSerie').val(strSerie);
            $('#hdnFactura').val(strFactura);
            $('#txtSubtotal').val(dblSubTotal);
            $('#txtIva').val(dblIva);
            $('#txtTotal').val(dblImporte);
            $('.precio').priceFormat({
                prefix: '$ ',
                centsSeparator: '.',
                thousandsSeparator: ','
            });
        }

        function StorePedidos() {
            $('#ctl00_BodyContent_hdnPedidos').val(GetValuesPedidos());
        }

        function BuscarPedidos() {


            var strFechaInicio = $('#ctl00_BodyContent_txtFechaInicio').val();;
            var strFechaFin = $('#ctl00_BodyContent_txtFechaFin').val();;
            var urlData = 'FacturaAnticipadaPedidos.aspx/GetListPedidos';
            var dataData = '{ strFechaInicio:\'' + strFechaInicio + '\', strFechaFin: \'' + strFechaFin + '\'}';
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


            if ($.fn.DataTable.isDataTable('#table_pedidos')) {
                var datatable = $('#table_pedidos').DataTable();
                datatable.clear();
                datatable.rows.add(data);
                datatable.draw();
            }
            else {
                $('#table_pedidos').DataTable({
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
                          { className: "text-center", "targets": [0, 1, 2, 6] },
                          { className: "text-right", "targets": [7] },
                          { width: "10%", "targets": 3 }
                    ],
                    columns: [
                        {
                            data: null,
                            bSortable: false,
                            render: function (o) {
                                var options = '<input type="checkbox" name="chkPedido" id="chkPedido" onchange="StorePedidos();" value="' + o.intPedido + '">';
                                return options;
                            }
                        },
                        { data: "intPedido", title: "#" },
                        { data: "PO_Num", title: "OC" },
                        { data: "strCliente", title: "Cliente" },
                        { data: "City", title: "Ciudad" },
                        { data: "Estatus", title: "Estatus" },
                        { data: "datFechaAlta", title: "Fecha Alta" },
                        { data: "dblTotal", title: "Total", render: $.fn.dataTable.render.number(',', '.', 2, '$ ') },

                    ],
                    "initComplete": function () {
                        $('#table_pedidos_info').html(
                            ''
                        );
                        CheckPedidos();
                    }
                });
            }
        }

        function CheckPedidos() {
            var dataTable = $('#table_pedidos').DataTable();
            var pedidos = $('#ctl00_BodyContent_hdnPedidos').val();
            
            if (pedidos != '') {
                var arrPedidos = pedidos.split(',');
                for (var i = 0; i <= arrPedidos.length; i++) {
                    dataTable.rows().nodes().to$().find('input[value="' + arrPedidos[i] + '"]').prop("checked", true);
                }
            }
        }

        function GetValuesPedidos() {
            var values = new Array();
            var dataTable = $('#table_pedidos').DataTable();
            dataTable.rows().nodes().to$().find('input[type="checkbox"]').each(function () {
                if (this.checked) {
                    values.push($(this).val());
                }
            });

            var strValues = values.join(",");
            return strValues;
        }

        function Save() {
           
            try {
                var strFactura = $('#hdnFactura').val();
                var strPedidos = GetValuesPedidos();
                

                if (strFactura == '') {
                    alert('Favor de seleccionar una factura.');
                    return false;
                }
                if (strPedidos == '') {
                    alert('Favor de seleccionar un pedido.');
                    return false;
                }

                var intEmpresa = $('#hdnEmpresa').val();
                var strSerie = $('#hdnSerie').val();
                
               
                var dblSubTotal = $('#txtSubtotal').priceToFloat()
                var dblImporte = $('#txtTotal').priceToFloat()


                var urlData = 'FacturaAnticipadaPedidos.aspx/Save';
                var obj = {
                    intEmpresa: intEmpresa,
                    strSerie: strSerie,
                    strFactura: strFactura,
                    strPedidos: strPedidos,
                    dblSubTotal: dblSubTotal,
                    dblImporte: dblImporte
                }
                
                var dataData = '{ ent: ' + JSON.stringify(obj) + '}';
                CallMethod(urlData, dataData, SuccessSave);
            }
            catch (e) {
                alert(e);
            }
        }
        function SuccessSave(response) {
            var message = response.d[0];
            var data = response.d[1];
            if (message == "ok") {
                alert('Datos guardados correctamente.');
                window.location = 'FacturaAnticipadaPedidos.aspx'
            } else {
                alert(data);
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <asp:HiddenField runat="server" ID="hdnPedidos" />
    <input id="hdnEmpresa" type="hidden" value="0" />
    <input id="hdnSerie" type="hidden" value="" />
    <input id="hdnFactura" type="hidden" value="" />
    <h2>Factura Anticipada Remisión</h2>
    <hr />
    <div class="row form-horizontal">
        <div class="form-group">
            <label class="col-md-2 control-label">Factura</label>
            <div class="col-md-2">
                <select id="ddlFactura" class="form-control" onchange="GetData(this.value);">

                </select>
            </div>
        </div>
       
        <div class="form-group">
            <label class="col-md-2 control-label">SubTotal</label>
            <div class="col-md-2">
                <input id="txtSubtotal" class="form-control precio" readonly="" />
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-2 control-label">Iva</label>
            <div class="col-md-2">
                <input id="txtIva" class="form-control precio" readonly="" />
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-2 control-label">Total</label>
            <div class="col-md-2">
                <input id="txtTotal" class="form-control precio" readonly="" />
            </div>
        </div>
    </div>
    <div class="row form-horizontal">
        <div class="form-group ">
            <label class="col-md-2 control-label">Fecha Inicio</label>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtFechaInicio" ReadOnly="true" CssClass="form-control"></asp:TextBox>
            </div>
            <label class="col-md-1 control-label">Fecha Fin</label>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtFechaFin" ReadOnly="true" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2"><input type="button" onclick="$('#ctl00_BodyContent_hdnPedidos').val(''); BuscarPedidos();" value="Filtrar" class="btn btn-primary btn-block" /></div>
            <div class="col-md-2"><input type="button" class="btn btn-primary btn-block" onclick="Save();" value="Guardar" /></div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <table id="table_pedidos" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">
                
                </table>
            </div>
        </div>
    </div>
</asp:Content>

