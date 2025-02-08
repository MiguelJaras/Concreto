using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using ClasicoConcreto.Entity;
using ClasicoConcreto.Bussines;

public partial class NuevoPassword : System.Web.UI.Page
    {
       private Entity_Clientes CurrentUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Proveedor.Text = Session["strEmail"].ToString();
            } 
                      
        }
         
       protected void btnAceptar_Click(object sender, EventArgs e)
       {
           try
           {
                Clientes ObjUser = new Clientes();
                Entity_Clientes usuario = new Entity_Clientes();

                usuario.intCliente = Convert.ToInt32(Session["intCliente"]);
                usuario.strPassword = password.Text;

                ObjUser.ChangePassword(usuario);
                ObjUser = null;

                Response.Redirect("Login.aspx");
           }
           catch (Exception ex)
           {
               ClientScript.RegisterStartupScript(Page.GetType(), "msf", "alert('" + ex.Message + "');", true);
           }
        }
            
   }

