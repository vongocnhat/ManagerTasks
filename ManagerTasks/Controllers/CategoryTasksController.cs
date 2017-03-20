using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagerTasks.Models;
using PagedList;
using ManagerTasks.Common;

namespace Tasks.Areas.Admin.Controllers
{
    public class CategoryTasksController : Controller
    {
        // GET: Admin/CategoryTasks
        TasksDbContext db = null;
        public CategoryTasksController()
        {
            db = new TasksDbContext();
        }

        public ActionResult Index(string searchString, int page = 1, string pageSize = "5", string searchColumn = "Category Title")
        {
            var Utilities = new Utilities();

            IEnumerable<CategoryTask> model = db.CategoryTasks;
            if (!string.IsNullOrEmpty(searchString))
            {                
                searchString = searchString.ToLower();
                switch (searchColumn)
                {
                    case "Category Title":
                        model = model.Where(x => Utilities.ConvertEn(x.Title, searchString).ToLower().Contains(searchString));
                        break;
                    case "Description":
                        model = model.Where(x => Utilities.ConvertEn(x.Description, searchString).ToLower().Contains(searchString));
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
                ? (ActionResult)PartialView("CategoryTasksList", model)
                : View(model);
        }
        [HttpGet]
        public ActionResult Manager(int Id)
        {
            if (Id == 0)
            {
                ViewBag.Title = "Create Category Task";
                return View();
            }
            ViewBag.Title = "Edit Category Task";
            var categoryTask = db.CategoryTasks.Find(Id);
            return View(categoryTask);
        }
        [HttpPost]
        public ActionResult Manager(CategoryTask entity)
        {
            try
            {
                //create
                if (entity.Id == 0)
                {
                    db.CategoryTasks.Add(entity);
                    db.SaveChanges();
                    TempData["Success"] = "Added Category Task Successfully!";
                }
                else
                {
                    var categoryTask = db.CategoryTasks.Find(entity.Id);
                    categoryTask.Title = entity.Title;
                    categoryTask.Description = entity.Description;
                    categoryTask.IsActive = entity.IsActive;
                    db.SaveChanges();
                    TempData["Success"] = "Updated Category Task Successfully!";
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
                    var categoryTask = db.CategoryTasks.Find(int.Parse(Id));
                    db.CategoryTasks.Remove(categoryTask);
                }
                db.SaveChanges();
                TempData["Success"] = "Deleted Category Task(s) Successfully!";
            }
            catch (Exception ex)
            {
                TempData["Danger"] = ex.Message + "Details: Item selected is available in tasks table!";
            }
            return RedirectToAction("Index");
        }
    }
}