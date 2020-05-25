using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualQueue.Models;

namespace VirtualQueue.Controllers
{
    public class DisplayController : Controller
    {
        VQContext db = new VQContext();
        // GET: Display
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            List<List<Guest>> l = new List<List<Guest>>();
           Guest g1 = db.Guests.Where(c => c.status =="Pending")
                                .OrderByDescending(t => t.waiting)
                                .FirstOrDefault();


            l.Add (new List<Guest>{g1});
            l.Add(db.Guests.Where(x => x.status == "Waiting").ToList());
            
            return Json(l, JsonRequestBehavior.AllowGet);
        }
    }
}