using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DBUtility;
using Entity;

namespace DAL
{  
	/// <summary>
	/// 数据访问类:EmpoloyeeDAL
	/// </summary>
	public partial class EmployeeDAL
	{
        public EmployeeDAL()
		{

        }
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ms_empoloyee");
			strSql.Append(" where Id=@Id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@Id", MySqlDbType.VarChar,50)			};
			parameters[0].Value = Id;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public bool Add(EmployeeEntity model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ms_empoloyee(");
			strSql.Append("Id,DepartmentId,EmployeeNo,EmployeeName,EmployeeSex,EmployeeBirth,IsJob,Remarks)");
			strSql.Append(" values (");
			strSql.Append("@Id,@DepartmentId,@EmployeeNo,@EmployeeName,@EmployeeSex,@EmployeeBirth,@IsJob,@Remarks)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@Id", MySqlDbType.VarChar,50),
					new MySqlParameter("@DepartmentId", MySqlDbType.VarChar,50),
					new MySqlParameter("@EmployeeNo", MySqlDbType.VarChar,10),
					new MySqlParameter("@EmployeeName", MySqlDbType.VarChar,20),
					new MySqlParameter("@EmployeeSex", MySqlDbType.VarChar,10),
					new MySqlParameter("@EmployeeBirth", MySqlDbType.DateTime),
					new MySqlParameter("@IsJob", MySqlDbType.Int32,1),
					new MySqlParameter("@Remarks", MySqlDbType.VarChar,100)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.DepartmentId;
			parameters[2].Value = model.EmployeeNo;
			parameters[3].Value = model.EmployeeName;
			parameters[4].Value = model.EmployeeSex;
			parameters[5].Value = model.EmployeeBirth;
			parameters[6].Value = model.IsJob;
			parameters[7].Value = model.Remarks;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
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
        public bool Update(EmployeeEntity model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ms_empoloyee set ");
			strSql.Append("DepartmentId=@DepartmentId,");
			strSql.Append("EmployeeNo=@EmployeeNo,");
			strSql.Append("EmployeeName=@EmployeeName,");
			strSql.Append("EmployeeSex=@EmployeeSex,");
			strSql.Append("EmployeeBirth=@EmployeeBirth,");
			strSql.Append("IsJob=@IsJob,");
			strSql.Append("Remarks=@Remarks");
			strSql.Append(" where Id=@Id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@DepartmentId", MySqlDbType.VarChar,50),
					new MySqlParameter("@EmployeeNo", MySqlDbType.VarChar,10),
					new MySqlParameter("@EmployeeName", MySqlDbType.VarChar,20),
					new MySqlParameter("@EmployeeSex", MySqlDbType.VarChar,10),
					new MySqlParameter("@EmployeeBirth", MySqlDbType.DateTime),
					new MySqlParameter("@IsJob", MySqlDbType.Int32,1),
					new MySqlParameter("@Remarks", MySqlDbType.VarChar,100),
					new MySqlParameter("@Id", MySqlDbType.VarChar,50)};
			parameters[0].Value = model.DepartmentId;
			parameters[1].Value = model.EmployeeNo;
			parameters[2].Value = model.EmployeeName;
			parameters[3].Value = model.EmployeeSex;
			parameters[4].Value = model.EmployeeBirth;
			parameters[5].Value = model.IsJob;
			parameters[6].Value = model.Remarks;
			parameters[7].Value = model.Id;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ms_empoloyee ");
			strSql.Append(" where Id=@Id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@Id", MySqlDbType.VarChar,50)			};
			parameters[0].Value = Id;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ms_empoloyee ");
			strSql.Append(" where Id in ("+Idlist + ")  ");
			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString());
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
        public EmployeeEntity GetModel(string Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,DepartmentId,EmployeeNo,EmployeeName,EmployeeSex,EmployeeBirth,IsJob,Remarks from ms_empoloyee ");
			strSql.Append(" where Id=@Id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@Id", MySqlDbType.VarChar,50)			};
			parameters[0].Value = Id;

