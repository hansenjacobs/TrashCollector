﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace TrashCollector.Controllers
{
    [Authorize(Roles =RoleName.Employee)]
    public class WorkOrderController : Controller
    {
        private ApplicationDbContext _context;

        public WorkOrderController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: WorkOrder
        public ActionResult Index()
        {
            var workOrders = _context.WorkOrders
                .Include(w => w.ServiceAddress.PostalCode.City.State)
                .Include(w => w.Status)
                .Include(w => w.Type)
                .Include(w => w.Customer);
            return View(workOrders);
        }

        public ActionResult EmployeeDashboard()
        {
            int? userServicePostalCode = Employee.GetEmployeeById(_context, User.Identity.GetUserId()).ServicePostalCodeId;

            List<WorkOrder> workOrders;

            if (userServicePostalCode != null && userServicePostalCode > 0)
            {
                workOrders = _context.WorkOrders
                .Include(w => w.ServiceAddress.PostalCode.City.State)
                .Include(w => w.Status)
                .Include(w => w.Type)
                .Include(w => w.Customer)
                .Where(w => w.ServiceAddress.PostalCodeId == userServicePostalCode)
                .ToList();
            }
            else
            {
                return RedirectToAction("Index");
            }
            
            return View(workOrders);
        }

        // GET: WorkOrder/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WorkOrder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkOrder/Create
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

        // GET: WorkOrder/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WorkOrder/Edit/5
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

        // GET: WorkOrder/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WorkOrder/Delete/5
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