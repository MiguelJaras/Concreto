<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="NotaCreditoListado.aspx.cs" Inherits="Pages_Admin_NotaCreditoListado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript" src="../../Scripts/pages/admin/notaslistado.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <h2>Listado de Notas de Credito</h2>
    <hr />
    <div>
        <a href="NotaCredito.aspx" class="btn btn-primary">
            <span>Alta</span>
        </a>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <table id="table_notas" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">
                
            </table>
        </div>
    </div>
</asp:Content>

