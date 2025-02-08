using System;
namespace ClasicoConcreto.Entity
{
    public class Entity_ITEM
    {
        #region variables privadas

        private Guid _ItemID;
        private string _Item_Code;
        private string _Description;
        private string _Other_Code;
        private Guid _CompanyID;
        private string _Order_UOM;
        private string _Ext_Description;

        #endregion

        #region propiedades públicas

        public Guid ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        public string Item_Code
        {
            get { return _Item_Code; }
            set { _Item_Code = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public string Other_Code
        {
            get { return _Other_Code; }
            set { _Other_Code = value; }
        }
        public Guid CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        public string Order_UOM
        {
            get { return _Order_UOM; }
            set { _Order_UOM = value; }
        }
        public string Ext_Description
        {
            get { return _Ext_Description; }
            set { _Ext_Description = value; }
        }

        #endregion
    }
}
