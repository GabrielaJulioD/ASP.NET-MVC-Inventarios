using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspmvc1967149.Controllers;
using aspmvc1967149.Models;
using MySql.Data.MySqlClient.Memcached;

namespace aspmvc1967149.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            using(var db = new inventarioEntities1())
            {
                return View(db.cliente.ToList());
            }
           
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(cliente newcliente)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventarioEntities1())
                {
                    db.cliente.Add(newcliente);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    cliente findCliente = db.cliente.Where(a => a.id == id).FirstOrDefault();
                    return View(findCliente);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(cliente editCliente)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    //consultar el usuario por id
                    cliente cliente = db.cliente.Find(editCliente.id);

                    //actualizar el usuario con el objeto que llega por parametro
                    cliente.nombre = editCliente.nombre;
                    cliente.documento = editCliente.documento;
                    cliente.email = editCliente.email;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;
            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new inventarioEntities1())
            {
                var findCliente = db.cliente.Find(id);
                return View(findCliente);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    //consultar el usuario
                    var findCliente = db.cliente.Find(id);
                    //eliminar el usuario
                    db.cliente.Remove(findCliente);
                    //actualizar la bd
                    db.SaveChanges();
                    //redireccionar al metodo index
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;
            }
        }

    }
}