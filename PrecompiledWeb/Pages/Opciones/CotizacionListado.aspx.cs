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

public partial class Pages_Opciones_CotizacionListado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFechaInicio.Text = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
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
            Cotizacion objCotizacion = new Cotizacion();
            DataTable lst = objCotizacion.GetList(intCliente, datFechaInicio, datFechaFin);
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

}