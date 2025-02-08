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

public partial class EnvioPassword : System.Web.UI.Page
    {
       private Entity_Clientes CurrentUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            } 
                      
        }
         
       protected void btnAceptar_Click(object sender, EventArgs e)
       {
           //try
           //{
           //    Entity_Clientes usuario;
           //    usuario = new Entity_Clientes();

           //    usuario.StrEmail = Email.Text;

           //    Proveedores ObjUser;
           //    ObjUser = new Proveedores();

           //    usuario = ObjUser.FillByEmail(usuario);

           //    ObjUser = null;

           //    Email email;
           //    email = new Email();

           //    email.Password(usuario.StrNombre, usuario.StrEmail, usuario.StrPassword);
               
           //    ClientScript.RegisterStartupScript(Page.GetType(), "msgAcept", "alert('Se acaba de enviar el password al correo " + usuario.StrEmail + ".');", true);
           //    Response.Redirect("Login.aspx");

           //    email = null;

           //}
           //catch (Exception ex)
           //{
           //    ClientScript.RegisterStartupScript(Page.GetType(), "msf", "alert('" + ex.Message + "');", true);
           //}
        }

       protected void btnReturn_Click(object sender, EventArgs e)
       {
           Response.Redirect("Login.aspx");
       }       
    
            
   }

