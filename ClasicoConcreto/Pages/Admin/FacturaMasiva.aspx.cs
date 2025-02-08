using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text.RegularExpressions;
using ClasicoConcreto.Entity;
using ClasicoConcreto.Bussines;

public partial class Pages_Admin_FacturaMasiva : BasePage
{
    public static string strExtensionFilePDF = ".pdf";
    public string strDirectoryTmp = "", strDirectory = "", strDiasValidos, strHorarioValido;
    public int intCliente;
    public List<ValidaFactura> lstValidaFactura = new List<ValidaFactura>();
    public bool blnRemisionReq;
    protected void Page_Load(object sender, EventArgs e)
    {
        intCliente = ClasicoConcreto.SEMSession.GetInstance.IntCliente;
        strDirectory = GetParametro(ParametroFactura.RutaFisicaFacturas); //strRutaFisicaFacturas;
        strDirectoryTmp = Server.MapPath("~/Temp/Facturas Masivas/" + intCliente + "/");
       
        if (!Directory.Exists(strDirectoryTmp))
            Directory.CreateDirectory(strDirectoryTmp);

        if (!IsPostBack)
        {

        }
    }

    #region btnGuardarArchivos_Click
    protected void btnGuardarArchivos_Click(object sender, EventArgs e)
    {
        GuardarArchivos();
    }
    #endregion

    #region GuardarArchivos
    protected void GuardarArchivos()
    {
        try
        {
            HttpFileCollection uploadedFiles = Request.Files;
            if (uploadedFiles.Count > 0)
            {
                for (int i = 0; i < uploadedFiles.Count; i++)
                {
                    HttpPostedFile fuImagen = uploadedFiles[i];
                    string strExtensionFile = Path.GetExtension(fuImagen.FileName).ToLower();
                    string strNombreArchivo = fuImagen.FileName;

                    if (strExtensionFile == ".pdf" || strExtensionFile == ".xml")
                    {
                        if (File.Exists(strDirectoryTmp + strNombreArchivo))
                        {
                            File.Delete(strDirectoryTmp + strNombreArchivo);
                        }
                        fuImagen.SaveAs(strDirectoryTmp + strNombreArchivo);
                    }
                }
                //Se validan archivos guardados
                ValidarFacturas();
            }
        }
        catch { }


    }
    #endregion


