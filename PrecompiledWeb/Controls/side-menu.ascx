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
                <a href="../../Pages/Opciones/PedidoListado.aspx">
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
                            <li><a href="<%=Page.ResolveUrl("~/Pages/Opciones/PedidoListado.aspx")%>">Listado</a></li>
                            <li><a href="<%=Page.ResolveUrl("~/Pages/Opciones/Pedido.aspx")%>">Alta de Pedido</a></li>
                            <li><a href="<%=Page.ResolveUrl("~/Pages/Opciones/CotizacionListado.aspx")%>">Cotizaciones</a></li>


                             <%if (IntCliente == 3 || IntCliente == 9 || IntCliente == 5 || IntCliente == 6)
                               {%>
                                <li><a href='<%=Page.ResolveUrl("~/Pages/Admin/ProgramacionConcreto.aspx")%>'>Programación de Pedidos</a></li>
                                <li><a href='<%=Page.ResolveUrl("~/Pages/Admin/PlantaCargaOrdenes.aspx")%>'>Carga Command Batch</a></li>
                                <li><a href='<%=Page.ResolveUrl("~/Pages/Admin/PrecioBase.aspx")%>'>Precio Base</a></li>
                            <%} %>

                            <%if (IntCliente == 3 || IntCliente == 9)
                               {%>
                          <%--  <li><a href='<%=Page.ResolveUrl("~/Pages/Admin/PlantaCargaExterna.aspx")%>'>Carga Venta Externa</a></li>--%>
                             <li><a href='<%=Page.ResolveUrl("~/Pages/Admin/PlantaCargaRemisiones.aspx")%>'>Carga Venta Mensual</a></li>
                             <%} %>

                        </ul>
                    </div>
                </li>
                <%--<li class="">
                    <a data-toggle="collapse" href="#dropdown-facturas">
                        <span class="glyphicon glyphicon-list"></span> Facturas<span class="caret"></span>
                    </a>
                    <div id="dropdown-facturas" class="panel-collapse collapse">
                        <ul class="dropdown-menu">
                            <li><a href="<%=Page.ResolveUrl("~/Pages/Opciones/FacturaListado.aspx")%>">Listado</a></li>
                        </ul>
                    </div>
                </li>--%>
                
                <%if (IntCliente == 3 || IntCliente == 9)
                  { %>
                <li class="">
                    <a data-toggle="collapse" href="#dropdown-admin">
                        <span class="glyphicon glyphicon-lock"></span> Admin<span class="caret"></span>
                    </a>
                    <div id="dropdown-admin" class="panel-collapse collapse">
                        <ul class="dropdown-menu">
                            <li><a href='<%=Page.ResolveUrl("~/Pages/Admin/PedidoListado.aspx")%>'>Admin. de Pedidos</a></li>
                            <li><a href='<%=Page.ResolveUrl("~/Pages/Admin/PedidoListadoImpresion.aspx")%>'>Impresión de Pedidos</a></li>                       
                            <li><a href='<%=Page.ResolveUrl("~/Pages/Admin/NotaCreditoListado.aspx")%>'>Notas de Crédito</a></li>
                            <%--<li><a href='<%=Page.ResolveUrl("~/Pages/Admin/FacturaMasiva.aspx")%>'>Facturas Masivas</a></li>--%>
                            <li><a href='<%=Page.ResolveUrl("~/Pages/Admin/Clientes.aspx")%>'>Admin. de Clientes</a></li>  
                             <li><a href='<%=Page.ResolveUrl("~/Pages/Admin/Precios.aspx")%>'>Precios</a></li>
                        </ul>
                    </div>
                </li>

                <li class="">
                    <a data-toggle="collapse" href="#dropdown-fac">
                        <span class="glyphicon glyphicon-usd"></span> Facturacion<span class="caret"></span>
                    </a>
                    <div id="dropdown-fac" class="panel-collapse collapse">
                        <ul class="dropdown-menu">
                            <li><a href='<%=Page.ResolveUrl("~/Pages/Admin/FacturaListado.aspx")%>'>Facturas</a></li>
                            <li><a href='<%=Page.ResolveUrl("~/Pages/Admin/FacturaGenerarListado.aspx")%>'>Generar Factura</a></li>
                            <li><a href='<%=Page.ResolveUrl("~/Pages/Admin/NotaCreditoGenerarListado.aspx")%>'>Generar Nota Crédito</a></li>
                            <li><a href='<%=Page.ResolveUrl("~/Pages/Admin/FacturaClienteListado.aspx")%>'>Alta de Cliente</a></li>
                        </ul>
                    </div>
                </li>


                <li class="">
                    <a data-toggle="collapse" href="#dropdown-cat">
                        <span class="glyphicon glyphicon-book"></span> Catalogos<span class="caret"></span>
                    </a>
                    <div id="dropdown-cat" class="panel-collapse collapse">
                        <ul class="dropdown-menu">
                            <li><a href='<%=Page.ResolveUrl("~/Pages/Catalogos/Producto.aspx")%>'>Productos</a></li>
                            <li><a href='<%=Page.ResolveUrl("~/Pages/Admin/PrecioProducto.aspx")%>'>Lista de Precios Productos</a></li>
                            <li><a href='<%=Page.ResolveUrl("~/Pages/Catalogos/Servicio.aspx")%>'>Servicios</a></li>
                            <li><a href='<%=Page.ResolveUrl("~/Pages/Admin/PrecioServicio.aspx")%>'>Lista de Precios Servicios</a></li>
                            <li><a href='<%=Page.ResolveUrl("~/Pages/Catalogos/CapturaConsumo.aspx")%>'>Captura de Consumo</a></li>
                           
                        </ul>
                    </div>
                </li>
                <li class="">
                    <a data-toggle="collapse" href="#dropdown-rep">
                        <span class="glyphicon glyphicon-list-alt"></span> Reportes<span class="caret"></span>
                    </a>
                    <div id="dropdown-rep" class="panel-collapse collapse">
                        <ul class="dropdown-menu">
                            <li><a href='<%=Page.ResolveUrl("~/Pages/Reportes/PedidoFactura.aspx")%>'>Relación Facturas - Remisión</a></li>
                            <li><a href='<%=Page.ResolveUrl("~/Pages/Reportes/PedidoSinFactura.aspx")%>'>Remisiones sin facturas</a></li>
                            <li><a href='<%=Page.ResolveUrl("~/Pages/Reportes/Facturas.aspx")%>'>Facturas</a></li>
                             <li><a href='<%=Page.ResolveUrl("~/Pages/Reportes/Consumos.aspx")%>'>Consumos</a></li>
                             <li><a href='<%=Page.ResolveUrl("~/Pages/Reportes/ReportSignature.aspx")%>'>Firma</a></li>

                        </ul>
                    </div>
                </li>


                


                <%} %>
                <li class="">
                    <a href="../../Login/FinSesion.aspx"><span class="glyphicon glyphicon-log-out"></span> Cerrar Sesión</a>
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
                <%if (IntCliente != 3){ %>
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#"><span class="glyphicon glyphicon-list"></span> Pedidos<span class="caret"></span></a>
                    <ul class="dropdown-menu">
                        <li><a href="PedidoListado.aspx">Listado</a></li>
                        <li><a href="Pedido.aspx">Alta de Pedido</a></li>
                    </ul>
                </li>
                <%} %>
                <%if (IntCliente != 2 && IntCliente != 1)
                  { %>
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#"><span class="glyphicon glyphicon-lock"></span> Admin<span class="caret"></span></a>
                    <ul class="dropdown-menu">
                        <li><a href='<%=Page.ResolveUrl("~/Pages/Admin/PedidoListado.aspx")%>'>Admin. de Pedidos</a></li>
                        <li><a href='<%=Page.ResolveUrl("~/Pages/Admin/PrecioProducto.aspx")%>'>Lista de Precios Productos</a></li>
                        <li><a href='<%=Page.ResolveUrl("~/Pages/Admin/PrecioServicio.aspx")%>'>Lista de Precios Servicios</a></li>
                        <li><a href='<%=Page.ResolveUrl("~/Pages/Admin/FacturaListado.aspx")%>'>Facturas</a></li>
                    </ul>
                </li>
                <%} %>
                <li class="">
                    <a href="../../Login/FinSesion.aspx"><span class="glyphicon glyphicon-log-out"></span> Cerrar Sesión</a>
                </li>
            </ul>
        </div>
        <!--MENU MOBILE-->

    </div>
</nav>
