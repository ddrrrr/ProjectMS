using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMS.Core.ErrorManager
{
    /// <summary>
    /// 
    /// </summary>
    class ErrorManager
    {
        public List<ErrorBase> Errors;
        public string ErrorPrefix;
        
        /// <summary>
        /// 初始化警报管理
        /// </summary>
        /// <param name="prefix">警报代码前缀，限定3个字符</param>
        public ErrorManager(string prefix)
        {
            if (prefix.Length >= 3)
                prefix = prefix.Substring(0, 3);
            else
                prefix = prefix.PadRight(3, 'E');

            if (!ErrorLogManager.LPrefix.Exists(s => s == prefix))
                ErrorPrefix = prefix;
            else
                ErrorLogManager.SystemErrors.RaiseError(101);
            Errors = new List<ErrorBase>();
        }

        public ErrorManager(string prefix, List<Tuple<int, string, string, ErrorSerious>> initError) : this(prefix)
        {
            foreach (var x in initError)
                this.AddError(x.Item1, x.Item2, x.Item3, x.Item4);
        }

        public void AddError(int code, string reason, string resolution, ErrorSerious serious)
        {
            if (code.ToString().Length <= 3)
                Errors.Add(new ErrorBase(ErrorPrefix + code.ToString().PadLeft(3, '0'), reason, resolution, serious));
            else
                ErrorLogManager.SystemErrors.RaiseError(102);
            ErrorLogManager.AddErrorBase(new ErrorBase(ErrorPrefix + code.ToString().PadLeft(3, '0'), reason, resolution, serious));
        }

        public void RaiseError(int code, string remark = "", bool IsShowMessage = false)
        {
            ErrorBase err = Errors.Find(x =>
            {
                return Convert.ToInt32(x.ErrorCode.Substring(3, 3)) == code;
            });
            if (err == null)
            {
                ErrorLogManager.SystemErrors.RaiseError(103, "使用了错误代号：" + code.ToString() + "!", true);
                return;
            }
            ErrorLogManager.AddErrorLog(err, remark);
        }
    }
    
    /// <summary>
    /// 警报等级
    /// </summary>
    public enum ErrorSerious
    {
        提示,
        一般,
        严重,
        致命
    }
    
    /// <summary>
    /// 单种警报
    /// </summary>
    public class ErrorBase
    {
        /// <summary>
        /// 警报代码，6位，前三位为英语字母，代表警报类型，后三位为数字，划分具体警报
        /// </summary>
        public string ErrorCode { set; get; }
        /// <summary>
        /// 造成警报的可能原因
        /// </summary>
        public string ErrorReason { set; get; }
        /// <summary>
        /// 解决警报的可能方法
        /// </summary>
        public string Resolution { set; get; }
        /// <summary>
        /// 警报等级
        /// </summary>
        public ErrorSerious Serious { get; set; }

        public ErrorBase(string ECode, string EReason, string Resolution, ErrorSerious serious)
        {
            this.ErrorCode = ECode;
            this.ErrorReason = EReason;
            this.Resolution = Resolution;
            this.Serious = serious;
        }
    }

    /// <summary>
    /// 历史警报数据管理器，负责将历史警报信息读写入数据库中
    /// </summary>
    static class ErrorLogManager
    {
        public static List<string> LPrefix = new List<string>();
        static List<ErrorBase> AllError = new List<ErrorBase>();
        static string path = "ErrorLog.db";
        private static SQLiteConnection con;
        private static SQLiteCommand cmd;
        public static ErrorManager SystemErrors = new ErrorManager("SYS", new List<Tuple<int, string, string, ErrorSerious>>
        {
            new Tuple<int, string, string, ErrorSerious>(0, "正常", "", ErrorSerious.提示),
            new Tuple<int, string, string, ErrorSerious>(101, "错误代号前缀已存在", "请更改错误代号前缀", ErrorSerious.提示),
            new Tuple<int, string, string, ErrorSerious>(102, "错误管理器错添加新错误中错误代号超出999", "请设置合理的错误代号", ErrorSerious.提示),
            new Tuple<int, string, string, ErrorSerious>(103, "RaiseError中使用错误代号", "请检查代码", ErrorSerious.提示)
        });

        static ErrorLogManager()
        {
            con = new SQLiteConnection("data source=" + path);
            cmd = new SQLiteCommand();
            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS ErrorLog(id INTEGER PRIMARY KEY AUTOINCREMENT, errorcode NVARCHAR, happentime DATETIME, remark NVARCHAR)";
                cmd.ExecuteNonQuery();
            }
            con.Close();
            //AllError = new List<ErrorBase>();
        }

        public static event Action<ErrorBase> ErrorRasied;
        public static void AddErrorLog(ErrorBase E, string remark = "")
        {
            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "INSERT INTO ErrorLog(errorcode, happentime, remark) VALUES(@errorcode,@happentime,@remark)";
                cmd.Parameters.Add("errorcode", System.Data.DbType.String).Value = E.ErrorCode;
                cmd.Parameters.Add("happentime", System.Data.DbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("remark", System.Data.DbType.String).Value = remark;
                cmd.ExecuteNonQuery();
            }
            con.Close();
            ErrorRasied?.Invoke(E);
        }

        public static List<Tuple<ErrorBase, DateTime, string>> GetHistoryError()
        {
            var re = new List<Tuple<ErrorBase, DateTime, string>>();
            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
                cmd.Connection = con;
                string sql = string.Format("select * from ErrorLog");
                SQLiteCommand command = new SQLiteCommand(sql, con);
                SQLiteDataReader reader = command.ExecuteReader();
                string errorcode;
                DateTime time;
                string remark;
                while (reader.Read())
                {
                    errorcode = reader["errorcode"].ToString();
                    time = DateTime.Parse(reader["happentime"].ToString());
                    remark = reader["remark"].ToString();
                    var err = FindError(errorcode);
                    re.Add(new Tuple<ErrorBase, DateTime, string>(err, time, remark));
                }
            }
            con.Close();
            return re;
        }

        public static void AddErrorBase(ErrorBase E)
        {
            AllError.Add(E);
        }

        public static ErrorBase FindError(string Errorcode)
        {
            var err = AllError.Find(x => x.ErrorCode == Errorcode);
            if (err != null)
                return err;
            else
                return null;
        }

    }
}
