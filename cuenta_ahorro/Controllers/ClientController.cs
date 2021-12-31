using cuenta_ahorro.EF;
using cuenta_ahorro.EF.Entities;
using cuenta_ahorro.EF.Helpers;
using cuenta_ahorro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace cuenta_ahorro.Controllers
{
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ClientController> _logger;
        private readonly Email_ Email;

        public ClientController(ApplicationDbContext dbContext, ILogger<ClientController> logger, IConfiguration Configuration)
        {
            _dbContext = dbContext;
            _logger = logger;
            Email = new Email_(Configuration);
        }

        public ActionResult Index()
        {
            _logger.LogInformation("Obteniendo clientes!!");
            return View(new PersonHelper(_dbContext).Get().Result.Value);
        }

        public ActionResult Account()
        {
            var key = (int)HttpContext.Session.GetInt32("cuenta-ahorro-user-key");
            return View(new OpeningSavingAccountHelper(_dbContext).GetBySession(key).Result.Value);
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        public ActionResult List()
        {
            return PartialView(new PersonHelper(_dbContext).Get().Result.Value);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Person _person)
        {
            try
            {
                _dbContext.Add(_person);
                await _dbContext.SaveChangesAsync();
                SendMail(_person);
                return Ok(new { message = "Nuevo cliente agregado exitosamente!!!" });
            }catch(Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest( new { message = "No se pudieron guardar los cambios." });
            }
        }

        public string SendMail(Person _person)
        {
            try
            {
                Email.Subject = "Registro cuenta de ahorro";
                Email.Body = "<div>Bienvenido " +_person.FullName+ "  !!" +
                    " <br> Ahora puedes ingresar a tu cuenta y aperturar tu primera cuenta de ahorro " 
                    + "<br> usuario: " + _person.Email
                    + "<br> password: " + _person.Password
                    + "</div>";
                Email.To = new string[1] { _person.Email };
                Email.Send();
                return "Correo enviado";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
