using cuenta_ahorro.EF;
using cuenta_ahorro.EF.Entities;
using cuenta_ahorro.EF.Helpers;
using cuenta_ahorro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace cuenta_ahorro.Controllers
{
    public class ManagementAccountController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ManagementAccountController> _logger;

        public ManagementAccountController(ApplicationDbContext dbContext, ILogger<ManagementAccountController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [AccessView(IsPartialView = false, Type = 1)]
        public ActionResult Index(int id)
        {
            return View(new ManagementAccountHelper(_dbContext).Get(id).Result.Value);
        }

        [AccessView(IsPartialView = false, Type = 1)]
        public async Task<ActionResult> Create([FromBody] ManagementAccount _managementAccount)
        {
            try
            {
                var _openingSavingHelper = new OpeningSavingAccountHelper(_dbContext).Get_(_managementAccount.IdOpeningSavingAccount).Result.Value;
                var _balanceDue = _managementAccount.Type == 1 ? _openingSavingHelper.Balance + _managementAccount.Amount :_openingSavingHelper.Balance - _managementAccount.Amount;
                
                if (_managementAccount.Amount <= 0)
                    return BadRequest(new { message = "El monto no puede ser menor e igual a 0" });
                if (_openingSavingHelper.Balance < _managementAccount.Amount && _managementAccount.Type == 0)
                    return BadRequest(new { message = "El monto a retirar no puede ser mayor al saldo actual " + _openingSavingHelper.Balance });

                _dbContext.Add(_managementAccount);
                await _dbContext.SaveChangesAsync();
                

                _openingSavingHelper.Balance = _balanceDue;
                await new OpeningSavingAccountHelper(_dbContext).Update(_openingSavingHelper);

                return Ok(new { message = string.Format("Nueva solicitud realizada") });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(new { message = "No se pudieron guardar los cambios." });
            }
        }
    }
}
