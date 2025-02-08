using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ClasicoConcreto.Entity;
using ClasicoConcreto.Bussines;
using Newtonsoft.Json;
using System.Web.Hosting;
using System.Web.Services;
using System.Net.Mail;
using Quiksoft.FreeSMTP;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelDataReader;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.ServiceModel.Channels;
using System.Runtime.Remoting.Messaging;

public partial class Pages_Admin_PlantaCargaRemisiones : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFechaInicio.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
            txtFechaFin.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
     protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (fileExcel.HasFile)
        {
            string end = (Request.ApplicationPath.EndsWith("/")) ? "" : "/";
            string path = Request.ApplicationPath + end;
            string appPath = Request.PhysicalApplicationPath;
            string fileName = fileExcel.FileName;

            int lastPoint = fileExcel.FileName.LastIndexOf(".");
            string extension = fileExcel.FileName.Substring(lastPoint + 1);

            fileExcel.PostedFile.SaveAs(appPath + "Temp/" + fileName);

            ExportRemisiones(appPath + "Temp/", fileName);

            File.Delete(fileName);
        }
    }

    #region ExportRemisiones
     private void ExportRemisiones(string path, string fileName)
    {
        try
        {
            DataTable dt = new DataTable();
            using (var stream = File.Open(path + fileName, FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader excelReader; //= ExcelReaderFactory.CreateBinaryReader(stream);
                try
                {
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                catch
                {
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                DataSet ds = excelReader.AsDataSet();
                if (ds.Tables.Count > 0)
                    dt = ds.Tables[0];
                excelReader.Close();
            }
            int i = 0;
            foreach (DataRow Row in dt.Rows)
            {
                Entity_PlantaOrdenesRemisiones ent = new Entity_PlantaOrdenesRemisiones();
                if (i == 0)
                {
                    i = i + 1;
                    continue;
                }

                string strPlanta = string.IsNullOrEmpty(Row[0].ToString()) == true ? "" : (string)Row[0];
                string strFecha;

                if (strPlanta.Contains("PC-"))
                {
                    ent.strRemision = string.IsNullOrEmpty(Row[1].ToString()) == true ? "" : (string)Row[1];
                    ent.decCantidad = Convert.ToDecimal(string.IsNullOrEmpty(Row[3].ToString()) == true ? 0 : (double)Row[3]);
                    ent.intFolio = Convert.ToInt32(string.IsNullOrEmpty(Row[4].ToString()) == true ? 0 : (double)Row[4]);
                    ent.strStatus = string.IsNullOrEmpty(Row[5].ToString()) == true ? "" : (string)Row[5];

                    try
                    {
                        DateTime datFechaExcel = (DateTime)Row[2];
                        strFecha = datFechaExcel.ToString("dd/MM/yyyy");
                    }
                    catch
                    {
                        strFecha = (string)Row[2];
                    }

                    int intPlanta;

                    switch (strPlanta)
                    {
                        case "PC-01":
                            intPlanta = 1;
                            break;
                        case "PC-02":
                            intPlanta = 2;
                            break;
                        case "PC-03":
                            intPlanta = 3;
                            break;
                        default:
                            intPlanta = 1;
                            break;
                    }

                    string[] arrFecha = strFecha.Split(new char[] { '/' });
                    if (arrFecha.Length == 3)
                    {
                        int intMes, intDia;
                        int.TryParse(arrFecha[1], out intMes);
                        int.TryParse(arrFecha[0], out intDia);

                        if (intMes > 12)
                        {
                            int.TryParse(arrFecha[0], out intMes);
                            int.TryParse(arrFecha[1], out intDia);
                        }

                        ent.datFecha = new DateTime(int.Parse(arrFecha[2]), intMes, intDia);
                    }

                    ent.intPlanta = intPlanta;
                    ent.strMaquinaAlta = ClasicoConcreto.SEMSession.GetInstance.StrMaquina;
                    ent.strUsuarioAlta = ClasicoConcreto.SEMSession.GetInstance.IntCliente.ToString();

                    Ordenes objOrden = new Ordenes();
                    objOrden.SavePlantaOrdenRemisiones(ent);
                }
            }
            i = i + 1;
            
            fileExcel.Dispose();
            fileExcel.UpdateAfterCallBack = true;
            string script = "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'La información se procesó correctamente.'});";
            Anthem.Manager.RegisterStartupScript(Page.GetType(), "msgSave", script, true);

        }
        catch (Exception e)
        {
            string script = "Swal.fire({icon: 'warning', title: 'Advertencia', text: 'Favor de revisar el archivo, no se permiten valores vacíos.'});";
            Anthem.Manager.RegisterStartupScript(Page.GetType(), "msgErr", script, true);
            Console.WriteLine(e.Message);
        }
    }
    #endregion
    [WebMethod]
    public static string[] GetList(string strFechaInicio, string strFechaFin)
    {
        string[] rtnData = new string[2];
        try
        {
            DateTime datFechaInicio, datFechaFin;
            DateTime.TryParse(strFechaInicio, out datFechaInicio);
            DateTime.TryParse(strFechaFin, out datFechaFin);
            Ordenes objOrdenes = new Ordenes();
            DataTable lst = objOrdenes.GetListPlantaOrdenRemisiones(datFechaInicio, datFechaFin);
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
    [WebMethod]
    public static string[] ReporteMensual(string strFecha)
    {
        string[] rtnData = new string[2];
        try
        {
            DateTime datFecha;
            int intMes, intAño;
            DateTime.TryParse(strFecha, out datFecha);
            intMes = datFecha.Month;
            intAño = datFecha.Year;
            string strFileName = "ReporteVenta_" + intMes.ToString() + "_"+ intAño.ToString()+ ".pdf";
            string PathReport = AppDomain.CurrentDomain.BaseDirectory + "Pages\\Reportes\\ConcretoVentaMensual.rpt";
            string PathEnviar = AppDomain.CurrentDomain.BaseDirectory + "Temp\\" + strFileName;
            CrystalDecisions.CrystalReports.Engine.ReportDocument RptImpresion = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            RptImpresion.Load(PathReport);
            ExportaReportes(RptImpresion, PathEnviar, intMes, intAño);

            rtnData[0] = "ok";
            rtnData[1] = strFileName;
        }
        catch
        {
            rtnData[0] = "no";
            rtnData[1] = "[]";
        }
        return rtnData;

    }

    #region ExportaReportes
    public static void ExportaReportes(CrystalDecisions.CrystalReports.Engine.ReportDocument Rpt, string FName, int intMes, int intAño)
    {
        try
        {
            ReportDocument crReportDocument;
            ExportOptions crExportOptions;
            DiskFileDestinationOptions crDiskFileDestinationOptions;

            crReportDocument = Rpt;

            crReportDocument.SetParameterValue("@intMes", intMes);
            crReportDocument.SetParameterValue("@intAnio", intAño);
            SetReportConnectionInfo(crReportDocument);

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
}