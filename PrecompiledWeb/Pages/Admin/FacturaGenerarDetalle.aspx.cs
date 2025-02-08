using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using ClasicoConcreto.Bussines;
using ClasicoConcreto.Entity;
using System.Data;
using Newtonsoft.Json;
public partial class Pages_Admin_FacturaGenerarDetalle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string[] Get(int intFactura)
    {
        string[] rtnData = new string[2];
        try
        {
            FacturasGenerar obj = new FacturasGenerar();
            DataTable dt = obj.Get(intFactura);
            rtnData[0] = "ok";
            rtnData[1] = JsonConvert.SerializeObject(dt);
            
        }
        catch (Exception ex)
        {
            rtnData[0] = "no";
            rtnData[1] = ex.Message;
        }
        return rtnData;
    }

    [WebMethod]
    public static string[] GetDetalle(int intFactura)
    {
        string[] rtnData = new string[2];
        try
        {
            FacturasGenerar obj = new FacturasGenerar();
            DataTable dt = obj.GetDetalle(intFactura);
            
                rtnData[0] = "ok";
                rtnData[1] = JsonConvert.SerializeObject(dt);
            
        }
        catch (Exception ex)
        {
            rtnData[0] = "no";
            rtnData[1] = ex.Message;
        }
        return rtnData;
    }



    [WebMethod]
    public static string[] Procesar(int intFactura)
    {
        string[] rtnData = new string[2];
        try
        {
            FacturasGenerar obj = new FacturasGenerar();
            obj.SaveEstatus(intFactura, 2);
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