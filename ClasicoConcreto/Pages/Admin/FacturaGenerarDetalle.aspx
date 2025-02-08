<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="FacturaGenerarDetalle.aspx.cs" Inherits="Pages_Admin_FacturaGenerarDetalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script>
        var interval;
        var intFactura;
        $(document).ready(function () {
            intFactura = getParameterByName("id");
            if (intFactura == null || intFactura == '')
            {
                window.location = 'FacturaGenerarListado.aspx';
                return;
            }

            GetFacturaGenerar();
            GetFacturaGenerarDetalle();
            

        });

        function GetFacturaGenerar() {
            var urlData = 'FacturaGenerarDetalle.aspx/Get';
            var dataData = '{ intFactura: ' + intFactura + '}';
            CallMethod(urlData, dataData, SuccessGetFacturaGenerar);
        }

        function SuccessGetFacturaGenerar(response) {
            var message = response.d[0];
            if (message == "ok") {

                

                var data = JSON.parse(response.d[1]);
                if (data.length > 0) {
                    $('#lblCliente').text(data[0].strCliente);
                    $('#lblConcepto').text(data[0].strConcepto);
                    $('#lblEstatus').text(data[0].strEstatus);

                    $('#lblSerie').text(data[0].strSerie);
                    $('#lblFolio').text(data[0].decFolio);
                    $('#lblError').text(data[0].strError);

                    $('#lblUsoCFDI').text(data[0].strUsoCFDI + ' ' + data[0].strUsoCFDIDesc);
                    $('#lblFormaPago').text(data[0].strFormaPago + ' ' + data[0].strFormaPagoDesc);
                    $('#lblDescuento').text(parseFloat(data[0].decDescuento).toFixed(2));

                    $('#lblMetodoPago').text(data[0].strMetodoPago + ' ' + data[0].strMetodoPagoDesc);

                    if (data[0].intEstatus == 1) {
                        $('#btnProcesar').removeClass('hide');
                    } else {
                        $('#btnProcesar').addClass('hide');
                    }

                    if (data[0].intEstatus == 3) {
                        if (interval) {
                            clearInterval(interval);
                            interval = null;
                        }
                    }
                }

            } else {
                alert('Error al cargar los datos')
            }
        }

        function GetFacturaGenerarDetalle() {
            var urlData = 'FacturaGenerarDetalle.aspx/GetDetalle';
            var dataData = '{ intFactura: ' + intFactura + '}';
            CallMethod(urlData, dataData, SuccessFacturaGenerarDetalle);
        }

        function SuccessFacturaGenerarDetalle(response) {
            var message = response.d[0];
            if (message == "ok") {
                var data = JSON.parse(response.d[1]);

                if (data.length > 0) {

                    $('#lblSubTotalFac').text(parseFloat(data[0].dblSubTotalFac).toFixed(2));
                    $('#lblIvaFac').text(parseFloat(data[0].dblIvaFac).toFixed(2));
                    $('#lblTotalFac').text(parseFloat(data[0].dblTotalFac).toFixed(2));
                    $('#lblDescuentoFac').text(parseFloat(data[0].dblDescuentoFac).toFixed(2));

                    $('.precio').priceFormat({
                        prefix: '$ ',
                        centsSeparator: '.',
                        thousandsSeparator: ','
                    });

                    CreateTable(data);
                }

            } else {
                alert('Error al cargar los datos')
            }
        }


        function CreateTable(data) {
            if ($.fn.DataTable.isDataTable('#table_pedidos')) {
                $('#table_pedidos').dataTable().fnDestroy();
            }
            var table = $('#table_pedidos').DataTable({
                searching: false,
                bLengthChange: false,
                paging: false,
                pageLength: 20,
                responsive: true,
                orderCellsTop: true,
                language: {
                    url: "../../Scripts/dataTables/js/Spanish.js"
                },
                
                data: data,
                columnDefs: [
                      { className: "text-center", "targets": [0, 1, 4] },
                      { className: "text-right", "targets": [2, 3, 5,6,7] },
                ],
                columns: [

                    { data: "strNombre", title: "Producto" },
                    { data: "dblCantidad", title: "Cantidad" },
                    { data: "dblPrecio", title: "Precio", render: $.fn.dataTable.render.number(',', '.', 2, '$ ') },
                    { data: "dblSubtotal", title: "Total", render: $.fn.dataTable.render.number(',', '.', 2, '$ ') },

                    { data: "dblPorcDescuento", title: "% Desc.", render: $.fn.dataTable.render.number(',', '.', 2, '') },
                    { data: "dblDescuento", title: "Descuento", render: $.fn.dataTable.render.number(',', '.', 2, '$ ') },
                    { data: "dblIva", title: "Iva", render: $.fn.dataTable.render.number(',', '.', 2, '$ ') },
                    { data: "dblTotal", title: "Total", render: $.fn.dataTable.render.number(',', '.', 2, '$ ') },


                ],
                "initComplete": function () {
                    $('#table_pedidos_info').html(
                        ''
                    );
                }
            });
        }



        function Procesar()
        {
            var url = 'FacturaGenerarDetalle.aspx/Procesar';
            var data = '{';
            data += 'intFactura:' + intFactura
            data += '}';

            CallMethod(url, data, SuccessProcesar);
        }

        function SuccessProcesar(response) {
            var message = response.d[0];
            if (message == "ok") {

                alert('Datos guardados correctamente.');
                GetFacturaGenerar();

                interval = setInterval(function () {
                    GetFacturaGenerar();
                }, 15000);


                //var data = response.d[1];
            }
            else {
                alert('Error al procesar la factura.')
            }
        }


    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <h2>Generar Factura</h2>
    <hr />
    
    <div class="panel panel-primary">
        <div class="panel-heading">Datos Generales</div>
        <div class="panel-body form-horizontal">
            <div class="form-group">
                <label class="col-md-2 control-label">Cliente</label>
                <div class="col-md-4">
                    <label id="lblCliente" class="form-control"></label>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">Concepto</label>
                <div class="col-md-4">
                    <label id="lblConcepto" class="form-control"></label>
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-2 control-label">Uso CFDI</label>
                <div class="col-md-4">
                    <label id="lblUsoCFDI" class="form-control"></label>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">Forma de Pago</label>
                <div class="col-md-4">
                    <label id="lblFormaPago" class="form-control"></label>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">Metodo de Pago</label>
                <div class="col-md-4">
                    <label id="lblMetodoPago" class="form-control"></label>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">% Descuento</label>
                <div class="col-md-4">
                    <label id="lblDescuento" class="form-control"></label>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-2 control-label">Estatus</label>
                <div class="col-md-2">
                    <label id="lblEstatus" class="form-control"></label>
                </div>
                <div class="col-md-4">
                    <label id="lblError" class="form-control danger"></label>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">Folio</label>
                <div class="col-md-1">
                    <label id="lblSerie" class="form-control"></label>
                </div>
                <div class="col-md-2">
                    <label id="lblFolio" class="form-control"></label>
                </div>
            </div>

            
            <div class="form-group">
                <label class="col-md-2 control-label"></label>
                <div class="col-md-4">
                    
                    <a href="JavaScript:Procesar();" id="btnProcesar" class="btn btn-primary hide">Procesar</a>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-sm-10 col-md-10 col-lg-10">
            <table id="table_pedidos" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">
                
            </table>
        </div>
    </div>
    <div class="row">
        <div class="form-group">
            <div class="col-md-8 text-right"><label for="">SubTotal</label></div>
            <div class="col-md-2">
                <label id="lblSubTotalFac" class="form-control precio"></label>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-8 text-right"><label for="">Desc</label></div>
            <div class="col-md-2">
                
                <label id="lblDescuentoFac" class="form-control precio"></label>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-8 text-right"><label for="">Iva</label></div>
            <div class="col-md-2">
                
                <label id="lblIvaFac" class="form-control precio"></label>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-8 text-right"><label for="">Total</label></div>
            <div class="col-md-2">
                
                <label id="lblTotalFac" class="form-control precio"></label>
            </div>
        </div>
    </div>
</asp:Content>

