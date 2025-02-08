using System;
namespace ClasicoConcreto.Entity
{
    public class Entity_Clientes : EntityBaseClass
    {
        #region variables privadas

        private int _intCliente;
        private string _strNombre;
        private string _strEmail;
        private string _strPassword;
        private string _Customer_Code;
        private string _State_Code;
        private string _City;
        private string _Project_Code;
        private int _intLista;
        private bool _bPrecioEditable;
        private int _intActivo;
        private string _strEmpresa;
        private string _strFirma;
        #endregion

        #region propiedades públicas

        public int intCliente
        {
            get { return _intCliente; }
            set { _intCliente = value; }
        }
        public string strNombre
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }
        public string strEmail
        {
            get { return _strEmail; }
            set { _strEmail = value; }
        }
        public string strPassword
        {
            get { return _strPassword; }
            set { _strPassword = value; }
        }
        public string Customer_Code
        {
            get { return _Customer_Code; }
            set { _Customer_Code = value; }
        }

        public string State_Code
        {
            get { return _State_Code; }
            set { _State_Code = value; }
        }

        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        public string Project_Code
        {
            get { return _Project_Code; }
            set { _Project_Code = value; }
        }

        public int intLista
        {
            get { return _intLista; }
            set { _intLista = value; }
        }
        public bool bPrecioEditable
        {
            get { return _bPrecioEditable; }
            set { _bPrecioEditable = value; }
        }

        public int intActivo
        {
            get { return _intActivo; }
            set { _intActivo = value; }
        }

        public string strEmpresa
        {
            get { return _strEmpresa; }
            set { _strEmpresa = value; }
        }


        public string strFirma
        {
            get { return _strFirma; }
            set { _strFirma = value; }
        }

        #endregion
    }
}
