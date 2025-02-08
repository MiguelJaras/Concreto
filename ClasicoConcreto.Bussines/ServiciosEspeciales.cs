// Decompiled with JetBrains decompiler
// Type: ClasicoConcreto.Bussines.ServiciosEspeciales
// Assembly: ClasicoConcreto.Bussines, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 738E42E7-E7AA-4142-A88C-A56E90652464
// Assembly location: C:\Users\jsoto\Desktop\Recuperacion Concreto\ClasicoConcreto.Bussines.dll

using ClasicoConcreto.DataAccess;
using ClasicoConcreto.Entity;
using System.Collections.Generic;
using System.Data;

namespace ClasicoConcreto.Bussines
{
    public class ServiciosEspeciales
    {
        public DataTable GetList()
        {
            return DACServiciosEspeciales.GetList();
        }

        public List<Entity_ServiciosEspeciales> GetListPrecio(int intLista,decimal decPorcentaje)
        {
            return DACServiciosEspeciales.GetListPrecio(intLista,decPorcentaje);
        }

        public List<Entity_ServiciosEspeciales> GetListPrecioPorcentaje(int intLista,decimal decPorcentaje)
        {
            return DACServiciosEspeciales.GetListPrecioPorcentaje(intLista, decPorcentaje);
        }


        public void Save(Entity_ServiciosEspeciales entServicio)
        {
            DACServiciosEspeciales.Save(entServicio);
        }

        public void Delete(int intServicio)
        {
            DACServiciosEspeciales.Delete(intServicio);
        }
    }
}
