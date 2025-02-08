<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="PedidoListado.aspx.cs" Inherits="Pages_Admin_PedidoListado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

    <script src="../../Scripts/pages/admin/pedidolistado.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">

    <h2>Administración de Pedidos</h2>
    <hr />

    <div class="row form-horizontal">
        <div class="col-lg-12">

            <div class="form-group">
                <label class="col-md-1 control-label">Clientes</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlClientes" CssClass="btn dropdown-toggle btn-default">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-1 control-label">Fecha Inicial</label>
                <div class="input-group col-md-2">
                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></span>
                    <asp:textbox ID="txtFechaInicio" runat="server" CssClass="form-control"></asp:textbox>
                </div>
            
            </div>
            <div class="form-group">
                <label class="col-md-1 control-label">Fecha Final</label>
                <div class="input-group col-md-2">
                    <span class="input-group-addon" id="Span1"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></span>
                    <asp:textbox ID="txtFechaFin" runat="server" CssClass="form-control"></asp:textbox>
                </div>

            </div>
            <div class="form-group ">
                <label class="col-md-1 control-label"></label>
                <div class="col-md-2">
                    <input type="button" class="btn btn-primary btn-block" value="Filtrar" onclick="BuscarPedidos();" />
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <table id="table_pedidos" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">
                
            </table>
        </div>
    </div>

</asp:Content>

