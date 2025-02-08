using System;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using ClasicoConcreto.Entity;
using System.IO;
using Newtonsoft.Json;
using System.Web.Services;
using ClasicoConcreto.Bussines;
using System.Data;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

public partial class Pages_Admin_FacturaAnticipadaPedidos : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFechaInicio.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
            txtFechaFin.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }

    #region WebMethod
    [WebMethod]
    public static string[] GetListFacturas()
    {
        string[] rtnData = new string[2];
        try
        {

            Facturas objFactura = new Facturas();
            DataTable lst = objFactura.GetListSinPedido();
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
    public static string[] GetListPedidos(string strFechaInicio, string strFechaFin)
    {
        string[] rtnData = new string[2];
        try
        {
            DateTime dtInicio, dtFin;
            DateTime.TryParse(strFechaInicio, out dtInicio);
            DateTime.TryParse(strFechaFin, out dtFin);

            Pedido objPedido = new Pedido();
            DataTable lst = objPedido.GetListSinFactura(dtInicio, dtFin);
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
    public static string[] Save(Entity_Facturas ent)
    {
        string[] rtnData = new string[2];
        try
        {
            Entity_Facturas entValida = new Entity_Facturas();
            entValida.strEmisorRFC = "";
            entValida.strReceptorRFC = "";
            entValida.strFactura = "";
            entValida.strPedidos = ent.strPedidos;
            entValida.dblImporte = ent.dblImporte;
            entValida.dblSubTotal = ent.dblSubTotal;

            Facturas obj = new Facturas();
            obj.Valida(entValida);
            obj.SavePedidos(ent);
            rtnData[0] = "ok";
            rtnData[1] = "";
        }
        catch(Exception ex)
        {
            rtnData[0] = "no";
            rtnData[1] = ex.Message;
        }
        return rtnData;
    }
    #endregion



}