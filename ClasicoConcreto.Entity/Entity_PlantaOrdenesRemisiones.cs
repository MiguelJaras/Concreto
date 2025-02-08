using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasicoConcreto.Entity
{
    public class Entity_PlantaOrdenesRemisiones : EntityBaseClass
    {
        #region variables privadas

        private int _intId;
        private int _intPlanta;
        private int _intFolio;
        private DateTime _datFecha;
        private string _strRemision;
        private decimal _decCantidad;
        private string _strStatus;

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
        public int intFolio
        {
            get { return _intFolio; }
            set { _intFolio = value; }
        }
        public decimal decCantidad
        {
            get { return _decCantidad; }
            set { _decCantidad = value; }
        }
        public string strStatus
        {
            get { return _strStatus; }
            set { _strStatus = value; }
        }
        #endregion
    }
}
