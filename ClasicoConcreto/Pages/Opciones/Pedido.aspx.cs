using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClasicoConcreto.Entity;
using ClasicoConcreto.Bussines;
using System.Data;
using Newtonsoft.Json;
using System.Web.Services;
using System.Text.RegularExpressions;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net.Mail;
using System.IO;
using ExtensionMethods;
using HtmlAgilityPack;
using System.Text;

public partial class Pages_Opciones_Pedido : BasePage
{
    public string strInsumos = "";
    public string strServicios = "";
    public int intCliente = 0; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!ClasicoConcreto.SEMSession.GetInstance.BPrecioEditable){
            txtPrecio.Attributes.Add("readonly", "readonly");
        }
        //txtRemision.Attributes.Add("readonly", "readonly");
        txtSubTotal.Attributes.Add("readonly", "readonly");
        txtIva.Attributes.Add("readonly", "readonly");
        txtTotal.Attributes.Add("readonly", "readonly");
        txtFechaEntrega.Attributes.Add("readonly", "readonly");

        
        if (!IsPostBack)
        {
            int intPedido = 0;
            txtFechaEntrega.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
            GetCiudades();
            GetEstado();
            GetPorcentaje();

            decimal porcentaje = Convert.ToDecimal(ddlPorcentaje.SelectedValue);
            GetProductos(porcentaje);
           
          
            GetHoras();
            GetPorcIva();
            if (Request.QueryString["id"] != null)
            {
                int.TryParse(Request.QueryString["id"].ToString(), out intPedido);
                txtRemision.Text = intPedido.ToString();
                GetDatosPedido(intPedido);
            }
            GetServicios();
        }
        intCliente = ClasicoConcreto.SEMSession.GetInstance.IntCliente;
        hdnCliente.Value = intCliente.ToString();
        
    }

    #region EVENTOS

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        GuardarPedido();
    }

    protected void btnEmail_Click(object sender, EventArgs e)
    {
        //EnviarEmail();
    }

    #endregion EVENTOS

    #region CATALOGOS

    protected void GetPorcIva()
    { 
        ddlPorcIva.Items.Insert(0, new ListItem("16.00", "16.00"));
        int intCliente = ClasicoConcreto.SEMSession.GetInstance.IntCliente;
        if (intCliente == 3 || intCliente == 9)
        {
            //ddlPorcIva.Items.Insert(1, new ListItem("8.00", "8.00"));
            ddlPorcIva.Items.Insert(1, new ListItem("0.00", "0.00"));
        }
        ddlPorcIva.SelectedValue = "16.00";
    }
    protected void ddlPorcentaje_Change(object sender, EventArgs e)
    {
        ddlProducto.Items.Clear();
        decimal porcentaje = Convert.ToDecimal(ddlPorcentaje.SelectedValue);
        GetProductos(porcentaje);
        GetServiciosPorcentaje(porcentaje);
    }

    protected void GetProductos(decimal dblPorcentaje)
    {
        txtPrecio.Text = "0.00";
        int intLista = ClasicoConcreto.SEMSession.GetInstance.IntLista;
        Producto obj = new Producto();
        var lst = obj.GetListActivos(intLista, dblPorcentaje);
        ddlProducto.DataSource = lst;
        ddlProducto.DataTextField = "strNombre";
        ddlProducto.DataValueField = "intProducto";
        ddlProducto.DataBind();
        ddlProducto.Items.Insert(0, new ListItem("--Seleccione--","0"));
        strInsumos = JsonConvert.SerializeObject(lst);
        hdnArrInsumos.Value = strInsumos;
    }

    protected void GetServiciosPorcentaje(decimal porcentaje)
    {
        int intLista = ClasicoConcreto.SEMSession.GetInstance.IntLista;
        ServiciosEspeciales obj = new ServiciosEspeciales();
        var lst = obj.GetListPrecioPorcentaje(intLista,porcentaje);
        rptServiciosEspeciales.DataSource = lst.Where(i => i.intServicio != 7 && i.intServicio != 1);
        rptServiciosEspeciales.DataBind();
        strServicios = JsonConvert.SerializeObject(lst);
        hdnArrServicios.Value = strServicios;
    }





    protected void GetServicios()
    {
        int intLista = ClasicoConcreto.SEMSession.GetInstance.IntLista;
        ServiciosEspeciales obj = new ServiciosEspeciales();

        Decimal decPorcentaje = Convert.ToDecimal(ddlPorcentaje.SelectedValue);

        var lst = obj.GetListPrecio(intLista, decPorcentaje);



        rptServiciosEspeciales.DataSource = lst.Where(i=> i.intServicio != 7 && i.intServicio != 1);
        rptServiciosEspeciales.DataBind();
        strServicios = JsonConvert.SerializeObject(lst);
        hdnArrServicios.Value = strServicios;
    }

    protected void GetPorcentaje()
    {
     
        Producto obj = new Producto();
        var lst = obj.GetListPorcentaje();
        ddlPorcentaje.DataSource = lst;
        ddlPorcentaje.DataTextField = "Nombre";
        ddlPorcentaje.DataValueField = "PorcentajePrecio";
        ddlPorcentaje.DataBind();
        ddlPorcentaje.Items.Insert(0, new ListItem(" REMISIONADO ", "0.00"));
        
    }



    protected void GetHoras()
    {
        Horas objHoras = new Horas();
        List<Entity_Horas> lst = objHoras.GetList();
        ddlHoras.DataSource = lst;
        ddlHoras.DataTextField = "strHora";
        ddlHoras.DataValueField = "strHora";
        ddlHoras.DataBind();
        ddlHoras.Items.Insert(0, new ListItem("--Seleccione--", "0"));
    }

    protected void GetCiudades()
    {
        CITY obj = new CITY();
        var lst = obj.GetList();
        ddlCiudad.DataSource = lst;
        ddlCiudad.DataTextField = "City";
        ddlCiudad.DataValueField = "City";
        ddlCiudad.DataBind();
        ddlCiudad.Items.Insert(0, new ListItem("--Seleccione--", ""));
        ddlCiudad.SelectedValue = ClasicoConcreto.SEMSession.GetInstance.StrCity;
        
    }

    protected void GetEstado()
    {
        STATE obj = new STATE();
        var lst = obj.GetList();
        ddlEstado.DataSource = lst;
        ddlEstado.DataTextField = "State_Code";
        ddlEstado.DataValueField = "State_Code";
        ddlEstado.DataBind();
        ddlEstado.Items.Insert(0, new ListItem("--Seleccione--", ""));
        ddlEstado.SelectedValue = ClasicoConcreto.SEMSession.GetInstance.StrStateCode;
    }

    #endregion CATALOGOS

    #region METODOS

    protected void GuardarPedido()
    {

        
        string strRemision, strHoraEntrega;
        int intTipoPrecio, intProducto, intPedido, intPartida = 0;
        decimal decPorcIva, decSubTotal, decIva, decTotal, decCantidad, decPorcentaje,decPrecio = 0;
        DateTime datFechaEntrega;
        try
        {
            int intCliente = ClasicoConcreto.SEMSession.GetInstance.IntCliente;
            string strMaquina = ClasicoConcreto.SEMSession.GetInstance.StrMaquina;
            int intLista = ClasicoConcreto.SEMSession.GetInstance.IntLista;
            int.TryParse(ddlProducto.SelectedValue, out intProducto);
            decimal.TryParse(txtCantidad.Text, out decCantidad);
            DateTime.TryParse(txtFechaEntrega.Text, out datFechaEntrega);
            bool blnEmailFactura = rdbEmailFactura.Checked;
            int intFactura = 0;
            if(blnEmailFactura){
                intFactura = 1;
            }

            strRemision = txtRemision.Text;

            //combo de porcentaje 

            decPorcentaje = Convert.ToDecimal(ddlPorcentaje.SelectedValue);

            int.TryParse(strRemision, out intPedido);
            intTipoPrecio = 3;
            decimal.TryParse(ddlPorcIva.SelectedValue, out decPorcIva); //decPorcIva = 16;
            decSubTotal = 0; //decimal.Parse(txtSubTotal.Text, NumberStyles.Currency);
            decIva = 0; //decimal.Parse(txtIva.Text, NumberStyles.Currency);
            decTotal = 0; //decimal.Parse(txtTotal.Text, NumberStyles.Currency);
            if (txtPrecio.Text != "")
            {
                decPrecio = decimal.Parse(txtPrecio.Text, NumberStyles.Currency);
            }

            strHoraEntrega = ddlHoras.Text;
            int.TryParse(hdnIntPartida.Value, out intPartida);
            TimeSpan tm;
            TimeSpan.TryParse(strHoraEntrega, out tm);
            strHoraEntrega = tm.ToString(@"hh\:mm");

            string strServicios = hdnServicios.Value;
            int intServicioBombeo = 1;
            bool blnServicioBombeo = intServicioBombeo.ContainsValue(strServicios.Split(new char[] { ',' }));
            
            Pedido objPedido = new Pedido();
            Entity_Pedido entPedido = new Entity_Pedido();
            entPedido.intPedido = intPedido;
            entPedido.Project_Code = ClasicoConcreto.SEMSession.GetInstance.StrProject_Code;
            entPedido.PO_Num = txtOrdenCompra.Text;
            entPedido.intTipoPrecio = intTipoPrecio;
            entPedido.strCliente = txtCliente.Text;
            entPedido.strEncargado = txtEncargadoObra.Text;
            entPedido.strTelefonos = txtTelefonos.Text;
            entPedido.strCalle = txtCalle.Text;
            entPedido.strColonia = txtColonia.Text;
            entPedido.strCalleEntre = txtEntreCalles.Text;
            entPedido.strCalleEntre2 = txtEntreCalles2.Text;
            entPedido.dblPorcentajeIva = decPorcIva;
            entPedido.dblSubtotal = decSubTotal;
            entPedido.dblIva = decIva;
            entPedido.dblTotal = decTotal;
            entPedido.Order_Status = "O";
            entPedido.State_Code = ddlEstado.SelectedValue;
            entPedido.City = ddlCiudad.SelectedValue;
            entPedido.Postal_Code = txtCodigoPostal.Text;
            entPedido.Delivery_Instructions = txtObservaciones.Text;
            entPedido.intCliente = intCliente;
            entPedido.strMaquina = strMaquina;
            entPedido.datFechaEntrega = datFechaEntrega;
            entPedido.strElemento = txtElemento.Text;
            entPedido.strEmail = txtEmail.Text.Replace("EXAMPLE@MARFIL.COM", "");
            entPedido.strVendedor = txtVendedor.Text;
            entPedido.intFactura = intFactura;
            entPedido.dblPorcentaje = Convert.ToDecimal(ddlPorcentaje.SelectedValue);



            Entity_PedidoDet entPedidoDet = new Entity_PedidoDet();
            entPedidoDet.intPedido = intPedido;
            entPedidoDet.intPartida = intPartida;
            entPedidoDet.intProducto = intProducto;
            entPedidoDet.strHoraEntrega = strHoraEntrega;
            entPedidoDet.item_Code = "";
            entPedidoDet.dblCantidad = decCantidad;
            entPedidoDet.dblPrecio = decPrecio;
            

            //Se valida pedido
            objPedido.ValidarPedido(entPedido, blnServicioBombeo);

            //Se valida detalle del pedido
            if (intProducto > 0)
            {
                objPedido.ValidarPedidoDet(entPedido, entPedidoDet, blnServicioBombeo);
            }

            //Guardar Pedido
            intPedido = objPedido.SavePedido(entPedido);
            txtRemision.Text = intPedido.ToString();
            if(intProducto > 0)
            {
                //Se asigna idPedido generado
                entPedidoDet.intPedido = intPedido;

                //Se guarda producto y servicios del pedido 
                intPartida = objPedido.SavePedidoDet(entPedidoDet, blnServicioBombeo);
                objPedido.DeletePedidoServicio(intPedido, intPartida);
                int intTipoBombeo;
                int.TryParse(hddTipoBombeo.Value, out intTipoBombeo);

                if (!string.IsNullOrEmpty(strServicios))
                {
                    string[] arrServicios = strServicios.Split(new char[] { ',' });
                    foreach (string strServicio in arrServicios)
                    {
                        bool bGrua = false;
                        int intServicio;
                        int.TryParse(strServicio.Trim(), out intServicio);
                        
                        if(intServicio == 1){
                            if (hdnGrua.Value == "2") {
                                bGrua = true;
                            }
                        }
                        objPedido.SavePedidoDetServicio(intPedido, intPartida, intServicio, intLista, decCantidad, bGrua, intTipoBombeo, decPorcentaje);
                    }
                }
                string strCantidadTuberia = hddCantidadTuberia.Value;
                decimal dblCantidadTuberia;
                decimal.TryParse(strCantidadTuberia, out dblCantidadTuberia);
                if (dblCantidadTuberia > 0)
                    objPedido.SavePedidoDetServicio(intPedido, intPartida, 7, intLista, dblCantidadTuberia, false, intTipoBombeo, decPorcentaje);
            }

            //Calcular precios
            objPedido.SavePedidoPrecio(intPedido);

            GetDatosPedido(intPedido);
            ClientScript.RegisterStartupScript(Page.GetType(), "msf", "DatosGuardados();", true);
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "msf", "alert(\"" + ex.Message + "\"); EditarPartida(" + intPartida + ")", true);
        }
    }


    protected static void EnviarEmail(int intPedido)
    {
        //int intPedido;
        //int.TryParse(txtRemision.Text.ToString(), out intPedido);
        if (intPedido > 0)
        {
            //string end = (Request.ApplicationPath.EndsWith("/")) ? "" : "/";
            //string path = Request.ApplicationPath + end;
            string PathReport = AppDomain.CurrentDomain.BaseDirectory + "Pages\\Reportes\\Concreto.rpt";
            string PathEnviar = AppDomain.CurrentDomain.BaseDirectory + "Temp\\Pedido_" + intPedido.ToString() + ".pdf";
            try
            {
                CrystalDecisions.CrystalReports.Engine.ReportDocument RptImpresion = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                RptImpresion.Load(PathReport);
                ExportaReportes(RptImpresion, PathEnviar, intPedido);

                MailAddress strFrom = new MailAddress("SEM@marfil.com", "Marfil Sistemas");
                string strContenido = "<html><body><table width='95%'><tr><td><font size='4' color='black' style='font-weight:bold'>Clasico Concreto</font></td></tr>";
                strContenido = strContenido + "<tr><td> <font size='2' color='black' style='font-family:Arial'> </font><font size='2' color='black' style='font-family:Arial'>Se ha generado un nuevo pedido</font><br/><br/></td></tr>";
                strContenido = strContenido + "<tr><td> <font size='2' color='black' style='font-family:Arial'></font><font size='2' color='black' style='font-family:Arial'> </font><br/><br/></td></tr>";
                strContenido = strContenido + "<tr><td> <font size='2' color='black' style='font-family:Arial'>Atte.</font><font size='2' color='black' style='font-family:Arial'> </font></td></tr>";
                strContenido = strContenido + "<tr><td> <font size='2' color='black' style='font-family:Arial'>Clásico Concreo</font><font size='2' color='black' style='font-family:Arial'> </font></td></tr>";
                strContenido = strContenido + "<tr><td> <font size='2' color='black' style='font-family:Arial'></font><font size='2' color='black' style='font-family:Arial'> </font></td></tr>";
                strContenido = strContenido + "</table></body></html>";

                //erickechevarria@marfil.com
                //rosalindachavez@marfil.com


                string strEmailCliente = "erickechevarria@clasicoconcreto.com";//ClasicoConcreto.SEMSession.GetInstance.StrEmail; //"juliosoto@marfil.com"
                string strNombre = ClasicoConcreto.SEMSession.GetInstance.StrNombre;
                List<MailAddress> lstTo = new List<MailAddress>();
                List<MailAddress> lstCC = new List<MailAddress>();
                List<MailAddress> lstBBC = new List<MailAddress>();
                lstTo.Add(new MailAddress("erickechevarria@clasicoconcreto.com", "Erick Echevarria"));
                //lstCC.Add(new MailAddress("rosalindachavez@marfil.com", "Rosalinda Chavez"));
                lstCC.Add(new MailAddress("xochitlmelendez@concretoclasico.com", "Xochitl Nohemi Melendez lopez"));
                lstCC.Add(new MailAddress("jazmingarza@clasicoconcreto.com", "Jazmin Graza"));
                lstBBC.Add(new MailAddress("juliosoto@marfil.com", "Julio César"));
                List<string> lstAttachment = new List<string>();
                lstAttachment.Add(PathEnviar);

                Entity_Email entEmail = new Entity_Email(strFrom, strContenido, lstTo, "Nuevo Pedido " + intPedido.ToString(), lstAttachment);
                entEmail.lstCC = lstCC;
                entEmail.lstBBC = lstBBC;
                Email objEmail = new Email();
                objEmail.Send(entEmail);

                //ClientScript.RegisterStartupScript(Page.GetType(), "msgEr", "alert('Se envio correctamente el correo a la direccion: " + txtEmail.Text + "'); window.close();", true);
            }
            catch (Exception ex)
            {
                
            }

            System.IO.File.Delete(PathEnviar);
        }
    }

    protected static void EmailCancelar(int intPedido)
    {
        if (intPedido > 0)
        {
            try
            {
                string strCliente = ClasicoConcreto.SEMSession.GetInstance.StrNombre;
                MailAddress strFrom = new MailAddress("SEM@marfil.com", "Marfil Sistemas");

                string strContenido = "";
                string local = AppDomain.CurrentDomain.BaseDirectory + "Temp\\CancelarPedido.htm";
                HtmlDocument doc = new HtmlDocument();
                doc.Load(local);
                strContenido = doc.DocumentNode.InnerHtml;
                strContenido = strContenido.Replace("[@PEDIDO]", intPedido.ToString());
                strContenido = strContenido.Replace("[@FECHA]", DateTime.Now.ToString("dd/MM/yyyy"));
                strContenido = strContenido.Replace("[@CLIENTE]", strCliente);
                //erickechevarria@marfil.com
                //rosalindachavez@marfil.com


                //string strEmailCliente = "erickechevarria@marfil.com"; //"juliosoto@marfil.com"
                //string strNombre = ClasicoConcreto.SEMSession.GetInstance.StrNombre;
                List<MailAddress> lstTo = new List<MailAddress>();
                List<MailAddress> lstCC = new List<MailAddress>();
                List<MailAddress> lstBBC = new List<MailAddress>();
                lstTo.Add(new MailAddress("erickechevarria@clasicoconcreto.com", "Erick Echevarria"));
                //lstTo.Add(new MailAddress("juliosoto@marfil.com", "Julio César"));
                //lstCC.Add(new MailAddress("rosalindachavez@marfil.com", "Rosalinda Chavez"));
                lstBBC.Add(new MailAddress("juliosoto@marfil.com", "Julio César"));
                lstBBC.Add(new MailAddress("rubenmora@marfil.com", "Ruben Mora"));
                lstCC.Add(new MailAddress("xochitlmelendez@concretoclasico.com", "Xochitl Nohemi Melendez lopez"));
                lstCC.Add(new MailAddress("jazmingarza@clasicoconcreto.com", "Jazmin Graza"));
                List<string> lstAttachment = new List<string>();
                
                Entity_Email entEmail = new Entity_Email(strFrom, strContenido, lstTo, "Cancelación de Pedido " + intPedido.ToString(), lstAttachment);
                entEmail.lstBBC = lstBBC;
                entEmail.lstCC = lstCC;


                Email objEmail = new Email();
                objEmail.Send(entEmail);
            }
            catch (Exception ex)
            {

            }
        }
    }

    private static void SetReportConnectionInfo(ReportDocument reportDocument)
    {
        Servidor ser = new Servidor();
        Entity_Servidor obj = new Entity_Servidor();

        obj = ser.Credenciales();

        foreach (CrystalDecisions.CrystalReports.Engine.Table table in reportDocument.Database.Tables)
        {

            table.LogOnInfo.ConnectionInfo.ServerName = obj.StrSQLIP;
            table.LogOnInfo.ConnectionInfo.UserID = obj.StrSQLUser;
            table.LogOnInfo.ConnectionInfo.Password = obj.StrSQLPass;
            table.ApplyLogOnInfo(table.LogOnInfo);
        }
    }


    #region ExportaReportes
    public static void ExportaReportes(CrystalDecisions.CrystalReports.Engine.ReportDocument Rpt, string FName, int intPedido)
    {
        try
        {
            ReportDocument crReportDocument;
            ExportOptions crExportOptions;
            DiskFileDestinationOptions crDiskFileDestinationOptions;

            crReportDocument = Rpt;

            crReportDocument.SetParameterValue("@intPedido", intPedido);
            SetReportConnectionInfo(crReportDocument);
            //crReportDocument.SetDatabaseLogon("vetec", "vetec", "192.168.100.10", "dbConcreto", true);

            crDiskFileDestinationOptions = new DiskFileDestinationOptions();
            crDiskFileDestinationOptions.DiskFileName = FName;
            crExportOptions = new ExportOptions();

            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();

            crExportOptions = crReportDocument.ExportOptions;
            {
                crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                crExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                crExportOptions.DestinationOptions = crDiskFileDestinationOptions;
                crExportOptions.FormatOptions = CrFormatTypeOptions;
            }

            crReportDocument.Export();
            //System.IO.File.Delete(FName);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion
    

    public static decimal Parse(string input)
    {
        return decimal.Parse(Regex.Match(input, @"-?\d{1,3}(,\d{3})*(\.\d+)?").Value);
    }

    protected void GetDatosPedido(int intPedido) 
    {
        int intCliente = ClasicoConcreto.SEMSession.GetInstance.IntCliente;
        if (intCliente == 3 || intCliente == 9)
            intCliente = 0;
        //int intPedido;
        //int.TryParse(txtRemision.Text, out intPedido);
        Pedido objPedido = new Pedido();
        DataTable dtPedido = new DataTable();
        dtPedido = objPedido.GetPedido(intCliente, intPedido);
        if (dtPedido.Rows.Count > 0)
        {
            int intFactura = int.Parse(dtPedido.Rows[0]["intFactura"].ToString());
            txtOrdenCompra.Text = dtPedido.Rows[0]["PO_Num"].ToString();
            txtFechaEntrega.Text = DateTime.Parse(dtPedido.Rows[0]["datFechaEntrega"].ToString()).ToString("dd/MM/yyyy");
            txtCliente.Text = dtPedido.Rows[0]["strCliente"].ToString();
            txtEncargadoObra.Text = dtPedido.Rows[0]["strEncargado"].ToString();
            txtTelefonos.Text = dtPedido.Rows[0]["strTelefonos"].ToString();
            ddlEstado.SelectedValue = dtPedido.Rows[0]["State_Code"].ToString();
            ddlCiudad.SelectedValue = dtPedido.Rows[0]["City"].ToString();
            txtCalle.Text = dtPedido.Rows[0]["strCalle"].ToString();
            txtColonia.Text = dtPedido.Rows[0]["strColonia"].ToString();
            txtEntreCalles.Text = dtPedido.Rows[0]["strCalleEntre"].ToString();
            txtEntreCalles2.Text = dtPedido.Rows[0]["strCalleEntre2"].ToString();
            txtCodigoPostal.Text = dtPedido.Rows[0]["Postal_Code"].ToString();
            txtObservaciones.Text = dtPedido.Rows[0]["Delivery_Instructions"].ToString();
            txtSubTotal.Text = dtPedido.Rows[0]["dblSubtotal"].ToString();
            txtIva.Text = dtPedido.Rows[0]["dblIva"].ToString();
            txtTotal.Text = dtPedido.Rows[0]["dblTotal"].ToString();
            txtElemento.Text = dtPedido.Rows[0]["strElemento"].ToString();
            txtEmail.Text = dtPedido.Rows[0]["strEmail"].ToString();
            txtVendedor.Text = dtPedido.Rows[0]["strVendedor"].ToString();
            ddlPorcIva.SelectedValue = dtPedido.Rows[0]["dblPorcentajeIva"].ToString();
            int intClientePedido = int.Parse(dtPedido.Rows[0]["intCliente"].ToString());
            ddlPorcentaje.SelectedValue = dtPedido.Rows[0]["dblPorcentaje"].ToString();


            if (intFactura == 1)
            {
                rdbEmailFactura.Checked = true;
                rdbEmailFactura2.Checked = false;
            }
            else {
                rdbEmailFactura2.Checked = true;
                rdbEmailFactura.Checked = false;
                txtEmail.Text = "EXAMPLE@MARFIL.COM";
            }
            string strEstatus = dtPedido.Rows[0]["Order_Status"].ToString();
            hdnOrdenStatus.Value = strEstatus;
            //if(strEstatus.ToLower() != "o")
            if (intClientePedido == 16)
            {
                txtOrdenCompra.Attributes.Add("readonly", "readonly");
                txtFechaEntrega.Attributes.Add("readonly", "readonly");
                txtCliente.Attributes.Add("readonly", "readonly");
                txtEncargadoObra.Attributes.Add("readonly", "readonly");
                txtTelefonos.Attributes.Add("readonly", "readonly");
                ddlEstado.Enabled = false;
                ddlCiudad.Enabled = false;
                txtCalle.Attributes.Add("readonly", "readonly");
                txtColonia.Attributes.Add("readonly", "readonly");
                txtEntreCalles.Attributes.Add("readonly", "readonly");
                txtEntreCalles2.Attributes.Add("readonly", "readonly");
                txtCodigoPostal.Attributes.Add("readonly", "readonly");
                txtObservaciones.Attributes.Add("readonly", "readonly");
                txtSubTotal.Attributes.Add("readonly", "readonly");
                txtIva.Attributes.Add("readonly", "readonly");
                txtTotal.Attributes.Add("readonly", "readonly");
                txtElemento.Attributes.Add("readonly", "readonly");
                txtEmail.Attributes.Add("readonly", "readonly");
                txtVendedor.Attributes.Add("readonly", "readonly");
                rdbEmailFactura.Attributes.Add("readonly", "readonly");
                rdbEmailFactura2.Attributes.Add("readonly", "readonly");

            }
            if (strEstatus.ToLower() == "c")
                btnAceptar.Visible = false;

            if (intCliente == 0)
                objPedido.SetRead(intPedido);

        }
    }

    #endregion METODOS

    #region WEBMETHOD
    [WebMethod]
    public static string[] GetListPartidas(int intPedido)
    {
        string[] rtnData = new string[2];
        try
        {
            int intCliente = ClasicoConcreto.SEMSession.GetInstance.IntCliente;
            Pedido objPedido = new Pedido();
            DataTable dtPartidas = objPedido.GetListPedidoDet(intPedido, 0);
            rtnData[0] = "ok";
            rtnData[1] = JsonConvert.SerializeObject(dtPartidas);
        }
        catch (Exception ex)
        {
            rtnData[0] = "no";
            rtnData[1] = "[]";
        }
        return rtnData;
    }

    [WebMethod]
    public static string[] GetPartida(int intPedido,int intPartida)
    {
        string[] rtnData = new string[3];
        try
        {
            Pedido objPartida = new Pedido();
            DataTable dtPartida = objPartida.GetListPedidoDet(intPedido, intPartida);
            var lstServicios = objPartida.GetListPedidoDetServicios(intPedido, intPartida);
            rtnData[0] = "ok";
            rtnData[1] = JsonConvert.SerializeObject(dtPartida);
            rtnData[2] = JsonConvert.SerializeObject(lstServicios); ;
        }
        catch (Exception ex)
        {
            rtnData[0] = "no";
            rtnData[1] = "[]";
        }
        return rtnData;
    }

    [WebMethod]
    public static string[] EliminarPartida(int intPedido, int intPartida)
    {
        string[] rtnData = new string[2];
        try
        {
            Pedido objPartida = new Pedido();
            int intCliente = ClasicoConcreto.SEMSession.GetInstance.IntCliente;
            objPartida.DeletePedidoDet(intCliente, intPedido, intPartida);
            objPartida.SavePedidoPrecio(intPedido);
            rtnData[0] = "ok";
            rtnData[1] = "Partida eliminada correctamente.";
        }
        catch(Exception ex)
        {
            rtnData[0] = "no";
            rtnData[1] = ex.Message;
        }
        return rtnData;
    }

    [WebMethod]
    public static string[] EnviarPedido(int intPedido)
    {
        string[] rtnData = new string[2];
        try
        {
            if (intPedido > 0)
            {
                Pedido objPartida = new Pedido();
                int intCliente = ClasicoConcreto.SEMSession.GetInstance.IntCliente;
                objPartida.EnviarPedido(intCliente, intPedido);
                EnviarEmail(intPedido);
            }
            rtnData[0] = "ok";
            rtnData[1] = "Pedido Enviado Correctamente.";
        }
        catch
        {
            rtnData[0] = "no";
            rtnData[1] = "Error al tratar de enviar el pedido.";
        }
        return rtnData;
    }

    [WebMethod]
    public static string[] CancelarPedido(int intPedido)
    {
        string[] rtnData = new string[2];
        try
        {
            if (intPedido > 0)
            {
                Pedido objPartida = new Pedido();
                int intCliente = ClasicoConcreto.SEMSession.GetInstance.IntCliente;
                objPartida.CancelarPedido(intCliente, intPedido);
                EmailCancelar(intPedido);
            }
            rtnData[0] = "ok";
            rtnData[1] = "Pedido cancelado correctamente.";
        }
        catch(Exception ex)
        {
            rtnData[0] = "no";
            rtnData[1] = ex.Message;
        }
        return rtnData;
    }

    [WebMethod]
    public static void GuardarPedido(Entity_Pedido ent) 
    {
        try
        {
            
        }
        catch (Exception ex)
        {
            
        }
    }

    [WebMethod]
    public static string[] PreviewPedido(int intPedido)
    {
        string[] rtnData = new string[2];
        try
        {
            //Se crea archivo temporal
            string strFileName = "Pedido_" + ClasicoConcreto.SEMSession.GetInstance.IntCliente.ToString() + ".pdf";
            string PathReport = AppDomain.CurrentDomain.BaseDirectory + "Pages\\Reportes\\Concreto.rpt";
            string PathEnviar = AppDomain.CurrentDomain.BaseDirectory + "Temp\\" + strFileName;
            CrystalDecisions.CrystalReports.Engine.ReportDocument RptImpresion = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            RptImpresion.Load(PathReport);
            ExportaReportes(RptImpresion, PathEnviar, intPedido);
            
            rtnData[0] = "ok";
            rtnData[1] = strFileName;
        }
        catch(Exception ex)
        {
            rtnData[0] = "no";
            rtnData[1] = ex.Message;
        }
        return rtnData;
    }


    [WebMethod]
    public static string[] GetListVendedores()
    {
        string[] rtnData = new string[2];
        try
        {
            int intCliente = ClasicoConcreto.SEMSession.GetInstance.IntCliente;
            Pedido objPedido = new Pedido();
            DataTable dtVendedores = objPedido.GetListVendedores(intCliente);
            string[] arrVendedores = dtVendedores.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();


            rtnData[0] = "ok";
            rtnData[1] = JsonConvert.SerializeObject(arrVendedores);
        }
        catch
        {
            rtnData[0] = "no";
            rtnData[1] = "[]";
        }
        return rtnData;
    }


    [WebMethod]
    public static string[] GetListClientes()
    {
        string[] rtnData = new string[2];
        try
        {
            Pedido objPedido = new Pedido();
            DataTable dtClientes = objPedido.GetListClientes();
            string[] arrClientes = dtClientes.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();


            rtnData[0] = "ok";
            rtnData[1] = JsonConvert.SerializeObject(arrClientes);
        }
        catch
        {
            rtnData[0] = "no";
            rtnData[1] = "[]";
        }
        return rtnData;
    }
    #endregion

}