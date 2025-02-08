<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="PedidoFactura.aspx.cs" Inherits="Pages_Reportes_PedidoFactura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

    <script type="text/javascript">

        $(document).ready(function () {
            
            $('#ctl00_BodyContent_txtFechaInicio').datepicker({
                dateFormat: 'dd/mm/yy',
            });

            $('#ctl00_BodyContent_txtFechaFin').datepicker({
                dateFormat: 'dd/mm/yy',
            });
            
        });

        function Exportar()
        {
            var strFechaInicio = $('#ctl00_BodyContent_txtFechaInicio').val();
            var strFechaFin = $('#ctl00_BodyContent_txtFechaFin').val();

            var reporte = $('input[name=reporte]:checked').val();
            if (reporte == 'pdf') {
                ShowPDF(strFechaInicio, strFechaFin);
            } else if (reporte == 'excel') {
                window.open('../../Utils/Excel.aspx?type=PedidosFacturas&params=' + strFechaInicio + ',' + strFechaFin);
            }
        }


        function ShowPDF(fechaInicio, fechaFin) {
            var urlData = 'PedidoFactura.aspx/ShowReport';
            var dataData = '{ datFechaInicio:\'' + fechaInicio + '\', datFechaFin:\'' + fechaFin + '\'}';            CallMethod(urlData, dataData, SuccessShowPDF);
        }

        function SuccessShowPDF(response) {
            var message = response.d[0];
            var data = response.d[1];
            if (message == "ok") {
                ShowModalPDF(data);
            } else {

            }
        }

        function ShowModalPDF(fileName) {

            var htmlContent = "<div id=detallePedido class=white-popup><object data=\"{FileName}\" type=\"application/pdf\" width=\"100%\" height=\"100%\">";
            htmlContent += "If you are unable to view file, you can download from <a href = \"{FileName}\">here</a>";
            htmlContent += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            htmlContent += "</div></object>";
            htmlContent = htmlContent.replace(/{FileName}/g, "../../Temp/Reporte Remisiones/" + fileName);

            $.magnificPopup.open({
                items: [
                    {
                        type: 'inline',
                        src: $(htmlContent)
                    }
                ],
            });

        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <h2>Reporte Remisiones - Facturas</h2>
    <hr />

    <div class="row form-horizontal">
            <div class="form-group ">
                <label class="col-md-2 control-label">Fecha Inicio</label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtFechaInicio" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                </div>
                <label class="col-md-1 control-label">Fecha Fin</label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtFechaFin" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2"></div>
                <div class="col-md-2"></div>
            </div>
            <div class="form-group ">
                <label class="col-md-2 control-label">Exportar</label>
                <div class="col-md-2">

                    <input type="radio" id="pdf" name="reporte" value="pdf" checked="checked" />
                    <img src="../../Img/pdf.png" />
                    &nbsp&nbsp&nbsp&nbsp&nbsp
                    <input type="radio" id="excel" name="reporte" value="excel" />
                    <img src="../../Img/Excel-icon24.png" />

                   <%-- <asp:RadioButton runat="server" ID="RadioButton1" GroupName="Tipo" Text="" Checked="true" />&nbsp&nbsp&nbsp&nbsp&nbsp
                    <asp:RadioButton runat="server" ID="RadioButton2" GroupName="Tipo" Text="" />&nbsp--%>
                </div>
            </div>
            <div class="form-group ">
                <label class="col-md-2 control-label"></label>
                <div class="col-md-2">
                    <input type="button" class="btn btn-primary btn-block" value="Exportar" onclick="Exportar();" />
                </div>
            </div>

        </div>
</asp:Content>

