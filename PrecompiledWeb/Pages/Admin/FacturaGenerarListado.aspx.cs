using ClasicoConcreto.Bussines;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.Services;
using ClasicoConcreto.Entity;
using System.IO;

public partial class Pages_Admin_FacturaGenerarListado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFechaInicio.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
            txtFechaFin.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }

    [WebMethod]
    public static string[] GetList(string strFechaInicio, string strFechaFin)
    {
        string[] rtnData = new string[2];
        try
        {
            DateTime datFechaInicio, datFechaFin;
            DateTime.TryParse(strFechaInicio, out datFechaInicio);
            DateTime.TryParse(strFechaFin, out datFechaFin);
            FacturasGenerar objFacturas = new FacturasGenerar();
            DataTable lst = objFacturas.GetList(0, datFechaInicio, datFechaFin);
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
    public static string[] Delete(int intFactura)
    {
        string[] rtnData = new string[2];
        try
        {
            FacturasGenerar objFacturas = new FacturasGenerar();
            objFacturas.Delete(intFactura);
            rtnData[0] = "ok";
            rtnData[1] = "";
        }
        catch
        {
            rtnData[0] = "no";
            rtnData[1] = "Error al tratar el registro";
        }
        return rtnData;
    }


}