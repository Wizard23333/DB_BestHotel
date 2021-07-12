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
            string connString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = orcl.mshome.net))); Persist Security Info=True;User ID=system;Password=Bac66238#tan;";
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
        public static bool IsUserExist(string user_id, string user_password)
        {
            int Count;
            CreateConn();
            OracleCommand CMD = DB.CreateCommand();
            CMD.CommandText = "select count(*) from USERS where user_id=:user_id and user_password=:user_password";
            CMD.Parameters.Add(new OracleParameter(":user_id", user_id));
            CMD.Parameters.Add(new OracleParameter(":user_password", user_password));
            Count = Convert.ToInt32(CMD.ExecuteScalar());
            CloseConn();
            if (Count == 0)
                return false;
            else
                return true;

        }

        //初步专用测试
        public static List<User> test()
        {

            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select * from EMPLOYEE";
            OracleDataReader Ord = Search.ExecuteReader();
            List<User> a = new List<User>();
            while (Ord.Read())
            {
                a.Add(new User { user_id = Ord.GetValue(0).ToString(), user_password = Ord.GetValue(1).ToString() });
            }
            return a;
        }

        

        //向Users表中增加一个新用户(注册)
        //添加成功返回true，添加失败返回“0”
        public static bool AddUser(User req)
        {
            CreateConn();
            OracleCommand Insert = DB.CreateCommand();
            Insert.CommandText = "insert into USERS values(:USER_ID,:USER_PASSWORD,:USER_TYPE,:SECURITY_Q,:S_Q_ANSWER,:USER_NAME)";
            Insert.Parameters.Add(new OracleParameter(":USER_ID", req.user_id));
            Insert.Parameters.Add(new OracleParameter(":USER_PASSWORD", req.user_password));
            Insert.Parameters.Add(new OracleParameter(":USER_TYPE", req.user_type));
            Insert.Parameters.Add(new OracleParameter(":SECURITY_Q", req.security_q));
            Insert.Parameters.Add(new OracleParameter(":S_Q_ANSWER", req.s_q_answer));
            Insert.Parameters.Add(new OracleParameter(":USER_NAME", req.user_name));
            int Result = Insert.ExecuteNonQuery();
            CloseConn();
            if (Result == 1)
            {
                return true;
            }
            else return false;
        }

        //向Client表中增加一个新客户(注册)
        //添加成功返回1，添加失败返回0
        public static bool AddClient(Client req)
        {
            CreateConn();
            OracleCommand Insert = DB.CreateCommand();
            Insert.CommandText = "insert into CLIENT values(:CLIENT_ID,:CLIENT_NAME,:CLIENT_SEX,to_date(:CLIENT_BIRTHDAY,'YYYY-MM-DD'),:CLIENT_TELEPHONENUMBER,:CLIENT_IDENTITY_CARD_NUMBER)";
            Insert.Parameters.Add(new OracleParameter(":CLIENT_ID", req.client_id));
            Insert.Parameters.Add(new OracleParameter(":CLIENT_NAME", req.client_name));
            Insert.Parameters.Add(new OracleParameter(":CLIENT_SEX", req.client_sex));
            Insert.Parameters.Add(new OracleParameter(":CLIENT_BIRTHDAY", req.client_birthday));
            Insert.Parameters.Add(new OracleParameter(":CLIENT_TELEPHONENUMBER", req.client_mobile));
            Insert.Parameters.Add(new OracleParameter(":CLIENT_IDENTITY_CARD_NUMBER", req.client_idCard));
            int Result = Insert.ExecuteNonQuery();
            CloseConn();
            if (Result == 1)
            {
                return true;
            }
            else return false;
        }

        //向Staff表中增加一个新客户(注册)
        //添加成功返回true，添加失败返回false
        public static bool AddStaff(Staff req)
        {
            CreateConn();
            OracleCommand Insert = DB.CreateCommand();
            Insert.CommandText = "insert into STAFF values(	:STAFF_ID,:STAFF_NAME,:STAFF_SEX,:STAFF_AGE	,:STAFF_IDENTITY_CARD_NUMBER,:STAFF_ADDRESS	,:STAFF_DEPARTMENT,:STAFF_POSITION,to_date(:STAFF_ENTRY_DATE,'YYYY-MM-DD'),:STAFF_SALARY)";
            Insert.Parameters.Add(new OracleParameter("	:STAFF_ID	", req.staff_id));
            Insert.Parameters.Add(new OracleParameter("	:STAFF_NAME	", req.staff_name));
            Insert.Parameters.Add(new OracleParameter("	:STAFF_SEX	", req.staff_sex));
            Insert.Parameters.Add(new OracleParameter("	:STAFF_AGE	", req.staff_age));
            Insert.Parameters.Add(new OracleParameter("	:STAFF_IDENTITY_CARD_NUMBER	", req.staff_identity_card_number));
            Insert.Parameters.Add(new OracleParameter("	:STAFF_ADDRESS	", req.staff_address));
            Insert.Parameters.Add(new OracleParameter("	:STAFF_DEPARTMENT	", req.staff_department));
            Insert.Parameters.Add(new OracleParameter("	:STAFF_POSITION	", req.staff_position));
            Insert.Parameters.Add(new OracleParameter("	:STAFF_ENTRY_DATE	", req.staff_entry_date));
            Insert.Parameters.Add(new OracleParameter("	:STAFF_SALARY	", req.staff_salary));
            int Result = Insert.ExecuteNonQuery();
            CloseConn();
            if (Result == 1)
            {
                return true;
            }
            else return false;
        }

        //查找个人信息
        public static User FindUserInfo(string user_id)
        {
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select * from USERS where user_id=:user_id";
            Search.Parameters.Add(new OracleParameter(":user_id", user_id));
            OracleDataReader Ord = Search.ExecuteReader();
            User a = new User ();
            while (Ord.Read())
            {
                a.user_id = Ord.GetValue(0).ToString();
                a.user_name = Ord.GetValue(5).ToString();
                a.user_password = Ord.GetValue(1).ToString();
                a.user_type = (Ord.GetValue(2).ToString() == "1" ? "client":"staff");
                a.security_q = Ord.GetValue(3).ToString();
                a.s_q_answer = Ord.GetValue(4).ToString();
            }
            CloseConn();
            return a;
        }
        //查找客户信息
        public static List<Client> FindClientInfo()
        {
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select * from CLIENT";
            OracleDataReader Ord = Search.ExecuteReader();
            List<Client> a = new List<Client>();
            while (Ord.Read())
            {
                a.Add(new Client
                {
                    client_id = Ord.GetValue(0).ToString() ,  client_name = Ord.GetValue(1).ToString() ,  client_sex = Ord.GetValue(2).ToString() ,  client_birthday = Ord.GetValue(3).ToString() ,  client_mobile = Ord.GetValue(4).ToString()  , client_idCard = Ord.GetValue(5).ToString()
                });
            }
            return a;
        }
        public static Client FindClientInfo(string client_id)
        {
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select * from CLIENT where CLIENT_ID=:client_id";
            Search.Parameters.Add(new OracleParameter(":client_id", client_id));
            OracleDataReader Ord = Search.ExecuteReader();
            Client a = new Client();
            while (Ord.Read())
            {
                a.client_id = Ord.GetValue(0).ToString();
                a.client_name = Ord.GetValue(1).ToString();
                a.client_sex = Ord.GetValue(2).ToString();
                a.client_birthday = Ord.GetValue(3).ToString();
                a.client_mobile = Ord.GetValue(4).ToString();
                a.client_idCard = Ord.GetValue(5).ToString();
            }
            return a;
        }
        public static bool AlterUserPassword(string id, string password)
        {
            CreateConn();
            OracleCommand CMD = DB.CreateCommand();
            CMD.CommandText = "update USERS set USER_PASSWORD=:password where USER_ID=:id";
            CMD.Parameters.Add(new OracleParameter(":password", password));
            CMD.Parameters.Add(new OracleParameter(":id", id));
            int Result = CMD.ExecuteNonQuery();
            CloseConn();
            if (Result != 1)
                return false;
            else
                return true;
        }
        public static bool AlterClient(string id,string phone)
        {
            CreateConn();
            OracleCommand CMD = DB.CreateCommand();
            CMD.CommandText = "update CLIENT set CLIENT_TELEPHONENUMBER=:phone where CLIENT_ID=:id";
            CMD.Parameters.Add(new OracleParameter(":phone", phone));
            CMD.Parameters.Add(new OracleParameter(":id", id));
            int Result = CMD.ExecuteNonQuery();
            CloseConn();
            if (Result != 1)
                return false;
            else
                return true;

        }
    }
}
