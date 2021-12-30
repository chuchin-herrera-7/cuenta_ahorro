using cuenta_ahorro.EF;
using cuenta_ahorro.EF.Entities;
using cuenta_ahorro.EF.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
            _logger.LogInformation("Obteniendo clientes!!");
            return View(new ClientHelper(_dbContext).Get().Result.Value);
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        public ActionResult List()
        {
            return PartialView(new ClientHelper(_dbContext).Get().Result.Value);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Client _client)
        {
            try
            {
                _dbContext.Add(_client);
                await _dbContext.SaveChangesAsync();
                return Ok(new { message = "Nuevo cliente agregado exitosamente!!!" });
            }catch(Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest( new { message = "No se pudieron guardar los cambios." });
            }
        }
    }
}
