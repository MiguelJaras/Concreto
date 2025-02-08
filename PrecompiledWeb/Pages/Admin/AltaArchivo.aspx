<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="AltaArchivo.aspx.cs" Inherits="Pages_Admin_AltaArchivo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <h2>Alta de Archivos</h2>
    <hr />
    <div>
        <label>
            <input type="file" id="fuImagen" multiple="multiple" name="fuFile" runat="server" size="100" />
       
        </label>
        <asp:Button runat="server" Text="Guardar" ID="btnGuardar" onclick="btnGuardar_Click" CssClass="button" />
    </div>
</asp:Content>

