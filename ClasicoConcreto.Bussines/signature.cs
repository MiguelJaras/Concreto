using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasicoConcreto.Entity;
using ClasicoConcreto.DataAccess;
using System.Data;

namespace ClasicoConcreto.Bussines
{
    public class signature
    {


        #region GetReportSignature
        public DataSet GetReportSignature()
        {
            return DACSignature.GetReportSignature();
        }
        #endregion


    }
}
