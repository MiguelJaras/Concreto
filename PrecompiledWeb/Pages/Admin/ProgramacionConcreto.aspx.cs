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



public partial class Pages_Admin_ProgramacionConcreto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            GetHoras();
        }
    }

    protected void GetHoras()
    {
        Horas objHoras = new Horas();
        List<Entity_Horas> lst = objHoras.GetList();
        ddlHoras.DataSource = lst;
        ddlHoras.DataTextField = "strHora";
        ddlHoras.DataValueField = "strHora";
        ddlHoras.DataBind();
        ddlHoras.Items.Insert(0, new ListItem("--Seleccione--", "0"));
    }

    /*WebMethods*/
    #region WebMethods
    [WebMethod]
    public static string[] GetList(string strFecha)
    {
        string[] rtnData = new string[2];
        try
        {
            int intCliente = ClasicoConcreto.SEMSession.GetInstance.IntCliente;
            DateTime datFecha;
            DateTime.TryParse(strFecha, out datFecha);
            ProgramacionConcreto obj = new ProgramacionConcreto();
            DataTable dt = obj.GetList(datFecha, intCliente);
            string JSONData = JsonConvert.SerializeObject(dt);
            rtnData[0] = "ok";
            rtnData[1] = JSONData;
        }
        catch
        {
            rtnData[0] = "no";
            rtnData[1] = "[]";
        }
        return rtnData;
    }
    #endregion

    /*WebMethods*/
    #region WebMethods
    [WebMethod]
    public static string[] Save(int intReqDet, string strHoraAnt, string strHoraNueva, int intPlanta, string strRemision)
    {
        string[] rtnData = new string[2];
        try
        {

            ProgramacionConcreto obj = new ProgramacionConcreto();
            obj.Save(intReqDet, strHoraAnt, strHoraNueva);
            obj.SavePlantaRemision(intReqDet, intPlanta, strRemision);

            //string JSONData = JsonConvert.SerializeObject(dt);
            rtnData[0] = "ok";
            rtnData[1] = "";
        }
        catch
        {
            rtnData[0] = "no";
            rtnData[1] = "";
        }
        return rtnData;
    }
    #endregion


}