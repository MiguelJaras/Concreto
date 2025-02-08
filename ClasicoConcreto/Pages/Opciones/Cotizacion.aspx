<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/site.master" AutoEventWireup="true" CodeFile="Cotizacion.aspx.cs" Inherits="Pages_Opciones_Cotizacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="../../Scripts/select2/js/select2.min.js"></script>
    <link href="../../Scripts/select2/css/select2.min.css" rel="stylesheet" />
    
    <script src="js/cotizacion.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <h2>Cotización</h2>
    
    <hr />
    <div class="panel panel-primary">
        <div class="panel-heading">Datos Generales</div>
        <div class="panel-body form-horizontal">
            <div class="form-group">
                <label class="col-md-1 control-label">Cliente</label>
                <div class="col-md-4">
                    <input type="text" id="txtCliente" name="txtCliente" class="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-1 control-label">Obra</label>
                <div class="col-md-4">
                    <input type="text" id="txtObra" class="form-control" />
                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-1 control-label">Elemento</label>
                <div class="col-md-4">
                    <input type="text" id="txtElemento" class="form-control" />
                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-1 control-label">Fecha Colado</label>
                <div class="col-md-1">
                    <input type="text" id="txtFechaColado" name="txtFechaColado" class="form-control" />

                </div>
                <label class="col-md-1 control-label">Tipo Concreto</label>
                <div class="col-md-2">
                    <input type="text" id="txtTipoConcreto" class="form-control" />
                </div>
                <div class="col-md-4">

                </div>
            </div>

            <div class="form-group">
                <label class="col-md-1 control-label">Resistencia</label>
                <div class="col-md-1">
                    <input type="text" id="txtResistencia" class="form-control" />
                </div>

                <label class="col-md-1 control-label">Revenimiento</label>
                <div class="col-md-1">
                    <input type="text" id="txtRevenimiento" class="form-control" onkeypress="return KeyPressOnlyInteger(event, this, 20)"/>
                </div>

                <label class="col-md-1 control-label">Agregado</label>
                <div class="col-md-1">
                    <input type="text" id="txtAgregado" class="form-control" onkeypress="return KeyPressOnlyInteger(event, this, 20)"/>
                </div>

                <label class="col-md-1 control-label">Tipo:</label>
                <div class="col-md-1">

                    <select id="txtTipo" class="btn dropdown-toggle btn-default">
                        <option value="0">--Seleccione--</option>
                        <option value="1">Normal</option>
                        <option value="2">Relleno</option>
                    </select>

                    <%--<input type="text" id="txtTipo" class="form-control" onkeypress="return KeyPressOnlyInteger(event, this, 20)"/>--%>
                </div>

            </div>

            <div class="form-group">
                <label class="col-md-1 control-label">Extras</label>
                <div class="col-md-1">
                    <input type="text" id="txtExtras" class="form-control" />
                </div>
                <label class="col-md-1 control-label">Extras</label>
                <div class="col-md-3">
                    <input type="text" id="txtExtras2" class="form-control" />
                </div>
                <label class="col-md-1 control-label">Tiro</label>
                <div class="col-md-1">
                    <select id="txtTiro" class="btn dropdown-toggle btn-default">
                        <option value="0">--Seleccione--</option>
                        <option value="1">Directo</option>
                        <option value="2">Bombeable</option>
                    </select>
                    <%--<input type="text" id="txtTiro" class="form-control" onkeypress="return KeyPressOnlyInteger(event, this, 20)"/>--%>
                </div>
            </div>

        </div>
    </div>


    <div class="panel panel-primary">
        <div class="panel-heading">
            Productos
        </div>
        <div class="panel-body">

            


            <div class="row">
                <div class="col-md-5">
                    <label for="ddlProductos">Producto</label>
                    <select id="ddlProductos" class="form-control"></select>
                </div>
                <div class="col-md-1">

                </div>
                <div class="col-md-2">
                    <label for="txtPrecio">Precio</label>
                    <input type="text" id="txtPrecio" onkeyup="CalcularTotalProducto();" class="form-control precio" value="0" />
                </div>

                <div class="col-md-2">
                    <label for="txtCantidad">Cantidad</label>
                    <input type="text" id="txtCantidad" onkeyup="CalcularTotalProducto();" class="form-control text-right" value="0" onkeypress="return KeyPressOnlyDecimal(event, this, 10, 4)" />
                </div>
                <div class="col-md-2">
                    <label for="txtTotal">Total</label>
                    <input type="text" id="txtTotal" class="form-control precio" value="0" readonly="" />
                </div>

            </div>
            <br />
            <div class="row">
                <div class="col-md-11">

                </div>
                <div class="col-md-1">
                    <input type="button" id="btnClear" class="btn btn-primary" value="Limpiar" onclick="Clear();" />
                </div>


            </div>

        </div>
    </div>

    <!-- LISTADO PRODUCTOS -->
            <div class="row">
                <div class="col-lg-12">
                    <table id="table_productos" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%">
                
                    </table>
                </div>
            </div>
            <!-- LISTADO PRODUCTOS -->

    <div class="row">
        <div class="form-group">
            <div class="col-md-6"></div>
            
            <%--<div class="col-md-2">
                <input type="button" class="btn btn-primary" id="btnLimpiar" value="Limpiar" onclick="Clear();" />
            </div>--%>

            <div class="col-md-2">
                <label for="txtSubTotal">SubTotal</label>
                <input type="text" id="txtSubTotal" readonly="" class="form-control precio" value="$ 0.00" />
            </div>
            <div class="col-md-2">
                <label for="txtIva">IVA</label>
                <input type="text" id="txtIva" readonly="" class="form-control precio" value="$ 0.00" />
            </div>
            <div class="col-md-2">
                <label for="txtTotalPartida">Total</label>
                <input type="text" id="txtTotalPartida" readonly="" class="form-control precio" value="$ 0.00" />
            </div>
        </div>
    </div>
    <br />
    <div class="row text-center">
       
        <input type="button" value="Regresar" onclick="window.location='CotizacionListado.aspx'" class="btn btn-primary" id="btnRegresar" />
        <input type="button" id="btnPreview" value="Vista Previa" onclick="Preview()" class="btn btn-primary" />
        <input type="button" value="Guardar" onclick="Guardar();" class="btn btn-primary" id="btnGuardar" />
        
    </div>
</asp:Content>

