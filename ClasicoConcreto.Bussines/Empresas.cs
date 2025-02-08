using ClasicoConcreto.DataAccess;
using ClasicoConcreto.Entity;
using System.Collections.Generic;

namespace ClasicoConcreto.Bussines
{
    public class Empresas
    {
        #region Get
        public Entity_Empresas GetByRfc(string rfc)
        {
            Entity_Empresas entEmpresas = new Entity_Empresas();
            entEmpresas.strRfc = rfc;
            return DACEmpresas.Get(entEmpresas);
        }
        #endregion

        #region GetList
        public List<Entity_Empresas> GetList(int intEmpresa)
        {
            Entity_Empresas entEmpresas = new Entity_Empresas();
            entEmpresas.intEmpresa = intEmpresa;
            return DACEmpresas.GetList(entEmpresas);
        }
        #endregion 

    }
}
