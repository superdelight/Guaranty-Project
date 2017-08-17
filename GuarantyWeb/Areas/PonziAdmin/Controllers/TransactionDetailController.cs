using GuarantyWeb.Areas.PonziAdmin.Models;
using PonziBussinessLogic.Implementation;
using PonziBussinessLogic.Interface;
using PonziRepostiory;
using PonziRepostiory.Implementation;
using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuarantyWeb.Areas.PonziAdmin.Controllers
{
    public class TransactionDetailController : Controller
    {
        PonziAdminLogin Context = new PonziAdminLogin();
      
        // GET: PonziAdmin/TransactionDetail
        public TransactionDetailController()
        {

         
        
            
        }
        public ActionResult Index()
        {
            var det = Context.GetAllTransactions();
            return View(det.Result);
        }

        // GET: PonziAdmin/TransactionDetail/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PonziAdmin/TransactionDetail/Create
      
        public ActionResult Create()
        {
            return View();
        }

        // POST: PonziAdmin/TransactionDetail/Create
        [HttpPost]
        public ActionResult Create(TransactionDetail trans)
        {

            try
            {
              
              
                var det = Context.CreateNewTransactionDetail(trans);
                

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: PonziAdmin/TransactionDetail/Edit/5
        public ActionResult Edit(int id)
        {
            var trans = Context.GetSingleTransactionDetail(id);
           
            return View(trans.Result);
        }

        // POST: PonziAdmin/TransactionDetail/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TransactionDetail transDet)
        {
            try
            {
                Context.EditTransactionDetail(transDet);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PonziAdmin/TransactionDetail/Delete/5
        public ActionResult Delete(int id)
        {
            
            return View();
        }

        // POST: PonziAdmin/TransactionDetail/Delete/5
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
