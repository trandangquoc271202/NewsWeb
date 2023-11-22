using NewsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsWeb.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            string email = form["email"];
            string password = form["password"];
            Model1 db = new Model1();
            Customer user = db.Customers.FirstOrDefault(c => c.email == email && c.password == password);

            if (user != null)
            {

                Customer loggedInUser = new Customer
                {
                    email = user.email,
                    permission = user.permission
                };

                Session["LoggedInUser"] = loggedInUser;
                if (loggedInUser.permission.Trim() == "admin")
                {

                    return RedirectToAction("HomeAdmin", "HomeAdmin");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            else
            {
                ViewBag.ErrorMessage = "Email hoặc mật khẩu không đúng";
                return View();
            }
        }
    }
}