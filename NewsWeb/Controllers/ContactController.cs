using NewsWeb.Models;
using System;
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
            return RedirectToAction("Contact", "Contact");
        }

        public ActionResult ContactManager(string pagestring)
        {
            int page = 0;
            if (pagestring == null)
            {
                page = 1;
            }
            else
            {
                page = int.Parse(pagestring);
            }

            int contain = 8;
            int pages = 0;
            // Thực hiện truy vấn để lấy tất cả dữ liệu từ bảng Contacts
            List<Contacts> contacts = new List<Contacts>();
            contacts = db.Contacts.ToArray().Reverse().ToList();
            if (contacts.Count == 0)
            {
                contain = 0;
                page = 0;
            }
            else if (contacts.Count % contain == 0)
            {
                pages = contacts.Count / contain;
            }
            else
            {
                pages = contacts.Count / contain + 1;
            }

            List<Contacts> selectedcontacts = contacts.GetRange(0, contain);

            // Gửi dữ liệu đến View thông qua ViewBag hoặc Model (tùy thuộc vào cách bạn muốn sử dụng)
            ViewBag.ContactList = selectedcontacts;
            ViewBag.pages = pages;
            ViewBag.page = page;

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Detail()
        {
            int id = int.Parse(Request["id"]);
            // Thực hiện truy vấn để lấy một Contact theo id
            Contacts contact = db.Contacts.Find(id);

            // Kiểm tra xem contact có tồn tại không
            if (contact == null)
            {
                // Xử lý khi không tìm thấy contact với id cụ thể
                return HttpNotFound();
            }

            // Gửi dữ liệu đến View thông qua ViewBag hoặc Model (tùy thuộc vào cách bạn muốn sử dụng)
            ViewBag.Contact = contact;

            return View();
        }
        [HttpPost]
        public JsonResult Delete(string id)
        {
            int id_new = int.Parse(id);
            try
            {
                // Thực hiện truy vấn để lấy Contact theo id
                Contacts contactToDelete = db.Contacts.Find(id_new);

                // Kiểm tra xem contact có tồn tại không
                if (contactToDelete == null)
                {
                    return Json(new { success = false, message = "Contact not found." });
                }

                // Xóa contact từ database
                db.Contacts.Remove(contactToDelete);
                db.SaveChanges();

                return Json(new { success = true, message = "Contact deleted successfully." });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return Json(new { success = false, message = "Error deleting contact.", error = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ContactManagerPage(string pagestring)
        {
            int page = 0;
            if (pagestring == null || pagestring == "0")
            {
                page = 1;
            }
            else
            {
                page = int.Parse(pagestring);
            }

            int contain = 8;
            int pages = 0;
            // Lấy sản phẩm từ index = 3 đến index = 5
            int startIndex = (page - 1) * contain;
            int count = contain; // Đếm từ startIndex
            // Thực hiện truy vấn để lấy tất cả dữ liệu từ bảng Contacts
            List<Contacts> contacts = db.Contacts.ToArray().Reverse().ToList();
            if (contacts.Count % contain == 0)
            {
                pages = contacts.Count / contain;
            }
            else
            {
                pages = contacts.Count / contain + 1;
            }
            if (contacts.Count - startIndex < contain)
            {
                count = contacts.Count - startIndex;
            }
            List<Contacts> seletedcontacts = contacts.GetRange(startIndex, count);

            return Json(new { list = seletedcontacts, current = page, total = pages });
        }

    }
}