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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net.Mail;

public partial class Pages_Opciones_PedidoListado : BasePage
{
    
    public string[] arr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFechaInicio.Text = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
            txtFechaFin.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }

    [WebMethod]
    public static string[] GetList(string strFechaInicio, string strFechaFin)
    {
        string[] rtnData = new string[3];
        try
        {
            DateTime datFechaInicio, datFechaFin;

            DateTime.TryParse(strFechaInicio, out datFechaInicio);
            DateTime.TryParse(strFechaFin, out datFechaFin);

            int intCliente = ClasicoConcreto.SEMSession.GetInstance.IntCliente;
            Pedido objPedido = new Pedido();
            DataTable lst = objPedido.GetList(intCliente, datFechaInicio, datFechaFin);
            rtnData[0] = "ok";
            rtnData[1] = JsonConvert.SerializeObject(lst);
        }
        catch (Exception ex)
        {
            rtnData[0] = "no";
            rtnData[1] = "[]";
        }
        return rtnData;
    }


    [WebMethod]
    public static string[] GetDetallePedido(int intPedido)
    {
        string[] rtnData = new string[3];
        try
        {
            int intCliente = ClasicoConcreto.SEMSession.GetInstance.IntCliente;
            Ordenes objOrdenes = new Ordenes();
            DataTable dt = objOrdenes.GetOrdenesByPedido(intCliente, intPedido);
            rtnData[0] = "ok";
            rtnData[1] = JsonConvert.SerializeObject(dt);
        }
        catch (Exception ex)
        {
            rtnData[0] = "no";
            rtnData[1] = "[]";
        }
        return rtnData;
    }

    protected void btnEmail_Click(object sender, EventArgs e)
    {
        EnviarEmail();
    }
    protected void EnviarEmail()
    {
        int intPedido = 0;
        int.TryParse(hddIntPedido.Value, out intPedido);
        if (intPedido > 0)
        {
            //string end = (Request.ApplicationPath.EndsWith("/")) ? "" : "/";
            //string path = Request.ApplicationPath + end;
            string PathReport = AppDomain.CurrentDomain.BaseDirectory + "Pages\\Reportes\\Concreto.rpt";
            string PathEnviar = AppDomain.CurrentDomain.BaseDirectory + "Temp\\Pedido" + ClasicoConcreto.SEMSession.GetInstance.IntCliente.ToString() + ".pdf";
            try
            {
                CrystalDecisions.CrystalReports.Engine.ReportDocument RptImpresion = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                RptImpresion.Load(PathReport);
                ExportaReportes(RptImpresion, PathEnviar, intPedido);

                
                MailAddress strFrom = new MailAddress("SEM@marfil.com", "Marfil Sistemas");
                string strContenido = "<html><body><table width='95%'><tr><td><font size='4' color='black' style='font-weight:bold'></font></td></tr>";
                strContenido = strContenido + "<tr><td> <font size='2' color='black' style='font-family:Arial'> </font><font size='2' color='black' style='font-family:Arial'> </font><br/><br/></td></tr>";
                strContenido = strContenido + "<tr><td> <font size='2' color='black' style='font-family:Arial'></font><font size='2' color='black' style='font-family:Arial'> </font><br/><br/></td></tr>";
                strContenido = strContenido + "<tr><td> <font size='2' color='black' style='font-family:Arial'>Atte.</font><font size='2' color='black' style='font-family:Arial'> </font></td></tr>";
                strContenido = strContenido + "<tr><td> <font size='2' color='black' style='font-family:Arial'>Clásico Concreo</font><font size='2' color='black' style='font-family:Arial'> </font></td></tr>";
                strContenido = strContenido + "<tr><td> <font size='2' color='black' style='font-family:Arial'></font><font size='2' color='black' style='font-family:Arial'> </font></td></tr>";
                strContenido = strContenido + "</table></body></html>";

                string strEmailCliente = "juliosoto@marfil.com";//ClasicoConcreto.SEMSession.GetInstance.StrEmail;
                string strNombre = ClasicoConcreto.SEMSession.GetInstance.StrNombre;
                List<MailAddress> lstTo = new List<MailAddress>();
                lstTo.Add(new MailAddress(strEmailCliente, strNombre));

                List<string> lstAttachment = new List<string>();
                lstAttachment.Add(PathEnviar);

                Entity_Email entEmail = new Entity_Email(strFrom, strContenido, lstTo, "Pedido " + intPedido.ToString(), lstAttachment);
                Email objEmail = new Email();
                objEmail.Send(entEmail);

                //ClientScript.RegisterStartupScript(Page.GetType(), "msgEr", "alert('Se envio correctamente el correo a la direccion: " + txtEmail.Text + "'); window.close();", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "msgEr", "alert('" + ex.Message + "');", true);
            }

            System.IO.File.Delete(PathEnviar);


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
    public void ExportaReportes(CrystalDecisions.CrystalReports.Engine.ReportDocument Rpt, string FName, int intPedido)
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



}