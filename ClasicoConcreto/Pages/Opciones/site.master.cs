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
using ClasicoConcreto.Bussines;
using ClasicoConcreto.Entity;

public partial class Pages_Base : MasterPage
{
    public event HandlerEvent PageEvent;
    public string realPath;
    public int intProveedor;
    protected void Page_Load(object sender, EventArgs e)
    {
        //string end = (Request.ApplicationPath.EndsWith("/")) ? "" : "/";
        //string path = Request.ApplicationPath + end;
        //intProveedor = ClasicoConcreto.SEMSession.GetInstance.IntProveedor;
        //if (!IsPostBack)
        //{
        //    //lblProveedor.Text = Session["Proveedor"].ToString();
        //}

        //realPath = String.Format("http://{0}{1}{2}", Request.Url.Authority, path, "Img/bg_btn.png");        
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //RefreshSession();
    }

    #region RefreshSession
    private void RefreshSession()
    {
        string end = (Request.ApplicationPath.EndsWith("/")) ? "" : "/";
        string path = Request.ApplicationPath + end;
        string appPath = String.Format("http://{0}{1}{2}", Request.Url.Authority, path, "Login/FinSesion.aspx?id=0");
        
        Response.ClearHeaders();
        Response.AppendHeader("Refresh", Convert.ToString((Session.Timeout * 50)) + "; URL=" + appPath);
    }
    #endregion

    protected void lnkCerrarSesion_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
    }

}
