using System;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using ClasicoConcreto.Entity;
using System.IO;
using Newtonsoft.Json;
using System.Web.Services;
using ClasicoConcreto.Bussines;
using System.Data;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

public partial class Pages_Admin_Factura : BasePage
{
    private string strDirectoryTemp, strDirectory, strFileFacturaPDF, strFileFacturaXML, strFileRemision;
    protected void Page_Load(object sender, EventArgs e)
    {
        strDirectory = GetParametro(ParametroFactura.RutaFisicaFacturas); //strRutaFisicaFacturas;
        strDirectoryTemp = Server.MapPath("~/Temp/");
        
        strFileFacturaPDF = ClasicoConcreto.SEMSession.GetInstance.IntCliente.ToString() + "_factura.pdf";
        strFileFacturaXML = ClasicoConcreto.SEMSession.GetInstance.IntCliente.ToString() + "_factura.xml";
        strFileRemision = ClasicoConcreto.SEMSession.GetInstance.IntCliente.ToString() + "_remision.pdf";
        if (!IsPostBack)
        {
            txtFechaInicio.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
            txtFechaFin.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        btnGuardar.Attributes.Add("onclick", "if($('#ctl00_BodyContent_hdnPedidos').val() == ''){ alert('Favor de seleccionar un pedido.'); return false;}");
        string target = Request.Params.Get("__EVENTTARGET");
        if (target != null)
        {
            FileType fileType;
            Enum.TryParse(target, out fileType);
            UploadFile(fileType);
        }
    }

    #region eventos
    protected void UploadFile(FileType fileType)
    {
        try
        {
            //Variables
            string strFileName = "";
            string strPathFile;
            FileUpload fileUpload = new FileUpload();
            Label lblFileUpload = new Label();
            switch (fileType)
            { 
                case FileType.FacturaPDF:
                    strFileName = strFileFacturaPDF;
                    fileUpload = fileFacturaPDF;
                    lblFileUpload = lblFacturaPDF;
                    break;
                case FileType.FacturaXML:
                    strFileName = strFileFacturaXML;
                    fileUpload = fileFacturaXML;
                    lblFileUpload = lblFacturaXML;
                    
                    break;
                case FileType.Remision:
                    strFileName = strFileRemision;
                    fileUpload = fileRemision;
                    lblFileUpload = lblFacturaRemision;
                    break;
                default:
                    return;
            }
            strPathFile = strDirectoryTemp + strFileName;
            fileUpload.PostedFile.SaveAs(strPathFile);
            lblFileUpload.Text = fileUpload.FileName;
            fileUpload.Visible = false;
            if (FileType.FacturaXML == fileType)
                LeerFacturaXML();

        }
        catch (Exception ex){
        
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        GuardarFactura();
    }

    #endregion

    #region metodos
    protected void GuardarFactura()
    {
        try
        {
            //DateTime dtFecha = DateTime.Now;
            //string strFecha = dtFecha.ToString("ddMMyyyy");
            string strPedidos;
            Entity_Facturas entFactura = EntFactura;
            Facturas objFacturas = new Facturas();
            strPedidos = hdnPedidos.Value;
            entFactura.strPedidos = strPedidos;
            entFactura.strFactura = txtFactura.Text;
            int intCliente = ClasicoConcreto.SEMSession.GetInstance.IntCliente;
            entFactura.strUsuarioAlta = intCliente.ToString();
            objFacturas.Valida(entFactura);
            if (!MoveFiles(ref entFactura))
            {
                throw new Exception("Error al tratar de guardar los archivos.");
            }
            objFacturas.Save(entFactura);

            //Envio de facturas por Email
            //EnvioEmail();
            
            Response.Redirect("FacturaListado.aspx", false);
        }
        catch(Exception ex)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "msf", "alert('" + ex.Message + "'); BuscarPedidos();", true);
        }
    }
    #endregion

