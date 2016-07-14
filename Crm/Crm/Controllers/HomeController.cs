using Crm.Models;
using Crm.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Crm.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        //[CustomAuthorize(Roles = "superadmin,admin,user")]
        public ActionResult Index()
        {

            CRMEntities c = new CRMEntities();



            List<DefectView> newList = c.vW_Arızalar

             .Select(m => new DefectView
             {

                 ArızaID=m.DefectId,
                 ArızaDurumu=m.DefectStatus,
                 ArızaNot=m.DefectNote,
                 KayıtTarihi=m.DefectRecordDate,
                 MüdahaleEdenId=m.LastModificatorId,
                 MüdahaleEdenİsim = m.UserName,
                 MüdahaleNotu=m.LastModificationNote,
                 MüdahaleTarihi=m.LastModificationDate,
                 ProjeId=m.ProjectId,
                 Projeİsmi=m.ProjectName,
                 SikayetEden=m.ComplaintName,
                 SikayetEdenDetay=m.ComplaintDetail,
                 

             }).ToList();


            return View(newList);
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Home/Create

        [CustomAuthorize(Roles = "superadmin,admin")]
        public ActionResult Work()
        {
            return View();
        }

        //
        // POST: /Home/Create

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
        // GET: /Home/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Home/Edit/5

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
        // GET: /Home/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Home/Delete/5

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
