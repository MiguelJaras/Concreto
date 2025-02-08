<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="Clientes.aspx.cs" Inherits="Pages_Admin_Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript" src="../../Scripts/pages/admin/clientes.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <h2>Alta de Clientes</h2>
    <hr />


    <div>
        <input type="button" class="btn btn-primary" value="Agregar" onclick="Agregar();" />
    </div>
    <div class="row">
        <div class="col-lg-12">
            <table id="table_clientes" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">
                
            </table>
        </div>
    </div>

    <div id="PopUpCliente" class="white-popup-producto mfp-hide">
        <h2 id=""><label id="titPopUp"></label></h2>
        <div class="form-group">
            <label for="txtNombre">Nombre</label>
            <input type="text" id="txtNombre" class="form-control" value="" />
            <input type="hidden" id="hdnIntCliente" class="form-control" value="0" />
        </div>

        <div class="form-group">
            <label for="txtEmail">Correo</label>
            <input type="text" id="txtEmail" class="form-control" value="" />
            
        </div>
        <div class="form-group">
            <label for="ddlListaPrecio">Lista de Precios
                <select id="ddlListaPrecio" class="dropdown-toggle btn-default form-control">
                    <option value="2" selected>Público General</option>
                    <option value="1">GAMA</option>
                </select>
            </label>
        </div>

        <div class="form-group">
            <label for="chkEditable">Precio Editable
                <input type="checkbox" class="checkbox" id="chkEditable" />
            </label>
        </div>

        <div class="form-group">
            <label for="chkActivo">Activo
                <input type="checkbox" class="checkbox" id="chkActivo" />
            </label>
        </div>

        <div class="text-center">
            <button type="button" onclick="CerrarPopUp();" class="btn btn-danger">Cancelar</button>
            <button type="button" onclick="Save();" class="btn btn-success">Guardar</button>
        </div>
    </div>


</asp:Content>

