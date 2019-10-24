using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    /// <summary>
    /// ms_department:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class DepartmentEntity
    {
        public DepartmentEntity()
        { }
        #region Entity
        private string _id;
        private string _departmentno;
        private string _departmentname;
        private string _remarks;
        /// <summary>
        /// 主键Id
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 部门编码
        /// </summary>
        public string DepartmentNo
        {
            set { _departmentno = value; }
            get { return _departmentno; }
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName
        {
            set { _departmentname = value; }
            get { return _departmentname; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        #endregion Entity

    }
}




