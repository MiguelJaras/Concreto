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

public partial class Pages_Admin_FacturaGenerar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string strRutaExe = @"C:\Program Files\Compacw\FacturacionConcreto\FacturacionConcreto.exe";
        //string strServeName = @"\\EFISCALES";

        //ProcessStartInfo info = new ProcessStartInfo("C:\\PsTools");
        //info.FileName = @"C:\Proyectos\Marfil\Codigo\ClasicoConcreto\ClasicoConcreto\PsExec.exe";
        //info.Arguments = @"\\" + strServeName + @" -i C:\Program Files\Compacw\FacturacionConcreto\FacturacionConcreto.exe";
        //info.RedirectStandardOutput = true;
        //info.UseShellExecute = false;
        //Process p = Process.Start(info);
        //p.WaitForExit();

        if (!IsPostBack)
        {
            GetClientes();
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


    protected void GetClientes()
    {
        Facturas objFacturas = new Facturas();
        DataTable dt = objFacturas.GetListClientes();
        ddlCliente.DataSource = dt;
        ddlCliente.DataValueField = "strCliente";
        ddlCliente.DataTextField = "strCliente";
        ddlCliente.DataBind();
        ddlCliente.Items.Insert(0, new ListItem("--Seleccione--", ""));
        ddlCliente.SelectedValue = "0";

    }

    [WebMethod]
    public static string[] GetList(string strCliente)
    {
        string[] rtnData = new string[2];
        try
        {
            Pedido objPedido = new Pedido();
            DataTable dt = objPedido.GetListSinFactura(strCliente);
            rtnData[0] = "ok";
            rtnData[1] = JsonConvert.SerializeObject(dt);
        }
        catch
        {
            rtnData[0] = "no";
            rtnData[1] = "[]";
        }
        return rtnData;
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
        catch (Exception ex)
        {
            rtnData[0] = "no";
            rtnData[1] = ex.Message;
        }
        return rtnData;
    }

    [WebMethod]
    //public static string[] GenerarFactura(string strPedidos, string strCliente, string strConcepto)
    public static string[] GenerarFactura(Entity_FacturasGenerar ent)
    {
        string[] rtnData = new string[2];
        try
        {
            FacturasGenerar obj = new FacturasGenerar();
            ent.intEstatus = 1;
            ent.strUsuarioAlta = ClasicoConcreto.SEMSession.GetInstance.IntCliente.ToString();
            ent.strMaquinaAlta = ClasicoConcreto.SEMSession.GetInstance.StrMaquina;

            int intFactura = obj.Save(ent);

            rtnData[0] = "ok";
            rtnData[1] = intFactura.ToString();
        }
        catch(Exception ex)
        {
            rtnData[0] = "no";
            rtnData[1] = ex.Message;
        }
        return rtnData;
    }
}