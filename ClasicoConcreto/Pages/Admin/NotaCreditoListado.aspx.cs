using ClasicoConcreto.Bussines;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.Services;
using ClasicoConcreto.Entity;
using System.IO;

public partial class Pages_Admin_NotaCreditoListado : BasePage
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
            NotaCredito objNotas = new NotaCredito();
            DataTable lst = objNotas.GetList();
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
    public static string[] Eliminar(string strFolio)
    {
        string[] rtnData = new string[2];
        try
        {
            NotaCredito objNotaCredito = new NotaCredito();
            DataTable dt = objNotaCredito.Get(strFolio);
            if (dt.Rows.Count > 0)
            {

                objNotaCredito.Delete(strFolio);
                //string strPDF = dt.Rows[0]["strPDF"].ToString();
                //string strXML = dt.Rows[0]["strXML"].ToString();
                //string strDirectory = GetParametro(ParametroFactura.RutaFisicaFacturas);
                //string strRutaFisica = strDirectory + strPDF;
            }


            rtnData[0] = "ok";
            rtnData[1] = "";
        }
        catch (Exception ex)
        {
            rtnData[0] = "no";
            rtnData[1] = ex.Message.ToString();
        }
        return rtnData;
    }



}