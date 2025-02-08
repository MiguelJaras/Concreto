using ClasicoConcreto.Bussines;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.Services;
using ClasicoConcreto.Entity;
using System.IO;

public partial class Pages_Admin_NotaCreditoGenerarListado : System.Web.UI.Page
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
            NotaCreditoGenerar objNotas = new NotaCreditoGenerar();
            DataTable dt = objNotas.GetList();
            rtnData[0] = "ok";
            rtnData[1] = JsonConvert.SerializeObject(dt);
        }
        catch
        {
            rtnData[0] = "no";
            rtnData[1] = "[]";
        }
        return rtnData;
    }

    [WebMethod]
    public static string[] Delete(int intNotaCredito)
    {
        string[] rtnData = new string[2];
        try
        {
            NotaCreditoGenerar objNota = new NotaCreditoGenerar();
            objNota.Delete(intNotaCredito);
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