using NewsWeb.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NewsWeb.Controllers
{
    public class ContactController : Controller
    {
        Model1 db = new Model1();

        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            string name = form["name"];
            string email = form["email"];
            string subject = form["subject"];
            string message = form["message"];
            Contacts contact = new Contacts
            {
                name = name,
                email = email,
                subject = subject,
                message = message
            };
            db.Contacts.Add(contact);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult ContactManager()
        {
            // Thực hiện truy vấn để lấy tất cả dữ liệu từ bảng Contacts
            List<Contacts> contacts = db.Contacts.ToList();

            // Gửi dữ liệu đến View thông qua ViewBag hoặc Model (tùy thuộc vào cách bạn muốn sử dụng)
            ViewBag.ContactList = contacts;

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}