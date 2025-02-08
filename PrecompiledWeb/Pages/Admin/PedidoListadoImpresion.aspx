<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="PedidoListadoImpresion.aspx.cs" Inherits="Pages_Admin_PedidoListadoImpresion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="../../Scripts/pages/admin/pedidolistadoImpresion.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <h2>Impresión de Pedidos</h2>
    <hr />
    <div class="row form-group">
        <div class="col-md-4">
            <label class="control-label">Clientes</label>
            <asp:DropDownList runat="server" ID="ddlClientes" CssClass="btn dropdown-toggle btn-default">
            </asp:DropDownList>
        </div>
        <div class="col-md-2">
            <label class="control-label">Fecha Inicio</label>
            <asp:TextBox runat="server" ID="txtFechaInicio" ReadOnly="true" CssClass="form-control"></asp:TextBox>
        </div>
        
        <div class="col-md-2">
            <label class="control-label">Fecha Fin</label>
            <asp:TextBox runat="server" ID="txtFechaFin" ReadOnly="true" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-2">
            <label class="control-label">&nbsp</label>
            <input type="button" value="Filtrar" onclick="GetPedidos();" class="btn btn-primary btn-block" />
        </div>
        <div class="col-md-2">
            <label class="control-label">&nbsp</label>
            <input type="button" value="Imprimir" onclick="Print();" class="btn btn-success btn-block" />
        </div>
    </div>
   
    <div class="row">
        <div class="col-sm-12 col-md-12 col-lg-12">
            <table id="table_pedidos" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">
                
            </table>
        </div>
    </div>
</asp:Content>

