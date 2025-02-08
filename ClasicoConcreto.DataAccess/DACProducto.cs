using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess
{
    public class DACProducto : Base
    {
        #region GetList
        public static DataTable GetList()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                IDataReader drd;
                string strQuery = "usp_tbProducto_Sel";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetList




        #region GetListPorcentaje
        public static DataTable GetListPorcentaje()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                IDataReader drd;
                string strQuery = "SELECT dblPorcentajePrecio [PorcentajePrecio], REPLACE(CONVERT(VARCHAR,dblPorcentajePrecio),'.00','') + '%' [Nombre] FROM dbConcreto.dbo.fn_PorcentajePrecio()";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strQuery).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetListPorcentaje



        #region GetList
        public static List<Entity_Producto> GetListActivos(int intLista,decimal dblPorcentaje)
        {
            List<Entity_Producto> lstReturn = new List<Entity_Producto>();
            try
            {
                IDataReader drd;
                SqlParameter[] arrPar = new SqlParameter[2];
                arrPar[0] = new SqlParameter("@intLista", SqlDbType.Int);
                arrPar[0].Value = intLista;
                arrPar[1] = new SqlParameter("@dblPorcentaje", SqlDbType.Decimal);
                arrPar[1].Value = dblPorcentaje;
                string strQuery = "usp_tbProducto_SelActivo";
                drd = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);
                while (drd.Read())
                {
                    lstReturn.Add(CreateObject(drd));
                }
            }
            catch
            {

            }
            return lstReturn;
        }
        #endregion GetList


        #region GetListCotizacion
        public static DataTable GetListCotizacion(int intLista)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[1];
                arrPar[0] = new SqlParameter("@intLista", SqlDbType.Int);
                arrPar[0].Value = intLista;
                string strQuery = "usp_tbProductos_ListCotizacion";
                dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];             
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        #endregion GetListCotizacion



        #region Save
        public static void Save(Entity_Producto entProducto)
        {
            try
            {
                int intClienteAlta;
                int.TryParse(entProducto.strUsuarioAlta, out intClienteAlta);

                SqlParameter[] arrPar = new SqlParameter[5];
                arrPar[0] = new SqlParameter("@intProducto", SqlDbType.Int);
                arrPar[0].Value = entProducto.intProducto;
                arrPar[1] = new SqlParameter("@strNombre", SqlDbType.VarChar);
                arrPar[1].Value = entProducto.strNombre;
                arrPar[2] = new SqlParameter("@bEstatus", SqlDbType.Bit);
                arrPar[2].Value = entProducto.bEstatus;
                arrPar[3] = new SqlParameter("@intClienteAlta", SqlDbType.Int);
                arrPar[3].Value = intClienteAlta;
                arrPar[4] = new SqlParameter("@strMaquina", SqlDbType.VarChar);
                arrPar[4].Value = entProducto.strMaquinaAlta;

                string strQuery = "usp_tbProducto_Save";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion Save


        #region Delete
        public static void Delete(int intProducto)
        {
            try
            {
                SqlParameter[] arrPar = new SqlParameter[1];
                arrPar[0] = new SqlParameter("@intProducto", SqlDbType.Int);
                arrPar[0].Value = intProducto;
                string strQuery = "usp_tbProducto_Del";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion Delete


        #region Create
        static Entity_Producto CreateObject(IDataReader drd)
        {
            Entity_Producto oEnt = new Entity_Producto();
            oEnt.intProducto = (int)drd["intProducto"];
            oEnt.strNombre = (string)drd["strNombre"];
            oEnt.dblMenudeo = (decimal)drd["dblMenudeo"];
            oEnt.dblMedioMayoreo = (decimal)drd["dblMedioMayoreo"];
            oEnt.dblMayoreo = (decimal)drd["dblMayoreo"];
            return oEnt;
        }
        #endregion
    }
}
