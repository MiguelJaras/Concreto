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

public partial class Pages_Admin_PedidoListado : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetClientes();
            txtFechaInicio.Text = DateTime.Now.AddDays(-6).ToString("dd/MM/yyyy");
            txtFechaFin.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }

    protected void GetClientes() {
        Clientes objClientes = new Clientes();
        DataTable dtClientes = objClientes.GetList(0);
        ddlClientes.DataSource = dtClientes;
        ddlClientes.DataValueField = "intCliente";
        ddlClientes.DataTextField = "strNombre";
        ddlClientes.DataBind();
        ddlClientes.Items.Insert(0, new ListItem("--Todos--", "0"));
        ddlClientes.SelectedValue = "0";

    }

    [WebMethod]
    public static string[] GetList(int intCliente, string strFechaInicio, string strFechaFin)
    {
        string[] rtnData = new string[2];
        try
        {
            DateTime datFechaInicio, datFechaFin;

            DateTime.TryParse(strFechaInicio, out datFechaInicio);
            DateTime.TryParse(strFechaFin, out datFechaFin);

            Pedido objPedido = new Pedido();
            DataTable lst = objPedido.GetList(intCliente, datFechaInicio, datFechaFin);
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
    public static string[] GetDetallePedido(int intPedido)
    {
        string[] rtnData = new string[3];
        try
        {
            int intCliente = 0; //Si es admin se envia 0
            Ordenes objOrdenes = new Ordenes();
            DataTable dt = objOrdenes.GetOrdenesByPedido(intCliente, intPedido);
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