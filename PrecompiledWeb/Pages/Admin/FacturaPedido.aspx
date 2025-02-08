<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="FacturaPedido.aspx.cs" Inherits="Pages_Admin_FacturaPedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../../Scripts/file/fileinput.css" rel="stylesheet" />
    <script src="../../Scripts/file/fileinput.js"></script>
    <script src="../../Scripts/file/es.js"></script>
    
    <script type="text/javascript">

        $(document).ready(function () {

            $("#ctl00_BodyContent_fileFacturaPDF").fileinput({
                'showRemove': false,
                'showUpload': false,
                'showPreview': false,
                'allowedFileExtensions': ["pdf"],
                'required': true,
                'language': "es",
            });

            $("#ctl00_BodyContent_fileFacturaXML").fileinput({
                'showRemove': false,
                'showUpload': false,
                'showPreview': false,
                'allowedFileExtensions': ["xml"],
                'required': true,
                'language': "es",
            });
            $('.fileUpload').on('fileselect', function (event, numFiles, label) {
                Upload(this.id);
            });

            $('.precio').priceFormat({
                prefix: '$ ',
                centsSeparator: '.',
                thousandsSeparator: ','
            });


        });

        function Upload(Id) {
            switch (Id) {
                case 'ctl00_BodyContent_fileFacturaPDF':
                    __doPostBack('FacturaPDF', '');
                    break;
                case 'ctl00_BodyContent_fileFacturaXML':
                    __doPostBack('FacturaXML', '');
                    break;
                default:
                    break;
            }
        }

        function Validar() {

            var facturaPDF = $('#ctl00_BodyContent_lblFacturaPDF').text();
            var facturaXML = $('#ctl00_BodyContent_lblFacturaXML').text();
            if (facturaPDF == '' || facturaXML == '') {
                alert('Favor de seleccionar la factura.')
                return false;
            }
            //if ($('#ctl00_BodyContent_txtPedido').val() == '')
            //{
            //    alert('Favor de ingresar un pedido.');
            //    return false;
            //}


            return true;

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <h2>Alta de Facturas</h2>
    <hr />

    <div class="panel panel-primary">
        <div class="panel-heading">Datos Generales</div>
        <div class="panel-body form-horizontal">
            <div class="form-group">
                <label class="col-md-2 control-label">Factura (PDF)</label>
                <div class="col-md-6">
                    <asp:FileUpload runat="server" ID="fileFacturaPDF" CssClass="fileUpload" />
                    <asp:Label runat="server" ID="lblFacturaPDF"></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-2 control-label">Factura (XML)</label>
                <div class="col-md-6">
                    <asp:FileUpload runat="server" ID="fileFacturaXML" CssClass="fileUpload" />
                    <asp:Label runat="server" ID="lblFacturaXML"></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-2 control-label">Factura</label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtFactura" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-2 control-label">Remisión</label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtPedido" CssClass="form-control" onkeypress="return KeyPressOnlyInteger(event, this, 10)"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">SubTotal</label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtSubtotal" CssClass="form-control precio"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-2 control-label">Iva</label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtIva" CssClass="form-control precio"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-2 control-label">Total</label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtTotal" CssClass="form-control precio"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-2">
                    
                    <asp:LinkButton runat="server" ID="btnGuardar" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardar_Click"></asp:LinkButton>
                </div>
            </div>
            
        </div>
    </div>
</asp:Content>

