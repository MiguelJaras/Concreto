using System;
namespace ClasicoConcreto.Entity
{
    public class Entity_PlantaOrdenes
    {
        #region variables privadas

        private int _intId;
        private int _intPlanta;
        private int _intPlantaOrden;
        private int _intFolioOC;
        private string _strRemision;
        private DateTime _datFecha;
        private decimal _decCarga;
        private DateTime _datFechaAlta;
        private string _strMaquinaAlta;
        private string _strUsuarioAlta;
        private string _strCliente;
        private string _datFechaCarga;
        private string _strEstatus;
        private string _strProducto;
        #endregion

        #region propiedades públicas


        public string strProducto
        {
            get { return _strProducto; }
            set { _strProducto = value; }
        }


        public string strEstatus
        {
            get { return _strEstatus; }
            set { _strEstatus = value; }
        }



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
        public int intPlantaOrden
        {
            get { return _intPlantaOrden; }
            set { _intPlantaOrden = value; }
        }
        public int intFolioOC
        {
            get { return _intFolioOC; }
            set { _intFolioOC = value; }
        }
        public string strRemision
        {
            get { return _strRemision; }
            set { _strRemision = value; }
        }
        public DateTime datFecha
        {
            get { return _datFecha; }
            set { _datFecha = value; }
        }

        public string datFechaCarga
        {
            get { return _datFechaCarga; }
            set { _datFechaCarga = value; }
        }
        public decimal decCarga
        {
            get { return _decCarga; }
            set { _decCarga = value; }
        }
        public DateTime datFechaAlta
        {
            get { return _datFechaAlta; }
            set { _datFechaAlta = value; }
        }
        public string strMaquinaAlta
        {
            get { return _strMaquinaAlta; }
            set { _strMaquinaAlta = value; }
        }
        public string strUsuarioAlta
        {
            get { return _strUsuarioAlta; }
            set { _strUsuarioAlta = value; }
        }
        public string strCliente
        {
            get { return _strCliente; }
            set { _strCliente = value; }
        }
        #endregion
    }
}
