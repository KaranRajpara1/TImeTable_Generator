using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeTale_Generator.Controllers
{
    public class ClassController : BaseController
    {
        // GET: Class
        public ActionResult Index()
        {
            if(!IsValidUser()) return RedirectToAction("Login", "Security");
            
            TimeTable_GeneratorEntities timeTable_GeneratorEntities = new TimeTable_GeneratorEntities();
            return View(timeTable_GeneratorEntities.tbl_lab.ToList());
        }

        // GET: Class/Details/5
        public ActionResult Details(int id)
        {
            if (!IsValidUser()) return RedirectToAction("Login", "Security");
            TimeTable_GeneratorEntities timeTable_GeneratorEntities = new TimeTable_GeneratorEntities();
            var myRec = timeTable_GeneratorEntities.tbl_lab.FirstOrDefault(x => x.ID == id);
            return View(myRec);
        }

        // GET: Class/Create
        public ActionResult Create()
        {
            if (!IsValidUser()) return RedirectToAction("Login", "Security");
            return View();
        }

        // POST: Class/Create
        [HttpPost]
        public ActionResult Create(tbl_lab tbl_Lab)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    TimeTable_GeneratorEntities timeTable_GeneratorEntities = new TimeTable_GeneratorEntities();
                    //tbl_lab tbl_Lab = new tbl_lab();
                    //tbl_Lab.LOCATION = collection["LOCATION"];
                    //tbl_Lab.LAB_TYPE = collection["LAB_TYPE"];
                    //tbl_Lab.ADDITIONAL_DETAILS = collection["ADDITIONAL_DETAILS"];
                    //tbl_Lab.ISLAB = Boolean.Parse(collection["ISLAB"]);
                    timeTable_GeneratorEntities.tbl_lab.Add(tbl_Lab);
                    timeTable_GeneratorEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Class/Edit/5
        public ActionResult Edit(int id)
        {
            if (!IsValidUser()) return RedirectToAction("Login", "Security");
            TimeTable_GeneratorEntities timeTable_GeneratorEntities = new TimeTable_GeneratorEntities();
            var myRec = timeTable_GeneratorEntities.tbl_lab.FirstOrDefault(x => x.ID == id);
            return View(myRec);
        }

        // POST: Class/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tbl_lab tbl_Lab)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here
                    TimeTable_GeneratorEntities timeTable_GeneratorEntities = new TimeTable_GeneratorEntities();
                    var myRec = timeTable_GeneratorEntities.tbl_lab.FirstOrDefault(x=>x.ID == id); 
                    
                    if(myRec != null)
                    {
                        myRec.ISLAB = tbl_Lab.ISLAB;
                        myRec.LAB_TYPE = tbl_Lab.LAB_TYPE;
                        myRec.LOCATION = tbl_Lab.LOCATION;
                        myRec.ADDITIONAL_DETAILS = tbl_Lab.ADDITIONAL_DETAILS;
                        timeTable_GeneratorEntities.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Class/Delete/5
        public ActionResult Delete(int id)
        {
            if (!IsValidUser()) return RedirectToAction("Login", "Security");
            TimeTable_GeneratorEntities timeTable_GeneratorEntities = new TimeTable_GeneratorEntities();
            var myRec = timeTable_GeneratorEntities.tbl_lab.FirstOrDefault(x => x.ID == id);
            return View(myRec);
        }

        // POST: Class/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                TimeTable_GeneratorEntities timeTable_GeneratorEntities = new TimeTable_GeneratorEntities();
                var myRec = timeTable_GeneratorEntities.tbl_lab.FirstOrDefault(x => x.ID == id);
                if(myRec != null)
                {
                    timeTable_GeneratorEntities.tbl_lab.Remove(myRec);
                    timeTable_GeneratorEntities.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
