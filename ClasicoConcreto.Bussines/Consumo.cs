using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasicoConcreto.DataAccess;
using ClasicoConcreto.Entity;
using System.Data;
namespace ClasicoConcreto.Bussines
{
    public class Consumo
    {

        #region GetList
        public DataTable GetList(DateTime datFechaInicio, DateTime datFechaFin)
        {
            return DACConsumo.GetList(datFechaInicio, datFechaFin);
        }
        #endregion


        #region Save
        public string Save(Entity_ConsumoMaterial obj)
        {
            return DACConsumo.Save(obj);
        }
        #endregion





    }
}
