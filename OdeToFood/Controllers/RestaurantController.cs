﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OdeToFood.Models;

namespace OdeToFood.Controllers
{
    public class RestaurantController : Controller
    {
        private IOdeToFoodDb _db;

        public RestaurantController(IOdeToFoodDb db)
        {
            // TODO: Complete member initialization
            this._db = db;
        }

        public RestaurantController()
        {
            // TODO: Complete member initialization
            _db = new OdeToFoodDb();
        }

        // GET: /Restaurant/
        public ActionResult Index()
        {
            return View(_db.Query<Restaurant>().ToList());
        }       

        // GET: /Restaurant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Restaurant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]    
        public ActionResult Create([Bind(Include="Id,Name,City,Country")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _db.Add<Restaurant>(restaurant);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(restaurant);
        }

        // GET: /Restaurant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = (_db.Query<Restaurant>() as DbSet<Restaurant>).Find(id);//.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: /Restaurant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,City,Country")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                //_db.Entry(restaurant).State = EntityState.Modified;
                _db.Update<Restaurant>(restaurant);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        // GET: /Restaurant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = (_db.Query<Restaurant>() as DbSet<Restaurant>).Find(id);//_db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: /Restaurant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurant restaurant = (_db.Query<Restaurant>() as DbSet<Restaurant>).Find(id);//_db.Restaurants.Find(id);
            _db.Remove<Restaurant>(restaurant);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
