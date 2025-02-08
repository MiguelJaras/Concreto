using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Collections;
using System.Data;

namespace ClasicoConcreto.DataAccess
{
    public static class Extension
    {
        public static List<T> ConvertToList<T>(this IDataReader dr) where T : new()
        {
            Type objEntity = typeof(T);
            List<T> lstEntity = new List<T>();
            Hashtable hashtable = new Hashtable();
            PropertyInfo[] properties = objEntity.GetProperties();
            foreach (PropertyInfo info in properties)
            {
                hashtable[info.Name.ToUpper()] = info;
            }
            while (dr.Read())
            {
                T newObject = new T();
                for (int index = 0; index < dr.FieldCount; index++)
                {
                    PropertyInfo info = (PropertyInfo)
                                        hashtable[dr.GetName(index).ToUpper()];
                    if ((info != null) && info.CanWrite)
                    {
                        info.SetValue(newObject, dr.GetValue(index), null);
                    }
                }
                lstEntity.Add(newObject);
            }
            dr.Close();
            return lstEntity;
        }
    }
}
