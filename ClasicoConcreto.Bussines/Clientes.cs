using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasicoConcreto.Entity;
using ClasicoConcreto.DataAccess;
using System.Data;

namespace ClasicoConcreto.Bussines
{
    public class Clientes
    {
        #region Login
        public Entity_Clientes Login(Entity_Clientes ent)
        {
            return DACClientes.Login(ent);
        }
        #endregion

        #region ChangePassword
        public bool ChangePassword(Entity_Clientes ent)
        {
            return DACClientes.ChangePassword(ent);
        }
        #endregion 

        #region GetList
        public DataTable GetList(int intCliente)
        {
            return DACClientes.GetList(intCliente);
        }
        #endregion

        #region Save
        public int Save(Entity_Clientes ent)
        {
            return DACClientes.Save(ent);
        }
        #endregion 

        #region Delete
        public void Delete(int intCliente)
        {
            DACClientes.Delete(intCliente);
        }
        #endregion

    }
}
