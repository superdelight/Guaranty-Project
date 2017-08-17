using PonziRepostiory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuarantyWeb.Controllers
{
    public class TransactionDetailController : Controller
    {
      
        // GET: TransactionDetail
        public ActionResult Index()
        {
            return View();
        }

        // GET: TransactionDetail/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TransactionDetail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransactionDetail/Create
        [HttpPost]
        public ActionResult Create(TransactionDetail trans)
        {
            //try
            //{
            //    var det= Context.CreateNewPackage(trans.Description,(int)trans.BenefitPercentage,(bool)trans.IsActive,(int)trans.MaturityTime);

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
                return View();
           // }
        }

        // GET: TransactionDetail/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TransactionDetail/Edit/5
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

        // GET: TransactionDetail/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TransactionDetail/Delete/5
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
