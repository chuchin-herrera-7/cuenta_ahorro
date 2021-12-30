using cuenta_ahorro.EF;
using cuenta_ahorro.EF.Entities;
using cuenta_ahorro.EF.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace cuenta_ahorro.Controllers
{
    public class OpeningController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<OpeningController> _logger;

        public OpeningController(ApplicationDbContext dbContext, ILogger<OpeningController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public ActionResult Index(int id)
        {
            return View(new OpeningSavingAccountHelper(_dbContext).Get(id).Result.Value);
        }

        public async Task<ActionResult> Create([FromBody] OpeningSavingAccount _openingSavingAccount)
        {
            try
            {
                _dbContext.Add(_openingSavingAccount);
                await _dbContext.SaveChangesAsync();
                return Ok(new { message = string.Format("Nueva cuenta de ahorro creada !!") });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(new { message = "No se pudieron guardar los cambios." });
            }
        }
    }
}
