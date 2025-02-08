using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess
{
    public class DACPrecios : Base
    {
        #region GetList

        public static DataTable GetList(int intEmpresa)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[2];
                arrPar[0] = new SqlParameter("@intEmpresa", SqlDbType.Int);
                arrPar[0].Value = intEmpresa;
                string strQuery = "usp_tbPrecios_Sel";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetList

        #region Save
        public static void Save(Entity_Precios entPrecios)
        {
            try
            {
                SqlParameter[] arrPar = new SqlParameter[7];
                arrPar[0] = new SqlParameter("@intEmpresa", SqlDbType.Int);
                arrPar[0].Value = entPrecios.IntEmpresa;
                arrPar[1] = new SqlParameter("@strInsumo", SqlDbType.VarChar);
                arrPar[1].Value = entPrecios.StrInsumo;
                arrPar[2] = new SqlParameter("@intProducto", SqlDbType.Int);
                arrPar[2].Value = entPrecios.IntProducto;
                arrPar[3] = new SqlParameter("@dblPrecio", SqlDbType.Decimal);
                arrPar[3].Value = entPrecios.DblPrecio;
                arrPar[4] = new SqlParameter("@strUsuario", SqlDbType.VarChar);
                arrPar[4].Value = entPrecios.StrUsuario;
                arrPar[5] = new SqlParameter("@strMaquina", SqlDbType.VarChar);
                arrPar[5].Value = entPrecios.StrMaquina;
                arrPar[6] = new SqlParameter("@datFecha", SqlDbType.DateTime);
                arrPar[6].Value = entPrecios.DatFecha;

                string strQuery = "usp_tbPrecio_Save";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion Save

        


        #region Delete
        public static void Delete(int intEmpresa, string strInsumo, int intProducto)
        {
            try
            {
                SqlParameter[] arrPar = new SqlParameter[3];
                arrPar[0] = new SqlParameter("@intEmpresa", SqlDbType.Int);
                arrPar[0].Value = intEmpresa;
                arrPar[1] = new SqlParameter("@strInsumo", SqlDbType.VarChar);
                arrPar[1].Value = strInsumo;
                arrPar[2] = new SqlParameter("@intProducto", SqlDbType.Int);
                arrPar[2].Value = intProducto;
                string strQuery = "usp_tbPrecio_Del";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion Delete


        #region Create
        static Entity_Precios CreateObject(IDataReader drd)
        {
            Entity_Precios oEnt = new Entity_Precios();
            //oEnt.intProducto = (int)drd["intProducto"];
            //oEnt.strNombre = (string)drd["strNombre"];
            //oEnt.dblMenudeo = (decimal)drd["dblMenudeo"];
            //oEnt.dblMedioMayoreo = (decimal)drd["dblMedioMayoreo"];
            //oEnt.dblMayoreo = (decimal)drd["dblMayoreo"];
            return oEnt;
        }
        #endregion

        #region GetList

        public static DataTable GetInsumo(int intEmpresa)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[2];
                arrPar[0] = new SqlParameter("@intEmpresa", SqlDbType.Int);
                arrPar[0].Value = intEmpresa;
                string strQuery = "usp_tbArticulo_Sel";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetList
    }
}
