using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using ClasicoConcreto.Bussines;
using ClasicoConcreto.Entity;


public partial class Utils_Excel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            string type = Request.QueryString["type"];
            string strParams = Request.QueryString["params"];
            string fileName = "";
            //type = "PedidosFacturas";
            //strParams = "20180601,20180630";
            string[] arrParams = strParams.Split(new char[] { ',' });
            string strQuery = "";
            if (type == "PedidosFacturas")
            {
                strQuery = "usp_tbPedido_RptFacturaExcel '" + arrParams[0] + "', '" + arrParams[1] + "'";
                fileName = "PedidosFacturas.xls";
            }
            else if (type == "Facturas")
            {
                strQuery = "usp_tbFacturas_RptExcel '" + arrParams[0] + "', '" + arrParams[1] + "'";
                fileName = "Facturas.xls";
            }
            else if (type == "Consumos")
            {
                strQuery = "usp_tbConsumoMateriales_Excel '" + arrParams[0] + "', '" + arrParams[1] + "'";
                fileName = "Consumos.xls";
            }


            ClasicoConcreto.Bussines.Excel objExcel = new ClasicoConcreto.Bussines.Excel();
            if (strQuery != "")
                dt = objExcel.BindGrid(strQuery).Tables[0];

            ExportToExcel(dt, fileName);
        }
        catch
        {
        
        }
    }

    private void ExportToExcel(DataTable table, string fileName)
    {
        //fileName = "PedidosFacturas.xls";

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Tahoma;'>");
        HttpContext.Current.Response.Write("<br>");
        HttpContext.Current.Response.Write("<table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'>");
        HttpContext.Current.Response.Write("<tr>");
        
        //Columns
        int columnscount = table.Columns.Count;
        //Header
        for (int j = 0; j < columnscount; j++)
        {
            HttpContext.Current.Response.Write("<td style='font-size:10pt; font-family:Tahoma; background:#383838; color:White '>");            
            HttpContext.Current.Response.Write("<b>");
            HttpContext.Current.Response.Write(table.Columns[j].ColumnName);
            HttpContext.Current.Response.Write("</b>");
            HttpContext.Current.Response.Write("</td>");
        }
        HttpContext.Current.Response.Write("</tr>");

        //Rows
        foreach (DataRow row in table.Rows)
        {
            HttpContext.Current.Response.Write("<tr>");

            for (int i = 0; i < table.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write("<td>");
                HttpContext.Current.Response.Write(Text(row[i].ToString()));
                HttpContext.Current.Response.Write("</td>");
            }

            HttpContext.Current.Response.Write("</tr>");
        }
        HttpContext.Current.Response.Write("</table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }

    private string Text(string data)
    {
        string value = "";

        if (data.Contains("<"))
        {
            int point;

            point = data.LastIndexOf(">");

            value = data.Substring(point + 1);
        }
        else
            value = data;

        return value;
    }


}
