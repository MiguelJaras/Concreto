using System;
using System.Web;
using ClasicoConcreto.Entity;
using System.Collections.Generic;
using Newtonsoft.Json;
using ClasicoConcreto.Bussines;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{
    public List<Entity_Opcion> lstOpcionUsuario = new List<Entity_Opcion>();
    public BasePage()
    {
        Init += new EventHandler(BasePage_Init);
    }

    protected void BasePage_Init(object sender, EventArgs e)
    {
        var blnAutenticado = HttpContext.Current.User.Identity.IsAuthenticated;
        if (!blnAutenticado)
        {
            Response.Redirect("~/Login/Login.aspx");
        }
        else 
        {
            CargarPermisos();
            //ValidarPermiso();
        }

    }

    int _IdMenu = 0;
    public int IdMenu
    {
        get { return _IdMenu; }
        set { _IdMenu = value; }
    }

    //Consulta parametros de la aplicación
    protected static string GetParametro(ParametroFactura enumParametro)
    {
        string strValue = "";
        int intParametro = (int)enumParametro; //Se extrae el valor numerico del enum
        Parametros objParametros = new Parametros();
        strValue = objParametros.Get(intParametro).strValor;
        return strValue;
    }


    #region Validar Permiso
    public void ValidarPermiso()
    {
        //Listado de paginas que tiene permitido el usuario
        //Validar pagina
        bool blnValida = false;
        foreach (var objOpcion in lstOpcionUsuario)
        {
            switch (strPaginaActual)
            {
                case "~/Pages/Default.aspx":
                    blnValida = true;
                    break;
                case "~/Utils/DisplayFile.aspx":
                    blnValida = true;
                    break;
                case "~/Pages/Admin/NotaCredito.aspx":
                    blnValida = true;
                    break;
                case "~/Pages/Admin/FacturaEmail.aspx":
                    blnValida = true;
                    break;
                case "~/Pages/Admin/FacturaPedido.aspx":
                    blnValida = true;
                    break;
                default:
                    if ((strPaginaActual).ToLower().Contains(objOpcion.StrURL.ToLower()))
                        blnValida = true;
                    break;
            }
        }
        if (!blnValida)
            Response.Redirect("~/Pages/Opciones/Default.aspx", false);
    }
    #endregion

    protected void CargarPermisos()
    {
        if (Session["lstPermisos"] == null)
        {
            int intCliente = ClasicoConcreto.SEMSession.GetInstance.IntCliente;
            Session["lstPermisos"] = JsonConvert.SerializeObject(GetListOpcion(intCliente));
        }
        lstOpcionUsuario = JsonConvert.DeserializeObject<List<Entity_Opcion>>(Session["lstPermisos"].ToString());
    }

    protected List<Entity_Opcion> GetListOpcion(int intCliente)
    {
        List<Entity_Opcion> lstOpcion = new List<Entity_Opcion>();
        Opcion objOpcion = new Opcion();
        lstOpcion = objOpcion.GetListByUser(intCliente);
        return lstOpcion;
    }

    protected string strPaginaActual
    {
        get { return Request.AppRelativeCurrentExecutionFilePath; }
    }

    protected enum ParametroFactura
    {
        RutaFisicaFacturas = 1,
        RutaFisicaNotasCredito = 2
    }

}
