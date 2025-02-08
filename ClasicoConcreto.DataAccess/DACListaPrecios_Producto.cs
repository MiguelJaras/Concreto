using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess
{
    public class DACListaPrecios_Producto : Base
    {

        #region GetListProductos
        public static DataTable GetListProductos(int intLista)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[1];
                arrPar[0] = new SqlParameter("@intListaPrecio", SqlDbType.Int);
                arrPar[0].Value = intLista;
                string strQuery = "usp_tbListaPrecios_Producto_Sel";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetListProductos



        #region Save
        public static void Save(Entity_ListaPrecios_Producto entListaPrecio)
        {
            try
            {
                SqlParameter[] arrPar = new SqlParameter[7];
                arrPar[0] = new SqlParameter("@intLista", SqlDbType.Int);
                arrPar[0].Value = entListaPrecio.intLista;
                arrPar[1] = new SqlParameter("@intProducto", SqlDbType.Int);
                arrPar[1].Value = entListaPrecio.intProducto;
                arrPar[2] = new SqlParameter("@dblMenudeo", SqlDbType.Decimal);
                arrPar[2].Value = entListaPrecio.dblMenudeo;
                arrPar[3] = new SqlParameter("@dblMedioMayoreo", SqlDbType.Decimal);
                arrPar[3].Value = entListaPrecio.dblMedioMayoreo;
                arrPar[4] = new SqlParameter("@dblMayoreo", SqlDbType.Decimal);
                arrPar[4].Value = entListaPrecio.dblMayoreo;
                arrPar[5] = new SqlParameter("@intClienteAlta", SqlDbType.Int);
                arrPar[5].Value = entListaPrecio.intClienteAlta;
                arrPar[6] = new SqlParameter("@strMaquina", SqlDbType.VarChar);
                arrPar[6].Value = entListaPrecio.strMaquina;

                string strQuery = "usp_tbListaPrecios_Producto_Save";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion Save



        #region Create
        static Entity_ListaPrecios_Producto CreateObject(IDataReader drd)
        {
            Entity_ListaPrecios_Producto oEnt = new Entity_ListaPrecios_Producto();
            oEnt.intLista = (int)drd["intLista"];
            oEnt.intProducto = (int)drd["intProducto"];
            oEnt.dblMenudeo = (decimal)drd["dblMenudeo"];
            oEnt.dblMedioMayoreo = (decimal)drd["dblMedioMayoreo"];
            oEnt.dblMayoreo = (decimal)drd["dblMayoreo"];
            oEnt.datFechaAlta = (DateTime)drd["datFechaAlta"];
            oEnt.intClienteAlta = (int)drd["intClienteAlta"];
            oEnt.strMaquina = (string)drd["strMaquina"];
            return oEnt;
        }
        #endregion
    }
}
