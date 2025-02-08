using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasicoConcreto.Entity;
using ClasicoConcreto.DataAccess;
using System.Data;

namespace ClasicoConcreto.Bussines
{
    public class NotaCreditoGenerar
    {

        #region Get
        public DataTable Get(int intNotaCredito)
        {
            return DACNotaCreditoGenerar.Get(intNotaCredito);
        }
        #endregion

        #region GetList
        public DataTable GetList()
        {
            return DACNotaCreditoGenerar.GetList();
        }
        #endregion


        #region Save
        public int Save(Entity_NotaCreditoGenerar entGenerar)
        {
            return DACNotaCreditoGenerar.Save(entGenerar);
        }
        #endregion

        #region Delete
        public void Delete(int intNotaCredito)
        {
            DACNotaCreditoGenerar.Delete(intNotaCredito);
        }
        #endregion

        #region SaveEstatus
        public bool SaveEstatus(int intNotaCredito, int intEstatus)
        {
            return DACNotaCreditoGenerar.SaveEstatus(intNotaCredito, intEstatus);
        }
        #endregion
    }
}
