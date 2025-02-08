using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess
{
    public class DACServidor : Base
    {
        public static Entity_Servidor Credenciales()
        {
            Entity_Servidor obj = new Entity_Servidor();
            obj.StrSQLIP = "192.168.80.5";
            obj.StrSQLUser = "sa";
            obj.StrSQLPass = "M1rf3l";
            //obj.StrSQLIP = "192.168.100.10";
            //obj.StrSQLUser = "vetec";
            //obj.StrSQLPass = "vetec";
            return obj;
        }
    }
}
