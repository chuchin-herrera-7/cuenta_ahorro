using cuenta_ahorro.EF;
using cuenta_ahorro.EF.Entities;
using cuenta_ahorro.EF.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace cuenta_ahorro.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ClientController> _logger;

        public LoginController(ApplicationDbContext dbContext, ILogger<ClientController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SigIn(Person _person)
        {
            try
            {
                var _user = new PersonHelper(_dbContext).ValidateUser(_person.Email, _person.Password).Result.Value;
                if (_user == null)
                {
                    ModelState.AddModelError("", "Usuario o contraseña incorrectas");
                    return View("Index");
                }

                InitialSession(_user);
                if (HttpContext.Session.GetInt32("cuenta-ahorro-user-key") != null)
                {
                    if(_user.Type == 1)
                    {
                        return RedirectToAction("Account", "Client");
                    }
                        return RedirectToAction("Index", "Client");
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index");
            }
        }

        private void InitialSession(Person user)
        {
            HttpContext.Session.SetInt32("cuenta-ahorro-user-key", user.Id);
            HttpContext.Session.SetString("cuenta-ahorro-inventario-fullName", user.FullName);
            HttpContext.Session.SetString("cuenta-ahorro-inventario-email", user.Email);
            HttpContext.Session.SetInt32("cuenta-ahorro-type", user.Type);
        }
    }
}
