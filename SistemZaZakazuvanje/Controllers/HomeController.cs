using SistemZaZakazuvanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using Microsoft.AspNet.Identity;
using Syncfusion.EJ2.Schedule;
using Microsoft.AspNetCore.Mvc;

namespace SistemZaZakazuvanje.Controllers
{
    public class HomeController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext(); 
        public System.Web.Mvc.ActionResult Index()
        {
            ViewBag.user=User.Identity.GetUserId();
            ViewBag.datasource = db.Appointments.ToList();
            ViewBag.uslugi = db.Uslugi.ToList();
            return View();
        }
        public System.Web.Mvc.ActionResult FileUpload(HttpPostedFileBase file)
        {
            Review review = new Review();
            if (file != null)
            {
                string ImageName = System.IO.Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath("~/Images/" + ImageName);
                file.SaveAs(physicalPath);
                review.ImageUrl = ImageName;
            }
            else
            {
                review.ImageUrl = "No image";
            }
                review.Description= Request.Form["Description"];
                review.UslugaId= Convert.ToInt32(Request.Form["UslugaId"]);
                review.Usluga = db.Uslugi.Find(review.UslugaId).Name;
                review.UserId = User.Identity.GetUserId();
                review.UserName = db.Users.Find(User.Identity.GetUserId()).firstName;
                db.Reviews.Add(review);
                db.SaveChanges();

            
            return RedirectToAction("Reviews","Home");
        }
        public System.Web.Mvc.ActionResult Reviews()
        {
            ViewBag.userId=User.Identity.GetUserId();
            return View(db.Reviews.ToList());
        }
        public System.Web.Mvc.ActionResult AddReview()
        {
            ViewBag.uslugi = db.Uslugi.ToList();
            return View();
        }
        public System.Web.Mvc.ActionResult DeleteReview(int id)
        {
            foreach (Review re in db.Reviews)
            {
                if (re.Id == id)
                {
                    db.Reviews.Remove(re);

                }
            }
            db.SaveChanges();
            return RedirectToAction("Reviews", "Home");
        }
        public System.Web.Mvc.ActionResult EditReview(int id)
        {
            ViewBag.uslugi = db.Uslugi.ToList();
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var re = db.Reviews.Single(a => a.Id == id);
            if (re == null)
            {
                return HttpNotFound();
            }
            return View(re);

        }
        [System.Web.Mvc.HttpPost]
        public System.Web.Mvc.ActionResult EditReview(HttpPostedFileBase file)
        {
            Review review = new Review();
            if(file == null)
            {
                    review.ImageUrl = Request.Form["ImageUrl"];  
                
            }
            else { 
            string ImageName = System.IO.Path.GetFileName(file.FileName);
            string physicalPath = Server.MapPath("~/Images/" + ImageName);
            file.SaveAs(physicalPath);
            review.ImageUrl = ImageName;
            }
            review.Id = Convert.ToInt32(Request.Form["Id"]);
            review.Description = Request.Form["Description"];
            review.UslugaId = Convert.ToInt32(Request.Form["UslugaId"]);
            review.Usluga = db.Uslugi.Find(review.UslugaId).Name;
            review.UserId = User.Identity.GetUserId();
            review.UserName = db.Users.Find(User.Identity.GetUserId()).firstName;
            
            
            var ap = db.Reviews.Find(Convert.ToInt32(Request.Form["Id"]));

            if (ModelState.IsValid)
            {
                db.Reviews.Remove(ap);
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Reviews","Home");
               

            }
            return View(review);

        }
        [System.Web.Mvc.HttpPost]
        public System.Web.Mvc.ActionResult Create(Appointment app)
        {
            List<Appointment> appointments = db.Appointments.ToList();
            Usluga us = null;
            foreach (var u in db.Uslugi.ToList())
            {
                if (u.Id == app.UslugaId)
                {
                    us = u;
                    app.Usluga = u.Name;
                }
            }
            app.EndTime = app.StartTime.AddHours(us.Duration);
            app.Status = "Pending";
            app.Subject = "Pending";
         
            app.UserId = User.Identity.GetUserId();
            foreach(var a in appointments)
            {
                if ((a.StartTime < app.StartTime && app.StartTime < a.EndTime) || (a.StartTime < app.EndTime && app.EndTime <= a.EndTime) || (app.StartTime < a.StartTime && app.EndTime > a.EndTime))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            db.Appointments.Add(app);
            db.SaveChanges();
            
            return RedirectToAction("Index", "Home");
        }
        
        public System.Web.Mvc.ActionResult Cancel(int id)
        {
            foreach (Appointment app in db.Appointments)
            {
                if (app.Id == id)
                {
                    db.Appointments.Remove(app);
                    
                }
            }db.SaveChanges();
            return RedirectToAction("List", "Home");
        }

        public System.Web.Mvc.ActionResult CancelApp(int id)
        {
            foreach (Appointment app in db.Appointments)
            {
                if (app.Id == id)
                {
                    db.Appointments.Remove(app);
                    Cancelled c = new Cancelled();
                    DeletedAppointment deletedAppointment = new DeletedAppointment();
                    deletedAppointment.StartTime = app.StartTime;
                    deletedAppointment.EndTime = app.EndTime;
                    deletedAppointment.Usluga = app.Usluga;
                    deletedAppointment.Status = "Cancelled";
                    deletedAppointment.UserId = app.UserId;
                    c.appointment = deletedAppointment;
                    c.appointment.Status = "Cancelled";
                    db.Cancels.Add(c);
                }
            }
            db.SaveChanges();
            
            return RedirectToAction("ListAll", "Home");
        }

        public System.Web.Mvc.ActionResult Approve(int id)
        {
            var appointments = db.Appointments.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var app = db.Appointments.Single(a => a.Id == id);
            if (app == null)
            {
                return HttpNotFound();
            }
            var ap = db.Appointments.Find(app.Id);
            ap.Status = "Confirmed";

            ap.color = "#00AD9E";
            db.SaveChanges();
            return RedirectToAction("ListAll","Home");
        }
        public System.Web.Mvc.ActionResult Edit(int id)
        {
            ViewBag.datasource = db.Appointments.ToList();
            ViewBag.uslugi = db.Uslugi.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var app = db.Appointments.Single(a => a.Id == id);
            if (app == null)
            {
                return HttpNotFound();
            }
            return View(app);

        }
        [System.Web.Mvc.HttpPost]
        public System.Web.Mvc.ActionResult Edit(Appointment app)
        {
            ViewBag.datasource = db.Appointments.ToList();
            List<Appointment> appointments = db.Appointments.ToList();
            ViewBag.uslugi = db.Uslugi.ToList();
            Usluga us=null;
            foreach (var u in db.Uslugi.ToList())
            {
                if (u.Id == app.UslugaId)
                {
                    us = u;
                    app.Usluga = u.Name;
                }
            }
            app.EndTime = app.StartTime.AddHours(us.Duration);
            app.Status = "Pending";
            app.Subject = "Pending";
            app.UserId = User.Identity.GetUserId();
            var ap = db.Appointments.Find(app.Id);
            int f = 0;
            foreach (var a in appointments)
            {
                if ((a.StartTime < app.StartTime && app.StartTime < a.EndTime) || (a.StartTime < app.EndTime && app.EndTime <= a.EndTime) || (app.StartTime < a.StartTime && app.EndTime > a.EndTime))
                {
                    if(ap.Id!=a.Id)
                    f = 1;                    
                }
            }
            if (ModelState.IsValid&&f==0)
            {
                db.Appointments.Remove(ap);
                db.Appointments.Add(app);
                db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(app);

        }
        public System.Web.Mvc.ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public System.Web.Mvc.ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public System.Web.Mvc.ActionResult List()
        {
            ListApp listApp = new ListApp();
            listApp.mine = new List<Appointment>();
            listApp.cancelled = new List<DeletedAppointment>();
            foreach (Appointment app in db.Appointments)
            {
                if (app.UserId == User.Identity.GetUserId())
                {
                    listApp.mine.Add(app);  
                   
                }
            }
            foreach(Cancelled can in db.Cancels)
            {
                if(can.appointment.UserId == User.Identity.GetUserId())
                {
                    DeletedAppointment deletedAppointment = new DeletedAppointment();
                    deletedAppointment.StartTime = can.appointment.StartTime;
                    deletedAppointment.EndTime = can.appointment.EndTime;
                    deletedAppointment.Usluga = can.appointment.Usluga;
                    deletedAppointment.Status = "Cancelled";
                    deletedAppointment.UserId = can.appointment.UserId;
                    listApp.cancelled.Add(deletedAppointment);
                }
            }
            return View(listApp);
        }
        public System.Web.Mvc.ActionResult ListAll()
        {
            
            return View(db.Appointments.ToList());
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public System.Web.Mvc.ActionResult LoadData()  // Here we get the Start and End Date and based on that can filter the data and return to Scheduler
        {
            var data = db.Appointments.ToList();
            return Json(data);
        }
        [System.Web.Mvc.HttpPost]
        public System.Web.Mvc.ActionResult UpdateData([FromBody] EditParams param)
        {
            if (param.action == "insert" || (param.action == "batch" && param.added != null)) // this block of code will execute while inserting the appointments
            {
                var value = (param.action == "insert") ? param.value : param.added[0];
                int intMax = db.Appointments.ToList().Count > 0 ? db.Appointments.ToList().Max(p => p.Id) : 1;
                DateTime startTime = Convert.ToDateTime(value.StartTime);
                DateTime endTime = Convert.ToDateTime(value.EndTime);
                Appointment appointment = new Appointment()
                {
                    Id = intMax + 1,
                    StartTime = startTime,
                    EndTime = endTime,
                    Subject = db.Users.Find(User.Identity.GetUserId()).firstName,
                    UslugaId = value.UslugaId,
                    UserId = User.Identity.GetUserId(),
                    Status="Pending",
                    color= "#FFA17A"

                };
                ViewBag.uslugi = db.Uslugi.ToList();
                Usluga us = null;
                foreach (var u in db.Uslugi.ToList())
                {
                    if (u.Id == appointment.UslugaId)
                    {
                        us = u;
                        appointment.Usluga = u.Name;
                    }
                }
                appointment.EndTime= appointment.StartTime.AddHours(us.Duration);
                db.Appointments.Add(appointment);
                db.SaveChanges();
            }
            if (param.action == "update" || (param.action == "batch" && param.changed != null)) // this block of code will execute while updating the appointment
            {
                var value = (param.action == "update") ? param.value : param.changed[0];
                var filterData = db.Appointments.Where(c => c.Id == Convert.ToInt32(value.Id));

                if (filterData != null)
                {
                    DateTime startTime = Convert.ToDateTime(value.StartTime);
                    DateTime endTime = Convert.ToDateTime(value.EndTime);
                    Appointment appointment = db.Appointments.Single(A => A.Id == value.Id);
                    appointment.StartTime = startTime;
                    appointment.Subject = "Name";
                    appointment.Usluga = value.Usluga;
                    appointment.UslugaId = value.UslugaId;
                    appointment.UserId= User.Identity.GetUserId();
                    appointment.color = value.color;
                    appointment.EndTime = appointment.StartTime.AddHours(db.Uslugi.Find(appointment.UslugaId).Duration);
                }
                
                db.SaveChanges();
            }
            if (param.action == "remove" || (param.action == "batch" && param.deleted != null)) // this block of code will execute while removing the appointment
            {
                if (param.action == "remove")
                {
                    int key = Convert.ToInt32(param.key);
                    Appointment appointment = db.Appointments.Where(c => c.Id == key).FirstOrDefault();
                    if (appointment != null) db.Appointments.Remove(appointment);
                }
                else
                {
                    foreach (var apps in param.deleted)
                    {
                        Appointment appointment = db.Appointments.Where(c => c.Id == apps.Id).FirstOrDefault();
                        if (appointment != null) db.Appointments.Remove(appointment);
                    }
                }
                db.SaveChanges();
            }
            var data = db.Appointments.ToList();
            return Json(data);
        }
        public System.Web.Mvc.ActionResult ListComments(int id)
        {
            var comments=new List<Comment>();
            foreach(var com in db.Comments)
            {
                if(com.IdReview==id) 
                    comments.Add(com);
            }
            ViewBag.comments = comments;
            var rev = db.Reviews.Find(id);
            if(rev.Comments==null)
            rev.Comments = new LinkedList<Comment>();
            if (rev != null)
                return View(rev);
            else
                return HttpNotFound();
        }
        public System.Web.Mvc.ActionResult AddComment(int id)
        {
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.id = id;
            Comment model = new Comment();
            return View(model);
        }
        [System.Web.Mvc.HttpPost]
        public System.Web.Mvc.ActionResult AddComment(Comment model)
        {
            var rev = db.Reviews.Find(model.IdReview);
            model.UserName= db.Users.Find(User.Identity.GetUserId()).firstName;
            if (rev.Comments == null)
                rev.Comments = new LinkedList<Comment>();
            db.Comments.Add(model);
            rev.Comments.AddFirst(model);
            db.SaveChanges();
            return RedirectToAction("ListComments",new { id = model.IdReview });
        }
        public class EditParams
        {
            public string key { get; set; }
            public string action { get; set; }
            public List<Appointment> added { get; set; }
            public List<Appointment> changed { get; set; }
            public List<Appointment> deleted { get; set; }
            public Appointment value { get; set; }
        }
    }
}
