using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeTale_Generator.Controllers
{
    public class TimeTableController : BaseController
    {
        struct Subject
        {
            public String Sub;
            public int? lab_hours;
            public int? theory_hours;
        };

        static String[,] arr = new String[6, 5];
        static bool[,] visited = new bool[6, 5];

        static void PrintTT()
        {
            Console.WriteLine();
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    //Console.Write("..  ");
                    if (arr[i, j] == null)
                    {
                        Console.Write("NA  ");
                    }
                    else
                    {
                        Console.Write(arr[i, j] + "  ");
                    }
                }
                Console.WriteLine();
            }
        }

        static void printVisited()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(visited[i, j]);
                }
                Console.WriteLine();
            }
        }



        // GET: TimeTable
        public ActionResult Index()
        {
            if (!IsValidUser()) return RedirectToAction("Login", "Security");
            TimeTable_GeneratorEntities timeTable_GeneratorEntities = new TimeTable_GeneratorEntities();
            
            return View(timeTable_GeneratorEntities.tbl_subject.ToList());
        }
        [HttpPost]
        public ActionResult Index(int Sem)
        {
            int total_subject = 8;
            int i, j;
            ArrayList a = new ArrayList();

            //for (i = 0; i < total_subject; i++)
            //{
            //    String sName = Console.ReadLine();
            //    int theory_hours = Convert.ToInt32(Console.ReadLine());
            //    int lab_hours = Convert.ToInt32(Console.ReadLine());
            //    Subject s;
            //    s.Sub = sName;
            //    s.lab_hours = lab_hours;
            //    s.theory_hours = theory_hours;
            //    a.Add(s);
            //}
            TimeTable_GeneratorEntities timeTable_GeneratorEntities = new TimeTable_GeneratorEntities();
            var myRec = timeTable_GeneratorEntities.tbl_subject.Where(x => x.SEMESTER == Sem).ToList();
            int total_hours = 0;
            foreach(var item in myRec)
            {
                Subject s;
                s.Sub = item.SUBJECT_NAME;
                if (item.THEORY_HOURS.Value != null)
                {
                    s.theory_hours = item.THEORY_HOURS.Value;
                    total_hours += item.THEORY_HOURS.Value;
                }
                else
                {
                    s.theory_hours = 0;
                }
                if (item.LAB_HOURS.Value != null)
                {
                    total_hours += item.LAB_HOURS.Value;
                    s.lab_hours = item.LAB_HOURS.Value;
                }
                else
                {
                    s.lab_hours = 0;
                }
                
                a.Add(s);
            }
            if (total_hours > 30)
            {
                ViewBag.ErrMessage = "Total hours > 30";
                return View();
            }
            else
            {


                //Subject s1,s2,s3,s4,s5,s6,s7,s8;
                //s1.Sub = "ML";
                //s1.theory_hours = 4;
                //s1.lab_hours = 3;
                //s2.Sub = ".NET";
                //s2.theory_hours = 0;
                //s2.lab_hours = 6;
                //s3.Sub = "WCMC";
                //s3.theory_hours = 3;
                //s3.lab_hours = 2;
                //s4.Sub = "SE";
                //s4.theory_hours = 3;
                //s4.lab_hours = 2;
                //s5.Sub = "DSP";
                //s5.theory_hours = 3;
                //s5.lab_hours = 2;
                //s6.Sub = "CA";
                //s6.theory_hours = 2;
                //s6.lab_hours = 0;
                //s7.Sub = "RE";
                //s7.theory_hours = 0;
                //s7.lab_hours = 2;
                //s8.Sub = "BB";
                //s8.theory_hours = 1;
                //s8.lab_hours = 0;

                //a.Add(s1);
                //a.Add(s2);
                //a.Add(s3);
                //a.Add(s4);
                //a.Add(s5);
                //a.Add(s6);
                //a.Add(s7);
                //a.Add(s8);

                //foreach (Subject it in a)
                //{
                //    Console.WriteLine(it.Sub + " " + it.theory_hours + " " + it.lab_hours);
                //}

                for (i = 0; i < 6; i++)
                {
                    for (j = 0; j < 5; j++)
                    {
                        visited[i, j] = false;
                    }
                }
                i = 5;
                j = 4;
                printVisited();
                foreach (Subject it in a)
                {
                    int? labhrs = it.lab_hours;
                    while (i >= 1 && j >= 0 && labhrs > 0)
                    {
                        if (!visited[i, j])
                        {
                            arr[i, j] = it.Sub + " Lab";
                            arr[i - 1, j] = it.Sub + " Lab";
                            //Console.Write(it.lab_hours);
                            labhrs -= 2;
                            visited[i, j] = true;
                            visited[i - 1, j] = true;
                            j--;
                            if (j < 0)
                            {
                                i -= 2;
                                j = 4;
                            }
                        }
                    }
                }

                i = 0;
                j = 0;
                foreach (Subject it in a)
                {
                    int? thryhrs = it.theory_hours;
                    while (i < 6 && j < 5 && thryhrs > 0)
                    {
                        if (!visited[i, j])
                        {
                            arr[i, j] = it.Sub;
                            thryhrs--;
                            visited[i, j] = true;
                            j++;
                            if (j > 4)
                            {
                                i++;
                                j = 0;
                            }
                        }
                        else
                        {
                            i++;
                            j = 0;
                        }
                    }
                }


                PrintTT();
                printVisited();
                //Console.ReadKey();


                
            }
            return RedirectToAction("Generated");
        }

        public ActionResult Generated()
        {
            if (!IsValidUser()) return RedirectToAction("Login", "Security");
            ViewBag.TT = arr;
            return View();
        }

        //public JsonResult GetDetails(string Sem)
        //{
        //    return Json(GetStudentDetails().Where(x => (x.SEMESTER).ToString() == Sem), JsonRequestBehavior.AllowGet);
        //}
        //private List<tbl_subject> GetStudentDetails()
        //{
        //    TimeTable_GeneratorEntities timeTable_GeneratorEntities = new TimeTable_GeneratorEntities();
        //    return timeTable_GeneratorEntities.tbl_subject.ToList();
        //}

        //private static tbl_subject PopulateModel(string sem)
        //{
        //    using (tbl_subject entities = new tbl_subject())
        //    {
        //        tbl_subject model = new tbl_subject()
        //        {

        //            SEMESTER = (from c in entities.SEMESTER
        //                        where c.SEMESTER.Equals(sem) || string.IsNullOrEmpty(sem)
        //                        select c).ToList()
        //        };
        //    }
        //} 


        // GET: TimeTable/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TimeTable/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TimeTable/Create
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

        // GET: TimeTable/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TimeTable/Edit/5
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

        // GET: TimeTable/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TimeTable/Delete/5
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


        public ActionResult Timetable()
        {
            return View();
        }

        //[HttpPost]
        //public  ActionResult Timetable(int Sem)
        //{
        //    TimeTable_GeneratorEntities timeTable_GeneratorEntities = new TimeTable_GeneratorEntities();
            
        //    var myrecs = timeTable_GeneratorEntities.tbl_subject.Where(x => int.Parse(x.SEMESTER) ==  Sem);
        //    //return View(myrecs);
        //    return RedirectToAction("Semester", myrecs);
        //}

        //public ActionResult Semester(IQueryable<tbl_subject> myRec)
        //{

        //    return View(myRec.ToList()); 
        //}
    }
}
