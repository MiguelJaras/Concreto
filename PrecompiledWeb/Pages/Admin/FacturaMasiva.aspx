<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="FacturaMasiva.aspx.cs" Inherits="Pages_Admin_FacturaMasiva" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../../Scripts/file/fileinput.css" rel="stylesheet" />
    <script src="../../Scripts/file/fileinput.js"></script>
    <script src="../../Scripts/file/es.js"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {

            $("#files").fileinput({
                'showRemove': false,
                'showUpload': false,
                'showPreview': false,
                'allowedFileExtensions': ["pdf","xml"],
                'required': true,
                'language': "es",
            });

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">

    <h2>Alta de Facturas</h2>
    <hr />
    <div class="row form-horizontal">
        <div class="form-group">
            <label class="col-md-2 control-label">Archivos</label>
            <div class="col-md-6">
                <input type="file" name="files" class="fileUpload" id="files" multiple="multiple" onselect="DisplayFiles()" onchange="DisplayFiles()"/>
            </div>
            <div class="col-md-4">
                <asp:Button runat="server" ID="btnGuardarArchivos" Text="Guardar" OnClick="btnGuardarArchivos_Click" EnableViewState="false" CssClass="btn" />
            </div>
        </div>
    </div>
    
    <asp:GridView runat="server" ID="gvValidaFactura" CssClass="table table-striped table-bordered dt-responsive nowrap table-style1" AutoGenerateColumns="false" EnableViewState="false">
        <Columns>
            <asp:TemplateField HeaderText="#">
                <ItemTemplate>
                    <%# (Container.DataItemIndex + 1)%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Factura" DataField="strFactura" />
            <asp:TemplateField HeaderText="Mensaje">
            <ItemTemplate>
                <div class='<%# (bool)Eval("esError") ? "ui-state-error-text" : "" %>'>
                    <asp:Repeater ID="rptError" runat="server" DataSource='<%# Eval("lstError") %>'>
                        <ItemTemplate>
                            <span style='color:'><%# (Container.ItemIndex + 1)+"."+ Container.DataItem  %></span><br />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

