using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspmvc1967149.Models;

namespace aspmvc1967149.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            using (var db = new inventarioEntities1())
            {
                return View(db.producto.ToList());
            }
        }

        public static string NombreProveedor(int? idProveedor)
        {
            using (var db = new inventarioEntities1())
            {
                return db.proveedor.Find(idProveedor).nombre;
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(producto newProducto)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventarioEntities1())
                {
                    db.producto.Add(newProducto);
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

        public ActionResult listaProveedores()
        {
            using (var db = new inventarioEntities1())
            {
                return PartialView(db.proveedor.ToList());
            }
        }

        public ActionResult Edit(int id)
        {
            using (var db = new inventarioEntities1())
            {
                producto producto = db.producto.Where(a => a.id == id).FirstOrDefault();
                return View(producto);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(producto productoUpdate)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventarioEntities1())
                {
                    producto producto = db.producto.Find(productoUpdate.id);
                    producto.nombre = productoUpdate.nombre;
                    producto.cantidad = productoUpdate.cantidad;
                    producto.descripcion = productoUpdate.descripcion;
                    producto.percio_unitario = productoUpdate.percio_unitario;
                    producto.id_proveedor = productoUpdate.id_proveedor;
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
                return View(db.producto.Find(id));
            }
        }

        public ActionResult Delete(int id)
        {
            using (var db = new inventarioEntities1())
            {
                var producto = db.producto.Find(id);
                db.producto.Remove(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}

