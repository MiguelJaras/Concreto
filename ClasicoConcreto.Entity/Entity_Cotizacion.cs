using System;
namespace ClasicoConcreto.Entity
{
    public class Entity_Cotizacion
    {
        #region variables privadas

        private int _intCotizacion;
        private string _strCliente;
        private string _strObra;
        private string _strElemento;
        private string _datFechaColado;
        private string _strTipoConcreto;
        private string _strResistencia;
        private int _intRevenimiento;
        private int _intAgregado;
        private int _intTipo;
        private string _strExtras;
        private string _strExtras2;
        private int _intTiro;
        private decimal _decPorcIva;
        private decimal _decSubTotal;
        private decimal _decIva;
        private decimal _decTotal;
        private int _intClienteAlta;
        private string _strMaquinAlta;

        #endregion

        #region propiedades públicas

        public int intCotizacion
        {
            get { return _intCotizacion; }
            set { _intCotizacion = value; }
        }
        public string strCliente
        {
            get { return _strCliente; }
            set { _strCliente = value; }
        }
        public string strObra
        {
            get { return _strObra; }
            set { _strObra = value; }
        }
        public string strElemento
        {
            get { return _strElemento; }
            set { _strElemento = value; }
        }
        public string datFechaColado
        {
            get { return _datFechaColado; }
            set { _datFechaColado = value; }
        }
        public string strTipoConcreto
        {
            get { return _strTipoConcreto; }
            set { _strTipoConcreto = value; }
        }
        public string strResistencia
        {
            get { return _strResistencia; }
            set { _strResistencia = value; }
        }
        public int intRevenimiento
        {
            get { return _intRevenimiento; }
            set { _intRevenimiento = value; }
        }
        public int intAgregado
        {
            get { return _intAgregado; }
            set { _intAgregado = value; }
        }
        public int intTipo
        {
            get { return _intTipo; }
            set { _intTipo = value; }
        }
        public string strExtras
        {
            get { return _strExtras; }
            set { _strExtras = value; }
        }
        public string strExtras2
        {
            get { return _strExtras2; }
            set { _strExtras2 = value; }
        }
        public int intTiro
        {
            get { return _intTiro; }
            set { _intTiro = value; }
        }
        public decimal decPorcIva
        {
            get { return _decPorcIva; }
            set { _decPorcIva = value; }
        }
        public decimal decSubTotal
        {
            get { return _decSubTotal; }
            set { _decSubTotal = value; }
        }
        public decimal decIva
        {
            get { return _decIva; }
            set { _decIva = value; }
        }
        public decimal decTotal
        {
            get { return _decTotal; }
            set { _decTotal = value; }
        }
        
        public int intClienteAlta
        {
            get { return _intClienteAlta; }
            set { _intClienteAlta = value; }
        }
        public string strMaquinAlta
        {
            get { return _strMaquinAlta; }
            set { _strMaquinAlta = value; }
        }

        #endregion
    }
}
