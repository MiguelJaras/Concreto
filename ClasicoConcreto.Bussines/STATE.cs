using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasicoConcreto.Entity;
using ClasicoConcreto.DataAccess;
namespace ClasicoConcreto.Bussines
{
    public class STATE
    {
        #region GetList
        public List<Entity_STATE> GetList()
        {
            return DACSTATE.GetList();
        }
        #endregion
    }
}
