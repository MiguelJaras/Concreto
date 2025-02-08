using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Web.Services;
using ClasicoConcreto.Entity;
using ClasicoConcreto.Bussines;
using System.Data;

public partial class Pages_Admin_PrecioServicio : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetPrecioLista();
        }
    }
    private void GetPrecioLista()
    {
        ListaPrecios objListaPrecios = new ListaPrecios();
        DataTable dt = objListaPrecios.GetList();
        ddlListaPrecio.DataSource = dt;
        ddlListaPrecio.DataValueField = "intLista";
        ddlListaPrecio.DataTextField = "strNombre";
        ddlListaPrecio.DataBind();
        ddlListaPrecio.Items.Insert(0, new ListItem("--Todos--", "0"));
    }

    [WebMethod]
    public static string[] GetList(int intLista)
    {
        string[] rtnData = new string[2];
        try
        {
            ListaPrecios_Servicio objListaPrecios = new ListaPrecios_Servicio();
            DataTable dt = objListaPrecios.GetList(intLista);
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
    public static string[] Save(int intLista, int intServicio, decimal dblPrecio)
    {
        string[] rtnData = new string[2];
        try
        {
            Entity_ListaPrecios_Servicio entListaPrecios = new Entity_ListaPrecios_Servicio();
            entListaPrecios.intLista = intLista;
            entListaPrecios.intServicio = intServicio;
            entListaPrecios.dblPrecio = dblPrecio;
            entListaPrecios.intClienteAlta = ClasicoConcreto.SEMSession.GetInstance.IntCliente;
            entListaPrecios.strMaquina = ClasicoConcreto.SEMSession.GetInstance.StrMaquina;

            ListaPrecios_Servicio objListaPrecios = new ListaPrecios_Servicio();
            objListaPrecios.Save(entListaPrecios);
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