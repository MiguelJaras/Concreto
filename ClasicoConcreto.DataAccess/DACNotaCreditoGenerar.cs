using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess
{
    public class DACNotaCreditoGenerar : Base
    {

        #region GetList
        public static DataTable GetList()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[1];
                arrPar[0] = new SqlParameter("@intEstatus", SqlDbType.Int);
                arrPar[0].Value = 0;

                string strQuery = "usp_tbNotaCreditoGenerar_List";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetList

        #region Get
        public static DataTable Get(int intNotaCredito)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[1];
                arrPar[0] = new SqlParameter("@intNotaCredito", SqlDbType.Int);
                arrPar[0].Value = intNotaCredito;
                string strQuery = "usp_tbNotaCreditoGenerar_Sel";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetList

        #region Save
        public static int Save(Entity_NotaCreditoGenerar entGenerar)
        {
            int intId = 0;
            try
            {
                SqlParameter[] arrPar = new SqlParameter[11];
                arrPar[0] = new SqlParameter("@intNotaCredito", SqlDbType.Int);
                arrPar[0].Value = entGenerar.intNotaCredito;
                arrPar[1] = new SqlParameter("@strSerieFactura", SqlDbType.VarChar);
                arrPar[1].Value = entGenerar.strSerieFactura;
                arrPar[2] = new SqlParameter("@decFolioFactura", SqlDbType.Decimal);
                arrPar[2].Value = entGenerar.decFolioFactura;
                arrPar[3] = new SqlParameter("@intEstatus", SqlDbType.Int);
                arrPar[3].Value = entGenerar.intEstatus;
                arrPar[4] = new SqlParameter("@strUsuarioAlta", SqlDbType.VarChar);
                arrPar[4].Value = entGenerar.strUsuarioAlta;
                arrPar[5] = new SqlParameter("@strMaquinaAlta", SqlDbType.VarChar);
                arrPar[5].Value = entGenerar.strMaquinaAlta;
                arrPar[6] = new SqlParameter("@strReferencia", SqlDbType.VarChar);
                arrPar[6].Value = entGenerar.strReferencia;
                arrPar[7] = new SqlParameter("@strCliente", SqlDbType.VarChar);
                arrPar[7].Value = entGenerar.strCliente;
                arrPar[8] = new SqlParameter("@decImporte", SqlDbType.VarChar);
                arrPar[8].Value = entGenerar.decImporte;
                arrPar[9] = new SqlParameter("@strFormaPago", SqlDbType.VarChar);
                arrPar[9].Value = entGenerar.strFormaPago;
                arrPar[10] = new SqlParameter("@strMetodopago", SqlDbType.VarChar);
                arrPar[10].Value = entGenerar.strMetodopago;

                string strQuery = "usp_tbNotaCreditoGenerar_Save";
                intId = int.Parse(SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intId;
        }
        #endregion Save

        #region SaveEstatus
        public static bool SaveEstatus(int intNotaCredito, int intEstatus)
        {
            bool bSave = false;
            try
            {
                SqlParameter[] arrPar = new SqlParameter[2];
                arrPar[0] = new SqlParameter("@intNotaCredito", SqlDbType.Int);
                arrPar[0].Value = intNotaCredito;
                arrPar[1] = new SqlParameter("@intEstatus", SqlDbType.Int);
                arrPar[1].Value = intEstatus;

                string strQuery = "usp_tbNotaCreditoGenerar_SaveEstatus";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);
                bSave = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bSave;
        }
        #endregion SaveEstatus


        #region Delete
        public static void Delete(int intNotaCredito)
        {
            try
            {
                SqlParameter[] arrPar = new SqlParameter[1];
                arrPar[0] = new SqlParameter("@intNotaCredito", SqlDbType.Int);
                arrPar[0].Value = intNotaCredito;
                string strQuery = "usp_tbNotaCreditoGenerar_Delete";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Delete

        #region Create
        static Entity_NotaCreditoGenerar CreateObject(IDataReader drd)
        {
            Entity_NotaCreditoGenerar oEnt = new Entity_NotaCreditoGenerar();
            oEnt.intNotaCredito = (int)drd["intNotaCredito"];
            oEnt.strSerieFactura = (string)drd["strSerieFactura"];
            oEnt.decFolioFactura = (decimal)drd["decFolioFactura"];
            oEnt.strSerie = (string)drd["strSerie"];
            oEnt.decFolio = (decimal)drd["decFolio"];
            oEnt.datFechaGen = (DateTime)drd["datFechaGen"];
            oEnt.intEstatus = (int)drd["intEstatus"];
            oEnt.datFechaAlta = (DateTime)drd["datFechaAlta"];
            oEnt.strUsuarioAlta = (string)drd["strUsuarioAlta"];
            oEnt.strMaquinaAlta = (string)drd["strMaquinaAlta"];
            oEnt.strReferencia = (string)drd["strReferencia"];
            oEnt.strCliente = (string)drd["strCliente"];
            oEnt.strError = (string)drd["strError"];
            oEnt.strUsoCFDI = (string)drd["strUsoCFDI"];
            oEnt.strFormaPago = (string)drd["strFormaPago"];
            oEnt.decImporte = (decimal)drd["decImporte"];
            oEnt.strMetodopago = (string)drd["strMetodopago"];
            return oEnt;
        }
        #endregion
    }
}
