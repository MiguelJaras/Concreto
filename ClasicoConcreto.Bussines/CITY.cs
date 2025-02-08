using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasicoConcreto.Entity;
using ClasicoConcreto.DataAccess;
namespace ClasicoConcreto.Bussines
{
    public class CITY
    {
        #region GetList
        public List<Entity_CITY> GetList()
        {
            return DACCITY.GetList();
        }
        #endregion
    }
}