    #region ValidarFacturas
    protected void ValidarFacturas()
    {
        try
        {
            lstValidaFactura = new List<ValidaFactura>();

            //Directorio fisico donde se guardan los archivos temporales
            string strPhyPath = "~/Temp/Facturas Masivas/" + intCliente + "/";
            strPhyPath = Server.MapPath(strPhyPath);

            //Se recorren todos los archivos xml
            string[] arrFilePaths = Directory.GetFiles(strPhyPath, "*.xml");
            foreach (string fileXML in arrFilePaths)
            {
                ValidaFactura objValidaFactura = new ValidaFactura();
                //Nombres de archivos
                string strFileFactXML = Path.GetFileName(fileXML);
                string strFileFactPDF = Path.GetFileNameWithoutExtension(fileXML) + strExtensionFilePDF;
                string strFileRemisionPDF = Path.GetFileNameWithoutExtension(fileXML) + "_Remision" + strExtensionFilePDF;

                objValidaFactura.strFileXML = strFileFactXML;

                XmlDocument doc = new XmlDocument();
                doc.Load(fileXML);
                XmlNode root = doc.DocumentElement;

                //Se remueven los prefijos namespace xmlns
                doc = UtilFunctions.RemoveAllNamespaces(doc);

                //Se declaran variables
                string strEmisorRFC = "", strReceptorRFC = "", strFolioFiscal = "", strTotal = "", strFolio = "", strSerie = "", strFactura = "", strFolioOC = "", strIP = "", strOCRFactura = "", strFechaFac = "", strSubTotal = "", strDescripcion = "", strIva = "", strRetencion = "";
                string strMetodoPago, strNumCtaPago;
                decimal decTotal = 0, decSubTotal, decIva, decRetencion;
                int intFolioOC = 0;
                DateTime datFechaFac = DateTime.Now;

                //Se extraen valores del xml para validar en el SAT que la factura sea valida
                strSerie = UtilFunctions.GetValueAttribute(doc, "", new string[] { "serie" });
                strFolio = UtilFunctions.GetValueAttribute(doc, "", new string[] { "folio" });
                strFolioFiscal = UtilFunctions.GetValueAttribute(doc, "", new string[] { "uuid" });
                strFolioFiscal = strFolioFiscal.Replace("-", "");
                strTotal = UtilFunctions.GetValueAttribute(doc, "", new string[] { "total", "monto" });
                strIva = XMLIva(doc);
                strSubTotal = UtilFunctions.GetValueAttribute(doc, "", new string[] { "subtotal" });
                strRetencion = XMLRetencion(doc);
                strMetodoPago = UtilFunctions.GetValueAttribute(doc, "Comprobante", new string[] { "metodoDePago" });
                strNumCtaPago = UtilFunctions.GetValueAttribute(doc, "Comprobante", new string[] { "NumCtaPago" });
                strFechaFac = UtilFunctions.GetValueAttribute(doc, "", new string[] { "fecha" });
                strEmisorRFC = UtilFunctions.GetValueAttribute(doc, "Emisor", new string[] { "rfc" });
                strReceptorRFC = UtilFunctions.GetValueAttribute(doc, "Receptor", new string[] { "rfc" });
                strFactura = strSerie + strFolio;
                strIP = Request.ServerVariables["REMOTE_HOST"].ToString();
                //strDescripcion = XMLDescripcion(doc);
                //strFolioOC = GetOC(strDescripcion);
                

                //Se convierten valores
                DateTime.TryParse(strFechaFac, out datFechaFac);
                decimal.TryParse(strTotal, out decTotal);
                int.TryParse(strFolioOC, out intFolioOC);
                decimal.TryParse(strSubTotal, out decSubTotal);
                decimal.TryParse(strIva, out decIva);
                decimal.TryParse(strRetencion, out decRetencion);

                //Guarda factura en el objeto
                objValidaFactura.strFactura = strFactura;
                try
                {
                    ////Validar que exista pdf de la factura 
                    //if (!File.Exists(strPhyPath + strFileFactPDF))
                    //{
                    //    objValidaFactura.esError = true;
                    //    objValidaFactura.lstError.Add(UtilFunctions.GetEnumDescription(MessageError.ErrorFilePDF));
                    //}

                    
                    //Si no existe algun error, se inserta en la base de datos
                    if (objValidaFactura.lstError.Count == 0)
                    {
                        //Consultamos el nombre a guardar por tipo de archivo
                        string strFileFactXMLSave = GetFileNameFactura(FileType.FacturaXML, strFactura);
                        string strFileFactPDFSave = GetFileNameFactura(FileType.FacturaPDF, strFactura);
                        string strFileRemisionPDFSave = "";

                        //Si existe el archivo remision y se requiere, se guarda en BD
                        if (blnRemisionReq && File.Exists(strPhyPath + strFileRemisionPDF)) //Si remision no es obligatoria no se guarda en BD
                            strFileRemisionPDFSave = GetFileNameFactura(FileType.Remision, strFactura);


                        
                        DateTime datFechaSave = DateTime.Now; //Fecha a Guardar
                        datFechaSave = datFechaFac; //Fecha de la factura
                        
                        //if (blnRemisionReq) //Si es proveedor especial se guarda fecha actual si no el siguiente martes
                        //    if (datFechaSave.DayOfWeek != DayOfWeek.Tuesday)//Se inserta el martes proximo
                        //        datFechaSave = DateTime.Now.Next(DayOfWeek.Tuesday);

                        strOCRFactura = UtilFunctions.OCR(strPhyPath + strFileFactPDF);
                        strFolioFiscal = strFolioFiscal.Replace("-", "");


                        // Se guardan los datos de factura en la BD
                        Entity_Facturas ent = new Entity_Facturas();
                        Facturas objFacturas = new Facturas();
                        //obj.intProveedor = intProveedor;
                        //obj.intEmpresa = intEmpresa;
                        //obj.intFolioOC = intFolioOC;
                        ent.strFactura = strFactura;
                        ent.strFolioFiscal = strFolioFiscal;
                        ent.strOCR = strOCRFactura;
                        ent.strPDF = strFileFactPDFSave;
                        ent.strXML = strFileFactXMLSave;
                        ent.strMaquinaAlta = strIP;
                        ent.dblImporte = decTotal;
                        //obj.datFecha = datFechaSave;
                        ent.strRemision = strFileRemisionPDFSave;
                        //obj.strDescripcion = strDescripcion;
                        ent.dblIva = decIva;
                        ent.dblRetencion = decRetencion;
                        ent.dblSubTotal = decSubTotal;
                        ent.strMetodoPago = strMetodoPago;
                        ent.datFechaFactura = datFechaFac;
                        objFacturas.Valida(ent);
                        objFacturas.Save(ent);

                        //Se mueven archivos a la carpeta de producción
                        MoveFile(fileXML, strDirectory + strFileFactXMLSave);
                        MoveFile(strPhyPath + strFileFactPDF, strDirectory + strFileFactPDFSave);

                        if (blnRemisionReq) //Si la remision es obligatoria se guarda en el servidor
                            MoveFile(strPhyPath + strFileRemisionPDF, strDirectory + strFileRemisionPDFSave);

                        ent = null;
                        objFacturas = null;
                    }
                }
                catch (Exception ex)
                {
                    objValidaFactura.lstError.Add(ex.Message); //Se guarda la excepcion
                }

                //Si existen errores se eliminan los archivos
                if (objValidaFactura.lstError.Count() > 0)
                {
                    DeleteFile(strPhyPath + strFileFactXML);
                    DeleteFile(strPhyPath + strFileFactPDF);
                    DeleteFile(strPhyPath + strFileFactXML);
                }
                else
                {
                    objValidaFactura.esError = false;
                    objValidaFactura.lstError.Add("Factura agregada correctamente");
                }
                lstValidaFactura.Add(objValidaFactura);
            }

            FillValidaFactura();
        }
        catch (Exception ex)
        { }
    }
    #endregion


