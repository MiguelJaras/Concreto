<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="ProgramacionConcreto.aspx.cs" Inherits="Pages_Admin_ProgramacionConcreto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">


    <script src="../../Scripts/datatables/JSZip-2.5.0/jszip.min.js"></script>
    <script src="../../Scripts/datatables/pdfmake-0.1.36/pdfmake.min.js"></script>
    <script src="../../Scripts/datatables/pdfmake-0.1.36/vfs_fonts.js"></script>
    <script src="../../Scripts/datatables/Buttons-1.5.6/js/dataTables.buttons.min.js"></script>
    <script src="../../Scripts/datatables/Buttons-1.5.6/js/buttons.bootstrap.min.js"></script>
    <script src="../../Scripts/datatables/Buttons-1.5.6/js/buttons.html5.min.js"></script>
    <script src="../../Scripts/datatables/Buttons-1.5.6/js/buttons.print.min.js"></script>
    <script src="../../Scripts/datatables/Buttons-1.5.6/js/buttons.flash.min.js"></script>


    <script src="../../Scripts/pages/admin/programacionConcreto.js"></script>


    <style>
        .btn-group {
          position:relative;
          vertical-align:middle;
          display:block;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">

    <h2>Programación de Pedidos</h2>
    <hr />
    <div class="row form-inline">
        <div class="col-lg-12">
            <div class="form-group">
                <label class="align-middle">Fecha Entrega</label>
                <div class="input-group">
                    <span class="input-group-addon" id="basic-addon1"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></span>
                    <asp:textbox ID="txtFecha" runat="server" CssClass="form-control"></asp:textbox>
                </div>
            </div>
            <input id="btnBuscar" class="btn btn-primary" value="Filtrar" type="button" onclick="GetProgramacion();" />
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-lg-12">
            <table id="table_programacion" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">

            </table>
        </div>
    </div>

    <div id="PopUp" class="white-popup-mid mfp-hide">
        <input type="hidden" id="hdnReqDet" value="0" />
        <input type="hidden" id="hdnHoraAnt" value="" />


         <div class="form-group">
            <label for="txtOC">Orden Compra</label>
            <input type="text" id="txtOC" class="form-control" value="" readonly="" />
        </div>

        <div class="form-group">
            <label for="txtObra">Obra</label>
            <input type="text" id="txtObra" class="form-control" value="" readonly="" />
        </div>

        <div class="form-group">
            <label for="txtColonia">Colonia</label>
            <input type="text" id="txtColonia" class="form-control" value="" readonly="" />
        </div>
        
        <div class="form-group">
            <label for="txtManzana">Manzana</label>
            <input type="text" id="txtManzana" class="form-control" value="" readonly=""/>
        </div>
        
        <div class="form-group">
            <label for="txtLote">Lote</label>
            <input type="text" id="txtLote" class="form-control" value="" readonly="" />
        </div>
        
        <div class="form-group">
            <label for="txtElemento">Elemento</label>
            <input type="text" id="txtElemento" class="form-control" value="" readonly=""/>
        </div>

        <div class="form-group">
            <label for="<%=ddlHoras.ClientID %>">Horas</label>
            <asp:DropDownList runat="server" ID="ddlHoras"  CssClass="btn dropdown-toggle btn-default form-control">

            </asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="ddlPlanta">Planta</label>
            <select id="ddlPlanta" class="btn dropdown-toggle btn-default form-control">
                <option value="0">--Seleccione--</option>
                <option value="1">1</option>
                <option value="2">2</option>
                <option value="3">3</option>
            </select>
        </div>

        <div class="form-group">
            <label for="txtRemision">Remisión</label>
            <input type="text" id="txtRemision" class="form-control" value=""/>
        </div>


        <div class="text-center">
            <button type="button" onclick="CerrarPopUp();" class="btn btn-danger">Cancelar</button>
            <button type="button" onclick="Guardar();" class="btn btn-success">Guardar</button>
        </div>
    </div>

</asp:Content>

