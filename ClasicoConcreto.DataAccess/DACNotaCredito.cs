using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess
{
    public class DACNotaCredito : Base
    {

        #region GetList
        public static DataTable Get(string strFolio)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[1];
                arrPar[0] = new SqlParameter("@strFolio", SqlDbType.VarChar);
                arrPar[0].Value = strFolio;
                string strQuery = "usp_tbNotaCredito_Sel";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetList


        #region GetList
        public static DataTable GetList()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                string strQuery = "usp_tbNotaCredito_list";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetList


        #region Save
        public static void Save(Entity_NotaCredito entNotaCredito)
        {
            try
            {
                SqlParameter[] arrPar = new SqlParameter[16];
                arrPar[0] = new SqlParameter("@intEmpresa", SqlDbType.Int);
                arrPar[0].Value = entNotaCredito.intEmpresa;
                arrPar[1] = new SqlParameter("@strFolio", SqlDbType.VarChar);
                arrPar[1].Value = entNotaCredito.strFolio;
                arrPar[2] = new SqlParameter("@strPedido", SqlDbType.VarChar);
                arrPar[2].Value = entNotaCredito.strPedido;
                arrPar[3] = new SqlParameter("@strFolioFiscal", SqlDbType.VarChar);
                arrPar[3].Value = entNotaCredito.strFolioFiscal;
                arrPar[4] = new SqlParameter("@dblImporte", SqlDbType.Decimal);
                arrPar[4].Value = entNotaCredito.dblImporte;
                arrPar[5] = new SqlParameter("@dblIva", SqlDbType.Decimal);
                arrPar[5].Value = entNotaCredito.dblIva;
                arrPar[6] = new SqlParameter("@dblSubTotal", SqlDbType.Decimal);
                arrPar[6].Value = entNotaCredito.dblSubTotal;
                arrPar[7] = new SqlParameter("@strPDF", SqlDbType.VarChar);
                arrPar[7].Value = entNotaCredito.strPDF;
                arrPar[8] = new SqlParameter("@strXML", SqlDbType.VarChar);
                arrPar[8].Value = entNotaCredito.strXML;
                arrPar[9] = new SqlParameter("@datFecha", SqlDbType.DateTime);
                arrPar[9].Value = entNotaCredito.datFecha;
                arrPar[10] = new SqlParameter("@strOCR", SqlDbType.VarChar);
                arrPar[10].Value = entNotaCredito.strOCR;
                arrPar[11] = new SqlParameter("@intEstatus", SqlDbType.Int);
                arrPar[11].Value = entNotaCredito.intEstatus;
                arrPar[12] = new SqlParameter("@strMaquinaAlta", SqlDbType.VarChar);
                arrPar[12].Value = entNotaCredito.strMaquinaAlta;
                arrPar[13] = new SqlParameter("@strDescripcion", SqlDbType.VarChar);
                arrPar[13].Value = entNotaCredito.strDescripcion;
                arrPar[14] = new SqlParameter("@strSerie", SqlDbType.VarChar);
                arrPar[14].Value = entNotaCredito.strSerie;
                arrPar[15] = new SqlParameter("@strFactura", SqlDbType.VarChar);
                arrPar[15].Value = entNotaCredito.strFactura;


                string strQuery = "usp_tbNotaCredito_Save";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Save


        #region Valida
        public static void Valida(Entity_NotaCredito entNotaCredito)
        {
            try
            {
                SqlParameter[] arrPar = new SqlParameter[3];
                arrPar[0] = new SqlParameter("@strFolio", SqlDbType.VarChar);
                arrPar[0].Value = entNotaCredito.strFolio;
                arrPar[1] = new SqlParameter("@strSerie", SqlDbType.VarChar);
                arrPar[1].Value = entNotaCredito.strSerie;
                arrPar[2] = new SqlParameter("@strFactura", SqlDbType.VarChar);
                arrPar[2].Value = entNotaCredito.strFactura;
                string strQuery = "usp_tbNotaCredito_Valida";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Valida


        #region Delete
        public static void Delete(string strFolio)
        {
            try
            {
                SqlParameter[] arrPar = new SqlParameter[1];
                arrPar[0] = new SqlParameter("@strFolio", SqlDbType.VarChar);
                arrPar[0].Value = strFolio;

                string strQuery = "usp_tbNotaCredito_Del";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Delete


        #region Create
        static Entity_NotaCredito CreateObject(IDataReader drd)
        {
            Entity_NotaCredito oEnt = new Entity_NotaCredito();
            oEnt.intEmpresa = (int)drd["intEmpresa"];
            oEnt.strFolio = (string)drd["strFolio"];
            oEnt.strPedido = (string)drd["strPedido"];
            oEnt.strFolioFiscal = (string)drd["strFolioFiscal"];
            oEnt.dblImporte = (decimal)drd["dblImporte"];
            oEnt.dblIva = (decimal)drd["dblIva"];
            oEnt.dblSubTotal = (decimal)drd["dblSubTotal"];
            oEnt.strPDF = (string)drd["strPDF"];
            oEnt.strXML = (string)drd["strXML"];
            oEnt.datFecha = (DateTime)drd["datFecha"];
            oEnt.strOCR = (string)drd["strOCR"];
            oEnt.intEstatus = (int)drd["intEstatus"];
            oEnt.strMaquinaAlta = (string)drd["strMaquinaAlta"];
            oEnt.datFechaAlta = (DateTime)drd["datFechaAlta"];
            oEnt.strDescripcion = (string)drd["strDescripcion"];
            return oEnt;
        }
        #endregion
    }
}
