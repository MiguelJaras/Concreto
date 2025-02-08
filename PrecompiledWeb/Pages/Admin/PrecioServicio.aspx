<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="PrecioServicio.aspx.cs" Inherits="Pages_Admin_PrecioServicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ctl00_BodyContent_ddlListaPrecio').on("change", function (event) {
                BuscarPrecios();
            });
            BuscarPrecios();
        });

        function BuscarPrecios() {
            var intLista = $('#ctl00_BodyContent_ddlListaPrecio').val();
            var urlData = 'PrecioServicio.aspx/GetList';
            var dataData = '{ intLista:' + intLista + ' }';
            CallMethod(urlData, dataData, SuccessData);
        }
        function SuccessData(response) {
            var message = response.d[0];
            var data = response.d[1];
            if (message == "ok") {

                if ($.fn.DataTable.isDataTable('#table_precios')) {
                    $('#table_precios').dataTable().fnDestroy();
                }
                data = JSON.parse(data);
                var table = $('#table_precios').DataTable({
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
                        { className: "text-center", "targets": [0, 1, 3] },
                        { className: "text-right", "targets": [2] },
                    ],
                    columns: [
                        { data: "Lista", title: "Lista" },
                        { data: "strServicio", title: "Servicio" },
                        { data: "dblPrecio", title: "Precio" },
                        
                        {
                            data: null,
                            bSortable: false,
                            render: function (data, type, object, row) {
                                var index = row.row;
                                var options = '';
                                options = '<a href="#" onclick=JavaScript=PopUpEdit(' + index + ')><span class="glyphicon glyphicon-pencil"></span></a>&nbsp&nbsp&nbsp'

                                return options;
                            }
                        }
                    ],

                });


            } else {
                alert('Error al cargar los datos')
            }
        }

        function PopUpEdit(index) {
            var table = $('#table_precios').DataTable();
            var row = table.row(index);
            var data = row.data();

            $('#hdnIntLista').val(data.intLista);
            $('#txtLista').val(data.Lista);
            $('#hdnIntServicio').val(data.intServicio);
            $('#txtServicio').val(data.strServicio);
            $('#txtPrecio').val(data.dblPrecio);

            $.magnificPopup.open({
                items: [
                    {
                        type: 'inline',
                        src: $('#PopUpPrecio')
                    }
                ],
            });
        }

        function CerrarPopUp() {
            var magnificPopup = $.magnificPopup.instance;
            magnificPopup.close();
        }

        function Guardar() {
            try {
                var intLista = $('#hdnIntLista').val();
                var intServicio = $('#hdnIntServicio').val();
                var dblPrecio = $('#txtPrecio').val();
                
                var url = 'PrecioServicio.aspx/Save';
                var data = '{';
                data += 'intLista:' + intLista
                data += ',intServicio:' + intServicio
                data += ',dblPrecio:' + dblPrecio
                data += '}';

                CallMethod(url, data, SuccessSave);
            }
            catch (e) {
                alert(e);
            }
        }

        function SuccessSave(response) {
            var message = response.d[0];
            var data = response.d[1];
            if (message == "ok") {
                alert('Datos guardados correctamente.')
                BuscarPrecios();
                CerrarPopUp();
            }
            else {
                alert('Error al guardar los datos')
            }
        }



    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <h2>Listado de Servicios </h2>
    <hr />
    <div class="form-group">
        <label class="col-md-2 control-label">Lista de Servicios</label>
        <div class="col-md-4">
            <asp:DropDownList runat="server" ID="ddlListaPrecio" CssClass="btn dropdown-toggle btn-default">
            </asp:DropDownList>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <table id="table_precios" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">
                
            </table>
        </div>
    </div>

    <div id="PopUpPrecio" class="white-popup mfp-hide">
        <div class="form-group">
            <label for="txtLista">Lista</label>
            <input type="text" id="txtLista" class="form-control" value="" readonly="" />
            <input type="hidden" id="hdnIntLista" class="form-control" value="0" />
        </div>
        <div class="form-group">
            <label for="txtServicio">Servicio</label>
            <input type="text" id="txtServicio" class="form-control" value="" readonly="" />
            <input type="hidden" id="hdnIntServicio" class="form-control" value="0" />
        </div>
        <div class="form-group">
            <label for="txtPrecio">Precio</label>
            <input type="text" id="txtPrecio" class="form-control" value="" onkeypress="return KeyPressOnlyDecimal(event,this,10,2)" />
        </div>
        <div class="text-center">
            <button type="button" onclick="CerrarPopUp();" class="btn btn-danger">Cancelar</button>
            <button type="button" onclick="Guardar();" class="btn btn-success">Aceptar</button>
        </div>
    </div>

</asp:Content>

