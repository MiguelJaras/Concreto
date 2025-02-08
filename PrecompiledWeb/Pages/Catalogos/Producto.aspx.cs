using ClasicoConcreto.Bussines;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.Services;
using ClasicoConcreto.Entity;
using ClasicoConcreto.Bussines;
using System.Collections.Generic;

public partial class Pages_Catalogos_Producto : BasePage
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
            Producto objProducto = new Producto();
            DataTable dtProductos = objProducto.GetList();
            rtnData[0] = "ok";
            rtnData[1] = JsonConvert.SerializeObject(dtProductos);
        }
        catch
        {
            rtnData[0] = "no";
            rtnData[1] = "[]";
        }
        return rtnData;
    }


    [WebMethod]
    public static string[] Guardar(int intProducto, string strProducto, bool bActivo)
    {
        string[] rtnData = new string[1];
        try
        {
            Entity_Producto entProducto = new Entity_Producto();
            entProducto.intProducto = intProducto;
            entProducto.strNombre = UtilFunctions.UnEscape(strProducto);
            entProducto.bEstatus = bActivo;
            entProducto.strUsuarioAlta = ClasicoConcreto.SEMSession.GetInstance.IntCliente.ToString();
            entProducto.strMaquinaAlta = ClasicoConcreto.SEMSession.GetInstance.StrMaquina.ToString();
            Producto objProducto = new Producto();
            objProducto.Save(entProducto);
            rtnData[0] = "ok";
        }
        catch(Exception ex)
        {
            rtnData[0] = "no";
        }
        return rtnData;
    }

    [WebMethod]
    public static string[] Eliminar(int intProducto)
    {
        string[] rtnData = new string[1];
        try
        {
            Producto objProducto = new Producto();
            objProducto.Delete(intProducto);
            rtnData[0] = "ok";
        }
        catch (Exception ex)
        {
            rtnData[0] = "no";
        }
        return rtnData;
    }
}