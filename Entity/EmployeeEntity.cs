using Commom;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    /// <summary>
    /// EmpoloyeeModel:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class EmployeeEntity
    {
        public EmployeeEntity()
        { }
        #region Entity
        private string _id;
        private string _departmentid;
        private string _employeeno;
        private string _employeename;
        private string _employeesex;
        private DateTime? _employeebirth;
        private int? _isjob;
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
        /// 部门Id
        /// </summary>
        public string DepartmentId
        {
            set { _departmentid = value; }
            get { return _departmentid; }
        }
        /// <summary>
        /// 员工编码
        /// </summary>
        public string EmployeeNo
        {
            set { _employeeno = value; }
            get { return _employeeno; }
        }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName
        {
            set { _employeename = value; }
            get { return _employeename; }
        }
        /// <summary>
        /// 员工性别
        /// </summary>
        public string EmployeeSex
        {
            set { _employeesex = value; }
            get { return _employeesex; }
        }
        /// <summary>
        /// 员工生日
        /// </summary>   
        public DateTime? EmployeeBirth
        {
            set { _employeebirth = value; }
            get { return _employeebirth; }
        }
        /// <summary>
        /// 是否在职
        /// </summary>
        public int? IsJob
        {
            set { _isjob = value; }
            get { return _isjob; }
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


