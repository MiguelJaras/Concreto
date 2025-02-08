<%@ Page Language="c#" Inherits="EnvioPassword" CodeFile="EnvioPassword.aspx.cs" %>

<!DOCTYPE html>
<!--[if lt IE 7 ]> <html lang="en" class="ie6 ielt8"> <![endif]-->
<!--[if IE 7 ]>    <html lang="en" class="ie7 ielt8"> <![endif]-->
<!--[if IE 8 ]>    <html lang="en" class="ie8"> <![endif]-->
<!--[if (gte IE 9)|!(IE)]><!--> <html lang="en"> <!--<![endif]-->
<head>
<meta charset="utf-8">
<title>Reenvio de Password</title>
<link href="../Styles/PassWord.css" rel="stylesheet" type="text/css" />
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
                        <form method='post' id='form1' runat="server">
                            <fieldset>
                                <legend class="text-center">Recuperar Password</legend>
                                <div class="form-group">
                                    <asp:TextBox runat="server" placeholder="Email" required="" id="Email"  CssClass="form-control input-lg input-round text-center"></asp:TextBox>
			                    </div>						
			                    <div class="form-group form-inline">
                                    <asp:Button ID="btnIngresar" runat="server" Text="Enviar" OnClick="btnAceptar_Click" CssClass="btn btn-primary"/>
                                    &nbsp;
                                    <asp:Button ID="btnReturn" runat="server" Text="Regresar" OnClientClick="window.location = 'Login.aspx'; return false;" CssClass="btn btn-primary"/>
			                    </div>
                            </fieldset>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    
    <%--<br />
	<section id="content">
        <form runat="server" id="form1">
            <h1>
                <img alt="ha" src="../Img/top_empresa.png" />              
            </h1>
            <h3>
                Reenvio de Password
            </h3>
            <h3>
                &nbsp;
            </h3>
            <div>
                <asp:TextBox runat="server" placeholder="Email" required="" id="Email"></asp:TextBox>
			</div>						
			<div>
                <asp:Button ID="btnIngresar" runat="server" Text="Enviar" OnClick="btnAceptar_Click" />
                &nbsp;
                <asp:Button ID="btnReturn" runat="server" Text="Regresar" OnClientClick="window.location = 'Login.aspx'; return false;"/>
			</div>
		</form><!-- form -->		
	</section><!-- content -->--%>
</div><!-- container -->
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