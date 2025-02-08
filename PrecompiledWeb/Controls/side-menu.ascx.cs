using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Opciones_Controls_side_menu : System.Web.UI.UserControl
{
    public int IntCliente = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        IntCliente = ClasicoConcreto.SEMSession.GetInstance.IntCliente;
    }
}