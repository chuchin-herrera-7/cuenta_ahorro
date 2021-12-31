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
    public class OpeningController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<OpeningController> _logger;
        private readonly Email_ Email;

        public OpeningController(ApplicationDbContext dbContext, ILogger<OpeningController> logger, IConfiguration Configuration)
        {
            _dbContext = dbContext;
            _logger = logger;
            Email = new Email_(Configuration);
        }

        public ActionResult Index(int id)
        {
            return View(new OpeningSavingAccountHelper(_dbContext).Get(id).Result.Value);
        } 
        
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> NewOpeningSavingsAccount()
        {
            try
            {
                var _idClient = (int)HttpContext.Session.GetInt32("cuenta-ahorro-user-key");
                var _openingSavingAccountHelper = new OpeningSavingAccountHelper(_dbContext).GetLast().Result.Value;
                var consecutivo = 1;
                if(_openingSavingAccountHelper != null)
                {
                    consecutivo = (_openingSavingAccountHelper.Id + 1);
                }
                var accountNumber = "128923" + _idClient + consecutivo;

                OpeningSavingAccount _openingSavingAccount = new OpeningSavingAccount()
                {
                    Id = 0,
                    AccountNumber = accountNumber,
                    Balance = 0,
                    IdClient = _idClient
                };

                
                _dbContext.Add(_openingSavingAccount);
                await _dbContext.SaveChangesAsync();
                SendMail(accountNumber, _idClient);

                return Ok(new { message = string.Format("Nueva cuenta de ahorro creada !!") });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(new { message = "No se pudieron guardar los cambios." });
            }
        }

        private string SendMail(string accountNumber, int _idClient)
        {
            try
            {
                var _client = new PersonHelper(_dbContext).GetBy(_idClient).Result.Value;
                Email.Subject = "Apertura cuenta de ahorro";
                Email.Body = "<div>Hola "+ _client.FullName + "</br> Numero de cuenta: "+ accountNumber + "</div>";
                Email.To = new string[1] { _client.Email };
                Email.Send();
                return "Correo enviado";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] OpeningSavingAccount _openingSavingAccount)
        {
            try
            {
                if(_openingSavingAccount.Balance <= 0)
                    return BadRequest(new { message = "El saldo no puede ser menor e igual a 0" });

                _openingSavingAccount.IdClient = (int)HttpContext.Session.GetInt32("cuenta-ahorro-user-key");
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
