using PonziBussinessLogic.Implementation;
using PonziRepostiory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuarantyWeb.Areas.PonziAdmin.Controllers
{
    public class TransactionSplitController : Controller
    {
        PonziAdminLogin Context = new PonziAdminLogin();
        // GET: PonziAdmin/TransactionSplit
        public ActionResult Index()
        {
            try
            {
                ViewBag.msg = "";
                ViewBag.trans = new SelectList(Context.GetAllTransactions().Result, "Id", "Description");
            //    var det = Context.GetAllTransactionSplit((int)trans);

                return View();

            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Index(int trans)
        {
            try
            {
                var det = Context.GetAllTransactionSplit(trans);
                ViewBag.trans = new SelectList(Context.GetAllTransactions().Result, "Id", "Description",trans);
                return View(det.Result);
            }
            catch
            {
                return View();
            }
        }
        // GET: PonziAdmin/TransactionSplit/Details/5
        public ActionResult Details(int id)
        {
            var msg = Context.GetSingleTransactionSplit(id);
            return View(msg.Result);
        }

        // GET: PonziAdmin/TransactionSplit/Create
        public ActionResult Create()
        {
            ViewBag.trans = new SelectList(Context.GetAllTransactions().Result, "Id", "Description");
            return View();
        }

        // POST: PonziAdmin/TransactionSplit/Create
        [HttpPost]
        public ActionResult Create(TransactionSplit transSplit)
        {
            try
            {
            
                var msg= Context.CreateNewTransactionSplit(transSplit);
                ViewBag.trans = new SelectList(Context.GetAllTransactions().Result, "Id", "Description", transSplit.TransId);

                if (msg.Result == false)
                {
                    ViewBag.msg = msg.Message;
                    return View();
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: PonziAdmin/TransactionSplit/Edit/5
        public ActionResult Edit(int id)
        {

            ViewBag.trans = new SelectList(Context.GetAllTransactions().Result, "Id", "Description");
            var msg = Context.GetSingleTransactionSplit(id);
            return View(msg.Result);
        }

        // POST: PonziAdmin/TransactionSplit/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TransactionSplit trans)
        {
            try
            {
                // TODO: Add update logic here
                var msg = Context.EditTransactionSplit(trans);
                ViewBag.trans = new SelectList(Context.GetAllTransactions().Result, "Id", "Description",trans.TransId);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PonziAdmin/TransactionSplit/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PonziAdmin/TransactionSplit/Delete/5
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
