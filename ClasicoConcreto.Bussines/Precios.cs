using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasicoConcreto.DataAccess;
using ClasicoConcreto.Entity;
using System.Data;
namespace ClasicoConcreto.Bussines
{
    public class Precios
    {
        #region GetList
        public DataTable GetList(int intEmpresa)
        {
            return DACPrecios.GetList(intEmpresa);
        }
        #endregion

        #region Save
        public void Save(Entity_Precios entProducto)
        {
            DACPrecios.Save(entProducto);
        }
        #endregion

        #region Delete
        public void Delete(int intEmpresa, string strInsumo, int intProducto)
        {
            DACPrecios.Delete(intEmpresa, strInsumo, intProducto);
        }
        #endregion

        #region GetInsumo
        public DataTable GetInsumo(int intEmpresa)
        {
            return DACPrecios.GetInsumo(intEmpresa);
        }
        # endregion
        
    }
}
