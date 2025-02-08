<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="ReportSignature.aspx.cs" Inherits="Pages_Reportes_ReportSignature" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
  
    <script type="text/javascript">

        $(document).ready(function () {

            $('#mastermodal').remove();
            $(window).resize();

            $("#mdlFirma").on('shown.bs.modal', function (e) {
                canvas.height = canvas.offsetHeight;
                canvas.width = canvas.offsetWidth; 
                
            });
           
        });

        function Exportar() {   
                ShowPDF();
        }

        function ShowPDF() {
            var urlData = 'ReportSignature.aspx/ShowReport';
            var dataData = '{}';
            CallMethod(urlData, dataData, SuccessShowPDF);
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

            var htmlContent = "<div id=detalle class=white-popup><object data=\"{FileName}\" type=\"application/pdf\" width=\"100%\" height=\"100%\">";
            htmlContent += "If you are unable to view file, you can download from <a href = \"{FileName}\">here</a>";
            htmlContent += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            htmlContent += "</div></object>";
            htmlContent = htmlContent.replace(/{FileName}/g, "../../Temp/Reportes Firmas/" + fileName);

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
        <style type="text/css">

        #signature-pad.signature-pad {

            height:900px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <h2>Firmas</h2>
    <hr />
    <div class="row form-horizontal">
     
         <div class="form-group ">
            <label class="col-md-2 control-label"></label>
            <div class="col-md-2">
                 <a href="/Pages/Reportes/Firma.aspx" class="btn btn-primary btn-block" data-target="#mdlFirma" data-toggle="modal">FIRMAR</a>
             
            </div>
        </div>

        <div class="form-group ">
            <label class="col-md-2 control-label">Exportar</label>
            <div class="col-md-2">

                <input type="radio" id="pdf" name="reporte" value="pdf" checked="checked" />
                <img src="../../Img/pdf.png" alt ="PDF"/>

            </div>
        </div>
        <div class="form-group ">
            <label class="col-md-2 control-label"></label>
            <div class="col-md-2">
                <input type="button" class="btn btn-primary btn-block" value="Exportar" onclick="Exportar();" />
            </div>
        </div>
    </div>
       <div id="dialog"></div>

   
<div id="mdlFirma" class="modal fade " role="dialog" aria-hidden="true">
  <div class="modal-dialog">
  
    <div class="modal-content">

    </div>
  </div>
</div>
 




</asp:Content>

