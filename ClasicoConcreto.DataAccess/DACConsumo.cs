using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess
{
    public class DACConsumo : Base
    {
        #region GetList
        public static DataTable GetList(DateTime datFechaInicio, DateTime datFechaFin)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[2];
          
         
                arrPar[0] = new SqlParameter("@datFechaInicio", SqlDbType.DateTime);
                arrPar[0].Value = datFechaInicio;
                arrPar[1] = new SqlParameter("@datFechaFin", SqlDbType.DateTime);
                arrPar[1].Value = datFechaFin;

                string strQuery = "usp_tbConsumoMateriales_sel";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch(Exception ex)
            {
                
            }
            return dtReturn;
        }
        #endregion GetList

        #region Save
        public static string Save(Entity_ConsumoMaterial ent)
        {
            string res = "";
            res = "false";

           SqlParameter[] arrPar = new SqlParameter[21];
           try
           {
                

            arrPar[0] = new SqlParameter("@intMaterial", SqlDbType.Int);
            arrPar[0].Value = ent.IntMaterial;

            arrPar[1] = new SqlParameter("@datFecha", SqlDbType.VarChar);
            //var dia = DateTime.ParseExact(ent.DatFecha.Month.ToString(), "dd", System.Globalization.CultureInfo.CurrentCulture).Day;
            //var mes = DateTime.ParseExact(ent.DatFecha.Day.ToString(), "MM", System.Globalization.CultureInfo.CurrentCulture).Month;
            //var año = ent.DatFecha.Year.ToString();

            arrPar[1].Value = ent.StrDatFecha; 

            arrPar[2] = new SqlParameter("@intPlanta", SqlDbType.Int);
            arrPar[2].Value = ent.IntPlanta;

            arrPar[3] = new SqlParameter("@decConcPremM3", SqlDbType.Decimal);
            arrPar[3].Value = ent.DecConcPremM3;

            arrPar[4] = new SqlParameter("@decAguaLto", SqlDbType.Decimal);
            arrPar[4].Value = ent.DecAguaLto;

            arrPar[5] = new SqlParameter("@decArena4Ton", SqlDbType.Decimal);
            arrPar[5].Value = ent.DecArena4Ton;

            arrPar[6] = new SqlParameter("@decCal", SqlDbType.Decimal);
            arrPar[6].Value = ent.DecCal;

            arrPar[7] = new SqlParameter("@decArena5Ton", SqlDbType.Decimal);
            arrPar[7].Value = ent.DecArena5Ton;

            arrPar[8] = new SqlParameter("@decCalFlow100", SqlDbType.Decimal);
            arrPar[8].Value = ent.DecCalFlow100;

            arrPar[9] = new SqlParameter("@decCampOx4060", SqlDbType.Decimal);
            arrPar[9].Value = ent.DecCampOx4060;

            arrPar[10] = new SqlParameter("@decCamcret", SqlDbType.Decimal);
            arrPar[10].Value = ent.DecCamcret;

            arrPar[11] = new SqlParameter("@decCamptard", SqlDbType.Decimal);
            arrPar[11].Value = ent.DecCamptard;

            arrPar[12] = new SqlParameter("@decCementoTon", SqlDbType.Decimal);
            arrPar[12].Value = ent.DecCementoTon;

            arrPar[13] = new SqlParameter("@decFibraBolsa", SqlDbType.Decimal);
            arrPar[13].Value = ent.DecFibraBolsa;

            arrPar[14] = new SqlParameter("@decColorByFerrox", SqlDbType.Decimal);
            arrPar[14].Value = ent.DecColorByFerrox;

            arrPar[15] = new SqlParameter("@decGrava1Ton", SqlDbType.Decimal);
            arrPar[15].Value = ent.DecGrava1Ton;

            arrPar[16] = new SqlParameter("@decGrava2Ton", SqlDbType.Decimal);
            arrPar[16].Value = ent.decGrava2Ton;
            
            arrPar[17] = new SqlParameter("@decImper", SqlDbType.Decimal);
            arrPar[17].Value = ent.DecImper;

            arrPar[18] = new SqlParameter("@decMortardELto", SqlDbType.Decimal);
            arrPar[18].Value = ent.DecMortardELto;

            arrPar[19] = new SqlParameter("@strUsuarioAlta", SqlDbType.VarChar);
            arrPar[19].Value = ent.strUsuarioAlta;

            arrPar[20] = new SqlParameter("@strMaquinaAlta", SqlDbType.VarChar);
            arrPar[20].Value = ent.strMaquinaAlta;

  
            string strQuery = "usp_tbConsumoMateriales_UpdateSave";
            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);
            res = "true";             

            }
            catch (Exception e)
            {
                throw e;
            }
            return res;
        }
        #endregion Save









    }
}
