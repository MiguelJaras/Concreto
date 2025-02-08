using System;
namespace ClasicoConcreto.Entity
{
    public class Entity_Pedido
    {
        #region variables privadas

        private string _Project_Code;
        private int _intPedido;
        private string _PO_Num;
        private int _intTipoPrecio;
        private string _strCliente;
        private string _strEncargado;
        private string _strTelefonos;
        private string _strCalle;
        private string _strColonia;
        private string _strCalleEntre;
        private string _strCalleEntre2;
        private decimal _dblPorcentajeIva;
        private decimal _dblSubtotal;
        private decimal _dblIva;
        private decimal _dblTotal;
        private string _Order_Status;
        private string _State_Code;
        private string _City;
        private string _Postal_Code;
        private string _Delivery_Instructions;
        private int _intCliente;
        private string _strMaquina;
        private DateTime _datFechaAlta;
        private DateTime _datFechaEntrega;
        private string _strElemento;
        private string _strEmail;
        private string _strVendedor;
        private int _intFactura;
        private Decimal _dblPorcentaje;


        #endregion

        #region propiedades públicas


        public Decimal dblPorcentaje
        {
            get
            {
                return this._dblPorcentaje;
            }
            set
            {
                this._dblPorcentaje = value;
            }
        }




        public string Project_Code
        {
            get { return _Project_Code; }
            set { _Project_Code = value; }
        }
        public int intPedido
        {
            get { return _intPedido; }
            set { _intPedido = value; }
        }
        public string PO_Num
        {
            get { return _PO_Num; }
            set { _PO_Num = value; }
        }
        public int intTipoPrecio
        {
            get { return _intTipoPrecio; }
            set { _intTipoPrecio = value; }
        }
        public string strCliente
        {
            get { return _strCliente; }
            set { _strCliente = value; }
        }
        public string strEncargado
        {
            get { return _strEncargado; }
            set { _strEncargado = value; }
        }
        public string strTelefonos
        {
            get { return _strTelefonos; }
            set { _strTelefonos = value; }
        }
        public string strCalle
        {
            get { return _strCalle; }
            set { _strCalle = value; }
        }
        public string strColonia
        {
            get { return _strColonia; }
            set { _strColonia = value; }
        }
        public string strCalleEntre
        {
            get { return _strCalleEntre; }
            set { _strCalleEntre = value; }
        }
        public string strCalleEntre2
        {
            get { return _strCalleEntre2; }
            set { _strCalleEntre2 = value; }
        }
        public decimal dblPorcentajeIva
        {
            get { return _dblPorcentajeIva; }
            set { _dblPorcentajeIva = value; }
        }
        public decimal dblSubtotal
        {
            get { return _dblSubtotal; }
            set { _dblSubtotal = value; }
        }
        public decimal dblIva
        {
            get { return _dblIva; }
            set { _dblIva = value; }
        }
        public decimal dblTotal
        {
            get { return _dblTotal; }
            set { _dblTotal = value; }
        }
        public string Order_Status
        {
            get { return _Order_Status; }
            set { _Order_Status = value; }
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
        public string Postal_Code
        {
            get { return _Postal_Code; }
            set { _Postal_Code = value; }
        }
        public string Delivery_Instructions
        {
            get { return _Delivery_Instructions; }
            set { _Delivery_Instructions = value; }
        }
        public int intCliente
        {
            get { return _intCliente; }
            set { _intCliente = value; }
        }
        public string strMaquina
        {
            get { return _strMaquina; }
            set { _strMaquina = value; }
        }
        public DateTime datFechaAlta
        {
            get { return _datFechaAlta; }
            set { _datFechaAlta = value; }
        }
        public DateTime datFechaEntrega
        {
            get { return _datFechaEntrega; }
            set { _datFechaEntrega = value; }
        }

        public string strElemento
        {
            get { return _strElemento; }
            set { _strElemento = value; }
        }

        public string strEmail
        {
            get { return _strEmail; }
            set { _strEmail = value; }
        }
        public string strVendedor
        {
            get { return _strVendedor; }
            set { _strVendedor = value; }
        }

        public int intFactura
        {
            get { return _intFactura; }
            set { _intFactura = value; }
        }

        #endregion
    }
}
