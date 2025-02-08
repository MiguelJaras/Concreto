using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess
{
    public class DACServiciosEspeciales : Base
    {

        #region GetList
        public static DataTable GetList()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                IDataReader drd;
                string strQuery = "usp_tbServiciosEspeciales_Sel";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetList

        #region GetListPrecio
        public static List<Entity_ServiciosEspeciales> GetListPrecio(int intLista,decimal decPorcentaje)
        {
            List<Entity_ServiciosEspeciales> lstReturn = new List<Entity_ServiciosEspeciales>();
            try
            {
                IDataReader drd;
                SqlParameter[] arrPar = new SqlParameter[2];

                arrPar[0] = new SqlParameter("@intLista", SqlDbType.Int);
                arrPar[0].Value = intLista;

                arrPar[1] = new SqlParameter("@dblPorcentaje", SqlDbType.Decimal);
                arrPar[1].Value = decPorcentaje;


                string strQuery = "usp_tbServiciosEspeciales_SelPrecio";
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
        #endregion GetListPrecio



        #region GetListPrecio
        public static List<Entity_ServiciosEspeciales> GetListPrecioPorcentaje(int intLista, decimal decPorcentaje)
        {
            List<Entity_ServiciosEspeciales> lstReturn = new List<Entity_ServiciosEspeciales>();
            try
            {
                IDataReader drd;
                SqlParameter[] arrPar = new SqlParameter[2];
                arrPar[0] = new SqlParameter("@intLista", SqlDbType.Int);
                arrPar[0].Value = intLista;

                arrPar[1] = new SqlParameter("@decPorcentaje", SqlDbType.Decimal);
                arrPar[1].Value = decPorcentaje;

                string strQuery = "usp_tbServiciosEspeciales_SelPrecioPorcentaje";
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
        #endregion GetListPrecio






        #region Save
        public static void Save(Entity_ServiciosEspeciales entServicio)
        {
            try
            {
                int intClienteAlta;
                int.TryParse(entServicio.strUsuarioAlta, out intClienteAlta);

                SqlParameter[] arrPar = new SqlParameter[5];
                arrPar[0] = new SqlParameter("@intServicio", SqlDbType.Int);
                arrPar[0].Value = entServicio.intServicio;
                arrPar[1] = new SqlParameter("@strNombre", SqlDbType.VarChar);
                arrPar[1].Value = entServicio.strNombre;
                arrPar[2] = new SqlParameter("@decPrecio", SqlDbType.Decimal);
                arrPar[2].Value = entServicio.dblPrecio;
                arrPar[3] = new SqlParameter("@intClienteAlta", SqlDbType.Int);
                arrPar[3].Value = intClienteAlta;
                arrPar[4] = new SqlParameter("@strMaquina", SqlDbType.VarChar);
                arrPar[4].Value = entServicio.strMaquinaAlta;

                string strQuery = "usp_tbServiciosEspeciales_Save";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion Save


        #region Delete
        public static void Delete(int intServicio)
        {
            try
            {
                SqlParameter[] arrPar = new SqlParameter[1];
                arrPar[0] = new SqlParameter("@intServicio", SqlDbType.Int);
                arrPar[0].Value = intServicio;
                string strQuery = "usp_tbServiciosEspeciales_Del";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion Delete

        #region Create
        static Entity_ServiciosEspeciales CreateObject(IDataReader drd)
        {
            Entity_ServiciosEspeciales oEnt = new Entity_ServiciosEspeciales();
            oEnt.intServicio = (int)drd["intServicio"];
            oEnt.strNombre = (string)drd["strNombre"];
            oEnt.dblPrecio = (decimal)drd["dblPrecio"];
            oEnt.intGrupo = (int)drd["intGrupo"];
            return oEnt;
        }
        #endregion
    }
}
