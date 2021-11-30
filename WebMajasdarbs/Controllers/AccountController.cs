using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace WebMajasdarbs.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            using(Models.OurDbContext db = new Models.OurDbContext())
            {
                return View(db.userAccount.ToList());
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Models.UserAccount account)
        {
            if(ModelState.IsValid)
            {
                using(Models.OurDbContext db = new Models.OurDbContext())
                {
                    db.userAccount.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = account.PhoneNumber + " successfully registered.";
            }
            return View();
        }

        //Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.UserAccount user)
        {
            using (Models.OurDbContext db = new Models.OurDbContext())
            {
                var usr = db.userAccount.Single(u => u.PhoneNumber == 
                user.PhoneNumber && u.Password == user.Password);
                if (usr != null)
                {
                    HttpContext.Session["UserID"] = usr.UserID.ToString();
                    HttpContext.Session["PhoneNumber"] = usr.PhoneNumber.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "PhoneNumber or Password is wrong. ");
                }
            }
            return View();
        }
        public ActionResult LoggedIn()
        {
            if(HttpContext.Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}