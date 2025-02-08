<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Telefono.aspx.cs" Inherits="Pages_Test_Telefono" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html>
		<html lang="es">		
		<head>
		    <meta charset="iso-8859-1" />
			<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
			<meta name="viewport" content="width=device-width, initial-scale=1"/>
			<meta name='mobile-web-app-capable' content='yes'/>
            <link rel='icon' sizes='192x192' href='Css/marfil.ico'>
   
			<meta name="theme-color" content="#019BF5" />
			<meta name='keywords' content='MARFIL CONCRETO - Pedidos Concreto' />
            <meta name='description' content='MARFIL CONCRETO - Pedidos Concreto' />
			<meta name="author" content="Marfil Sistemas" />
		
			<title>Marfil Concreto</title>
 		    
			<LINK rel='SHORTCUT ICON' HREF='Css/marfil.ico'>
            <link href="Css/bootstrap.css" rel="stylesheet" type="text/css" />
            <link href="Css/metisMenu.min.css" rel="stylesheet" type="text/css" />
            <link href="Css/admin.css" rel="stylesheet" type="text/css" />
            <link href="Css/font-awesome.min.css" rel="stylesheet" type="text/css" />
			
		
			<!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
			<!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
			<!--[if lt IE 9]>
				<script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
				<script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
			<![endif]-->
			
			<style type="text/css">
			
			.btn-primary {
               color: #fff;
               background-color: #1D628A;
               border-color: #035280;
            } 
			a {
               color: #FFFFFF;
               text-decoration: none;
            }
			.text-muted {
               color: #FFF;
            } 			
			
			
			</style>

            <script src="Script/jquery.js" type="text/javascript"></script>
            <script src="Script/bootstrap.js" type="text/javascript"></script>
            <script src="Script/metisMenu.min.js" type="text/javascript"></script>
            <script src="Script/jquery.maskedinput.js" type="text/javascript"></script>
            <script src="Script/admin.js" type="text/javascript"></script>


			<script type="text/javascript">
			    window.addEventListener('load', function () {
			        setTimeout(function () {
			            window.scrollTo(0, 1);
			        }, 0);
			    });
			</script>
         
			 <script type="text/javascript">

			     $(function () {
			         $("#btn_login").click(function () {
			             $('#fm_login').submit();
			             return false;
			         });

			         $('.form-control').keypress(function (e) {
			             if (e.which == 13) {
			                 e.preventDefault();
			                 $('#fm_login').submit();
			             }
			         });

			         // tooltip 
			         $('.tooltip-citi').tooltip({
			             selector: "[data-toggle=tooltip]",
			             container: "body"
			         })

			         // popover demo
			         $("[data-toggle=popover]")
						.popover()


			         $("#login").mask("999 999999");

			         $('#error').hide();
			         $('#error').fadeIn('slow');

			         $('#aviso').hide();
			         $('#aviso').fadeIn('slow');

			         setTimeout(function () { $('#aviso').slideUp('slow') }, 5000);
			         setTimeout(function () { $('#ok').slideUp('slow') }, 5000);
			         setTimeout(function () { $('#error').slideUp('slow') }, 5000);


			     });

			     function marcar(id) {
			         $(id).addClass('marcado');

			         $(id).keyup(function () {
			             $(this).removeClass('marcado');
			         });
			     }
			   
			 </script>
			 
			
			
		
		</head>
		
		
		<body>

    <div class="container">
        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <div class="login-panel ">
                    <div class="panel-heading">
                        <h3 class="logo text-center"><a href="http://www.citibox.com" target="_blank" title="citibox.com"><img src='Css/logo_citibox_med.png'></a></h3>
                    </div>
					
					<p class="text-center text-muted text-med tooltip-citi">Enviar contraseña
					  <button type="button" class="btn btn-success btn-circle btn-lg" data-container="body" data-toggle="popover" data-placement="right" data-content="Te mandaneros un sms/email para confirmar vuestra contraseña."><i class="fa fa-phone"></i></button>
					</p>
 
					
					
                    <div class="panel-body">
                       <form method='post' action='Telefono.aspx' id='fm_login'>
                            <fieldset>
                                <div class="form-group">
                                    <input class="form-control input-lg input-round text-center" placeholder="Teléfono" name="login" id="login" value=""  tabindex="1">
                                </div>

                                <a href="" id='btn_login' class="btn btn-primary btn-lg input-round btn-block text-center" title='Pedir contraseña a citibox' tabindex="3">Pedir</a>
                            </fieldset>
                        </form>
                    </div>
					
					<section>
					  
                    <p class="text-center"><a href="Telefono.aspx">Acceder como usuario</a></p>
                    </section>
                </div>
            </div>
        </div>
    </div>


<script type="text/javascript">
    (function (i, s, o, g, r, a, m) {
        i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
            (i[r].q = i[r].q || []).push(arguments)
        }, i[r].l = 1 * new Date(); a = s.createElement(o),
  m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
    })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

    ga('create', 'UA-59847540-2', 'auto');
    ga('send', 'pageview');

</script>

</body>

</html>
