<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="Servicio.aspx.cs" Inherits="Pages_Catalogos_Servicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="../../Scripts/pages/catalogos/servicios.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <h2>Alta de Servicios</h2>
    <hr />
    <div id="PopUpServicio" class="white-popup-producto mfp-hide">
        <h2 id=""><label id="titPopUp"></label></h2>
        <div class="form-group">
            <label for="txtServicio">Servicio</label>
            <input type="text" id="txtServicio" class="form-control" value="" />
            <input type="hidden" id="hdnIntServicio" class="form-control" value="0" />
        </div>
         <div class="form-group">
            <label for="txtServicio">Precio Base</label>
            <input type="text" id="txtPrecioBase" class="form-control" value="0" />
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
            <table id="table_servicios" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">
                
            </table>
        </div>
    </div>
</asp:Content>

