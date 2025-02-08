using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess
{
    public class DACCITY : Base
    {

        #region GetList
        public static List<Entity_CITY> GetList()
        {
            IDataReader drd;
            List<Entity_CITY> lstRet = new List<Entity_CITY>();
            try
            {
                drd = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "usp_City_Sel");
                while(drd.Read())
                    lstRet.Add(CreateObject(drd));
            }
            catch (Exception e)
            {
                throw e;
            }

            return lstRet;
        }
        #endregion GetList



        #region Create
        static Entity_CITY CreateObject(IDataReader drd)
        {
            Entity_CITY oEnt = new Entity_CITY();
            oEnt.City = (string)drd["City"];
            oEnt.State_Code = (string)drd["State_Code"];
            return oEnt;
        }
        #endregion
    }
}
