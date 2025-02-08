using ClasicoConcreto.Bussines;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.Services;
using ClasicoConcreto.Entity;
using System.IO;
using System.Collections.Generic;

public partial class Pages_Admin_FacturaEmail : BasePage
{
    private string strDirectory;
    protected void Page_Load(object sender, EventArgs e)
    {
        strDirectory = GetParametro(ParametroFactura.RutaFisicaFacturas); //strRutaFisicaFacturas;
    }


    [WebMethod]
    public static string[] GetList()
    {
        string[] rtnData = new string[2];
        try
        {
            Facturas objFacturas = new Facturas();
            DataTable lst = objFacturas.GetList(false);
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
    public static string[] EnviarEmail(List<string> lstFacturas)
    {
        string[] rtnData = new string[2];
        try
        {
            string strDirectory = GetParametro(ParametroFactura.RutaFisicaFacturas);
            Facturas objFactura = new Facturas();
            foreach (string strFactura in lstFacturas)
            {
                DataTable dtPedidos = objFactura.GetListPedidosSinEnviar(strFactura);
                foreach(DataRow dtRow in dtPedidos.Rows)
                {
                    int intPedido = dtRow.Field<int>("intPedido");
                    string strEmail = dtRow.Field<string>("strEmail");
                    string strPDF = dtRow.Field<string>("strPDF");
                    string strXML = dtRow.Field<string>("strXML");
                    

                    Entity_Email entEmail = new Entity_Email();
                    Email objEmail = new Email();
                    

                }

            }

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

}