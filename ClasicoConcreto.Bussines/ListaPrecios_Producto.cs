// Decompiled with JetBrains decompiler
// Type: ClasicoConcreto.Bussines.ListaPrecios_Producto
// Assembly: ClasicoConcreto.Bussines, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 738E42E7-E7AA-4142-A88C-A56E90652464
// Assembly location: C:\Users\jsoto\Desktop\Recuperacion Concreto\ClasicoConcreto.Bussines.dll

using ClasicoConcreto.DataAccess;
using ClasicoConcreto.Entity;
using System.Data;

namespace ClasicoConcreto.Bussines
{
    public class ListaPrecios_Producto
    {
        public DataTable GetListProductos(int intLista)
        {
            return DACListaPrecios_Producto.GetListProductos(intLista);
        }

        public void Save(Entity_ListaPrecios_Producto entListaPrecios)
        {
            DACListaPrecios_Producto.Save(entListaPrecios);
        }
    }
}
