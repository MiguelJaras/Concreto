<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="Producto.aspx.cs" Inherits="Pages_Catalogos_Producto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="../../Scripts/pages/catalogos/producto.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <h2>Alta de Productos</h2>
    <hr />

    <div id="PopUpProducto" class="white-popup-producto mfp-hide">
        <h2 id=""><label id="titPopUp"></label></h2>
        <div class="form-group">
            <label for="txtProducto">Producto</label>
            <input type="text" id="txtProducto" class="form-control" value="" />
            <input type="hidden" id="hdnIntProducto" class="form-control" value="0" />
        </div>
        <div class="form-group">
            <label for="chkActivo">Activo
                <input type="checkbox" class="" id="chkActivo" />
            </label>
        </div>
        <div class="text-center">
            <button type="button" onclick="CerrarPopUp();" class="btn btn-danger">Cancelar</button>
            <button type="button" onclick="Guardar();" class="btn btn-success">Guardar</button>
        </div>
    </div>

    <div>
        <input type="button" class="btn btn-primary" value="Agregar" onclick="Agregar();" />
    </div>
    <div class="row">
        <div class="col-lg-12">
            <table id="table_productos" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">
                
            </table>
        </div>
    </div>

</asp:Content>

