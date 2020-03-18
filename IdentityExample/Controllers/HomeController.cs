using IdentityExample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityExample.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateRole(string role)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var r = new IdentityRole(role);
            roleManager.Create(r);
            return RedirectToAction("Create");
        }
        [Authorize]
        public ActionResult CreateUser()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            ViewBag.roles = roleManager.Roles.ToList();

            return View();
        }
        [Authorize(Roles ="Admin")]
        public ActionResult EmployeeIndex()
        {
            return View(context.tblEmployee.ToList());
        }
        public ActionResult CreateEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateEmployee(Employee e)
        {
            context.tblEmployee.Add(e);
            context.SaveChanges();
            return RedirectToAction("EmployeeIndex");
        }
    }
}