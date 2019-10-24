using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
namespace DBUtility
{
    /// <summary>
    /// 数据访问抽象基础类
    /// Copyright (C) Maticsoft
    /// </summary>
    public abstract class DbHelperMySQL
    {
        //数据库连接字符串(web.config来配置)，可以动态更改connectionString支持多数据库.        
        //public static readonly string connectionString = ConfigurationManager.ConnectionStrings["MySqlStr"].ConnectionString;
        public static readonly string connectionString = "server=127.0.0.1;database=mstest;port=3306;user=root;password=shensu;";
        public DbHelperMySQL()
        {
        }

        /// <summary>
        /// DataTable to tree 根据数据id,txt生成对应的json数据格式
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="pField">Parentid</param>
        /// <param name="pValue">Parentid value</param>
        /// <param name="kField">pkid </param>
        /// <returns></returns>
        public static string TableToEasyUITreeJson(DataTable dt, string pField, string pValue, string pkid, string pktext, string url)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string filter = String.Format(" {0}='{1}' ", pField, pValue);//获取顶级目录
                if (pValue == string.Empty || pValue == null)
                {
                    filter = String.Format("{0}='{1}' or {0} is null", pField, pValue);
                }
                //filter = "Parentid is null";
                DataRow[] drs = dt.Select(filter);
                if (drs.Length < 1) return "";
                sb.Append("<UL id='treeList'>\n");


                foreach (DataRow dr in drs)
                {
                    string pcv = dr[pkid].ToString();


                    sb.AppendFormat("<li id='tree{0}'><a href='list.aspx?typeId={0}&ptypeId=#$' title='{1}'>\n", dr[pkid].ToString(), dr[pktext].ToString());
                    sb.AppendFormat("{0}\n</a>", dr[pktext].ToString());
                    //sb.Append("\"checked\":true,");
                    //if (pValue == null || pValue == string.Empty)//根节点没有url
                    //{
                    //    sb.AppendFormat("\"attributes\":\"{0}\"", "");
                    //}
                    //else
                    //{
                    //    sb.AppendFormat("\"attributes\":\"{0}\"", url);
                    //}

                    sb.Append(TableToEasyUITreeJson(dt, pField, pcv, pkid, pktext, url).TrimEnd(','));
                    sb.Append("</li>\n");
                }


                //if (sb.ToString().EndsWith(","))
                //{
                //    sb.Remove(sb.Length - 1, 1);
                //}
                sb.Append("</ul>\n");
                return sb.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string TableToEasyUITreeJson(DataTable dt, string pField, string pValue, string pkid)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                string filter = String.Format(" {0}='{1}' ", pField, pValue);//获取顶级目录
                if (pValue == string.Empty || pValue == null)
                {
                    filter = String.Format("{0}='{1}' or {0} is null", pField, pValue);
                }
                //filter = "Parentid is null";
                DataRow[] drs = dt.Select(filter);
                if (drs.Length < 1) return "";

                //sb.AppendFormat("{0},", pValue);

                foreach (DataRow dr in drs)
                {
                    string pcv = dr[pkid].ToString();


                    sb.AppendFormat("{0},", dr[pkid].ToString());



                    sb.Append(TableToEasyUITreeJson(dt, pField, pcv, pkid));

                }




