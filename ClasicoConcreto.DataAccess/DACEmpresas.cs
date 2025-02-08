using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using ClasicoConcreto.Entity;

namespace ClasicoConcreto.DataAccess
{
    public class DACEmpresas : Base
    {

        #region Get
        public static Entity_Empresas Get(Entity_Empresas entEmpresas)
        {
            IDataReader drd;
            Entity_Empresas objEnt = new Entity_Empresas();

            SqlParameter[] arrPar = new SqlParameter[2];
            arrPar[0] = new SqlParameter("@intEmpresa", SqlDbType.Int);
            arrPar[0].Value = entEmpresas.intEmpresa;

            arrPar[1] = new SqlParameter("@strRfc", SqlDbType.VarChar);
            arrPar[1].Value = entEmpresas.strRfc;

            try
            {
                drd = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "usp_tbEmpresas_Fill", arrPar);
                if (drd.Read())
                {
                    objEnt = (CreateObject(drd));
                }
            }
            catch
            {

            }

            return objEnt;
        }
        #endregion Get

        #region GetList
        public static List<Entity_Empresas> GetList(Entity_Empresas entEmpresas)
        {
            IDataReader drd;
            List<Entity_Empresas> lstEmpresas = new List<Entity_Empresas>();

            SqlParameter[] arrPar = new SqlParameter[2];
            arrPar[0] = new SqlParameter("@intEmpresa", SqlDbType.Int);
            arrPar[0].Value = entEmpresas.intEmpresa;

            arrPar[1] = new SqlParameter("@strRfc", SqlDbType.VarChar);
            arrPar[1].Value = entEmpresas.strRfc;

            try
            {
                drd = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "usp_tbEmpresas_Fill", arrPar);
                while (drd.Read())
                {
                    lstEmpresas.Add(CreateObject(drd));
                }
            }
            catch
            {

            }

            return lstEmpresas;
        }
        #endregion Get



        #region Create
        static Entity_Empresas CreateObject(IDataReader drd)
        {
            Entity_Empresas oEnt = new Entity_Empresas();
            oEnt.intEmpresa = (int)drd["intEmpresa"];
            oEnt.strNombre = (string)drd["strNombre"];
            oEnt.strNombreCorto = (string)drd["strNombreCorto"];
            oEnt.intGrupo = (int)drd["intGrupo"];
            oEnt.strDireccion = (string)drd["strDireccion"];
            oEnt.strColonia = (string)drd["strColonia"];
            oEnt.strDelegacion = (string)drd["strDelegacion"];
            oEnt.intEstado = (int)drd["intEstado"];
            oEnt.intCiudad = (int)drd["intCiudad"];
            oEnt.strRfc = (string)drd["strRfc"];
            oEnt.strRegImss = (string)drd["strRegImss"];
            oEnt.strCodigoPostal = (string)drd["strCodigoPostal"];
            oEnt.strResponsable = (string)drd["strResponsable"];
            oEnt.strRfcResponsable = (string)drd["strRfcResponsable"];
            oEnt.intTipoMoneda = (int)drd["intTipoMoneda"];
            oEnt.intLogo = (int)drd["intLogo"];
            oEnt.datFechaAlta = (DateTime)drd["datFechaAlta"];
            oEnt.strUsuarioAlta = (string)drd["strUsuarioAlta"];
            oEnt.strMaquinaAlta = (string)drd["strMaquinaAlta"];
            oEnt.datFechaMod = (DateTime)drd["datFechaMod"];
            oEnt.strUsuarioMod = (string)drd["strUsuarioMod"];
            oEnt.strMaquinaMod = (string)drd["strMaquinaMod"];
            return oEnt;
        }
        #endregion
    }
}
