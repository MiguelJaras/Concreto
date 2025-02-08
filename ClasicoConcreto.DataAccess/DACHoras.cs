using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess
{
    public class DACHoras : Base
    {

        #region GetList
        public static List<Entity_Horas> GetList()
        {
            IDataReader drd;
            List<Entity_Horas> lstHoras = new List<Entity_Horas>();

            try
            {
                drd = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "usp_tbHoras_Sel");
                while (drd.Read())
                {
                    lstHoras.Add(CreateObject(drd));
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return lstHoras;
        }
        #endregion Login


        #region Create
        static Entity_Horas CreateObject(IDataReader drd)
        {
            Entity_Horas oEnt = new Entity_Horas();
            oEnt.strHora = (string)drd["strHora"];
            return oEnt;
        }
        #endregion
    }
}