    protected void EnvioEmail()
    {
        try
        {
            string strPlantilla = "";
            string local = AppDomain.CurrentDomain.BaseDirectory + "Temp\\AltaFactura.htm";
            HtmlDocument doc = new HtmlDocument();
            doc.Load(local);
            strPlantilla = doc.DocumentNode.InnerHtml;


            Facturas objFactura = new Facturas();
            DataTable dtPedidos = objFactura.GetListPedidosSinEnviar(txtFactura.Text);
            foreach (DataRow dtRow in dtPedidos.Rows)
            {
                int intEmpresa = dtRow.Field<int>("intEmpresa");
                int intPedido = dtRow.Field<int>("intPedido");
                string strEmail = dtRow.Field<string>("strEmail");
                string strPDF = dtRow.Field<string>("strPDF");
                string strXML = dtRow.Field<string>("strXML");
                string strFactura = dtRow.Field<string>("strFactura");
                string strSerie = dtRow.Field<string>("strSerie");

                string strContenido = strPlantilla;

                strContenido = strContenido = strContenido.Replace("[@PEDIDO]", intPedido.ToString());
                strContenido = strContenido.Replace("[@FECHA]", DateTime.Now.ToString("dd/MM/yyyy"));
                strContenido = strContenido.Replace("[@FACTURA]", strSerie + " " + strFactura);


                Entity_Email entEmail = new Entity_Email();
                Email objEmail = new Email();

                entEmail.lstAttachmentPath.Add(strDirectory + strPDF);
                entEmail.lstAttachmentPath.Add(strDirectory + strXML);
                entEmail.strFrom = new MailAddress("SEM@marfil.com", "Marfil Sistemas");
                entEmail.strSubject = "Envió de factura";
                entEmail.strContenido = strContenido;
                MailAddress entTo = new MailAddress(strEmail, strEmail);

                List<MailAddress> lstBCC = new List<MailAddress>();
                lstBCC.Add(new MailAddress("juliosoto@marfil.com", "Julio Soto"));
                //MailAddress entTo = new MailAddress("juliosoto@marfil.com", "juliosoto@marfil.com");
                entEmail.lstTo.Add(entTo);
                entEmail.lstBBC = lstBCC;
                

                if (objEmail.Send(entEmail))
                {
                    objFactura.SaveEstatusEmail(intEmpresa, strFactura, strSerie);
                }
                else
                {
                    entTo = new MailAddress("juliosoto@marfil.com", "juliosoto@marfil.com");
                    entEmail.lstTo.Clear();
                    entEmail.lstTo.Add(entTo);
                    entEmail.strFrom = new MailAddress("SEM@marfil.com", "Marfil Sistemas");
                    entEmail.strSubject = "Error al enviar la factura al cliente";
                    entEmail.lstAttachmentPath.Clear();
                    objEmail.Send(entEmail);
                }


            }
        }
        catch { }

    }



    #region MoveFiles
    /*Devuelve true cuando todos los archivos se movieron correctamente*/
    public bool MoveFiles(ref Entity_Facturas entFactura)
    {
        bool bMoveValid = true;
        DateTime dtFecha = DateTime.Now;
        string strFecha = dtFecha.ToString("ddMMyyyy");
        string strNameFacturaPDF, strNameFacturaXML, strNameRemision;
        strNameFacturaPDF = strFecha + "-" + entFactura.strFactura + ".pdf";
        strNameFacturaXML = strFecha + "-" + entFactura.strFactura + ".xml";
        strNameRemision = strFecha + "-" + entFactura.strFactura + "-remision.pdf";

        if (!MoveFile(strDirectoryTemp + strFileFacturaPDF, strDirectory + strNameFacturaPDF))
            bMoveValid = false;
        if (!MoveFile(strDirectoryTemp + strFileFacturaXML, strDirectory + strNameFacturaXML))
            bMoveValid = false;
        entFactura.strPDF = strNameFacturaPDF;
        entFactura.strXML = strNameFacturaXML;

        if (File.Exists(strDirectoryTemp + strFileRemision))
        {
            if (MoveFile(strDirectoryTemp + strFileRemision, strDirectory + strNameRemision))
            {
                entFactura.strRemision = strNameRemision;
            }
        }
        return bMoveValid;
    }
    #endregion


