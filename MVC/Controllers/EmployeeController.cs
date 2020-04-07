using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;

namespace MVC.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeBL employeeBL = new EmployeeBL();
        // GET: Employee
        
        public ActionResult Index()
        {
            return View(employeeBL.Employees);
        }

        public ActionResult About()
        {
            return View();
        }
        [HttpGet]
        [ActionName("Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post()
        {
            Employee employee = new Employee();
            TryUpdateModel<Employee>(employee);
            if (ModelState.IsValid)
            {
                employeeBL.AddEmployee(employee);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]

        public ActionResult Edit(int Id)
        {

            return View(employeeBL.Employees.Single(x => x.EmployeeId.Equals(Id)));
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {

            if (ModelState.IsValid)
            {
                employeeBL.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }
            return View();

        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {

            if (ModelState.IsValid)
            {
                employeeBL.DeleteEmployee(Id);
                return RedirectToAction("Index");
            }
            return View();

        }
    }
}