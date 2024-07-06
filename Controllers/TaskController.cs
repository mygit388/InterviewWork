using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InterviewWork.DAL;
using InterviewWork.Models;

namespace InterviewWork.Controllers
{
    public class TaskController : Controller
    {
        TaskDAL _taskDAL = new TaskDAL();
        // GET: Task
        public ActionResult Index(int profileId)
        {
            ViewBag.ProfileId = profileId;
            var tasks = _taskDAL.GetTasks(profileId);
            return View(tasks);
        }

        // GET: Task/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Task/Create
        public ActionResult Create(int profileId)
        {
            ViewBag.ProfileId = profileId;
            return View();
        
        }

        // POST: Task/Create
        [HttpPost]
        public ActionResult Create(Task task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _taskDAL.InsertTask(task);
                    return RedirectToAction("Index", new { profileId = task.ProfileId });
                }
                ViewBag.ProfileId = task.ProfileId;
                return View(task);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
                return View();
            }
        }

        // GET: Task/Edit/5
        public ActionResult Edit(int id)
        {
            var task = _taskDAL.getTaskByID(id); 
            if (task == null)
            {
                TempData["InfoMessage"] = "Task Not Found";
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // POST: Task/Edit/5
        [HttpPost]
        public ActionResult Edit(Task task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _taskDAL.UpdateTask(task);
                    return RedirectToAction("Index", new { profileId = task.ProfileId });
                }
                return View(task);

                
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
                return View();
            }
        }

        // GET: Task/Delete/5
        public ActionResult Delete(int id)
        {
            var task = _taskDAL.getTaskByID(id);
            if (task == null)
            {
                TempData["InfoMessage"] = "Task Not Found";
                return RedirectToAction("Index", new { profileId = task.ProfileId });
            }
            return View(task);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                var task = _taskDAL.getTaskByID(id);
                _taskDAL.DeleteTask(id);
                return RedirectToAction("Index", new { profileId = task.ProfileId });

            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
                return View();
            }
        }
    }
}
