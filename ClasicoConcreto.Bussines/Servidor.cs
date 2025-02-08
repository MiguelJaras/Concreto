using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasicoConcreto.Entity;
using ClasicoConcreto.DataAccess;
using System.Data;

namespace ClasicoConcreto.Bussines
{
    public class Servidor
    {
        public Entity_Servidor Credenciales()
        {
            return DACServidor.Credenciales();
        }
     
    }
}
