<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="FacturaListado.aspx.cs" Inherits="Pages_Admin_FacturaListado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript" src="../../Scripts/pages/admin/facturalistado.js"></script>
    <style>
        
        .text-wrap{
            white-space:normal;
        }
        .width-400{
            width:400px;
        }


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <h2>Listado de Facturas</h2>
    <hr />
    <div>
        <a href="Factura.aspx" class="btn btn-primary">
            <span>Alta de Factura</span>
        </a>
        <a href="FacturaPedido.aspx" class="btn btn-primary">
            <span>Alta de Factura Remisión</span>
        </a>
        <a href="FacturaAnticipadaPedidos.aspx" class="btn btn-primary">
            <span>Pedidos Factura</span>
        </a>
    </div>
    <div class="row">
        <div class="col-lg-11">
            <table id="table_facturas" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">
                
            </table>
        </div>
    </div>
</asp:Content>

