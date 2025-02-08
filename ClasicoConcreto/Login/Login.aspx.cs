using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClasicoConcreto.Entity;
using ClasicoConcreto.Bussines;
using System.Web.Security;
using Newtonsoft.Json;

public partial class Pages_Test_Login : System.Web.UI.Page
{
    private Entity_Clientes CurrentUser;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        try
        {
            CurrentUser = GetUser();
            if (CurrentUser != null)
            {
                Session["intCliente"] = CurrentUser.intCliente;
                Session["strEmail"] = CurrentUser.strEmail;

                if (CurrentUser.IntParametroInicial == 1)
                {
                    //Cambiar password                   
                    Response.Redirect("NuevoPassword.aspx");
                }
                else
                {
                    String arrUser = "{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}";
                    arrUser = String.Format(arrUser, CurrentUser.intCliente, CurrentUser.strNombre, CurrentUser.strEmail, Request.ServerVariables["REMOTE_HOST"], CurrentUser.Customer_Code, CurrentUser.State_Code, CurrentUser.City, CurrentUser.Project_Code, CurrentUser.intLista, CurrentUser.bPrecioEditable, CurrentUser.strFirma);
                    FormsAuthentication.RedirectFromLoginPage(arrUser, false);
                    string url = FormsAuthentication.GetRedirectUrl(arrUser, false);

                    //Se guardan permisos de usuario en sesion
                    List<Entity_Opcion> lstOpcion = new List<Entity_Opcion>();
                    Opcion objOpcion = new Opcion();
                    lstOpcion = objOpcion.GetListByUser(CurrentUser.intCliente);
                    string strJSON = JsonConvert.SerializeObject(lstOpcion);
                    Session["lstPermisos"] = strJSON;
                    Response.Redirect(FormsAuthentication.DefaultUrl + (url != FormsAuthentication.DefaultUrl ? "?login=true&ReturnUrl=" + url : "?login=true"), false);
                }
            }
            else
            {
                lblMessage.Text = "Usuario Invalido, favor de intentarlo nuevamente.";
                error.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "msgFa", "alert('" + ex.Message + "');", true);
        }
    }


    #region GetUser
    private Entity_Clientes GetUser()
    {
        Clientes ObjUser = new Clientes();
        Entity_Clientes usuario = new Entity_Clientes();
        usuario.strEmail = username.Text;
        usuario.strPassword = password.Text;
        usuario.strMaquinaAlta = Request.ServerVariables["REMOTE_HOST"].ToString();
        usuario = ObjUser.Login(usuario);
        ObjUser = null;

        return usuario;
    }
    #endregion         


}