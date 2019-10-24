using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DBUtility;//Please add references
using Entity;

namespace DAL
{

    /// <summary>
    /// 数据访问类:DepartmentDAL
    /// </summary>
    public partial class DepartmentDAL
    {
        public DepartmentDAL()
        { 

        }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ms_department");
            strSql.Append(" where Id=@Id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@Id", MySqlDbType.VarChar,50)			};
            parameters[0].Value = Id;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(DepartmentEntity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ms_department(");
            strSql.Append("Id,DepartmentNo,DepartmentName,Remarks)");
            strSql.Append(" values (");
            strSql.Append("@Id,@DepartmentNo,@DepartmentName,@Remarks)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@Id", MySqlDbType.VarChar,50),
					new MySqlParameter("@DepartmentNo", MySqlDbType.VarChar,10),
					new MySqlParameter("@DepartmentName", MySqlDbType.VarChar,20),
					new MySqlParameter("@Remarks", MySqlDbType.VarChar,100)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.DepartmentNo;
            parameters[2].Value = model.DepartmentName;
            parameters[3].Value = model.Remarks;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DepartmentEntity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ms_department set ");
            strSql.Append("DepartmentNo=@DepartmentNo,");
            strSql.Append("DepartmentName=@DepartmentName,");
            strSql.Append("Remarks=@Remarks");
            strSql.Append(" where Id=@Id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@DepartmentNo", MySqlDbType.VarChar,10),
					new MySqlParameter("@DepartmentName", MySqlDbType.VarChar,20),
					new MySqlParameter("@Remarks", MySqlDbType.VarChar,100),
					new MySqlParameter("@Id", MySqlDbType.VarChar,50)};
            parameters[0].Value = model.DepartmentNo;
            parameters[1].Value = model.DepartmentName;
            parameters[2].Value = model.Remarks;
            parameters[3].Value = model.Id;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ms_department ");
            strSql.Append(" where Id=@Id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@Id", MySqlDbType.VarChar,50)			};
            parameters[0].Value = Id;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ms_department ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DepartmentEntity GetModel(string Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,DepartmentNo,DepartmentName,Remarks from ms_department ");
            strSql.Append(" where Id=@Id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@Id", MySqlDbType.VarChar,50)			};
            parameters[0].Value = Id;

            DepartmentEntity model = new DepartmentEntity();
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体根据DepartmentNo
        /// </summary>
        public DepartmentEntity GetModelByDepartmentNo(string DepartmentNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,DepartmentNo,DepartmentName,Remarks from ms_department ");
            strSql.Append(" where DepartmentNo=@DepartmentNo ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@DepartmentNo", MySqlDbType.VarChar,50)			};
            parameters[0].Value = DepartmentNo;

            DepartmentEntity model = new DepartmentEntity();
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据Id和编码查询
        /// </summary>
        public bool IsExistByIdAndDepartmentNo(string Id, string DepartmentNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ms_department");
            strSql.Append(" where DepartmentNo=@DepartmentNo and Id!=@Id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@DepartmentNo", MySqlDbType.VarChar,50),
                    new MySqlParameter("@Id", MySqlDbType.VarChar,50)
                                          };
            parameters[0].Value = DepartmentNo;
            parameters[1].Value = Id;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据编码查询
        /// </summary>
        public bool IsExistByDepartmentNo(string DepartmentNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ms_department");
            strSql.Append(" where DepartmentNo=@DepartmentNo");
            MySqlParameter[] parameters = {
					new MySqlParameter("@DepartmentNo", MySqlDbType.VarChar,50)	
                                          };
            parameters[0].Value = DepartmentNo;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }       

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DepartmentEntity DataRowToModel(DataRow row)
        {
            DepartmentEntity model = new DepartmentEntity();
            if (row != null)
            {
                if (row["Id"] != null)
                {
                    model.Id = row["Id"].ToString();
                }
                if (row["DepartmentNo"] != null)
                {
                    model.DepartmentNo = row["DepartmentNo"].ToString();
                }
                if (row["DepartmentName"] != null)
                {
                    model.DepartmentName = row["DepartmentName"].ToString();
                }
                if (row["Remarks"] != null)
                {
                    model.Remarks = row["Remarks"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,DepartmentNo,DepartmentName,Remarks ");
            strSql.Append(" FROM ms_department");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" Order by DepartmentNo asc ");
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="departmentNo"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        public DataSet GetModelListByNoAndName(string departmentNo, string departmentName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,DepartmentNo,DepartmentName,Remarks ");
            strSql.Append(" FROM ms_department");
            strSql.Append(" where 1=1 ");
            if (departmentNo.Trim() != "")
            {
                strSql.Append(" and DepartmentNo like '%" + departmentNo + "%'");
            }
            if (departmentName.Trim() != "")
            {
                strSql.Append(" and DepartmentName like '%" + departmentName + "%'");
            }
            strSql.Append(" Order by DepartmentNo asc ");
            return DbHelperMySQL.Query(strSql.ToString());
        }        

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM ms_department ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperMySQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }            
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Id desc");
            }
            strSql.Append(")AS Row, T.*  from ms_department T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            MySqlParameter[] parameters = {
                    new MySqlParameter("@tblName", MySqlDbType.VarChar, 255),
                    new MySqlParameter("@fldName", MySqlDbType.VarChar, 255),
                    new MySqlParameter("@PageSize", MySqlDbType.Int32),
                    new MySqlParameter("@PageIndex", MySqlDbType.Int32),
                    new MySqlParameter("@IsReCount", MySqlDbType.Bit),
                    new MySqlParameter("@OrderType", MySqlDbType.Bit),
                    new MySqlParameter("@strWhere", MySqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "ms_department";
            parameters[1].Value = "Id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperMySQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}


