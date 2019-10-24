using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Entity;
using DAL;

namespace BLL
{
	/// <summary>
    /// EmpoloyeeBLL
	/// </summary>
	public partial class EmployeeBLL
	{
        private readonly EmployeeDAL dal = new EmployeeDAL();
        public EmployeeBLL()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string Id)
		{
			return dal.Exists(Id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public bool Add(EmployeeEntity model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EmployeeEntity model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string Id)
		{
			
			return dal.Delete(Id);
		}		

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EmployeeEntity GetModel(string Id)
		{
			
			return dal.GetModel(Id);
		}

        /// <summary>
        /// 得到一个对象实体根据员工编码
        /// </summary>
        public EmployeeEntity GetModelByEmployeeNo(string EmployeeNo)
        {

            return dal.GetModelByEmployeeNo(EmployeeNo);
        }

        /// <summary>
        /// 根据Id和编码查询
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <returns></returns>
        public bool IsExistByIdAndEmployeeNo(string Id, string EmployeeNo)
        {
            return dal.IsExistByIdAndEmployeeNo(Id,EmployeeNo);
        }

        /// <summary>
        /// 根据编码查询
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <returns></returns>
        public bool IsExistByEmployeeNo(string EmployeeNo)
        {
            return dal.IsExistByEmployeeNo(EmployeeNo);
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<EmployeeEntity> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<EmployeeEntity> GetModelListByNoAndName(string employyeeNo, string employyeeName)
        {
            DataSet ds = dal.GetModelListByNoAndName(employyeeNo, employyeeName);
            return DataTableToList(ds.Tables[0]);
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<EmployeeEntity> DataTableToList(DataTable dt)
		{
            List<EmployeeEntity> modelList = new List<EmployeeEntity>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
                EmployeeEntity model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}


