using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasicoConcreto.Entity;
using ClasicoConcreto.DataAccess;
using System.Data;

namespace ClasicoConcreto.Bussines
{
    public class ListaPrecios
    {
        #region GetList
        public DataTable GetList()
        {
            return DACListaPrecios.GetList();
        }
        #endregion
    }
}
