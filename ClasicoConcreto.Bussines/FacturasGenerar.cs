using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasicoConcreto.Entity;
using ClasicoConcreto.DataAccess;
using System.Data;

namespace ClasicoConcreto.Bussines
{
    public class FacturasGenerar
    {
        #region Get
        public DataTable Get(int intFactura)
        {
            return DACFacturasGenerar.Get(intFactura);
        }
        #endregion

        #region GetList
        public DataTable GetList(int intEstatus, DateTime datFechaInicio, DateTime datFechaFin)
        {
            return DACFacturasGenerar.GetList(intEstatus, datFechaInicio, datFechaFin);
        }
        #endregion

        #region Save
        public int Save(Entity_FacturasGenerar entGenerar)
        {
            return DACFacturasGenerar.Save(entGenerar);
        }
        #endregion


        #region GetDetalle
        public DataTable GetDetalle(int intFactura)
        {
            return DACFacturasGenerar.GetDetalle(intFactura);
        }
        #endregion

        #region SaveEstatus
        public bool SaveEstatus(int intFactura, int intEstatus)
        {
            return DACFacturasGenerar.SaveEstatus(intFactura, intEstatus);
        }
        #endregion SaveEstatus


        #region Delete
        public void Delete(int intFactura)
        {
            DACFacturasGenerar.Delete(intFactura);
        }
        #endregion


    }
}
