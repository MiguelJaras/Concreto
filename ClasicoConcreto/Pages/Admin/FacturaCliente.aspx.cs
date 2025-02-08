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
public partial class Pages_Admin_FacturaCliente : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            GetCiudades();
            GetEstado();
            
        }
    }

    protected void GetCiudades()
    {
        CITY obj = new CITY();
        var lst = obj.GetList();
        ddlCiudad.DataSource = lst;
        ddlCiudad.DataTextField = "City";
        ddlCiudad.DataValueField = "City";
        ddlCiudad.DataBind();
        ddlCiudad.Items.Insert(0, new ListItem("--Seleccione--", ""));
        ddlCiudad.SelectedValue = ClasicoConcreto.SEMSession.GetInstance.StrCity;

    }

    protected void GetEstado()
    {
        STATE obj = new STATE();
        var lst = obj.GetList();
        ddlEstado.DataSource = lst;
        ddlEstado.DataTextField = "State_Code";
        ddlEstado.DataValueField = "State_Code";
        ddlEstado.DataBind();
        ddlEstado.Items.Insert(0, new ListItem("--Seleccione--", ""));
        ddlEstado.SelectedValue = ClasicoConcreto.SEMSession.GetInstance.StrStateCode;
    }

    [WebMethod]
    public static string[] Get(int intCliente)
    {
        string[] rtnData = new string[2];
        try
        {
            FacturasCliente obj = new FacturasCliente();
            DataTable dt = obj.Get(intCliente);
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
    public static string[] Save(Entity_FacturasCliente ent)
    {
        string[] rtnData = new string[2];
        try
        {
            FacturasCliente obj = new FacturasCliente();
            ent.intEstatus = 1;
            ent.strUsuarioAlta = ClasicoConcreto.SEMSession.GetInstance.IntCliente.ToString();
            ent.strMaquinaAlta = ClasicoConcreto.SEMSession.GetInstance.StrMaquina;

            int intCliente = obj.Save(ent);

            rtnData[0] = "ok";
            rtnData[1] = intCliente.ToString();
        }
        catch (Exception ex)
        {
            rtnData[0] = "no";
            rtnData[1] = ex.Message;
        }
        return rtnData;
    }

    [WebMethod]
    public static string[] Procesar(int intCliente)
    {
        string[] rtnData = new string[2];
        try
        {
            FacturasCliente obj = new FacturasCliente();
            obj.SaveEstatus(intCliente, 2);
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