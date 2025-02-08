using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasicoConcreto.Entity;
using ClasicoConcreto.DataAccess;
using System.Data;
namespace ClasicoConcreto.Bussines
{
    public class ITEM
    {
        #region GetItems
        public DataTable GetItems()
        {
            return DACITEM.GetItems();
        }
        #endregion
    }
}
