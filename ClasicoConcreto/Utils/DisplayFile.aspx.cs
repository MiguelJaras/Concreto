using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClasicoConcreto.Bussines;

public partial class Utils_DisplayFile : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strFileName = "", strType = "";
            if (Request.QueryString["fileName"] != null)
                strFileName = Request.QueryString["fileName"].ToString();

            if (Request.QueryString["type"] != null)
                strType = Request.QueryString["type"].ToString();

            if (strFileName != "" && strType != "")
                LoadFile(strFileName, strType);


        }
    }

    protected void LoadFile(string strFileName, string strType)
    {

        try
        {
            string strPath = "application/pdf", strApp = "", strFilePath;
            string strDirectory = GetParametro(ParametroFactura.RutaFisicaFacturas); //strRutaFisicaFacturas;
            Facturas objFac = new Facturas();
            switch (strType.ToLower())
            {
                case "mergefile":
                    strPath = "~/Temp/Combinar Archivos/";
                    strFilePath = Server.MapPath(strPath + strFileName);
                    strApp = "application/pdf";
                    break;
                case "factura":
                    strPath = strDirectory;
                    strFilePath = strPath + strFileName;
                    strApp = "application/pdf";
                    break;
                case "ncpdf":
                    strPath = GetParametro(ParametroFactura.RutaFisicaNotasCredito);
                    strFilePath = strPath + strFileName;
                    strApp = "application/pdf";
                    break;
                case "xml":
                    strPath = strDirectory;
                    strFilePath = strPath + strFileName;
                    strApp = "application/xml";
                    break;
                case "ncxml":
                    strPath = GetParametro(ParametroFactura.RutaFisicaNotasCredito);
                    strFilePath = strPath + strFileName;
                    strApp = "application/xml";
                    break;
                default:
                    return;
            }

            byte[] bytes = System.IO.File.ReadAllBytes(strFilePath);
            Response.ClearHeaders();
            Response.Clear();
            Response.AddHeader("Content-Type", strApp);
            Response.AddHeader("Content-Length", bytes.Length.ToString());
            Response.AddHeader("Content-Disposition", "attchment; filename=" + strFileName);
            Response.BinaryWrite(bytes);
            //Response.WriteFile(strFilePath);
            Response.Flush();
            Response.End();

        }
        catch(Exception ex) { }

    }


}