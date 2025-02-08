using System;
namespace ClasicoConcreto.Entity
{
    public class Entity_Empresas : EntityBaseClass
    {
        #region variables privadas

        private int _intEmpresa;
        private string _strNombre;
        private string _strNombreCorto;
        private int _intGrupo;
        private string _strDireccion;
        private string _strColonia;
        private string _strDelegacion;
        private int _intEstado;
        private int _intCiudad;
        private string _strRfc;
        private string _strRegImss;
        private string _strCodigoPostal;
        private string _strResponsable;
        private string _strRfcResponsable;
        private int _intTipoMoneda;
        private int _intLogo;
        private DateTime _datFechaAlta;
        private string _strUsuarioAlta;
        private string _strMaquinaAlta;
        private DateTime _datFechaMod;
        private string _strUsuarioMod;
        private string _strMaquinaMod;
        private decimal _dblInteresMoratorio;

        #endregion

        #region propiedades públicas

        public int intEmpresa
        {
            get { return _intEmpresa; }
            set { _intEmpresa = value; }
        }
        public string strNombre
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }
        public string strNombreCorto
        {
            get { return _strNombreCorto; }
            set { _strNombreCorto = value; }
        }
        public int intGrupo
        {
            get { return _intGrupo; }
            set { _intGrupo = value; }
        }
        public string strDireccion
        {
            get { return _strDireccion; }
            set { _strDireccion = value; }
        }
        public string strColonia
        {
            get { return _strColonia; }
            set { _strColonia = value; }
        }
        public string strDelegacion
        {
            get { return _strDelegacion; }
            set { _strDelegacion = value; }
        }
        public int intEstado
        {
            get { return _intEstado; }
            set { _intEstado = value; }
        }
        public int intCiudad
        {
            get { return _intCiudad; }
            set { _intCiudad = value; }
        }
        public string strRfc
        {
            get { return _strRfc; }
            set { _strRfc = value; }
        }
        public string strRegImss
        {
            get { return _strRegImss; }
            set { _strRegImss = value; }
        }
        public string strCodigoPostal
        {
            get { return _strCodigoPostal; }
            set { _strCodigoPostal = value; }
        }
        public string strResponsable
        {
            get { return _strResponsable; }
            set { _strResponsable = value; }
        }
        public string strRfcResponsable
        {
            get { return _strRfcResponsable; }
            set { _strRfcResponsable = value; }
        }
        public int intTipoMoneda
        {
            get { return _intTipoMoneda; }
            set { _intTipoMoneda = value; }
        }
        public int intLogo
        {
            get { return _intLogo; }
            set { _intLogo = value; }
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
        public DateTime datFechaMod
        {
            get { return _datFechaMod; }
            set { _datFechaMod = value; }
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
        public decimal dblInteresMoratorio
        {
            get { return _dblInteresMoratorio; }
            set { _dblInteresMoratorio = value; }
        }

        #endregion
    }
}
