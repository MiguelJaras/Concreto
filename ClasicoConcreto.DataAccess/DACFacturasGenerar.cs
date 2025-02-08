using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess
{
    public class DACFacturasGenerar : Base
    {

        #region Get
        public static DataTable Get(int intFactura)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[7];
                arrPar[0] = new SqlParameter("@intFactura", SqlDbType.Int);
                arrPar[0].Value = intFactura;
                string strQuery = "usp_tbFacturasGenerar_Sel";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetList


        #region GetList
        public static DataTable GetList(int intEstatus, DateTime datFechaInicio, DateTime datFechaFin)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[3];
                arrPar[0] = new SqlParameter("@intEstatus", SqlDbType.Int);
                arrPar[0].Value = intEstatus;
                arrPar[1] = new SqlParameter("@datFechaInicio", SqlDbType.DateTime);
                arrPar[1].Value = datFechaInicio;
                arrPar[2] = new SqlParameter("@datFechaFin", SqlDbType.DateTime);
                arrPar[2].Value = datFechaFin;
                string strQuery = "usp_tbFacturasGenerar_ListByDate";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetList


        #region GetDetalle
        public static DataTable GetDetalle(int intFactura)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[1];
                arrPar[0] = new SqlParameter("@intFactura", SqlDbType.Int);
                arrPar[0].Value = intFactura;
                string strQuery = "usp_tbFacturasGenerar_SelPedidos";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetDetalle



        #region Save
        public static int Save(Entity_FacturasGenerar entGenerar)
        {
            int intId = 0;
            try
            {
                SqlParameter[] arrPar = new SqlParameter[11];
                arrPar[0] = new SqlParameter("@intFactura", SqlDbType.Int);
                arrPar[0].Value = entGenerar.intFactura;
                arrPar[1] = new SqlParameter("@strPedidos", SqlDbType.VarChar);
                arrPar[1].Value = entGenerar.strPedidos;
                arrPar[2] = new SqlParameter("@intEstatus", SqlDbType.VarChar);
                arrPar[2].Value = entGenerar.intEstatus;
                arrPar[3] = new SqlParameter("@strUsuarioAlta", SqlDbType.VarChar);
                arrPar[3].Value = entGenerar.strUsuarioAlta;
                arrPar[4] = new SqlParameter("@strMaquinaAlta", SqlDbType.VarChar);
                arrPar[4].Value = entGenerar.strMaquinaAlta;
                arrPar[5] = new SqlParameter("@strConcepto", SqlDbType.VarChar);
                arrPar[5].Value = entGenerar.strConcepto;
                arrPar[6] = new SqlParameter("@strCliente", SqlDbType.VarChar);
                arrPar[6].Value = entGenerar.strCliente;
                arrPar[7] = new SqlParameter("@strUsoCFDI", SqlDbType.VarChar);
                arrPar[7].Value = entGenerar.strUsoCFDI;
                arrPar[8] = new SqlParameter("@strFormaPago", SqlDbType.VarChar);
                arrPar[8].Value = entGenerar.strFormaPago;
               
                arrPar[9] = new SqlParameter("@decDescuento", SqlDbType.Decimal);
                arrPar[9].Value = entGenerar.decDescuento;

                arrPar[10] = new SqlParameter("@strMetodoPago", SqlDbType.VarChar);
                arrPar[10].Value = entGenerar.strMetodoPago;


                string strQuery = "usp_tbFacturasGenerar_Save";
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
        public static bool SaveEstatus(int intFactura, int intEstatus)
        {
            bool bSave = false;
            try
            {
                SqlParameter[] arrPar = new SqlParameter[2];
                arrPar[0] = new SqlParameter("@intFactura", SqlDbType.Int);
                arrPar[0].Value = intFactura;
                arrPar[1] = new SqlParameter("@intEstatus", SqlDbType.Int);
                arrPar[1].Value = intEstatus;

                string strQuery = "usp_tbFacturasGenerar_SaveEstatus";
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
        public static void Delete(int intFactura)
        {
            try
            {
                SqlParameter[] arrPar = new SqlParameter[1];
                arrPar[0] = new SqlParameter("@intFactura", SqlDbType.Int);
                arrPar[0].Value = intFactura;
                string strQuery = "usp_tbFacturasGenerar_Delete";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion Delete

        #region Create
        static Entity_FacturasGenerar CreateObject(IDataReader drd)
        {
            Entity_FacturasGenerar oEnt = new Entity_FacturasGenerar();
            oEnt.intFactura = (int)drd["intFactura"];
            oEnt.strPedidos = (string)drd["strPedidos"];
            oEnt.strSerie = (string)drd["strSerie"];
            oEnt.decFolio = (decimal)drd["decFolio"];
            oEnt.datFechaGen = (DateTime)drd["datFechaGen"];
            oEnt.intEstatus = (int)drd["intEstatus"];
            oEnt.datFechaAlta = (DateTime)drd["datFechaAlta"];
            oEnt.strUsuarioAlta = (string)drd["strUsuarioAlta"];
            oEnt.strMaquinaAlta = (string)drd["strMaquinaAlta"];
            oEnt.strConcepto = (string)drd["strConcepto"];
            oEnt.strCliente = (string)drd["strCliente"];
            return oEnt;
        }
        #endregion
    }
}
