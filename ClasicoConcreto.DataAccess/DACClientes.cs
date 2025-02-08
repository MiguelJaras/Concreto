using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess
{
    public class DACClientes : Base
    {

        #region Login
        public static Entity_Clientes Login(Entity_Clientes obj)
        {
            IDataReader drd;
            Entity_Clientes entCliente = new Entity_Clientes();

            SqlParameter[] arrPar = new SqlParameter[3];
            arrPar[0] = new SqlParameter("@strEmail", SqlDbType.VarChar);
            arrPar[0].Value = obj.strEmail;
            arrPar[1] = new SqlParameter("@strPassword", SqlDbType.VarChar);
            arrPar[1].Value = obj.strPassword;
            arrPar[2] = new SqlParameter("@strMaquina", SqlDbType.VarChar);
            arrPar[2].Value = obj.strMaquinaAlta;

            try
            {
                drd = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "usp_tbClientes_Login", arrPar);
                if (drd.Read())
                {
                    entCliente = CreateObject(drd);
                }
                else
                    entCliente = null;
            }
            catch (Exception e)
            {
                throw e;
            }

            return entCliente;
        }
        #endregion Login


        #region ChangePassword
        public static bool ChangePassword(Entity_Clientes obj)
        {
            bool state;
            Entity_Clientes oEntity_Proveedores = new Entity_Clientes();

            SqlParameter[] arrPar = new SqlParameter[2];
            arrPar[0] = new SqlParameter("@intCliente", SqlDbType.Int);
            arrPar[0].Value = obj.intCliente;
            arrPar[1] = new SqlParameter("@strPassword", SqlDbType.VarChar);
            arrPar[1].Value = obj.strPassword;

            try
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "usp_tbClientes_Password", arrPar);
                state = true;
            }
            catch (Exception e)
            {
                throw e;
            }

            return state;
        }
        #endregion Login


        #region GetList
        public static DataTable GetList(int intCliente)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[2];
                arrPar[0] = new SqlParameter("@intCliente", SqlDbType.Int);
                arrPar[0].Value = intCliente;

                string strQuery = "usp_tbClientes_Sel";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetList


        #region Save
        public static int Save(Entity_Clientes entClientes)
        {
            int intCliente = 0;
            try
            {
                
                
                SqlParameter[] arrPar = new SqlParameter[11];
                arrPar[0] = new SqlParameter("@intCliente", SqlDbType.Int);
                arrPar[0].Value = entClientes.intCliente;
                arrPar[1] = new SqlParameter("@strNombre", SqlDbType.VarChar);
                arrPar[1].Value = entClientes.strNombre;
                arrPar[2] = new SqlParameter("@strEmail", SqlDbType.VarChar);
                arrPar[2].Value = entClientes.strEmail;
                arrPar[3] = new SqlParameter("@strPassword", SqlDbType.VarChar);
                arrPar[3].Value = entClientes.strPassword;
                arrPar[4] = new SqlParameter("@Customer_Code", SqlDbType.VarChar);
                arrPar[4].Value = entClientes.Customer_Code;
                arrPar[5] = new SqlParameter("@strUsuarioAlta", SqlDbType.VarChar);
                arrPar[5].Value = entClientes.strUsuarioAlta;
                arrPar[6] = new SqlParameter("@strMaquinaAlta", SqlDbType.VarChar);
                arrPar[6].Value = entClientes.strMaquinaAlta;
                arrPar[7] = new SqlParameter("@intLista", SqlDbType.Int);
                arrPar[7].Value = entClientes.intLista;
                arrPar[8] = new SqlParameter("@bPrecioEditable", SqlDbType.Bit);
                arrPar[8].Value = entClientes.bPrecioEditable;
                arrPar[9] = new SqlParameter("@strEmpresa", SqlDbType.VarChar);
                arrPar[9].Value = entClientes.strEmpresa;
                arrPar[10] = new SqlParameter("@intActivo", SqlDbType.Int);
                arrPar[10].Value = entClientes.intActivo;

                string strQuery = "usp_tbClientes_Save";
                intCliente = int.Parse(SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intCliente;
        }
        #endregion Save

        #region Delete
        public static void Delete(int intCliente)
        {
            try
            {
                SqlParameter[] arrPar = new SqlParameter[1];
                arrPar[0] = new SqlParameter("@intCliente", SqlDbType.Int);
                arrPar[0].Value = intCliente;
                string strQuery = "usp_tbClientes_Delete";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Delete

        #region Create
        static Entity_Clientes CreateObject(IDataReader drd)
        {
            Entity_Clientes oEnt = new Entity_Clientes();
            oEnt.intCliente = (int)drd["intCliente"];
            oEnt.strNombre = (string)drd["strNombre"];
            oEnt.strEmail = (string)drd["strEmail"];
            oEnt.Customer_Code = (string)drd["Customer_Code"];
            oEnt.IntParametroInicial = (int)drd["EsNuevo"];
            oEnt.State_Code = (string)drd["State_Code"];
            oEnt.City = (string)drd["City"];
            oEnt.Project_Code = (string)drd["Project_Code"];
            oEnt.intLista = (int)drd["intLista"];
            oEnt.bPrecioEditable = (bool)drd["bPrecioEditable"];
            return oEnt;
        }
        #endregion
    }
}
