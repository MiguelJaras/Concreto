using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.IO;
public partial class Pages_Reportes_ContentFirma : System.Web.UI.Page
{

    private const string path = @"\\192.168.100.10\wwwroot\Firmas\";
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string[] SaveFile(string fileBase64)
    {
        string[] rtnData = new string[3];
        try
        {
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            string name = currentTime.ToString().Replace(".", "_").Replace(":","_");
            string imgPath = Path.Combine(path, name + ".jpg");
            string dataUrl = fileBase64.Substring(fileBase64.LastIndexOf(',') + 1);

            byte[] imageBytes = Convert.FromBase64String(dataUrl);
            File.WriteAllBytes(imgPath, imageBytes);

            rtnData[0] = "ok";
        }

        catch (Exception ex)
        {
            rtnData[0] = "Error image save";
            System.Diagnostics.Debug.Print(ex.Message);
        }
        return rtnData;
    }

}