using JQueryAjax.Data;
using JQueryAjax.Data.Model;
using JQueryAjax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace JQueryAjax.Controllers
{
    public class HomeController : Controller
    {
        EmployeeDbContext _context;
        public HomeController()
        {
            _context = new EmployeeDbContext();
        } 

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult LoadData(int page, int pageSize = 3)
        {
            var model = _context.Employees.OrderBy(x=>x.Name).Skip((page - 1) * pageSize).Take(pageSize);
            int totalRow = _context.Employees.Count();
            return Json(new
            {
                data = model,
                total = totalRow,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Update(string model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Employee employee = serializer.Deserialize<Employee>(model);

            //save db
            var entity = _context.Employees.Find(employee.ID);
            entity.Salary = employee.Salary;
            return Json(new
            {
                status = true
            });
        }
    }
}