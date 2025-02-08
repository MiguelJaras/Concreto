using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasicoConcreto.Entity;
using ClasicoConcreto.DataAccess;
using System.Data;
namespace ClasicoConcreto.Bussines
{
    public class Facturas
    {
        #region GetList
        public DataTable GetList()
        {
            return DACFacturas.GetList();
        }
        #endregion

        #region GetListSinPedido
        public DataTable GetListSinPedido()
        {
            return DACFacturas.GetListSinPedido();
        }
        #endregion

        #region GetList
        public DataTable GetList(bool bEnvioEmail)
        {
            return DACFacturas.GetListByEstatus(bEnvioEmail);
        }
        #endregion


        #region GetListPedidosSinEnviar
        public DataTable GetListPedidosSinEnviar(string strFactura)
        {
            return DACFacturas.GetListPedidosSinEnviar(strFactura);
        }
        #endregion GetListPedidosSinEnviar

        #region Get
        public DataTable Get(Entity_Facturas entFactura)
        {
            return DACFacturas.Get(entFactura);
        }
        #endregion

        #region GetList
        public DataTable GetListClientes()
        {
            return DACFacturas.GetListClientes();
        }
        #endregion



        #region Save
        public void Save(Entity_Facturas entFactura)
        {
            DACFacturas.Save(entFactura);
        }
        #endregion

        #region SavePedidos
        public void SavePedidos(Entity_Facturas entFactura)
        {
            DACFacturas.SavePedidos(entFactura);
        }
        #endregion


        #region SaveEstatusEmail
        public void SaveEstatusEmail(int intEmpresa, string strFactura, string strSerie)
        {
            DACFacturas.SaveEstatusEmail(intEmpresa, strFactura, strSerie);
        }
        #endregion


        #region Delete
        public void Delete(Entity_Facturas entFactura)
        {
            DACFacturas.Delete(entFactura);
        }
        #endregion

        #region SaveFacturaDescarga
        public void SaveFacturaDescarga(Entity_Facturas entFactura)
        {
            DACFacturas.SaveFacturaDescarga(entFactura);
        }
        #endregion

        #region Valida
        public void Valida(Entity_Facturas entFactura)
        {
            DACFacturas.Valida(entFactura);
        }
        #endregion



    }
}
