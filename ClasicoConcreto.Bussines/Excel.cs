using System;
using System.Data;
using System.Collections;
using ClasicoConcreto.DataAccess;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.Bussines
{
    public class Excel
    {
        #region BindGrid
        public DataSet BindGrid(string query)
        {
            return DACExcel.BindGrid(query);
        }
        #endregion
    }
}
