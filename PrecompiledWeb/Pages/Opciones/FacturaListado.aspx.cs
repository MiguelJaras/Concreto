using ClasicoConcreto.Bussines;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.Services;

public partial class Pages_Opciones_FacturasListado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string[] GetList()
    {
        string[] rtnData = new string[2];
        try
        {
            Facturas objFacturas = new Facturas();
            DataTable lst = objFacturas.GetList();
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