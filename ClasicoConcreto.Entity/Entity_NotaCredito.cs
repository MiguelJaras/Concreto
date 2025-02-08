using System;
namespace ClasicoConcreto.Entity
{
    [System.Serializable]
    public class Entity_NotaCredito
    {
        #region variables privadas

        private int _intEmpresa;
        private string _strFolio;
        private string _strPedido;
        private string _strFolioFiscal;
        private decimal _dblImporte;
        private decimal _dblIva;
        private decimal _dblSubTotal;
        private string _strPDF;
        private string _strXML;
        private DateTime _datFecha;
        private string _strOCR;
        private int _intEstatus;
        private string _strMaquinaAlta;
        private DateTime _datFechaAlta;
        private string _strDescripcion;

        private string _strSerie;
        private string _strFactura;


        #endregion

        #region propiedades públicas

        public int intEmpresa
        {
            get { return _intEmpresa; }
            set { _intEmpresa = value; }
        }
        public string strFolio
        {
            get { return _strFolio; }
            set { _strFolio = value; }
        }
        public string strPedido
        {
            get { return _strPedido; }
            set { _strPedido = value; }
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
        public DateTime datFecha
        {
            get { return _datFecha; }
            set { _datFecha = value; }
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
        public string strMaquinaAlta
        {
            get { return _strMaquinaAlta; }
            set { _strMaquinaAlta = value; }
        }
        public DateTime datFechaAlta
        {
            get { return _datFechaAlta; }
            set { _datFechaAlta = value; }
        }
        public string strDescripcion
        {
            get { return _strDescripcion; }
            set { _strDescripcion = value; }
        }

        public string strSerie
        {
            get { return _strSerie; }
            set { _strSerie = value; }
        }
        public string strFactura
        {
            get { return _strFactura; }
            set { _strFactura = value; }
        }

        #endregion
    }
}
