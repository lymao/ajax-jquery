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
        public JsonResult LoadData(string name, string status, int page, int pageSize = 3)
        {
            IEnumerable<Employee> model = _context.Employees;
            if (!string.IsNullOrEmpty(name))
                model = model.Where(x => x.Name.Contains(name));
            if (!string.IsNullOrEmpty(status))
            {
                var statusBool = bool.Parse(status);
                model = model.Where(x => x.Status == statusBool);
            }
                int totalRow = model.Count();

             model = model.OrderBy(x => x.Name).Skip((page - 1) * pageSize).Take(pageSize);
            
            return Json(new
            {
                data = model,
                total = totalRow,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetDetail(int id)
        {
            var employee = _context.Employees.Find(id);
            return Json(new
            {
                data = employee,
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
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var entity = _context.Employees.Find(id);
            _context.Employees.Remove(entity);
            try
            {
                _context.SaveChanges();
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false,
                    message = ex.Message
                });
            }

        }
        [HttpPost]
        public JsonResult SaveData(string strEmployee)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Employee employee = serializer.Deserialize<Employee>(strEmployee);
            bool status = false;
            string message = string.Empty;
            if (employee.ID == 0)
            {
                //Add data
                employee.CreatedDate = DateTime.Now;
                _context.Employees.Add(employee);
                try
                {
                    _context.SaveChanges();
                    status = true;
                }
                catch (Exception ex)
                {
                    status = false;
                    message = ex.Message;
                }

            }
            else
            {
                //Update data
                var entity = _context.Employees.Find(employee.ID);
                entity.Salary = employee.Salary;
                entity.Name = employee.Name;
                entity.Status = employee.Status;
                try
                {
                    _context.SaveChanges();
                    status = true;
                }
                catch (Exception ex)
                {
                    status = false;
                    message = ex.Message;
                }


            }

            return Json(new
            {
                status = status,
                message = message
            });
        }
    }
}