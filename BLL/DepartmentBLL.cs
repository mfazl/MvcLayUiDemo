using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;
using Entity;

namespace BLL
{

    /// <summary>
    /// DepartmentBLL
    /// </summary>
    public partial class DepartmentBLL
    {
        private readonly DepartmentDAL dal = new DepartmentDAL();
        public DepartmentBLL()
        { }
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
        public bool Add(DepartmentEntity model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DepartmentEntity model)
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
        public DepartmentEntity GetModel(string Id)
        {
            return dal.GetModel(Id);
        }

        /// <summary>
        /// 得到一个对象实体根据DepartmentNo
        /// </summary>
        /// <param name="DepartmentNo"></param>
        /// <returns></returns>
        public DepartmentEntity GetModelByDepartmentNo(string DepartmentNo)
        {
            return dal.GetModelByDepartmentNo(DepartmentNo);
        }

        /// <summary>
        /// 根据Id和编码查询
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <returns></returns>
        public bool IsExistByIdAndDepartmentNo(string Id, string DepartmentNo)
        {
            return dal.IsExistByIdAndDepartmentNo(Id, DepartmentNo);
        }

        /// <summary>
        /// 根据编码查询
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <returns></returns>
        public bool IsExistByDepartmentNo(string EmployeeNo)
        {
            return dal.IsExistByDepartmentNo(EmployeeNo);
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
        public List<DepartmentEntity> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DepartmentEntity> GetModelListByNoAndName(string departmentNo,string departmentName)
        {
            DataSet ds = dal.GetModelListByNoAndName(departmentNo, departmentName);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DepartmentEntity> DataTableToList(DataTable dt)
        {
            List<DepartmentEntity> modelList = new List<DepartmentEntity>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                DepartmentEntity model;
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
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
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



