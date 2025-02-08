using System;
namespace ClasicoConcreto.Entity
{
    public class Entity_CITY
    {
        #region variables privadas

        private string _City;
        private string _State_Code;
        private string _CreatedBy;
        private DateTime _CreateDate;

        #endregion

        #region propiedades públicas

        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        public string State_Code
        {
            get { return _State_Code; }
            set { _State_Code = value; }
        }
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        #endregion
    }
}
