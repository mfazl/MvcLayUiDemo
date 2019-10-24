using Commom;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// EmpoloyeeModel:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class EmployeeInfo
    {
        public EmployeeInfo()
        { 
        }
        #region Model
        /// <summary>
        /// 主键Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 部门Id
        /// </summary>
        public string DepartmentId { get; set; }
        /// <summary>
        /// 员工名称
        /// </summary>
        public string  DepartmentName { get; set; }
        /// <summary>
        /// 员工编码
        /// </summary>
        public string EmployeeNo { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName{ get; set;}
        /// <summary>
        /// 员工性别
        /// </summary>
        public string EmployeeSex { get; set; }
        /// <summary>
        /// 员工生日
        /// </summary>        
        public string EmployeeBirth { get; set; }
        /// <summary>
        /// 是否在职
        /// </summary>
        public bool IsJob { get; set; }

        /// <summary>
        /// 是否在职
        /// </summary>
        public string IsJobTra { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        public List<DepartmentInfo> deptlist { get; set; } = new List<DepartmentInfo>();
        #endregion Model

    }
}


