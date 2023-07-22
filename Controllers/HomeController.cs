using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_multipleClass.Models;
using System.Diagnostics;

namespace MVC_multipleClass.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        bool ascendingOrder = true;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            bool ascendingOrder = true;
            using Context myContext = new Context();

            var query = from subject in myContext.Subjects
                        join student in myContext.Students on subject.ID equals student.SId
                        join teacher in myContext.Teachers on subject.ID equals teacher.SId
                        group new { subject, student, teacher } by student.Name into subjectGroup
                        select new
                        {
                            SubjectName = subjectGroup.Select(g => g.subject.SubjectName).Distinct(),

                            Students = subjectGroup.Key,
                            Teachers = subjectGroup.Select(g => g.teacher.Name).Distinct()
                        };
            var result = query.ToList();
            var formattedResult = result.Select(r => new
            {
                Students = r.Students,
                SubjectName = string.Join(", ", r.SubjectName),
                Teachers = string.Join(", ", r.Teachers)
            }).ToList();

            //var orderedList = formattedResult.OrderBy(r => r.SubjectName).ThenBy(r => r.SubjectName).ThenBy(r => r.Teachers).ToList();


            if (ascendingOrder)
            {
                var orderedList = formattedResult.OrderBy(r => r.Students).ThenBy(r => r.SubjectName).ThenBy(r => r.Teachers).ToList();

                ascendingOrder = false;
                return View(orderedList);
            }
            else
            {
                var orderedList = formattedResult.OrderByDescending(r => r.Students).ThenByDescending(r => r.SubjectName).ThenBy(r => r.Teachers).ToList();

                ascendingOrder = true;
                return View(orderedList);
            }

        }

        public IActionResult Subject()
        {
            Context myContext = new();
            var subjects=myContext.Subjects.ToList();
            return View(subjects);
        }
        [HttpGet]
        public IActionResult AddSubjects()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSubjects(Subject sub)
        {
            Context myContext =new();
            myContext.Subjects.Add(sub);
            myContext.SaveChanges();
            return RedirectToAction("Subject");
        }
        public IActionResult Students()
        {
            Context myContext = new();
            var students = myContext.Students.ToList();
            return View(students);
        }
        [HttpGet]
        public IActionResult AddStudents()
        {
            Context myContext = new();
            var subjects=myContext.Subjects.Select(c=>c.ID).ToList();

            // Add the SelectList to the ViewBag to make it available in the view
            ViewBag.SId = new SelectList(subjects);
            return View();
        }
        [HttpPost]
        public IActionResult AddStudents(Student data)
        {
            Context myContext = new();
            myContext.Students.Add(new Student()
            {
                Name = data.Name,
                SId = data.SId,
            });
            myContext.SaveChanges();
            return RedirectToAction("Students");
        }
        [HttpGet]
        public IActionResult EditStudents(int id)
        {
            Context myContext = new();
            var subjects = myContext.Subjects.ToList();
            ViewBag.SubjectsList = new SelectList(subjects, "ID", "SubjectName");
            var data=myContext.Students.Where(c => c.Id == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public IActionResult EditStudents(Student data)
        {
            Context myContex = new Context();
            var d1 = myContex.Students.Where(c => c.Id == data.Id).FirstOrDefault();
            d1.Id = data.Id;
            d1.Name = data.Name;
            d1.SId=data.SId;
            myContex.SaveChanges();
            return RedirectToAction("Students");
        }
        [HttpGet]
        public IActionResult DeleteStudents(int id)
        {
            Context myContex = new Context();
            var d = myContex.Students.Where(c => c.Id == id).FirstOrDefault();
            myContex.Students.Remove(d);
            myContex.SaveChanges();
            return RedirectToAction("Students");
        }
        public IActionResult Teachers()
        {
            Context myContext = new();
            var teachers = from subject in myContext.Subjects
                           join teacher in myContext.Teachers on subject.ID equals teacher.SId
                           select new
                           {
                               Name = teacher.Name,
                               Subject = subject.SubjectName,
                               Address = teacher.Address,
                           };
            return View(teachers);
        }

        [HttpGet]
        public IActionResult AddTeachers()
        {
            Context myContext = new();
            var subjects = myContext.Subjects.ToList();
            ViewBag.SubjectsList = new SelectList(subjects, "ID", "SubjectName");
            // Add the SelectList to the ViewBag to make it available in the view
            //ViewBag.SId = new SelectList(subjects);


            myContext.SaveChanges();
            return View();
        }
        [HttpPost]
        public IActionResult AddTeachers(Teacher data)
        {
            Context myContext = new();
            myContext.Teachers.Add(new Teacher()
            {
                Name = data.Name,
                Address = data.Address,
                SId = data.SId,
            });
            myContext.SaveChanges();
            return RedirectToAction("Teachers");
        }
        public IActionResult OrderByStuSubTec()
        {
           
            using Context myContext = new Context();

            var query = from subject in myContext.Subjects
                        join student in myContext.Students on subject.ID equals student.SId
                        join teacher in myContext.Teachers on subject.ID equals teacher.SId
                        group new { subject, student, teacher } by student.Name into subjectGroup
                        select new
                        {
                            SubjectName = subjectGroup.Select(g => g.subject.SubjectName).Distinct(),

                            Students = subjectGroup.Key,
                            Teachers = subjectGroup.Select(g => g.teacher.Name).Distinct()
                        };
            var result = query.ToList();
            var formattedResult = result.Select(r => new
            {
                Students = r.Students,
                SubjectName = string.Join(", ", r.SubjectName),
                Teachers = string.Join(", ", r.Teachers)
            }).ToList();

            //var orderedList = formattedResult.OrderBy(r => r.SubjectName).ThenBy(r => r.SubjectName).ThenBy(r => r.Teachers).ToList();


            if (ascendingOrder)
            {
                var orderedList = formattedResult.OrderBy(r => r.Students).ThenBy(r => r.SubjectName).ThenBy(r => r.Teachers).ToList();

                ascendingOrder = false;
                return View("Index",orderedList);
            }
            else
            {
                var orderedList = formattedResult.OrderByDescending(r => r.Students).ThenByDescending(r => r.SubjectName).ThenBy(r => r.Teachers).ToList();

                ascendingOrder = true;
                return View("Index",orderedList);
            }

        }
        public IActionResult OrderBySubSutTec()
        {
            
            using Context myContext = new Context();

            var query = from subject in myContext.Subjects
                        join student in myContext.Students on subject.ID equals student.SId
                        join teacher in myContext.Teachers on subject.ID equals teacher.SId
                        group new { subject, student, teacher } by student.Name into subjectGroup
                        select new
                        {
                            SubjectName = subjectGroup.Select(g => g.subject.SubjectName).Distinct(),

                            Students = subjectGroup.Key,
                            Teachers = subjectGroup.Select(g => g.teacher.Name).Distinct()
                        };
            var result = query.ToList();
            var formattedResult = result.Select(r => new
            {
                Students = r.Students,
                SubjectName = string.Join(", ", r.SubjectName),
                Teachers = string.Join(", ", r.Teachers)
            }).ToList();

            //var orderedList = formattedResult.OrderBy(r => r.SubjectName).ThenBy(r => r.SubjectName).ThenBy(r => r.Teachers).ToList();


            if (ascendingOrder)
            {
                var orderedList = formattedResult.OrderBy(r => r.SubjectName).ThenBy(r => r.Students).ThenBy(r => r.Teachers).ToList();

                ascendingOrder = false;
                return View("Index", orderedList);
            }
            else
            {
                var orderedList = formattedResult.OrderByDescending(r => r.SubjectName).ThenByDescending(r => r.Students).ThenBy(r => r.Teachers).ToList();

                ascendingOrder = true;
                return View("Index", orderedList);
            }

        }
        public IActionResult OrderByTecStuSub()
        {
            
            using Context myContext = new Context();

            var query = from subject in myContext.Subjects
                        join student in myContext.Students on subject.ID equals student.SId
                        join teacher in myContext.Teachers on subject.ID equals teacher.SId
                        group new { subject, student, teacher } by student.Name into subjectGroup
                        select new
                        {
                            SubjectName = subjectGroup.Select(g => g.subject.SubjectName).Distinct(),

                            Students = subjectGroup.Key,
                            Teachers = subjectGroup.Select(g => g.teacher.Name).Distinct()
                        };
            var result = query.ToList();
            var formattedResult = result.Select(r => new
            {
                Students = r.Students,
                SubjectName = string.Join(", ", r.SubjectName),
                Teachers = string.Join(", ", r.Teachers)
            }).ToList();

            //var orderedList = formattedResult.OrderBy(r => r.SubjectName).ThenBy(r => r.SubjectName).ThenBy(r => r.Teachers).ToList();


            if (ascendingOrder)
            {
                var orderedList = formattedResult.OrderBy(r => r.Teachers ).ThenBy(r => r.Students).ThenBy(r => r.SubjectName).ToList();

                ascendingOrder = false;
                return View("Index", orderedList);
            }
            else
            {
                var orderedList = formattedResult.OrderByDescending(r => r.Teachers).ThenByDescending(r => r.Students).ThenBy(r => r.SubjectName).ToList();

                ascendingOrder = true;
                return View("Index", orderedList);
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}