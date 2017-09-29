using DataAccess;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleChatApp.Controllers
{
    public class ChatController : Controller
    {

        DataContext db = new DataContext();
        Repository _rep = new Repository();

        public ActionResult StartPage()
        {
            if (TempData["message"] != null)
            {
                ViewBag.Message = TempData["message"].ToString();
            }
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(User usr)
        {
            User dbUser = db.Users.Where(u => u.Login == usr.Login).FirstOrDefault();
           
            if(dbUser != null)
            {
                if(dbUser.Password == usr.Password)
                {
                    Session["user_id"] = dbUser.UserId;
                    Session["login"] = dbUser.Login;
                    return RedirectToAction("Chat");
                }
                else
                {
                    ModelState.AddModelError("", "Пароль введен неверно");
                    return View(usr);
                }
            }
            else
            {
                ModelState.AddModelError("", "Данный пользователь еще не зарегистрирован");
                return View(usr);
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User usr)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Where(u => u.Login == usr.Login).FirstOrDefault() == null)
                {
                    _rep.insertUser(usr);
                    TempData["message"] = "Пользователь " + usr.Login + " успешно зарегистрирован!";
                    return RedirectToAction("StartPage");
                }
                else
                {
                    ModelState.AddModelError("", "Данный пользователь уже существует!");
                    return View(usr);
                }
            }
            else
            {
                return View(usr);
            }
        }

        public ActionResult Chat()
        {
            return View(_rep.getAllMessages());
        }
        
        [HttpPost]
        public ActionResult Chat(string message)
        {
            User user = _rep.getUserById((int)Session["user_id"]);
            Message msg = new Message()
            {
                MessageText = message,
                MessageTime = DateTime.Now,
                UserId = (int)Session["user_id"],
                User = user
            };

            _rep.insertMessage(msg);

            return PartialView("Message", msg);
        }

        public ActionResult LogOff()
        {
            Session.Clear();

            return RedirectToAction("StartPage");
        }

    }
}