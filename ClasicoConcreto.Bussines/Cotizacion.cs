// Decompiled with JetBrains decompiler
// Type: ClasicoConcreto.Bussines.Cotizacion
// Assembly: ClasicoConcreto.Bussines, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 738E42E7-E7AA-4142-A88C-A56E90652464
// Assembly location: C:\Users\jsoto\Desktop\Recuperacion Concreto\ClasicoConcreto.Bussines.dll

using ClasicoConcreto.DataAccess;
using ClasicoConcreto.Entity;
using System;
using System.Data;

namespace ClasicoConcreto.Bussines
{
    public class Cotizacion
    {
        public DataTable Get(int intCliente, int intCotizacion)
        {
            return DACCotizacion.Get(intCliente, intCotizacion);
        }

        public DataTable GetDet(int intCotizacion)
        {
            return DACCotizacion.GetDet(intCotizacion);
        }

        public DataTable GetList(int intCliente, DateTime datFechaInicio, DateTime datFechaFin)
        {
            return DACCotizacion.GetList(intCliente, datFechaInicio, datFechaFin);
        }

        public int Save(Entity_Cotizacion ent)
        {
            return DACCotizacion.Save(ent);
        }

        public int SaveDet(Entity_CotizacionDet ent)
        {
            return DACCotizacion.SaveDet(ent);
        }

        public void DeleteProducto(int intCotizacion, int intPartida)
        {
            DACCotizacion.DeleteProducto(intCotizacion, intPartida);
        }
    }
}
