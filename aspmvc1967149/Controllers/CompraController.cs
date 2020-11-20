using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspmvc1967149.Models;

namespace aspmvc1967149.Controllers
{
    public class CompraController : Controller
    {
        // GET: Compra
        public ActionResult Index()
        {
            using (var db = new inventarioEntities1())
            {
                return View(db.compra.ToList());
            }
        }

        public static string NombreUsuario(int? idUsuario)
        {
            using (var db = new inventarioEntities1())
            {
                return db.usuario.Find(idUsuario).nombre;
            }
        }

        public static string NombreCliente(int? idCliente)
        {
            using (var db = new inventarioEntities1())
            {
                return db.cliente.Find(idCliente).nombre;
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(compra newCompra)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventarioEntities1())
                {
                    db.compra.Add(newCompra);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
                throw;
            }
        }

        public ActionResult listaClientes()
        {
            using (var db = new inventarioEntities1())
            {
                return PartialView(db.cliente.ToList());
            }
        }

        public ActionResult listaUsuarios()
        {
            using (var db = new inventarioEntities1())
            {
                return PartialView(db.usuario.ToList());
            }
        }

        public ActionResult Edit(int id)
        {
            using (var db = new inventarioEntities1())
            {
                compra compra= db.compra.Where(a => a.id == id).FirstOrDefault();
                return View(compra);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(compra compraUpdate)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventarioEntities1())
                {
                    compra compra = db.compra.Find(compraUpdate.id);
                    compra.fecha = compraUpdate.fecha;
                    compra.total = compraUpdate.total;
                    compra.id_usuario = compraUpdate.id_usuario;
                    compra.id_cliente = compraUpdate.id_cliente;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
                throw;
            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new inventarioEntities1())
            {
                return View(db.compra.Find(id));
            }
        }

        public ActionResult Delete(int id)
        {
            using (var db = new inventarioEntities1())
            {
                var producto = db.compra.Find(id);
                db.compra.Remove(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}