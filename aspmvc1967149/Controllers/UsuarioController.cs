using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspmvc1967149.Models;

namespace aspmvc1967149.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {
                return View(db.usuario.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(usuario newUser)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventarioEntities())
                {
                    db.usuario.Add(newUser);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }catch(Exception ex)
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
                using (var db = new inventarioEntities())
                {
                    usuario findUser = db.usuario.Where(a => a.id == id).FirstOrDefault();
                    return View(findUser);
                }
            }catch(Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(usuario editUser)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    //consultar el usuario por id
                    usuario user = db.usuario.Find(editUser.id);

                    //actualizar el usuario con el objeto que llega por parametro
                    user.nombre = editUser.nombre;
                    user.apellido = editUser.apellido;
                    user.email = editUser.email;
                    user.fecha_nacimiento = editUser.fecha_nacimiento;
                    user.password = editUser.password;

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
            using(var db = new inventarioEntities()){
                var findUser = db.usuario.Find(id);
                return View(findUser);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    //consultar el usuario
                    var findUser = db.usuario.Find(id);
                    //eliminar el usuario
                    db.usuario.Remove(findUser);
                    //actualizar la bd
                    db.SaveChanges();
                    //redireccionar al metodo index
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;
            }
        }
    }
}