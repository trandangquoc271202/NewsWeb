﻿using NewsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsWeb.Areas.Admin.Controllers
{
    public class AccoutManageController : Controller
    {
        Model1 db = new Model1();
        // GET: Admin/AccoutManager
        public ActionResult Account()
        {
           // var loggedInUser = Session["LoggedInUser"] as NewsWeb.Models.Users;
            //if (loggedInUser != null && loggedInUser.permission == "admin")
           // {
                List<Users> ItemList = db.Users.ToList();
                ViewBag.ItemList = ItemList;
                return View();
            //}
           // return Redirect("/Login/Login");
        }
        [HttpGet]
        public ActionResult Detail()
        {
            try
            {
                int id = int.Parse(Request["id"]);
                Users user = db.Users.FirstOrDefault(c => c.id == id);
                if(user != null)
                {
                    return View(user);
                }
            }
            catch (Exception e)
            {
                
            }
            return View();
        }
        public ActionResult Update(Users user)
        {

            if (ModelState.IsValid)
            {
                Users userUpdate = db.Users.FirstOrDefault(c => c.id == user.id);
                if (userUpdate != null)
                {
                    userUpdate.name = user.name;
                    userUpdate.permission = user.permission;
                    userUpdate.status = user.status;
                    userUpdate.phone = user.phone;
                    userUpdate.address = user.address;
                    userUpdate.birthDay = user.birthDay;
                    db.SaveChanges();
                }
            }

            return Redirect("/Admin/AccoutManage/Detail?id=" + user.id);
        }
        public ActionResult Delete()
        {
            int id = int.Parse(Request["id"]);
            var user = db.Users.FirstOrDefault(u => u.id == id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }

            return Redirect("/Admin/AccoutManage/Account");
        }
    }
}