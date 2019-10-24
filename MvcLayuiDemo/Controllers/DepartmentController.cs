using BLL;
using Entity;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLayuiDemo.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentBLL deptbll = new DepartmentBLL();
        // GET: Department
        [HttpGet]
        public ViewResult List()
        {
            string jsonStr = string.Empty;
            List<DepartmentEntity> deptlist = deptbll.GetModelList("");
            List<DepartmentInfo> deptinfolist = new List<DepartmentInfo>();
            foreach (DepartmentEntity entity in deptlist)
            {

                DepartmentInfo deptInfo = new DepartmentInfo();
                deptInfo.Id = entity.Id;
                deptInfo.DepartmentNo = entity.DepartmentNo;
                deptInfo.DepartmentName = entity.DepartmentName;
                deptInfo.Remarks = entity.Remarks;
                deptinfolist.Add(deptInfo);
            }
            return View(deptinfolist);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentInfo model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (model.DepartmentName.Length > 15)
            {
                ViewBag.IsError = true;
                return View();
            }

            var entity = new DepartmentEntity
            {
                Id = Guid.NewGuid().ToString(),
                DepartmentName = model.DepartmentName,
                DepartmentNo = model.DepartmentNo,
                Remarks = model.Remarks
            };

            deptbll.Add(entity);
            return RedirectToAction("List");
        }
        public ActionResult empList()
        {
            string jsonStr = string.Empty;
            List<DepartmentEntity> deptlist = deptbll.GetModelList("");
            List<DepartmentInfo> deptinfolist = new List<DepartmentInfo>();
            foreach (DepartmentEntity entity in deptlist)
            {

                DepartmentInfo deptInfo = new DepartmentInfo();
                deptInfo.Id = entity.Id;
                deptInfo.DepartmentNo = entity.DepartmentNo;
                deptInfo.DepartmentName = entity.DepartmentName;
                deptInfo.Remarks = entity.Remarks;
                deptinfolist.Add(deptInfo);
            }
            //把一个list转换为json字符串
            return Json(deptinfolist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("List");
            var entity = deptbll.GetModel(id);
            if (entity == null)
            {
                return RedirectToAction("List");
            }
            DepartmentInfo deptInfo = new DepartmentInfo();
            deptInfo.Id = entity.Id;
            deptInfo.DepartmentNo = entity.DepartmentNo;
            deptInfo.DepartmentName = entity.DepartmentName;
            deptInfo.Remarks = entity.Remarks;

            return View(deptInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DepartmentInfo model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            if (model.DepartmentName.Length > 15)
            {
                ViewBag.IsError = true;
                return View();
            }

            var entity = deptbll.GetModel(model.Id);
            entity.Id = model.Id;
            entity.DepartmentNo = model.DepartmentNo;
            entity.DepartmentName = model.DepartmentName;
            entity.Remarks = model.Remarks;

            deptbll.Update(entity);

            return RedirectToAction("List");

        }

        [HttpGet]
        public ActionResult Delete(string  id)
        {
            var Id =id;
            if (string.IsNullOrEmpty(Id))
                return Json(new { success = false, message = "删除失败，请刷新页面重试！" });

            //判断该部门下是否存在员工
            //var exists = deptbll.Exists(Id);
            //if (exists)
            //    return Json(new { success = false, message = "该部门下存在员工，请先删除该部门下的员工！" });

            deptbll.Delete(Id);
            //JsonResult result = new JsonResult();
            //result.Data = new
            //{
            //    success = true,
            //    message = "删除成功!"
            //};
            //result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult CheckDepartmentNo()
        {
            var value = Request["EmployeeNo"];
            var flag = false;
            //根据员工编码判断此员工编码是否已经存在
            bool exist = deptbll.IsExistByDepartmentNo(value);
            if (exist == true)
            {
                flag = true;
            }
            return Json(new { success = flag, message = flag ? "部门编码已存在" : "编码可用！" }, JsonRequestBehavior.AllowGet);
        }
    }
}