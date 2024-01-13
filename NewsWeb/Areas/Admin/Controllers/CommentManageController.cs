using NewsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsWeb.Areas.Admin.Controllers
{
    public class CommentManageController : Controller
    {
        Model1 db = new Model1();
        // GET: Admin/AccoutManager
        public ActionResult Comment()
        {
            // var loggedInUser = Session["LoggedInUser"] as NewsWeb.Models.Users;
            //if (loggedInUser != null && loggedInUser.permission == "admin")
            // {
            List<Comment> comments = db.Comments.ToList();
            ViewBag.ItemList = comments;
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
                Comment comment = db.Comments.FirstOrDefault(c => c.id == id);
                if (comment != null)
                {
                    return View(comment);
                }
            }
            catch (Exception e)
            {

            }
            return View();
        }
        public ActionResult Update(Comment comment)
        {

            if (ModelState.IsValid)
            {
                Comment commentUpdate = db.Comments.FirstOrDefault(c => c.id == comment.id);
                if (commentUpdate != null)
                {
                    commentUpdate.allow = comment.allow;
                    db.SaveChanges();
                }
            }

            return Redirect("/Admin/CommentManage/Detail?id=" + comment.id);
        }
        public ActionResult Delete()
        {
            int id = int.Parse(Request["id"]);
            var comment = db.Comments.FirstOrDefault(u => u.id == id);
            if (comment != null)
            {
                db.Comments.Remove(comment);
                db.SaveChanges();
            }

            return Redirect("/Admin/CommentManage/Comment");
        }
    }
}