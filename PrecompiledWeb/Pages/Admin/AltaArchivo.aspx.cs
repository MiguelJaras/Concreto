using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_AltaArchivo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        HttpFileCollection uploadedFiles = Request.Files;
        if (uploadedFiles.Count > 0)
        {
            for (int i = 0; i < uploadedFiles.Count; i++)
            {
                HttpPostedFile file = uploadedFiles[i];

            }
        }
    }
}