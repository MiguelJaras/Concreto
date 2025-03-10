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
using ClasicoConcreto.Entity;
using ClasicoConcreto.Bussines;

public partial class Pages_Base : MasterPage
{
    public event HandlerEvent PageEvent;
    public string realPath;

    protected void Page_Load(object sender, EventArgs e)
    {
        string end = (Request.ApplicationPath.EndsWith("/")) ? "" : "/";
        string path = Request.ApplicationPath + end;

        btnSave.Attributes.Add("onclick", "return datosCompletos();");
        btnDelete.Attributes.Add("onclick", "return confirm('�Desea eliminar?');");

        btnSave.UpdateAfterCallBack = true;
        btnDelete.UpdateAfterCallBack = true;

        realPath = String.Format("http://{0}{1}{2}", Request.Url.Authority, path, "Img/bg_btn.png");
        Permissions();
    }

    #region btn_Click
    protected void btn_Click(object sender, ImageClickEventArgs e)
    {
        Anthem.ImageButton btn = (Anthem.ImageButton)sender;
        HandlerArgs args = new HandlerArgs();
        switch (btn.CommandName)
        {
            case "Save":
                args.Event = Event.Save;
                break;
            case "Delete":
                args.Event = Event.Delete;
                break;
            case "New":
                args.Event = Event.New;
                break;
            case "List":
                args.Event = Event.List;
                break;
            case "Email":
                args.Event = Event.Email;
                break;
            case "Print":
                args.Event = Event.Print;
                break;
            default:
                break;
        }

        if (PageEvent != null)
            PageEvent(sender, args);

    }
    #endregion btn_Click

    private void Permissions()
    {
        btnSave.Visible = true;
        btnSave.UpdateAfterCallBack = true;
        btnDelete.Visible = true;
        btnDelete.UpdateAfterCallBack = true;
        btnNew.Visible = true;
        btnNew.UpdateAfterCallBack = true;
        btnList.Visible = true;
        btnList.UpdateAfterCallBack = true;
        btnPrint.Visible = true;
        btnPrint.UpdateAfterCallBack = true;
        btnEmail.Visible = true;
        btnEmail.UpdateAfterCallBack = true;

        //Entity_Usuarios obj;
        //obj = new Entity_Usuarios();
        //Usuario user;
        //user = new Usuario();

        //obj.StrUsuario = Abasto.SEMSession.GetInstance.StrUsuario;
        //obj.IntMenu = Convert.ToInt32(Request.QueryString["page"].ToString());

        //DataTable dt;
        //dt = user.Permissions(obj);

        //foreach (DataRow dr in dt.Rows)
        //{
        //    string funcion = dr["strFuncion"].ToString();
        //    switch (funcion)
        //    {
        //        case "btnSave":
        //            btnSave.Visible = true;
        //            btnSave.UpdateAfterCallBack = true;
        //            break;
        //        case "btnDelete":
        //            btnDelete.Visible = true;
        //            btnDelete.UpdateAfterCallBack = true;
        //            break;
        //        case "btnNew":
        //            btnNew.Visible = true;
        //            btnNew.UpdateAfterCallBack = true;
        //            break;
        //        case "btnList":
        //            btnList.Visible = true;
        //            btnList.UpdateAfterCallBack = true;
        //            break;
        //        case "btnPrint":
        //            btnPrint.Visible = true;
        //            btnPrint.UpdateAfterCallBack = true;
        //            break;
        //        case "btnEmail":
        //            btnEmail.Visible = true;
        //            btnEmail.UpdateAfterCallBack = true;
        //            break;
        //        case "btnPreview":
        //            //btnPreview.Visible = true;
        //            //btnPreview.UpdateAfterCallBack = true;
        //            break;
        //    }
        //}
         
        //obj = null;
    }

    public void New(Boolean visible)
    {
        btnNew.Visible = visible;
        btnNew.UpdateAfterCallBack = true;
    }

    public void Email(Boolean visible)
    {
        btnEmail.Visible = visible;
        btnEmail.UpdateAfterCallBack = true;
    }

    public void List(Boolean visible)
    {
        btnList.Visible = visible;
        btnList.UpdateAfterCallBack = true;
    }

    public void Delete(Boolean visible)
    {
        btnDelete.Visible = visible;
        btnDelete.UpdateAfterCallBack = true;
    }

    public void Print(Boolean visible)
    {
        btnPrint.Visible = visible;
        btnPrint.UpdateAfterCallBack = true;
    }

    public void Save(Boolean visible)
    {
        btnSave.Visible = visible;
        btnSave.UpdateAfterCallBack = true;
    }

    public void PrintValid()
    {
        btnPrint.Attributes.Add("onClick", "return validPrint();");
        btnPrint.UpdateAfterCallBack = true;
    }

    public void DeleteValid()
    {
        btnDelete.Attributes.Add("onClick", "return validDelete();");
        btnDelete.UpdateAfterCallBack = true;
    }

    public void ListPostBack()
    {
        btnList.Attributes.Add("onClick", "document.forms[0].submit();");
        btnList.UpdateAfterCallBack = true;
    }
}
