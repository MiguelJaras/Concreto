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
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Pages_Opciones_Cotizacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }


    [WebMethod]
    public static string[] GetListProductos()
    {
        string[] rtnData = new string[2];
        try
        {
            int intLista = ClasicoConcreto.SEMSession.GetInstance.IntLista;
            Producto objProducto = new Producto();
            DataTable dtProductos = objProducto.GetListCotizacion(intLista);
            rtnData[0] = "ok";
            rtnData[1] = JsonConvert.SerializeObject(dtProductos);
        }
        catch
        {
            rtnData[0] = "no";
            rtnData[1] = "[]";
        }
        return rtnData;
    }

    [WebMethod]
    public static string[] GetCotizacion(int id)
    {
        string[] rtnData = new string[2];
        try
        {
            int intCliente = ClasicoConcreto.SEMSession.GetInstance.IntCliente;
            Cotizacion obj = new Cotizacion();
            DataTable dtCot = obj.Get(intCliente, id);
            rtnData[0] = "ok";
            rtnData[1] = JsonConvert.SerializeObject(dtCot);
        }
        catch
        {
            rtnData[0] = "no";
            rtnData[1] = "[]";
        }
        return rtnData;
    }

    [WebMethod]
    public static string[] GetCotizacionDet(int id)
    {
        string[] rtnData = new string[2];
        try
        {
            Cotizacion obj = new Cotizacion();
            DataTable dtCotDet = obj.GetDet(id);
            rtnData[0] = "ok";
            rtnData[1] = JsonConvert.SerializeObject(dtCotDet);
        }
        catch
        {
            rtnData[0] = "no";
            rtnData[1] = "[]";
        }
        return rtnData;
    }

    [WebMethod]
    public static string[] Save(Entity_Cotizacion ent)
    {
        string[] rtnData = new string[2];
        try
        {
            int intCliente = ClasicoConcreto.SEMSession.GetInstance.IntCliente;
            ent.intClienteAlta = intCliente;
            ent.strMaquinAlta = ClasicoConcreto.SEMSession.GetInstance.StrMaquina;
            Cotizacion objCotizacion = new Cotizacion();
            int intCotizacion = objCotizacion.Save(ent);
            rtnData[0] = "ok";
            rtnData[1] = intCotizacion.ToString();
        }
        catch
        {
            rtnData[0] = "no";
            rtnData[1] = "[]";
        }
        return rtnData;
    }


    [WebMethod]
    public static string[] SaveProducto(Entity_CotizacionDet entDet)
    {
        string[] rtnData = new string[2];
        try
        {
            Cotizacion objCotizacion = new Cotizacion();
            int intPartida = objCotizacion.SaveDet(entDet);
            rtnData[0] = "ok";
            rtnData[1] = intPartida.ToString();
        }
        catch
        {
            rtnData[0] = "no";
            rtnData[1] = "[]";
        }
        return rtnData;
    }


    [WebMethod]
    public static string[] EliminarProducto(int id, int partida)
    {
        string[] rtnData = new string[2];
        try
        {
            Cotizacion obj = new Cotizacion();
            int intCliente = ClasicoConcreto.SEMSession.GetInstance.IntCliente;
            obj.DeleteProducto(id, partida);
            //objPartida.SavePedidoPrecio(intPedido);
            rtnData[0] = "ok";
            rtnData[1] = "Producto eliminado correctamente.";
        }
        catch (Exception ex)
        {
            rtnData[0] = "no";
            rtnData[1] = ex.Message;
        }
        return rtnData;
    }

    [WebMethod]
    public static string[] Preview(int intCotizacion)
    {
        string[] rtnData = new string[2];
        try
        {
            //Se crea archivo temporal
            string strFileName = "Cotizacion_" + ClasicoConcreto.SEMSession.GetInstance.IntCliente.ToString() + ".pdf";
            string PathReport = AppDomain.CurrentDomain.BaseDirectory + "Pages\\Reportes\\Cotizacion.rpt";
            string PathEnviar = AppDomain.CurrentDomain.BaseDirectory + "Temp\\" + strFileName;
            CrystalDecisions.CrystalReports.Engine.ReportDocument RptImpresion = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            RptImpresion.Load(PathReport);
            ExportaReportes(RptImpresion, PathEnviar, intCotizacion);

            rtnData[0] = "ok";
            rtnData[1] = strFileName;
        }
        catch (Exception ex)
        {
            rtnData[0] = "no";
            rtnData[1] = ex.Message;
        }
        return rtnData;
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
    public static void ExportaReportes(CrystalDecisions.CrystalReports.Engine.ReportDocument Rpt, string FName, int intCotizacion)
    {
        try
        {
            ReportDocument crReportDocument;
            ExportOptions crExportOptions;
            DiskFileDestinationOptions crDiskFileDestinationOptions;

            crReportDocument = Rpt;

            crReportDocument.SetParameterValue("@intCotizacion", intCotizacion);
            SetReportConnectionInfo(crReportDocument);
            //crReportDocument.SetDatabaseLogon("sa", "M1rf3l", "192.168.80.5", "dbConcreto", true);

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

}