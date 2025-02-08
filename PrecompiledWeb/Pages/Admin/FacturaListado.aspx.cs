using ClasicoConcreto.Bussines;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.Services;
using ClasicoConcreto.Entity;
using System.IO;
public partial class Pages_Admin_FacturaListado : BasePage
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

    [WebMethod]
    public static string[] SaveFacturaDescarga(string strFactura, string strPDF)
    {
        string[] rtnData = new string[2];
        try
        {
            Entity_Facturas entFacturas = new Entity_Facturas();
            entFacturas.strFactura = strFactura;
            entFacturas.strPDF = strPDF;
            entFacturas.IntParametroInicial = ClasicoConcreto.SEMSession.GetInstance.IntCliente;
            entFacturas.strMaquinaAlta = ClasicoConcreto.SEMSession.GetInstance.StrMaquina;

            Facturas objFacturas = new Facturas();
            objFacturas.SaveFacturaDescarga(entFacturas);
            rtnData[0] = "ok";
            rtnData[1] = "";
        }
        catch
        {
            rtnData[0] = "no";
            rtnData[1] = "[]";
        }
        return rtnData;
    }

    [WebMethod]
    public static string[] Eliminar(int intEmpresa, string strFactura, string strSerie)
    {
        string[] rtnData = new string[2];
        try
        {
            Facturas objFacturas = new Facturas();
            Entity_Facturas entFacturas = new Entity_Facturas();
            entFacturas.intEmpresa = intEmpresa;
            entFacturas.strFactura = strFactura;
            entFacturas.strSerie = strSerie;


            DataTable dt = objFacturas.Get(entFacturas);
            if (dt.Rows.Count > 0)
            {

                objFacturas.Delete(entFacturas);

                string strPDF = dt.Rows[0]["strPDF"].ToString();
                string strXML = dt.Rows[0]["strXML"].ToString();
                string strDirectory = GetParametro(ParametroFactura.RutaFisicaFacturas);
                string strRutaFisica = strDirectory + strPDF;
                //if (File.Exists(strRutaFisica))
                //    File.Delete(strRutaFisica);

                //strRutaFisica = strDirectory + strXML;
                //if (File.Exists(strRutaFisica))
                //    File.Delete(strRutaFisica);
            }

            
            rtnData[0] = "ok";
            rtnData[1] = "";
        }
        catch(Exception ex)
        {
            rtnData[0] = "no";
            rtnData[1] = ex.Message.ToString();
        }
        return rtnData;
    }

}