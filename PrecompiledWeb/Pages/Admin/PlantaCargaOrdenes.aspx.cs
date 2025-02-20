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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


public partial class Pages_Admin_PlantaCargaOrdenes : System.Web.UI.Page
{


    string fechaDia = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFechaInicio.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
            txtFechaFin.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFechaDiario.Text = DateTime.Now.ToString("dd/MM/yyyy");
            hddFechDiaria.Value = txtFechaDiario.Text;


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

            //fileName = appPath + "Temp/" + fileName;
            fileExcel.PostedFile.SaveAs(appPath + "Temp/" + fileName);
                    

            ExportOrdenesPlanta(appPath + "Temp/", fileName);

            File.Delete(fileName);

            Anthem.Manager.AddScriptForClientSideEval("ocultarModalEspera();");

        }
    }


    #region ExportOrdenesPlanta
    private void ExportOrdenesPlanta(string path, string fileName)
    {
        var strRemision = "";
        int i = 0;
        try
        {
            DataTable dt = new DataTable();
            using (var stream = File.Open(path+ fileName, FileMode.Open, FileAccess.Read))
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
                if(ds.Tables.Count > 0)
                dt = ds.Tables[0];
                excelReader.Close();
            }

            foreach (DataRow Row in dt.Rows)
            {
                if (i == 0)
                {
                    i = i + 1;
                    continue;

                }

                if (Row[0].ToString() != "")
                {

                    string strPlanta, strEstatus, strCliente;

                    strPlanta = string.IsNullOrEmpty(Row[1].ToString()) == true ? "" : (string)Row[1];
                    strEstatus = string.IsNullOrEmpty(Row[3].ToString()) == true ? "" : (string)Row[3];
                    strCliente = string.IsNullOrEmpty(Row[4].ToString()) == true ? "" : (string)Row[4];

                    //if (strEstatus == "Abierto" && !strCliente.Contains("CLIENTE") && !strCliente.Contains("VT-GAMA"))
                    // if (strEstatus == "Abierto" && strCliente != "")
                    //{

                    string strFecha = "";
                    var strPlantaOrden = Row[5];
                    var strFolioOC = Row[6];
                    strRemision = Row[8].ToString();
                    string strProducto = Row[16].ToString();

                    try
                    {
                        DateTime datFechaExcel = (DateTime)Row[10];
                        strFecha = datFechaExcel.ToString("dd/MM/yyyy");
                    }
                    catch
                    {
                        strFecha = (string)Row[10];
                    }



                    var strCarga = Row[14];

                    int intPlanta, intPlantaOrden, intFolioOC;
                    DateTime datFecha = new DateTime();
                    decimal decCarga;


                    Entity_PlantaOrdenes ent = new Entity_PlantaOrdenes();
                    switch (strPlanta.Replace(" ", ""))
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


                    int.TryParse(strPlantaOrden.ToString(), out intPlantaOrden);
                    int.TryParse(strFolioOC.ToString(), out intFolioOC);
                    decimal.TryParse(strCarga.ToString(), out decCarga);

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


                        datFecha = new DateTime(int.Parse(arrFecha[2]), intMes, intDia);
                    }


                    //    System.Diagnostics.Debug.WriteLine("i: " + i.ToString());


                    ent.strEstatus = strEstatus;
                    ent.intPlanta = intPlanta;
                    ent.intPlantaOrden = intPlantaOrden;
                    ent.intFolioOC = intFolioOC;
                    ent.strRemision = strRemision.ToString();
                    ent.datFechaCarga = hddFechDiaria.Value;
                    ent.datFecha = datFecha;
                    ent.decCarga = decCarga;
                    ent.strMaquinaAlta = ClasicoConcreto.SEMSession.GetInstance.StrMaquina;
                    ent.strUsuarioAlta = ClasicoConcreto.SEMSession.GetInstance.IntCliente.ToString();
                    ent.strCliente = strCliente;
                    ent.strProducto = strProducto;
                    Ordenes objOrden = new Ordenes();
                    objOrden.SavePlantaOrden(ent);


                    System.Diagnostics.Debug.WriteLine(strRemision.ToString());

                    // }// fin if



                    i = i + 1;
                }
            }
            
            fileExcel.Dispose();
            fileExcel.UpdateAfterCallBack = true;
            string script = "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'La información se procesó correctamente.'}).then((result) => { Exportar(); });";
            Anthem.Manager.RegisterStartupScript(Page.GetType(), "msgSave", script, true);


        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            //ClientScript.RegisterStartupScript(Page.GetType(), "msgErr", "alert('Favor de revisar el archivo, no se permiten valores vacios.');", true);
            string script = string.Format("Swal.fire({{icon: 'error', title: 'Error', text: '{0}. {1}'}});",e.Message.Replace("'", "\\'"), i);
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
            DataTable lst = objOrdenes.GetListPlantaOrden(datFechaInicio, datFechaFin);
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
    public static string[] ShowReport(string datFechaDia)
    {
        string[] rtnData = new string[2];
        try
        {
            //Se crea archivo temporal
          
            string rDate = datFechaDia.Replace('/', '_');
            string strFileName = "ReporteDiarioCommandBatch_" + rDate + ClasicoConcreto.SEMSession.GetInstance.IntCliente + ".pdf";
            string PathReport = AppDomain.CurrentDomain.BaseDirectory + "Pages\\Reportes\\PlantaOrdenesRemisionPorDia.rpt";
            string PathEnviar = AppDomain.CurrentDomain.BaseDirectory + "Temp\\ReporteRemisionDiarioCmdBatch\\" + strFileName;
            CrystalDecisions.CrystalReports.Engine.ReportDocument RptImpresion = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            RptImpresion.Load(PathReport);
            ExportaReportes(RptImpresion, PathEnviar, datFechaDia);

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
    public static void ExportaReportes(CrystalDecisions.CrystalReports.Engine.ReportDocument Rpt, string FName, string datFechaDia)
    {
        try
        {
            ReportDocument crReportDocument;
            ExportOptions crExportOptions;
            DiskFileDestinationOptions crDiskFileDestinationOptions;

            crReportDocument = Rpt;

            crReportDocument.SetParameterValue("@datFecha", datFechaDia);
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


}