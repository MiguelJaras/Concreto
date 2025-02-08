using ClasicoConcreto.Bussines;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.Services;
using ClasicoConcreto.Entity;
using ClasicoConcreto.Bussines;
using System.Collections.Generic;
using System.Web;

public partial class Pages_Catalogos_Servicio : BasePage
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
            ServiciosEspeciales objServicios = new ServiciosEspeciales();
            DataTable lst = objServicios.GetList();
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
    public static string[] Guardar(int intServicio, string strServicio, string strPrecioBase)
    {
        string[] rtnData = new string[1];
        try
        {
            decimal decPrecioBase;
            decimal.TryParse(strPrecioBase, out decPrecioBase);

            Entity_ServiciosEspeciales entServicio = new Entity_ServiciosEspeciales();
            entServicio.intServicio = intServicio;
            entServicio.strNombre = UtilFunctions.UnEscape(strServicio);
            entServicio.dblPrecio = decPrecioBase;
            entServicio.strUsuarioAlta = ClasicoConcreto.SEMSession.GetInstance.IntCliente.ToString();
            entServicio.strMaquinaAlta = ClasicoConcreto.SEMSession.GetInstance.StrMaquina.ToString();
            ServiciosEspeciales objServicio = new ServiciosEspeciales();
            objServicio.Save(entServicio);
            rtnData[0] = "ok";
        }
        catch (Exception ex)
        {
            rtnData[0] = "no";
        }
        return rtnData;
    }

    [WebMethod]
    public static string[] Eliminar(int intServicio)
    {
        string[] rtnData = new string[1];
        try
        {
            ServiciosEspeciales objServicio = new ServiciosEspeciales();
            objServicio.Delete(intServicio);
            rtnData[0] = "ok";
        }
        catch (Exception ex)
        {
            rtnData[0] = "no";
        }
        return rtnData;
    }

}