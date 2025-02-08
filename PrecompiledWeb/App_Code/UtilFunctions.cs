using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using iTextSharp.text.pdf;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Linq;
using ClasicoConcreto.Entity;
using ClasicoConcreto.Bussines;

/// <summary>
/// Descripción breve de UtilFunctions
/// </summary>
public class UtilFunctions
{
	public UtilFunctions()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    #region Function XML
    #region RemoveAllNamespaces
    public static XmlDocument RemoveAllNamespaces(XmlDocument doc)
    {
        XDocument d;
        using (var nodeReader = new XmlNodeReader(doc))
            d = XDocument.Load(nodeReader);

        d.Root.Descendants().Attributes().Where(x => x.IsNamespaceDeclaration).Remove();
        foreach (var elem in d.Descendants())
            elem.Name = elem.Name.LocalName;

        var xmlDocument = new XmlDocument();
        using (var xmlReader = d.CreateReader())
            xmlDocument.Load(xmlReader);
        return xmlDocument;
    }
    #endregion

    #region GetValueAttribute
    public static string GetValueAttribute(XmlDocument doc, string strNode, string[] arrAttribute)
    {
        string strValue = "";
        XmlNode objNode;

        objNode = doc.DocumentElement[strNode];
        if (objNode == null)
            objNode = doc.DocumentElement;

        //Recorre el arreglo de string hasta encontrar el primer valor
        strValue = GetValueAttribute(objNode, arrAttribute);
        return strValue;
    }
    #endregion

    #region GetValueAttribute
    public static string GetValueAttribute(XmlNode root, string[] arrAttribute)
    {
        string str = "";
        try
        {

            //Se recorren todos los atributos del nodo principal
            for (int i = 0; i < root.Attributes.Count; i++)
            {
                string strAtributo = root.Attributes[i].Name.ToLower();
                // se busca en el arreglo de string el atributo
                string[] result = Array.FindAll(arrAttribute, a => a.Equals(strAtributo, StringComparison.CurrentCultureIgnoreCase));
                if (result.Count() > 0) //Si se encuentra el atributo se retorna el valor
                {
                    str = root.Attributes[i].Value;
                    return str;
                }
            }

            //Se recorren los nodos
            for (int i = 0; i < root.ChildNodes.Count; i++)
            {
                XmlNode childNode = root.ChildNodes[i];
                str = GetValueAttribute(childNode, arrAttribute);
                if (str != "")
                    return str;
            }
        }
        catch { }
        return str;
    }
    #endregion

    #endregion

    #region function PDF
    public static string OCR(string strFileName)
    {
        string sResult = "";
        try
        {
            using (PdfReader reader = new PdfReader(strFileName))
            {
                //PdfReader reader = new PdfReader(strFileName);
                iTextSharp.text.pdf.parser.PdfReaderContentParser parser = new iTextSharp.text.pdf.parser.PdfReaderContentParser(reader);
                iTextSharp.text.pdf.parser.ITextExtractionStrategy strategy;

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    strategy = parser.ProcessContent(i, new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy());
                    sResult += strategy.GetResultantText();
                    sResult = sResult.Replace("\n", "").Replace("<", "").Replace(">", "").TrimStart().TrimEnd();
                }
            }
        }
        catch { };
        return sResult;
    }
    #endregion

    #region HTML
    public static string UnEscape(string str)
    {
        return HttpUtility.UrlDecode(str);
    }
    #endregion

    #region GetEnumDescription
    public static string GetEnumDescription(Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());

        DescriptionAttribute[] attributes =
            (DescriptionAttribute[])fi.GetCustomAttributes(
            typeof(DescriptionAttribute),
            false);

        if (attributes != null &&
            attributes.Length > 0)
            return attributes[0].Description;
        else
            return value.ToString();
    }
    #endregion

    #region Report
    public static byte[] ExportReportToStream(ReportDocument crReportDocument, ExportFormatType exptype)
    {
        Stream st;
        st = crReportDocument.ExportToStream(exptype);

        byte[] arr = new byte[st.Length];
        st.Read(arr, 0, (int)st.Length);

        return arr;

    }

    private static void SetReportConnectionInfo(ReportDocument reportDocument)
    {
        Servidor ser = new Servidor();

        Entity_Servidor obj = new Entity_Servidor();

        obj = ser.Credenciales();

        foreach (CrystalDecisions.CrystalReports.Engine.Table table in reportDocument.Database.Tables)
        {

            table.LogOnInfo.ConnectionInfo.ServerName = obj.StrSQLIP;
            table.LogOnInfo.ConnectionInfo.UserID = obj.StrSQLUser;
            table.LogOnInfo.ConnectionInfo.Password = obj.StrSQLPass;
            table.ApplyLogOnInfo(table.LogOnInfo);
        }

    }
    
    //Retorna arreglo de bytes, de un reporte
    public static byte[] GetReport(string strMapPath, string[] arrParam, string strDB, ExportFormatType objType)
    {

        byte[] arrReport;
        try
        {
            //ReportDocument objRD = new ReportDocument();
            using (ReportDocument objRD = new ReportDocument())
            {
                string strMap = HostingEnvironment.MapPath(strMapPath);
                objRD.Load(strMap);
                SetReportConnectionInfo(objRD);
                //objRD.SetDatabaseLogon("sa", "M1rf3l", "192.168.80.5", strDB, true);
                for (int i = 0; i < arrParam.Length; i++)
                {
                    objRD.SetParameterValue(i, arrParam[i]);
                }
                arrReport = UtilFunctions.ExportReportToStream(objRD, objType);
            }
            //objRD.Dispose();
        }
        catch (Exception ex)
        {
            arrReport = null;
        }

        return arrReport;
    }
    #endregion

}