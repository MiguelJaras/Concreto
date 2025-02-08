<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="PrecioProducto.aspx.cs" Inherits="Pages_Admin_PrecioProducto" %>

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
            var urlData = 'PrecioProducto.aspx/GetList';
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
                        { className: "text-center", "targets": [0, 1, 5] },
                        { className: "text-right", "targets": [2, 3, 4] },
                    ],
                    columns: [
                        { data: "Lista", title: "Lista" },
                        { data: "strProducto", title: "Producto" },
                        { data: "dblMenudeo", title: "Menudeo" },
                        { data: "dblMedioMayoreo", title: "Medio Mayoreo" },
                        { data: "dblMayoreo", title: "Mayoreo" },
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

        function PopUpEdit(index)
        {
            var table = $('#table_precios').DataTable();
            var row = table.row(index);
            var data = row.data();
            
            $('#hdnIntLista').val(data.intLista);
            $('#txtLista').val(data.Lista);
            $('#hdnIntProducto').val(data.intProducto);
            $('#txtProducto').val(data.strProducto);
            $('#txtMenudeo').val(data.dblMenudeo);
            $('#txtMedioMayoreo').val(data.dblMedioMayoreo);
            $('#txtMayoreo').val(data.dblMayoreo);

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
            try
            {
                var intLista = $('#hdnIntLista').val();
                var intProducto = $('#hdnIntProducto').val();
                var dblMenudeo = $('#txtMenudeo').val();
                var dblMedioMayoreo = $('#txtMedioMayoreo').val();
                var dblMayoreo = $('#txtMayoreo').val();
           
                var url = 'PrecioProducto.aspx/Save';
                var data = '{';
                data += 'intLista:' + intLista
                data += ',intProducto:' + intProducto
                data += ',dblMenudeo:' + dblMenudeo
                data += ',dblMedioMayoreo:' + dblMedioMayoreo
                data += ',dblMayoreo:' + dblMayoreo
                data += '}';

                CallMethod(url, data, SuccessSave);
            }
            catch (e)
            {
                alert(e);
            }
            
        }

        function SuccessSave(response)
        {
            var message = response.d[0];
            var data = response.d[1];
            if (message == "ok"){
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
    <h2>Listado de Precios </h2>
    <hr />
    <div class="form-group">
        <label class="col-md-2 control-label">Lista de Precios</label>
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
            <label for="txtProducto">Producto</label>
            <input type="text" id="txtProducto" class="form-control" value="" readonly="" />
            <input type="hidden" id="hdnIntProducto" class="form-control" value="0" />
        </div>
        <div class="form-group">
            <label for="txtMenudeo">Menudeo</label>
            <input type="text" id="txtMenudeo" class="form-control" value="" onkeypress="return KeyPressOnlyDecimal(event,this,10,2)" />
        </div>
        <div class="form-group">
            <label for="txtMedioMayoreo">Medio Mayoreo</label>
            <input type="text" id="txtMedioMayoreo" class="form-control" value="" onkeypress="return KeyPressOnlyDecimal(event,this,10,2)" />
        </div>
        <div class="form-group">
            <label for="txtMayoreo">Mayoreo</label>
            <input type="text" id="txtMayoreo" class="form-control" value="" onkeypress="return KeyPressOnlyDecimal(event,this,10,2)" />
        </div>

        <div class="text-center">
            <button type="button" onclick="CerrarPopUp();" class="btn btn-danger">Cancelar</button>
            <button type="button" onclick="Guardar();" class="btn btn-success">Aceptar</button>
        </div>
    </div>
</asp:Content>

