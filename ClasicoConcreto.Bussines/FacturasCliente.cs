using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasicoConcreto.Entity;
using ClasicoConcreto.DataAccess;
using System.Data;

namespace ClasicoConcreto.Bussines
{
    public class FacturasCliente
    {

        #region Get
        public DataTable Get(int intCliente)
        {
            return DACFacturasCliente.Get(intCliente);
        }
        #endregion


        #region GetList
        public DataTable GetList(int intEstatus)
        {
            return DACFacturasCliente.GetList(intEstatus);
        }
        #endregion


        #region Save
        public int Save(Entity_FacturasCliente ent)
        {
            return DACFacturasCliente.Save(ent);
        }
        #endregion

        #region SaveEstatus
        public bool SaveEstatus(int intCliente, int intEstatus)
        {
            return DACFacturasCliente.SaveEstatus(intCliente, intEstatus);
        }
        #endregion
    }
}
