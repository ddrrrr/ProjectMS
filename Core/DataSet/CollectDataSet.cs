using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMS.Core.DataSet
{
    class CollectDataSet
    {
        static string path = "DataSet.db";
        static SQLiteConnection con;
        static SQLiteCommand cmd;

        static CollectDataSet()
        {
            con = new SQLiteConnection("data source=" + path);
            cmd = new SQLiteCommand();
            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS DataSetInfo(id UNSIGNEDINTEGER, cardname NVARCHAR, name NVARCHAR, " +
                    "sensortype NVARCHAR, detectequipment NVARCHAR, " +
                    "position NVARCHAR, signalrange NVARCHAR," +
                    "resolution UNSIGNEDINTEGER16, resolutiondouble DOUBLE," +
                    "basevalue DOUBLE, remark NVARCHAR)";
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }

        public static bool AddSignal(SignalInfo info)
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO DataSetInfo(cardname, name, sensortype, detectequipment,position,signalrange,resolution,resolutiondouble,basevalue,remark) " +
                "VALUES(@cardname, @name, @sensortype, @detectequipment,@position,@signalrange,@resolution,@resolutiondouble,@basevalue,@remark)";
            cmd.Parameters.Add("cardname", System.Data.DbType.String).Value = info.cardname;
            cmd.Parameters.Add("name", System.Data.DbType.String).Value = info.name;
            cmd.Parameters.Add("sensortype", System.Data.DbType.String).Value = info.sensortype;
            cmd.Parameters.Add("detectequipment", System.Data.DbType.String).Value = info.detectequipment;
            cmd.Parameters.Add("position", System.Data.DbType.String).Value = info.position;
            cmd.Parameters.Add("signalrange", System.Data.DbType.String).Value = info.signalrange;
            cmd.Parameters.Add("resolution", System.Data.DbType.UInt32).Value = info.resolution;
            cmd.Parameters.Add("resolutiondouble", System.Data.DbType.Double).Value = info.resolutiondouble;
            cmd.Parameters.Add("basevalue", System.Data.DbType.Double).Value = info.basevalue;
            cmd.Parameters.Add("remark", System.Data.DbType.String).Value = info.remark;
            cmd.ExecuteNonQuery();

            cmd.CommandText = string.Format("CREATE TABLE IF NOT EXISTS {0}(id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "time DOUBLE, increase DOUBLE, value UNSIGNEDINTEGER16, remark UNSIGNEDINTEGER16)", info.name);
            cmd.ExecuteNonQuery();

            cmd.CommandText = string.Format("CREATE TABLE IF NOT EXISTS {0}Time (starttime DOUBLE, stoptime DOUBLE)", info.name);
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }

        public static bool DelSignal(string name)
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = string.Format("DELETE FROM DataSetInfo where name='{0}'", name);
            cmd.ExecuteNonQuery();

            cmd.CommandText = string.Format("DROP TABLE {0}", name);
            cmd.ExecuteNonQuery();

            cmd.CommandText = string.Format("DROP TABLE {0}Time", name);
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }

        public static SignalInfo GetSignal(string name)
        {
            SignalInfo info = new SignalInfo();
            info.name = name;

            con.Open();
            cmd.Connection = con;
            string sql = string.Format("select * from DataSetInfo where name='{0}'", name);
            SQLiteCommand command = new SQLiteCommand(sql, con);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                info.position = reader["position"] == DBNull.Value ? "" : (string)reader["position"];
                info.detectequipment = reader["detectequipment"] == DBNull.Value ? "" : (string)reader["detectequipment"];
                info.remark = (reader["remark"] == DBNull.Value) ? "" : (string)reader["remark"];
                info.resolution = (ushort)int.Parse(reader["resolution"].ToString());
                info.resolutiondouble = Double.Parse(reader["resolutiondouble"].ToString());
                info.basevalue = Double.Parse(reader["basevalue"].ToString());
                info.sensortype = (string)reader["sensortype"];
                info.signalrange = (string)reader["signalrange"];
            }
            con.Close();
            return info;
        }

        public static bool ChgSignal(SignalInfo preinfo, SignalInfo info)
        {
            con.Open();
            cmd.Connection = con;
            string sql = "update DataSetInfo set ";

            sql += string.Format("cardname='{0}', ", info.cardname);
            sql += string.Format("name='{0}', ", info.name);
            sql += string.Format("detectequipment='{0}', ", info.detectequipment);
            sql += string.Format("position='{0}', ", info.position);
            sql += string.Format("remark='{0}', ", info.remark);
            sql += string.Format("resolution={0}, ", info.resolution);
            sql += string.Format("resolutiondouble={0}, ", info.resolutiondouble);
            sql += string.Format("basevalue={0}, ", info.basevalue);
            sql += string.Format("sensortype='{0}', ", info.sensortype);
            sql += string.Format("signalrange='{0}' ", info.signalrange);

            sql += string.Format("where name='{0}'", preinfo.name);

            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();

            sql = string.Format("alter table {0} rename to {1}", preinfo.name, info.name);
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();

            sql = string.Format("alter table {0}Time rename to {1}Time", preinfo.name, info.name);
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();

            con.Close();
            return true;
        }

        public static bool AddData(string name, double starttime, double stoptime, double increase, short[] values, short[] remark = null)
        {
            con.Open();
            cmd.Connection = con;

            using (var transaction = con.BeginTransaction())
            {
                string sql;
                sql = string.Format("insert into {0} (id,time,increase,value,remark) values(NULL,{1},{2},{3},'{4}')", name, starttime, increase, values[0], remark != null ? remark[0] : 0);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                for (int i = 1; i < values.Length; i++)
                {
                    if (remark != null)
                        sql = string.Format("insert into {0} (id,value,remark) values(NULL,{1},'{2}')", name, values[i], remark[i]);
                    else
                        sql = string.Format("insert into {0} (id,value) values(NULL,{1})", name, values[i]);
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            con.Close();
            return true;
        }

        public static bool GetData(string name, Tuple<DateTime, DateTime> selecttime, out double[] times, out double[] values, out short[] remark)
        {
            List<double> Ltimes = new List<double>();
            List<double> Lvalues = new List<double>();
            List<short> Lremarks = new List<short>();
            var info = GetSignal(name);

            con.Open();
            cmd.Connection = con;
            string sql = string.Format("select * from {0} where time > {1} and time < {2}", name, selecttime.Item1.ToOADate(), selecttime.Item2.ToOADate());
            SQLiteCommand command = new SQLiteCommand(sql, con);
            SQLiteDataReader reader = command.ExecuteReader();
            int startid, stopid;
            reader.Read();
            try
            {
                startid = int.Parse(reader["id"].ToString());
                stopid = int.Parse(reader["id"].ToString());
                while (reader.Read())
                {
                    stopid = int.Parse(reader["id"].ToString());
                }

                sql = string.Format("select * from {0} where id >= {1} and id < {2}", name, startid, stopid);
                command = new SQLiteCommand(sql, con);
                reader = command.ExecuteReader();

                double increase = 0;
                while (reader.Read())
                {
                    if (reader["time"] != DBNull.Value)
                    {
                        increase = double.Parse(reader["increase"].ToString());
                        Ltimes.Add(double.Parse(reader["time"].ToString()));
                    }
                    else
                    {
                        Ltimes.Add(Ltimes.Last() + increase);
                    }

                    Lvalues.Add((ushort.Parse(reader["value"].ToString())) * info.resolutiondouble + info.basevalue);
                    Lremarks.Add((reader["remark"] == DBNull.Value) ? (short)0 : short.Parse(reader["remark"].ToString()));
                }
                con.Close();
                times = Ltimes.ToArray();
                values = Lvalues.ToArray();
                remark = Lremarks.ToArray();
                return true;
            }
            catch
            {
                times = null;
                values = null;
                remark = null;
                return false;
            }

        }

        public static string[] GetExistData(Tuple<DateTime, DateTime> t)
        {
            List<string> Lnames = new List<string>();
            con.Open();
            cmd.Connection = con;
            string sql = "select name from DataSetInfo";
            SQLiteCommand command = new SQLiteCommand(sql, con);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                Lnames.Add(reader["name"].ToString());

            List<string> returnnames = new List<string>();
            foreach (var onename in Lnames)
            {
                sql = string.Format("select count(*) from {0} where time >= {1} and time < {2}", onename, t.Item1.ToOADate(), t.Item2.ToOADate());
                command = new SQLiteCommand(sql, con);
                int a = Convert.ToInt32(command.ExecuteScalar());
                if (a != 0)
                    returnnames.Add(onename);
            }
            con.Close();
            return returnnames.ToArray();
        }
        /// <summary>
        /// 保存信号采集的时间段
        /// </summary>
        /// <param name="name">信号名称</param>
        /// <param name="starttime">开始采集时间</param>
        /// <param name="stoptime">停止采集时间</param>
        /// <returns>是否保存成功</returns>
        public static bool AddTime(string name, double starttime, double stoptime)
        {
            con.Open();
            cmd.Connection = con;
            string sql = string.Format("insert into {0}Time (starttime,stoptime) values({1},{2})", name, starttime, stoptime);
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }

        public static List<Tuple<double, double>> GetTime(string name)
        {
            con.Open();
            List<Tuple<double, double>> r = new List<Tuple<double, double>>();
            string sql = string.Format("select * from {0}Time", name);
            SQLiteCommand command = new SQLiteCommand(sql, con);
            SQLiteDataReader reader = command.ExecuteReader();
            double tmpstarttime, tmpstoptime;
            while (reader.Read())
            {
                tmpstarttime = double.Parse(reader["starttime"].ToString());
                tmpstoptime = double.Parse(reader["stoptime"].ToString());
                r.Add(new Tuple<double, double>(tmpstarttime, tmpstoptime));
            }
            con.Close();
            return r;
        }
    }

    /// <summary>
    /// 总表各行信息
    /// </summary>
    struct SignalInfo
    {
        /// <summary>
        /// 采集卡名称
        /// </summary>
        public string cardname;
        /// <summary>
        /// 信号名称
        /// </summary>
        public string name;
        /// <summary>
        /// 传感器类型
        /// </summary>
        public string sensortype;
        /// <summary>
        /// 所检测的设备
        /// </summary>
        public string detectequipment;
        /// <summary>
        /// 传感器安装在设备的位置
        /// </summary>
        public string position;
        /// <summary>
        /// 电信号的范围
        /// </summary>
        public string signalrange;
        /// <summary>
        /// 精度（分辨率），多少位（bit）
        /// </summary>
        public ushort resolution;
        /// <summary>
        /// ushort转double时的分度值
        /// </summary>
        public double resolutiondouble;
        /// <summary>
        /// ushort转double时的最小值
        /// </summary>
        public double basevalue;
        /// <summary>
        /// 备注
        /// </summary>
        public string remark;
    }
}
