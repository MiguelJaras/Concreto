<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="Precios.aspx.cs" Inherits="Pages_Admin_Precios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="../../Scripts/pages/admin/precios.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <h2>Alta de Precios</h2>
    <hr />

    <div id="PopUpPrecio" class="white-popup-producto mfp-hide">
        <h2 id=""><label id="titPopUp"></label></h2>
       
        <div class="form-group" style="padding-top: 30px" >
            <label for="txtEmpresa"  class="col-md-4 control-label" >Empresa</label>
            <div class="col-md-8">
                <asp:DropDownList runat="server" ID="ddlEmpresa" ClientIDMode="Static" class="form-control" disabled onchange="habilitar(this)">
                </asp:DropDownList>
            </div>
        </div>

        <div class="form-group" style="padding-top: 30px ">
            <label for="txtInsumo" class="col-md-4 control-label" >Insumo</label>
            <div class="col-md-8">
                <asp:DropDownList runat="server" ID="ddlInsumo"  ClientIDMode="Static" class="form-control" disabled onchange="habilitar(this)">
                </asp:DropDownList>
            </div>
        </div>

        <div class="form-group" style="padding-top: 30px ">
            <label for="txtProducto" class="col-md-4 control-label" >Producto</label>
            <div class="col-md-8">
                <asp:DropDownList runat="server" ID="ddlProducto"  ClientIDMode="Static" class="form-control" disabled onchange="habilitar(this)">
                </asp:DropDownList>
            </div>
        </div>

      
        <div class="form-group" style="padding-top: 30px">
            <label for="txtPrecio" class="col-md-4 control-label" >Precio</label>
             <div class="col-md-8">
                <asp:TextBox runat="server" ID="txtPrecio"   ClientIDMode="Static" class="form-control"  Text="$ 0.00" oninput="formatPriceInput(this)">
                     </asp:TextBox>
            </div> 
        </div>
        <div class="text-center"  style="padding-top: 30px">
            <button type="button" onclick="CerrarPopUp();" class="btn btn-danger">Cancelar</button>
            <button type="button" onclick="Guardar();" class="btn btn-success" >Guardar</button>
        </div>
    </div>

    <div>
        <input type="button" class="btn btn-primary" value="Agregar" onclick="Agregar();" />
    </div>
    <div class="row" >
        <div class="col-lg-12">
            <table id="table_precios" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">
                
            </table>
        </div>
    </div>

</asp:Content>
