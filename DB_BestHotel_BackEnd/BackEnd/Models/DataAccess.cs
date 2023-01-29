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
            string connString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = orcl.mshome.net))); Persist Security Info=True;User ID=system;Password=;";
            DB = new OracleConnection(connString);
            DB.Open();
        }

        //关闭数据库连接
        public static void CloseConn()
        {
            DB.Close();
        }

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
        public static List<UserModel> test()
        {

            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select * from EMPLOYEE";
            OracleDataReader Ord = Search.ExecuteReader();
            List<UserModel> a = new List<UserModel>();
            while (Ord.Read())
            {
                a.Add(new UserModel { user_id = Ord.GetValue(0).ToString(), user_password = Ord.GetValue(1).ToString() });
            }
            return a;
        }



        //向Users表中增加一个新用户(注册)
        //添加成功返回true，添加失败返回“0”
        public static bool AddUser(UserModel req)
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
        public static UserModel FindUserInfo(string user_id)
        {
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select * from USERS where user_id=:user_id";
            Search.Parameters.Add(new OracleParameter(":user_id", user_id));
            OracleDataReader Ord = Search.ExecuteReader();
            UserModel a = new UserModel();
            while (Ord.Read())
            {
                a.user_id = Ord.GetValue(0).ToString();
                a.user_name = Ord.GetValue(5).ToString();
                a.user_password = Ord.GetValue(1).ToString();
                a.user_type = (Ord.GetValue(2).ToString() == "1" ? "client" : "staff");
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
                    client_id = Ord.GetValue(0).ToString(),
                    client_name = Ord.GetValue(1).ToString(),
                    client_sex = Ord.GetValue(2).ToString(),
                    client_birthday = Ord.GetValue(3).ToString(),
                    client_mobile = Ord.GetValue(4).ToString(),
                    client_idCard = Ord.GetValue(5).ToString()
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
        public static bool AlterClient(string id, string phone)
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
        //查找设备状态
        public static string FindFacilityStatus(string facilty_id, string room_id)
        {
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select FACILITY_CONDITION from ROOM_FACILITY_CONDITION where FACILITY_ID=:facilty_id and ROOM_ID=:room_id";
            Search.Parameters.Add(new OracleParameter(":facilty_id", facilty_id));
            Search.Parameters.Add(new OracleParameter(":room_id", room_id));
            OracleDataReader Ord = Search.ExecuteReader();
            string a = "";
            while (Ord.Read())
            {
                a = Ord.GetValue(0).ToString();
            }
            return a;
        }
        //修改设备状态
        public static bool AlertFacilityCondition(string facility_id, string room_id, bool status)
        {
            CreateConn();
            OracleCommand CMD = DB.CreateCommand();
            CMD.CommandText = "update ROOM_FACILITY_CONDITION set FACILITY_CONDITION =:status  where FACILITY_ID =:facility_id and ROOM_ID =:room_id";
            CMD.Parameters.Add(new OracleParameter(":status", (status == true ? 1 : 0)));
            CMD.Parameters.Add(new OracleParameter(":facility_id", facility_id));
            CMD.Parameters.Add(new OracleParameter(":room_id", room_id));
            int Result = CMD.ExecuteNonQuery();
            CloseConn();
            if (Result != 1)
                return false;
            else
                return true;
        }
        //查找设备负责员工
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
                a.Add(new FaciltyStaffResponse { staff_id = Ord.GetValue(0).ToString() });
            }
            return a;
        }
        //查找员工负责设施id
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
        //查询所有设施信息
        public static List<FaciltyListResponse> FindFacilityInfo()
        {
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select * from facility NATURAL JOIN  room_facility_condition";
            //Search.Parameters.Add(new OracleParameter(":staff_id", staff_id));
            OracleDataReader Ord = Search.ExecuteReader();
            List<FaciltyListResponse> a = new List<FaciltyListResponse>();
            while (Ord.Read())
            {
                int pos = a.FindIndex(o => ((FaciltyListResponse)o).facility_id == Ord["FACILITY_ID"].ToString());
                if (pos < 0)
                    a.Add(new FaciltyListResponse { facility_id = Ord["FACILITY_ID"].ToString(), facility_name = Ord["FACILITY_NAME"].ToString(), staff_id = Ord["STAFF_ID"].ToString(), facility_price = int.Parse(Ord["FACILITY_PRICE"].ToString()), children = new List<FaciltyRoom>() { new FaciltyRoom() { room_id = Ord["ROOM_ID"].ToString(), facility_condition = (Ord["FACILITY_CONDITION"].ToString() == "0" ? false : true) } } });
                else
                    a[pos].children.Add(new FaciltyRoom() { room_id = Ord["ROOM_ID"].ToString(), facility_condition = (Ord["FACILITY_CONDITION"].ToString() == "0" ? false : true) });
            }
            return a;
        }
        //删除设施信息
        public static bool DelFacility(string facility_id)
        {
            CreateConn();
            OracleCommand CMD = DB.CreateCommand();
            CMD.CommandText = "delete from facility where facility_id=:facility_id";
            CMD.Parameters.Add(new OracleParameter(":facility_id", facility_id));
            int Result = CMD.ExecuteNonQuery();
            CloseConn();
            if (Result < 1)
                return false;
            else
                return true;
        }
        //添加设施信息
        public static bool AddFacility(FaciltyListResponse value)
        {
            CreateConn();
            int Result = 0;
            int Count = value.children.Count();
            OracleCommand CMD = DB.CreateCommand();
            CMD.CommandText = "insert into facility values(:FACILITY_ID,:FACILITY_NAME,:FACILITY_PRICE,:STAFF_ID)";
            CMD.Parameters.Add(new OracleParameter(":FACILITY_ID", value.facility_id));
            CMD.Parameters.Add(new OracleParameter(":FACILITY_NAME", value.facility_name));
            CMD.Parameters.Add(new OracleParameter(":FACILITY_PRICE", value.facility_price));
            CMD.Parameters.Add(new OracleParameter(":STAFF_ID", value.staff_id));
            Result += CMD.ExecuteNonQuery();
            for (int i = 0; i < Count; i++)
            {
                CMD = DB.CreateCommand();
                CMD.CommandText = "insert into room_facility_condition values(:FACILITY_ID,:ROOM_ID,:FACILITY_CONDITION)";
                CMD.Parameters.Add(new OracleParameter(":FACILITY_ID", value.facility_id));
                CMD.Parameters.Add(new OracleParameter(":ROOM_ID", value.children[i].room_id));
                CMD.Parameters.Add(new OracleParameter(":FACILITY_CONDITION", (value.children[i].facility_condition == false ? 0 : 1)));
                Result += CMD.ExecuteNonQuery();
            }
            CloseConn();
            if (Result <= 0)
                return false;
            else
                return true;
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
            if (Result < 0)
                return false;
            else
                return true;
        }
        //添加监控
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
        //显示所有车位信息
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
        //修改车位信息
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
        //添加车位
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
            if (Result != 1)
                return false;
            else
                return true;

        }
        //删除车位
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
        //查找某个车位信息
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



        //返回房间订单列表
        public static ListInfo DisplayRoomOrderListInfo(int pagesize, int pagenum, string query = "*")
        {
            try
            {
                List<OrderModel> orders = new List<OrderModel>();
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
                    orders.Add(new OrderModel { order_id = Ord.GetValue(0).ToString(), client_id = Ord.GetValue(1).ToString(), room_order_date = Ord.GetValue(2).ToString(), amount = (decimal)Ord.GetValue(3), state = (Int16)Ord.GetValue(4), client_name = Ord.GetValue(5).ToString(), client_telephonenumber = Ord.GetValue(6).ToString(), client_identity_card_number = Ord.GetValue(7).ToString(), room_id = Ord.GetValue(8).ToString(), room_type = Ord.GetValue(9).ToString(), stay_time = (decimal)Ord.GetValue(10) });
                }
                int total = orders.Count();
                ListInfo list = new ListInfo { pagesize = pagesize, pagenum = pagenum, total = total, list = orders };
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //返回菜品订单列表
        public static ListInfo DisplayDishOrderListInfo(string query = "*")
        {
            try
            {
                List<OrderModel> orders = new List<OrderModel>();
                OracleCommand Search = DB.CreateCommand();
                string Strsql;
                if (query == "" || query == "*")
                    Strsql = "select order_id,client_id,dish_date,amount,state,client_name,client_telephonenumber,dish_name from dish_order natural join client natural join dish";
                else
                {
                    Strsql = "select order_id,client_id,dish_date,amount,state,client_name,client_telephonenumber,dish_name from dish_order natural join client natural join dish where order_id like '%" + query + "%' or client_id like '%" + query + "%' or dish_date like '%" + query + "%' or amount like '%" + query + "%' or state like '%" + query + "%' or dish_name like '%" + query + "%' or client_telephonenumber like '%" + query + "%' or client_name like '%" + query + "%'";
                }
                Search.CommandText = Strsql;
                OracleDataReader Ord = Search.ExecuteReader();

                while (Ord.Read())
                {
                    orders.Add(new OrderModel { order_id = Ord.GetValue(0).ToString(), client_id = Ord.GetValue(1).ToString(), dish_order_date = Ord.GetValue(2).ToString(), amount = (decimal)Ord.GetValue(3), dish_order_state = Ord.GetValue(4).ToString(), client_name = Ord.GetValue(5).ToString(), client_telephonenumber = Ord.GetValue(6).ToString(), dish_name = Ord.GetValue(7).ToString() });

                }
                int total = orders.Count();
                ListInfo list = new ListInfo { total = total, list = orders };
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //修改订单信息
        public static int ModifyOrderInfo(string order_id, int state)
        {
            try
            {
                OracleCommand Update = DB.CreateCommand();
                Update.CommandText = "update room_order set state=:state where order_id=:order_id";
                Update.Parameters.Add(new OracleParameter(":state", state));
                Update.Parameters.Add(new OracleParameter(":order_id", order_id));
                int Result = Update.ExecuteNonQuery();
                return Result;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //修改房间订单信息
        public static int ModifyRoomOrderInfo(string order_id, int state)
        {
            try
            {
                OracleCommand Update = DB.CreateCommand();
                Update.CommandText = "update room_order set state=:state where order_id=:order_id";
                Update.Parameters.Add(new OracleParameter(":state", state));
                Update.Parameters.Add(new OracleParameter(":order_id", order_id));
                int Result = Update.ExecuteNonQuery();
                if (state == 1)
                {
                    OracleCommand Search = DB.CreateCommand();
                    Search.CommandText = "select client_id,room_id from room_order where order_id=:order_id";
                    Search.Parameters.Add(new OracleParameter(":order_id", order_id));
                    OracleDataReader Ord = Search.ExecuteReader();
                    while (Ord.Read())
                    {
                        string client_id = Ord.GetValue(0).ToString();
                        string room_id = Ord.GetValue(1).ToString();
                        OracleCommand Insert = DB.CreateCommand();

                        Insert.CommandText = "insert into check_in values(:client_id,:room_id,to_date(:check_in_time,'yyyy-mm-dd hh24:mi:ss'),to_date('2001-01-01 00:00:00','yyyy-mm-dd hh24:mi:ss'))";
                        Insert.Parameters.Add(new OracleParameter(":client_id", client_id));
                        Insert.Parameters.Add(new OracleParameter(":room_id", room_id));
                        Insert.Parameters.Add(new OracleParameter(":check_in_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                        Insert.ExecuteNonQuery();
                        OracleCommand Update2 = DB.CreateCommand();
                        Update2.CommandText = "update room set room_condition='入住' where room_id=:room_id";
                        Update2.Parameters.Add(new OracleParameter(":room_id", room_id));
                        Update2.ExecuteNonQuery();
                    }
                }
                if (state == 2)
                {
                    OracleCommand Search = DB.CreateCommand();
                    Search.CommandText = "select client_id,room_id from room_order where order_id=:order_id";
                    Search.Parameters.Add(new OracleParameter(":order_id", order_id));
                    OracleDataReader Ord = Search.ExecuteReader();
                    while (Ord.Read())
                    {
                        string client_id = Ord.GetValue(0).ToString();
                        string room_id = Ord.GetValue(1).ToString();
                        OracleCommand Update2 = DB.CreateCommand();
                        Update2.CommandText = "update room set room_condition='空闲' where room_id=:room_id";
                        Update2.Parameters.Add(new OracleParameter(":room_id", room_id));
                        Update2.ExecuteNonQuery();
                        OracleCommand Update3 = DB.CreateCommand();
                        Update3.CommandText = "update check_in set withdraw_time=to_date(:withdraw_time,'yyyy-mm-dd hh24:mi:ss') where client_id=:client_id,room_id=:room_id";
                        Update3.Parameters.Add(new OracleParameter(":withdraw_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                        Update3.Parameters.Add(new OracleParameter(":client_id", client_id));
                        Update3.Parameters.Add(new OracleParameter(":room_id", room_id));
                        Update3.ExecuteNonQuery();
                    }
                }
                if(state==3)
                {
                    OracleCommand Search = DB.CreateCommand();
                    Search.CommandText = "select room_id from room_order where order_id=:order_id";
                    Search.Parameters.Add(new OracleParameter(":order_id", order_id));
                    OracleDataReader Ord = Search.ExecuteReader();
                    while(Ord.Read())
                    {
                        string room_id = Ord.GetValue(0).ToString();
                        OracleCommand Update4 = DB.CreateCommand();
                        Update4.CommandText = "update room set room_condition='空闲' where room_id=:room_id";
                        Update4.Parameters.Add(new OracleParameter(":room_id", room_id));
                        Update4.ExecuteNonQuery();
                    }
                }
                return Result;
            }
            catch(Exception)
            {
                return 0;
            }
        }

        //修改菜品订单信息
        public static int ModifyDishOrderInfo(string order_id, int state)
        {
            try
            {
                OracleCommand Update = DB.CreateCommand();
                Update.CommandText = "update dish_order set state=:state where order_id=:order_id";
                Update.Parameters.Add(new OracleParameter(":state", state));
                Update.Parameters.Add(new OracleParameter(":order_id", order_id));
                int Result = Update.ExecuteNonQuery();
                return Result;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //查询订单信息
        public static OrderModel QueryOrderInfo(string order_id)
        {
            try
            {
                OracleCommand Search = DB.CreateCommand();
                Search.CommandText = "select * from room_order where order_id=:order_id";
                Search.Parameters.Add(new OracleParameter(":order_id", order_id));
                OracleDataReader Ord = Search.ExecuteReader();
                OrderModel order = new OrderModel();
                while (Ord.Read())
                {
                    order.order_id = Ord.GetValue(0).ToString();
                    order.client_id = Ord.GetValue(1).ToString();
                    order.room_order_date = Ord.GetValue(3).ToString();
                    order.amount = (decimal)Ord.GetValue(4);
                    order.state = (Int16)Ord.GetValue(5);

                }
                Search = DB.CreateCommand();
                Search.CommandText = "select * from dish_order where order_id=:order_id";
                Search.Parameters.Add(new OracleParameter(":order_id", order_id));
                Ord = Search.ExecuteReader();
                while (Ord.Read())
                {

                    order.dish_order_state = Ord["STATE"].ToString();

                }
                return order;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //增加菜品订单
        public static DishOrderResponse AddDishOrder(string client_name, string client_id, string dish_name, string client_telephonenumber, string dish_order_date, int number = 1)
        {

            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select dish_id from dish where  dish_name=:dish_name";
            Search.Parameters.Add(new OracleParameter(":dish_name", dish_name));
            int dish_id = (int)Search.ExecuteScalar();
            Search.CommandText = "select dish_price from dish where  dish_name=:dish_name";
            Int64 dish_price = (Int64)Search.ExecuteScalar();
            OracleCommand Insert = DB.CreateCommand();
            Insert.CommandText = "insert into dish_order values(dish_order_seq.nextval,:client_id,:dish_id,to_date(:dish_date,'yyyy-mm-dd'),:amount,'0',:dish_number)";
            Insert.Parameters.Add(new OracleParameter(":client_id", client_id));
            Insert.Parameters.Add(new OracleParameter(":dish_id", dish_id));
            Insert.Parameters.Add(new OracleParameter(":dish_date", dish_order_date));
            Insert.Parameters.Add(new OracleParameter(":amount", number * dish_price));
            Insert.Parameters.Add(new OracleParameter(":dish_number", number));
            try
            {
                Insert.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return null;
            }
            Search.CommandText = "select dish_order_seq.currval from dual";
            string order_id = Search.ExecuteScalar().ToString();
            DishOrderResponse response = new DishOrderResponse { order_id = order_id, amount = number * dish_price, state = "0", client_name = client_name, client_telephonenumber = client_telephonenumber, dish_name = dish_name, dish_order_date = dish_order_date, number = number };
            return response;
        }

        //增加房间订单

        public static RoomOrderResponse AddRoomOrder(string client_id, string order_date, string room_type, decimal amount, string client_name, string client_identity_card_number, string room_order_date, decimal stay_time = 1, string client_telephonenumber = null)
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
            Insert.CommandText = "insert into room_order values(room_order_seq.nextval,:client_id,:room_id,to_date(:order_date,'yyyy-mm-dd'),:amount,'0',:stay_time)";
            Insert.Parameters.Add(new OracleParameter(":client_id", client_id));
            Insert.Parameters.Add(new OracleParameter(":room_id", room_id));
            Insert.Parameters.Add(new OracleParameter(":order_date", order_date));
            Insert.Parameters.Add(new OracleParameter(":amount", room_price * stay_time));
            Insert.Parameters.Add(new OracleParameter(":stay_time", stay_time));
            try
            {
                Insert.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return null;
            }
            Search.CommandText = "select room_order_seq.currval from dual";
            string order_id = Search.ExecuteScalar().ToString();
            RoomOrderResponse response = new RoomOrderResponse { order_id = order_id, client_id = client_id, amount = amount, state = 0, client_name = client_name, client_telephonenumber = client_telephonenumber, client_identity_card_number = client_identity_card_number, room_id = room_id, room_type = room_type, room_order_date = room_order_date, stay_time = stay_time };
            OracleCommand Update2 = DB.CreateCommand();
            Update2.CommandText = "update room set room_condition='已预定' where room_id=:room_id";
            Update2.Parameters.Add(new OracleParameter(":room_id", room_id));
            Update2.ExecuteNonQuery();
            return response;
        }


        //查询所有房间信息
        public static List<RoomModel> DisplayRoomInfo()
        {
            List<RoomModel> rooms = new List<RoomModel>();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select room_id,room_price,room_type,room_condition,client_name,client_telephonenumber,live_time,staff_id from room natural join clean natural left outer join check_in natural left outer join client ";
            try
            {
                OracleDataReader Ord = Search.ExecuteReader();
                while (Ord.Read())
                {
                    string room_id = Ord.GetValue(0).ToString();
                    string room_condition = Ord.GetValue(3).ToString();
                    if (room_condition == "入住")
                        rooms.Add(new RoomModel { room_id = Ord.GetValue(0).ToString(), room_price = (decimal)Ord.GetValue(1), room_type = Ord.GetValue(2).ToString(), room_condition = Ord.GetValue(3).ToString(), name = Ord.GetValue(4).ToString(), phone = Ord.GetValue(5).ToString(), time = Ord.GetValue(6).ToString(), staff_id = Ord.GetValue(7).ToString() });
                    else if (room_condition == "已预定")
                    {
                        OracleCommand Search2 = DB.CreateCommand();
                        Search2.CommandText = "select client_name,client_telephonenumber from room_order natural join client where room_id=:room_id";
                        Search2.Parameters.Add(new OracleParameter(":room_id", room_id));
                        OracleDataReader Ord2 = Search2.ExecuteReader();
                        if(Ord2.Read())
                            rooms.Add(new RoomModel { room_id = Ord.GetValue(0).ToString(), room_price = (decimal)Ord.GetValue(1), room_type = Ord.GetValue(2).ToString(), room_condition = Ord.GetValue(3).ToString(), name = Ord2.GetValue(0).ToString(), phone = Ord2.GetValue(1).ToString(), time = null, staff_id = Ord.GetValue(7).ToString() });
                    }
                    else
                        rooms.Add(new RoomModel { room_id = Ord.GetValue(0).ToString(), room_price = (decimal)Ord.GetValue(1), room_type = Ord.GetValue(2).ToString(), room_condition = Ord.GetValue(3).ToString(), name = null, phone = null, time = null, staff_id = Ord.GetValue(7).ToString() });
                }
                return rooms;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //修改房间信息
        public static int ModifyRoomInfo(string room_id, int room_price, string room_type, string room_condition, string staff_id)
        {
            try
            {
                OracleCommand Update = DB.CreateCommand();
                Update.CommandText = "update room set room_id=:room_id,room_price=:room_price,room_type=:room_type,room_condition=:room_condition where room_id=:room_id";
                Update.Parameters.Add(new OracleParameter(":room_id", room_id));
                Update.Parameters.Add(new OracleParameter(":room_price", room_price));
                Update.Parameters.Add(new OracleParameter(":room_type", room_type));
                Update.Parameters.Add(new OracleParameter(":room_condition", room_condition));

                int Result = Update.ExecuteNonQuery();
                OracleCommand Update2 = DB.CreateCommand();
                Update2.CommandText = "update clean set staff_id=:staff_id  where room_id=:room_id";
                Update2.Parameters.Add(new OracleParameter(":staff_id", staff_id));
                Update2.Parameters.Add(new OracleParameter(":room_id", room_id));
                Update2.ExecuteNonQuery();
                return Result;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //删除房间信息
        public static int DeleteRoomInfo(string room_id)
        {
            OracleCommand Delete = DB.CreateCommand();
            Delete.CommandText = "delete from room where room_id=:room_id";
            Delete.Parameters.Add(new OracleParameter(":room_id", room_id));
            try
            {
                Delete.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }

        //增加房间信息
        public static decimal AddRoomInfo(string room_condition, string staff_id, decimal room_price, string room_type)
        {
            OracleCommand Insert = DB.CreateCommand();
            Insert.CommandText = "insert into room values(room_seq.nextval,:room_price,:room_type,:room_condition)";
            Insert.Parameters.Add(new OracleParameter(":room_price", room_price));
            Insert.Parameters.Add(new OracleParameter(":room_type", room_type));
            Insert.Parameters.Add(new OracleParameter(":room_condition", room_condition));
            try
            {
                Insert.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return 0;
            }
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select room_seq.currval from dual";
            decimal room_id = (decimal)Search.ExecuteScalar();
            OracleCommand Insert2 = DB.CreateCommand();
            Insert2.CommandText = "insert into clean values(:staff_id,:room_id)";
            Insert2.Parameters.Add(new OracleParameter(":staff_id", staff_id));
            Insert2.Parameters.Add(new OracleParameter(":room_id", room_id.ToString()));
            Insert2.ExecuteNonQuery();
            return room_id;
        }

        //查询房间类型信息
        public static List<RoomTypeInfo> DisplayRoomTypeInfo()
        {
            try
            {
                List<RoomTypeInfo> roomtypes = new List<RoomTypeInfo>();
                OracleCommand Search = DB.CreateCommand();
                Search.CommandText = "select room_type,room_price,room_url,room_explain,count(*) as room_workable from room natural full outer join room_type where room_condition='空闲' group by room_type,room_price,room_url,room_explain";

                OracleDataReader Ord = Search.ExecuteReader();
                while (Ord.Read())
                {

                    roomtypes.Add(new RoomTypeInfo { room_type = Ord.GetValue(0).ToString(), room_price = (decimal)Ord.GetValue(1), room_url = Ord.GetValue(2).ToString(), room_explain = Ord.GetValue(3).ToString(), room_workable = (decimal)Ord.GetValue(4) });


                }
                OracleCommand Search2 = DB.CreateCommand();
                Search2.CommandText = "select room_type,room_url,room_explain  from room natural full outer join room_type  group by room_type,room_price,room_url,room_explain minus select room_type,room_url,room_explain from room natural full outer join room_type where room_condition='空闲' group by room_type,room_price,room_url,room_explain";
                OracleDataReader Ord2 = Search2.ExecuteReader();
                while (Ord2.Read())
                {
                    roomtypes.Add(new RoomTypeInfo { room_type = Ord2.GetValue(0).ToString(), room_price = 0, room_url = Ord2.GetValue(1).ToString(), room_explain = Ord2.GetValue(2).ToString(), room_workable = 0 });
                }
                return roomtypes;
            }
            catch(Exception)
            {
                return null;
            }


        }

        //增加房间类型信息

        public static int AddRoomTypeInfo(string room_type, string room_url, string room_explain)
        {
            OracleCommand Insert = DB.CreateCommand();
            Insert.CommandText = "insert into room_type values(:room_type,:room_url,:room_explain)";
            Insert.Parameters.Add(new OracleParameter(":room_type", room_type));
            Insert.Parameters.Add(new OracleParameter(":room_url", room_url));
            Insert.Parameters.Add(new OracleParameter(":room_explain", room_explain));
            try
            {
                Insert.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }

        //修改房间类型信息
        public static int ModifyRoomTypeInfo(string room_type, string room_explain)
        {
            OracleCommand Update = DB.CreateCommand();
            Update.CommandText = "update room_type set room_explain=:room_explain where room_type=:room_type";
            Update.Parameters.Add(new OracleParameter(":room_explain", room_explain));
            Update.Parameters.Add(new OracleParameter(":room_type", room_type));
            try
            {
                Update.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }

        //删除房间类型信息
        public static int DeleteRoomTypeInfo(string room_type)
        {
            OracleCommand Delete = DB.CreateCommand();
            Delete.CommandText = "delete from room_type where room_type=:room_type";
            Delete.Parameters.Add(new OracleParameter(":room_type", room_type));
            try
            {
                Delete.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
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

            OracleCommand Delete = DB.CreateCommand();
            Delete.CommandText = "delete from STAFF where staff_id=:staff_id";
            Delete.Parameters.Add(new OracleParameter(":staff_id", staff_id));
            int Result = Delete.ExecuteNonQuery();

            OracleCommand Delete_Users = DB.CreateCommand();
            Delete_Users.CommandText = "delete from USERS where user_id=:staff_id";
            Delete_Users.Parameters.Add(new OracleParameter(":staff_id", staff_id));
            Delete_Users.ExecuteNonQuery();

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
        //添加leader
        public static bool AddLeader(StaffAddRequest value)
        {
            CreateConn();
            OracleCommand Add = DB.CreateCommand();
            Add.CommandText = "insert into LEADER values(:leader_id,:staff_id)";

            Add.Parameters.Add(new OracleParameter(":leader_id", value.leader_id));
            Add.Parameters.Add(new OracleParameter(":staff_id", value.staff_id));
            int Result = Add.ExecuteNonQuery();
            if (Result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //给定新的员工属性，更新对应id的员工属性和leader，成功返回true 不成功返回false
        public static bool StaffUpdate(StaffUpdateRequest value)
        {
            CreateConn();
            OracleCommand Update = DB.CreateCommand();
            Update.CommandText = "update STAFF set staff_id=:staff_id ,staff_name=:staff_name ,staff_sex=:staff_sex ,staff_age=:staff_age ,staff_identity_card_number=:staff_identity_card_number ,staff_address=:staff_address ,staff_department=:staff_department ,staff_position=:staff_position ,staff_entry_date=to_date(:staff_entry_data,'yyyy-mm-dd hh24:mi:ss') ,staff_salary=:staff_salary where staff_id=:staff_id";
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

            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select count(*) from LEADER where staff_id=:staff_id";
            Search.Parameters.Add(new OracleParameter(":staff_id", value.staff_id));
            int count = Convert.ToInt32(Search.ExecuteScalar());
            if (count == 1)
            {
                if (value.leader_id == "")
                {
                    OracleCommand Update_Leader = DB.CreateCommand();
                    Update_Leader.CommandText = "delete from LEADER where staff_id=:staff_id";
                    Update_Leader.Parameters.Add(new OracleParameter(":staff_id", value.staff_id));
                    Update_Leader.ExecuteNonQuery();
                }
                else
                {
                    OracleCommand Update_Leader = DB.CreateCommand();
                    Update_Leader.CommandText = "update LEADER set leader_id=:leader_id where staff_id=:staff_id";
                    Update_Leader.Parameters.Add(new OracleParameter(":staff_id", value.staff_id));
                    Update_Leader.Parameters.Add(new OracleParameter(":leader_id", value.leader_id));
                    Update_Leader.ExecuteNonQuery();
                }
            }
            else
            {
                if (value.leader_id != "")
                {
                    OracleCommand Update_Leader = DB.CreateCommand();
                    Update_Leader.CommandText = "insert into LEADER values(:leader_id,:staff_id)";
                    Update_Leader.Parameters.Add(new OracleParameter(":leader_id", value.leader_id));
                    Update_Leader.Parameters.Add(new OracleParameter(":staff_id", value.staff_id));
                    Update_Leader.ExecuteNonQuery();
                }
            }




            OracleCommand Update_User = DB.CreateCommand();
            Update_User.CommandText = "update USERS set user_name=:staff_name where user_id=:staff_id";
            Update_User.Parameters.Add(new OracleParameter(":staff_name", value.staff_name));
            Update_User.Parameters.Add(new OracleParameter(":staff_id", value.staff_id));
            Update_User.ExecuteNonQuery();

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
        //返回所有staff信息和他的leader_id
        public static List<ReturnAllResponse> ReturnAll()
        {
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select * from STAFF LEFT JOIN LEADER on staff.staff_id=leader.staff_id";
            OracleDataReader Ord = Search.ExecuteReader();
            List<ReturnAllResponse> a = new List<ReturnAllResponse>();
            while (Ord.Read())
            {
                a.Add(new ReturnAllResponse { staff_id = Ord.GetValue(0).ToString(), staff_name = Ord.GetValue(1).ToString(), staff_sex = Ord.GetValue(2).ToString(), staff_age = Ord.GetValue(3).ToString(), staff_identity_card_number = Ord.GetValue(4).ToString(), staff_address = Ord.GetValue(5).ToString(), staff_department = Ord.GetValue(6).ToString(), staff_position = Ord.GetValue(7).ToString(), staff_entry_date = Ord.GetValue(8).ToString(), staff_salary = Ord.GetValue(9).ToString(), leader_id = Ord.GetValue(10).ToString() });
            }
            CloseConn();
            return a;
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
        //返回菜品的所有信息
        public static List<DishReturnResponse> ReturnAll_Dish()
        {
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select * from DISH";
            OracleDataReader Ord = Search.ExecuteReader();
            List<DishReturnResponse> a = new List<DishReturnResponse>();
            while (Ord.Read())
            {
                a.Add(new DishReturnResponse { dish_id = Ord.GetValue(0).ToString(), dish_name = Ord.GetValue(1).ToString(), dish_price = Ord.GetValue(2).ToString(), dish_picture = Ord.GetValue(3).ToString(), dish_explain = Ord.GetValue(4).ToString() });
            }
            CloseConn();
            return a;
        }
        //删除指定菜品
        public static string DeleteDish(string dish_id)
        {
            CreateConn();

            OracleCommand Delete_Dish_Order = DB.CreateCommand();
            Delete_Dish_Order.CommandText = "delete from DISH_ORDER where dish_id=:dish_id";
            Delete_Dish_Order.Parameters.Add(new OracleParameter(":dish_id", dish_id));
            Delete_Dish_Order.ExecuteNonQuery();

            OracleCommand Delete_Cook = DB.CreateCommand();
            Delete_Cook.CommandText = "delete from COOK where dish_id=:dish_id";
            Delete_Cook.Parameters.Add(new OracleParameter(":dish_id", dish_id));
            Delete_Cook.ExecuteNonQuery();

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
        //添加菜品
        public static string DishAdd(DishAddRequest value)
        {
            CreateConn();
            OracleCommand Add = DB.CreateCommand();
            Add.CommandText = "insert into DISH values(null,:dish_name,:dish_price,:dish_picture,:dish_explain)";

            Add.Parameters.Add(new OracleParameter(":dish_name", value.dish_name));
            Add.Parameters.Add(new OracleParameter(":dish_price", value.dish_price));
            Add.Parameters.Add(new OracleParameter(":dish_picture", value.dish_picture));
            Add.Parameters.Add(new OracleParameter(":dish_explain", value.dish_explain));
            Add.ExecuteNonQuery();
            OracleCommand GetPrimary1 = DB.CreateCommand();
            GetPrimary1.CommandText = "select dish_seq.nextval from dual";

            OracleCommand GetPrimary = DB.CreateCommand();
            GetPrimary.CommandText = "select dish_seq.currval from dual";

            string e = GetPrimary.ExecuteScalar().ToString();
            CloseConn();

            if (e != "")
            {
                return e;
            }
            else return "0";
        }


    }


}
