<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="NotaCredito.aspx.cs" Inherits="Pages_Admin_NotaCredito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../../Scripts/file/fileinput.css" rel="stylesheet" />
    <script src="../../Scripts/file/fileinput.js"></script>
    <script src="../../Scripts/file/es.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            $("#ctl00_BodyContent_filePDF").fileinput({
                'showRemove': false,
                'showUpload': false,
                'showPreview': false,
                'allowedFileExtensions': ["pdf"],
                'required': true,
                'language': "es",
            });

            $("#ctl00_BodyContent_fileXML").fileinput({
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
                case 'ctl00_BodyContent_filePDF':
                    __doPostBack('NotaPDF', '');
                    break;
                case 'ctl00_BodyContent_fileXML':
                    __doPostBack('NotaXML', '');
                    break;
                
                default:
                    break;
            }
        }

        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <h2>Alta de Nota de Crédito</h2>
    <hr />
    <div class="row form-horizontal">
        <div class="form-group">
            <label class="col-md-2 control-label">Nota de Crédito (PDF)</label>
            <div class="col-md-6">
                <asp:FileUpload runat="server" ID="filePDF" CssClass="fileUpload" />
                <asp:Label runat="server" ID="lblFilePDF"></asp:Label>
            </div>
        </div>

        <div class="form-group">
            <label class="col-md-2 control-label">Nota de Crédito (XML)</label>
            <div class="col-md-6">
                <asp:FileUpload runat="server" ID="fileXML" CssClass="fileUpload" />
                <asp:Label runat="server" ID="lblFileXML"></asp:Label>
            </div>
        </div>

        <div class="form-group">
            <label class="col-md-2 control-label">Folio</label>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtFolio" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <div class="form-group">
            <label class="col-md-2 control-label">Serie</label>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtSerie" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-2 control-label">Factura</label>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtFactura" CssClass="form-control"></asp:TextBox>
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
            <label class="col-md-2 control-label">Importe</label>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtImporte" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-2">
            <asp:LinkButton runat="server" ID="btnGuardar" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardar_Click"></asp:LinkButton>
        </div>
    </div>
</asp:Content>

