using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasicoConcreto.Entity
{
    [System.Serializable]
    public class EntityBaseClass
    {
        private int _intParametroInicial;
        private string _strUsuarioAlta;
        private string _strMaquinaAlta;
        private DateTime _datFechaAlta;

        private string _strUsuarioMod;
        private string _strMaquinaMod;
        private DateTime _datFechaAltaMod;


        
        public int IntParametroInicial
        {
            get { return _intParametroInicial; }
            set { _intParametroInicial = value; }
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
        public DateTime datFechaAlta
        {
            get { return _datFechaAlta; }
            set { _datFechaAlta = value; }
        }

        public string strUsuarioMod
        {
            get { return _strUsuarioMod; }
            set { _strUsuarioMod = value; }
        }
        public string strMaquinaMod
        {
            get { return _strMaquinaMod; }
            set { _strMaquinaMod = value; }
        }
        public DateTime datFechaAltaMod
        {
            get { return _datFechaAltaMod; }
            set { _datFechaAltaMod = value; }
        }


    }
}