    #region FillValidaFactura
    protected void FillValidaFactura()
    {
        gvValidaFactura.DataSource = lstValidaFactura.Where(p => p.lstError.Count > 0).OrderBy(p => p.strFactura); ;
        gvValidaFactura.DataBind();
    }
    #endregion


    #region DeleteFile
    protected bool DeleteFile(string strFile)
    {
        bool blnValid = true;
        try
        {
            if (File.Exists(strFile))
                File.Delete(strFile);
        }
        catch { blnValid = false; }
        return blnValid;
    }
    #endregion


    #region MoveFiles
    public void MoveFile(string strFile, string strFileNew) //file, filenew
    {
        //Se valida que exista el archivo
        if (File.Exists(strFile))
        {
            //En caso de existir el archivo nuevo en la ruta, se elimina
            if (File.Exists(strFileNew))
                File.Delete(strFileNew);

            File.Move(strFile, strFileNew);
        }
    }
    #endregion


    #region XMLIva
    private string XMLIva(XmlDocument doc)
    {
        string strIva = "";
        decimal dblIva = 0;
        XmlNodeList nodos = doc.SelectNodes("/Comprobante/Impuestos/Traslados/Traslado[@impuesto='IVA']");
        foreach (XmlNode nodo in nodos)
        {
            string strValor = nodo.Attributes["importe"].Value;
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
        foreach (XmlNode nodo in nodos)
        {
            string strValor = nodo.Attributes["importe"].Value;
            decimal dblValor;
            decimal.TryParse(strValor, out dblValor);
            dblRetencion += dblValor;
        }

        strRetencion = dblRetencion.ToString();
        return strRetencion;
    }
    #endregion


    #region GetFileNameFactura
    public string GetFileNameFactura(FileType objFileType, string strFactura)
    {
        string strFileName = "", strExtension = "", strType = "", strFecha = "";
        DateTime dtFecha = DateTime.Now;
        strFecha = dtFecha.ToString("ddMMyyyy");
        switch (objFileType)
        {
            case FileType.FacturaPDF: //No.Prov-Fecha(DDMMYYY)-No.Factura ejemplo: 17-02092015-GAMA001.pdf
                strType = "";
                strExtension = ".pdf";
                break;
            case FileType.FacturaXML: //No.Prov-Fecha(DDMMYYY)-No.Factura ejemplo: 17-02092015-GAMA001.xml
                strType = "";
                strExtension = ".xml";
                break;
            case FileType.Remision: //No.Prov-Fecha(DDMMYYY)-Remision-No.Factura. 17-02092015-Remision-GAMA001.pdf
                strType = "-Remision";
                strExtension = ".pdf";
                break;
            default:
                break;
        }
        strFileName = intCliente.ToString() + "-" + strFecha + "-" + strFactura + strType + strExtension;

        return strFileName;
    }
    #endregion

    #region class

    #region ValidaFactura
    public class ValidaFactura
    {
        string _strFactura;
        string _strFileXML;
        List<string> _lstError;
        bool _esError;
        public ValidaFactura()
        {
            _strFactura = "";
            _strFileXML = "";
            _lstError = new List<string>();
            _esError = false;
        }

        public string strFactura
        {
            get { return _strFactura; }
            set { _strFactura = value; }
        }

        public string strFileXML
        {
            get { return _strFileXML; }
            set { _strFileXML = value; }
        }

        public List<string> lstError
        {
            get { return _lstError; }
            set { _lstError = value; }
        }

        public bool esError
        {
            get { return _esError; }
            set { _esError = value; }
        }

    }
    #endregion

    #endregion

    #region enum
    #region MessageError
    private enum MessageError
    {
        [Description("La factura no es válida.")]
        ErrorFactura,

        [Description("El archivo PDF no existe.")]
        ErrorFilePDF,

        [Description("El archivo de remisión no existe.")]
        ErrorFileRemision,

        [Description("La razón social no existe en la base de datos.")]
        ErrorEmpresa,

        [Description("Favor de revisar los archivos de factura y remisión.")]
        ErrorFacturaRemision


    }
    #endregion

    #region FileType
    public enum FileType
    {
        FacturaPDF,
        FacturaXML,
        Remision
    }
    #endregion
    #endregion
}