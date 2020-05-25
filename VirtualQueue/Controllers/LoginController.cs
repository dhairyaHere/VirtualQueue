using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualQueue.Models;

namespace VirtualQueue.Controllers
{
    
    public class LoginController : Controller
    {
        VQContext cont = new VQContext();
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["User"] == null)
                return View();
            else
                return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public ActionResult Index(Credential credentials)
        {
                //Debug.WriteLine(credentials.roleType, credentials.password);
                Credential cred = cont.Credentials.FirstOrDefault(x=>x.roleType==credentials.roleType && x.password==credentials.password);
                if(cred == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid credentials.Try again!");
                    return View(credentials);
                }
                else
                {
                    Session.Add("User", credentials.roleType);
                }

                return RedirectToAction("Index", "Home", null);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            //ViewBag.Clear();
            //TempData.Clear();
            return RedirectToAction("Index","Home" ,null);
        }
    }
}