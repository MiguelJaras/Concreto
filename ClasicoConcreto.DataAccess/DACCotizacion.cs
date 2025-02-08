using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess
{
    public class DACCotizacion : Base
    {

        #region Get
        public static DataTable Get(int intCliente, int intCotizacion)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[2];
                arrPar[0] = new SqlParameter("@intCliente", SqlDbType.Int);
                arrPar[0].Value = intCliente;
                arrPar[1] = new SqlParameter("@intCotizacion", SqlDbType.Int);
                arrPar[1].Value = intCotizacion;
                string strQuery = "usp_tbCotizacion_Sel";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion Get


        #region GetDet
        public static DataTable GetDet(int intCotizacion)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[1];
                arrPar[0] = new SqlParameter("@intCotizacion", SqlDbType.Int);
                arrPar[0].Value = intCotizacion;
                string strQuery = "usp_tbCotizacionDet_Sel";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetDet




        #region GetList
        public static DataTable GetList(int intCliente, DateTime datFechaInicio, DateTime datFechaFin)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[4];
                arrPar[0] = new SqlParameter("@intCliente", SqlDbType.Int);
                arrPar[0].Value = intCliente;
                arrPar[1] = new SqlParameter("@datFechaInicio", SqlDbType.DateTime);
                arrPar[1].Value = datFechaInicio;
                arrPar[2] = new SqlParameter("@datFechaFin", SqlDbType.DateTime);
                arrPar[2].Value = datFechaFin;
                string strQuery = "usp_tbCotizacion_List";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetList

        #region Save
        public static int Save(Entity_Cotizacion ent)
        {
            int intCotizacion = 0;
            try
            {
                SqlParameter[] arrPar = new SqlParameter[16];
                arrPar[0] = new SqlParameter("@intCotizacion", SqlDbType.Int);
                arrPar[0].Value = ent.intCotizacion;
                arrPar[1] = new SqlParameter("@strCliente", SqlDbType.VarChar);
                arrPar[1].Value = ent.strCliente;
                arrPar[2] = new SqlParameter("@strObra", SqlDbType.VarChar);
                arrPar[2].Value = ent.strObra;
                arrPar[3] = new SqlParameter("@strElemento", SqlDbType.VarChar);
                arrPar[3].Value = ent.strElemento;
                arrPar[4] = new SqlParameter("@datFechaColado", SqlDbType.VarChar);
                arrPar[4].Value = ent.datFechaColado;
                arrPar[5] = new SqlParameter("@strTipoConcreto", SqlDbType.VarChar);
                arrPar[5].Value = ent.strTipoConcreto;
                arrPar[6] = new SqlParameter("@strResistencia", SqlDbType.VarChar);
                arrPar[6].Value = ent.strResistencia;
                arrPar[7] = new SqlParameter("@intRevenimiento", SqlDbType.Int);
                arrPar[7].Value = ent.intRevenimiento;
                arrPar[8] = new SqlParameter("@intAgregado", SqlDbType.Int);
                arrPar[8].Value = ent.intAgregado;
                arrPar[9] = new SqlParameter("@intTipo", SqlDbType.Int);
                arrPar[9].Value = ent.intTipo;
                arrPar[10] = new SqlParameter("@strExtras", SqlDbType.VarChar);
                arrPar[10].Value = ent.strExtras;
                arrPar[11] = new SqlParameter("@strExtras2", SqlDbType.VarChar);
                arrPar[11].Value = ent.strExtras2;
                arrPar[12] = new SqlParameter("@intTiro", SqlDbType.Int);
                arrPar[12].Value = ent.intTiro;
                arrPar[13] = new SqlParameter("@decPorcIva", SqlDbType.Decimal);
                arrPar[13].Value = ent.decPorcIva;
                arrPar[14] = new SqlParameter("@intClienteAlta", SqlDbType.Int);
                arrPar[14].Value = ent.intClienteAlta;
                arrPar[15] = new SqlParameter("@strMaquinAlta", SqlDbType.VarChar);
                arrPar[15].Value = ent.strMaquinAlta;
                string strQuery = "usp_tbCotizacion_Save";
                string strIntCotizacion = SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).ToString();
                int.TryParse(strIntCotizacion, out intCotizacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intCotizacion;
        }
        #endregion Save


        #region Save
        public static int SaveDet(Entity_CotizacionDet ent)
        {
            int intPartida = 0;
            try
            {
                SqlParameter[] arrPar = new SqlParameter[9];
                arrPar[0] = new SqlParameter("@intPartida", SqlDbType.Int);
                arrPar[0].Value = ent.intPartida;
                arrPar[1] = new SqlParameter("@intCotizacion", SqlDbType.Int);
                arrPar[1].Value = ent.intCotizacion;
                arrPar[2] = new SqlParameter("@intProducto", SqlDbType.Int);
                arrPar[2].Value = ent.intProducto;
                arrPar[3] = new SqlParameter("@intTipo", SqlDbType.Int);
                arrPar[3].Value = ent.intTipo;
                arrPar[4] = new SqlParameter("@decCantidad", SqlDbType.Decimal);
                arrPar[4].Value = ent.decCantidad;
                arrPar[5] = new SqlParameter("@decPrecio", SqlDbType.Decimal);
                arrPar[5].Value = ent.decPrecio;
                arrPar[6] = new SqlParameter("@decTotal", SqlDbType.Decimal);
                arrPar[6].Value = ent.decTotal;
                arrPar[7] = new SqlParameter("@intClienteAlta", SqlDbType.Int);
                arrPar[7].Value = ent.intUsuarioAlta;
                arrPar[8] = new SqlParameter("@strMaquinaAlta", SqlDbType.VarChar);
                arrPar[8].Value = ent.strMaquinaAlta;
                
                string strQuery = "usp_tbCotizacionDet_Save";
                string strIntPartida = SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).ToString();
                int.TryParse(strIntPartida, out intPartida);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intPartida;
        }
        #endregion Save





        #region DeleteProducto
        public static void DeleteProducto(int intCliente, int intPartida)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[2];
                arrPar[0] = new SqlParameter("@intCotizacion", SqlDbType.Int);
                arrPar[0].Value = intCliente;
                arrPar[1] = new SqlParameter("@intPartida", SqlDbType.Int);
                arrPar[1].Value = intPartida;
                string strQuery = "usp_tbCotizacionDet_Del";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);
            }
            catch
            {

            }
        }
        #endregion DeleteProducto




        #region Create
        static Entity_Cotizacion CreateObject(IDataReader drd)
        {
            Entity_Cotizacion oEnt = new Entity_Cotizacion();
            oEnt.intCotizacion = (int)drd["intCotizacion"];
            oEnt.strCliente = (string)drd["strCliente"];
            oEnt.strObra = (string)drd["strObra"];
            oEnt.strElemento = (string)drd["strElemento"];
            oEnt.datFechaColado = (string)drd["datFechaColado"];
            oEnt.strTipoConcreto = (string)drd["strTipoConcreto"];
            oEnt.strResistencia = (string)drd["strResistencia"];
            oEnt.intRevenimiento = (int)drd["intRevenimiento"];
            oEnt.intAgregado = (int)drd["intAgregado"];
            oEnt.intTipo = (int)drd["intTipo"];
            oEnt.strExtras = (string)drd["strExtras"];
            oEnt.strExtras2 = (string)drd["strExtras2"];
            oEnt.intTiro = (int)drd["intTiro"];
            oEnt.decPorcIva = (decimal)drd["decPorcIva"];
            oEnt.decSubTotal = (decimal)drd["decSubTotal"];
            oEnt.decIva = (decimal)drd["decIva"];
            oEnt.decTotal = (decimal)drd["decTotal"];
            oEnt.intClienteAlta = (int)drd["intClienteAlta"];
            oEnt.strMaquinAlta = (string)drd["strMaquinAlta"];
            return oEnt;
        }
        #endregion
    }
}
