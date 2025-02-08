using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Collections;
using System.Data;

/// <summary>
/// Descripción breve de Extensiones
/// </summary>
namespace ExtensionMethods
{
    public static class Extension
    {
        public static DateTime Next(this DateTime dtFrom, DayOfWeek dayOfWeek)
        {
            int start = (int)dtFrom.DayOfWeek;
            int target = (int)dayOfWeek;
            if (target <= start)
                target += 7;
            return dtFrom.AddDays(target - start);
        }

        public static int NextDay(this DateTime dtValue, string[] values)
        {
            int intDayToday = (int)dtValue.DayOfWeek;
            int intNextDay = 2; //Default
            int[,] arrDias = new int[7, 2];
            bool bRound = false;
            for (int i = intDayToday + 1; i <= 7; i++)
            {
                if (i > 6) //Si es mayora los 7 dias se reinicia el contador
                {
                    if (bRound) //Si ya realizo un ciclo se termina el for
                        break;
                    else
                    {
                        i = 0;
                        bRound = true;
                    }
                }
                else
                {
                    int index = Array.IndexOf(values, i.ToString());
                    if (index != -1) // si se encuentra en el arreglo se termina el for
                    {
                        int.TryParse(values[index], out intNextDay);
                        break;
                    }
                }

            }
            return intNextDay;
        }

        #region extensions integer
        public static bool ContainsValue(this int value, string[] values)
        {
            foreach (string strValue in values)
            {
                int intVal = 0;
                int.TryParse(strValue, out intVal);
                if (value == intVal)
                {
                    return true;
                }
            }
            return false;
        }


        #endregion

        public static bool Between(this TimeSpan tsCurrent, TimeSpan tsStart, TimeSpan tsEnd)
        {
            bool bln = false;
            if ((tsCurrent >= tsStart) && (tsCurrent <= tsEnd))
            {
                bln = true;
            }

            return bln;
        }



    }
}