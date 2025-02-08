using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess
{
    public class DACFacturas : Base
    {

        #region GetList
        public static DataTable GetList()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                string strQuery = "usp_tbFacturas_Sel";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetList
        
        #region GetListSinPedido
        public static DataTable GetListSinPedido()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                string strQuery = "usp_tbFacturas_ListSinPedido";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetListSinPedido
        
        #region GetListByEstatus
        public static DataTable GetListByEstatus(bool bEnvioEmail)
        {
            SqlParameter[] arrPar = new SqlParameter[1];
            arrPar[0] = new SqlParameter("@bEnvioEmail", SqlDbType.Bit);
            arrPar[0].Value = bEnvioEmail;
            DataTable dtReturn = new DataTable();
            try
            {
                string strQuery = "usp_tbFacturas_SelByEstatus";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetListByEstatus
        
        #region GetListPedidosSinEnviar
        public static DataTable GetListPedidosSinEnviar(string strFactura)
        {
            SqlParameter[] arrPar = new SqlParameter[1];
            arrPar[0] = new SqlParameter("@strFactura", SqlDbType.VarChar);
            arrPar[0].Value = strFactura;
            DataTable dtReturn = new DataTable();
            try
            {
                string strQuery = "usp_tbFacturas_PedidoSinEnviar";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return dtReturn;
        }
        #endregion GetListPedidosSinEnviar


        public static DataTable GetListClientes()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                string strQuery = "usp_tbPedido_SelClientes";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }


        #region Get
        public static DataTable Get(Entity_Facturas entFacturas)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[3];
                arrPar[0] = new SqlParameter("@intEmpresa", SqlDbType.Int);
                arrPar[0].Value = entFacturas.intEmpresa;
                arrPar[1] = new SqlParameter("@strFactura", SqlDbType.VarChar);
                arrPar[1].Value = entFacturas.strFactura;
                arrPar[2] = new SqlParameter("@strSerie", SqlDbType.VarChar);
                arrPar[2].Value = entFacturas.strSerie;
                string strQuery = "usp_tbFacturas_SelByFolio";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch(Exception ex)
            {

            }
            return dtReturn;
        }
        #endregion Get

        #region Save
        public static void Save(Entity_Facturas entFacturas)
        {
            try
            {
                SqlParameter[] arrPar = new SqlParameter[21];
                arrPar[0] = new SqlParameter("@intEmpresa", SqlDbType.Int);
                arrPar[0].Value = entFacturas.intEmpresa;
                arrPar[1] = new SqlParameter("@strFactura", SqlDbType.VarChar);
                arrPar[1].Value = entFacturas.strFactura;
                arrPar[2] = new SqlParameter("@strFolioFiscal", SqlDbType.VarChar);
                arrPar[2].Value = entFacturas.strFolioFiscal;
                arrPar[3] = new SqlParameter("@dblImporte", SqlDbType.Decimal);
                arrPar[3].Value = entFacturas.dblImporte;
                arrPar[4] = new SqlParameter("@dblIva", SqlDbType.Decimal);
                arrPar[4].Value = entFacturas.dblIva;
                arrPar[5] = new SqlParameter("@dblSubTotal", SqlDbType.Decimal);
                arrPar[5].Value = entFacturas.dblSubTotal;
                arrPar[6] = new SqlParameter("@dblRetencion", SqlDbType.Decimal);
                arrPar[6].Value = entFacturas.dblRetencion;
                arrPar[7] = new SqlParameter("@strPDF", SqlDbType.VarChar);
                arrPar[7].Value = entFacturas.strPDF;
                arrPar[8] = new SqlParameter("@strXML", SqlDbType.VarChar);
                arrPar[8].Value = entFacturas.strXML;
                arrPar[9] = new SqlParameter("@strRemision", SqlDbType.VarChar);
                arrPar[9].Value = entFacturas.strRemision;
                arrPar[10] = new SqlParameter("@strMetodoPago", SqlDbType.VarChar);
                arrPar[10].Value = entFacturas.strMetodoPago;
                arrPar[11] = new SqlParameter("@datFechaFactura", SqlDbType.DateTime);
                arrPar[11].Value = entFacturas.datFechaFactura;
                arrPar[12] = new SqlParameter("@strOCR", SqlDbType.VarChar);
                arrPar[12].Value = entFacturas.strOCR;
                arrPar[13] = new SqlParameter("@intEstatus", SqlDbType.Int);
                arrPar[13].Value = entFacturas.intEstatus;
                arrPar[14] = new SqlParameter("@strMaquinaAlta", SqlDbType.VarChar);
                arrPar[14].Value = entFacturas.strMaquinaAlta;
                arrPar[15] = new SqlParameter("@strPedidos", SqlDbType.VarChar);
                arrPar[15].Value = entFacturas.strPedidos;
                arrPar[16] = new SqlParameter("@strRFCReceptor", SqlDbType.VarChar);
                arrPar[16].Value = entFacturas.strReceptorRFC;
                arrPar[17] = new SqlParameter("@strCliente", SqlDbType.VarChar);
                arrPar[17].Value = entFacturas.strCliente;
                arrPar[18] = new SqlParameter("@intCliente", SqlDbType.Int);
                arrPar[18].Value = int.Parse(entFacturas.strUsuarioAlta);
                arrPar[19] = new SqlParameter("@strSerie", SqlDbType.VarChar);
                arrPar[19].Value = entFacturas.strSerie;
                arrPar[20] = new SqlParameter("@dblDescuento", SqlDbType.Decimal);
                arrPar[20].Value = entFacturas.dblDescuento;


                string strQuery = "usp_tbFacturas_Save";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Save
        
        #region SavePedidos
        public static void SavePedidos(Entity_Facturas entFacturas)
        {
            try
            {
                SqlParameter[] arrPar = new SqlParameter[4];
                arrPar[0] = new SqlParameter("@intEmpresa", SqlDbType.Int);
                arrPar[0].Value = entFacturas.intEmpresa;
                arrPar[1] = new SqlParameter("@strSerie", SqlDbType.VarChar);
                arrPar[1].Value = entFacturas.strSerie;
                arrPar[2] = new SqlParameter("@strFactura", SqlDbType.VarChar);
                arrPar[2].Value = entFacturas.strFactura;
                arrPar[3] = new SqlParameter("@strPedidos", SqlDbType.VarChar);
                arrPar[3].Value = entFacturas.strPedidos;

                string strQuery = "usp_tbFacturas_Pedidos_Save";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion SavePedidos
        
        #region SaveFacturaDescarga
        public static void SaveFacturaDescarga(Entity_Facturas entFacturas)
        {
            try
            {
                SqlParameter[] arrPar = new SqlParameter[4];
                arrPar[0] = new SqlParameter("@strFactura", SqlDbType.VarChar);
                arrPar[0].Value = entFacturas.strFactura;
                arrPar[1] = new SqlParameter("@strArchivo", SqlDbType.VarChar);
                arrPar[1].Value = entFacturas.strPDF;
                arrPar[2] = new SqlParameter("@intCliente", SqlDbType.Int);
                arrPar[2].Value = entFacturas.IntParametroInicial;
                arrPar[3] = new SqlParameter("@strMaquina", SqlDbType.VarChar);
                arrPar[3].Value = entFacturas.strMaquinaAlta;

                string strQuery = "usp_tbFacturasDescargas_Save";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion SaveFacturaDescarga
        
        #region SaveEstatusEmail
        public static void SaveEstatusEmail(int intEmpresa, string strFactura, string strSerie)
        {
            try
            {
                SqlParameter[] arrPar = new SqlParameter[3];
                arrPar[0] = new SqlParameter("@intEmpresa", SqlDbType.Int);
                arrPar[0].Value = intEmpresa;
                arrPar[1] = new SqlParameter("@strFactura", SqlDbType.VarChar);
                arrPar[1].Value = strFactura;
                arrPar[2] = new SqlParameter("@strSerie", SqlDbType.VarChar);
                arrPar[2].Value = strSerie;

                string strQuery = "usp_tbFacturas_SaveEstatusEmail";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion SaveEstatusEmail
        
        #region Valida
        public static void Valida(Entity_Facturas entFacturas)
        {
            try
            {
                SqlParameter[] arrPar = new SqlParameter[6];
                arrPar[0] = new SqlParameter("@strRFCEmisor", SqlDbType.VarChar);
                arrPar[0].Value = entFacturas.strEmisorRFC;
                arrPar[1] = new SqlParameter("@strRFCReceptor", SqlDbType.VarChar);
                arrPar[1].Value = entFacturas.strReceptorRFC;
                arrPar[2] = new SqlParameter("@strFactura", SqlDbType.VarChar);
                arrPar[2].Value = entFacturas.strFactura;
                arrPar[3] = new SqlParameter("@strPedidos", SqlDbType.VarChar);
                arrPar[3].Value = entFacturas.strPedidos;
                arrPar[4] = new SqlParameter("@dblImporteFactura", SqlDbType.Decimal);
                arrPar[4].Value = entFacturas.dblImporte;
                arrPar[5] = new SqlParameter("@dblImporteSubtotal", SqlDbType.Decimal);
                arrPar[5].Value = entFacturas.dblSubTotal;
              //  string strQuery = "usp_tbFacturas_Valida";
             //   SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Valida

        #region Delete
        public static void Delete(Entity_Facturas entFacturas)
        {
            try
            {
                SqlParameter[] arrPar = new SqlParameter[3];
                arrPar[0] = new SqlParameter("@intEmpresa", SqlDbType.Int);
                arrPar[0].Value = entFacturas.intEmpresa;
                arrPar[1] = new SqlParameter("@strFactura", SqlDbType.VarChar);
                arrPar[1].Value = entFacturas.strFactura;
                arrPar[2] = new SqlParameter("@strSerie", SqlDbType.VarChar);
                arrPar[2].Value = entFacturas.strSerie;

                string strQuery = "usp_tbFacturas_Del";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Delete

        #region Create
        static Entity_Facturas CreateObject(IDataReader drd)
        {
            Entity_Facturas oEnt = new Entity_Facturas();
            oEnt.intEmpresa = (int)drd["intEmpresa"];
            oEnt.strFactura = (string)drd["strFactura"];
            oEnt.strFolioFiscal = (string)drd["strFolioFiscal"];
            oEnt.dblImporte = (decimal)drd["dblImporte"];
            oEnt.dblIva = (decimal)drd["dblIva"];
            oEnt.dblSubTotal = (decimal)drd["dblSubTotal"];
            oEnt.dblRetencion = (decimal)drd["dblRetencion"];
            oEnt.strPDF = (string)drd["strPDF"];
            oEnt.strXML = (string)drd["strXML"];
            oEnt.strRemision = (string)drd["strRemision"];
            oEnt.strMetodoPago = (string)drd["strMetodoPago"];
            oEnt.datFechaFactura = (DateTime)drd["datFechaFactura"];
            oEnt.strOCR = (string)drd["strOCR"];
            oEnt.intEstatus = (int)drd["intEstatus"];
            oEnt.strMaquinaAlta = (string)drd["strMaquinaAlta"];
            oEnt.datFechaAlta = (DateTime)drd["datFechaAlta"];
            return oEnt;
        }
        #endregion



        

    }
}
