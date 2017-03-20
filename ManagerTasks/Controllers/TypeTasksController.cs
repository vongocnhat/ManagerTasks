using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagerTasks.Models;
using PagedList;
using ManagerTasks.Common;
using System.Data.Entity;

namespace Tasks.Areas.Admin.Controllers
{
    public class TypeTasksController : Controller
    {
        // GET: Admin/CategoryTasks
        TasksDbContext db = null;
        public TypeTasksController()
        {
            db = new TasksDbContext();
        }


        public ActionResult Index(string searchString, int page = 1, string pageSize = "5", string searchColumn = "Title")
        {
            var Utilities = new Utilities();
            //string columnSearch = "Title";
            IEnumerable<TypeTask> model = db.TypeTasks;
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                switch (searchColumn)
                {
                    case "Category Title":
                        model = model.Where(x => Utilities.ConvertEn(x.CategoryTask.Title, searchString).ToLower().Contains(searchString));
                        break;
                    case "Title":
                        model = model.Where(x => Utilities.ConvertEn(x.Title, searchString).ToLower().Contains(searchString));
                        break;
                }
            }

            if (pageSize.ToString() == "All")
                model = model.OrderBy(x => x.IdCategoryTask).ToPagedList(page, model.Count());
            else
                model = model.OrderBy(x => x.IdCategoryTask).ToPagedList(page, int.Parse(pageSize));

            return Request.IsAjaxRequest() ? (ActionResult)PartialView("TypeTasksList", model) : View(model);
        }

        public void SetViewBag(int? selectedId = null)
        {
            ViewBag.IdCategoryTask = new SelectList(db.CategoryTasks.OrderBy(x => x.Id).ToList(), "Id", "Title", selectedId);
        }
        [HttpGet]
        public ActionResult Manager(int id)
        {
            SetViewBag();

            if (id == 0)
            {
                ViewBag.Title = "Create Type Task";
                return View();
            }
            ViewBag.Title = "Edit Type Task";
            var typeTask = db.TypeTasks.Where(x => x.Id == id).Single();
            return View(typeTask);
        }
        [HttpPost]
        public ActionResult Manager(TypeTask entity)
        {
            try
            {
             
                if (TempData["Create"].ToString() == "Create Type Task")
                {
                    db.TypeTasks.Add(entity);
                    db.SaveChanges();
                    TempData["Success"] = "Added Type Task: "+entity.Title+" Successfully!";
                }
                else
                {
                    var typeTask = db.TypeTasks.Where(x => x.Id == entity.Id).Single();

                    typeTask.Id = entity.Id;
                    typeTask.IdCategoryTask = entity.IdCategoryTask;                    
                    typeTask.Title = entity.Title;
                    db.SaveChanges();
                    TempData["Success"] = "Updated Type Task: "+entity.Title+" Successfully!";
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
                    var typeTask = db.TypeTasks.Find(int.Parse(Id));
                    db.TypeTasks.Remove(typeTask);
                }
                db.SaveChanges();
                TempData["Success"] = "Deleted Type Task(s) Successfully!";
            }
            catch (Exception ex)
            {
                TempData["Danger"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}