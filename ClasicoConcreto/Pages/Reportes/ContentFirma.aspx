<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContentFirma.aspx.cs" Inherits="Pages_Reportes_ContentFirma" %>

<!doctype html>
<html lang="en">
<head>
  <meta charset="utf-8">
  <title>Signature Pad demo</title>
  

   <link href="../../Scripts/signature/signature-pad.css" rel="stylesheet" />
   <link href="../../Scripts/signature/ie9.css" rel="stylesheet" />

   <script src="../../Scripts/jquery/jquery-3.1.1.min.js"></script>
    <script>
        $(document).ready(function () { });


        function SendFile(B64) {
            var urlData = 'ContentFirma.aspx/SaveFile';
            var data = '{fileBase64:"' + B64 + '"}';
            CallMethod(urlData, data, ScsSaveFile, false);
        }

        function ScsSaveFile(response) {
            var message = response.d[0];
            if (message == "ok") {
                console.log("imagen en back");
            }
            else {
                console.log(message);
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
    <script src="../../Scripts/signature/signature_pad.js"></script>
    <script src="../../Scripts/signature/app.js"></script>

</body>
</html>
