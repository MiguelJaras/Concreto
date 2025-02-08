using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess
{
    public class DACListaPrecios : Base
    {
        #region GetList
        public static DataTable GetList()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                string strQuery = "usp_tbListaPrecios_Sel";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery).Tables[0];
            }
            catch
            { ; }
            return dtReturn;
        }
        #endregion GetList

        #region Create
        static Entity_ListaPrecios CreateObject(IDataReader drd)
        {
            Entity_ListaPrecios oEnt = new Entity_ListaPrecios();
            oEnt.intLista = (int)drd["intLista"];
            oEnt.strNombre = (string)drd["strNombre"];
            return oEnt;
        }
        #endregion
    }
}
