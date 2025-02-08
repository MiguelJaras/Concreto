<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.master" AutoEventWireup="true" CodeFile="CapturaConsumo.aspx.cs" Inherits="Pages_Catalogos_CapturaConsumo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="../../Scripts/pages/catalogos/Consumo.js"></script>
        <!-- Parsley -->
    <script src="../../Scripts/vendor/parsleyjs/dist/parsley.min.js"></script>
    <script src="../../Scripts/vendor/parsleyjs/dist/i18n/es.js"></script>

    <style>
        .parsley-required{
            color:red;   
            font-size:10PX;
            text-align:center;
        }
 
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
        <h2>Captura Consumo</h2>
        <hr />                      
    <div class="row form-horizontal">
        <div class="col-lg-12">
            <div class="form-group">
                <label class="col-md-1 control-label">Fecha Inicial</label>
                <div class="input-group col-md-2">
                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></span>
                    <asp:textbox ID="txtFechaInicio" runat="server" CssClass="form-control"></asp:textbox>
                </div>          
            </div>
            <div class="form-group">
                <label class="col-md-1 control-label">Fecha Final</label>
                <div class="input-group col-md-2">
                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></span>
                    <asp:textbox ID="txtFechaFin" runat="server" CssClass="form-control"></asp:textbox>
                </div>
            </div>

 
             <div class="form-group ">
                <label class="col-md-1 control-label"></label>
                <div class="col-md-2">
                    <input type="button" class="btn btn-primary btn-block" value="Filtrar" onclick="FiltroConsumo();" />
                </div>

               <label class="col-md-1 control-label"></label>
                <div class="col-md-2">
                
                  <button type="button" class="btn btn-primary btn-block" data-toggle="modal" data-target="#mdlConsumo">Nuevo</button>
                </div>               
            </div>
        </div>
    </div>


  <div class="row">
    <div class="col-lg-12">          
      <table id="table_consumo" class="table table-striped table-bordered dt-responsive nowrap table-style1" style="width:100%"> </table>
     </div>
  </div>

<div id="mdlConsumo" class="modal fade " role="dialog" aria-hidden="true">
  <div class="modal-dialog modal-lg">
  
    <div class="modal-content">
      <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span></button>
            <h4 class="modal-title text-center">Captura de Consumo</h4>
      </div>
      <div class="modal-body">
      


    
         <div class="row" >
                   <div class="col-md-4 col-sm-12">
                         <div class="form-group">
                                 <label for="txtFechaConsumo">FECHA</label>                              
                                 <input type="text" id="txtFechaConsumo" class="form-control" data-toggle="datepicker"  required="required" />                       
                         </div>

                          <div class="form-group">
                               <label for="numPlanta">FECHA</label>  
                                <select id="numPlanta" class="form-control">
                                  <option value="1">1</option>
                                  <option value="2">2</option>
                                  <option value="3">3</option>
                                </select>
                          </div>

                           <div class="form-group">
                            <label for="txtConcPremM3">CONC_PREM M3</label>
                            <input type="text" id="txtConcPremM3" class="form-control"  required="required" />         
                          </div>

                           <div class="form-group">
                            <label for="txtAguaLto">AGUA LTO</label>
                            <input type="text" id="txtAguaLto" class="form-control"  required="required" />         
                          </div>

                          <div class="form-group">
                            <label for="txtArena4Ton">ARENA 4 TON</label>
                            <input type="text" id="txtArena4Ton" class="form-control"  required="required" />         
                          </div>

                           <div class="form-group">
                            <label for="txtArena5Ton">ARENA 5 TON</label>
                            <input type="text" id="txtArena5Ton" class="form-control"  required="required" />         
                          </div>

                           <div class="form-group">
                            <label for="txtCal">CAL</label>
                            <input type="text" id="txtCal" class="form-control"  required="required" />         
                          </div> 
               </div>
                 <div class="col-md-4 col-sm-12">
                         <div class="form-group">
                            <label for="txtCalFlow100">CAL FLOW 100</label>
                            <input type="text" id="txtCalFlow100" class="form-control"  required="required" />         
                          </div>

                          <div class="form-group">
                            <label for="txtCampOx4060">CAMP OX40/60</label>
                            <input type="text" id="txtCampOx4060" class="form-control"  required="required" />         
                          </div>

                          <div class="form-group">
                            <label for="txtCamcret">CAMPCRET</label>
                            <input type="text" id="txtCamcret" class="form-control"  required="required"/>         
                          </div>

                        <div class="form-group">
                            <label for="txtCamptard">CAMPTARD</label>
                            <input type="text" id="txtCamptard" class="form-control"  required="required" />         
                          </div>


                      <div class="form-group">
                        <label for="txtCementoTon">CEMENTO TON</label>
                        <input type="text" id="txtCementoTon" class="form-control"  required="required" />         
                      </div>

                      <div class="form-group">
                        <label for="txtFibraBolsa">FIBRA BOLSA</label>
                        <input type="text" id="txtFibraBolsa" class="form-control"  required="required"/>         
                      </div>                

                       <div class="form-group">
                        <label for="txtGrava1Ton">GRAVA 1 TON</label>
                        <input type="text" id="txtGrava1Ton" class="form-control"  required="required"/>         
                      </div>
                </div>
                  <div class="col-md-4 col-sm-12">                 
                      
                       <div class="form-group">
                        <label for="txtGrava2Ton">GRAVA 2 TON</label>
                        <input type="text" id="txtGrava2Ton" class="form-control" required="required"/>         
                      </div>

           
                       <div class="form-group">
                        <label for="txtImper">IMPER:</label>
                        <input type="text" id="txtImper" class="form-control"  required="required" />         
                      </div>

                     <div class="form-group">
                        <label for="txtMortardELto">MORTARD E LTO:</label>
                        <input type="text" id="txtMortardELto" class="form-control"  required="required" />         
                      </div>

                       <div class="form-group">
                        <label for="txtColorByFerrox">COLOR BY FERROX</label>
                        <input type="text" id="txtColorByFerrox" class="form-control"  required="required" />         
                      </div>     
                     
                        <input type="hidden" id="hdnIntMaterial" class="form-control"  />
               </div>
        </div>
      </div>
      <div class="modal-footer">
 
          <button type="button" onclick="Guardar();" class="btn btn-success">Guardar</button>
      </div>
    </div>
  </div>
</div>
 


</asp:Content>

