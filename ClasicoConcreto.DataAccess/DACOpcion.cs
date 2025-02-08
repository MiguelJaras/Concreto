using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasicoConcreto.Entity;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;

namespace ClasicoConcreto.DataAccess
{
    public class DACOpcion : Base
    {
        #region GetListByUser
        public static List<Entity_Opcion> GetListByUser(int intCliente)
        {
            List<Entity_Opcion> lstOpcion = new List<Entity_Opcion>();
            IDataReader drd;
            try
            {
                SqlParameter[] arrPar = new SqlParameter[1];
                arrPar[0] = new SqlParameter("@intCliente", SqlDbType.Int);
                arrPar[0].Value = intCliente;

                drd = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "usp_tbOpcionCliente_List", arrPar);
                while (drd.Read())
                {
                    lstOpcion.Add(CreateObject(drd));
                }
            }
            catch
            {

            }
            return lstOpcion;
        }

        #endregion

        #region Create
        static Entity_Opcion CreateObject(IDataReader drd)
        {
            Entity_Opcion oEntOpcion = new Entity_Opcion();
            oEntOpcion.IntOpcion = short.Parse(drd["intOpcion"].ToString()); ;
            oEntOpcion.StrNombre = drd["strNombre"].ToString();
            oEntOpcion.StrURL = drd["strURL"].ToString();
            oEntOpcion.BitActivo = bool.Parse(drd["bitActivo"].ToString());

            return oEntOpcion;
        }
        #endregion
    }
}
