using ClasicoConcreto.Bussines;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.Services;
using ClasicoConcreto.Entity;
using ClasicoConcreto.Bussines;
using System.Collections.Generic;

public partial class Pages_Admin_Clientes : System.Web.UI.Page
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
            Clientes objClientes = new Clientes();
            DataTable dtClientes = objClientes.GetList(0);
            rtnData[0] = "ok";
            rtnData[1] = JsonConvert.SerializeObject(dtClientes);
        }
        catch
        {
            rtnData[0] = "no";
            rtnData[1] = "[]";
        }
        return rtnData;
    }

    [WebMethod]
    public static string[] Save(Entity_Clientes ent)
    {
        string[] rtnData = new string[2];
        try
        {
            ent.Customer_Code = "";
            ent.strPassword = ent.strEmail;
            ent.strUsuarioAlta = ClasicoConcreto.SEMSession.GetInstance.IntCliente.ToString();
            ent.strMaquinaAlta = ClasicoConcreto.SEMSession.GetInstance.StrMaquina;

            Clientes obj = new Clientes();
            obj.Save(ent);
            rtnData[0] = "ok";
            rtnData[1] = "";
        }
        catch(Exception ex)
        {
            rtnData[0] = "no";
            rtnData[1] = "[]";
        }
        return rtnData;
    }


    [WebMethod]
    public static string[] Delete(int intCliente)
    {
        string[] rtnData = new string[1];
        try
        {
            Clientes obj = new Clientes();
            obj.Delete(intCliente);
            rtnData[0] = "ok";
        }
        catch (Exception ex)
        {
            rtnData[0] = "no";
        }
        return rtnData;
    }


}