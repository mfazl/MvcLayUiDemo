using BLL;
using Commom;
using Entity;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MvcLayuiDemo.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeBLL empbll = new EmployeeBLL();
        DepartmentBLL deptbll = new DepartmentBLL();
        // GET: Employee
        [HttpGet]
        public ViewResult List()
        {
            string jsonStr = string.Empty;
            List<EmployeeEntity> emplist = empbll.GetModelList("");
            List<EmployeeInfo> empinfolist = new List<EmployeeInfo>();
            foreach (EmployeeEntity entity in emplist)
            {

                EmployeeInfo empInfo = new EmployeeInfo();

                //获取部门名称显示
                DepartmentEntity deptEntity = new DepartmentEntity();
                deptEntity = deptbll.GetModel(entity.DepartmentId);
                empInfo.Id = entity.Id;
                empInfo.DepartmentId = deptEntity.DepartmentName;

                empInfo.EmployeeNo = entity.EmployeeNo;
                empInfo.EmployeeName = entity.EmployeeName;
                //生日
                empInfo.EmployeeBirth = entity.EmployeeBirth.ToString();
                empInfo.EmployeeSex = entity.EmployeeSex;
                if (entity.IsJob == 1)
                {
                    empInfo.IsJob = true;
                    empInfo.IsJobTra = "是";
                }
                else
                {
                    empInfo.IsJob = false;
                    empInfo.IsJobTra = "否";
                }
                empInfo.Remarks = entity.Remarks;
                empinfolist.Add(empInfo);
            }
            return View(empinfolist);
        }

        public ActionResult Create()
        {
            List<DepartmentEntity> deptEntityList= deptbll.GetModelList("");
            List<DepartmentInfo> deptinfoList = new List<DepartmentInfo>();
            foreach (DepartmentEntity entity in deptEntityList)
            {
                DepartmentInfo deptinfo = new DepartmentInfo();
                deptinfo.Id = entity.Id;
                deptinfo.DepartmentName = entity.DepartmentName;
                deptinfo.DepartmentNo = entity.DepartmentNo;
                deptinfoList.Add(deptinfo);
            }
            return View(deptinfoList);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeInfo model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (model.EmployeeName.Length > 15)
            {
                ViewBag.IsError = true;
                return View();
            }
            var entity = new EmployeeEntity();
            entity.Id = Guid.NewGuid().ToString();
            entity.DepartmentId = model.DepartmentId;
            entity.EmployeeNo = model.EmployeeNo;
            entity.EmployeeName = model.EmployeeName;
            entity.EmployeeBirth = Convert.ToDateTime(model.EmployeeBirth);
            entity.EmployeeSex = model.EmployeeSex;
            if (model.IsJob==true)
            {
                entity.IsJob = 1;
            }
            else
            {
                entity.IsJob = 0;
            }
            entity.Remarks = model.Remarks;
           

            empbll.Add(entity);
            return RedirectToAction("List");
        }
        public ActionResult empList()
        {
            string jsonStr = string.Empty;
            List<EmployeeEntity> emplist = empbll.GetModelList("");
            List<EmployeeInfo> empinfolist = new List<EmployeeInfo>();
            foreach (EmployeeEntity entity in emplist)
            {
                
                EmployeeInfo empInfo = new EmployeeInfo();

                //获取部门名称显示
                DepartmentEntity deptEntity = new DepartmentEntity();
                deptEntity = deptbll.GetModel(entity.DepartmentId);
                empInfo.DepartmentId =deptEntity.DepartmentName;

                empInfo.EmployeeNo = entity.EmployeeNo;
                empInfo.EmployeeName = entity.EmployeeName;
                //生日
                empInfo.EmployeeBirth = entity.EmployeeBirth.ToString();
                empInfo.EmployeeSex = entity.EmployeeSex;
                if (entity.IsJob==1)
                {
                    empInfo.IsJob = true;
                }
                else
                {
                    empInfo.IsJob = false;
                }
                empInfo.Remarks = entity.Remarks;
                empinfolist.Add(empInfo);
            }
            //把一个list转换为json字符串
            return Json(empinfolist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("List");
            var entity = empbll.GetModel(id);
            if (entity==null)
            {
                return RedirectToAction("List");
            }
            EmployeeInfo entityInfo = new EmployeeInfo();
            entityInfo.Id = id;
            entityInfo.DepartmentName = deptbll.GetModel(entity.DepartmentId).DepartmentName;
            entityInfo.DepartmentId = entity.DepartmentId;
           entityInfo.EmployeeNo = entity.EmployeeNo;
            entityInfo.EmployeeName = entity.EmployeeName;
            entityInfo.EmployeeBirth = entity.EmployeeBirth.ToString();
            entityInfo.EmployeeSex = entity.EmployeeSex;
            if (entity.IsJob==1)
            {
                entityInfo.IsJob = true;
            }
            else
            {
                entityInfo.IsJob = false;
            }
            entityInfo.Remarks = entity.Remarks;

            //部门下拉列表
            List<DepartmentEntity> entityList=deptbll.GetModelList("");

            foreach (DepartmentEntity deptentity in entityList)
            {
                DepartmentInfo deptinfo = new DepartmentInfo();
                deptinfo.Id = deptentity.Id;
                deptinfo.DepartmentNo = deptentity.DepartmentNo;
                deptinfo.DepartmentName = deptentity.DepartmentName;
                deptinfo.Remarks = deptentity.Remarks;
                entityInfo.deptlist.Add(deptinfo);
            }

            return View(entityInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeInfo model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            if (model.EmployeeName.Length > 15)
            {
                ViewBag.IsError = true;
                return View();
            }

            var entity = empbll.GetModel(model.Id);
            entity.EmployeeName = model.EmployeeName;
            entity.DepartmentId = model.DepartmentId;
            entity.EmployeeNo = model.EmployeeNo;
            entity.EmployeeSex = model.EmployeeSex;
            entity.EmployeeBirth = Convert.ToDateTime(model.EmployeeBirth);
            entity.IsJob = Convert.ToInt32(model.IsJob);
            entity.Remarks = model.Remarks;

            empbll.Update(entity);

            return RedirectToAction("List");

        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            var Id = id;
            if (string.IsNullOrEmpty(Id))
                return Json(new { success = false, message = "删除失败，请刷新页面重试！" });      
            empbll.Delete(Id);

            //return Json(new { success = true, message = "删除成功!" });
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult CheckEmployeeNo()
        {
            var value = Request["EmployeeNo"];
            var flag = false;
            //根据员工编码判断此员工编码是否已经存在
            bool exist = empbll.IsExistByEmployeeNo(value);
            if (exist == true)
            {
                flag = true;
            }
            return Json(new { success = flag, message = flag ? "员工名称已存在" : "名称可用！" }, JsonRequestBehavior.AllowGet);
        }
    }
}