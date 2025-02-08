<%@ Control Language="C#" AutoEventWireup="true" CodeFile="side-menu.ascx.cs" Inherits="Pages_Opciones_Controls_side_menu" %>
<nav class="navbar navbar-default navbar-fixed-side navbar-inverse" id="nav-side">
    <div class="container">
        <div class="navbar-header">
            <button type="button" class="navbar-toggle" data-target="#menu-mobile" data-toggle="collapse">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            
            <div class="brand-logo">
                <a href="Default.aspx">
                    <img src="../../Img/Concreto Clasico.jpg" width="180" height="104" />
                </a>
            </div>
        </div>
        <!--MENU MD-->
        <div class="navbar-collapse collapse hidden-sm hidden-xs">
            <ul class="nav navbar-nav">
                <li>
                    <a href="#"><span class="glyphicon glyphicon-home"></span> Home</a>
                </li>
                <li class="">
                    <a data-toggle="collapse" href="#dropdown-pedidos">
                        <span class="glyphicon glyphicon-list"></span> Pedidos<span class="caret"></span>
                    </a>
                    <div id="dropdown-pedidos" class="panel-collapse collapse">
                        <ul class="dropdown-menu">
                            <li><a href="PedidoListado.aspx">Listado</a></li>
                            <li><a href="Pedido.aspx">Alta de Pedido</a></li>
                            <li><a href="PrecioBase.aspx">PrecioBase</a></li>
                        </ul>
                    </div>
                </li>
                <li class="">
                    <a data-toggle="collapse" href="#dropdown-cuenta">
                        <span class="glyphicon glyphicon-user"></span> Cuenta<span class="caret"></span>
                    </a>
                    <div id="dropdown-cuenta" class="panel-collapse collapse">
                        <ul class="dropdown-menu">
                            <li><a href="#">Datos</a></li>
                            <li><a href="#">Password</a></li>
                        </ul>
                    </div>
                </li>
                <li class="">
                    <a href="../../Login/FinSesion.aspx"><span class="glyphicon glyphicon-log-out"></span> Cerar Sesión</a>
                </li>
            </ul>
        </div>
        <!--MENU MD-->

        <!--MENU MOBILE-->
        <div class="collapse navbar-collapse" id="menu-mobile">
            <ul class="nav navbar-nav">
                <li>
                    <a href="#"><span class="glyphicon glyphicon-home"></span> Home</a>
                </li>
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#"><span class="glyphicon glyphicon-list"></span> Pedidos<span class="caret"></span></a>
                    <ul class="dropdown-menu">
                        <li><a href="PedidoListado.aspx">Listado</a></li>
                        <li><a href="Pedido.aspx">Alta de Pedido</a></li>
                        <li><a href="PrecioBase.aspx">PrecioBase</a></li>
                    </ul>
                </li>
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#"><span class="glyphicon glyphicon-user"></span> Cuenta<span class="caret"></span></a>
                    <ul class="dropdown-menu">
                        <li><a href="#">Datos</a></li>
                        <li><a href="#">Password</a></li>
                    </ul>
                </li>
                <li class="">
                    <a href="../../Login/FinSesion.aspx"><span class="glyphicon glyphicon-log-out"></span> Cerar Sesión</a>
                </li>
            </ul>
        </div>
        <!--MENU MOBILE-->

    </div>
</nav>
