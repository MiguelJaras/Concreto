using System;
namespace ClasicoConcreto.Entity
{
    public class Entity_Firma
    {
        #region variables privadas

        private string _name;
     

        #endregion

        #region propiedades públicas

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
       

        #endregion
    }
}
