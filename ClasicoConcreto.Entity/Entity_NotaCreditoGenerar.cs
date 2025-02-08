using System;
namespace ClasicoConcreto.Entity
{
    public class Entity_NotaCreditoGenerar
    {
        #region variables privadas

        private int _intNotaCredito;
        private string _strSerieFactura;
        private decimal _decFolioFactura;
        private string _strSerie;
        private decimal _decFolio;
        private DateTime _datFechaGen;
        private int _intEstatus;
        private DateTime _datFechaAlta;
        private string _strUsuarioAlta;
        private string _strMaquinaAlta;
        private string _strReferencia;
        private string _strCliente;
        private string _strError;
        private string _strUsoCFDI;
        private string _strFormaPago;
        private decimal _decImporte;
        private string _strMetodopago;

        #endregion

        #region propiedades públicas

        public int intNotaCredito
        {
            get { return _intNotaCredito; }
            set { _intNotaCredito = value; }
        }
        public string strSerieFactura
        {
            get { return _strSerieFactura; }
            set { _strSerieFactura = value; }
        }
        public decimal decFolioFactura
        {
            get { return _decFolioFactura; }
            set { _decFolioFactura = value; }
        }
        public string strSerie
        {
            get { return _strSerie; }
            set { _strSerie = value; }
        }
        public decimal decFolio
        {
            get { return _decFolio; }
            set { _decFolio = value; }
        }
        public DateTime datFechaGen
        {
            get { return _datFechaGen; }
            set { _datFechaGen = value; }
        }
        public int intEstatus
        {
            get { return _intEstatus; }
            set { _intEstatus = value; }
        }
        public DateTime datFechaAlta
        {
            get { return _datFechaAlta; }
            set { _datFechaAlta = value; }
        }
        public string strUsuarioAlta
        {
            get { return _strUsuarioAlta; }
            set { _strUsuarioAlta = value; }
        }
        public string strMaquinaAlta
        {
            get { return _strMaquinaAlta; }
            set { _strMaquinaAlta = value; }
        }
        public string strReferencia
        {
            get { return _strReferencia; }
            set { _strReferencia = value; }
        }
        public string strCliente
        {
            get { return _strCliente; }
            set { _strCliente = value; }
        }
        public string strError
        {
            get { return _strError; }
            set { _strError = value; }
        }
        public string strUsoCFDI
        {
            get { return _strUsoCFDI; }
            set { _strUsoCFDI = value; }
        }
        public string strFormaPago
        {
            get { return _strFormaPago; }
            set { _strFormaPago = value; }
        }
        public decimal decImporte
        {
            get { return _decImporte; }
            set { _decImporte = value; }
        }
        public string strMetodopago
        {
            get { return _strMetodopago; }
            set { _strMetodopago = value; }
        }

        #endregion
    }
}
