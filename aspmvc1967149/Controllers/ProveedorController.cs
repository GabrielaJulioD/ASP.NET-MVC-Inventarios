using aspmvc1967149.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aspmvc1967149.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {
            using (var db = new inventarioEntities1())
            {
                return View(db.proveedor.ToList());
            }
        }

        public ActionResult DetalleProveProduc()
        {
            var db = new inventarioEntities();
            var query = from proveedor in db.proveedor
                        join producto in db.producto on proveedor.id equals producto.id_proveedor
                        select new ProductoProveedor
                        {
                            nombreProducto = producto.nombre,
                            nombreProveedor = proveedor.nombre,
                            telefonoProveedor = proveedor.telefono,
                            descripcion = producto.descripcion,
                            precioUnitario = producto.percio_unitario
                        };
            return View(query);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(proveedor newproveedor)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventarioEntities1())
                {
                    db.proveedor.Add(newproveedor);
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
                    proveedor findProveedor = db.proveedor.Where(a => a.id == id).FirstOrDefault();
                    return View(findProveedor);
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
        public ActionResult Edit(proveedor editProveedor)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    //consultar el usuario por id
                    proveedor proveedor = db.proveedor.Find(editProveedor.id);

                    //actualizar el usuario con el objeto que llega por parametro
                    proveedor.nombre = editProveedor.nombre;
                    proveedor.direccion = editProveedor.direccion;
                    proveedor.telefono = editProveedor.telefono;
                    proveedor.nombre_contacto = editProveedor.nombre_contacto;

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
                var findproveedor = db.proveedor.Find(id);
                return View(findproveedor);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    //consultar el usuario
                    var findProveedor = db.proveedor.Find(id);
                    //eliminar el usuario
                    db.proveedor.Remove(findProveedor);
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