                return sb.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static DataTable getDataTable(string sql, params MySqlParameter[] pms)
        {
            DataTable dt;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = sql;
                        if (connection.State != ConnectionState.Open)
                            connection.Open();

                        if (pms != null)
                        {


                            foreach (MySqlParameter parameter in pms)
                            {
                                if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                                    (parameter.Value == null))
                                {
                                    parameter.Value = DBNull.Value;
                                }
                                cmd.Parameters.Add(parameter);
                            }
                        }
                        MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
                        dt = new DataTable();
                        mda.Fill(dt);

                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                throw e;
            }
            return dt;
        }

        #region 公用方法
        /// <summary>
        /// 得到最大值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static bool Exists(string strSql)
        {
            object obj = GetSingle(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 是否存在（基于MySqlParameter）
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static bool Exists(string strSql, params MySqlParameter[] cmdParms)
        {
            object obj = GetSingle(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region  执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (MySql.Data.MySqlClient.MySqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }



        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlConn(string SQLString, string connectStr)
        {
            using (MySqlConnection connection = new MySqlConnection(connectStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (MySql.Data.MySqlClient.MySqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        public static int ExecuteSqlByTime(string SQLString, int Times)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = Times;
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (MySql.Data.MySqlClient.MySqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>        
        public static int ExecuteSqlTran(List<String> SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                MySqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return count;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    throw ex;
                    return 0;
                }
            }
        }
        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, string content)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(SQLString, connection);
                MySql.Data.MySqlClient.MySqlParameter myParameter = new MySql.Data.MySqlClient.MySqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (MySql.Data.MySqlClient.MySqlException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static object ExecuteSqlGet(string SQLString, string content)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(SQLString, connection);
                MySql.Data.MySqlClient.MySqlParameter myParameter = new MySql.Data.MySqlClient.MySqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (MySql.Data.MySqlClient.MySqlException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(strSQL, connection);
                MySql.Data.MySqlClient.MySqlParameter myParameter = new MySql.Data.MySqlClient.MySqlParameter("@fs", SqlDbType.Image);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (MySql.Data.MySqlClient.MySqlException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (MySql.Data.MySqlClient.MySqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }
        public static object GetSingle(string SQLString, int Times)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = Times;
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (MySql.Data.MySqlClient.MySqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回MySqlDataReader ( 注意：调用该方法后，一定要对MySqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>MySqlDataReader</returns>
        public static MySqlDataReader ExecuteReader(string strSQL)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand(strSQL, connection);
            try
            {
                connection.Open();
                MySqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                throw e;
            }

        }


        /// <summary>
        /// 执行查询语句，返回MySqlDataReader ( 注意：调用该方法后，一定要对MySqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>MySqlDataReader</returns>
        public static MySqlDataReader ExecuteReader(string strSQL, string connectionStr)
        {
            MySqlConnection connection = new MySqlConnection(connectionStr);
            MySqlCommand cmd = new MySqlCommand(strSQL, connection);
            try
            {
                connection.Open();
                MySqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                throw e;
            }

        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    MySqlDataAdapter command = new MySqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet QueryConn(string SQLString, string conStr)
        {
            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    MySqlDataAdapter command = new MySqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                return ds;
            }
        }

        public static DataSet Query(string SQLString, int Times)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    MySqlDataAdapter command = new MySqlDataAdapter(SQLString, connection);
                    command.SelectCommand.CommandTimeout = Times;
                    command.Fill(ds, "ds");
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }



        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, params MySqlParameter[] cmdParms)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (MySql.Data.MySqlClient.MySqlException e)
                    {
                        throw e;
                    }
                }
            }
        }


        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的MySqlParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            MySqlParameter[] cmdParms = (MySqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的MySqlParameter[]）</param>
        public static void ExecuteSqlTranWithIndentity(Hashtable SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        int indentity = 0;
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            MySqlParameter[] cmdParms = (MySqlParameter[])myDE.Value;
                            foreach (MySqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.InputOutput)
                                {
                                    q.Value = indentity;
                                }
                            }
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            foreach (MySqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.Output)
                                {
                                    indentity = Convert.ToInt32(q.Value);
                                }
                            }
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, params MySqlParameter[] cmdParms)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (MySql.Data.MySqlClient.MySqlException e)
                    {
                        throw e;
                    }
                    cmd.Dispose();
                }
                connection.Close();

            }
        }

        /// <summary>
        /// 执行查询语句，返回MySqlDataReader ( 注意：调用该方法后，一定要对MySqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>MySqlDataReader</returns>
        public static MySqlDataReader ExecuteReader(string SQLString, params MySqlParameter[] cmdParms)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                MySqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                throw e;
            }
            //            finally
            //            {
            //                cmd.Dispose();
            //                connection.Close();
            //            }    

        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, params MySqlParameter[] cmdParms)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (MySql.Data.MySqlClient.MySqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }


        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, string cmdText, MySqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {


                foreach (MySqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        #endregion

        /// <summary>
        //@tblName     nvarchar(200),        ----要显示的表或多个表的连接
        //@fldName     nvarchar(500) = '*',    ----要显示的字段列表
        //@pageSize    int = 1,        ----每页显示的记录个数
        //@page        int = 10,        ----要显示那一页的记录
        //@pageCount    int = 1 output,            ----查询结果分页后的总页数
        //@Counts    int = 1 output,                ----查询到的记录数
        //@fldSort    nvarchar(200) = null,    ----排序字段列表或条件
        //@Sort        bit = 1,        ----排序方法，0为升序，1为降序(如果是多字段排列Sort指代最后一个排序字段的排列顺序(最后一个排序字段不加排序标记)--程序传参如：' SortA Asc,SortB Desc,SortC ')
        //@strCondition    nvarchar(1000) = null,    ----查询条件,不需where
        //@ID        nvarchar(150),        ----主表的主键
        //@Dist                 bit = 0           ----是否添加查询字段的 DISTINCT 默认0不添加/1添加

        /// </summary>
        /// <param name="tblName"></param>
        /// <param name="fldName"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="pagecout"></param>
        /// <param name="counts"></param>
        /// <param name="fldSort"></param>
        /// <param name="Sort"></param>
        /// <param name="strCondition"></param>
        /// <param name="ID"></param>
        /// <param name="Dist"></param>
        /// <returns></returns>
        public static DataTable RunProcedure_GetListPage(
            string tblName,
            string fldName,
            int pagesize,
            int page,
            ref int pagecout,
            ref int counts,
            string fldSort,
            int Sort,
            string strCondition,
            string ID,
            int Dist
            )
        {



            DataTable dt;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "P_ListPage";
                    cmd.CommandType = CommandType.StoredProcedure;
                    IDataParameter[] parameters = new IDataParameter[] { 
                        new MySqlParameter("?p_table_name", MySqlDbType.VarChar, 100), 
                        new MySqlParameter("?p_fields", MySqlDbType.VarChar, 100), 
                        new MySqlParameter("?p_page_size", MySqlDbType.Int32),                         
                        new MySqlParameter("?p_page_now", MySqlDbType.Int32), 
                        new MySqlParameter("?p_pagecount", MySqlDbType.Int32), 
                        new MySqlParameter("?p_totals", MySqlDbType.Int32), 
                        new MySqlParameter("?p_fldSort", MySqlDbType.VarChar, 200), 
                        new MySqlParameter("?p_sort", MySqlDbType.Int32), 
                        new MySqlParameter("?p_where_string", MySqlDbType.VarChar, 200), 
                        new MySqlParameter("?p_id", MySqlDbType.VarChar, 200)
                    };
                    parameters[0].Value = tblName;
                    parameters[1].Value = fldName;
                    parameters[2].Value = pagesize;
                    parameters[3].Value = page;
                    parameters[4].Direction = ParameterDirection.Output;
                    parameters[5].Direction = ParameterDirection.Output;
                    parameters[6].Value = fldSort;
                    parameters[7].Value = Sort;
                    parameters[8].Value = " 1=1 ";
                    if (strCondition != null)
                    {
                        parameters[8].Value = " 1=1 and  " + strCondition;
                    }
                    parameters[9].Value = ID;
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        cmd.Parameters.Add(parameters[i]);
                    }
                    MySqlDataAdapter dp = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    dp.Fill(ds);
                    pagecout = int.Parse(ds.Tables[1].Rows[0]["p_pagecount"].ToString());
                    counts = int.Parse(ds.Tables[1].Rows[0]["p_totals"].ToString());
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }



        /// <summary>
        // @tblName     nvarchar(200),        ----要显示的表或多个表的连接
        //@fldName     nvarchar(500) = '*',    ----要显示的字段列表
        //@pageSize    int = 1,        ----每页显示的记录个数
        //@page        int = 10,        ----要显示那一页的记录
        //@pageCount    int = 1 output,            ----查询结果分页后的总页数
        //@Counts    int = 1 output,                ----查询到的记录数
        //@fldSort    nvarchar(200) = null,    ----排序字段列表或条件
        //@Sort        bit = 1,        ----排序方法，0为升序，1为降序(如果是多字段排列Sort指代最后一个排序字段的排列顺序(最后一个排序字段不加排序标记)--程序传参如：' SortA Asc,SortB Desc,SortC ')
        //@strCondition    nvarchar(1000) = null,    ----查询条件,不需where
        //@ID        nvarchar(150),        ----主表的主键
        //@Dist                 bit = 0           ----是否添加查询字段的 DISTINCT 默认0不添加/1添加
        /// </summary>
        /// <param name="tblName"></param>
        /// <param name="fldName"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="pagecout"></param>
        /// <param name="counts"></param>
        /// <param name="fldSort"></param>
        /// <param name="Sort"></param>
        /// <param name="strCondition"></param>
        /// <param name="ID"></param>
        /// <param name="Dist"></param>
        /// <returns></returns>
        public static DataTable RunProcedure_GetListPagebySort(
            string tblName,
            string fldName,
            int pagesize,
            int page,
            ref int pagecout,
            ref int counts,
            string p_order_string,
            string strCondition,
            string ID,
            int Dist
            )
        {



            DataTable dt;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "P_ListPagebySort";
                    cmd.CommandType = CommandType.StoredProcedure;
                    IDataParameter[] parameters = new IDataParameter[] { 
                        new MySqlParameter("?p_table_name", MySqlDbType.VarChar, 100), 
                        new MySqlParameter("?p_fields", MySqlDbType.VarChar, 100), 
                        new MySqlParameter("?p_page_size", MySqlDbType.Int32),                         
                        new MySqlParameter("?p_page_now", MySqlDbType.Int32), 
                        new MySqlParameter("?p_pagecount", MySqlDbType.Int32), 
                        new MySqlParameter("?p_totals", MySqlDbType.Int32), 
                        new MySqlParameter("?p_order_string", MySqlDbType.VarChar, 200),                         
                        new MySqlParameter("?p_where_string", MySqlDbType.VarChar, 200), 
                        new MySqlParameter("?p_id", MySqlDbType.VarChar, 200)
                    };
                    parameters[0].Value = tblName;
                    parameters[1].Value = fldName;
                    parameters[2].Value = pagesize;
                    parameters[3].Value = page;
                    parameters[4].Direction = ParameterDirection.Output;
                    parameters[5].Direction = ParameterDirection.Output;
                    parameters[6].Value = p_order_string;
                    parameters[7].Value = " 1=1 ";
                    if (strCondition != null)
                    {
                        parameters[7].Value = " 1=1 and  " + strCondition;
                    }
                    parameters[8].Value = ID;
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        cmd.Parameters.Add(parameters[i]);
                    }
                    MySqlDataAdapter dp = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    dp.Fill(ds);
                    pagecout = int.Parse(ds.Tables[1].Rows[0]["p_pagecount"].ToString());
                    counts = int.Parse(ds.Tables[1].Rows[0]["p_totals"].ToString());
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
            }
            return dt;
        }





        /// <summary>
        // @tblName     nvarchar(200),        ----要显示的表或多个表的连接
        //@fldName     nvarchar(500) = '*',    ----要显示的字段列表
        //@pageSize    int = 1,        ----每页显示的记录个数
        //@page        int = 10,        ----要显示那一页的记录
        //@pageCount    int = 1 output,            ----查询结果分页后的总页数
        //@Counts    int = 1 output,                ----查询到的记录数
        //@fldSort    nvarchar(200) = null,    ----排序字段列表或条件
        //@Sort        bit = 1,        ----排序方法，0为升序，1为降序(如果是多字段排列Sort指代最后一个排序字段的排列顺序(最后一个排序字段不加排序标记)--程序传参如：' SortA Asc,SortB Desc,SortC ')
        //@strCondition    nvarchar(1000) = null,    ----查询条件,不需where
        //@ID        nvarchar(150),        ----主表的主键
        //@Dist                 bit = 0           ----是否添加查询字段的 DISTINCT 默认0不添加/1添加
        /// </summary>
        /// <param name="tblName"></param>
        /// <param name="fldName"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="pagecout"></param>
        /// <param name="counts"></param>
        /// <param name="fldSort"></param>
        /// <param name="Sort"></param>
        /// <param name="strCondition"></param>
        /// <param name="ID"></param>
        /// <param name="Dist"></param>
        /// <returns></returns>
        public static DataTable RunProcedure_GetListPagebyGroup(
            string tblName,
            string fldName,
            int pagesize,
            int page,
            ref int pagecout,
            ref int selfcounts,
            ref int counts,
            string p_order_string,
            string strCondition,
            string strgroup,
            string ID,
            int Dist
            )
        {



            DataTable dt;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "P_ListPagebyGroup";
                    cmd.CommandType = CommandType.StoredProcedure;
                    IDataParameter[] parameters = new IDataParameter[] { 
                        new MySqlParameter("?p_table_name", MySqlDbType.VarChar, 100), 
                        new MySqlParameter("?p_fields", MySqlDbType.VarChar, 1000), 
                        new MySqlParameter("?p_page_size", MySqlDbType.Int32),                         
                        new MySqlParameter("?p_page_now", MySqlDbType.Int32), 
                        new MySqlParameter("?p_pagecount", MySqlDbType.Int32), 
                        new MySqlParameter("?p_totals", MySqlDbType.Int32), 
                        new MySqlParameter("?p_order_string", MySqlDbType.VarChar, 200),                         
                        new MySqlParameter("?p_where_string", MySqlDbType.VarChar, 200), 
                        new MySqlParameter("?p_group_string", MySqlDbType.VarChar, 200), 
                        new MySqlParameter("?p_id", MySqlDbType.VarChar, 200)
                    };
                    parameters[0].Value = tblName;
                    parameters[1].Value = fldName;
                    parameters[2].Value = pagesize;
                    parameters[3].Value = page;
                    parameters[4].Direction = ParameterDirection.Output;
                    parameters[5].Direction = ParameterDirection.Output;
                    parameters[6].Value = p_order_string;
                    parameters[7].Value = " 1=1 ";
                    if (strCondition != null)
                    {
                        parameters[7].Value = " 1=1 and  " + strCondition;
                    }
                    if (strgroup != null && strgroup != "")
                    {
                        parameters[8].Value = strgroup;
                    }

                    parameters[9].Value = ID;
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        cmd.Parameters.Add(parameters[i]);
                    }
                    MySqlDataAdapter dp = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    dp.Fill(ds);

                    pagecout = int.Parse(ds.Tables[1].Rows[0]["p_pagecount"].ToString());
                    selfcounts = int.Parse(ds.Tables[1].Rows[0]["p_totals"].ToString());
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
            }
            return dt;
        }


    }

}
