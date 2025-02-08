using System.Text;
using ClasicoConcreto.Entity;
using ClasicoConcreto.DataAccess;
using System.Data;
using System.Collections.Generic;
namespace ClasicoConcreto.Bussines
{
    public class Horas
    {
        #region GetList
        public List<Entity_Horas> GetList()
        {
            return DACHoras.GetList();
        }
        #endregion
    }
}
