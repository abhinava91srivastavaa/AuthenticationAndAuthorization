using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Globalization;
using System.Xml;
using System.Data.SqlClient;
using System.Data.Entity;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Database = Microsoft.Practices.EnterpriseLibrary.Data.Database;

namespace AuthenticationAndAuthorization.Models
{
    public class LogINDAL
    {
        public static DataSet get_loginDetails(string UserEmail, int userType)
        {
            DataSet ds = new DataSet();
            try
            {
                Database myDataBase = DatabaseFactory.CreateDatabase("Abhinava");
                DbCommand myCommand = myDataBase.GetStoredProcCommand("CheckEmailID");
                myDataBase.AddInParameter(myCommand, "@UserEmail", DbType.String, UserEmail);
                myDataBase.AddInParameter(myCommand, "@UserType", DbType.Int32, userType);
                //myDataBase.AddInParameter(myCommand, "@UserID", DbType.Int32, userID);
                ds = myDataBase.ExecuteDataSet(myCommand);
                return ds;
            }
            catch { return ds; }

        }
    }
}