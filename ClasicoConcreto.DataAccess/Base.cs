using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Xml;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace ClasicoConcreto.DataAccess
{
    public abstract class Base 
    {            

        private string _ConnectionString = "";
        SqlConnection objConnection;
        private SqlDataAdapter objAdapter;

        protected Base()
        {
        }

        public static string ConnectionString
        {
            get
            {
                //return @"Data Source=192.168.80.154\DEVSQLSERRRIOS;Initial Catalog=dbClasicoConcreto;User ID=ClasicoConcreto;password=ClasicoConcreto2014;";
                return "Data Source=192.168.80.5;Initial Catalog=dbConcreto;User ID=sa;password=M1rf3l";
                //return "Data Source=RUBEN-PC;Initial Catalog=VetecMarfil;User ID=sa;password=mora";
            }
        }

        public virtual string QueryHelp(int intEmpresa, int intSucursal, string[] parametros, int version)
        {
            string a = "";
            return a;
        }

        public virtual DataSet QueryHelpData(int intEmpresa, int intSucursal, string[] parametros, int version)
        {
            return new DataSet();
        }

        public void CloseIfOpen()
        {
            if (ConnectionState.Open == objConnection.State)
                objConnection.Close();
        }

        public XmlDocument QueryXML(string strQuery)
        {
            try
            {
                XmlDocument objXML;
                SqlConnection objConnection = new SqlConnection(ConnectionString);
                objConnection.Open();
                System.Data.SqlClient.SqlDataAdapter objAdapter = new System.Data.SqlClient.SqlDataAdapter(strQuery, objConnection);
                DataSet objDataSet = new System.Data.DataSet();
                objAdapter.Fill(objDataSet);
                objXML = new XmlDocument();
                objXML.InnerXml = objDataSet.GetXml();
                objConnection.Close();
                return objXML;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CloseIfOpen();
            }
        }


        #region ExecuteQuery
        public static DataSet ExecuteQuery(string strQuery)
        {
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strQuery))
                {
                    return ds;
                }
            }
            catch
            {
                return null;
            }
        }        
        #endregion

        #region ExecuteStored
        public static DataSet ExecuteStored(string strStoredName, SqlParameter[] arrParDet)
        {
            try
            {
                return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strStoredName, arrParDet);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        public SqlDataReader QueryDR(string strQuery)
        {
            try
            {
                return SqlHelper.ExecuteReader(ConnectionString, CommandType.Text, strQuery);
            }
            catch (Exception e)
            {
                throw e;
            }

            return null;
        }

        public static string QueryEscalar(string strQuery)
        {
            try
            {
                return SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strQuery).ToString();
            }
            catch (Exception e)
            {
                throw e;
            }

            return null;
        }


    }
}