            EmployeeEntity model = new EmployeeEntity();
			DataSet ds=DbHelperMySQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <returns></returns>
        public EmployeeEntity GetModelByEmployeeNo(string EmployeeNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,DepartmentId,EmployeeNo,EmployeeName,EmployeeSex,EmployeeBirth,IsJob,Remarks from ms_empoloyee ");
            strSql.Append(" where EmployeeNo=@EmployeeNo ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@EmployeeNo", MySqlDbType.VarChar,50)			};
            parameters[0].Value = EmployeeNo;

            EmployeeEntity model = new EmployeeEntity();
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
        /// <param name="Id"></param>
        /// <param name="EmployeeNo"></param>
        /// <returns></returns>
        public bool IsExistByIdAndEmployeeNo(string Id,string EmployeeNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ms_empoloyee");
            strSql.Append(" where EmployeeNo=@EmployeeNo and Id!=@Id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@EmployeeNo", MySqlDbType.VarChar,50),
                    new MySqlParameter("@Id", MySqlDbType.VarChar,50)
                                          };
            parameters[0].Value = EmployeeNo;
            parameters[1].Value = Id;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据编码查询
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <returns></returns>
        public bool IsExistByEmployeeNo(string EmployeeNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ms_empoloyee");
            strSql.Append(" where EmployeeNo=@EmployeeNo");
            MySqlParameter[] parameters = {
					new MySqlParameter("@EmployeeNo", MySqlDbType.VarChar,50)	
                                          };
            parameters[0].Value = EmployeeNo;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }       

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public EmployeeEntity DataRowToModel(DataRow row)
		{
            EmployeeEntity model = new EmployeeEntity();
			if (row != null)
			{
				if(row["Id"]!=null)
				{
					model.Id=row["Id"].ToString();
				}
				if(row["DepartmentId"]!=null)
				{
					model.DepartmentId=row["DepartmentId"].ToString();
				}
				if(row["EmployeeNo"]!=null)
				{
					model.EmployeeNo=row["EmployeeNo"].ToString();
				}
				if(row["EmployeeName"]!=null)
				{
					model.EmployeeName=row["EmployeeName"].ToString();
				}
				if(row["EmployeeSex"]!=null)
				{
					model.EmployeeSex=row["EmployeeSex"].ToString();
				}
				if(row["EmployeeBirth"]!=null && row["EmployeeBirth"].ToString()!="")
				{
					model.EmployeeBirth=DateTime.Parse(row["EmployeeBirth"].ToString());
				}
				if(row["IsJob"]!=null && row["IsJob"].ToString()!="")
				{
                    if (row["IsJob"].ToString()=="True")
                    {
                        model.IsJob = 1;
                    }
                    else
                    {
                        model.IsJob = 0;
                    }
				}
				if(row["Remarks"]!=null)
				{
					model.Remarks=row["Remarks"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,DepartmentId,EmployeeNo,EmployeeName,EmployeeSex,EmployeeBirth,IsJob,Remarks ");
			strSql.Append(" FROM ms_empoloyee ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            strSql.Append(" Order by DepartmentId,EmployeeNo asc ");
			return DbHelperMySQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="employeeNo"></param>
        /// <param name="employeeName"></param>
        /// <returns></returns>
        public DataSet GetModelListByNoAndName(string employeeNo, string employeeName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,DepartmentId,EmployeeNo,EmployeeName,EmployeeSex,EmployeeBirth,IsJob,Remarks ");
            strSql.Append(" FROM ms_empoloyee ");
            strSql.Append(" where 1=1 ");
            if (employeeNo.Trim() != "")
            {
                strSql.Append(" and EmployeeNo like '%" + employeeNo + "%'");
            }
            if (employeeName.Trim() != "")
            {
                strSql.Append(" and EmployeeName like '%" + employeeName + "%'");
            }
            strSql.Append(" Order by DepartmentId,EmployeeNo asc ");
            return DbHelperMySQL.Query(strSql.ToString());
        }        


		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM ms_empoloyee ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.Id desc");
			}
			strSql.Append(")AS Row, T.*  from ms_empoloyee T ");
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
			parameters[0].Value = "ms_empoloyee";
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


