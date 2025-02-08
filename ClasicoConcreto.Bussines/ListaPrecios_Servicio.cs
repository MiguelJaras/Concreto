using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasicoConcreto.DataAccess;
using ClasicoConcreto.Entity;
using System.Data;
namespace ClasicoConcreto.Bussines
{
    public class ListaPrecios_Servicio
    {
        #region GetList
        public DataTable GetList(int intLista)
        {
            return DACListaPrecios_Servicio.GetList(intLista);
        }
        #endregion

        #region Save
        public void Save(Entity_ListaPrecios_Servicio entListaPrecios)
        {
            DACListaPrecios_Servicio.Save(entListaPrecios);
        }
        #endregion

    }
}
