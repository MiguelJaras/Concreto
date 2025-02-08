using ClasicoConcreto.Bussines;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.Services;
using ClasicoConcreto.Entity;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Precios : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetEmpresas();
        GetProductos();
    }

    [WebMethod]
    public static string[] GetList()
    {
        string[] rtnData = new string[2];
        try
        {
            //Falta ingresar empresa
            Precios objPrecios = new Precios();
            DataTable dtPrecios = objPrecios.GetList(0);
            rtnData[0] = "ok";
            rtnData[1] = JsonConvert.SerializeObject(dtPrecios);
        }
        catch
        {
            rtnData[0] = "no";
            rtnData[1] = "[]";
        }
        return rtnData;
    }


    [WebMethod]
    public static string[] Guardar(int intEmpresa, string strInsumo, int intProducto, decimal dblPrecio)
    {
        string[] rtnData = new string[1];
        try
        {
            Entity_Precios entPrecio = new Entity_Precios();
            entPrecio.IntEmpresa = intEmpresa;
            entPrecio.StrInsumo = strInsumo;
            entPrecio.DblPrecio = dblPrecio;
            entPrecio.IntProducto = intProducto;
            entPrecio.StrUsuario = ClasicoConcreto.SEMSession.GetInstance.IntCliente.ToString();
            entPrecio.StrMaquina = ClasicoConcreto.SEMSession.GetInstance.StrMaquina.ToString();
            entPrecio.DatFecha = DateTime.Now;
            Precios objPrecios = new Precios();
            objPrecios.Save(entPrecio);
            rtnData[0] = "ok";
        }
        catch(Exception ex)
        {
            rtnData[0] = "no";
        }
        return rtnData;
    }


    [WebMethod]
    public static string[] Eliminar(int intEmpresa, string strInsumo, int intProducto)
    {
        string[] rtnData = new string[1];
        try
        {
            Precios objPrecios = new Precios();
            objPrecios.Delete(intEmpresa, strInsumo, intProducto);
            rtnData[0] = "ok";
        }
        catch (Exception ex)
        {
            rtnData[0] = "no";
        }
        return rtnData;
    }

    protected void GetEmpresas()
    {
        Empresas obj = new Empresas();
        int emp = 0;
        var lst = obj.GetList(emp);
        ddlEmpresa.DataSource = lst;
        ddlEmpresa.DataTextField = "strNombre";
        ddlEmpresa.DataValueField = "intEmpresa";
        ddlEmpresa.DataBind();
        ddlEmpresa.Items.Insert(0, new ListItem("--Seleccione--", ""));
        ddlEmpresa.SelectedValue = ClasicoConcreto.SEMSession.GetInstance.StrStateCode;

    }

    protected void GetProductos()
    {
        Producto obj = new Producto();
        var lst = obj.GetList(); ;
        ddlProducto.DataSource = lst;
        ddlProducto.DataTextField = "strNombre";
        ddlProducto.DataValueField = "intProducto";
        ddlProducto.DataBind();
        ddlProducto.Items.Insert(0, new ListItem("--Seleccione--", ""));
        ddlProducto.SelectedValue = ClasicoConcreto.SEMSession.GetInstance.StrStateCode;
    }

    [WebMethod]
    public static string[] GetInsumo(int intEmpresa)
    {
        string[] rtnData = new string[2];
        try
        {
            Precios obj = new Precios();
            DataTable lst = obj.GetInsumo(intEmpresa);
            rtnData[0] = "ok";
            rtnData[1] = JsonConvert.SerializeObject(lst);
        }
        catch (Exception ex)
        {
            rtnData[0] = "no";
            rtnData[1] = ex.Message;
        }
        return rtnData;
    }

}