    #region MoveFile
    /*Devuelve true cuando todos los archivos se movieron correctamente*/
    public bool MoveFile(string strRutaTemp, string strRuta)
    {
        bool bMoveValid = true;
        try
        {
            //Se valida que exista el archivo temporal
            if (File.Exists(strRutaTemp))
            {
                //En caso de existir el archivo nuevo en la ruta, se elimina
                if (File.Exists(strRuta))
                    File.Delete(strRuta);

                File.Move(strRutaTemp, strRuta);
            }
        }
        catch
        {
            bMoveValid = false;
            return bMoveValid;
        }

        return bMoveValid;
    }
    #endregion

    #region LeerFacturaXML
    protected void LeerFacturaXML()
    {

        string strSerie, strFolio, strFolioFiscal, strTotal, strIva, strSubTotal, strRetencion, strDescuento, strMetodoPago, strFechaFactura, strOCR = "", strEmisorRFC, strReceptorRFC;
        string strFactura, strCliente;
        decimal decImporte, decIva, decSubTotal, decRetencion, decDescuento;
        DateTime datFechaFactura;

        XmlDocument doc = new XmlDocument();
        //Se carga xml del fileupload
        doc.Load(strDirectoryTemp + strFileFacturaXML);

        //Se remueven los prefijos namespace xmlns
        doc = UtilFunctions.RemoveAllNamespaces(doc);

        strSerie = UtilFunctions.GetValueAttribute(doc, "Comprobante", new string[] { "serie" });
        strFolio = UtilFunctions.GetValueAttribute(doc, "Comprobante", new string[] { "Folio", "NO.", "NUM" });
        strFolioFiscal = UtilFunctions.GetValueAttribute(doc, "", new string[] { "UUID" });
        strFolioFiscal = strFolioFiscal.Replace("-", "");
        strTotal = UtilFunctions.GetValueAttribute(doc, "Comprobante", new string[] { "Total", "Monto" });
        strIva = XMLIva(doc);
        strSubTotal = UtilFunctions.GetValueAttribute(doc, "Comprobante", new string[] { "Subtotal" });
        strRetencion = XMLRetencion(doc);
        strMetodoPago = UtilFunctions.GetValueAttribute(doc, "Comprobante", new string[] { "metodoDePago", "MetodoPago" });
        strFechaFactura = UtilFunctions.GetValueAttribute(doc, "Comprobante", new string[] { "Fecha" });
        strEmisorRFC = UtilFunctions.GetValueAttribute(doc, "Emisor", new string[] { "Rfc" });
        strReceptorRFC = UtilFunctions.GetValueAttribute(doc, "Receptor", new string[] { "Rfc" });
        strCliente = UtilFunctions.GetValueAttribute(doc, "Receptor", new string[] { "Nombre" });
        strDescuento = UtilFunctions.GetValueAttribute(doc, "Comprobante", new string[] { "Descuento" });

        if (File.Exists(strDirectoryTemp + strFileFacturaPDF))
        {
            strOCR = UtilFunctions.OCR(strDirectoryTemp + strFileFacturaPDF);
        }
        strFactura = strFolio;
        decimal.TryParse(strTotal, out decImporte);
        decimal.TryParse(strIva, out decIva);
        decimal.TryParse(strSubTotal, out decSubTotal);
        decimal.TryParse(strRetencion, out decRetencion);
        decimal.TryParse(strDescuento, out decDescuento);

        DateTime.TryParse(strFechaFactura, out datFechaFactura);



        //Valida que la empresa exista
        Entity_Empresas entEmpresas = new Entity_Empresas();
        Empresas objEmpresa = new Empresas();
        entEmpresas = objEmpresa.GetByRfc(strEmisorRFC);
        int intEmpresa = entEmpresas.intEmpresa;
        if (intEmpresa == 0)
        {
            Anthem.Manager.RegisterStartupScript(Page.GetType(), "msgXMLError", "alert('La razón social no existe en la base de datos.');", true);
            return;
        }
        else
        {

            txtFactura.Text = strFactura;
            txtSubtotal.Text = strSubTotal;
            txtIva.Text = strIva;
            txtTotal.Text = strTotal;

            Entity_Facturas entFactura = new Entity_Facturas();
            entFactura.strSerie = strSerie;
            entFactura.strFactura = strFactura;
            entFactura.strFolioFiscal = strFolioFiscal;
            entFactura.dblImporte = decImporte;
            entFactura.dblIva = decIva;
            entFactura.dblSubTotal = decSubTotal;
            entFactura.dblRetencion = decRetencion;
            entFactura.strMetodoPago = strMetodoPago;
            entFactura.datFechaFactura = datFechaFactura;
            entFactura.strOCR = strOCR;
            entFactura.strEmisorRFC = strEmisorRFC;
            entFactura.strReceptorRFC = strReceptorRFC;
            entFactura.strMaquinaAlta = ClasicoConcreto.SEMSession.GetInstance.StrMaquina;
            entFactura.strCliente = strCliente;
            entFactura.intEstatus = 1;
            entFactura.intEmpresa = intEmpresa;
            entFactura.dblDescuento = decDescuento;

            ViewState["vsFactura"] = entFactura;
            //ClientScript.RegisterStartupScript(Page.GetType(), "msf", "BuscarPedidos();", true);
        }
    }
    #endregion

