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
using System.Text.RegularExpressions;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net.Mail;
using System.IO;
using ExtensionMethods;
using HtmlAgilityPack;
using System.Text;
public partial class Pages_Admin_NotaCreditoGenerar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetClientes();
        }
    }

    protected void GetClientes()
    {
        Facturas objFacturas = new Facturas();
        DataTable dt = objFacturas.GetListClientes();
        ddlCliente.DataSource = dt;
        ddlCliente.DataValueField = "strCliente";
        ddlCliente.DataTextField = "strCliente";
        ddlCliente.DataBind();
        ddlCliente.Items.Insert(0, new ListItem("--Seleccione--", ""));
        ddlCliente.SelectedValue = "0";

    }

    [WebMethod]
    public static string[] Get(int intNotaCredito)
    {
        string[] rtnData = new string[2];
        try
        {
            NotaCreditoGenerar obj = new NotaCreditoGenerar();
            DataTable dt = obj.Get(intNotaCredito);
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
    public static string[] GenerarNotaCredito(Entity_NotaCreditoGenerar ent)
    {
        string[] rtnData = new string[2];
        try
        {
            NotaCreditoGenerar obj = new NotaCreditoGenerar();
            ent.intEstatus = 1;
            ent.strUsuarioAlta = ClasicoConcreto.SEMSession.GetInstance.IntCliente.ToString();
            ent.strMaquinaAlta = ClasicoConcreto.SEMSession.GetInstance.StrMaquina;

            int intNotaCredito = obj.Save(ent);

            rtnData[0] = "ok";
            rtnData[1] = intNotaCredito.ToString();
        }
        catch (Exception ex)
        {
            rtnData[0] = "no";
            rtnData[1] = ex.Message;
        }
        return rtnData;
    }

    [WebMethod]
    public static string[] Procesar(int intNotaCredito)
    {
        string[] rtnData = new string[2];
        try
        {
            NotaCreditoGenerar obj = new NotaCreditoGenerar();
            obj.SaveEstatus(intNotaCredito, 2);
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