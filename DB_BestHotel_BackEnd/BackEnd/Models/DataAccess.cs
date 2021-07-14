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
        //查找所有客户信息
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

        //查客户信息
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

        //修改用户密码
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
        //修改客户信息
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
        public static string FindFacilityStatus(string facilty_id, string room_id)
        {
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select FACILITY_CONDITION from ROOM_FACILITY_CONDITION where FACILITY_ID=:facilty_id and ROOM_ID=:room_id";
            Search.Parameters.Add(new OracleParameter(":facilty_id", facilty_id));
            Search.Parameters.Add(new OracleParameter(":room_id", room_id));
            OracleDataReader Ord = Search.ExecuteReader();
            string a="";
            while (Ord.Read())
            {
                a = Ord.GetValue(0).ToString();
            }
            return a;
        }
        public static List<FaciltyStaffResponse> FindFacilityStaff(string facility_id)
        {
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select staff_id from repair where facility_id=:facility_id";
            Search.Parameters.Add(new OracleParameter(":facilty_id", facility_id));
            OracleDataReader Ord = Search.ExecuteReader();
            List<FaciltyStaffResponse> a = new List<FaciltyStaffResponse>();
            while (Ord.Read())
            {
                a.Add(new FaciltyStaffResponse { staff_id = Ord.GetValue(0).ToString()});
            }
            return a;
        }
        public static List<StaffFaciltyResponse> FindStaffFacility(string staff_id)
        {
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select facility_id from repair where staff_id=:staff_id";
            Search.Parameters.Add(new OracleParameter(":staff_id", staff_id));
            OracleDataReader Ord = Search.ExecuteReader();
            List<StaffFaciltyResponse> a = new List<StaffFaciltyResponse>();
            while (Ord.Read())
            {
                a.Add(new StaffFaciltyResponse { facility_id = Ord.GetValue(0).ToString() });
            }
            return a;
        }
        public static List<FaciltyListResponse> FindFacilityInfo()
        {
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select * from facility NATURAL JOIN repair NATURAL JOIN room_facility_condition";
            //Search.Parameters.Add(new OracleParameter(":staff_id", staff_id));
            OracleDataReader Ord = Search.ExecuteReader();
            List<FaciltyListResponse> a = new List<FaciltyListResponse>();
            while (Ord.Read())
            {
                int pos=a.FindIndex(o => ((FaciltyListResponse)o).facility_id == Ord["FACILITY_ID"].ToString());
                if(pos<0)
                    a.Add(new FaciltyListResponse { facility_id = Ord["FACILITY_ID"].ToString(),facility_name=Ord["FACILITY_NAME"].ToString(),staff_id= Ord["STAFF_ID"].ToString(), children= new List<MonitorRoom>() { new MonitorRoom() { room_id= Ord["ROOM_ID"].ToString() } } });
                else
                    a[pos].children.Add(new MonitorRoom() { room_id = Ord["ROOM_ID"].ToString() });
            }  
            return a;
        }
        //查所有监控信息
        public static List<MonitorRequest> FindMonitorInfo()
        {
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select CAMERA_ID,ROOM_ID from monitoring_facilities_room group by CAMERA_ID,ROOM_ID";
            OracleDataReader Ord = Search.ExecuteReader();
            List<MonitorRequest> a = new List<MonitorRequest>();
            while (Ord.Read())
            {
                int pos = a.FindIndex(o => ((MonitorRequest)o).monitor_id == Ord["CAMERA_ID"].ToString());
                if (pos < 0)
                    a.Add(new MonitorRequest { monitor_id = Ord["CAMERA_ID"].ToString(), rooms = new List<MonitorRoom>() { new MonitorRoom() { room_id = Ord["ROOM_ID"].ToString() } } });
                else
                    a[pos].rooms.Add(new MonitorRoom() { room_id = Ord["ROOM_ID"].ToString() });
            }
            return a;
        }

        //删除监控信息
        public static bool DelMonitor(MonitorRequest value)
        {
            CreateConn();
            OracleCommand CMD = DB.CreateCommand();
            CMD.CommandText = "delete from monitoring_facilities_room where CAMERA_ID=:camera_id";
            CMD.Parameters.Add(new OracleParameter(":camera_id", value.monitor_id));
            int Result = CMD.ExecuteNonQuery();
            CloseConn();
            if (Result <0)
                return false;
            else
                return true;
        }
        public static bool AddMonitor(MonitorRequest value)
        {
            CreateConn();
            int Result = 0;
            int Count = value.rooms.Count();
            for (int i = 0; i < Count; i++)
            {
                OracleCommand CMD = DB.CreateCommand();
                CMD.CommandText = "insert into monitoring_facilities_room values(:room_id,:camera_id)";
                CMD.Parameters.Add(new OracleParameter(":room_id", value.rooms[i].room_id));
                CMD.Parameters.Add(new OracleParameter(":camera_id", value.monitor_id));
                Result += CMD.ExecuteNonQuery();
            }
            CloseConn();
            if (Result <= 0)
                return false;
            else
                return true;
        }
        public static List<ParkingMsg> ShowParkingInfo()
        {
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select * from Parking";
            OracleDataReader Ord = Search.ExecuteReader();
            List<ParkingMsg> a = new List<ParkingMsg>();
            while (Ord.Read())
            {
                a.Add(new ParkingMsg { parking_lot_id = Ord["PARKING_LOT_ID"].ToString(), user_id = Ord["USER_ID"].ToString(), car_number = Ord["CAR_NUMBER"].ToString() });
            }
            CloseConn();
            return a;
        }
        public static bool AlertParking(ParkingMsg value)
        {
            CreateConn();
            OracleCommand Update = DB.CreateCommand();
            Update.CommandText = "update PARKING set user_id=:user_id,car_number=:car_number where parking_lot_id=:parking_lot_id";
            Update.Parameters.Add(new OracleParameter(":user_id", value.user_id));
            Update.Parameters.Add(new OracleParameter(":car_number", value.car_number));
            Update.Parameters.Add(new OracleParameter(":parking_lot_id", value.parking_lot_id));
            int Result = Update.ExecuteNonQuery();
            CloseConn();
            if (Result < 0)
                return false;
            else
                return true;

        }
        public static bool AddParking(string parking_lot_id)
        {
            CreateConn();
            OracleCommand CMD = DB.CreateCommand();
            CMD.CommandText = "insert into PARKING values(:parking_lot_id,:user_id,:car_number)";
            CMD.Parameters.Add(new OracleParameter(":parking_lot_id", parking_lot_id));
            CMD.Parameters.Add(new OracleParameter(":user_id", null));
            CMD.Parameters.Add(new OracleParameter(":car_number", null));
            int Result = CMD.ExecuteNonQuery();
            CloseConn();
            if (Result !=1)
                return false;
            else
                return true;

        }
        public static bool DelParking(string parking_lot_id)
        {
            CreateConn();
            OracleCommand CMD = DB.CreateCommand();
            CMD.CommandText = "delete from PARKING where PARKING_LOT_ID=:parking_lot_id";
            CMD.Parameters.Add(new OracleParameter(":parking_lot_id", parking_lot_id));
            int Result = CMD.ExecuteNonQuery();
            CloseConn();
            if (Result < 0)
                return false;
            else
                return true;

        }
        public static ParkingMsg FindParkingInfo(string park_id)
        {
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select * from Parking where PARKING_LOT_ID=:park_id";
            Search.Parameters.Add(new OracleParameter(":park_id", park_id));
            OracleDataReader Ord = Search.ExecuteReader();
            ParkingMsg a = new ParkingMsg();
            while (Ord.Read())
            {
                a.parking_lot_id = Ord["PARKING_LOT_ID"].ToString();
                a.user_id = Ord["USER_ID"].ToString();
                a.car_number = Ord["CAR_NUMBER"].ToString();
            }
            CloseConn();
            return a;
        }
        //给定staff_id，查找是否存在此id 存在返回true 不存在返回false
        public static bool IsStaffExist(string staff_id)
        {
            int Count;
            CreateConn();
            OracleCommand CMD = DB.CreateCommand();
            CMD.CommandText = "select count(*) from STAFF where staff_id=:staff_id";
            CMD.Parameters.Add(new OracleParameter(":staff_id", staff_id));
            Count = Convert.ToInt32(CMD.ExecuteScalar());
            CloseConn();
            if (Count == 0)
                return false;
            else
                return true;

        }
        //给定staff_id，返回这个id对应的员工的所有信息
        public static Staff FindStaffInfo(string staff_id)
        {
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select * from STAFF where staff_id=:staff_id";
            Search.Parameters.Add(new OracleParameter(":staff_id", staff_id));
            OracleDataReader Ord = Search.ExecuteReader();
            Staff b = new Staff();
            while (Ord.Read())
            {
                b.staff_id = Ord.GetValue(0).ToString();
                b.staff_name = Ord.GetValue(1).ToString();
                b.staff_sex = Ord.GetValue(2).ToString();
                b.staff_age = Ord.GetValue(3).ToString();
                b.staff_identity_card_number = Ord.GetValue(4).ToString();
                b.staff_address = Ord.GetValue(5).ToString();
                b.staff_department = Ord.GetValue(6).ToString();
                b.staff_position = Ord.GetValue(7).ToString();
                b.staff_entry_date = Ord.GetValue(8).ToString();
                b.staff_salary = Ord.GetValue(9).ToString();
            }
            CloseConn();
            return b;
        }
        //给定职员id，返回上司id
        public static string FindUperId(string staff_id)
        {
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select leader_id from LEADER where staff_id=:staff_id";
            Search.Parameters.Add(new OracleParameter(":staff_id", staff_id));
            OracleDataReader Ord = Search.ExecuteReader();
            string c = "";
            while (Ord.Read())
            {
                c = Ord.GetValue(0).ToString();

            }
            CloseConn();
            return c;
        }
        //给定职员id 删除此id对应的员工信息
        public static string DeleteStaff(string staff_id)
        {
            CreateConn();
            OracleCommand Delete_Clean = DB.CreateCommand();
            Delete_Clean.CommandText = "delete from CLEAN where staff_id=:staff_id";
            Delete_Clean.Parameters.Add(new OracleParameter(":staff_id", staff_id));

            OracleCommand Delete = DB.CreateCommand();
            Delete.CommandText = "delete from STAFF where staff_id=:staff_id";
            Delete.Parameters.Add(new OracleParameter(":staff_id", staff_id));
            int Result = Delete.ExecuteNonQuery();
            CloseConn();
            if (Result == 1)
            {
                return staff_id;
            }
            else
            {
                return "0";
            }
        }
        //给定新的员工属性，更新对应id的员工属性和leader，成功返回true 不成功返回false
        public static bool StaffUpdate(StaffUpdateRequest value)
        {
            CreateConn();
            OracleCommand Update = DB.CreateCommand();
            Update.CommandText = "update STAFF set staff_id=:staff_id ,staff_name=:staff_name ,staff_sex=:staff_sex ,staff_age=:staff_age ,staff_identity_card_number=:staff_identity_card_number ,staff_address=:staff_address ,staff_department=:staff_department ,staff_position=:staff_position ,staff_entry_date=:staff_entry_date ,staff_salary=:staff_salary where staff_id=:staff_id";
            Update.Parameters.Add(new OracleParameter(":staff_id", value.staff_id));
            Update.Parameters.Add(new OracleParameter(":staff_name", value.staff_name));
            Update.Parameters.Add(new OracleParameter(":staff_sex", value.staff_sex));
            Update.Parameters.Add(new OracleParameter(":staff_age", value.staff_age));
            Update.Parameters.Add(new OracleParameter(":staff_identity_card_number", value.staff_identity_card_number));
            Update.Parameters.Add(new OracleParameter(":staff_address", value.staff_address));
            Update.Parameters.Add(new OracleParameter(":staff_department", value.staff_department));
            Update.Parameters.Add(new OracleParameter(":staff_position", value.staff_position));
            Update.Parameters.Add(new OracleParameter(":staff_entry_date", value.staff_entry_date));
            Update.Parameters.Add(new OracleParameter(":staff_salary", value.staff_salary));
            Update.Parameters.Add(new OracleParameter(":staff_id", value.staff_id));
            OracleCommand Update_Leader = DB.CreateCommand();
            Update_Leader.CommandText = "update LEADER set leader_id=:leader_id where staff_id=:staff_id";
            Update_Leader.Parameters.Add(new OracleParameter(":staff_id", value.staff_id));
            Update_Leader.Parameters.Add(new OracleParameter(":leader_id", value.leader_id));
            int Result = Update.ExecuteNonQuery();
            CloseConn();
            if (Result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        //
        public static List<ReturnAllResponse> ReturnAll()
        {
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select * from STAFF";
            OracleDataReader Ord = Search.ExecuteReader();
            List<ReturnAllResponse> a = new List<ReturnAllResponse>();


            int i = 0;
            OracleCommand Search_leader_id = DB.CreateCommand();
            Search_leader_id.CommandText = "select leader_id from LEADER where staff_id=:staff_id";
            while (Ord.Read())
            {
                a.Add(new ReturnAllResponse { staff_id = Ord.GetValue(0).ToString(), staff_name = Ord.GetValue(1).ToString(), staff_sex = Ord.GetValue(2).ToString(), staff_age = Ord.GetValue(3).ToString(), staff_identity_card_number = Ord.GetValue(4).ToString(), staff_address = Ord.GetValue(5).ToString(), staff_department = Ord.GetValue(6).ToString(), staff_position = Ord.GetValue(7).ToString(), staff_entry_date = Ord.GetValue(8).ToString(), staff_salary = Ord.GetValue(9).ToString() });

                Search_leader_id.Parameters.Add(new OracleParameter(":staff_id", a[i].staff_id));
                OracleDataReader Ord1 = Search_leader_id.ExecuteReader();
                while (Ord1.Read()) { a[i].leader_id = Ord.GetValue(0).ToString(); }
                i++;
            }
            CloseConn();
            return a;
        }
        //添加新员工
        public static string StaffAdd(StaffAddRequest value)
        {
            CreateConn();
            User data1 = new User { user_id = value.staff_id, user_name = value.staff_name, user_password = "88888888", user_type = "2", security_q = "我是谁", s_q_answer = value.staff_name };
            AddUser(data1);
            OracleCommand Add = DB.CreateCommand();
            Add.CommandText = "insert into STAFF value(:staff_id ,:staff_name ,:staff_sex ,:staff_age ,:staff_identity_card_number ,:staff_address ,:staff_department ,:staff_position ,:staff_entry_date ,:staff_salary)";
            Add.Parameters.Add(new OracleParameter(":staff_id", value.staff_id));
            Add.Parameters.Add(new OracleParameter(":staff_name", value.staff_name));
            Add.Parameters.Add(new OracleParameter(":staff_sex", value.staff_sex));
            Add.Parameters.Add(new OracleParameter(":staff_age", value.staff_age));
            Add.Parameters.Add(new OracleParameter(":staff_identity_card_number", value.staff_identity_card_number));
            Add.Parameters.Add(new OracleParameter(":staff_address", value.staff_address));
            Add.Parameters.Add(new OracleParameter(":staff_department", value.staff_department));
            Add.Parameters.Add(new OracleParameter(":staff_position", value.staff_position));
            Add.Parameters.Add(new OracleParameter(":staff_entry_date", value.staff_entry_date));
            Add.Parameters.Add(new OracleParameter(":staff_salary", value.staff_salary));
            OracleCommand Add_Leader = DB.CreateCommand();
            Add_Leader.CommandText = "insert into LEADER value(:staff_id,:leader_id)";
            Add_Leader.Parameters.Add(new OracleParameter(":staff_id", value.staff_id));
            Add_Leader.Parameters.Add(new OracleParameter(":leader_id", value.leader_id));
            int Result = Add.ExecuteNonQuery();
            CloseConn();
            if (Result == 1)
            {
                return value.staff_id;
            }
            else return "0";
        }
        //判断dish_id存不存在
        public static bool IsDishExist(string dish_id)
        {
            int Count;
            CreateConn();
            OracleCommand CMD = DB.CreateCommand();
            CMD.CommandText = "select count(*) from DISH where dish_id=:dish_id";
            CMD.Parameters.Add(new OracleParameter(":dish_id", dish_id));
            Count = Convert.ToInt32(CMD.ExecuteScalar());
            CloseConn();
            if (Count == 0)
                return false;
            else
                return true;
        }

        //返回房间订单列表
        public static ListInfo DisplayRoomOrderListInfo(string query = "*")
        {
            List<Order> orders = new List<Order>();
            OracleCommand Search = DB.CreateCommand();
            string Strsql;
            if (query == "" || query == "*")
                Strsql = "select order_id,client_id,order_date,amount,state,client_name,client_telephonenumber,client_identity_card_number,room_id,room_type,stay_time from room_order natural join client natural join room";
            else
            {
                Strsql = "select order_id,client_id,order_date,amount,state,client_name,client_telephonenumber,client_identity_card_number,room_id,room_type,stay_time from room_order natural join client natural join room where order_id like '%" + query + "%' or client_id like '%" + query + "%' or order_date like '%" + query + "%' or amount like '%" + query + "%' or state like '%" + query + "%' or room_id like '%" + query + "%' or client_name like '%" + query + "%' or client_telephonenumber like '%" + query + "%' or client_identity_card_number like '%" + query + "%' or room_type like '%" + query + "%' or stay_time like '%" + query + "%'";
            }
            Search.CommandText = Strsql;
            OracleDataReader Ord = Search.ExecuteReader();

            while (Ord.Read())
            {
                orders.Add(new Order { order_id = Ord.GetValue(0).ToString(), client_id = Ord.GetValue(1).ToString(), room_order_date = Ord.GetValue(2).ToString(), amount = (decimal)Ord.GetValue(3), state = (Int16)Ord.GetValue(4), client_name = Ord.GetValue(5).ToString(), client_telephonenumber = Ord.GetValue(6).ToString(), client_identity_card_number = Ord.GetValue(7).ToString(), room_id = Ord.GetValue(8).ToString(), room_type = Ord.GetValue(9).ToString(), stay_time = (Int16)Ord.GetValue(10) });
                
            }
            int total = orders.Count();
            ListInfo list = new ListInfo { total = total, list = orders };
            return list;
        }

        //返回菜品订单列表
        public static ListInfo DisplayDishOrderListInfo(string query = "*")
        {
            List<Order> orders = new List<Order>();
            OracleCommand Search = DB.CreateCommand();
            string Strsql;
            if (query == ""||query=="*")
                Strsql = "select order_id,client_id,order_date,amount,state,client_name,client_telephonenumber,dish_name from dish_order natural join client natural join dish";
            else
            {
                Strsql = "select order_id,client_id,order_date,amount,state,client_name,client_telephonenumber,dish_name from dish_order natural join client natural join dish where order_id like '%" + query + "%' or client_id like '%" + query + "%' or order_date like '%" + query + "%' or amount like '%" + query + "%' or state like '%" + query + "%' or dish_name like '%" + query + "%' or client_telephonenumber like '%" + query + "%' or client_name like '%" + query + "%'";
            }
            Search.CommandText = Strsql;
            OracleDataReader Ord = Search.ExecuteReader();

            while (Ord.Read())
            {
                orders.Add(new Order { order_id = Ord.GetValue(0).ToString(), client_id = Ord.GetValue(1).ToString(), dish_order_date = Ord.GetValue(2).ToString(), amount = (decimal)Ord.GetValue(3), state = (Int16)Ord.GetValue(4), client_name = Ord.GetValue(5).ToString(), client_telephonenumber = Ord.GetValue(6).ToString(), dish_name = Ord.GetValue(7).ToString() });

            }
            int total = orders.Count();
            ListInfo list = new ListInfo { total = total, list = orders };
            return list;
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
                order.room_order_date = Ord.GetValue(3).ToString();
                order.amount = (decimal)Ord.GetValue(4);
                order.state = (Int16)Ord.GetValue(5);

            }
            return order;
        }


        public static string AddRoomOrder(string client_id, string order_date, string room_type, int stay_time = 1, string client_telephonenumber = null)
        {
            if (client_telephonenumber != null)
            {
                OracleCommand Update = DB.CreateCommand();
                Update.CommandText = "update client set client_telephonenumber=:client_telephonenumber where client_id=:client_id";
                Update.Parameters.Add(new OracleParameter(":client_telephonenumber", client_telephonenumber));
                Update.Parameters.Add(new OracleParameter(":client_id", client_id));
                Update.ExecuteNonQuery();
            }
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select room_id from room where room_condition='空闲' and room_type=:room_type";
            Search.Parameters.Add(new OracleParameter(":room_type", room_type));
            string room_id = (string)Search.ExecuteScalar();
            Search.CommandText = "select room_price from room where room_condition='空闲' and room_type=:room_type";
            decimal room_price = (decimal)Search.ExecuteScalar();


            OracleCommand Insert = DB.CreateCommand();
            Insert.CommandText = "insert into room_order values(null,:client_id,:room_id,to_date(:order_date,'YYYY-MM-DD'),:amount,'0',:stay_time)";
            Insert.Parameters.Add(new OracleParameter(":client_id", client_id));
            Insert.Parameters.Add(new OracleParameter(":room_id", room_id));
            Insert.Parameters.Add(new OracleParameter(":order_date", order_date));
            Insert.Parameters.Add(new OracleParameter(":amount", room_price * stay_time));
            Insert.Parameters.Add(new OracleParameter(":stay_time", stay_time));
            int Result = Insert.ExecuteNonQuery();
            Search.CommandText = "select reg_form.currval from dual";
            string order_id = Search.ExecuteReader().ToString();
            if (Result == 1)
                return order_id;
            else return null;
        }
        


        public static List<Room> DisplayRoomInfo()
        {
            List<Room> rooms = new List<Room>();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select room_id,room_price,room_type,room_condition,client_name,client_telephonenumber,live_time,staff_id from room natural join clean natural left outer join check_in natural left outer join client";
            OracleDataReader Ord = Search.ExecuteReader();
            while (Ord.Read())
            {
                rooms.Add(new Room { room_id = Ord.GetValue(0).ToString(), room_price = (decimal)Ord.GetValue(1), room_type = Ord.GetValue(2).ToString(), room_condition = Ord.GetValue(3).ToString(), name = Ord.GetValue(4).ToString(), phone = Ord.GetValue(5).ToString(), time = Ord.GetValue(6).ToString(), staff_id = Ord.GetValue(7).ToString() });
            }
            return rooms;
        }
        public static string AddDishOrder(string client_id, string dish_name, int number = 1)
        {

            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select dish_id from dish where  dish_name=:dish_name";
            Search.Parameters.Add(new OracleParameter(":dish_name", dish_name));
            int dish_id = (int)Search.ExecuteScalar();
            Search.CommandText = "select dish_price from dish where  dish_name=:dish_name";
            Int64 dish_price = (Int64)Search.ExecuteScalar();
            OracleCommand Insert = DB.CreateCommand();
            Insert.CommandText = "insert into dish_order values(null,:client_id,:dish_id,to_date(:dish_date,'YYYY-MM-DD'),:amount,'0',:number)";
            Insert.Parameters.Add(new OracleParameter(":client_id", client_id));
            Insert.Parameters.Add(new OracleParameter(":dish_id", dish_id));
            Insert.Parameters.Add(new OracleParameter(":dish_date", DateTime.Now.ToString()));
            Insert.Parameters.Add(new OracleParameter(":amount", number * dish_price));
            Insert.Parameters.Add(new OracleParameter(":number", number));
            int Result = Insert.ExecuteNonQuery();
            Search.CommandText = "select reg_form.currval from dual";
            string order_id = Search.ExecuteReader().ToString();
            if (Result == 1)
                return order_id;
            else return null;
        }

        public static int ModifyRoomInfo(string room_id, int room_price, string room_type, string room_condition, string staff_id)
        {
            OracleCommand Update = DB.CreateCommand();
            Update.CommandText = "update room set room_id=:room_id,room_price=:room_price,room_type=:room_type,room_condition=:room_condition,staff_id=:staff_id  where room_id=:room_id";
            Update.Parameters.Add(new OracleParameter(":room_id", room_id));
            Update.Parameters.Add(new OracleParameter(":room_price", room_price));
            Update.Parameters.Add(new OracleParameter(":room_type", room_type));
            Update.Parameters.Add(new OracleParameter(":room_condition", room_condition));
            Update.Parameters.Add(new OracleParameter(":staff_id", staff_id));
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

        public static string AddRoomInfo(string room_condition, decimal room_price, string room_type)
        {
            OracleCommand Insert = DB.CreateCommand();
            Insert.CommandText = "insert into room values(null,:room_price,:room_type,:room_condition)";
            Insert.Parameters.Add(new OracleParameter(":room_price", room_price));
            Insert.Parameters.Add(new OracleParameter(":room_type", room_type));
            Insert.Parameters.Add(new OracleParameter(":room_condition", room_condition));
            int Result = Insert.ExecuteNonQuery();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select reg_form.currval from dual";
            string room_id = Search.ExecuteReader().ToString();
            if (Result == 1)
                return room_id;
            else return null;
        }

        public static List<RoomTypeInfo> DisplayRoomTypeInfo()
        {
            List<RoomTypeInfo> roomtypes = new List<RoomTypeInfo>();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select room_type,room_price,room_url,room_explain,count(*) from room natural full outer join room_type where room_condition='空闲' group by room_type,room_price,room_url,room_explain";
            OracleDataReader Ord = Search.ExecuteReader();
            while (Ord.Read())
            {
                roomtypes.Add(new RoomTypeInfo { room_type = Ord.GetValue(0).ToString(), room_price = (decimal)Ord.GetValue(1), room_url = Ord.GetValue(2).ToString(), room_explain = Ord.GetValue(3).ToString(), room_workable = (decimal)Ord.GetValue(4) });
            }
            return roomtypes;
        }

        public static int AddRoomTypeInfo(string room_type, string room_url, string room_explain)
        {
            OracleCommand Insert = DB.CreateCommand();
            Insert.CommandText = "insert into room_type values(:room_type,:room_url,:room_explain)";
            Insert.Parameters.Add(new OracleParameter(":room_type", room_type));
            Insert.Parameters.Add(new OracleParameter(":room_url", room_url));
            Insert.Parameters.Add(new OracleParameter(":room_explain", room_explain));
            int Result = Insert.ExecuteNonQuery();
            return Result;
        }

        public static int DeleteRoomTypeInfo(string room_type)
        {
            OracleCommand Delete = DB.CreateCommand();
            Delete.CommandText = "delete from room_type where room_type=:room_type";
            Delete.Parameters.Add(new OracleParameter(":room_type", room_type));
            int Result = Delete.ExecuteNonQuery();
            return Result;
        }
       
        //更新菜品
        public static bool UpdateDish(DishUpdateRequest value)
        {

            CreateConn();
            OracleCommand Update = DB.CreateCommand();
            Update.CommandText = "update DISH set dish_id=:dish_id ,dish_name=:dish_name ,dish_explain=:dish_explain ,dish_price=:dish_price where dish_id=:dish_id";
            Update.Parameters.Add(new OracleParameter(":dish_id", value.dish_id));
            Update.Parameters.Add(new OracleParameter(":dish_name", value.dish_name));
            Update.Parameters.Add(new OracleParameter(":dish_explain", value.dish_explain));
            Update.Parameters.Add(new OracleParameter(":dish_price", value.dish_price));
            Update.Parameters.Add(new OracleParameter(":dish_id", value.dish_id));
            int Result = Update.ExecuteNonQuery();
            CloseConn();
            if (Result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static List<DishInfo> ReturnAll_Dish()
        {
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select * from DISH";
            OracleDataReader Ord = Search.ExecuteReader();
            List<DishInfo> a = new List<DishInfo>();
            while (Ord.Read())
            {
                a.Add(new DishInfo { dish_id = Ord.GetValue(0).ToString(), dish_explain = Ord.GetValue(1).ToString(), dish_price = Ord.GetValue(2).ToString(), dish_name = Ord.GetValue(3).ToString() });
            }
            CloseConn();
            return a;
        }
        public static string DeleteDish(string dish_id)
        {
            CreateConn();
            OracleCommand Delete = DB.CreateCommand();
            Delete.CommandText = "delete from DISH where dish_id=:dish_id";
            Delete.Parameters.Add(new OracleParameter(":dish_id", dish_id));


            int Result = Delete.ExecuteNonQuery();
            CloseConn();
            if (Result == 1)
            {
                return dish_id;
            }
            else
            {
                return "0";
            }
        }
        public static string DishAdd(DishAddRequest value)
        {
            CreateConn();
            OracleCommand Add = DB.CreateCommand();
            Add.CommandText = "insert into DISH value(:dish_id,:dish_explain,:dish_price,:dish_name,:dish_picture)";
            Add.Parameters.Add(new OracleParameter(":dish_id", value.dish_id));
            Add.Parameters.Add(new OracleParameter(":dish_explain", value.dish_explain));
            Add.Parameters.Add(new OracleParameter(":dish_price", value.dish_price));
            Add.Parameters.Add(new OracleParameter(":dish_name", value.dish_name));
            Add.Parameters.Add(new OracleParameter(":dish_picture", value.dish_picture));
            int Result = Add.ExecuteNonQuery();
            CloseConn();
            if (Result == 1)
            {
                return value.dish_id;
            }
            else return "0";
        }
    
    }

    
}
