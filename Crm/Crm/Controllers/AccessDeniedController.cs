using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Crm.Controllers
{
    public class AccessDeniedController : Controller
    {
        //
        // GET: /AccessDenied/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /AccessDenied/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /AccessDenied/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AccessDenied/Create

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

        //
        // GET: /AccessDenied/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /AccessDenied/Edit/5

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

        //
        // GET: /AccessDenied/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /AccessDenied/Delete/5

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
    }
}
