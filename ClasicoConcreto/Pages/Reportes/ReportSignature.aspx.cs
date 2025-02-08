using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web.Http.Cors;
using System.Web.Http;
using ClasicoConcreto.Entity;
using ClasicoConcreto.Bussines;

public partial class Pages_Reportes_ReportSignature : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
         
        }
    }

    [WebMethod]
    public static string[] ShowReport()
    {
        string[] rtnData = new string[2];
        try
        {
            //Se crea archivo temporal
            string strFileName = "Queja_" + ClasicoConcreto.SEMSession.GetInstance.IntCliente + ".pdf";
            string PathReport = AppDomain.CurrentDomain.BaseDirectory + "Pages\\Reportes\\Queja.rpt";
            string PathEnviar = AppDomain.CurrentDomain.BaseDirectory + "Temp\\Reportes Firmas\\" + strFileName;
            CrystalDecisions.CrystalReports.Engine.ReportDocument RptImpresion = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            RptImpresion.Load(PathReport);
            ExportaReportes(RptImpresion, PathEnviar);

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
    public static void ExportaReportes(CrystalDecisions.CrystalReports.Engine.ReportDocument Rpt, string FName)
    {
        try
        {
            ReportDocument crReportDocument;
            ExportOptions crExportOptions;
            DiskFileDestinationOptions crDiskFileDestinationOptions;

            crReportDocument = Rpt;

            //  string nombreFirma = ClasicoConcreto.SEMSession.GetInstance.StrFirma;
           
            crReportDocument.SetParameterValue("@intQueja", 18985);
            crReportDocument.SetParameterValue("location", "\\\\192.168.100.10\\wwwroot\\Firmas\\" + NameSignature.strNameSignature + ".png");
            SetReportConnectionInfo(crReportDocument);
            //crReportDocument.SetDatabaseLogon("sa", "M1rf3l", "192.168.80.5", "dbSAC", true);

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