<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Firma.aspx.cs" Inherits="Pages_Reportes_Firma" %>
<!doctype html>
<html lang="en">
<head>
  <meta charset="utf-8">
  <title>Signature</title>

    <link href="../../Scripts/signature/docs/css/signature-pad.css" rel="stylesheet" />
    <script src="../../Scripts/jquery/jquery-3.1.1.min.js"></script>
    <script>
        $(document).ready(function () {
            const signaturePad = new SignaturePad(canvas, {
                backgroundColor: 'rgba(255, 255, 255, 1)',
                minWidth: .7,
                maxWidth: .7,           
                penColor: "rgb(0,0,255)"
            });
        });


        function SendFile(B64) {
            var urlData = 'Firma.aspx/SaveFile';
            var data = '{fileBase64:"' + B64 + '"}';
            CallMethod(urlData, data, ScsSaveFile, false);
        }

        function ScsSaveFile(response) {
            var message = response.d[0];
            if (message == "ok") {
                alert("Se guardo correctamente")
                $('#signature-pad').remove();
                location.reload();
            }
            else {
                alert(message);
            }
        }

        function fnSave() {

            var bs64 = signaturePad.toDataURL();
            SendFile(bs64);
        }

        function CallMethod(url, data, successCallback, async) {
            try {
                $.ajax({
                    type: 'POST',
                    url: url,
                    data: data,
                    contentType: 'application/json; chartset:utf-8',
                    dataType: 'json',
                    success: successCallback,
                    error: function (XmlHttpError, error, description) {
                        if (XmlHttpError.status) {
                            alert(XmlHttpError.responseText);
                        }
                    },
                    async: async,
                });
            }
            catch (e) {
                message = "no";
                alert('Error ' + e);
            }
        }

        $.urlParam = function (name) {
            var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
            if (results == null) {
                return null;
            }
            return decodeURI(results[1]) || 0;
        }

    </script>
    <style>

        body{
            display: contents;
        }

   
    </style>
</head>
<body onselectstart="return false">

  <div id="signature-pad" class="signature-pad">
    <div class="signature-pad--body">
      <canvas></canvas>
    </div>
    <div class="signature-pad--footer">
      <div class="description">Ingrese Firma</div>

      <div class="signature-pad--actions">
        <div>
          <button type="button" class="button clear" data-action="clear">Limpiar</button>
          <button type="button" onclick ="fnSave()">Guardar</button>
        </div>
  
      </div>
    </div>
  </div>
    <script src="../../Scripts/signature/dist/signature_pad.umd.js"></script>
    <script src="../../Scripts/signature/docs/js/app.js"></script>

   <%-- https://learn.microsoft.com/en-us/aspnet/web-api/overview/security/enabling-cross-origin-requests-in-web-api--%>
</body>
</html>