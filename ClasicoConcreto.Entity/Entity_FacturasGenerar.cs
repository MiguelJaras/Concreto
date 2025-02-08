using System;
namespace ClasicoConcreto.Entity
{
    public class Entity_FacturasGenerar : EntityBaseClass
    {
        #region variables privadas

        private int _intFactura;
        private string _strPedidos;
        private string _strSerie;
        private decimal _decFolio;
        private DateTime _datFechaGen;
        private int _intEstatus;
        private string _strConcepto;
        private string _strCliente;
        private string _strUsoCFDI;
        private string _strFormaPago;
        private decimal _decDescuento;
        private string _strMetodoPago;

        #endregion

        #region propiedades públicas

        public int intFactura
        {
            get { return _intFactura; }
            set { _intFactura = value; }
        }
        public string strPedidos
        {
            get { return _strPedidos; }
            set { _strPedidos = value; }
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
        public string strConcepto
        {
            get { return _strConcepto; }
            set { _strConcepto = value; }
        }
        public string strCliente
        {
            get { return _strCliente; }
            set { _strCliente = value; }
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
        public decimal decDescuento
        {
            get { return _decDescuento; }
            set { _decDescuento = value; }
        }

        public string strMetodoPago
        {
            get { return _strMetodoPago; }
            set { _strMetodoPago = value; }
        }

        #endregion
    }
}
