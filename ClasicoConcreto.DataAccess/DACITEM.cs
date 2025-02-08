using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess
{
    public class DACITEM : Base
    {

        
        #region GetItems
        public static DataTable GetItems()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[4];
                string strQuery = "usp_ITEM_Sel";
                dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery).Tables[0];
            }
            catch
            {

            }

            return dt;
        }
        #endregion GetItems



        #region Create
        static Entity_ITEM CreateObject(IDataReader drd)
        {
            Entity_ITEM oEnt = new Entity_ITEM();
            oEnt.ItemID = (Guid)drd["ItemID"];
            oEnt.Item_Code = (string)drd["Item_Code"];
            oEnt.Description = (string)drd["Description"];
            oEnt.Other_Code = (string)drd["Other_Code"];
            oEnt.CompanyID = (Guid)drd["CompanyID"];
            oEnt.Order_UOM = (string)drd["Order_UOM"];
            oEnt.Ext_Description = (string)drd["Ext_Description"];
            return oEnt;
        }
        #endregion
    }
}
