using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess
{
    public class DACSignature: Base
    {

        #region GetReportSignature
        public static DataSet GetReportSignature()
        {
            DataSet numFirm = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand comandoSql = new SqlCommand("usp_NumeroDeFirma", connection);
                comandoSql.CommandType = CommandType.StoredProcedure;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(comandoSql);
                    da.Fill(numFirm);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data signature " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return numFirm;
        }
        #endregion GetReportSignature


    }
}
