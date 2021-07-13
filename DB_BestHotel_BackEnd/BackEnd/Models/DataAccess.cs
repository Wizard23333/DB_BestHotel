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
        //添加成功返回user_id，添加失败返回“0”
        public static string AddUser(string user_id, string UserPassword)
        {
            OracleCommand Insert = DB.CreateCommand();
            Insert.CommandText = "insert into USERS values(:user_id,:UserPassword)";
            Insert.Parameters.Add(new OracleParameter(":user_id", user_id));
            Insert.Parameters.Add(new OracleParameter(":UserPassword", UserPassword));
            int Result = Insert.ExecuteNonQuery();
            if (Result == 1)
            {
                return user_id;
            }
            else return "0";
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
                a.user_name = Ord.GetValue(1).ToString();
                a.user_password = Ord.GetValue(2).ToString();
                a.user_type = (Ord.GetValue(3).ToString() == "1" ? "client":"staff");
            }
            CloseConn();
            return a;
        }

        //查询数据库中所有订单信息
        public static List<Order> DisplayOrderInfo(string query = "*")
        {
            List<Order> orders = new List<Order>();
            OracleCommand Search = DB.CreateCommand();
            string Strsql="select " + query + "from room_order";
            Search.CommandText = Strsql;
            OracleDataReader Ord = Search.ExecuteReader();
            while (Ord.Read())
            {
                orders.Add(new Order { order_id = Ord.GetValue(0).ToString(), client_id = Ord.GetValue(1).ToString(), order_date = Ord.GetValue(3).ToString(), amount = (int)Ord.GetValue(4), state = (int)Ord.GetValue(5) });
            }
            return orders;
        }

        //修改订单信息
        public static int ModifyOrderInfo(string order_id)
        {
            OracleCommand Update = DB.CreateCommand();
            Update.CommandText = "update room_order set state=3 where order_id=:order_id";
            Update.Parameters.Add(new OracleParameter(":order_id", order_id));
            int Result = Update.ExecuteNonQuery();
            return Result;
        }

        //查询订单信息
        public static Order QueryOrderInfo(string order_id)
        {
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select * from room_order where order_id=:order_id";
            Search.Parameters.Add(new OracleParameter(":order_id", order_id));
            OracleDataReader Ord = Search.ExecuteReader();
            Order order = new Order();
            while (Ord.Read())
            {
                order.order_id = Ord.GetValue(0).ToString();
                order.client_id = Ord.GetValue(1).ToString();
                order.order_date = Ord.GetValue(3).ToString();
                order.amount = (int)Ord.GetValue(4);
                order.state = (int)Ord.GetValue(5);
                
            }
            return order;
        }


        public static void AddRoomOrder(string user_id, string order_date,string room_type,string client_telephonenumber=null,int stay_time=1)
        {
            OracleCommand Insert = DB.CreateCommand();
            Insert.CommandText = "insert into room_order values(:user_id,:order_date,:room_type,:client_telephonenumber,:stay_time)";
            Insert.Parameters.Add(new OracleParameter(":user_id", user_id));
            Insert.Parameters.Add(new OracleParameter(":order_date", order_date));
            Insert.Parameters.Add(new OracleParameter(":room_type", room_type));
            Insert.Parameters.Add(new OracleParameter(":client_telephonenumber", client_telephonenumber));
            Insert.Parameters.Add(new OracleParameter(":stay_time", stay_time));
            Insert.ExecuteNonQuery();
        }


        public static void AddDishOrder(string user_id, string dish_name,  int number = 1)
        {
            OracleCommand Insert = DB.CreateCommand();
            Insert.CommandText = "insert into dish_order values(:user_id,:dish_name,:number)";
            Insert.Parameters.Add(new OracleParameter(":user_id", user_id));
            Insert.Parameters.Add(new OracleParameter(":dish_name", dish_name));
            Insert.Parameters.Add(new OracleParameter(":number", number));
            Insert.ExecuteNonQuery();
        }

        public static List<Room> DisplayRoomInfo()
        {
            List<Room> rooms = new List<Room>();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select * from room left outer natural join check_in left outer natural join client";
            OracleDataReader Ord = Search.ExecuteReader();
            while (Ord.Read())
            {
                //待修改顺序
                rooms.Add(new Room { room_id = Ord.GetValue(0).ToString(), room_price =(int) Ord.GetValue(1), room_type = Ord.GetValue(2).ToString(), room_condition = Ord.GetValue(3).ToString(), name = Ord.GetValue(5).ToString(),phone= Ord.GetValue(5).ToString() ,time= Ord.GetValue(5).ToString() });
            }
            return rooms;
        }


        public static int ModifyRoomInfo(string room_id,int room_price,int room_type,string room_condition)
        {
            OracleCommand Update = DB.CreateCommand();
            Update.CommandText = "update room set room_id=:room_id,room_price=:room_price,room_type=:room_type,room_condition=:room_condition  where room_id=:room_id";
            Update.Parameters.Add(new OracleParameter(":room_id", room_id));
            Update.Parameters.Add(new OracleParameter(":room_price", room_price));
            Update.Parameters.Add(new OracleParameter(":room_type", room_type));
            Update.Parameters.Add(new OracleParameter(":room_condition", room_condition));
            int Result = Update.ExecuteNonQuery();
            return Result;
        }

        public static int DeleteRoomInfo(string room_id)
        {
            OracleCommand Delete = DB.CreateCommand();
            Delete.CommandText = "delete from room where room_id=:room_id";
            Delete.Parameters.Add(new OracleParameter(":room_id", room_id));
            int Result = Delete.ExecuteNonQuery();
            return Result;
        }

        public static int AddRoomInfo(string room_condition, int room_price, int room_type )
        {
            OracleCommand Insert = DB.CreateCommand();
            Insert.CommandText = "insert into room output inserted.room_id values(sys_guid(),:room_price,:room_type,:room_condition)";
            Insert.Parameters.Add(new OracleParameter(":room_price", room_price));
            Insert.Parameters.Add(new OracleParameter(":room_type", room_type));
            Insert.Parameters.Add(new OracleParameter(":room_condition", room_condition));
            int Result=Insert.ExecuteNonQuery();
            return Result;
        }

        public static List<RoomTypeInfo> DisplayRoomTypeInfo()
        {
            List<RoomTypeInfo> roomtypes = new List<RoomTypeInfo>();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select room_type,room_price,room_url,room_explain,sum(room_condition) as room_workable from room natural join room_type group by room_type,room_price,room_url,room_explain";
            OracleDataReader Ord = Search.ExecuteReader();
            while (Ord.Read())
            {
                roomtypes.Add(new RoomTypeInfo { room_type = Ord.GetValue(0).ToString(), room_price = (int)Ord.GetValue(1), room_url = Ord.GetValue(2).ToString(), room_explain = Ord.GetValue(3).ToString(), room_workable = (int)Ord.GetValue(4) });
            }
            return roomtypes;
        }

        public static int AddRoomTypeInfo(int room_type, string room_url, string room_explain)
        {
            OracleCommand Insert = DB.CreateCommand();
            Insert.CommandText = "insert into room_type values(:room_type,:room_url,:room_explain)";
            Insert.Parameters.Add(new OracleParameter(":room_type", room_type));
            Insert.Parameters.Add(new OracleParameter(":room_url", room_url));
            Insert.Parameters.Add(new OracleParameter(":room_explain", room_explain));
            int Result = Insert.ExecuteNonQuery();
            return Result;
        }

        public static int DeleteRoomTypeInfo(int room_type)
        {
            OracleCommand Delete = DB.CreateCommand();
            Delete.CommandText = "delete from room_type where room_type=:room_type";
            Delete.Parameters.Add(new OracleParameter(":room_type", room_type));
            int Result = Delete.ExecuteNonQuery();
            return Result;
        }
    }
}
