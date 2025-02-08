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

public partial class Pages_Admin_FacturaClienteListado : System.Web.UI.Page
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
            FacturasCliente obj = new FacturasCliente();
            DataTable dt = obj.GetList(0);
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

}