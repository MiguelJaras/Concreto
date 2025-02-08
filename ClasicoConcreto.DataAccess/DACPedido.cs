using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess
{
    public class DACPedido : Base
    {
        #region GetList
        public static DataTable GetList(int intCliente, DateTime datFechaInicio, DateTime datFechaFin)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[4];
                arrPar[0] = new SqlParameter("@intCliente", SqlDbType.Int);
                arrPar[0].Value = intCliente;
                arrPar[1] = new SqlParameter("@intPedido", SqlDbType.Int);
                arrPar[1].Value = 0;
                arrPar[2] = new SqlParameter("@datFechaInicio", SqlDbType.DateTime);
                arrPar[2].Value = datFechaInicio;
                arrPar[3] = new SqlParameter("@datFechaFin", SqlDbType.DateTime);
                arrPar[3].Value = datFechaFin;
                string strQuery = "usp_tbPedido_Sel";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch(Exception ex)
            {
                
            }
            return dtReturn;
        }
        #endregion GetList

        #region GetList
        public static DataTable GetListByDate(int intCliente, DateTime datFechaIni, DateTime datFechaFin)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[3];
                arrPar[0] = new SqlParameter("@intCliente", SqlDbType.Int);
                arrPar[0].Value = intCliente;
                arrPar[1] = new SqlParameter("@datFechaIni", SqlDbType.DateTime);
                arrPar[1].Value = datFechaIni;
                arrPar[2] = new SqlParameter("@datFechaFin", SqlDbType.DateTime);
                arrPar[2].Value = datFechaFin;


                string strQuery = "usp_tbPedido_SelByDate";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetList

        #region GetListSinFactura
        public static DataTable GetListSinFactura(DateTime datFechaIni, DateTime datFechaFin)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[2];
                arrPar[0] = new SqlParameter("@datFechaIni", SqlDbType.DateTime);
                arrPar[0].Value = datFechaIni;
                arrPar[1] = new SqlParameter("@datFechaFin", SqlDbType.DateTime);
                arrPar[1].Value = datFechaFin;
                string strQuery = "usp_tbPedido_SelSinFactura";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetListSinFactura

        #region GetListSinFactura
        public static DataTable GetListSinFactura(string strCliente)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                SqlParameter[] arrPar = new SqlParameter[1];
                arrPar[0] = new SqlParameter("@strCliente", SqlDbType.VarChar);
                arrPar[0].Value = strCliente;
                string strQuery = "usp_tbPedido_SelSinFacturaByCliente";
                dtReturn = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, strQuery, arrPar).Tables[0];
            }
            catch
            {

            }
            return dtReturn;
        }
        #endregion GetListSinFactura

        #region GetPedido
        public static DataTable GetPedido(int intCliente, int intPedido)
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[2]
                {
                  new SqlParameter("@intCliente", SqlDbType.Int),
                  null
                };
                sqlParameterArray[0].Value = (object)intCliente;
                sqlParameterArray[1] = new SqlParameter("@intPedido", SqlDbType.Int);
                sqlParameterArray[1].Value = (object)intPedido;
                dataTable = SqlHelper.ExecuteDataset(Base.ConnectionString, CommandType.StoredProcedure, "usp_tbPedido_Sel", sqlParameterArray).Tables[0];
            }
            catch
            {
            }
            return dataTable;
        }
        #endregion ValidarPedido


        #region ValidarPedidoDet
        public static void ValidarPedidoDet(Entity_Pedido ent, Entity_PedidoDet entDet, bool blnServicioBombeo)
        {
            try
            {
                SqlParameter[] arrPar = new SqlParameter[8];
                arrPar[0] = new SqlParameter("@intPedido", SqlDbType.Int);
                arrPar[0].Value = ent.intPedido;
                arrPar[1] = new SqlParameter("@intPartida", SqlDbType.Int);
                arrPar[1].Value = entDet.intPartida;
                arrPar[2] = new SqlParameter("@intProducto", SqlDbType.Int);
                arrPar[2].Value = entDet.intProducto;
                arrPar[3] = new SqlParameter("@strHoraEntrega", SqlDbType.VarChar);
                arrPar[3].Value = entDet.strHoraEntrega;
                arrPar[4] = new SqlParameter("@dblCantidad", SqlDbType.Decimal);
                arrPar[4].Value = entDet.dblCantidad;
                arrPar[5] = new SqlParameter("@dblPrecio", SqlDbType.Decimal);
                arrPar[5].Value = entDet.dblPrecio;
                arrPar[6] = new SqlParameter("@bitServicioBombeo", SqlDbType.Bit);
                arrPar[6].Value = blnServicioBombeo;
                arrPar[7] = new SqlParameter("@intCliente", SqlDbType.Int);
                arrPar[7].Value = ent.intCliente;

                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "usp_tbPedidoDet_Valida", arrPar).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion ValidarPedidoDet

        public static DataTable GetListPedidoDet(int intPedido, int intPartida)
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[2]
                {
                  new SqlParameter("@intPedido", SqlDbType.Int),
                  null
                };
                sqlParameterArray[0].Value = (object)intPedido;
                sqlParameterArray[1] = new SqlParameter("@intPartida", SqlDbType.Int);
                sqlParameterArray[1].Value = (object)intPartida;
                dataTable = SqlHelper.ExecuteDataset(Base.ConnectionString, CommandType.StoredProcedure, "usp_tbPedidoDet_Sel", sqlParameterArray).Tables[0];
            }
            catch
            {
            }
            return dataTable;
        }

        public static DataTable GetListPedidoDetServicios(int intPedido, int intPartida)
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[2]
                {
                  new SqlParameter("@intPedido", SqlDbType.Int),
                  null
                };
                sqlParameterArray[0].Value = (object)intPedido;
                sqlParameterArray[1] = new SqlParameter("@intPartida", SqlDbType.Int);
                sqlParameterArray[1].Value = (object)intPartida;
                dataTable = SqlHelper.ExecuteDataset(Base.ConnectionString, CommandType.StoredProcedure, "usp_tbPedidoDetServicios_Sel", sqlParameterArray).Tables[0];
            }
            catch
            {
            }
            return dataTable;
        }

        public static DataTable GetListVendedores(int intCliente)
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[2]
                {
                  new SqlParameter("@intCliente", SqlDbType.Int),
                  null
                };
                sqlParameterArray[0].Value = (object)intCliente;
                dataTable = SqlHelper.ExecuteDataset(Base.ConnectionString, CommandType.StoredProcedure, "usp_tbPedido_SelVendedores", sqlParameterArray).Tables[0];
            }
            catch
            {
            }
            return dataTable;
        }

        public static DataTable GetListClientes()
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = SqlHelper.ExecuteDataset(Base.ConnectionString, CommandType.StoredProcedure, "usp_tbPedido_ListClientes").Tables[0];
            }
            catch
            {
            }
            return dataTable;
        }

        public static int SavePedido(Entity_Pedido entPedido)
        {
            int result = 0;
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[28];
                sqlParameterArray[0] = new SqlParameter("@intPedido", SqlDbType.Int);
                sqlParameterArray[0].Value = (object)entPedido.intPedido;
                sqlParameterArray[1] = new SqlParameter("@Project_Code", SqlDbType.NVarChar);
                sqlParameterArray[1].Value = (object)entPedido.Project_Code;
                sqlParameterArray[2] = new SqlParameter("@PO_Num", SqlDbType.NVarChar);
                sqlParameterArray[2].Value = (object)entPedido.PO_Num;
                sqlParameterArray[3] = new SqlParameter("@intTipoPrecio", SqlDbType.Int);
                sqlParameterArray[3].Value = (object)entPedido.intTipoPrecio;
                sqlParameterArray[4] = new SqlParameter("@strCliente", SqlDbType.VarChar);
                sqlParameterArray[4].Value = (object)entPedido.strCliente;
                sqlParameterArray[5] = new SqlParameter("@strEncargado", SqlDbType.VarChar);
                sqlParameterArray[5].Value = (object)entPedido.strEncargado;
                sqlParameterArray[6] = new SqlParameter("@strTelefonos", SqlDbType.VarChar);
                sqlParameterArray[6].Value = (object)entPedido.strTelefonos;
                sqlParameterArray[7] = new SqlParameter("@strCalle", SqlDbType.VarChar);
                sqlParameterArray[7].Value = (object)entPedido.strCalle;
                sqlParameterArray[8] = new SqlParameter("@strColonia", SqlDbType.VarChar);
                sqlParameterArray[8].Value = (object)entPedido.strColonia;
                sqlParameterArray[9] = new SqlParameter("@strCalleEntre", SqlDbType.VarChar);
                sqlParameterArray[9].Value = (object)entPedido.strCalleEntre;
                sqlParameterArray[10] = new SqlParameter("@strCalleEntre2", SqlDbType.VarChar);
                sqlParameterArray[10].Value = (object)entPedido.strCalleEntre2;
                sqlParameterArray[11] = new SqlParameter("@dblPorcentajeIva", SqlDbType.Decimal);
                sqlParameterArray[11].Value = (object)entPedido.dblPorcentajeIva;
                sqlParameterArray[12] = new SqlParameter("@dblSubtotal", SqlDbType.Decimal);
                sqlParameterArray[12].Value = (object)entPedido.dblSubtotal;
                sqlParameterArray[13] = new SqlParameter("@dblIva", SqlDbType.Decimal);
                sqlParameterArray[13].Value = (object)entPedido.dblIva;
                sqlParameterArray[14] = new SqlParameter("@dblTotal", SqlDbType.Decimal);
                sqlParameterArray[14].Value = (object)entPedido.dblTotal;
                sqlParameterArray[15] = new SqlParameter("@Order_Status", SqlDbType.NVarChar);
                sqlParameterArray[15].Value = (object)entPedido.Order_Status;
                sqlParameterArray[16] = new SqlParameter("@State_Code", SqlDbType.NVarChar);
                sqlParameterArray[16].Value = (object)entPedido.State_Code;
                sqlParameterArray[17] = new SqlParameter("@City", SqlDbType.NVarChar);
                sqlParameterArray[17].Value = (object)entPedido.City;
                sqlParameterArray[18] = new SqlParameter("@Postal_Code", SqlDbType.NVarChar);
                sqlParameterArray[18].Value = (object)entPedido.Postal_Code;
                sqlParameterArray[19] = new SqlParameter("@Delivery_Instructions", SqlDbType.NVarChar);
                sqlParameterArray[19].Value = (object)entPedido.Delivery_Instructions;
                sqlParameterArray[20] = new SqlParameter("@intCliente", SqlDbType.Int);
                sqlParameterArray[20].Value = (object)entPedido.intCliente;
                sqlParameterArray[21] = new SqlParameter("@strMaquina", SqlDbType.NVarChar);
                sqlParameterArray[21].Value = (object)entPedido.strMaquina;
                sqlParameterArray[22] = new SqlParameter("@datFechaEntrega", SqlDbType.DateTime);
                sqlParameterArray[22].Value = (object)entPedido.datFechaEntrega;
                sqlParameterArray[23] = new SqlParameter("@strElemento", SqlDbType.VarChar);
                sqlParameterArray[23].Value = (object)entPedido.strElemento;
                sqlParameterArray[24] = new SqlParameter("@strEmail", SqlDbType.VarChar);
                sqlParameterArray[24].Value = (object)entPedido.strEmail;
                sqlParameterArray[25] = new SqlParameter("@strVendedor", SqlDbType.VarChar);
                sqlParameterArray[25].Value = (object)entPedido.strVendedor;
                sqlParameterArray[26] = new SqlParameter("@intFactura", SqlDbType.Int);
                sqlParameterArray[26].Value = (object)entPedido.intFactura;

                sqlParameterArray[27] = new SqlParameter("@dblPorcentaje", SqlDbType.Decimal);
                sqlParameterArray[27].Value = (object)entPedido.dblPorcentaje;

                int.TryParse(SqlHelper.ExecuteScalar(Base.ConnectionString, CommandType.StoredProcedure, "usp_tbPedido_Save", sqlParameterArray).ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static void SavePedidoPrecio(int intPedido)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[6];
                sqlParameterArray[0] = new SqlParameter("@intPedido", SqlDbType.Int);
                sqlParameterArray[0].Value = (object)intPedido;
                SqlHelper.ExecuteNonQuery(Base.ConnectionString, CommandType.StoredProcedure, "usp_tbPedido_SaveTotal", sqlParameterArray).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeletePedidoDet(int intCliente, int intPedido, int intPartida)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[3];
                sqlParameterArray[0] = new SqlParameter("@intCliente", SqlDbType.Int);
                sqlParameterArray[0].Value = (object)intCliente;
                sqlParameterArray[1] = new SqlParameter("@intPedido", SqlDbType.Int);
                sqlParameterArray[1].Value = (object)intPedido;
                sqlParameterArray[2] = new SqlParameter("@intPartida", SqlDbType.Int);
                sqlParameterArray[2].Value = (object)intPartida;
                SqlHelper.ExecuteNonQuery(Base.ConnectionString, CommandType.StoredProcedure, "usp_tbPedidoDet_Del", sqlParameterArray).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool SavePedidoDetServicio(int intPedido, int intPartida, int intServicio, int intLista, Decimal decCantidad, bool bGrua, int intTipoBomba, Decimal decPorcentaje)
        {
            bool flag = true;
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[8];
                sqlParameterArray[0] = new SqlParameter("@intPedido", SqlDbType.Int);
                sqlParameterArray[0].Value = (object)intPedido;
                sqlParameterArray[1] = new SqlParameter("@intPartida", SqlDbType.Int);
                sqlParameterArray[1].Value = (object)intPartida;
                sqlParameterArray[2] = new SqlParameter("@intServicio", SqlDbType.Int);
                sqlParameterArray[2].Value = (object)intServicio;
                sqlParameterArray[3] = new SqlParameter("@intLista", SqlDbType.Int);
                sqlParameterArray[3].Value = (object)intLista;
                sqlParameterArray[4] = new SqlParameter("@decCantidad", SqlDbType.Decimal);
                sqlParameterArray[4].Value = (object)decCantidad;
                sqlParameterArray[5] = new SqlParameter("@bGrua", SqlDbType.Bit);
                sqlParameterArray[5].Value = (object)bGrua;
                sqlParameterArray[6] = new SqlParameter("@intTipoBomba", SqlDbType.Int);
                sqlParameterArray[6].Value = (object)intTipoBomba;

                sqlParameterArray[7] = new SqlParameter("@dblPorcentaje", SqlDbType.Decimal);
                sqlParameterArray[7].Value = (object)decPorcentaje;

                SqlHelper.ExecuteNonQuery(Base.ConnectionString, CommandType.StoredProcedure, "usp_tbPedidoDet_Servicio_Ins", sqlParameterArray).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }

        public static int SavePedidoDet(Entity_PedidoDet entPedidoDet, bool blnServicioBombeo)
        {
            int result = 0;
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[8];
                sqlParameterArray[0] = new SqlParameter("@intPedido", SqlDbType.Int);
                sqlParameterArray[0].Value = (object)entPedidoDet.intPedido;
                sqlParameterArray[1] = new SqlParameter("@intPartida", SqlDbType.Int);
                sqlParameterArray[1].Value = (object)entPedidoDet.intPartida;
                sqlParameterArray[2] = new SqlParameter("@intProducto", SqlDbType.Int);
                sqlParameterArray[2].Value = (object)entPedidoDet.intProducto;
                sqlParameterArray[3] = new SqlParameter("@strHoraEntrega", SqlDbType.VarChar);
                sqlParameterArray[3].Value = (object)entPedidoDet.strHoraEntrega;
                sqlParameterArray[4] = new SqlParameter("@item_Code", SqlDbType.NVarChar);
                sqlParameterArray[4].Value = (object)entPedidoDet.item_Code;
                sqlParameterArray[5] = new SqlParameter("@dblCantidad", SqlDbType.Decimal);
                sqlParameterArray[5].Value = (object)entPedidoDet.dblCantidad;
                sqlParameterArray[6] = new SqlParameter("@dblPrecio", SqlDbType.Decimal);
                sqlParameterArray[6].Value = (object)entPedidoDet.dblPrecio;
                sqlParameterArray[7] = new SqlParameter("@bitServicioBombeo", SqlDbType.Bit);
                sqlParameterArray[7].Value = (object)blnServicioBombeo;
                int.TryParse(SqlHelper.ExecuteScalar(Base.ConnectionString, CommandType.StoredProcedure, "usp_tbPedidoDet_Save", sqlParameterArray).ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static void SavePedidoEstatus(int intCliente, int intPedido, int intEstatus)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[3]
                {
                  new SqlParameter("@intCliente", SqlDbType.Int),
                  null,
                  null
                };
                sqlParameterArray[0].Value = (object)intCliente;
                sqlParameterArray[1] = new SqlParameter("@intPedido", SqlDbType.Int);
                sqlParameterArray[1].Value = (object)intPedido;
                sqlParameterArray[2] = new SqlParameter("@intEstatus", SqlDbType.Int);
                sqlParameterArray[2].Value = (object)intEstatus;
                SqlHelper.ExecuteNonQuery(Base.ConnectionString, CommandType.StoredProcedure, "usp_tbPedido_SaveEstatus", sqlParameterArray).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SetRead(int intPedido)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
                  new SqlParameter("@intPedido", SqlDbType.Int)
                };
                sqlParameterArray[0].Value = (object)intPedido;
                SqlHelper.ExecuteNonQuery(Base.ConnectionString, CommandType.StoredProcedure, "usp_tbPedido_SaveRead", sqlParameterArray).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeletePedidoServicio(int intPedido, int intPartida)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[2];
                sqlParameterArray[0] = new SqlParameter("@intPedido", SqlDbType.Int);
                sqlParameterArray[0].Value = intPedido;
                sqlParameterArray[1] = new SqlParameter("@intPartida", SqlDbType.Int);
                sqlParameterArray[1].Value = intPartida;
                SqlHelper.ExecuteNonQuery(Base.ConnectionString, CommandType.StoredProcedure, "usp_tbPedidoDet_Servicio_Del", sqlParameterArray).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ValidarPedido(Entity_Pedido ent, bool blnServicioBombeo)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[4];
                sqlParameterArray[0] = new SqlParameter("@intPedido", SqlDbType.Int);
                sqlParameterArray[0].Value = ent.intPedido;
                sqlParameterArray[1] = new SqlParameter("@datFechaEntrega", SqlDbType.DateTime);
                sqlParameterArray[1].Value = ent.datFechaEntrega;
                sqlParameterArray[2] = new SqlParameter("@blnServicioBombeo", SqlDbType.Bit);
                sqlParameterArray[2].Value = blnServicioBombeo;
                sqlParameterArray[3] = new SqlParameter("@intCliente", SqlDbType.Int);
                sqlParameterArray[3].Value = ent.intCliente;
                SqlHelper.ExecuteNonQuery(Base.ConnectionString, CommandType.StoredProcedure, "usp_tbPedido_Valida", sqlParameterArray).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        #region Create
        static Entity_Pedido CreateObject(IDataReader drd)
        {
            Entity_Pedido oEnt = new Entity_Pedido();
            oEnt.Project_Code = (string)drd["Project_Code"];
            oEnt.intPedido = (int)drd["intPedido"];
            oEnt.PO_Num = (string)drd["PO_Num"];
            oEnt.intTipoPrecio = (int)drd["intTipoPrecio"];
            oEnt.strCliente = (string)drd["strCliente"];
            oEnt.strEncargado = (string)drd["strEncargado"];
            oEnt.strTelefonos = (string)drd["strTelefonos"];
            oEnt.strCalle = (string)drd["strCalle"];
            oEnt.strColonia = (string)drd["strColonia"];
            oEnt.strCalleEntre = (string)drd["strCalleEntre"];
            oEnt.strCalleEntre2 = (string)drd["strCalleEntre2"];
            oEnt.dblPorcentajeIva = (decimal)drd["dblPorcentajeIva"];
            oEnt.dblSubtotal = (decimal)drd["dblSubtotal"];
            oEnt.dblIva = (decimal)drd["dblIva"];
            oEnt.dblTotal = (decimal)drd["dblTotal"];
            oEnt.Order_Status = (string)drd["Order_Status"];
            oEnt.State_Code = (string)drd["State_Code"];
            oEnt.City = (string)drd["City"];
            oEnt.Postal_Code = (string)drd["Postal_Code"];
            oEnt.Delivery_Instructions = (string)drd["Delivery_Instructions"];
            oEnt.intCliente = (int)drd["intCliente"];
            oEnt.strMaquina = (string)drd["strMaquina"];
            oEnt.datFechaAlta = (DateTime)drd["datFechaAlta"];
            return oEnt;
        }
        #endregion
    }
}
