using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TimeTale_Generator.Controllers
{
    public class SecurityController : BaseController
    {
        // GET: Security
        public ActionResult Index()
        {
            return View();
        }

        // GET: Security/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Security/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Security/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Security/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Security/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Security/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Security/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SignUp()
        {

            return View();
        }
        [HttpPost]
        public ActionResult SignUp(tbl_faculty tbl_Faculty)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    // TODO: Add insert logic here
                    TimeTable_GeneratorEntities timeTable_GeneratorEntities = new TimeTable_GeneratorEntities();
                    //tbl_faculty tbl_Faculty = new tbl_faculty();
                    //tbl_Faculty.NAME = collection["NAME"];
                    //tbl_Faculty.EMAIL = collection["EMAIL"];
                    //tbl_Faculty.PASSWORD = collection["PASSWORD"];
                    //tbl_Faculty.MOBILE_NO = collection["MOBILE_NO"];
                    //tbl_Faculty.DESIGNATION = collection["DESIGNATION"];
                    timeTable_GeneratorEntities.tbl_faculty.Add(tbl_Faculty);
                    timeTable_GeneratorEntities.SaveChanges();
                    //return RedirectToAction("Index");
                    return RedirectToAction("Index", "Home");

                }
                catch (Exception E)
                {
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
            
        }

        public ActionResult Login()
        {
            return View();
        }

        

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            try
            {
                TimeTable_GeneratorEntities timeTable_GeneratorEntities = new TimeTable_GeneratorEntities();
                var email = collection["EMAIL"];
                var password = collection["PASSWORD"];
                var user = timeTable_GeneratorEntities.tbl_faculty.FirstOrDefault(x => x.EMAIL == email && x.PASSWORD == password);
                if (user == null)
                {
                    return RedirectToAction("SignUp");
                }
                else
                {
                    Session["ID"] = user.ID;
                    Session["Email"] = user.EMAIL;
                    return RedirectToAction("Index", "Home");
                }
            }
            catch(Exception e)
            {

            }
            
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session["ID"] = null;
            return RedirectToAction("Login");
        }
    }
}
