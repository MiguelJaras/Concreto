using ClasicoConcreto.Bussines;
using CrystalDecisions.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Hosting;
using System.Web.Services;
using System.Web.UI.WebControls;
public partial class Pages_Admin_PedidoListadoImpresion : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime dtIni = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime dtFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            txtFechaInicio.Text = dtIni.ToString("dd/MM/yyyy");
            txtFechaFin.Text = dtFin.ToString("dd/MM/yyyy");
            GetClientes();
        }
    }

    protected void GetClientes()
    {
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
            DateTime dtInicio, dtFin;
            DateTime.TryParse(strFechaInicio, out dtInicio);
            DateTime.TryParse(strFechaFin, out dtFin);


            Pedido objPedido = new Pedido();
            DataTable lst = objPedido.GetListByDate(intCliente, dtInicio, dtFin);
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

    #region WEBMETHOD
    [WebMethod]
    public static string[] Imprimir(string strPedidos)
    {
        string[] rtnData = new string[2];
        try
        {
            string strFileName = "ImpresionPedidos_" + ClasicoConcreto.SEMSession.GetInstance.IntCliente.ToString() + ".pdf";
            string[] arrParam = new string[2];
            List<byte[]> lstArrBytes = new List<byte[]>(); //Archivo Final

            string[] arrPedidos = strPedidos.Split(new char[] {','});
            foreach (string idPedido in arrPedidos)
            {
                byte[] arrConcreto = UtilFunctions.GetReport("~/Pages/Reportes/Concreto.rpt", new string[] { idPedido }, "dbConcreto", ExportFormatType.PortableDocFormat);
                lstArrBytes.Add(arrConcreto);
            }

            //Se crea archivo temporal
            if (lstArrBytes.Count > 0)
            {
                string strFileTemp = HostingEnvironment.MapPath("~/Temp/") + strFileName;
                MergePDF objMerge = new MergePDF();
                byte[] arrDocMerge = objMerge.Merge(lstArrBytes, OptionMergePDF.ITextSharp);
                System.IO.File.WriteAllBytes(strFileTemp, arrDocMerge);
            }
            
            
            rtnData[0] = "ok";
            rtnData[1] = strFileName;
        }
        catch(Exception ex)
        {
            rtnData[0] = "no";
            rtnData[1] = ex.Message;
        }
        return rtnData;
    }
    #endregion


    

}