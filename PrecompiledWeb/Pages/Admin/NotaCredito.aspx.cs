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

public partial class Pages_Admin_NotaCredito : BasePage
{
    private string strDirectoryTemp, strDirectory, strFileNotaPDF, strFileNotaXML;
    protected void Page_Load(object sender, EventArgs e)
    {

        strDirectory = GetParametro(ParametroFactura.RutaFisicaNotasCredito); //strRutaFisicaFacturas;
        strDirectoryTemp = Server.MapPath("~/Temp/");

        strFileNotaPDF = ClasicoConcreto.SEMSession.GetInstance.IntCliente.ToString() + "_notacredito.pdf";
        strFileNotaXML = ClasicoConcreto.SEMSession.GetInstance.IntCliente.ToString() + "_notacredito.xml";
        
        string target = Request.Params.Get("__EVENTTARGET");
        if (target != null)
        {
            FileType fileType;
            Enum.TryParse(target, out fileType);
            UploadFile(fileType);
        }
    }

    #region eventos
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        GuardarNotaCredito();
    }
    #endregion



    #region metodos

    #region UploadFile
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
                case FileType.NotaPDF:
                    strFileName = strFileNotaPDF;
                    fileUpload = filePDF;
                    lblFileUpload = lblFilePDF;
                    break;
                case FileType.NotaXML:
                    strFileName = strFileNotaXML;
                    fileUpload = fileXML;
                    lblFileUpload = lblFileXML;

                    break;
                default:
                    return;
            }
            strPathFile = strDirectoryTemp + strFileName;
            fileUpload.PostedFile.SaveAs(strPathFile);
            lblFileUpload.Text = fileUpload.FileName;
            fileUpload.Visible = false;
            if (FileType.NotaXML == fileType)
                LeerFacturaXML();

        }
        catch (Exception ex)
        {

        }
    }
    #endregion

    #region LeerFacturaXML
    protected void LeerFacturaXML()
    {
        string strEmisorRFC, strReceptorRFC, strSerie, strFolio, strFolioFiscal, strImporte, strIva, strSubTotal, strMetodoPago, strOCR = "", strFechaFactura;
        decimal decImporte, decIva, decSubTotal;
        DateTime datFechaFactura;

        XmlDocument doc = new XmlDocument();
        //Se carga xml del fileupload
        doc.Load(strDirectoryTemp + strFileNotaXML);

        //Se remueven los prefijos namespace xmlns
        doc = UtilFunctions.RemoveAllNamespaces(doc);

        strEmisorRFC = UtilFunctions.GetValueAttribute(doc, "Emisor", new string[] { "Rfc" });
        strReceptorRFC = UtilFunctions.GetValueAttribute(doc, "Receptor", new string[] { "Rfc" });
        strSerie = UtilFunctions.GetValueAttribute(doc, "Comprobante", new string[] { "serie" });
        strFolio = UtilFunctions.GetValueAttribute(doc, "Comprobante", new string[] { "Folio", "NO.", "NUM" });
        strFolioFiscal = UtilFunctions.GetValueAttribute(doc, "", new string[] { "UUID" });
        strImporte = UtilFunctions.GetValueAttribute(doc, "Comprobante", new string[] { "Total", "Monto" });
        strIva = XMLIva(doc);
        strSubTotal = UtilFunctions.GetValueAttribute(doc, "Comprobante", new string[] { "Subtotal" });
        strMetodoPago = UtilFunctions.GetValueAttribute(doc, "Comprobante", new string[] { "metodoDePago", "MetodoPago" });
        strFechaFactura = UtilFunctions.GetValueAttribute(doc, "Comprobante", new string[] { "Fecha" });
        
        if (File.Exists(strDirectoryTemp + strFileNotaPDF))
        {
            strOCR = UtilFunctions.OCR(strDirectoryTemp + strFileNotaPDF);
        }

        decimal.TryParse(strImporte, out decImporte);
        decimal.TryParse(strIva, out decIva);
        decimal.TryParse(strSubTotal, out decSubTotal);
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
            txtFolio.Text = strSerie + strFolio;
            txtSubtotal.Text = strSubTotal;
            txtIva.Text = strIva;
            txtImporte.Text = strImporte;

            Entity_NotaCredito entNota = new Entity_NotaCredito();
            entNota.strFolio = strSerie + strFolio;
            entNota.strFolioFiscal = strFolioFiscal;
            entNota.dblImporte = decImporte;
            entNota.dblIva = decIva;
            entNota.dblSubTotal = decSubTotal;
            entNota.strOCR = strOCR;
            entNota.intEmpresa = intEmpresa;
            entNota.strMaquinaAlta = ClasicoConcreto.SEMSession.GetInstance.StrMaquina; ;
            entNota.datFecha = datFechaFactura;
            ViewState["vsNotaCredito"] = entNota;
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

    protected void GuardarNotaCredito()
    {
        try
        {
            Entity_NotaCredito entNotaCredito = EntNotaCredito;
            NotaCredito objNotaCredito = new NotaCredito();

            entNotaCredito.strFolio = txtFolio.Text;
            entNotaCredito.strSerie = txtSerie.Text;
            entNotaCredito.strFactura = txtFactura.Text;
            entNotaCredito.strMaquinaAlta = ClasicoConcreto.SEMSession.GetInstance.StrMaquina;

            objNotaCredito.Valida(entNotaCredito);
            if (!MoveFiles(ref entNotaCredito))
            {
                throw new Exception("Error al tratar de guardar los archivos.");
            }
            objNotaCredito.Save(entNotaCredito);
            Response.Redirect("NotaCreditoListado.aspx");
        }
        catch(Exception ex)
        {
            string script = "Swal.fire({icon: 'error', title: 'Error', text: '" + ex.Message.Replace("'", "\\'") + "'});";
            ClientScript.RegisterStartupScript(Page.GetType(), "msf", script, true);
        }
    }

    #region MoveFiles
    /*Devuelve true cuando todos los archivos se movieron correctamente*/
    public bool MoveFiles(ref Entity_NotaCredito entNotaCredito)
    {
        bool bMoveValid = true;
        DateTime dtFecha = DateTime.Now;
        string strFecha = dtFecha.ToString("ddMMyyyy");
        string strNameFacturaPDF, strNameFacturaXML;
        strNameFacturaPDF = strFecha + "-" + entNotaCredito.strFolio + "-NC.pdf";
        strNameFacturaXML = strFecha + "-" + entNotaCredito.strFolio + "-NC.xml";
        

        if (!MoveFile(strDirectoryTemp + strFileNotaPDF, strDirectory + strNameFacturaPDF))
            bMoveValid = false;
        if (!MoveFile(strDirectoryTemp + strFileNotaXML, strDirectory + strNameFacturaXML))
            bMoveValid = false;
        entNotaCredito.strPDF = strNameFacturaPDF;
        entNotaCredito.strXML = strNameFacturaXML;

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


    #endregion

    #region enum
    public enum FileType
    {
        NotaPDF = 1,
        NotaXML = 2
    }
    #endregion

    #region EntNotaCredito
    public Entity_NotaCredito EntNotaCredito
    {
        get
        {
            if (!(ViewState["vsNotaCredito"] is Entity_NotaCredito))
                ViewState["vsNotaCredito"] = new Entity_NotaCredito();

            return (Entity_NotaCredito)ViewState["vsNotaCredito"];
        }
    }
    #endregion
}