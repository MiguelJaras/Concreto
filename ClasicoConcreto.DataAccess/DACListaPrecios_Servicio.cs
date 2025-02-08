// Decompiled with JetBrains decompiler
// Type: ClasicoConcreto.DataAccess.DACListaPrecios_Servicio
// Assembly: ClasicoConcreto.DataAccess, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CFCDC63A-3FD2-45CE-8F98-DFD3169D6077
// Assembly location: C:\Users\jsoto\Desktop\Recuperacion Concreto\ClasicoConcreto.DataAccess.dll

using ClasicoConcreto.Entity;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ClasicoConcreto.DataAccess
{
    public class DACListaPrecios_Servicio : Base
    {
        public static DataTable GetList(int intLista)
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
                    new SqlParameter("@intListaPrecio", SqlDbType.Int)
                };
                sqlParameterArray[0].Value = (object) intLista;
                dataTable = SqlHelper.ExecuteDataset(Base.ConnectionString, CommandType.StoredProcedure, "usp_tbListaPrecios_Servicio_Sel", sqlParameterArray).Tables[0];
            }
            catch
            {
            }
            return dataTable;
        }

        public static void Save(Entity_ListaPrecios_Servicio entListaPrecio)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[5]
                {
                    new SqlParameter("@intLista", SqlDbType.Int),
                    null,
                    null,
                    null,
                    null
                };
                sqlParameterArray[0].Value = (object) entListaPrecio.intLista;
                sqlParameterArray[1] = new SqlParameter("@intServicio", SqlDbType.Int);
                sqlParameterArray[1].Value = (object) entListaPrecio.intServicio;
                sqlParameterArray[2] = new SqlParameter("@dblPrecio", SqlDbType.Decimal);
                sqlParameterArray[2].Value = (object) entListaPrecio.dblPrecio;
                sqlParameterArray[3] = new SqlParameter("@intClienteAlta", SqlDbType.Int);
                sqlParameterArray[3].Value = (object) entListaPrecio.intClienteAlta;
                sqlParameterArray[4] = new SqlParameter("@strMaquina", SqlDbType.VarChar);
                sqlParameterArray[4].Value = (object) entListaPrecio.strMaquina;
                SqlHelper.ExecuteNonQuery(Base.ConnectionString, CommandType.StoredProcedure, "usp_tbListaPrecios_Servicio_Save", sqlParameterArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static Entity_ListaPrecios_Servicio CreateObject(IDataReader drd)
        {
            return new Entity_ListaPrecios_Servicio()
            {
                intLista = (int) drd["intLista"],
                intServicio = (int) drd["intServicio"],
                dblPrecio = (Decimal) drd["dblPrecio"],
                datFechaAlta = (DateTime) drd["datFechaAlta"],
                intClienteAlta = (int) drd["intClienteAlta"],
                strMaquina = (string) drd["strMaquina"]
            };
        }
    }
}