    #region XMLIva
    private string XMLIva(XmlDocument doc)
    {
        string strIva = "";
        decimal dblIva = 0;
        XmlNodeList nodos;
        nodos = doc.SelectNodes("/Comprobante/Impuestos/Traslados/Traslado[@impuesto='IVA']");
        if (nodos.Count == 0)
        {
            nodos = doc.SelectNodes("/Comprobante/Impuestos/Traslados/Traslado[@Impuesto='002']"); //Version 3.3 sat xml
            if (nodos.Count == 0)
            {
                nodos = doc.SelectNodes("/Comprobante/Impuestos/Traslados/Traslado[@Impuesto='003']"); //Version 3.3 sat xml
            }
        }
        foreach (XmlNode nodo in nodos)
        {
            string strValor = UtilFunctions.GetValueAttribute(nodo, new string[] { "importe" });
            decimal dblValor;
            decimal.TryParse(strValor, out dblValor);
            dblIva += dblValor;
        }

        strIva = dblIva.ToString();
        return strIva;
    }
    #endregion

    #region XMLRetencion
    private string XMLRetencion(XmlDocument doc)
    {
        string strRetencion = "";
        decimal dblRetencion = 0;
        XmlNodeList nodos = doc.SelectNodes("/Comprobante/Impuestos/Retenciones/Retencion[@impuesto='IVA']");
        if (nodos.Count == 0)
        {
            nodos = doc.SelectNodes("/Comprobante/Impuestos/Retenciones/Retencion[@Impuesto='002']"); //Version 3.3 sat xml
        }
        foreach (XmlNode nodo in nodos)
        {
            string strValor = UtilFunctions.GetValueAttribute(nodo, new string[] { "importe" });
            decimal dblValor;
            decimal.TryParse(strValor, out dblValor);
            dblRetencion += dblValor;
        }

        strRetencion = dblRetencion.ToString();
        return strRetencion;
    }
    #endregion

    #region WebMethod
    [WebMethod]
    public static string[] GetList(string strFechaInicio, string strFechaFin)
    {
        string[] rtnData = new string[2];
        try
        {
            DateTime dtInicio, dtFin;
            DateTime.TryParse(strFechaInicio, out dtInicio);
            DateTime.TryParse(strFechaFin, out dtFin);

            Pedido objPedido = new Pedido();
            DataTable lst = objPedido.GetListSinFactura(dtInicio, dtFin);
            rtnData[0] = "ok";
            rtnData[1] = JsonConvert.SerializeObject(lst);
        }
        catch
        {
            rtnData[0] = "no";
            rtnData[1] = "[]";
        }
        return rtnData;
    }
    #endregion

    #region enum
    public enum FileType
    {
        FacturaPDF = 1,
        FacturaXML = 2,
        Remision = 3
    }
    #endregion

    #region EntFactura
    public Entity_Facturas EntFactura
    {
        get
        {
            if (!(ViewState["vsFactura"] is Entity_Facturas))
                ViewState["vsFactura"] = new Entity_Facturas();

            return (Entity_Facturas)ViewState["vsFactura"];
        }
    }
    #endregion
    protected void txt_Click(object sender, EventArgs e)
    {
      //  EnvioEmail();
    }
}