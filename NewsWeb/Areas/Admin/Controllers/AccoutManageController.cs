using NewsWeb.Models;
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
            List<Users> ItemList = db.Users.ToList();
            ViewBag.ItemList = ItemList; 
            return View();
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
    }
}