using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasicoConcreto.DataAccess;
using ClasicoConcreto.Entity;
using System.Data;
namespace ClasicoConcreto.Bussines
{
    public class Producto
    {
        #region GetList
        public DataTable GetList()
        {
            return DACProducto.GetList();
        }
        #endregion

        #region GetList
        public DataTable GetListPorcentaje()
        {
            return DACProducto.GetListPorcentaje();
        }
        #endregion


        #region GetListActivos
        public List<Entity_Producto> GetListActivos(int intLista, decimal dblPorcentaje)
        {
            return DACProducto.GetListActivos(intLista, dblPorcentaje);
        }
        #endregion


        #region GetListCotizacion
        public DataTable GetListCotizacion(int intLista)
        {
            return DACProducto.GetListCotizacion(intLista);
        }
        #endregion


        #region Save
        public void Save(Entity_Producto entPrecios)
        {
            DACProducto.Save(entPrecios);
        }
        #endregion

        #region Delete
        public void Delete(int intProducto)
        {
            DACProducto.Delete(intProducto);
        }
        #endregion
    }
}
