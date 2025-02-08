using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess 
{
    public class DACExcel : Base
    {
        #region BindGrid
        public static DataSet BindGrid(string query)
        {
            DataSet ds;

            try
            {
                ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, query);
                return ds;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion
    }
}
