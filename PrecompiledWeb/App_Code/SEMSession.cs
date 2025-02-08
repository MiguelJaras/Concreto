using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace ClasicoConcreto
{
    public class SEMSession
    {
        private const string InstaceName = "_SEMSession";

        private Int32 intCliente;
        private string strNombre;
        private string strEmail;
        private string strMaquina;
        private string strStateCode;
        private string strCity;
        private string strProject_Code;
        private Int32 intLista;
        private bool bPrecioEditable;
        private string strFirma;

        private SEMSession(String[] Values)
        {
            intCliente = Convert.ToInt32(Values[0]);
            strNombre = Values[1];
            strEmail = Values[2];
            strMaquina = Values[3];
            strStateCode = Values[5];
            strCity = Values[6];
            strProject_Code = Values[7];
            intLista = Convert.ToInt32(Values[8]);
            bPrecioEditable = Convert.ToBoolean(Values[9]);
            strFirma = Values[10];
        }

        public static SEMSession GetInstance
        {
            get
            {
                SEMSession SEMSession = (SEMSession)HttpContext.Current.Items[InstaceName];

                if (SEMSession == null)
                {
                    SEMSession = new SEMSession(HttpContext.Current.User.Identity.Name.Split('|'));
                    HttpContext.Current.Items.Add(InstaceName, SEMSession);
                }
                return SEMSession;
            }
        }

        public int IntCliente
        {
            get { return intCliente; }
            set { intCliente = value; }
        }

        public string StrNombre
        {
            get { return strNombre; }
            set { strNombre = value; }
        }
        
        public string StrFirma
        {
            get { return strFirma; }
            set { strFirma = value; }
        }
        public string StrEmail
        {
            get { return strEmail; }
            set { strEmail = value; }
        }
        
        public string StrMaquina
        {
            get { return strMaquina; }
            set { strMaquina = value; }
        }
        public string StrStateCode
        {
            get { return strStateCode; }
            set { strStateCode = value; }
        }
        public string StrCity
        {
            get { return strCity; }
            set { strCity = value; }
        }

        public string StrProject_Code
        {
            get { return strProject_Code; }
            set { strProject_Code = value; }
        }

        public int IntLista
        {
            get { return intLista; }
            set { intLista = value; }
        }
        public bool BPrecioEditable
        {
            get { return bPrecioEditable; }
            set { bPrecioEditable = value; }
        }
    }
}