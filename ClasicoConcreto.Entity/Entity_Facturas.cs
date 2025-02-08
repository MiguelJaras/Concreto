using System;
namespace ClasicoConcreto.Entity
{
    [System.Serializable]
    public class Entity_Facturas : EntityBaseClass
    {
        

        #region variables privadas

        private int _intEmpresa;
        private string _strFactura;
        private string _strFolioFiscal;
        private decimal _dblImporte;
        private decimal _dblIva;
        private decimal _dblSubTotal;
        private decimal _dblRetencion;
        private string _strPDF;
        private string _strXML;
        private string _strRemision;
        private string _strMetodoPago;
        private DateTime _datFechaFactura;
        private string _strOCR;
        private int _intEstatus;
       
        private string _strEmisorRFC;
        private string _strReceptorRFC;
        private string _strPedidos;
        private string _strCliente;
        private string _strSerie;

        private decimal _dblDescuento;

        #endregion

        #region propiedades públicas

        public int intEmpresa
        {
            get { return _intEmpresa; }
            set { _intEmpresa = value; }
        }
        public string strFactura
        {
            get { return _strFactura; }
            set { _strFactura = value; }
        }
        public string strFolioFiscal
        {
            get { return _strFolioFiscal; }
            set { _strFolioFiscal = value; }
        }
        public decimal dblImporte
        {
            get { return _dblImporte; }
            set { _dblImporte = value; }
        }
        public decimal dblIva
        {
            get { return _dblIva; }
            set { _dblIva = value; }
        }
        public decimal dblSubTotal
        {
            get { return _dblSubTotal; }
            set { _dblSubTotal = value; }
        }
        public decimal dblRetencion
        {
            get { return _dblRetencion; }
            set { _dblRetencion = value; }
        }
        public string strPDF
        {
            get { return _strPDF; }
            set { _strPDF = value; }
        }
        public string strXML
        {
            get { return _strXML; }
            set { _strXML = value; }
        }
        public string strRemision
        {
            get { return _strRemision; }
            set { _strRemision = value; }
        }
        public string strMetodoPago
        {
            get { return _strMetodoPago; }
            set { _strMetodoPago = value; }
        }
        public DateTime datFechaFactura
        {
            get { return _datFechaFactura; }
            set { _datFechaFactura = value; }
        }
        public string strOCR
        {
            get { return _strOCR; }
            set { _strOCR = value; }
        }
        public int intEstatus
        {
            get { return _intEstatus; }
            set { _intEstatus = value; }
        }
        
        public string strEmisorRFC
        {
            get { return _strEmisorRFC; }
            set { _strEmisorRFC = value; }
        }

        public string strReceptorRFC
        {
            get { return _strReceptorRFC; }
            set { _strReceptorRFC = value; }
        }
        public string strPedidos
        {
            get { return _strPedidos; }
            set { _strPedidos = value; }
        }
        public string strCliente
        {
            get { return _strCliente; }
            set { _strCliente = value; }
        }
        public string strSerie
        {
            get { return _strSerie; }
            set { _strSerie = value; }
        }

        public decimal dblDescuento
        {
            get { return _dblDescuento; }
            set { _dblDescuento = value; }
        }


        #endregion

        public Entity_Facturas()
        {
            _intEmpresa = 0;
            _strFactura = "";
            _strFolioFiscal = "";
            _dblImporte = 0;
            _dblIva = 0;
            _dblSubTotal = 0;
            _dblRetencion = 0;
            _strPDF = "";
            _strXML = "";
            _strRemision = "";
            _strMetodoPago = "";
            _datFechaFactura = DateTime.Now;
            _strOCR = "";
            _intEstatus = 0;
            _dblDescuento = 0;
        }



    }
}
