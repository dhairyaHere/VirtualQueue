using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualQueue.Models;

namespace VirtualQueue.Controllers
{
    public class AdminController : Controller
    {
        VQContext db = new VQContext();
        [HttpGet]
        // GET: Admin
        public ActionResult Search()
        {
            if(Session["User"]!=null)
            {
                if (Session["User"].ToString() == "Admin")
                    return View();
                else
                {
                    ViewBag.errmsg = "You do not have permissions to access this page.";
                    return View("Error");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
        }

        [HttpGet]
        public ActionResult Event()
        {
            if(Session["User"]!=null)
            {
                if(Session["User"].ToString()=="Admin")
                {
                    return View();
                }
                else
                {
                    ViewBag.errmsg = "You do not have permission to access this page.";
                    return View("Error");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Event(string event_name)
        {
            if (Session["User"] != null)
            {
                if (Session["User"].ToString() == "Admin")
                {
                    if(event_name!="")
                    {
                        HttpContext.Application["EventName"] = event_name;
                        ProjectConfig c=db.ProjectConfigs.FirstOrDefault(x => x.att_key == "EventName");
                        c.att_val = event_name;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Event Name can not be blank.");
                        return View();
                    }
                }
                else
                {
                    ViewBag.errmsg = "You do not have permission to access this page.";
                    return View("Error");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Dashboard()
        {
            if (Session["User"] != null)
            {
                if (Session["User"].ToString() == "Admin")
                {
                    return View();
                }
                else
                {
                    ViewBag.errmsg = "You do not have permission to access this page.";
                    return View("Error");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult EditCred()
        {
            if (Session["User"] != null)
            {
                if (Session["User"].ToString() == "Admin")
                {
                    
                    return View();
                }
                else
                {
                    ViewBag.errmsg = "You do not have permission to access this page.";
                    return View("Error");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult EditCred(string sidtb, string secretkeytb, string mobiletb, string sendgridtb)
        {
            if (Session["User"] != null)
            {
                if (Session["User"].ToString() == "Admin")
                {
                    if( sidtb=="" || secretkeytb=="" ||  mobiletb==""  || sendgridtb=="")
                    {

                        ModelState.AddModelError(string.Empty,"All the fields are mandatory!");
                        return View();
                    }
                    else
                    {
                        if (mobiletb.Length < 12)
                        {
                            ModelState.AddModelError(string.Empty, "Enter valid mobile number along with the country code!");
                            return View();
                        }
                        HttpContext.Application["SID"] = sidtb;
                        HttpContext.Application["SecretKey"] = secretkeytb;
                        HttpContext.Application["MobileNo"] = mobiletb;
                        HttpContext.Application["SendGridAPIKey"] = sendgridtb;
                        HttpContext.Application["MessageCount"] = "0";

                        ProjectConfig sid, secretkey, mobile,sendgrid,messagecount;
                        sid = db.ProjectConfigs.FirstOrDefault(x=>x.att_key=="SID");
                        secretkey = db.ProjectConfigs.FirstOrDefault(x => x.att_key == "SecretKey");
                        mobile = db.ProjectConfigs.FirstOrDefault(x => x.att_key == "MobileNo");
                        sendgrid = db.ProjectConfigs.FirstOrDefault(x => x.att_key == "SendGridAPIKey");
                        messagecount = db.ProjectConfigs.FirstOrDefault(x => x.att_key == "MessageCount");

                        sid.att_val = sidtb;
                        secretkey.att_val = secretkeytb;
                        mobile.att_val = mobiletb;
                        sendgrid.att_val = sendgridtb;
                        messagecount.att_val = "0";

                        db.SaveChanges();
                        return RedirectToAction("Dashboard");
                    }
                    
                }
                else
                {
                    ViewBag.errmsg = "You do not have permission to access this page.";
                    return View("Error");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Export()
        {
            if (Session["User"] != null)
            {
                if (Session["User"].ToString() == "Admin")
                {
                    List<Guest> l=db.Guests.Where(x=>x.status=="Arrived").ToList();
                    return View(l);

                }
                else
                {
                    ViewBag.errmsg = "You do not have permission to access this page.";
                    return View("Error");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Download()
        {
            if (Session["User"] != null)
            {
                if (Session["User"].ToString() == "Admin")
                {
                    List<Guest> l = db.Guests.Where(x=>x.status=="Arrived").ToList();
                    if (l.Count > 0 && l != null)
                    {
                        StringWriter sw = new StringWriter();
                        sw.WriteLine("\"BookingID\",\"Guest Name\",\"Email\",\"Contact No\",\"Group Size\"" +
                            ",\"VIP\",\"Status\",\"Mailing List\",\"Waiting Area Time\",\"Pending Area Time\"");
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", "attachment;filename=GuestList_CSV.csv");
                        Response.ContentType = "text/csv";
                        foreach (Guest g in l)
                        {
                            sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"" +
                            ",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\"",
                            g.bookingID, g.guestName, g.email, g.contact_no, g.groupSize, g.isVIP, g.status,
                            g.persist,
                            g.waiting.Subtract(g.entry).ToString(), g.pending.Subtract(g.waiting).ToString()
                            )
                                );
                        }
                        Response.Write(sw.ToString());
                        Response.End();
                        return RedirectToAction("Export");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "No data to export!");
                        return View("Export",l);
                    }
                }
                else
                {
                    ViewBag.errmsg = "You do not have permission to access this page.";
                    return View("Error");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }


        [HttpPost]
        public ActionResult Search(string searchtb,string Search)
        {
            
            List<Guest> l = new List<Guest>();
            if (Session["User"] != null)
            {
                if (Session["User"].ToString() == "Admin")
                {
                    if(searchtb=="" || Search=="")
                    {
                        ModelState.AddModelError(string.Empty, "Please enter valid details.");
                        return View();
                    }
                    if (Search.Equals("bcon") == true)
                    {
                        l=db.Guests.Where(x => x.contact_no == searchtb).ToList();
                        if (l.Count == 0)
                        {
                            ModelState.AddModelError(string.Empty, "No such guest with this Contact Number found.");
                            return View();
                        }
                    }
                    else if(Search.Equals("bname")==true)
                    {
                        l = db.Guests.Where(x => x.guestName == searchtb).ToList();
                        if (l.Count == 0)
                        {
                            ModelState.AddModelError(string.Empty, "No such guest with this name found.");
                            return View();
                        }
                    }else if (Search.Equals("bid") == true)
                    {
                        long id=0;
                        
                        if (long.TryParse(searchtb,out id)==false)
                        {
                            id = -1;
                        }
                        else
                        {
                            id = Convert.ToInt64(searchtb);
                        }
                        l = db.Guests.Where(x => x.bookingID == id).ToList();
                        if (l.Count == 0)
                        {
                            ModelState.AddModelError(string.Empty, "No such guest with this Booking ID found.");
                            return View();
                        }
                    }else if (Search.Equals("bmail") == true)
                    {
                        l = db.Guests.Where(x => x.email == searchtb).ToList();
                        if (l.Count == 0)
                        {
                            ModelState.AddModelError(string.Empty, "No such guest with this email found.");
                            return View();
                        }
                    }
                    return View("SearchResults", l);
                }
                else
                {
                    ViewBag.errmsg = "You do not have permissions to access this page.";
                    return View("Error");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

            
        }
    }
}