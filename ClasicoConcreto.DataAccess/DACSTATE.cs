using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess
{
    public class DACSTATE : Base
    {

        #region GetList
        public static List<Entity_STATE> GetList()
        {
            IDataReader drd;
            List<Entity_STATE> lstRet = new List<Entity_STATE>();
            try
            {
                drd = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "usp_STATE_Sel");
                while (drd.Read())
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
        static Entity_STATE CreateObject(IDataReader drd)
        {
            Entity_STATE oEnt = new Entity_STATE();
            oEnt.State_Code = (string)drd["State_Code"];
            oEnt.Country_Code = (string)drd["Country_Code"];
            oEnt.CreatedBy = (string)drd["CreatedBy"];
            oEnt.CreateDate = (DateTime)drd["CreateDate"];
            return oEnt;
        }
        #endregion
    }
}
