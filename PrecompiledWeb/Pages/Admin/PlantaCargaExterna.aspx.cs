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


public partial class Pages_Admin_PlantaCargaExterna : BasePage
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

            //fileName = appPath + "Temp/" + fileName;
            fileExcel.PostedFile.SaveAs(appPath + "Temp/" + fileName);

            ExportOrdenesPlanta(appPath + "Temp/", fileName);

            File.Delete(fileName);
        }
    }

    #region ExportOrdenesPlanta
    private void ExportOrdenesPlanta(string path, string fileName)
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
                if (i == 0)
                {
                    i = i + 1;
                    continue;
                }
                if (i == 115)
                {
                    
                }
                string strPlanta, strFecha, strRemision, strCliente, strResistencia, strCantidad, strBombeable, strPrecioVenta, strIva, str8;


                strPlanta = string.IsNullOrEmpty(Row[0].ToString()) == true ? "" : (string)Row[0];

                if (strPlanta.Contains("PC-"))
                {
                    //strFecha = string.IsNullOrEmpty(Row[1].ToString()) == true ? "" : (string)Row[1];
                    strRemision = string.IsNullOrEmpty(Row[2].ToString()) == true ? "" : ((double)Row[2]).ToString();
                    strCliente = string.IsNullOrEmpty(Row[3].ToString()) == true ? "" : (string)Row[3];
                    strResistencia = string.IsNullOrEmpty(Row[4].ToString()) == true ? "" : (string)Row[4];
                    strCantidad = string.IsNullOrEmpty(Row[5].ToString()) == true ? "" : ((double)Row[5]).ToString();
                    strBombeable = string.IsNullOrEmpty(Row[6].ToString()) == true ? "" : ((double)Row[6]).ToString();
                    strPrecioVenta = string.IsNullOrEmpty(Row[7].ToString()) == true ? "" : ((double)Row[7]).ToString();
                    strIva = string.IsNullOrEmpty(Row[9].ToString()) == true ? "" : ((double)Row[9]).ToString();
                    str8 = string.IsNullOrEmpty(Row[10].ToString()) == true ? "" : ((double)Row[10]).ToString();

                    //strCantidad = strCantidad.Replace(",", "");
                    //strBombeable = strBombeable.Replace(",", "");
                    //strPrecioVenta = strPrecioVenta.Replace(",", "");
                    //strIva = strIva.Replace(",", "");
                    //str8 = str8.Replace(",", "");


                    try
                    {
                        DateTime datFechaExcel = (DateTime)Row[1];
                        strFecha = datFechaExcel.ToString("dd/MM/yyyy");
                    }
                    catch
                    {
                        strFecha = (string)Row[1];
                    }

                    int intPlanta;
                    DateTime datFecha = new DateTime();
                    decimal decCantidad, decBombeable, decPrecioVenta, decIva, dec8, decPorcIva = 0;

                    Entity_PlantaOrdenesExterna ent = new Entity_PlantaOrdenesExterna();
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

                    decimal.TryParse(strCantidad, out decCantidad);
                    decimal.TryParse(strBombeable, out decBombeable);
                    decimal.TryParse(strPrecioVenta, out decPrecioVenta);
                    decimal.TryParse(strIva, out decIva);
                    decimal.TryParse(str8, out dec8);

                    if (decIva > 0)
                        decPorcIva = 16;
                    else if(dec8 > 0)
                        decPorcIva = 8;


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

                    System.Diagnostics.Debug.WriteLine("i: " + i.ToString());

                    ent.intPlanta = intPlanta;
                    ent.datFecha = datFecha;
                    ent.strRemision = strRemision;
                    ent.strCliente = strCliente;
                    ent.strResistencia = strResistencia;
                    ent.decCantidad = decCantidad;
                    ent.decBombeable = decBombeable;
                    ent.decPrecioVenta = decPrecioVenta;
                    ent.decPorcIva = decPorcIva;

                    ent.strMaquinaAlta = ClasicoConcreto.SEMSession.GetInstance.StrMaquina;
                    ent.strUsuarioAlta = ClasicoConcreto.SEMSession.GetInstance.IntCliente.ToString();
                    ent.strCliente = strCliente;
                    Ordenes objOrden = new Ordenes();
                    objOrden.SavePlantaOrdenExterna(ent);
                }
                i = i + 1;
            }
            
            


            fileExcel.Dispose();
            fileExcel.UpdateAfterCallBack = true;
            Anthem.Manager.RegisterStartupScript(Page.GetType(), "msgSave", "alert('La informacion se proceso correctamente.');", true);

        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            //ClientScript.RegisterStartupScript(Page.GetType(), "msgErr", "alert('Favor de revisar el archivo, no se permiten valores vacios.');", true);
            Anthem.Manager.RegisterStartupScript(Page.GetType(), "msgErr", "alert('Favor de revisar el archivo, no se permiten valores vacios.');", true);
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
            DataTable lst = objOrdenes.GetListPlantaOrdenExterna(datFechaInicio, datFechaFin);
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

}