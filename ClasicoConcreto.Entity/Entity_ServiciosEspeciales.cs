namespace ClasicoConcreto.Entity
{
    public class Entity_ServiciosEspeciales : EntityBaseClass
    {
        #region variables privadas

        private int _intServicio;
        private string _strNombre;
        private decimal _dblPrecio;
        private int _intGrupo;
        #endregion

        #region propiedades públicas

        public int intServicio
        {
            get { return _intServicio; }
            set { _intServicio = value; }
        }
        public string strNombre
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }
        public decimal dblPrecio
        {
            get { return _dblPrecio; }
            set { _dblPrecio = value; }
        }
        public int intGrupo
        {
            get { return _intGrupo; }
            set { _intGrupo = value; }
        }
        #endregion
    }
}
