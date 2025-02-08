using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess
{
    public class DACProgramacionConcreto : Base
    {
        #region GetList
        public static DataTable GetList(DateTime datFecha, int intCliente)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[2];
                arrPar[0] = new SqlParameter("@datFecha", SqlDbType.DateTime);
                arrPar[0].Value = datFecha;
                arrPar[1] = new SqlParameter("@intCliente", SqlDbType.Int);
                arrPar[1].Value = intCliente;


                string strQuery = "vetecmarfil..usp_tbProgramacionConcreto_ListCliente";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {
                dtReturn = null;
            }
            return dtReturn;
        }
        #endregion GetList


        #region Save
        public static bool Save(int intRequisicionDet, string strHoraAnt, string strHora)
        {
            bool bReturn = true;
            try
            {
                SqlParameter[] arrPar = new SqlParameter[3];
                arrPar[0] = new SqlParameter("@intRequisicionDet", SqlDbType.Int);
                arrPar[0].Value = intRequisicionDet;
                arrPar[1] = new SqlParameter("@strHoraAnt", SqlDbType.VarChar);
                arrPar[1].Value = strHoraAnt;
                arrPar[2] = new SqlParameter("@strHora", SqlDbType.VarChar);
                arrPar[2].Value = strHora;


                string strQuery = "vetecmarfil..usp_tbProgramacionConcreto_SaveHoraEntrega";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);
            }
            catch
            {
                bReturn = false;
            }
            return bReturn;
        }
        #endregion Save

        #region SavePlantaRemision
        public static bool SavePlantaRemision(int intRequisicionDet, int intPlanta, string strRemision)
        {
            bool bReturn = true;
            try
            {
                SqlParameter[] arrPar = new SqlParameter[3];
                arrPar[0] = new SqlParameter("@intRequisicionDet", SqlDbType.Int);
                arrPar[0].Value = intRequisicionDet;
                arrPar[1] = new SqlParameter("@intPlanta", SqlDbType.Int);
                arrPar[1].Value = intPlanta;
                arrPar[2] = new SqlParameter("@strRemision", SqlDbType.VarChar);
                arrPar[2].Value = strRemision;


                string strQuery = "vetecmarfil..usp_tbProgramacionConcreto_SavePlantaRemision";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);
            }
            catch
            {
                bReturn = false;
            }
            return bReturn;
        }
        #endregion SavePlantaRemision

    }
}
