using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;


namespace BackEnd.Models
{
    public class DataAccess
    {
        public static OracleConnection DB;

        //建立数据库连接
        public static void CreateConn()
        {
            string connString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = orcl.mshome.net))); Persist Security Info=True;User ID=C##TEST;Password=C##TESTpwd;";
            DB = new OracleConnection(connString);
            DB.Open();
        }

        //关闭数据库连接
        public static void CloseConn()
        {
            DB.Close();
        }

        //与功能点1：登录与注册相关的操作

        //在USERS表中查询用户、密码是否错误(登录时使用)
        //密码或用户名错误返回false；密码和用户名正确返回true
        public static bool IsUserExist(string UserID, string Password)
        {
            int Count;
            OracleCommand CMD = DB.CreateCommand();
            CMD.CommandText = "select count(*) from USERS where UserID=:UserID and UserPassword=:Password";
            CMD.Parameters.Add(new OracleParameter(":UserID", UserID));
            CMD.Parameters.Add(new OracleParameter(":Password", Password));
            Count = Convert.ToInt32(CMD.ExecuteScalar());
            if (Count == 0)
                return false;
            else
                return true;

        }

        //初步专用测试
        public static string test()
        {

            string id="";
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select * from EMPLOYEE";
            OracleDataReader Ord = Search.ExecuteReader();
            while (Ord.Read())
            {
                id  = Ord.GetValue(0).ToString();
            }
            
            return id;
           

        }
        //向Users表中增加一个新用户(注册)
        //添加成功返回UserID，添加失败返回“0”
        public static string AddUser(string UserID, string UserPassword)
        {
            OracleCommand Insert = DB.CreateCommand();
            Insert.CommandText = "insert into USERS values(:UserID,:UserPassword)";
            Insert.Parameters.Add(new OracleParameter(":UserID", UserID));
            Insert.Parameters.Add(new OracleParameter(":UserPassword", UserPassword));
            int Result = Insert.ExecuteNonQuery();
            if (Result == 1)
            {
                return UserID;
            }
            else return "0";
        }

        //查找个人信息
        public static User FindUserInfo(string UserID)
        {
            User U = new User();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select * from USERS where UserID=:UserID";
            Search.Parameters.Add(new OracleParameter(":UserID", UserID));
            OracleDataReader Ord = Search.ExecuteReader();
            while (Ord.Read())
            {
                U.UserID = UserID;
                U.UserPassword = Ord.GetValue(1).ToString();
            }
            return U;
        }

    }
}
