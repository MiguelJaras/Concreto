<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Pages_Test_Login" %>
<!DOCTYPE html>
<html lang="es">
    <head>
	    <meta charset="iso-8859-1" />
	    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
	    <meta name="viewport" content="width=device-width, initial-scale=1"/>
	    <meta name='mobile-web-app-capable' content='yes'/>
        <link rel='icon' sizes='192x192' href='../Img/marfil.ico'>
	    <meta name="theme-color" content="#019BF5" />
	    <meta name='keywords' content='MARFIL CONCRETO - Pedidos Concreto' />
        <meta name='description' content='MARFIL CONCRETO - Pedidos Concreto' />
	    <meta name="author" content="Marfil Sistemas" />
	    <title>Marfil Concreto</title>
	    <link rel='SHORTCUT ICON' href='../Img/marfil.ico'>
        <link href="../Scripts/boostrap/css/bootstrap.css" rel="stylesheet" />

	    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
	    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->

        <script src="../Scripts/jquery/jquery-3.1.1.min.js"></script>
        <script src="../Scripts/boostrap/js/bootstrap.min.js"></script>
        <script>
            $(function () {
                $('.form-control').keypress(function (e) {
                    if (e.which == 13) {
                        e.preventDefault();
                        $('#btnAceptar').click();
                    }
                });
                var error = $('#<%=error.ClientID%>');
                error.hide();
                error.fadeIn('slow');
                setTimeout(function () { error.slideUp('slow') }, 5000);
            });
        </script>
    </head>
	
    <body>
        <div class="container">
            <div class="row">
                <div class="col-sm-8 col-sm-offset-2 col-md-4 col-md-offset-4">
                    <div class="login-panel ">
                        <div class="panel-heading">
                            <h3 class="logo text-center">
                                <img src="../Img/Concreto%20Clasico.jpg" width="250" height="135" />
                            </h3>
                        </div>
                        <div class="panel-body">
                           <form method='post' id='fm_login' runat="server">
                                <fieldset>
                                    <legend>Iniciar Sesión</legend>
                                    <div class="form-group">
                                        <asp:TextBox runat="server" placeholder="Email" required="" id="username" CssClass="form-control input-lg input-round text-center"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox runat="server" placeholder="Password" required="" id="password" TextMode="Password" CssClass="form-control input-lg input-round text-center"></asp:TextBox>
                                    </div>
                                    <asp:Button runat="server" ID="btnAceptar" OnClick="btnAceptar_Click" CssClass="btn btn-primary btn-lg input-round btn-block text-center" Text="Aceptar" />
                                </fieldset>
                            </form>
                        </div>
					    <section>
					        <div id='error' class='alert alert-danger' runat="server" visible="false">
                                <i class='fa fa-exclamation-triangle'></i> 
                                <asp:Label runat="server" ID="lblMessage"></asp:Label>
					        </div>  
                            <p class="text-center"><a href="EnvioPassword.aspx">¿Olvidaste tu contraseña?</a></p>
                        </section>
                    </div>
                </div>
            </div>
        </div>
    </body>
</html>


