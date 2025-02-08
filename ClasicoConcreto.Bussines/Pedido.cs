using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasicoConcreto.DataAccess;
using ClasicoConcreto.Entity;
using System.Data;
namespace ClasicoConcreto.Bussines
{
    public class Pedido
    {

        #region GetList
        public DataTable GetList(int intCliente, DateTime datFechaInicio, DateTime datFechaFin)
        {
            return DACPedido.GetList(intCliente, datFechaInicio, datFechaFin);
        }
        #endregion

        #region GetListByDate
        public DataTable GetListByDate(int intCliente, DateTime datFechaIni, DateTime datFechaFin)
        {
            return DACPedido.GetListByDate(intCliente, datFechaIni, datFechaFin);
        }
        #endregion

        #region GetListSinFactura
        public DataTable GetListSinFactura(DateTime datFechaIni, DateTime datFechaFin)
        {
            return DACPedido.GetListSinFactura(datFechaIni, datFechaFin);
        }
        #endregion

        #region GetListSinFactura
        public DataTable GetListSinFactura(string strCliente)
        {
            return DACPedido.GetListSinFactura(strCliente);
        }
        #endregion

        #region GetPedido
        public DataTable GetPedido(int intCliente, int intPedido)
        {
            return DACPedido.GetPedido(intCliente, intPedido);
        }
        #endregion

        #region GetListPedidoDet
        public DataTable GetListPedidoDet(int intPedido, int intPartida)
        {
            return DACPedido.GetListPedidoDet(intPedido, intPartida);
        }
        #endregion

        #region GetListPedidoDetServicios
        public DataTable GetListPedidoDetServicios(int intPedido, int intPartida)
        {
            return DACPedido.GetListPedidoDetServicios(intPedido, intPartida);
        }
        #endregion

        #region GetListVendedores
        public DataTable GetListVendedores(int intCliente)
        {
            return DACPedido.GetListVendedores(intCliente);
        }
        #endregion

        #region GetListClientes
        public DataTable GetListClientes()
        {
            return DACPedido.GetListClientes();
        }
        #endregion



        #region SavePedido
        public int SavePedido(Entity_Pedido entPedido)
        {
            return DACPedido.SavePedido(entPedido);
        }
        #endregion

        #region SavePedidoPrecio
        public void SavePedidoPrecio(int intPedido)
        {
            DACPedido.SavePedidoPrecio(intPedido);
        }
        #endregion

        #region DeletePedido
        public void DeletePedidoDet(int intCliente ,int intPedido, int intPartida)
        {
            DACPedido.DeletePedidoDet(intCliente, intPedido, intPartida);
        }
        #endregion DeletePedidoDet

        #region SavePedidoDet
        public int SavePedidoDet(Entity_PedidoDet entPedidoDet, bool blnServicioBombeo)
        {
            return DACPedido.SavePedidoDet(entPedidoDet, blnServicioBombeo);
        }
        #endregion

        #region EnviarPedido
        public void EnviarPedido(int intCliente, int intPedido)
        {
            DACPedido.SavePedidoEstatus(intCliente, intPedido, 1);
        }
        #endregion EnviarPedido

        #region CancelarPedido
        public void CancelarPedido(int intCliente, int intPedido)
        {
            DACPedido.SavePedidoEstatus(intCliente, intPedido, 2);
        }
        #endregion CancelarPedido

        #region SetRead
        public void SetRead(int intPedido)
        {
            DACPedido.SetRead(intPedido);
        }
        #endregion SetRead


        #region SavePedidoDetServicio
        public bool SavePedidoDetServicio(int intPedido, int intPartida, int intServicio, int intLista, decimal decCantidad, bool bGrua, int intTipoBombeo, decimal decProcentaje)
        {
            return DACPedido.SavePedidoDetServicio(intPedido, intPartida, intServicio, intLista, decCantidad, bGrua, intTipoBombeo, decProcentaje);
        }
        #endregion

        #region DeletePedidoServicio
        public void DeletePedidoServicio(int intPedido, int intPartida)
        {
            DACPedido.DeletePedidoServicio(intPedido, intPartida);
        }
        #endregion

        #region ValidarPedido
        public void ValidarPedido(Entity_Pedido ent, bool blnServicioBombeo)
        {
            DACPedido.ValidarPedido(ent, blnServicioBombeo);
        }
        #endregion


        #region ValidarPedidoDet
        public void ValidarPedidoDet(Entity_Pedido ent, Entity_PedidoDet entDet, bool blnServicioBombeo)
        {
            DACPedido.ValidarPedidoDet(ent, entDet, blnServicioBombeo);
        }
        #endregion

    }
}
