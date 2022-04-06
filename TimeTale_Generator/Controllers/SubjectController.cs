using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace TimeTale_Generator.Controllers
{
    public class SubjectController : BaseController
    {
        // GET: Subject
        public ActionResult Index()
        {
            if (!IsValidUser()) return RedirectToAction("Login", "Security");
            TimeTable_GeneratorEntities timeTable_GeneratorEntities = new TimeTable_GeneratorEntities();
            

            return View(timeTable_GeneratorEntities.tbl_subject.ToList());
        }

        // GET: Subject/Details/5
        public ActionResult Details(int id)
        {
            if (!IsValidUser()) return RedirectToAction("Login", "Security");
            TimeTable_GeneratorEntities timeTable_GeneratorEntities = new TimeTable_GeneratorEntities();
            var myRec = timeTable_GeneratorEntities.tbl_subject.FirstOrDefault(x => x.ID == id);
            return View(myRec);
        }

        // GET: Subject/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subject/Create
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

        // GET: Subject/Edit/5
        public ActionResult Edit(int id)
        {
            if (!IsValidUser()) return RedirectToAction("Login", "Security");
            TimeTable_GeneratorEntities timeTable_GeneratorEntities = new TimeTable_GeneratorEntities();
            var myRec = timeTable_GeneratorEntities.tbl_subject.FirstOrDefault(x => x.ID == id);
            using (TimeTable_GeneratorEntities timeTable_GeneratorEntities2 = new TimeTable_GeneratorEntities())
            {
                //var facultyName_formDB = new SelectList(timeTable_GeneratorEntities.tbl_faculty.ToList(), "ID", "NAME");
                var facultyName_formDB = new SelectList(timeTable_GeneratorEntities2.tbl_faculty.ToList(), "NAME", "NAME");
                ViewData["Faculty_Name"] = facultyName_formDB;

                var lab_formDB = new SelectList(timeTable_GeneratorEntities2.tbl_subject.ToList(), "LAB_LOCATION", "LAB_LOCATION");
                ViewData["Lab_Location"] = lab_formDB;
            }
            return View(myRec);
        }

        // POST: Subject/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tbl_subject tbl_Subject)
        {
            try
            {
                // TODO: Add update logic here
                TimeTable_GeneratorEntities timeTable_GeneratorEntities = new TimeTable_GeneratorEntities();
                var myRec = timeTable_GeneratorEntities.tbl_subject.FirstOrDefault(x => x.ID == id);
                myRec.SEMESTER = int.Parse(tbl_Subject.SEMESTER.ToString());
                myRec.SUBJECT_CODE = tbl_Subject.SUBJECT_CODE.ToString();
                myRec.SUBJECT_CREDIT = int.Parse(tbl_Subject.SUBJECT_CREDIT.ToString());
                myRec.SUBJECT_NAME = tbl_Subject.SUBJECT_NAME.ToString();
                myRec.COURSE_TYPE = tbl_Subject.COURSE_TYPE.ToString();
                myRec.THEORY_FACULTY = tbl_Subject.THEORY_FACULTY.ToString();
                myRec.THEORY_CLASS_LOCATION = tbl_Subject.THEORY_CLASS_LOCATION.ToString();
                myRec.LAB_FACULTY = tbl_Subject.LAB_FACULTY.ToString();
                myRec.LAB_LOCATION = tbl_Subject.LAB_LOCATION.ToString();
                myRec.THEORY_HOURS = int.Parse(tbl_Subject.THEORY_HOURS.ToString());
                myRec.LAB_HOURS = int.Parse(tbl_Subject.LAB_HOURS.ToString());
                if(myRec != null)
                {
                    timeTable_GeneratorEntities.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.errorMsg = "Invalid Data Entered";
                return View();
            }
        }

        // GET: Subject/Delete/5
        public ActionResult Delete(int id)
        {
            if (!IsValidUser()) return RedirectToAction("Login", "Security");
            TimeTable_GeneratorEntities timeTable_GeneratorEntities = new TimeTable_GeneratorEntities();
            var myRec = timeTable_GeneratorEntities.tbl_subject.FirstOrDefault(x => x.ID == id);
            return View(myRec);
            
        }

        // POST: Subject/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                TimeTable_GeneratorEntities timeTable_GeneratorEntities = new TimeTable_GeneratorEntities();
                var myRec = timeTable_GeneratorEntities.tbl_subject.FirstOrDefault(x => x.ID == id);
                if(myRec != null)
                {
                    timeTable_GeneratorEntities.tbl_subject.Remove(myRec);
                    timeTable_GeneratorEntities.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult AddSubject()
        {
            if (!IsValidUser()) return RedirectToAction("Login", "Security");
            //TimeTable_GeneratorEntities timeTable_GeneratorEntities = new TimeTable_GeneratorEntities();
            //List<SelectListItem> items = new List<SelectListItem>();
            //foreach (var element in timeTable_GeneratorEntities.tbl_faculty)
            //{
            //    items.Add(new SelectListItem
            //    { Text = element.NAME.ToString(), Value = element.NAME.ToString() });

            //}
            //ViewBag.CategoryType = items;
            using (TimeTable_GeneratorEntities timeTable_GeneratorEntities = new TimeTable_GeneratorEntities())
            {
                //var facultyName_formDB = new SelectList(timeTable_GeneratorEntities.tbl_faculty.ToList(), "ID", "NAME");
                var facultyName_formDB = new SelectList(timeTable_GeneratorEntities.tbl_faculty.ToList(), "NAME", "NAME");
                ViewData["Faculty_Name"] = facultyName_formDB;

                var lab_formDB = new SelectList(timeTable_GeneratorEntities.tbl_lab.ToList(), "LOCATION", "LOCATION");
                ViewData["Lab_Location"] = lab_formDB;
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddSubject(tbl_subject tbl_Subject)
        {
            
           
                try
                {
                    
                    TimeTable_GeneratorEntities timeTable_GeneratorEntities = new TimeTable_GeneratorEntities();
                    //tbl_subject tbl_Subject = new tbl_subject();
                    //tbl_Subject.SEMESTER = int.Parse(collection["SEMESTER"]);
                    //tbl_Subject.SUBJECT_CODE = collection["SUBJECT_CODE"];
                    //tbl_Subject.SUBJECT_CREDIT = int.Parse(collection["SUBJECT_CREDIT"]);
                    //tbl_Subject.SUBJECT_NAME = collection["SUBJECT_NAME"];
                    //tbl_Subject.COURSE_TYPE = collection["COURSE_TYPE"];
                    
                    
                    timeTable_GeneratorEntities.tbl_subject.Add(tbl_Subject);
                    timeTable_GeneratorEntities.SaveChanges();


                }
                catch (Exception e)
                {
                    // To be continue
                    return RedirectToAction("AddSubject");

                }
            
            return RedirectToAction("Index", "Home");
        }

        //public ActionResult FacultyDDList()
        //{
        //    TimeTable_GeneratorEntities timeTable_GeneratorEntities = new TimeTable_GeneratorEntities();
        //    var items = timeTable_GeneratorEntities.tbl_faculty.ToList();
        //    if(items != null)
        //    {
        //        ViewBag.data = items;
        //    }
        //    return View();
        //}
    }
}
