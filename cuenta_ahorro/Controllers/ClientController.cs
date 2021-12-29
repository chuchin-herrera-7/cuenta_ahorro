using cuenta_ahorro.EF;
using cuenta_ahorro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cuenta_ahorro.Controllers
{
    public class ClientController : Controller
    {
        public readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ClientController> _logger;

        public ClientController(ApplicationDbContext dbContext, ILogger<ClientController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public ActionResult Index()
        {
            return View(this.Get());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Create([FromBody]Client _client)
        {
            try
            {
                if (!ModelState.IsValid) 
                {
                    ModelState.AddModelError("", "No se pueden guardar los cambios. " +
                   "intenta de nuevo, y si el problema persiste " +
                   "contacta con el administrador.");
                }
                _dbContext.Add(_client);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            return View("Create");
        }

        public async Task<ActionResult> Edit(int Id)
        {
            var findClient = await _dbContext.Client.FindAsync(Id);
            if (findClient == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Edit(Client _client)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "No se pueden actualizar los cambios. " +
                   "intenta de nuevo, y si el problema persiste " +
                   "contacta con el administrador.");
                }

                var findClient = await _dbContext.Client.FindAsync(_client.Id);
                if(findClient == null)
                {
                    return NotFound();
                }

                _dbContext.Update(_client);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            return View("Edit");
        }

        private IEnumerable<Client> Get()
        {
            return _dbContext.Client;
        }
    }
}
