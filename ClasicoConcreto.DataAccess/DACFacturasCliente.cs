using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess
{
    public class DACFacturasCliente : Base
    {


        #region GetList
        public static DataTable GetList(int intEstatus)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[1];
                arrPar[0] = new SqlParameter("@intEstatus", SqlDbType.Int);
                arrPar[0].Value = intEstatus;
                string strQuery = "usp_tbFacturasCliente_List";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetList


        #region Get
        public static DataTable Get(int intCliente)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[1];
                arrPar[0] = new SqlParameter("@intCliente", SqlDbType.Int);
                arrPar[0].Value = intCliente;
                string strQuery = "usp_tbFacturasCliente_Sel";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetList


        #region Save
        public static int Save(Entity_FacturasCliente entCliente)
        {
            int intId = 0;
            try
            {
                SqlParameter[] arrPar = new SqlParameter[18];
                arrPar[0] = new SqlParameter("@intCliente", SqlDbType.Int);
                arrPar[0].Value = entCliente.intCliente;
                arrPar[1] = new SqlParameter("@strCodigo", SqlDbType.VarChar);
                arrPar[1].Value = entCliente.strCodigo;
                arrPar[2] = new SqlParameter("@strNombre", SqlDbType.VarChar);
                arrPar[2].Value = entCliente.strNombre;
                arrPar[3] = new SqlParameter("@strRFC", SqlDbType.VarChar);
                arrPar[3].Value = entCliente.strRFC;
                arrPar[4] = new SqlParameter("@strPais", SqlDbType.VarChar);
                arrPar[4].Value = entCliente.strPais;
                arrPar[5] = new SqlParameter("@strEstado", SqlDbType.VarChar);
                arrPar[5].Value = entCliente.strEstado;
                arrPar[6] = new SqlParameter("@strCiudad", SqlDbType.VarChar);
                arrPar[6].Value = entCliente.strCiudad;
                arrPar[7] = new SqlParameter("@strColonia", SqlDbType.VarChar);
                arrPar[7].Value = entCliente.strColonia;
                arrPar[8] = new SqlParameter("@strCalle", SqlDbType.VarChar);
                arrPar[8].Value = entCliente.strCalle;
                arrPar[9] = new SqlParameter("@strNumExt", SqlDbType.VarChar);
                arrPar[9].Value = entCliente.strNumExt;
                arrPar[10] = new SqlParameter("@strNumInt", SqlDbType.VarChar);
                arrPar[10].Value = entCliente.strNumInt;
                arrPar[11] = new SqlParameter("@strCodigoPostal", SqlDbType.VarChar);
                arrPar[11].Value = entCliente.strCodigoPostal;
                arrPar[12] = new SqlParameter("@strTelefono", SqlDbType.VarChar);
                arrPar[12].Value = entCliente.strTelefono;
                arrPar[13] = new SqlParameter("@strUsuarioAlta", SqlDbType.VarChar);
                arrPar[13].Value = entCliente.strUsuarioAlta;
                arrPar[14] = new SqlParameter("@strMaquinaAlta", SqlDbType.VarChar);
                arrPar[14].Value = entCliente.strMaquinaAlta;
                arrPar[15] = new SqlParameter("@strUsoCFDI", SqlDbType.VarChar);
                arrPar[15].Value = entCliente.strUsoCFDI;
                arrPar[16] = new SqlParameter("@strFormaPago", SqlDbType.VarChar);
                arrPar[16].Value = entCliente.strFormaPago;
                arrPar[17] = new SqlParameter("@strEmail", SqlDbType.VarChar);
                arrPar[17].Value = entCliente.strEmail;

                string strQuery = "usp_tbFacturasCliente_Save";
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
        public static bool SaveEstatus(int intCliente, int intEstatus)
        {
            bool bSave = false;
            try
            {
                SqlParameter[] arrPar = new SqlParameter[2];
                arrPar[0] = new SqlParameter("@intCliente", SqlDbType.Int);
                arrPar[0].Value = intCliente;
                arrPar[1] = new SqlParameter("@intEstatus", SqlDbType.Int);
                arrPar[1].Value = intEstatus;

                string strQuery = "usp_tbFacturasCliente_SaveEstatus";
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


    }
}
