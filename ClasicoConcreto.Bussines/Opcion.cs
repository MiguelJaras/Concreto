using System;
using System.Collections.Generic;
using ClasicoConcreto.DataAccess;
using ClasicoConcreto.Entity;
using System.Linq;

namespace ClasicoConcreto.Bussines
{
    public class Opcion
    {
        #region Opcion
        public List<Entity_Opcion> GetListByUser(int intCliente)
        {
            return DACOpcion.GetListByUser(intCliente);
        }
        #endregion
    }
}
