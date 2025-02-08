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

public partial class Pages_Admin_PrecioProducto : BasePage
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
        ddlListaPrecio.Items.Insert(0, new ListItem("--Todos--","0"));
    }

    [WebMethod]
    public static string[] GetList(int intLista)
    {
        string[] rtnData = new string[2];
        try
        {
            ListaPrecios_Producto objListaPrecios = new ListaPrecios_Producto();
            DataTable dt = objListaPrecios.GetListProductos(intLista);
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
    public static string[] Save(int intLista, int intProducto, decimal dblMenudeo, decimal dblMedioMayoreo, decimal dblMayoreo)
    {
        string[] rtnData = new string[2];
        try
        {
            Entity_ListaPrecios_Producto entListaPrecios = new Entity_ListaPrecios_Producto();
            entListaPrecios.intLista = intLista;
            entListaPrecios.intProducto = intProducto;
            entListaPrecios.dblMenudeo = dblMenudeo;
            entListaPrecios.dblMedioMayoreo = dblMedioMayoreo;
            entListaPrecios.dblMayoreo = dblMayoreo;
            entListaPrecios.intClienteAlta = ClasicoConcreto.SEMSession.GetInstance.IntCliente;
            entListaPrecios.strMaquina = ClasicoConcreto.SEMSession.GetInstance.StrMaquina;

            ListaPrecios_Producto objListaPrecios = new ListaPrecios_Producto();
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