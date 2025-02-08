using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasicoConcreto.Entity;
using ClasicoConcreto.DataAccess;
using System.Data;

namespace ClasicoConcreto.Bussines
{
    public class NotaCredito
    {

        #region Get
        public DataTable Get(string strFolio)
        {
            return DACNotaCredito.Get(strFolio);
        }
        #endregion

        #region GetList
        public DataTable GetList()
        {
            return DACNotaCredito.GetList();
        }
        #endregion


        #region GetList
        public void Valida(Entity_NotaCredito entNotaCredito)
        {
            DACNotaCredito.Valida(entNotaCredito);
        }
        #endregion


        #region Delete
        public void Delete(string strFolio)
        {
            DACNotaCredito.Delete(strFolio);
        }
        #endregion

        #region Save
        public void Save(Entity_NotaCredito entNota)
        {
            DACNotaCredito.Save(entNota);
        }
        #endregion
    }
}
