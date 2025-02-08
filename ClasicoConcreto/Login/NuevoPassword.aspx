<%@ Page Language="c#" Inherits="NuevoPassword" CodeFile="NuevoPassword.aspx.cs" %>

<!DOCTYPE html>
<!--[if lt IE 7 ]> <html lang="en" class="ie6 ielt8"> <![endif]-->
<!--[if IE 7 ]>    <html lang="en" class="ie7 ielt8"> <![endif]-->
<!--[if IE 8 ]>    <html lang="en" class="ie8"> <![endif]-->
<!--[if (gte IE 9)|!(IE)]><!--> <html lang="en"> <!--<![endif]-->
<head>
<meta charset="utf-8">
<title>Recepción de ClasicoConcreto</title>
    
    <link href="../Scripts/boostrap/css/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <div class="login-panel ">
                    <div class="panel-heading">
                        <h3 class="logo text-center">
                            <img src="../Img/Concreto%20Clasico.jpg" width="250" height="135" />
                        </h3>
                    </div>
                    <div class="panel-body">
                        <form method='post' id='fm_login' runat="server">
                            <fieldset>
                                <legend class="text-center">Cambiar Password</legend>
                                <div class="form-group">
                                    <asp:TextBox runat="server" placeholder="Proveedor" required="" id="Proveedor" CssClass="form-control input-lg input-round text-center"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox runat="server" placeholder="Password" required="" id="username" CssClass="form-control input-lg input-round text-center" TextMode="Password"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox runat="server" placeholder="Confirmacion del Password" required="" id="password" CssClass="form-control input-lg input-round text-center" TextMode="Password"></asp:TextBox>
                                </div>
                                <div class="text-center">
                                    <asp:Button ID="btnIngresar" runat="server" Text="Aceptar" OnClientClick="return ValidaCaracteres()" OnClick="btnAceptar_Click" CssClass="btn btn-primary" />
			                    </div>
                            </fieldset>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>

<script type="text/javascript">

    function ValidaCaracteres() {

        var pass = document.getElementById("username").value;
        var conf = document.getElementById("password").value;

        if (pass != conf) {
            alert('Confirmación incorrecta, favor de intentarlo nuevamente');
            document.getElementById("password").value = "";
            return false;
        }

        return true;
    }

</script>
</body>
</html>