using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Newtonsoft.Json;
using System.Web.Services;
using ClasicoConcreto.Entity;
using ClasicoConcreto.Bussines;

public partial class Pages_Catalogos_CapturaConsumo : System.Web.UI.Page
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
        string[] rtnData = new string[3];
        try
        {
            DateTime datFechaInicio, datFechaFin;

   
            DateTime.TryParse(strFechaInicio, out datFechaInicio);
            DateTime.TryParse(strFechaFin, out datFechaFin);
            
        
            Consumo obj = new Consumo();
            DataTable lst = obj.GetList(datFechaInicio, datFechaFin);

            rtnData[1] = JsonConvert.SerializeObject(lst);

            rtnData[0] = "ok";
           
        }
        catch (Exception ex)
        {
            rtnData[0] = "no";
            rtnData[1] = "[]";
        }
        return rtnData;
    }

    [WebMethod]
    public static string[] Save(Entity_ConsumoMaterial ent)
    {
        string[] rtnData = new string[2];
        try
        {
            Consumo obj = new Consumo();


            ent.strUsuarioAlta = ClasicoConcreto.SEMSession.GetInstance.IntCliente.ToString();
            ent.strMaquinaAlta = ClasicoConcreto.SEMSession.GetInstance.StrMaquina;

             obj.Save(ent);

        
            rtnData[0] = "ok";
            rtnData[1] = "";
     
        }
        catch (Exception ex)
        {
            rtnData[0] = "no";
            rtnData[1] = ex.Message;
        }
        return rtnData;
    }



}
