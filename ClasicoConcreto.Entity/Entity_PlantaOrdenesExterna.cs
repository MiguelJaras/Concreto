using System;
namespace ClasicoConcreto.Entity
{
    public class Entity_PlantaOrdenesExterna : EntityBaseClass
    {
        #region variables privadas

        private int _intId;
        private int _intPlanta;
        private DateTime _datFecha;
        private string _strRemision;
        private string _strCliente;
        private string _strResistencia;
        private decimal _decCantidad;
        private decimal _decBombeable;
        private decimal _decPrecioVenta;
        private decimal _decPorcIva;
        private decimal _decPrecioBase;
        private string _strClaveResistencia;

        private decimal _decRemisonado;
        #endregion

        #region propiedades públicas

        public int intId
        {
            get { return _intId; }
            set { _intId = value; }
        }
        public int intPlanta
        {
            get { return _intPlanta; }
            set { _intPlanta = value; }
        }
        public DateTime datFecha
        {
            get { return _datFecha; }
            set { _datFecha = value; }
        }
        public string strRemision
        {
            get { return _strRemision; }
            set { _strRemision = value; }
        }
        public string strCliente
        {
            get { return _strCliente; }
            set { _strCliente = value; }
        }
        public string strResistencia
        {
            get { return _strResistencia; }
            set { _strResistencia = value; }
        }
        public decimal decCantidad
        {
            get { return _decCantidad; }
            set { _decCantidad = value; }
        }
        public decimal decBombeable
        {
            get { return _decBombeable; }
            set { _decBombeable = value; }
        }
        public decimal decPrecioVenta
        {
            get { return _decPrecioVenta; }
            set { _decPrecioVenta = value; }
        }
        public decimal decPorcIva
        {
            get { return _decPorcIva; }
            set { _decPorcIva = value; }
        }
        public decimal decPrecioBase
        {
            get { return _decPrecioBase; }
            set { _decPrecioBase = value; }
        }

        public string strClaveResistencia
        {
            get { return _strClaveResistencia; }
            set { _strClaveResistencia = value; }
        }
        public decimal decRemisonado
        {
            get { return _decRemisonado; }
            set { _decRemisonado = value; }
        }


        #endregion
    }
}
