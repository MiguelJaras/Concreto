using ClasicoConcreto.DataAccess;
using System;
using System.Data;

namespace ClasicoConcreto.Bussines
{
    public class ProgramacionConcreto
    {
        #region GetList
        public DataTable GetList(DateTime datFecha, int intCliente)
        {
            return DACProgramacionConcreto.GetList(datFecha, intCliente);
        }
        #endregion


        #region Save
        public bool Save(int intRequisicionDet, string strHoraAnt, string strHora)
        {
            return DACProgramacionConcreto.Save(intRequisicionDet, strHoraAnt, strHora);
        }
        #endregion


        #region SavePlantaRemision
        public bool SavePlantaRemision(int intRequisicionDet, int intPlanta, string strRemision)
        {
            return DACProgramacionConcreto.SavePlantaRemision(intRequisicionDet, intPlanta, strRemision);
        }
        #endregion

    }
}
