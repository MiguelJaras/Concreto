using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace ClasicoConcreto.Entity
{
    public class Entity_Servidor 
    {
        private string _strSQLUser;
        private string _strSQLPass;
        private string _strSQLIP;


        public string StrSQLUser
        {
            get
            {
                return _strSQLUser;
            }
            set
            {
                _strSQLUser = value;
            }
        }

        public string StrSQLPass
        {
            get
            {
                return _strSQLPass;
            }
            set
            {
                _strSQLPass = value;
            }
        }

        public string StrSQLIP
        {
            get
            {
                return _strSQLIP;
            }
            set
            {
                _strSQLIP = value;
            }
        }
    }
}
