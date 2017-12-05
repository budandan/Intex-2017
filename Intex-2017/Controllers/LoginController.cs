﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Intex_2017.Models;
using Intex_2017.DAL;

namespace Intex_2017.Controllers
{
    public class LoginController : Controller
    {
        private IntexContext db = new IntexContext();
        //Get
        public ActionResult CustomerLogin()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult CustomerLogin(FormCollection form, bool rememberMe = false)
        {
            String userName = form["userName"].ToString();
            String password = form["Password"].ToString();

            List<Customer> customerList = new List<Customer>();
            bool usernameFound = false;
            customerList = db.Customers.ToList();

            foreach (Customer c in customerList)
            {
                if (c.CustUsername == userName)
                {
                    if (c.CustPassword == password)
                    {
                        FormsAuthentication.SetAuthCookie(userName, rememberMe);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "Invalid Password";
                    }
                    usernameFound = true;
                }
            }
            if (usernameFound != true)
            {
                ViewBag.Message = "User Name not found";
            }
            return View();
        }
        //Get
        public ActionResult EmployeeLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EmployeeLogin(FormCollection form, bool rememberMe = false)
        {
            String userName = form["userName"].ToString();
            String password = form["Password"].ToString();

            List<Employee> employeeList = new List<Employee>();
            bool usernameFound = false;
            employeeList = db.Employees.ToList();

            foreach (Employee e in employeeList)
            {
                if (e.EmpUsername == userName)
                {
                    if (e.EmpPassword == password)
                    {
                        FormsAuthentication.SetAuthCookie(userName, rememberMe);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "Invalid Password";
                    }
                    usernameFound = true;
                }
            }
            if (usernameFound != true)
            {
                ViewBag.Message = "User Name not found";
            }
            return View();
        }
        [Authorize]
        public ActionResult Logout()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("CustomerLogin", "Login");
            }
            else
            {
                return RedirectToAction("CustomerLogin", "Login");
            }
        }
    }
}