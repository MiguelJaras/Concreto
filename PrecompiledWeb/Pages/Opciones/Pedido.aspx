<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="Pedido.aspx.cs" Inherits="Pages_Opciones_Pedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="../../Scripts/pages/opciones/pedido.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <asp:HiddenField runat="server" ID="hdnArrInsumos" />
    <asp:HiddenField runat="server" ID="hdnArrServicios" />
    <asp:HiddenField runat="server" ID="hdnIntPartida" />
    <asp:HiddenField runat="server" ID="hdnOrdenStatus" />
    <asp:HiddenField runat="server" ID="hdnGrua" Value="1" />
    <asp:HiddenField runat="server" ID="hdnCliente" Value="0" />

    <asp:HiddenField runat="server" ID="hddTipoBombeo" Value="1" />

    <%--<asp:Button runat="server" ID="btnEmail" CssClass="hide" OnClick="btnEmail_Click" />--%>
    <%--<input type="button" id="btnGuardar" onclick="GuardarPedido()" value="Guardar" />--%>
    <h2>Pedido</h2>
    
    <hr />
    <!-- DATOS GENERALES -->
    <div class="panel panel-primary">
        <div class="panel-heading">Datos Generales</div>
        <div class="panel-body form-horizontal">
            <div class="form-group">
                <label class="col-md-2 control-label">Orden de Compra</label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtOrdenCompra" CssClass="form-control" onkeypress="return KeyPressOnlyInteger(event, this, 20)">

                    </asp:TextBox>
                </div>
                <label class="col-md-2 control-label">Remisión</label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtRemision" CssClass="form-control" onkeypress="return KeyPressOnlyInteger(event, this, 20)">

                    </asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">Tipo de Precio</label>
                 <%--<div class="col-md-2">
                   <label class="radio-inline">
                        <input type="radio" name="rdbTipoPrecio" onchange="CalcularPrecio()" value="1" />Menudeo
                    </label>
                    <label class="radio-inline">
                        <input type="radio" name="rdbTipoPrecio" onchange="CalcularPrecio()" value="2" />Medio Mayoreo
                    </label>
                    <label class="radio-inline">
                        <asp:RadioButton runat="server" ID="rdbTipoPrecio" GroupName="rdbTipoPrecio" Text="Mayoreo" value="3" onchange="CalcularPrecio();" Checked="true" />
                    </label>
                </div>
                --%>
              
                <div class="col-md-2">
                    <asp:DropDownList runat="server" ID="ddlPorcentaje" AutoPostBack="true" OnSelectedIndexChanged="ddlPorcentaje_Change" CssClass="btn dropdown-toggle btn-default">
                    </asp:DropDownList>
                </div>



            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">Fecha de Entrega</label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtFechaEntrega" CssClass="form-control">
                    </asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">Cliente</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtCliente" CssClass="form-control">
                    </asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">Encargado de Obra</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtEncargadoObra" CssClass="form-control">
                    </asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-2 control-label">Teléfono</label>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtTelefonos" CssClass="form-control" onkeypress="return KeyPressOnlyInteger(event, this, 20)">
                    </asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-2 control-label">Elemento</label>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtElemento" CssClass="form-control">
                    </asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">Estado</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlEstado" CssClass="btn dropdown-toggle btn-default">
                    </asp:DropDownList>
                </div>
            </div>


            <div class="form-group">
                <label class="col-md-2 control-label">Municipio</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlCiudad" CssClass="btn dropdown-toggle btn-default">
                    </asp:DropDownList>
                </div>
            </div>


            <div class="form-group">
                <label class="col-md-2 control-label">Calle y No.</label>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtCalle" CssClass="form-control">
                    </asp:TextBox>
                </div>
                <label class="col-md-2 control-label">Colonia</label>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtColonia" CssClass="form-control">
                    </asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-2 control-label">Entre Calles</label>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtEntreCalles" CssClass="form-control">
                    </asp:TextBox>
                </div>
                <label class="col-md-2 control-label">Código Postal</label>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtCodigoPostal" CssClass="form-control">
                    </asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-2 control-label"></label>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtEntreCalles2" CssClass="form-control">
                    </asp:TextBox>
                </div>
            </div>


            <div class="form-group">
                
                <label class="col-md-2 control-label">Factura</label>
                <div class="col-md-1">
                    <label class="radio-inline">
                        <asp:RadioButton runat="server" ID="rdbEmailFactura" GroupName="rdbEmailFactura" Text="Sí" onchange="ChangeFacturaEmail(1);" value="1" Checked="true" />
                    </label>
                    <label class="radio-inline">
                        <asp:RadioButton runat="server" ID="rdbEmailFactura2" GroupName="rdbEmailFactura" Text="No" onchange="ChangeFacturaEmail(0);" value="0" />
                    </label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" Text="EXAMPLE@MARFIL.COM">
                    </asp:TextBox>
                    <small id="emailHelp" class="form-text text-muted">Este correo se usara para el envió de factura.</small>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-2 control-label">Vendedor</label>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtVendedor" CssClass="form-control">
                    </asp:TextBox>
                </div>
            </div>



            <div class="form-group">
                <label class="col-md-2 control-label">Observaciones</label>
                <div class="col-md-4">
                    <asp:TextBox runat="server" TextMode="MultiLine" Rows="3" ID="txtObservaciones" Width="420">
                    </asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <!-- DATOS GENERALES -->


    <!-- LISTADO PRODUCTOS -->
    <div class="row">
        <div class="col-lg-12">
            <table id="table_partidas" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">
                
            </table>
        </div>
    </div>
    <!-- LISTADO PRODUCTOS -->

    <!-- INSUMOS -->
    <div class="panel panel-primary">
        <div class="panel-heading">
            Pedido de Concreto
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-4">
                    <label for="<%=ddlProducto.ClientID %>">Producto</label>
                    <asp:DropDownList runat="server" ID="ddlProducto" CssClass=" dropdown-toggle btn-default form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label for="<%=ddlHoras.ClientID %>">Hora Entrega</label>
                    <%--<asp:TextBox runat="server" ID="txtHoraEntrega" MaxLength="5" CssClass="form-control" placeholder="HH:MM">
                    </asp:TextBox>--%>
                    <asp:DropDownList runat="server" ID="ddlHoras"  CssClass="btn dropdown-toggle btn-default form-control">

                    </asp:DropDownList>
                </div>

                <div class="col-md-2">
                    <label for="<%=txtCantidad.ClientID %>">Cantidad</label>
                    <asp:TextBox runat="server" ID="txtCantidad" CssClass="form-control" onkeypress="return KeyPressOnlyDecimal(event, this, 10, 4)" Text="6">
                    </asp:TextBox>
                </div>

                <div class="col-md-2">
                    <label for="<%=txtPrecio.ClientID %>">Precio</label>
                    <asp:TextBox runat="server" ID="txtPrecio" CssClass="form-control precio" Text="0.00">
                    </asp:TextBox>
                </div>
                <div class="col-md-2">
                    <label for="<%=txtTotalProd.ClientID %>">Total</label>
                    <asp:TextBox runat="server" ID="txtTotalProd" CssClass="form-control precio" ReadOnly="true" Text="0.00">
                    </asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                <h5><label>Servicios</label></h5>
                    <asp:HiddenField runat="server" ID="hdnServicios" Value="" />

    

                    <div class="row">
                        <div class="col-md-4">
                            <div class="checkbox">
                                <label>
                                    <input type="checkbox" id='chkServicio_1' name="chkServicio" onchange="ValidCheckBox(this)" value='1' class="checkbox chkServicios" />Servicio de Bombeo
                                </label>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <label class="radio-inline">
                                <input type="radio" name="rdbBombeo" value="1" disabled="disabled" onchange="ValidCantidad();" checked="checked" />Bomba
                            </label>
                            <label class="radio-inline">
                                <input type="radio" name="rdbBombeo" value="2" disabled="disabled" onchange="ValidCantidad();" />Grúa
                            </label>
                        </div>

                        
                        <div class="col-md-2">
                            <label class="radio-inline">
                                <input type="radio" name="rdbBombeoTam" value="1" disabled="disabled" onchange="ValidCantidad();" checked="checked" />Chica
                            </label>
                            <label class="radio-inline">
                                <input type="radio" name="rdbBombeoTam" value="2" disabled="disabled" onchange="ValidCantidad();" />Grande
                            </label>
                        </div>


                        <div class="col-md-2">
                            <input type="text" readonly="" id='txtPrecioServicio_1' value="$ 0.00" class="form-control precio" />
                        </div>
                        <div class="col-md-2">
                            <input type="text" readonly="" id='txtTotalServicio_1' value="$ 0.00" class="form-control precio" />
                        </div>
                    </div>

                    <asp:Repeater runat="server" ID="rptServiciosEspeciales">
                        <ItemTemplate>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="checkbox">
                                        <label>
                                            <input type="checkbox" id='chkServicio_<%# Eval("intServicio") %>' name="chkServicio" onchange="ValidCheckBox(this)" value='<%# Eval("intServicio") %>' class="checkbox chkServicios" /><%# Eval("strNombre") %>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    
                                </div>
                                <div class="col-md-2">
                                    <input type="text" readonly="" id='txtPrecioServicio_<%# Eval("intServicio") %>' value="$ 0.00" class="form-control precio" />
                                </div>
                                <div class="col-md-2">
                                    <input type="text" readonly="" id="txtTotalServicio_<%# Eval("intServicio") %>" value="$ 0.00" class="form-control precio" />
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>


                    <div class="row">
                        <div class="col-md-6">
                            <div class="checkbox">
                                <label>
                                    <asp:HiddenField runat="server" ID="hddCantidadTuberia" />
                                    <%--<input type="checkbox" id='chkServicio_7' name="chkServicio" onchange="ValidCheckBox(this)" value='7' class="checkbox chkServicios" />--%>Tuberia
                                </label>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <input type="text" class="form-control" id="txtCantidadProd_7" onkeyup="ValidTextBox(7)" value="0" onkeypress="return KeyPressOnlyInteger(event, this, 20)"/>
                        </div>
                        <div class="col-md-2">
                            <input type="text" readonly="" id='txtPrecioServicio_7' value="$ 0.00" class="form-control precio" />
                        </div>
                        <div class="col-md-2">
                            <input type="text" readonly="" id='txtTotalServicio_7' value="$ 0.00" class="form-control precio" />
                        </div>
                    </div>


                    <div class="row hide" id="rowAjuste">
                        <div class="col-md-6">
                            <div class="checkbox">
                                <label>
                                    <input type="checkbox" id='chkAjuste'  onchange="CheckAjuste(this)" class="checkbox" />Ajuste
                                </label>
                            </div>
                        </div>
                        <div class="col-md-2">
                            
                        </div>
                        <div class="col-md-2">
                            
                        </div>
                        <div class="col-md-2">
                            
                        </div>
                    </div>


                </div>
            </div>
            <div class="row">
                <div class="form-group">
                    <div class="col-md-6"></div>
                    <div class="col-md-2">
                        
                    </div>
                    <div class="col-md-2">
                        <input type="button" class="btn btn-primary" id="btnLimpiar" value="Limpiar" onclick="Clear();" />
                    </div>
                    <div class="col-md-2">
                        <label for="txtTotalPartida">Total</label>
                        <input type="text" id="txtTotalPartida" readonly="" class="form-control precio" value="$ 0.00" />
                    </div>
                </div>
            </div>
            
        </div>
    </div>
    
    <div class="row">
        <div class="form-group">
            <div class="col-md-5"></div>

            <div class="col-md-1">
                <label for="<%=ddlPorcIva.ClientID %>">% Iva</label>
                <asp:DropDownList runat="server" ID="ddlPorcIva" CssClass="btn dropdown-toggle btn-default form-control">

                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <label for="<%=txtSubTotal.ClientID %>">SubTotal</label>
                <asp:TextBox runat="server" ID="txtSubTotal" CssClass="form-control precio">
                </asp:TextBox>
            </div>
            <div class="col-md-2">
                <label for="<%=txtIva.ClientID %>">Iva</label>
                <asp:TextBox runat="server" ID="txtIva" CssClass="form-control precio">
                </asp:TextBox>
            </div>
            <div class="col-md-2">
                <label for="<%=txtTotal.ClientID %>">Total</label>
                <asp:TextBox runat="server" ID="txtTotal" CssClass="form-control precio">
                </asp:TextBox>
            </div>
        </div>
    </div>
    <br />
    
    <div class="row text-center">
        <%--<input type="button" value="Nuevo" onclick="Clear();" class="btn btn-primary" />--%>

        <%if (intCliente != 16) {%>
        <asp:Button runat="server" ID="btnNuevo" CssClass="btn btn-primary" OnClientClick="location='Pedido.aspx';return false;" Text="Nuevo" />
        <asp:Button runat="server" ID="btnAceptar" OnClick="btnAceptar_Click" OnClientClick="Validate();" Text="Guardar" CssClass="btn btn-primary" />
        <input type="button" id="btnPreview" value="Vista Previa" onclick="PreviewPedido()" class="btn btn-primary" />
        <input type="button" value="Enviar" onclick="EnviarPedido();" class="btn btn-primary" id="btnEnviar" runat="server" />
         <%} %>

        <input type="button" value="Cancelar" onclick="CancelarPedido();" class="btn btn-primary" id="btnCancelar" runat="server" />
        
    </div>
   
    <!-- INSUMOS -->

    <br />
    <br />
</asp:Content>

