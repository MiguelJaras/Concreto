namespace ClasicoConcreto.Entity
{
    public class Entity_ListaPrecios
    {
        #region variables privadas

        private int _intLista;
        private string _strNombre;

        #endregion

        #region propiedades públicas

        public int intLista
        {
            get { return _intLista; }
            set { _intLista = value; }
        }
        public string strNombre
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }

        #endregion
    }
}
