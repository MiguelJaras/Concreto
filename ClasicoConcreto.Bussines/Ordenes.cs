using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasicoConcreto.Entity;
using ClasicoConcreto.DataAccess;
using System.Data;

namespace ClasicoConcreto.Bussines
{
    public class Ordenes
    {
        #region GetListPlantaOrden
        public DataTable GetListPlantaOrden(DateTime datFechaInicio, DateTime datFechaFin)
        {
            return DACOrdenes.GetListPlantaOrden(datFechaInicio, datFechaFin);
        }
        #endregion


        #region GetListPlantaOrdenExterna
        public DataTable GetListPlantaOrdenExterna(DateTime datFechaInicio, DateTime datFechaFin)
        {
            return DACOrdenes.GetListPlantaOrdenExterna(datFechaInicio, datFechaFin);
        }
        #endregion GetListPlantaOrdenExterna

        #region GetListPlantaOrdenRemisiones
        public DataTable GetListPlantaOrdenRemisiones(DateTime datFechaInicio, DateTime datFechaFin)
        {
            return DACOrdenes.GetListPlantaOrdenRemisiones(datFechaInicio, datFechaFin);
        }
        #endregion GetListPlantaOrdenExterna


        #region SavePlantaOrden
        public bool SavePlantaOrden(Entity_PlantaOrdenes ent)
        {
            return DACOrdenes.SavePlantaOrden(ent);
        }
        #endregion

        #region SavePlantaOrdenExterna
        public bool SavePlantaOrdenExterna(Entity_PlantaOrdenesExterna ent)
        {
            return DACOrdenes.SavePlantaOrdenExterna(ent);
        }
        #endregion

        #region GetOrdenesByPedido
        public DataTable GetOrdenesByPedido(int intPedido, int intCliente)
        {
            return DACOrdenes.GetOrdenesByPedido(intPedido, intCliente);
        }
        #endregion

        #region SavePlantaOrdenRemision
        public bool SavePlantaOrdenRemisiones(Entity_PlantaOrdenesRemisiones ent)
        {
            return DACOrdenes.SavePlantaOrdenRemision(ent);
        }
        #endregion
    }
}
