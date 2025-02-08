using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess
{
    public class DACOrdenes : Base
    {

        #region GetListPlantaOrden
        public static DataTable GetListPlantaOrden(DateTime datFechaInicio, DateTime datFechaFin)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[2];
                arrPar[0] = new SqlParameter("@datFechaInicio", SqlDbType.DateTime);
                arrPar[0].Value = datFechaInicio;
                arrPar[1] = new SqlParameter("@datFechaFin", SqlDbType.DateTime);
                arrPar[1].Value = datFechaFin;

                string strQuery = "usp_tbPlantaOrdenes_List";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetListPlantaOrden



        #region GetListPlantaOrdenExterna
        public static DataTable GetListPlantaOrdenExterna(DateTime datFechaInicio, DateTime datFechaFin)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[2];
                arrPar[0] = new SqlParameter("@datFechaInicio", SqlDbType.DateTime);
                arrPar[0].Value = datFechaInicio;
                arrPar[1] = new SqlParameter("@datFechaFin", SqlDbType.DateTime);
                arrPar[1].Value = datFechaFin;

                string strQuery = "usp_tbPlantaOrdeneExterna_List";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetListPlantaOrdenExterna

        #region GetListPlantaOrdenRemisiones
        public static DataTable GetListPlantaOrdenRemisiones(DateTime datFechaInicio, DateTime datFechaFin)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[2];
                arrPar[0] = new SqlParameter("@datFechaInicio", SqlDbType.DateTime);
                arrPar[0].Value = datFechaInicio;
                arrPar[1] = new SqlParameter("@datFechaFin", SqlDbType.DateTime);
                arrPar[1].Value = datFechaFin;

                string strQuery = "usp_tbPlantaOrdenesRemisiones_List";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetListPlantaOrdenExterna


        #region SavePlantaOrden
        public static bool SavePlantaOrden(Entity_PlantaOrdenes ent)
        {
            bool bReturn = false;
            try
            {
                SqlParameter[] arrPar = new SqlParameter[13];
                arrPar[0] = new SqlParameter("@intId", SqlDbType.Int);
                arrPar[0].Value = ent.intId;

                arrPar[1] = new SqlParameter("@intPlanta", SqlDbType.Int);
                arrPar[1].Value = ent.intPlanta;

                arrPar[2] = new SqlParameter("@intPlantaOrden", SqlDbType.Int);
                arrPar[2].Value = ent.intPlantaOrden;

                arrPar[3] = new SqlParameter("@intFolioOC", SqlDbType.Int);
                arrPar[3].Value = ent.intFolioOC;

                arrPar[4] = new SqlParameter("@strRemision", SqlDbType.VarChar);
                arrPar[4].Value = ent.strRemision;

                arrPar[5] = new SqlParameter("@datFecha", SqlDbType.DateTime);
                arrPar[5].Value = ent.datFecha;

                arrPar[6] = new SqlParameter("@decCarga", SqlDbType.Decimal);
                arrPar[6].Value = ent.decCarga;

                arrPar[7] = new SqlParameter("@strMaquinaAlta", SqlDbType.VarChar);
                arrPar[7].Value = ent.strMaquinaAlta;

                arrPar[8] = new SqlParameter("@strUsuarioAlta", SqlDbType.VarChar);
                arrPar[8].Value = "sistemasDic16012024";
               // arrPar[8].Value = ent.strUsuarioAlta;

                arrPar[9] = new SqlParameter("@strCliente", SqlDbType.VarChar);
                arrPar[9].Value = ent.strCliente;

                arrPar[10] = new SqlParameter("@strEstatus", SqlDbType.VarChar);
                arrPar[10].Value = ent.strEstatus;

                arrPar[11] = new SqlParameter("@datFechaCarga", SqlDbType.DateTime);
                arrPar[11].Value = ent.datFechaCarga;


                arrPar[12] = new SqlParameter("@strProducto", SqlDbType.VarChar);
                arrPar[12].Value = ent.strProducto;

                string strQuery = "usp_tbPlantaOrdenes_Save";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);
                bReturn = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return bReturn;
        }
        #endregion SavePlantaOrden



        #region GetOrdenesByPedido
        public static DataTable GetOrdenesByPedido(int intCliente, int intPedido)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[2];
                arrPar[0] = new SqlParameter("@intCliente", SqlDbType.Int);
                arrPar[0].Value = intCliente;
                arrPar[1] = new SqlParameter("@intPedido", SqlDbType.Int);
                arrPar[1].Value = intPedido;

                string strQuery = "usp_tbOrdenes_SelDetalle";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetOrdenesPedido



        #region SavePlantaOrdenExterna
        public static bool SavePlantaOrdenExterna(Entity_PlantaOrdenesExterna ent)
        {
            bool bReturn = false;
            try
            {
                SqlParameter[] arrPar = new SqlParameter[14];
                arrPar[0] = new SqlParameter("@intId", SqlDbType.Int);
                arrPar[0].Value = ent.intId;
                arrPar[1] = new SqlParameter("@intPlanta", SqlDbType.Int);
                arrPar[1].Value = ent.intPlanta;
                arrPar[2] = new SqlParameter("@datFecha", SqlDbType.DateTime);
                arrPar[2].Value = ent.datFecha;
                arrPar[3] = new SqlParameter("@strRemision", SqlDbType.VarChar);
                arrPar[3].Value = ent.strRemision;
                arrPar[4] = new SqlParameter("@strCliente", SqlDbType.VarChar);
                arrPar[4].Value = ent.strCliente;
                arrPar[5] = new SqlParameter("@strResistencia", SqlDbType.VarChar);
                arrPar[5].Value = ent.strResistencia;
                arrPar[6] = new SqlParameter("@decCantidad", SqlDbType.Decimal);
                arrPar[6].Value = ent.decCantidad;
                arrPar[7] = new SqlParameter("@decBombeable", SqlDbType.Decimal);
                arrPar[7].Value = ent.decBombeable;
                arrPar[8] = new SqlParameter("@decPrecioVenta", SqlDbType.Decimal);
                arrPar[8].Value = ent.decPrecioVenta;
                arrPar[9] = new SqlParameter("@decPorcIva", SqlDbType.Decimal);
                arrPar[9].Value = ent.decPorcIva;

                //arrPar[10] = new SqlParameter("@strClaveResistencia", SqlDbType.VarChar);
                //arrPar[10].Value = ent.strRemision;                       
                arrPar[10] = new SqlParameter("@decPrecioBase", SqlDbType.Decimal);
                arrPar[10].Value = ent.decPrecioBase;


                arrPar[11] = new SqlParameter("@strUsuarioAlta", SqlDbType.VarChar);
                arrPar[11].Value = ent.strUsuarioAlta;
                arrPar[12] = new SqlParameter("@strMaquina", SqlDbType.VarChar);
                arrPar[12].Value = ent.strMaquinaAlta;

                arrPar[13] = new SqlParameter("@decRemisionado", SqlDbType.Decimal);
                arrPar[13].Value = ent.decRemisonado;



                // string strQuery = "usp_tbPlantaOrdenesExterna_Save";
                string strQuery = "usp_tbPlantaOrdenesExternaPrecioBase_Save";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);
                bReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bReturn;
        }
        #endregion SavePlantaOrden

        #region SavePlantaOrdenRemision
        public static bool SavePlantaOrdenRemision(Entity_PlantaOrdenesRemisiones ent)
        {
            bool bReturn = false;
            try
            {
                SqlParameter[] arrPar = new SqlParameter[12];
                arrPar[0] = new SqlParameter("@intId", SqlDbType.Int);
                arrPar[0].Value = ent.intId;
                arrPar[1] = new SqlParameter("@intPlanta", SqlDbType.Int);
                arrPar[1].Value = ent.intPlanta;
                arrPar[2] = new SqlParameter("@strRemision", SqlDbType.VarChar);
                arrPar[2].Value = ent.strRemision;
                arrPar[3] = new SqlParameter("@intFolio", SqlDbType.Int);
                arrPar[3].Value = ent.intFolio;
                arrPar[4] = new SqlParameter("@decCantidad", SqlDbType.Decimal);
                arrPar[4].Value = ent.decCantidad;
                arrPar[5] = new SqlParameter("@datFecha", SqlDbType.DateTime);
                arrPar[5].Value = ent.datFecha;                                                                
                arrPar[6] = new SqlParameter("@strUsuarioAlta", SqlDbType.VarChar);
                arrPar[6].Value = ent.strUsuarioAlta;
                arrPar[7] = new SqlParameter("@strMaquina", SqlDbType.VarChar);
                arrPar[7].Value = ent.strMaquinaAlta;
                arrPar[8] = new SqlParameter("@strStatus", SqlDbType.VarChar);
                arrPar[8].Value = ent.strStatus;

                string strQuery = "usp_tbPlantaOrdenesRemisiones_Save";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);
                bReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bReturn;
        }
        #endregion SavePlantaOrden



    }
}
