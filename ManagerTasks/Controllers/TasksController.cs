using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ManagerTasks.Models;
using ManagerTasks.Common;

namespace Tasks.Areas.Admin.Controllers
{
    public class TasksController : Controller
    {
        // GET: Admin/Tasks
        TasksDbContext db = null;
        public TasksController()
        {
            db = new TasksDbContext();
        }

        public ActionResult Index(string searchString, int page = 1, string pageSize = "5", string searchColumn="Task Title")
        {
            var Utilities = new Utilities();
            
            IEnumerable<Task> model = db.Tasks;
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                switch (searchColumn)
                {
                    case "Type Task Title":
                        model = model.Where(x => Utilities.ConvertEn(x.TypeTask.Title, searchString).ToLower().Contains(searchString));
                        break;
                    case "Task Title":
                        model = model.Where(x => Utilities.ConvertEn(x.Title, searchString).ToLower().Contains(searchString));
                        break;
                    case "From Date":
                        model = model.Where(x => x.FromDate.ToString().Contains(searchString));
                        break;
                    case "Deadline Date":
                        model = model.Where(x => x.DeadlineDate.ToString().Contains(searchString));
                        break;
                    case "Description":
                        model = model.Where(x => Utilities.ConvertEn(x.Description, searchString).ToLower().Contains(searchString));
                        break;
                    case "Unit Per":
                        model = model.Where(x => x.UnitPer == float.Parse(searchString));
                        break;
                    case "Is Active":
                        model = model.Where(x => x.IsActive == bool.Parse(searchString));
                        break;
                }
                
            }
            if (pageSize.ToString() == "All")
                model = model.OrderBy(x => x.Id).ToPagedList(page, model.Count());
            else
                model = model.OrderBy(x => x.Id).ToPagedList(page, int.Parse(pageSize));
            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("TasksList", model)
                : View(model);
        }
        public JsonResult GetCategoryTasks(int? selected = null)
        {
            var categoryTasks = db.CategoryTasks.ToList();
            db.Configuration.ProxyCreationEnabled = false;
            return Json(new SelectList(categoryTasks, "Id", "Title", selected), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTypeTasks(int idCategoryTask, int? selected = null)
        {
            var typeTasks = db.TypeTasks.Where(x => x.IdCategoryTask == idCategoryTask).OrderBy(x => x.Id).ToList();
            db.Configuration.ProxyCreationEnabled = false;
            return Json(new SelectList(typeTasks, "Id", "Title", selected), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Manager(int Id)
        {
            if (Id == 0)
            {
                ViewBag.Title = "Create Task";
                return View();
            }
            ViewBag.Title = "Edit Task";
            var task = db.Tasks.Find(Id);
            return View(task);
        }
        [HttpPost]
        public ActionResult Manager(Task entity)
        {
            try
            {
                //create
                if (entity.Id == 0)
                {
                    db.Tasks.Add(entity);
                    db.SaveChanges();
                    TempData["Success"] = "Added Task Successfully!";
                }
                else
                {
                    var task = db.Tasks.Find(entity.Id);
                    task.IdTypeTask = entity.IdTypeTask;
                    task.Title = entity.Title;
                    task.FromDate = entity.FromDate;
                    task.DeadlineDate = entity.DeadlineDate;
                    task.Description = entity.Description;
                    task.UnitPer = entity.UnitPer;
                    task.IsActive = entity.IsActive;
                    db.SaveChanges();
                    TempData["Success"] = "Updated Task Successfully!";
                }
            }
            catch (Exception ex)
            {
                TempData["Danger"] = ex.Message;
            }                
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(FormCollection formCollection)
        {
                         
            try
            {
                string[] Ids = formCollection["Id"].Split(new char[] { ',' });
                foreach (string Id in Ids)
                {
                    var task = db.Tasks.Find(int.Parse(Id));
                    db.Tasks.Remove(task);
                }
                db.SaveChanges();
                TempData["Success"] = "Deleted Task(s) Successfully!";
            }
            catch (Exception ex)
            {
                TempData["Danger"] = ex.Message;
            }
            return RedirectToAction("Index");
        }        
    }
}